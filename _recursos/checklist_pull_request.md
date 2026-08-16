# Checklist de Pull Request

**Curso:** GitHub Copilot para Desarrollo de Software
**Repositorio:** `copilot-lab-catalogo` — C# / .NET 8, ASP.NET Core Web API + xUnit
**Requisitos contractuales cubiertos:** R-153 y R-088
**Se usa en:** LAB-08 (jornada 4) y LAB-10 (reto integrador)
**Fecha de verificación técnica:** 2026-08-14

Este documento es una lista accionable con casillas. Cada casilla se marca **solo** si se ha verificado con el comando o la inspección indicados. Una casilla marcada sin verificar es peor que una casilla sin marcar: convierte la lista en ruido y traslada al revisor un trabajo que se dijo hecho.

**Regla de uso.** El participante recorre las secciones 1 a 5 antes de abrir el PR, la sección 6 después de solicitar Copilot code review, y la sección 7 antes de fusionar. Las secciones 1 a 5 son de responsabilidad exclusiva del autor.

---

## Datos verificados sobre Copilot code review

Tres hechos que condicionan cómo se usa esta lista. Están verificados en la ficha técnica y deben conocerse antes de la sección 6.

| # | Dato verificado | Consecuencia práctica |
|---|---|---|
| 1 | **Copilot code review siempre deja una revisión de tipo "Comment"**, nunca "Approve" ni "Request changes" `[GA]` | **No cuenta para las aprobaciones requeridas y no bloquea el merge.** La aprobación sigue siendo humana. Un PR con la revisión de Copilot y sin revisión humana no está revisado |
| 2 | Copilot lee las custom instructions, las agent instructions y los agent skills de la **head branch** (la rama con los cambios), **no de la base** `[GA]` | Si añades o corriges `.github/copilot-instructions.md` en tu propia rama, la revisión de tu PR ya las respeta. No hace falta fusionarlas primero |
| 3 | Copilot code review está disponible **en todos los planes de pago** (Business, Pro, Pro+, Max y Enterprise) `[Depende del plan]` | **No requiere Copilot Enterprise.** Lo que sí depende de política de organización es la revisión automática por ruleset y el uso por miembros sin licencia |

Datos complementarios verificados, útiles cuando algo no funciona como se espera:

- La revisión suele tardar **menos de 30 segundos**. Si tarda mucho más, algo va mal en el entorno de build.
- Se puede solicitar una **re-revisión** con el botón junto al nombre de Copilot en el menú **Reviewers**.
- Desde GitHub CLI: `gh pr create --reviewer @copilot` o `gh pr edit <numero> --add-reviewer @copilot`.
- **Quedan excluidos de la revisión** los archivos de gestión de dependencias (los ejemplos documentados son `package.json` y `Gemfile.lock`), los archivos de log y los archivos SVG. **No está verificado** si `.csproj`, `Directory.Packages.props` o `packages.lock.json` cuentan como archivos de gestión de dependencias: la documentación no los nombra. No des por hecho que tu `.csproj` se revisa.
- El entorno de build de la revisión se define en `.github/workflows/copilot-code-review.yml` (específico, gana si existe) o en `.github/workflows/copilot-setup-steps.yml` (compartido con Copilot cloud agent). El repositorio del curso incluye el segundo, con el .NET 8 SDK.
- **Content exclusion** también se aplica a Copilot code review en GitHub.com: los archivos excluidos no se revisan `[Depende del plan/política: Business o Enterprise]`.
- El **review effort level** (**Lite** por defecto, o **Balanced**) se configura a nivel de organización y se puede sobrescribir a nivel de repositorio. Consume AI credits `[Depende del plan/política]`.

---

## 1. Antes de abrir el Pull Request

### 1.1 Estado de la rama

- [ ] La rama sigue la convención del curso: `feat/<iniciales>-precio-descuento`.
- [ ] La rama parte de `main` actualizada y está rebasada o fusionada con la `main` vigente (`git fetch origin && git log --oneline main..HEAD`).
- [ ] No hay conflictos de fusión pendientes.
- [ ] `git status` no muestra archivos sin seguimiento que deberían estar en el commit ni archivos que deberían estar ignorados.
- [ ] Ningún archivo de `bin/`, `obj/`, `.vs/`, `*.user` ni `TestResults/` aparece en el diff.

### 1.2 Compilación y pruebas

