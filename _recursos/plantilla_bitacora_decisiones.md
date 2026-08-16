# Bitácora de decisiones sobre el uso de Copilot

**Curso:** GitHub Copilot para Desarrollo de Software
**Repositorio:** `copilot-lab-catalogo` — C# / .NET 8
**Requisito contractual cubierto:** R-116 · **Indicador:** IND-4
**Se usa en:** todos los laboratorios, de LAB-02 a LAB-10
**Fecha de verificación técnica:** 2026-08-14

La bitácora es el **instrumento central del curso**. Es el único entregable que demuestra lo que realmente se evalúa: no que el participante sepa usar Copilot, sino que mantenga la responsabilidad humana sobre el resultado y sepa justificar por qué aceptó, modificó o descartó cada propuesta.

Un participante que produce código correcto sin bitácora no ha demostrado criterio: ha demostrado suerte o competencia previa. Un participante que produce código con defectos pero registra por qué tomó cada decisión y cómo la verificó ha demostrado exactamente lo que el curso persigue.

---

## 1. Instrucciones de uso

### Cómo se lleva

1. **Un archivo por participante**, llamado `bitacora_<iniciales>.md`, en la raíz de la rama de trabajo del participante o en la carpeta que indique el instructor.
2. **Se rellena durante el laboratorio, no después.** Reconstruir la bitácora al final del día produce entradas genéricas y motivos inventados, y se nota.
3. **Una entrada por decisión**, no una por prompt ni una por laboratorio.
4. **Se escribe en primera persona y en pasado.** "Descarté la sugerencia porque...", no "se recomienda descartar".
5. **Se entrega junto con el Pull Request**, y su contenido debe ser coherente con la tabla de decisiones sobre sugerencias de Copilot de la descripción del PR.

### Qué merece una entrada

| Sí merece entrada | No merece entrada |
|---|---|
| Aceptar código generado que pasa a formar parte del PR | Aceptar una sugerencia de una sola palabra del autocompletado |
| Modificar una propuesta antes de usarla | Corregir una errata de tecleo |
| Descartar una propuesta y hacer otra cosa | Cerrar el chat sin usar la respuesta porque cambió la tarea |
| Aceptar una prueba generada como barrera de regresión | Cada iteración de refinamiento de un mismo prompt (se registra la decisión final, citando cuántas iteraciones hicieron falta) |
| Decidir **no** usar Copilot para una tarea concreta | Consultar una duda conceptual que no produce código |
| Descartar un comentario de Copilot code review | Leer un comentario y estar de acuerdo sin que implique cambio |
| Detectar que la salida contenía una afirmación falsa | |
| Elegir entre dos alternativas que Copilot propuso | |

**Regla práctica:** si la decisión afecta a lo que queda en el repositorio, o si otra persona podría razonablemente haber decidido lo contrario, merece una entrada.

### Cuántas entradas

| Momento | Mínimo esperado |
|---|---|
| Al final de la jornada 2 (LAB-03 y LAB-04) | 2 entradas |
| Al final de la jornada 3 (LAB-05 y LAB-06) | 5 entradas acumuladas |
| Al final de la jornada 4 (LAB-07 y LAB-08) | 8 entradas acumuladas |
| Entrega final (LAB-10) | **8 entradas como mínimo**, con al menos una de cada tipo de decisión |

### Cómo se escribe cada campo

| Campo | Qué se escribe | Error frecuente |
|---|---|---|
| **Fecha y hora** | Fecha y hora local del momento de la decisión, no del momento de escribirla | Poner solo la fecha: se pierde la secuencia dentro del laboratorio |
| **Laboratorio o tarea** | Identificador del laboratorio y, entre paréntesis, la tarea concreta | "LAB-05" a secas, sin decir qué se estaba haciendo |
| **Qué se pidió (el prompt)** | El prompt real, resumido si es muy largo, o su identificador de la biblioteca (`P-PLA-02`) más lo que se sustituyó en los marcadores | Reescribir el prompt "bonito" en lugar del que se envió |
| **Qué propuso Copilot** | Resumen fiel de la propuesta en dos o tres líneas, incluido lo que hacía mal | "Generó el método": no dice nada |
| **Decisión** | Exactamente una de: **Aceptado**, **Modificado**, **Descartado** | Inventar categorías intermedias ("aceptado parcialmente") |
| **Motivo** | Por qué esa decisión, en términos del criterio que se aplicó: criterio de aceptación, convención del repositorio, regla de negocio, riesgo concreto | "Funcionaba" / "No me gustó" / "Estaba mal" |
| **Cómo se verificó** | El comando, la prueba o la inspección concreta que se ejecutó, y su resultado | "Lo revisé" |
| **Evidencia** | Commit (sha corto), nombre de la prueba, archivo y línea, o número del comentario del PR | Dejarlo vacío |

