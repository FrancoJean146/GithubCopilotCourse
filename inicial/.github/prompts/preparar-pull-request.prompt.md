---
name: "Preparar pull request"
description: "Redacta el título, resumen, cambios, pruebas y checklist de un pull request a partir del diff de la rama."
agent: agent
---

# Preparar el pull request

Obtén el diff de la rama actual contra `main` (`git diff main...HEAD` y `git status`) y redacta la
descripción del pull request **en español**, siguiendo la plantilla del repositorio
(`.github/pull_request_template.md`).

## Salida esperada

1. **Título**: una línea, imperativo, máximo 72 caracteres, con el prefijo de la historia
   (por ejemplo `HU-01: actualizar el precio de un producto aplicando descuentos`).
2. **Resumen**: dos o tres frases sobre el problema y la solución, sin detalles de implementación.
3. **Historia de usuario**: identificador y criterios de aceptación cubiertos.
4. **Cambios**: lista por archivo, agrupada por área (API, servicios, pruebas, configuración),
   explicando el porqué de cada cambio.
5. **Pruebas**: comandos ejecutados y su resultado real, pruebas agregadas y casos borde cubiertos.
6. **Riesgos**: impacto, compatibilidad y plan de reversión.
7. **Decisiones sobre sugerencias de Copilot**: tabla con sugerencias aceptadas, modificadas y
   descartadas, con el motivo de cada decisión.
8. **Checklist**: la de la plantilla, marcando solo lo que realmente se verificó.

## Reglas

- Básate únicamente en el diff: no inventes cambios ni resultados de pruebas.
- Si el diff toca configuración sensible, indícalo de forma explícita en Riesgos.
- Si hay archivos generados o formateo masivo, señálalos aparte para facilitar la revisión.