- [ ] `dotnet restore CopilotLabCatalogo.sln` termina sin errores.
- [ ] `dotnet build CopilotLabCatalogo.sln --no-restore` termina con **0 errores y 0 warnings**.
- [ ] `dotnet test CopilotLabCatalogo.sln` termina en verde, y el número de pruebas superadas es **mayor** que el del estado inicial (15 superadas y 2 omitidas).
- [ ] **Ninguna prueba existente fue modificada ni eliminada.** Verificado con `git diff main...HEAD -- tests/` leyendo el diff, no solo el nombre de los archivos.
- [ ] Las pruebas que se habilitaron quitando el `Skip` pasan por corrección del código de producción, no por cambio de la aserción.
- [ ] Se ejecutó la aplicación (`dotnet run --project src/Catalogo.Api/Catalogo.Api.csproj`) y se probó manualmente al menos un escenario de éxito y uno de error del cambio.

### 1.3 Alcance

- [ ] El PR resuelve **una** historia o **un** defecto. Si resuelve varios sin relación entre sí, se divide.
- [ ] No hay cambios de formato masivo mezclados con cambios funcionales. Si los hay, van en un commit propio y se señalan en la descripción.
- [ ] No hay código comentado, `TODO` nuevos sin identificador ni restos de depuración (`Console.WriteLine`, puntos de interrupción condicionales, valores fijos de prueba).
- [ ] No se añadió ningún paquete NuGet. Si se añadió, está justificado explícitamente en la descripción del PR.
- [ ] El diff se puede leer en menos de 20 minutos. Si no, se divide o se aporta la guía de revisión (**P-PRR-05** de la biblioteca de prompts).

### 1.4 Autorrevisión

- [ ] Se leyó el diff completo **línea a línea** en `git diff main...HEAD`, no solo los archivos que se recuerdan haber tocado.
- [ ] Se ejecutó la autorrevisión asistida (**P-PRR-02**) y se atendieron los hallazgos bloqueantes.
- [ ] Cada línea generada por Copilot se leyó y se entiende. Si hay una línea que el autor no sabría explicar en la revisión, se reescribe o se elimina.

---

## 2. Contenido de la descripción

- [ ] **Título:** una línea, imperativo, máximo 72 caracteres, con el prefijo de la historia. Ejemplo: `HU-01: actualizar el precio de un producto aplicando descuentos`.
- [ ] **Resumen:** dos o tres frases sobre el problema y la solución, sin detalles de implementación.
- [ ] **Historia de usuario:** identificador, título y **los criterios de aceptación cubiertos, citados por su identificador** (CA-1, CA-2, ...). Los no cubiertos se declaran como tales.
- [ ] **Cambios:** lista por archivo o por área (API, servicios, pruebas, configuración), explicando el **porqué** de cada cambio, no solo el qué.
- [ ] **Pruebas:** comandos ejecutados y su **resultado real**, pruebas añadidas y casos borde cubiertos.
- [ ] **Riesgos:** impacto, compatibilidad y plan de reversión. Si se endureció un contrato de entrada, se declara el impacto en los clientes que hoy envían datos inválidos.
- [ ] **Decisiones sobre sugerencias de Copilot:** la tabla está rellenada por la persona, con al menos una entrada de cada tipo cuando corresponda (ver sección 5).
- [ ] **Checklist de la plantilla:** solo las casillas realmente verificadas.
- [ ] La descripción se entiende **sin abrir el diff**.
- [ ] Idioma: español, según `.github/copilot-instructions.md`.

---

## 3. Pruebas