> **El campo que más se evalúa es "Cómo se verificó".** Una entrada con motivo excelente y verificación vacía indica una opinión, no una decisión técnica.

---

## 2. Plantilla

Copiar este bloque en `bitacora_<iniciales>.md` y añadir una fila por decisión.

```markdown
# Bitacora de decisiones — <Nombre Apellido> (<iniciales>)

**Curso:** GitHub Copilot para Desarrollo de Software
**Repositorio:** copilot-lab-catalogo
**Rama:** feat/<iniciales>-precio-descuento
**Periodo:** <fecha de inicio> a <fecha de fin>

## Registro

| # | Fecha y hora | Laboratorio o tarea | Que se pidio (prompt) | Que propuso Copilot | Decision | Motivo | Como se verifico | Evidencia |
| --- | --- | --- | --- | --- | --- | --- | --- | --- |
| 1 |  |  |  |  |  |  |  |  |
| 2 |  |  |  |  |  |  |  |  |
| 3 |  |  |  |  |  |  |  |  |

## Resumen

| Tipo de decision | Cantidad |
| --- | --- |
| Aceptado |  |
| Modificado |  |
| Descartado |  |
| **Total** |  |

## Reflexion final (maximo 10 lineas)

- **Que aprendi sobre cuando Copilot ayuda y cuando estorba:**
- **El error mas caro que evite gracias a verificar:**
- **Que haria distinto la proxima vez:**
- **Que tarea decidi NO delegar y por que:**
```

### Variante en formato de ficha

Para quien prefiera una entrada por bloque en lugar de una tabla, especialmente cuando el motivo requiere varias frases:

```markdown
### Entrada NN — <titulo corto>

- **Fecha y hora:** 2026-MM-DD HH:MM
- **Laboratorio o tarea:** LAB-0X (<tarea concreta>)
- **Prompt:** <P-XXX-NN adaptado> / <prompt literal resumido>
- **Propuesta de Copilot:** <resumen fiel, incluido lo que hacia mal>
- **Decision:** Aceptado | Modificado | Descartado
- **Motivo:** <criterio aplicado>
- **Verificacion:** <comando o prueba, y su resultado real>
- **Evidencia:** <sha> / <nombre de prueba> / <archivo:linea>
```

Ambos formatos son válidos. Lo que no es válido es mezclarlos dentro del mismo documento.

---

## 3. Cinco entradas de ejemplo completamente rellenadas

Estos ejemplos son verosímiles y están construidos sobre el repositorio real. Ilustran los tres tipos de decisión y los distintos momentos del curso. **No hay que copiarlos**: sirven para calibrar el nivel de detalle esperado.

### Entrada de ejemplo 1 — Aceptado

