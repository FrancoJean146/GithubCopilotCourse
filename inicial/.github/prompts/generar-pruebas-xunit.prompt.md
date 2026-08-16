---
name: "Generar pruebas xUnit"
description: "Genera pruebas unitarias xUnit para un tipo, cubriendo casos normales, casos borde y entradas inválidas."
agent: agent
---

# Generar pruebas xUnit

Genera pruebas unitarias para el tipo indicado (selección activa o `#file`) y escríbelas en
`tests/Catalogo.Api.Tests/<NombreDelTipo>Tests.cs`.

## Requisitos obligatorios

1. **xUnit** + **FluentAssertions**. Dobles de prueba con **Moq** o con clases falsas escritas a mano.
2. Nombres de prueba con el patrón `Metodo_Escenario_ResultadoEsperado`.
3. Estructura Arrange / Act / Assert con los tres comentarios visibles.
4. Un assert lógico por prueba y **sin lógica condicional** dentro de las pruebas.
5. `[Theory]` + `[InlineData]` para casos repetitivos.

## Cobertura mínima que debes producir

- Camino feliz de cada método público.
- **Umbrales exactos**: para cada límite documentado, prueba el valor anterior y el valor del umbral.
- Valores límite: cero, uno, negativos, valor máximo, cadena vacía, cadena con espacios, `null`.
- Entradas inválidas: verifica el tipo exacto de excepción con `Should().ThrowExactly<T>()`.
- Redondeo y precisión decimal cuando el método devuelva importes.
- Casos asincrónicos: usa `await` y `Should().ThrowAsync<T>()`, nunca `.Result`.

## Al terminar

- Enumera en la respuesta los casos cubiertos y los que decidiste no cubrir, con el motivo.
- No modifiques el código de producción ni las pruebas existentes.
- Ejecuta `dotnet test` y reporta el resultado.