- [ ] Cada criterio de aceptación de la historia tiene una forma concreta de verificación asignada: prueba unitaria, prueba de integración, comando manual o inspección de código, y se declara cuál (**P-TST-06**).
- [ ] Los **umbrales exactos** están probados por pares: el valor anterior y el valor del umbral (9/10, 49/50, 99/100 en el caso de HU-01).
- [ ] Los **valores frontera** del dominio están probados: cero, uno, negativos, cadena vacía, cadena de solo espacios, `null` donde sea posible.
- [ ] Las **entradas inválidas** verifican el tipo exacto de excepción con `Should().ThrowExactly<T>()` o `Should().ThrowAsync<T>()`.
- [ ] El **redondeo** está probado con al menos un caso que distinga `MidpointRounding.AwayFromZero` de `ToEven`.
- [ ] Los valores esperados están escritos **como literales calculados a mano**, nunca derivados con la misma fórmula que el código bajo prueba.
- [ ] Los nombres siguen el patrón `Metodo_Escenario_ResultadoEsperado`.
- [ ] Estructura Arrange / Act / Assert con los tres comentarios visibles.
- [ ] Un assert lógico por prueba. Sin `if`, `for`, `switch` ni `try` dentro de las pruebas.
- [ ] Ninguna prueba depende del orden de ejecución, de estado compartido, del reloj, de la red ni del sistema de archivos.
- [ ] Las pruebas asincrónicas usan `await`; no hay `.Result` ni `.Wait()`.
- [ ] **Prueba del interruptor:** se estropeó el código de producción a propósito y se comprobó que la prueba se pone en rojo. Una prueba que no falla nunca no protege nada.
- [ ] Se auditaron las pruebas generadas por Copilot (**P-TST-02**) buscando pruebas tautológicas, con múltiples asertos o con valores esperados derivados.

---

## 4. Seguridad

### 4.1 Validación de entradas

- [ ] Toda entrada pública nueva o modificada está validada: DataAnnotations en el DTO **y** validación explícita en el servicio, según la regla 6 de `.github/copilot-instructions.md`.
- [ ] La validación se ejecuta también cuando el método del servicio se invoca directamente, sin pasar por el controller.
- [ ] Los casos que las DataAnnotations no cubren están tratados explícitamente: cadenas formadas solo por espacios, valores de enum fuera del rango definido.
- [ ] Si se usa `Validator.TryValidateObject`, lleva el argumento `validateAllProperties: true`. Sin él solo se evalúan los atributos `[Required]`.
- [ ] Los datos inválidos producen **400 con `ProblemDetails`**, no 500 ni 201.

### 4.2 Secretos

- [ ] `git grep -n -i -E "(api[_-]?key|secret|password|token|bearer|connectionstring)"` sobre el diff no revela ninguna credencial nueva.
- [ ] Ninguna credencial aparece en el código, en `appsettings.json` ni en `appsettings.Development.json`.
- [ ] Ninguna credencial, cabecera de autorización ni dato personal se escribe en el log, en ningún nivel, ni siquiera como parte de una cadena mayor.
- [ ] La configuración sensible se lee de `IConfiguration` (variable de entorno con doble guion bajo, por ejemplo `LegacyPricing__ApiKey`, o `dotnet user-secrets`).
- [ ] Si se detectó un secreto en el historial, se declara en Riesgos que **retirarlo del código no lo elimina del historial de Git** y que en un repositorio real habría que rotarlo.
- [ ] El informe de la revisión de seguridad **no reproduce el valor completo** de ningún secreto: cita archivo y línea.

### 4.3 Dependencias

- [ ] No se añadieron paquetes NuGet. Si se añadió alguno, está justificado en la descripción y se indica para qué se usa exactamente.
- [ ] `dotnet list package --vulnerable --include-transitive` no reporta hallazgos nuevos.
- [ ] `dotnet list package --outdated` se ejecutó y su resultado se conoce, aunque no se actúe sobre él en este PR.
- [ ] Ninguna dependencia de solo pruebas quedó referenciada desde el proyecto de producción.

### 4.4 Manejo de errores

- [ ] No hay `catch { }` vacíos ni `catch` que capturen `Exception` para ignorarla.
- [ ] Las excepciones de dominio se propagan al middleware de errores, que las traduce a `ProblemDetails`.
- [ ] Los mensajes de error dirigidos al cliente **no filtran** detalles internos: sin `StackTrace`, sin nombres de tipos internos, sin rutas de archivo.
- [ ] El log del servidor **sí** conserva el detalle completo de la excepción.
- [ ] Los códigos de estado devueltos corresponden a lo declarado en `[ProducesResponseType]` y en la documentación del endpoint.
- [ ] `nullable` está respetado: no se usó `!` para silenciar el compilador sin un comentario que lo justifique.

---

## 5. Decisiones sobre sugerencias de Copilot

Esta sección es la que distingue un PR del curso de un PR cualquiera. **La rellena la persona**, no Copilot: es una declaración de autoría y de criterio.