| Campo | Contenido |
|---|---|
| **Fecha y hora** | 2026-09-16 15:42 |
| **Laboratorio o tarea** | LAB-05 (implementar `ActualizarPrecioAsync` para HU-01, TODO-01) |
| **Qué se pidió** | `P-PLA-02` adaptado: implementar `ActualizarPrecioAsync` en `ProductoService.cs`, delegando el cálculo en `IDescuentoCalculator`, devolviendo `null` si el producto no existe, propagando `cancellationToken` y sin cambiar la firma |
| **Qué propuso Copilot** | Implementación de 14 líneas: lee el producto con `ObtenerPorIdAsync`, devuelve `null` si es `null`, llama a `_calculadora.CalcularPrecioFinal(solicitud.PrecioBase, solicitud.Cantidad, solicitud.EsClientePreferente)`, asigna el resultado a `producto.PrecioBase`, llama a `_repositorio.ActualizarAsync(producto, cancellationToken)`, registra con `_logger.LogInformation` y devuelve `Mapear(actualizado)`. Propaga `cancellationToken` y usa `ConfigureAwait(false)` en las tres llamadas |
| **Decisión** | **Aceptado** |
| **Motivo** | Cumple CA-2 (delega en `IDescuentoCalculator` y no reimplementa ninguna regla: no hay ninguna constante numérica en el método), CA-5 y CA-6 (persiste con `ActualizarAsync`, que era el punto que más me preocupaba). Respeta las convenciones del repositorio: sufijo `Async`, `CancellationToken` propagado, `ConfigureAwait(false)` como el resto del archivo, sin `Console.WriteLine`. Lo leí línea a línea y sabría defender cada una |
| **Cómo se verificó** | 1) `dotnet build` → 0 errores, 0 warnings. 2) Quité el `Skip` de `ActualizarPrecioAsync_ProductoExistente_DevuelveElPrecioConDescuentoAplicado` → pasa (esperaba 95,00 con precioBase 100 y cantidad 11). 3) Escribí `ActualizarPrecioAsync_ProductoExistente_PersisteElPrecio`, que hace `PATCH` y después `ObtenerPorIdAsync`: pasa. 4) Busqué `0.05`, `0.10`, `0.15`, `0.08` y `0.20` en el método: ninguna coincidencia. 5) `dotnet test` → 17 superadas, 1 omitida |
| **Evidencia** | Commit `a3f19c2` · Pruebas `ActualizarPrecioAsync_ProductoExistente_DevuelveElPrecioConDescuentoAplicado` y `ActualizarPrecioAsync_ProductoExistente_PersisteElPrecio` · `src/Catalogo.Api/Services/ProductoService.cs:71-88` |

### Entrada de ejemplo 2 — Modificado

| Campo | Contenido |
|---|---|
| **Fecha y hora** | 2026-09-16 16:20 |
| **Laboratorio o tarea** | LAB-05 (acción `PATCH api/productos/{id:int}/precio` en el controller) |
| **Qué se pidió** | `P-PLA-03` adaptado: añadir la acción `PATCH`, respetando `.github/instructions/api.instructions.md`, con `[ProducesResponseType]` para 200, 400 y 404 |
| **Qué propuso Copilot** | Acción con `[HttpPatch("{id}/precio")]`, firma `Task<ActionResult<ProductoResponse>>`, llamada al servicio, `NotFound()` si el resultado es `null` y `Ok(resultado)` si no. Declaraba `[ProducesResponseType]` solo para 200 y 404. Además usaba `NotFound()` sin cuerpo, en lugar del `ProblemDetails` que usan las otras acciones del archivo |
| **Decisión** | **Modificado** |
| **Motivo** | Tres desviaciones respecto de las convenciones: (1) la restricción de ruta debe ser tipada, `{id:int}`, como en el resto del controller, y sin ella una petición a `/api/productos/abc/precio` entra en la acción y produce un 400 del framework en lugar de un 404 limpio; (2) faltaba `[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]`, exigido por CA-8 y por `api.instructions.md`; (3) `NotFound()` sin cuerpo incumple la convención de devolver `ProblemDetails`, y el resto del archivo ya lo hace con `Title`, `Detail` y `Status`. Cambié las tres cosas y añadí la documentación XML |
| **Cómo se verificó** | 1) `dotnet build` → 0 warnings. 2) `curl -i -X PATCH http://localhost:5080/api/productos/9999/precio -H "Content-Type: application/json" -d '{"precioBase":100,"cantidad":11,"esClientePreferente":false}'` → 404 con cuerpo `ProblemDetails` que incluye el identificador. 3) `curl` sobre `/api/productos/abc/precio` → 404 de enrutamiento, que es lo esperado con la restricción tipada. 4) Swagger en `http://localhost:5080/swagger` muestra los tres códigos declarados |
| **Evidencia** | Commit `b7d004e` · `src/Catalogo.Api/Controllers/ProductosController.cs:90-118` · Captura de Swagger en `evidencias/lab05-swagger.png` |

### Entrada de ejemplo 3 — Descartado

