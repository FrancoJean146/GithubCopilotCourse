---
name: "Documentar con XML"
description: "Agrega documentación XML en español a los miembros públicos del archivo indicado, sin cambiar el comportamiento."
agent: agent
---

# Documentar los miembros públicos con XML

Agrega documentación XML (`///`) **en español** a todos los miembros públicos del archivo indicado
(selección activa o `#file`).

## Requisitos

- Cubre: tipos, constructores, métodos, propiedades, campos públicos, miembros de enum y parámetros
  posicionales de los `record`.
- Etiquetas obligatorias según corresponda:
  - `<summary>`: qué hace, en una frase, sin repetir el nombre del miembro.
  - `<param name="...">`: significado y unidad o rango válido.
  - `<returns>`: qué devuelve y qué significa `null` si puede devolverlo.
  - `<exception cref="...">`: cada excepción que el miembro lanza de forma documentada.
  - `<remarks>`: reglas de negocio relevantes (por ejemplo, topes y redondeos).
- En los `record` posicionales usa `<param name="...">` en el comentario del tipo.

## Restricciones

- **No modifiques la firma ni el cuerpo de ningún miembro.** Solo agregas comentarios.
- No documentes miembros privados salvo que la lógica sea no evidente.
- Sin acentos ni caracteres especiales en los identificadores; en los textos mantén el estilo del resto del repositorio.
- Al terminar, compila con `dotnet build` y confirma que no aparecen advertencias CS1591.