- [ ] La tabla de la plantilla del PR está rellenada, con una fila por decisión relevante.
- [ ] Hay al menos una sugerencia **aceptada** con el motivo de por qué se aceptó (no basta "funcionaba").
- [ ] Hay al menos una sugerencia **modificada**, indicando qué se cambió y por qué.
- [ ] Si hubo alguna sugerencia **descartada**, aparece con su motivo. Un PR con código generado y ninguna sugerencia descartada es sospechoso: significa que no hubo criterio de selección.
- [ ] Cada fila indica **cómo se verificó** la decisión: prueba, comando, inspección.
- [ ] Las entradas de la tabla son coherentes con la bitácora de decisiones individual.
- [ ] Ninguna fila dice "aceptada porque compilaba".

### Criterios para decidir

| Decisión | Cuándo corresponde | Qué hay que escribir en el motivo |
|---|---|---|
| **Aceptada** | La sugerencia cumple los criterios de aceptación, respeta las convenciones del repositorio, se entiende línea a línea y está cubierta por una prueba | Qué criterio cumplió y cómo se verificó. Nunca "estaba bien" |
| **Modificada** | La estructura era útil pero faltaba algo: validación, propagación de `CancellationToken`, un caso borde, una convención del repositorio | Qué faltaba, qué se cambió y por qué el cambio es necesario |
| **Descartada** | Contradice una convención, reimplementa lógica que ya existe, introduce una dependencia, usa una API fuera de `net8.0`, o el autor no la entiende lo suficiente para defenderla | El motivo concreto y qué se hizo en su lugar |

### Ejemplo de tabla rellenada

| Sugerencia | Decisión | Motivo | Verificación |
|---|---|---|---|
| Implementación de `ActualizarPrecioAsync` delegando en `IDescuentoCalculator` | Aceptada | Respeta la separación de capas y no reimplementa la regla de descuento; cubre CA-2 | `dotnet test` en verde y prueba que falla si se reimplementa el cálculo |
| Misma implementación, sin llamada a `ActualizarAsync` | Modificada | Calculaba el precio pero no lo persistía, por lo que CA-6 no se cumplía | Prueba nueva `ActualizarPrecioAsync_ProductoExistente_PersisteElPrecio`, que fallaba antes del cambio |
| `Math.Round(precioFinal, 2)` sin `MidpointRounding` | Modificada | El comportamiento por defecto es `ToEven` y la regla R8 exige `AwayFromZero` | Caso (249,50, 49, false): esperado 237,03, con `ToEven` daría 237,02 |
| Uso de `System.Text.Json` con un `JsonSerializerOptions` nuevo para el `ProblemDetails` | Descartada | Introducía una configuración de serialización distinta de la del resto de la API sin necesidad | Se comparó la respuesta con la de las acciones existentes del controller |
| Añadir el paquete `FluentValidation` para validar el DTO | Descartada | La regla del repositorio prohíbe añadir paquetes NuGet sin justificación, y `DataAnnotations` cubre el requisito | Validación implementada con `Validator.TryValidateObject` |

---

## 6. Después de solicitar Copilot code review

### 6.1 Solicitud

- [ ] La revisión se solicitó en GitHub.com: en el PR, sección **Reviewers** del panel derecho, junto a **Copilot**, botón **Request**. Alternativa: `gh pr edit <numero> --add-reviewer @copilot`.
- [ ] Si el repositorio tiene un ruleset con **Automatically request Copilot code review**, la revisión llegó sola y no hizo falta solicitarla `[Depende del plan/política]`.
- [ ] La revisión llegó. Si tardó mucho más de 30 segundos o no llegó, se comprobó: que la política **Copilot code review** esté habilitada en la organización, que el archivo no esté cubierto por content exclusion, y que el PR no consista solo en archivos excluidos de la revisión.

### 6.2 Lectura

- [ ] Se leyeron **todos** los comentarios, no solo los que apuntan a las líneas propias.
- [ ] Se comprobó que la revisión tuvo en cuenta las custom instructions. Si el PR incluye cambios en `.github/copilot-instructions.md` o en `.github/instructions/`, Copilot los lee de la **head branch** y ya deberían notarse.
- [ ] Se entendió que la revisión es de tipo **"Comment"** y que **no bloquea el merge**: no marcarla como resuelta no impide fusionar, y resolverla toda tampoco autoriza a fusionar.

### 6.3 Clasificación

- [ ] Cada comentario está clasificado en una de las cuatro categorías de la tabla siguiente.
- [ ] Cada comentario tiene una **respuesta escrita** en el PR, incluidos los clasificados como incorrectos.
- [ ] Los comentarios clasificados como **incorrectos** citan la evidencia concreta que los contradice.
- [ ] La clasificación se hizo leyendo cada comentario, no aceptando en bloque la salida de **P-PRR-03**.