| Campo | Contenido |
|---|---|
| **Fecha y hora** | 2026-09-17 16:05 |
| **Laboratorio o tarea** | LAB-06 (corrección de BUG-01 en `DescuentoCalculator`) |
| **Qué se pidió** | Tras aplicar `P-DEP-01` y confirmar las dos causas raíz, pedí la corrección: *"Corrige `ObtenerDescuentoPorVolumen` y `CalcularPrecioFinal` para que los umbrales sean inclusivos y el descuento total se tope en 20 %."* |
| **Qué propuso Copilot** | Cambió los tres `>` por `>=` correctamente, pero implementó el tope **dentro** de `ObtenerDescuentoPorVolumen`, limitando ahí el descuento por volumen a 0,20m, y dejó `CalcularPrecioFinal` sin tope tras sumar el 8 % del cliente preferente. Además propuso extraer los umbrales a un `Dictionary<int, decimal>` estático "para facilitar la configuración futura" |
| **Decisión** | **Descartado** |
| **Motivo** | La corrección del tope es **incorrecta en el lugar equivocado**: el descuento por volumen nunca supera el 15 %, así que un tope del 20 % ahí no hace nada, y el caso que falla (cantidad 100 con cliente preferente, 15 % + 8 % = 23 %) sigue devolviendo 82,00 en lugar de 80,00. El tope debe aplicarse **después de sumar**, en `CalcularPrecioFinal`. La propuesta del `Dictionary` es una abstracción prematura: no hay ningún requisito de configurar los umbrales, añade una estructura mutable estática que las instrucciones del repositorio prohíben explícitamente, y complica un método de 18 líneas. Implementé la corrección a mano: `>=` en los tres umbrales y `const decimal TopeDescuento = 0.20m` con `if (descuento > TopeDescuento) descuento = TopeDescuento;` justo después de sumar el 8 % |
| **Cómo se verificó** | 1) Apliqué la propuesta de Copilot en una copia y quité el `Skip` de `CalcularPrecioFinal_CantidadCienYClientePreferente_TopaElDescuentoEnVeintePorCiento`: **seguía fallando** con "Expected 80.00M, but found 82.00M". Esa es la prueba de que la corrección estaba mal. 2) Revertí y apliqué mi versión: la prueba pasa. 3) Añadí `[Theory]` con los pares 9/10, 49/50 y 99/100: las seis pasan. 4) `dotnet test` → 24 superadas, 0 omitidas, 0 fallidas |
| **Evidencia** | Commit `c1e8ab5` · Prueba `CalcularPrecioFinal_CantidadCienYClientePreferente_TopaElDescuentoEnVeintePorCiento` · `src/Catalogo.Api/Services/DescuentoCalculator.cs:32-45` · Nota de causa raíz en `docs/causa-raiz-bug01.md` |

### Entrada de ejemplo 4 — Modificado (pruebas generadas)

| Campo | Contenido |
|---|---|
| **Fecha y hora** | 2026-09-18 15:15 |
| **Laboratorio o tarea** | LAB-07 (pruebas del endpoint nuevo, CA-7 de HU-01) |
| **Qué se pidió** | `P-TST-01` con la cobertura mínima explícita: umbrales por pares, tope del 20 %, suma frente a composición, redondeo `AwayFromZero` con los casos (249,50, 49, false) y (129,90, 10, false), y entradas inválidas |
| **Qué propuso Copilot** | 9 pruebas: una `[Theory]` con los umbrales, dos `[Fact]` para el tope y el cliente preferente, una `[Theory]` para las entradas inválidas y dos pruebas de redondeo. Las de umbral y entradas inválidas eran correctas. **Las dos pruebas de redondeo calculaban el valor esperado con `precioBase * 0.95m` dentro de la propia prueba**, en lugar de escribirlo como literal. Y faltaba por completo la prueba de suma frente a composición |
| **Decisión** | **Modificado** |
| **Motivo** | Una prueba que calcula el valor esperado con la misma fórmula que el código bajo prueba **no prueba nada**: si el código redondeara mal, la prueba redondearía igual de mal y pasaría. Sustituí las expresiones por los literales `237.03m` y `123.41m`, calculados a mano (249,50 × 0,95 = 237,025 → `AwayFromZero` → 237,03; 129,90 × 0,95 = 123,405 → 123,41). Añadí `CalcularPrecioFinal_VolumenMedioYPreferente_SumaLosDescuentos` con (100,00, 50, true) → 82,00, que es el caso que distingue la suma de la composición: si los descuentos se compusieran, el resultado sería 82,80 |
| **Cómo se verificó** | 1) Cambié temporalmente `MidpointRounding.AwayFromZero` por el redondeo por defecto en `DescuentoCalculator`: con las pruebas originales de Copilot **seguían pasando**; con mis literales, ambas fallan (237,02 y 123,40). Ese experimento es la prueba de que el cambio era necesario. 2) Revertí el sabotaje. 3) `dotnet test` → 24 superadas |
| **Evidencia** | Commit `d4a72f0` · `tests/Catalogo.Api.Tests/DescuentoCalculatorTests.cs:88-124` · Pruebas `CalcularPrecioFinal_RedondeoAlza_UsaAwayFromZero` y `CalcularPrecioFinal_VolumenMedioYPreferente_SumaLosDescuentos` |

