"""
Verificación del módulo matemático (plan de riego) de EcoFacility con Python.

Calcula el MISMO ejemplo que la prueba del programa en C#, pero con otros métodos:
  - W(T, H, A, S): misma fórmula, escrita de nuevo
  - caudal del tubo: integral doble con scipy.integrate.dblquad (o malla fina con numpy)
  - reparto de Lagrange: scipy.optimize.minimize (SLSQP) y bisección sobre λ
  - consumo real ∫ Q dt: numpy.trapezoid

Uso:
    pip install numpy scipy
    python verificacion_calculo.py

Escribe los resultados en resultados_python.json (junto a este archivo).
"""
import json
import math
import os

import numpy as np

try:
    from scipy import integrate, optimize
    HAY_SCIPY = True
except ImportError:
    HAY_SCIPY = False

# ---------------- Datos del ejemplo (iguales a los de la prueba en C#) ----------------
T, S = 26.0, 12.0                    # temperatura prevista y horas de luz
POROSIDAD = 0.4
HORAS_DISPONIBLES = 1.5              # ventana de riego
PRESUPUESTO, TARIFA = 60000.0, 5000.0
R, VMAX = 0.0254, 1.2                # tubo principal de 2" y velocidad en el centro

# (nombre, ancho, largo, Kc, Hmin, raíz, humedad medida)
ZONAS = [("Prado", 20.0, 10.0, 1.0, 30.0, 0.3, 26.0),
         ("Jardín", 12.0, 10.0, 0.8, 35.0, 0.4, 33.0),
         ("Huerta", 10.0, 6.0, 1.2, 40.0, 0.25, 30.0)]


# ---------------- 1. Necesidad de agua W(T, H, A, S) ----------------
def et0(t, s):
    p = s / 4380 * 100
    return max(0.0, p * (0.46 * t + 8.13))


def necesidad(area, kc, raiz, h, hobj):
    reposicion = max(0.0, hobj - h) / 100 * raiz * 1000 * POROSIDAD
    return area * (kc * et0(T, S) + reposicion)


A = np.array([an * la for _, an, la, _, _, _, _ in ZONAS])
W = np.array([necesidad(an * la, kc, pr, h, min(100, hm + 15)) for _, an, la, kc, hm, pr, h in ZONAS])
M = np.array([necesidad(an * la, kc, pr, h, hm) for _, an, la, kc, hm, pr, h in ZONAS])
p = np.array([kc / (an * la) for _, an, la, kc, _, _, _ in ZONAS])


# ---------------- 2. Agua disponible: flujo por el tubo (integral doble) ----------------
def integrando(r, theta):
    return VMAX * (1 - r ** 2 / R ** 2) * r      # v · n · jacobiano r


if HAY_SCIPY:
    q_m3s, _ = integrate.dblquad(lambda r, th: integrando(r, th), 0, 2 * math.pi, 0, R, epsabs=1e-12, epsrel=1e-12)
else:
    n = 2000                                     # punto medio en una malla fina
    rs = (np.arange(n) + 0.5) * R / n
    q_m3s = float(np.sum(integrando(rs, 0)) * (R / n) * 2 * math.pi)

q_formula = math.pi * R ** 2 * VMAX / 2
caudal_l_min = q_m3s * 1000 * 60
agua_disponible = caudal_l_min * HORAS_DISPONIBLES * 60
litros_por_dinero = PRESUPUESTO / TARIFA * 1000
B = min(agua_disponible, litros_por_dinero)


# ---------------- 3. Reparto: minimizar Σ p(W − x)² con Σx = B y x ≥ M ----------------
def reparto(lam):
    return np.maximum(M, W - lam / (2 * p))


lo, hi = 0.0, float((2 * p * (W - M)).max()) + 1
for _ in range(200):                              # bisección: Σ x(λ) baja cuando λ sube
    mid = (lo + hi) / 2
    if reparto(mid).sum() > B:
        lo = mid
    else:
        hi = mid
lam = (lo + hi) / 2
x_biseccion = reparto(lam)