### Cómo clasificar cada comentario de la revisión

| Categoría | Qué significa | Señales | Qué hacer | Qué se escribe en el PR |
|---|---|---|---|---|
| **Accionable ahora** | El comentario identifica un problema real dentro del alcance de este PR | Apunta a código que este PR introduce o modifica; el problema es verificable; la corrección es acotada | Corregirlo en esta rama, añadir la prueba que impide la regresión, y responder al comentario con el commit que lo resuelve | *"Corregido en `<sha>`. Se añadió la prueba `<nombre>`, que fallaba antes del cambio."* |
| **Accionable después** | El problema es real pero excede el alcance del PR, o exige una decisión que no corresponde tomar aquí | Toca código que este PR no modifica; requiere refactorización amplia; depende de una decisión de arquitectura | Abrir una incidencia con el enlace al comentario, y responder indicando el número | *"Válido, pero fuera del alcance de este PR: afecta a `<área>` y exige `<decisión>`. Registrado como issue #NN."* |
| **Informativo** | Observación correcta que no exige cambio: explica un matiz, sugiere una alternativa equivalente o señala una convención ya cumplida | No propone un cambio concreto, o el cambio es indiferente | Responder reconociéndolo, sin cambiar código | *"Anotado. La alternativa es equivalente; se mantiene la actual por coherencia con `<archivo>`."* |
| **Incorrecto** | El comentario contradice el código, las convenciones del repositorio o los criterios de aceptación | Cita una línea que no dice lo que afirma; propone algo que la convención prohíbe; asume un contexto que no existe | **No aplicarlo.** Responder con la evidencia que lo contradice y registrar la decisión en la bitácora | *"No aplica: `.github/instructions/csharp.instructions.md` exige `<regla>`, y la línea 42 ya lo cumple. Se mantiene la implementación actual."* |

> **Advertencia sobre la categoría "Incorrecto".** Es la más fácil de usar mal. Antes de clasificar un comentario como incorrecto, hay que releer el código con el comentario delante y buscar activamente el argumento a favor. Un comentario correcto descartado como incorrecto es el peor resultado posible de esta sección, y es el error que el instructor busca en la revisión de LAB-08.

### 6.4 Revisión humana

- [ ] Se solicitó al menos una revisión **humana**. La de Copilot no la sustituye ni cuenta para las aprobaciones requeridas.
- [ ] Se aportó la guía de revisión (**P-PRR-05**) si el PR toca más de tres archivos.
- [ ] Los comentarios humanos se respondieron con el mismo criterio de clasificación.

---

## 7. Antes del merge

- [ ] Todos los comentarios están respondidos y los "accionables ahora" están resueltos.
- [ ] Hay al menos una **aprobación humana**.
- [ ] `dotnet build` y `dotnet test` se ejecutaron **después** del último cambio, no antes.
- [ ] La rama está al día con `main` y no hay conflictos.
- [ ] Los checks de integración continua están en verde.
- [ ] La descripción del PR sigue siendo exacta después de todos los cambios: si el alcance cambió durante la revisión, la descripción se actualizó.
- [ ] La tabla de decisiones sobre sugerencias de Copilot incluye las decisiones tomadas **durante** la revisión, no solo las iniciales.
- [ ] La bitácora de decisiones está actualizada y es coherente con el PR.
- [ ] Se sabe cómo revertir: el PR indica el plan de reversión y los cambios son revertibles con un solo `git revert` del commit de merge.
- [ ] Ninguna casilla de este documento quedó marcada sin verificar.

---

## Plantilla de descripción del Pull Request

Alineada con `.github/pull_request_template.md` del repositorio. GitHub la carga automáticamente al crear el PR; esta versión ampliada añade las indicaciones de relleno, que se borran antes de publicar.