### Entrada de ejemplo 5 — Descartado (comentario de Copilot code review)

| Campo | Contenido |
|---|---|
| **Fecha y hora** | 2026-09-18 17:40 |
| **Laboratorio o tarea** | LAB-08 (clasificación de los comentarios de Copilot code review del PR #7) |
| **Qué se pidió** | No fue un prompt: fue la revisión automática. Solicité Copilot code review desde el panel **Reviewers** del PR y recibí cinco comentarios. Este es el tercero |
| **Qué propuso Copilot** | Comentario sobre `ProductoService.cs:78`: *"Consider catching the exception from `_repositorio.ActualizarAsync` and returning a default response to avoid propagating infrastructure errors to the caller."* Proponía envolver la llamada en un `try/catch` que devolviera `null` en caso de error |
| **Decisión** | **Descartado** |
| **Motivo** | Contradice directamente la regla 8 de `.github/copilot-instructions.md`: *"Las excepciones de dominio no se capturan silenciosamente: se propagan al middleware de errores, que las traduce a `ProblemDetails`. Prohibido `catch { }`."* Además, devolver `null` ante un error de infraestructura sería **peor que el problema**: el controller traduce `null` a 404, así que un fallo de persistencia se presentaría al cliente como "producto no encontrado", que es un diagnóstico falso. El comportamiento correcto es que la excepción llegue al middleware y produzca un 500 con `ProblemDetails`. Lo clasifiqué como **Incorrecto** y respondí en el PR con la cita de la regla y con la explicación del 404 falso |
| **Cómo se verificó** | 1) Releí `.github/copilot-instructions.md` regla 8 y `.github/instructions/csharp.instructions.md` ("Prohibido capturar `Exception` para ignorarla"). 2) Comprobé en `ProductosController.cs:105` que `null` se traduce a 404. 3) Simulé el escenario con un doble de `IProductoRepository` que lanza `InvalidOperationException` en `ActualizarAsync`: con el `try/catch` propuesto, la API devolvía 404; sin él, 500. 4) Publiqué la respuesta en el hilo del comentario |
| **Evidencia** | PR #7, comentario 3 · Respuesta publicada el 2026-09-18 17:52 · `.github/copilot-instructions.md` regla 8 · Prueba temporal descrita en el comentario (no se incorporó al repositorio) |

---

## 4. Rúbrica de evaluación de la bitácora

La bitácora vale **20 puntos** dentro de la evaluación formativa del curso y es la evidencia del indicador **IND-4**.

| Criterio | Peso | Cumple (puntuación completa) | Cumple parcialmente (mitad) | No cumple (0) |
|---|---|---|---|---|
| **Cantidad** | 4 | 8 o más entradas, distribuidas a lo largo de los laboratorios | Entre 5 y 7 entradas, o todas concentradas en un solo día | Menos de 5 entradas |
| **Variedad de decisiones** | 3 | Hay al menos una entrada de cada tipo: aceptado, modificado y descartado | Solo dos de los tres tipos | Todas las entradas son del mismo tipo |
| **Motivo explícito** | 5 | **Todas** las entradas explican el criterio aplicado: criterio de aceptación, convención del repositorio, regla de negocio o riesgo concreto | Al menos el 70 % de las entradas tiene motivo explícito; el resto es genérico | Predominan motivos como "funcionaba", "estaba bien" o "no me convenció" |
| **Verificación** | 5 | **Todas** las entradas indican el comando, la prueba o la inspección concreta que se ejecutó, y su resultado real | Al menos el 70 % de las entradas tiene verificación concreta | Predominan verificaciones como "lo revisé" o el campo vacío |
| **Evidencia trazable** | 2 | Todas las entradas apuntan a un commit, una prueba, un archivo con línea o un comentario del PR, y la referencia existe | Algunas referencias faltan o no se pueden localizar | Sin evidencias, o referencias que no existen |
| **Coherencia con el PR** | 1 | La bitácora y la tabla de decisiones del PR cuentan la misma historia | Hay divergencias menores | Se contradicen |
| **Reflexión final** | 0 (obligatoria) | Presente, específica y referida a hechos del curso | Presente pero genérica | Ausente: la bitácora se devuelve para completar |
| **Total** | **20** | | | |