if HAY_SCIPY:
    res = optimize.minimize(lambda x: float(np.sum(p * (W - x) ** 2)), x0=M.copy(), method="SLSQP",
                            bounds=[(m, None) for m in M],
                            constraints=[{"type": "eq", "fun": lambda x: x.sum() - B}],
                            options={"ftol": 1e-12, "maxiter": 500})
    x_scipy = res.x
else:
    x_scipy = x_biseccion

# ---------------- 4. Horario: una zona a la vez, la más seca (respecto a su mínimo) primero ----------------
orden = sorted(range(len(ZONAS)), key=lambda i: ZONAS[i][6] - ZONAS[i][4])
minutos = {ZONAS[i][0]: float(x_biseccion[i] / caudal_l_min) for i in orden}

# ---------------- 5. Consumo real: V = ∫ Q dt con la regla del trapecio ----------------
# Un medidor que lee cada 10 min: riega a 12 L/min entre los minutos 30 y 90, se apaga,
# y luego hay un hueco de 60 min sin lecturas (simulador apagado) que no se cuenta.
SERIE = [(0, 0), (10, 0), (20, 0), (30, 12), (40, 12), (50, 12), (60, 12), (70, 12), (80, 12), (90, 12),
         (100, 0), (110, 0), (170, 8), (180, 8), (190, 0)]
HUECO_MAX = 20     # min
tramos = []        # pedazos de la serie sin huecos
actual = [SERIE[0]]
for anterior, punto in zip(SERIE, SERIE[1:]):
    if punto[0] - anterior[0] > HUECO_MAX:
        tramos.append(actual)
        actual = [punto]
    else:
        actual.append(punto)
tramos.append(actual)
consumo = sum(float(np.trapezoid([q for _, q in t], [m for m, _ in t])) for t in tramos if len(t) > 1)
AREA_MEDIDA, LAMINA = 200.0, 5.0
dias = (SERIE[-1][0] - SERIE[0][0]) / (24 * 60)
tradicional = LAMINA * AREA_MEDIDA * dias
ahorro = (tradicional - consumo) / tradicional * 100

resultados = {
    "scipy": HAY_SCIPY,
    "consumo_serie": SERIE,
    "consumo_L": consumo,
    "tradicional_L": tradicional,
    "ahorro_pct": ahorro,
    "et0": et0(T, S),
    "W": W.tolist(),
    "M": M.tolist(),
    "caudal_L_min": caudal_l_min,
    "caudal_formula_L_min": q_formula * 1000 * 60,
    "agua_disponible_L": agua_disponible,
    "litros_por_dinero": litros_por_dinero,
    "B": B,
    "lambda": lam,
    "reparto": x_biseccion.tolist(),
    "reparto_scipy": np.asarray(x_scipy).tolist(),
    "orden": [ZONAS[i][0] for i in orden],
    "minutos": minutos,
}

print(f"scipy disponible: {HAY_SCIPY}")
print(f"ET0 = {resultados['et0']:.4f} mm/día")
for i, z in enumerate(ZONAS):
    print(f"   {z[0]:7s} W = {W[i]:9.3f} L   M = {M[i]:9.3f} L")
print(f"Tubo: ∬ v·n dS = {caudal_l_min:.4f} L/min   (fórmula πR²vmax/2 = {q_formula * 60000:.4f})")
print(f"Agua disponible V = Q·t = {agua_disponible:.2f} L   se puede pagar {litros_por_dinero:.2f} L   →  B = {B:.2f} L")
print(f"Lagrange: λ = {lam:.6f}")
print("   bisección:", np.round(x_biseccion, 3), " suma", round(float(x_biseccion.sum()), 3))
print("   scipy    :", np.round(x_scipy, 3), " suma", round(float(np.sum(x_scipy)), 3))
print("Horario:", ", ".join(f"{n} {m:.1f} min" for n, m in minutos.items()))
print(f"Consumo ∫Q dt (trapecio, sin el hueco) = {consumo:.2f} L   tradicional = {tradicional:.2f} L   ahorro = {ahorro:.2f} %")

ruta = os.path.join(os.path.dirname(os.path.abspath(__file__)), "resultados_python.json")
with open(ruta, "w", encoding="utf-8") as archivo:
    json.dump(resultados, archivo, indent=2, ensure_ascii=False)
print("Guardado en", ruta)
