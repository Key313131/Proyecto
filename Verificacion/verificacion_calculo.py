"""
Verificación del módulo matemático de EcoFacility con Python (numpy y, si está, scipy).

Calcula el MISMO ejemplo que la prueba del programa en C#, pero con otros métodos:
  - derivadas: fórmula exacta y diferencias centrales con numpy
  - integrales: scipy.integrate (dblquad, tplquad) o, sin scipy, una malla fina con numpy
  - Lagrange: scipy.optimize.minimize (SLSQP) o, sin scipy, bisección sobre λ

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
ZONA = dict(ancho=20.0, largo=10.0, x=0.0, y=0.0, kc=1.2, hmin=30.0, raiz=0.3)
T, H, S = 26.0, 25.0, 12.0
SENSORES = [(3.0, 3.0, 22.5), (17.0, 2.0, 28.0), (10.0, 8.0, 31.0)]
POROSIDAD = 0.4
HOBJ = min(100.0, ZONA["hmin"] + 15)
AREA = ZONA["ancho"] * ZONA["largo"]


# ---------------- 1. Función de varias variables W(T, H, A, S) ----------------
def et0(t, s):
    p = s / 4380 * 100
    return max(0.0, p * (0.46 * t + 8.13))


def necesidad(t, h, a, s, hobj=HOBJ):
    reposicion = max(0.0, hobj - h) / 100 * ZONA["raiz"] * 1000 * POROSIDAD
    return a * (ZONA["kc"] * et0(t, s) + reposicion)


W = necesidad(T, H, AREA, S)


# ---------------- 2. Gradiente de W: exacto y numérico ----------------
def gradiente_exacto(t, h, a, s):
    p = s / 4380 * 100
    dT = a * ZONA["kc"] * p * 0.46
    dH = -a * ZONA["raiz"] * 1000 * POROSIDAD / 100 if h < HOBJ else 0.0
    dA = necesidad(t, h, a, s) / a
    dS = a * ZONA["kc"] * (0.46 * t + 8.13) * 100 / 4380
    return np.array([dT, dH, dA, dS])


def gradiente_numerico(f, punto, h=1e-5):
    punto = np.asarray(punto, dtype=float)
    g = np.zeros_like(punto)
    for i in range(len(punto)):
        e = np.zeros_like(punto)
        e[i] = h * max(1.0, abs(punto[i]))
        g[i] = (f(*(punto + e)) - f(*(punto - e))) / (2 * e[i])
    return g


grad_exacto = gradiente_exacto(T, H, AREA, S)
grad_numerico = gradiente_numerico(necesidad, [T, H, AREA, S])


# ---------------- 3. Campo de humedad h(x, y) (distancia inversa) ----------------
def campo(x, y):
    suma, pesos = 0.0, 0.0
    for sx, sy, sh in SENSORES:
        d2 = (x - sx) ** 2 + (y - sy) ** 2
        if d2 < 1e-12:
            return sh
        w = 1 / d2
        suma += w * sh
        pesos += w
    return suma / pesos


def integral_doble(f, a, b, c, d):
    """∬ f(x, y) dy dx sobre [a, b] × [c, d]."""
    if HAY_SCIPY:
        valor, _ = integrate.dblquad(lambda y, x: f(x, y), a, b, c, d, epsabs=1e-7, epsrel=1e-7)
        return valor
    # Sin scipy: punto medio en una malla de 800 × 800
    n = 800
    xs = a + (np.arange(n) + 0.5) * (b - a) / n
    ys = c + (np.arange(n) + 0.5) * (d - c) / n
    total = sum(f(x, y) for x in xs for y in ys)
    return total * (b - a) * (d - c) / (n * n)


humedad_promedio = integral_doble(campo, 0, ZONA["ancho"], 0, ZONA["largo"]) / AREA

paso = 0.005 * max(ZONA["ancho"], ZONA["largo"])
cx, cy = ZONA["ancho"] / 2, ZONA["largo"] / 2
grad_h = ((campo(cx + paso, cy) - campo(cx - paso, cy)) / (2 * paso),
          (campo(cx, cy + paso) - campo(cx, cy - paso)) / (2 * paso))

factor = ZONA["raiz"] * 1000 * POROSIDAD / 100
volumen_faltante = integral_doble(lambda x, y: max(0.0, HOBJ - campo(x, y)) * factor,
                                  0, ZONA["ancho"], 0, ZONA["largo"])


# ---------------- 4. Terreno z = f(x, y) ----------------
def altura(x, y):
    return 4.0 * math.exp(-((x - 50) ** 2 + (y - 50) ** 2) / (2 * 30.0 ** 2)) + 0.02 * x


tx, ty = ZONA["x"] + cx, ZONA["y"] + cy
d = 1e-5
grad_z = ((altura(tx + d, ty) - altura(tx - d, ty)) / (2 * d),
          (altura(tx, ty + d) - altura(tx, ty - d)) / (2 * d))
pendiente = math.hypot(*grad_z) * 100


# ---------------- 5. Integral triple ----------------
def agua_en_suelo():
    f = lambda z, y, x: POROSIDAD * campo(x, y) / 100 * 1000
    if HAY_SCIPY:
        valor, _ = integrate.tplquad(f, 0, ZONA["ancho"], 0, ZONA["largo"], 0, ZONA["raiz"],
                                     epsabs=1e-5, epsrel=1e-6)
        return valor
    # El integrando no depende de z: ∭ = profundidad × ∬
    return ZONA["raiz"] * integral_doble(lambda x, y: f(0, y, x), 0, ZONA["ancho"], 0, ZONA["largo"])


volumen_suelo = AREA * ZONA["raiz"]
agua = agua_en_suelo()


# ---------------- 6. Green: área de una zona irregular (forma de L) ----------------
L_FORMA = [(0, 0), (12, 0), (12, 4), (5, 4), (5, 9), (0, 9)]


def area_green(v):
    return abs(sum(v[i][0] * v[(i + 1) % len(v)][1] - v[(i + 1) % len(v)][0] * v[i][1]
                   for i in range(len(v)))) / 2


def dentro_L(x, y):
    return (0 <= x <= 12 and 0 <= y <= 4) or (0 <= x <= 5 and 0 <= y <= 9)


# Comparación: ∬ 1 dA contando puntos de una malla fina que caen dentro de la L
n = 2000
xs = (np.arange(n) + 0.5) * 12 / n
ys = (np.arange(n) + 0.5) * 9 / n
X, Y = np.meshgrid(xs, ys)
dentro = ((X <= 12) & (Y <= 4)) | ((X <= 5) & (Y <= 9))
area_malla = dentro.sum() * (12 / n) * (9 / n)


# ---------------- 7. Flujo por el tubo ----------------
R = 0.0127
caudal_l_min = max(2.0, AREA * 0.05)                 # el mismo del simulador
q = caudal_l_min / 1000 / 60
vmax = 2 * q / (math.pi * R ** 2)
if HAY_SCIPY:
    flujo, _ = integrate.dblquad(lambda r, th: vmax * (1 - r ** 2 / R ** 2) * r, 0, 2 * math.pi, 0, R)
else:
    flujo = integral_doble(lambda r, th: vmax * (1 - r ** 2 / R ** 2) * r, 0, R, 0, 2 * math.pi)
flujo_l_min = flujo * 1000 * 60


# ---------------- 8. Lagrange: reparto de un presupuesto ----------------
# (nombre, área, Kc, Hmin, raíz, humedad actual)
ZONAS = [("Prado", 200.0, 1.0, 30.0, 0.3, 26.0),
         ("Jardín", 120.0, 0.8, 35.0, 0.4, 33.0),
         ("Huerta", 60.0, 1.2, 40.0, 0.25, 30.0)]


def necesidad_zona(area, kc, hmin, raiz, h, hobj):
    return area * (kc * et0(T, S) + max(0.0, hobj - h) / 100 * raiz * 1000 * POROSIDAD)


Wz = np.array([necesidad_zona(a, kc, hm, pr, h, min(100, hm + 15)) for _, a, kc, hm, pr, h in ZONAS])
Mz = np.array([necesidad_zona(a, kc, hm, pr, h, hm) for _, a, kc, hm, pr, h in ZONAS])
pz = np.array([kc / a for _, a, kc, _, _, _ in ZONAS])
B = float(Mz.sum() + 0.4 * (Wz.sum() - Mz.sum()))   # entre el mínimo y lo ideal


def reparto_biseccion(lam):
    return np.maximum(Mz, Wz - lam / (2 * pz))


# Σ x(λ) baja cuando λ sube: se busca el λ con Σ x = B por bisección
lo, hi = 0.0, float(2 * (pz * (Wz - Mz)).max()) + 1
for _ in range(200):
    mid = (lo + hi) / 2
    if reparto_biseccion(mid).sum() > B:
        lo = mid
    else:
        hi = mid
lam = (lo + hi) / 2
x_biseccion = reparto_biseccion(lam)

if HAY_SCIPY:
    res = optimize.minimize(lambda x: float(np.sum(pz * (Wz - x) ** 2)), x0=Mz.copy(), method="SLSQP",
                            bounds=[(m, None) for m in Mz],
                            constraints=[{"type": "eq", "fun": lambda x: x.sum() - B}],
                            options={"ftol": 1e-12, "maxiter": 500})
    x_scipy = res.x
else:
    x_scipy = x_biseccion

# ---------------- Resultados ----------------
resultados = {
    "scipy": HAY_SCIPY,
    "et0": et0(T, S),
    "W": W,
    "gradW_exacto": grad_exacto.tolist(),
    "gradW_numerico": grad_numerico.tolist(),
    "humedad_promedio": humedad_promedio,
    "grad_h": list(grad_h),
    "volumen_faltante_L": volumen_faltante,
    "pendiente_pct": pendiente,
    "volumen_suelo_m3": volumen_suelo,
    "agua_en_suelo_L": agua,
    "area_L_green": area_green(L_FORMA),
    "area_L_malla": float(area_malla),
    "caudal_L_min": caudal_l_min,
    "vmax_m_s": vmax,
    "flujo_L_min": flujo_l_min,
    "presupuesto_L": B,
    "lambda": lam,
    "reparto_biseccion": x_biseccion.tolist(),
    "reparto_scipy": np.asarray(x_scipy).tolist(),
}

print(f"scipy disponible: {HAY_SCIPY}")
print(f"ET0 = {resultados['et0']:.4f} mm/día   W = {W:.3f} L/día")
print("∇W exacto   =", np.round(grad_exacto, 4))
print("∇W numérico =", np.round(grad_numerico, 4))
print(f"Humedad promedio (1/A)∬h dA = {humedad_promedio:.4f} %   ∇h centro = ({grad_h[0]:.4f}, {grad_h[1]:.4f})")
print(f"Volumen faltante ∬ max(0, Hobj − h)·Pr·θ dA = {volumen_faltante:.3f} L")
print(f"Pendiente del terreno = {pendiente:.4f} %")
print(f"∭ dV = {volumen_suelo:.3f} m³   agua en el suelo = {agua:.3f} L")
print(f"Zona en L: Green = {resultados['area_L_green']:.4f} m²   malla = {area_malla:.4f} m²")
print(f"Tubo: Q = {caudal_l_min:.4f} L/min, vmax = {vmax:.5f} m/s, ∬ v·n dS = {flujo_l_min:.4f} L/min")
print(f"Lagrange: B = {B:.3f} L, λ = {lam:.6f}")
print("   bisección:", np.round(x_biseccion, 3), " suma", round(float(x_biseccion.sum()), 3))
print("   scipy    :", np.round(x_scipy, 3), " suma", round(float(np.sum(x_scipy)), 3))

ruta = os.path.join(os.path.dirname(os.path.abspath(__file__)), "resultados_python.json")
with open(ruta, "w", encoding="utf-8") as archivo:
    json.dump(resultados, archivo, indent=2)
print("Guardado en", ruta)