### Condiciones que anulan la evaluación

- **Reconstrucción posterior evidente:** todas las entradas con la misma hora, o motivos que no se corresponden con el código entregado.
- **Referencias inventadas:** commits o pruebas que no existen en la rama.
- **Bitácora sin ninguna decisión de tipo "modificado" ni "descartado" en un trabajo con código generado.** Es la señal más clara de que no hubo criterio de selección: se aceptó todo.

### Cómo se ve una entrada insuficiente frente a una suficiente

| Campo | Insuficiente | Suficiente |
|---|---|---|
| Qué propuso Copilot | "Generó el método" | "Implementación de 14 líneas que calcula el precio con `IDescuentoCalculator` pero no llama a `ActualizarAsync`, por lo que no persiste" |
| Motivo | "Estaba bien" | "Cumple CA-2 porque delega en `IDescuentoCalculator` y no contiene ninguna constante de descuento; verificado buscando `0.05`, `0.10`, `0.15`, `0.08` y `0.20` en el método" |
| Cómo se verificó | "Lo probé" | "`dotnet test` → 17 superadas, 1 omitida; y la prueba `..._PersisteElPrecio`, que hace `PATCH` y después `ObtenerPorIdAsync`, pasa" |
| Evidencia | (vacío) | "Commit `a3f19c2` · `src/Catalogo.Api/Services/ProductoService.cs:71-88`" |

---

## 5. Preguntas frecuentes

**¿Registro cada sugerencia del autocompletado?** No. El autocompletado en línea produce decenas de aceptaciones por hora y registrarlas todas convertiría la bitácora en un log inútil. Se registra cuando la sugerencia aceptada implica una decisión: una regla de negocio, una firma pública, un manejo de errores.

**¿Y si Copilot acertó a la primera y no cambié nada?** Se registra como **Aceptado**, con el motivo de por qué era correcto y con la verificación que lo confirma. Una aceptación justificada y verificada vale exactamente lo mismo que un descarte bien argumentado.

**¿Y si decidí no usar Copilot para algo?** Es una entrada válida y de las más interesantes. Decisión: **Descartado**, con el motivo (la tarea era más rápida a mano, el contexto era demasiado específico, la responsabilidad era de arquitectura) y la verificación (el resultado escrito a mano pasa las pruebas).

**¿Puedo pedirle a Copilot que redacte las entradas?** Puede ayudar a redactar el campo "Qué propuso Copilot", que es descriptivo. **No** puede escribir el motivo ni la verificación: son la declaración personal que se evalúa, y una bitácora redactada por el modelo se detecta con facilidad porque los motivos son genéricos y las verificaciones no se corresponden con comandos que se hayan ejecutado realmente.

**¿Qué pasa si me equivoqué y lo registré?** Nada malo: una entrada que documenta una decisión que después resultó incorrecta, con la corrección posterior, es mejor material de aprendizaje que una bitácora impecable. Se añade una entrada nueva que referencia la anterior.

---

## 6. Trazabilidad

| Elemento | Referencia |
|---|---|
| Requisito de la propuesta | R-116 |
| Indicador | IND-4 |
| Resultado de aprendizaje | RA-5.5 (justificar aceptado / modificado / descartado) |
| Laboratorios que la exigen | LAB-02 a LAB-10 |
| Documento relacionado | `06_recursos/checklist_pull_request.md`, sección 5 |
| Plantilla del repositorio | `.github/pull_request_template.md`, sección "Decisiones sobre sugerencias de Copilot" |
| Riesgo que mitiga | RG-09 del registro de decisiones (aceptar código sin revisarlo y llegar al PR con un defecto) |
