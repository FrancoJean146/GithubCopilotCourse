---
name: "Explicar modulo"
description: "Explica un módulo o archivo del repositorio con una estructura fija: propósito, dependencias, flujo, riesgos y mejoras."
agent: ask
---

# Explicar un módulo

Analiza el archivo o la carpeta indicada (usa la selección activa o `#file` si se proporciona)
y responde **en español**, respetando exactamente esta estructura y estos títulos:

## 1. Propósito
Una o dos frases: qué problema resuelve el módulo y quién lo usa.

## 2. Dependencias
Tabla con las dependencias entrantes y salientes:

| Dependencia | Tipo (entrante/saliente) | Para qué se usa |
| --- | --- | --- |

## 3. Flujo principal
Lista numerada del recorrido de una llamada típica, desde el punto de entrada hasta el retorno,
indicando los tipos que intervienen.

## 4. Riesgos
Máximo cinco puntos. Cada punto indica el riesgo, el archivo y la línea aproximada.
Cubre al menos: validación de entradas, manejo de errores, secretos y concurrencia.

## 5. Oportunidades de mejora
Máximo cinco puntos, ordenados por relación valor/esfuerzo. Cada punto propone un cambio concreto
y verificable (no generalidades).

## Reglas
- No inventes tipos, métodos ni archivos: cita solo lo que existe en el código.
- Si algo no se puede determinar con el contexto disponible, escribe "No determinable con el contexto actual".
- No propongas cambios de framework: el proyecto es .NET 8.