```markdown
## Resumen

<!-- Que resuelve este PR, en dos o tres frases. Problema y solucion, sin detalles
     de implementacion. Debe entenderse sin abrir el diff. -->

## Historia de usuario

<!-- Identificador y titulo, por ejemplo: HU-01 Actualizar el precio de un producto
     con descuentos. -->

- Historia:
- Criterios de aceptacion cubiertos:
- Criterios de aceptacion NO cubiertos y por que:

## Cambios

<!-- Lista por archivo o por area (API, servicios, pruebas, configuracion).
     Explica el POR QUE de cada cambio, no solo el que: el que ya esta en el diff. -->

-
-

## Pruebas

- [ ] `dotnet build` sin advertencias
- [ ] `dotnet test` en verde
- Comandos ejecutados y resultado real:
- Pruebas agregadas o modificadas:
- Cobertura de casos borde:
- Escenarios probados manualmente contra la API:

## Riesgos

<!-- Impacto, plan de reversion, datos afectados, compatibilidad.
     Si se endurecio un contrato de entrada, indica el impacto en los clientes
     que hoy envian datos invalidos. -->

## Decisiones sobre sugerencias de Copilot

<!-- Esta tabla la rellena una persona. Es una declaracion de autoria y de criterio.
     Motivo concreto y verificacion en cada fila. -->

| Sugerencia | Decision | Motivo | Verificacion |
| --- | --- | --- | --- |
|  | Aceptada |  |  |
|  | Modificada |  |  |
|  | Descartada |  |  |

## Checklist

- [ ] Sin secretos ni credenciales en el codigo o la configuracion
- [ ] Entradas publicas validadas
- [ ] Documentacion XML de los miembros publicos actualizada
- [ ] No se modificaron pruebas existentes para hacerlas pasar
- [ ] Los mensajes de commit describen el porque, no solo el que
- [ ] Revise personalmente cada linea generada por Copilot
```

> **Diferencia con la plantilla del repositorio.** La plantilla de `.github/pull_request_template.md` tiene la tabla de decisiones con tres columnas (Sugerencia, Decision, Motivo). Esta versión añade la columna **Verificación**, que es la que el instructor evalúa en LAB-08. Si se prefiere mantener la plantilla del repositorio intacta, la verificación se escribe dentro de la columna Motivo, después de la palabra "verificado con".

---

## Errores frecuentes en el PR

| Síntoma | Causa | Solución |
|---|---|---|
| El PR tiene 400 líneas de diff y 300 son formateo | Se guardó con formateo automático sobre archivos no tocados | Revertir el formateo (`git checkout main -- <archivos>`) o separarlo en un commit propio y señalarlo en la descripción |
| Aparecen `bin/` y `obj/` en el diff | Se añadieron antes de que `.gitignore` surtiera efecto | `git rm -r --cached bin obj` y commit; verificar `.gitignore` |
| Las pruebas pasan en local y fallan en el PR | Dependencia de estado local, orden de ejecución o rutas absolutas | Revisar que cada prueba cree su propio estado; el repositorio en memoria es singleton en la aplicación pero se instancia por prueba en los tests |
| Copilot code review no comenta nada | El PR solo toca archivos excluidos de la revisión, la política está deshabilitada, o content exclusion cubre las rutas | Verificar la política **Copilot code review** en la organización y la configuración de content exclusion `[Depende del plan/política]` |
| La revisión de Copilot ignora las convenciones del repositorio | Las instrucciones están en `main` pero no en la head branch, o el toggle **"Use custom instructions when reviewing pull requests"** está desactivado | Copilot lee las instrucciones de la **head branch**: incluirlas en la rama. Comprobar el toggle en Settings → Copilot → Code review |
| Se marcó "dotnet test en verde" pero la CI falla | Se ejecutaron las pruebas antes del último cambio | Volver a ejecutar `dotnet test` después de cada cambio, y antes de marcar la casilla |
| La tabla de decisiones de Copilot está vacía o dice "N/A" | Se generó código pero no se registró ninguna decisión | Rellenarla: es el entregable evaluable de LAB-08 (indicador IND-4) |
| El revisor pide cambios que ya estaban resueltos en un commit posterior | La descripción del PR no se actualizó tras los cambios | Actualizar la descripción y responder al comentario con el sha del commit |

---

## Trazabilidad

| Sección de este documento | Requisito | Resultado de aprendizaje | Laboratorio |
|---|---|---|---|
| 1 a 3 | R-086, R-087 | RA-4.1, RA-4.2, RA-4.4 | LAB-07, LAB-08 |
| 4 | R-086, R-087 | RA-4.3 | LAB-08 |
| 5 | R-088, R-116 | RA-5.5 | LAB-08, LAB-10 |
| 6 | R-088 | RA-4.5 | LAB-08 |
| 7 | R-088 | RA-4.4 | LAB-08, LAB-10 |
