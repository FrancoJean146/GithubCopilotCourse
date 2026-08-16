# Guía de uso responsable de GitHub Copilot

**Curso:** GitHub Copilot para Desarrollo de Software
**Repositorio de referencia:** `copilot-lab-catalogo` — C# / .NET 8
**Requisitos contractuales cubiertos:** R-093 y R-160
**Fecha de verificación técnica:** 2026-08-14

Esta guía no contiene eslóganes. Cada principio se enuncia como una regla accionable, se acompaña de la forma concreta de verificar que se está cumpliendo, y se ilustra con el código real del repositorio de práctica. Un principio que no se puede verificar no es un principio: es una declaración de intenciones.

**Alcance.** La guía aplica al uso de Copilot en el curso y sirve de base para la guía de equipo que se redacta en LAB-09. Los datos de producto que contiene están verificados en `06_recursos/ficha_tecnica_copilot_verificada.md` con fecha de consulta 2026-08-14; el producto evoluciona rápido y la guía debe revisarse antes de cada edición del curso.

---

## 1. Los seis principios y su verificación

### P1 — La responsabilidad humana sobre el resultado no se transfiere

**La regla.** Quien firma el commit es responsable del código, con independencia de quién o qué lo escribió. "Lo generó Copilot" no es una explicación válida en una revisión, en un incidente ni en una auditoría.

**Cómo se verifica.**

- Antes de hacer commit, el autor puede explicar en voz alta qué hace cada línea del cambio. Si hay una línea que no sabría defender, se reescribe o se elimina.
- El PR incluye la tabla de decisiones sobre sugerencias de Copilot, rellenada por la persona.
- La bitácora de decisiones tiene al menos una entrada por cada decisión relevante, con motivo y verificación.

**En el repositorio.** En LAB-06 el participante corrige BUG-01. Si acepta una corrección que aplica el tope del 20 % dentro de `ObtenerDescuentoPorVolumen` en lugar de después de sumar el descuento de cliente preferente, la prueba de `cantidad = 100` con cliente preferente sigue devolviendo 82,00 en lugar de 80,00. El defecto es suyo, no del modelo.

**Señal de incumplimiento.** Un PR cuyo autor no sabe explicar por qué su método usa `ConfigureAwait(false)`, o por qué el redondeo lleva `MidpointRounding.AwayFromZero`.

### P2 — Revisión obligatoria antes del commit

**La regla.** Ningún código generado llega al repositorio sin haber sido leído línea a línea y ejecutado. Leer no es hojear: es entender qué hace cada instrucción y por qué está ahí.

**Cómo se verifica.**

- `git diff --staged` leído completo antes de cada commit, no solo los archivos que se recuerdan haber tocado.
- `dotnet build` con 0 warnings y `dotnet test` en verde ejecutados **después** del último cambio.
- Al menos un escenario de éxito y uno de error probados contra la aplicación en marcha, no solo en pruebas unitarias.
- Ninguna prueba existente modificada: `git diff main...HEAD -- tests/`.

**En el repositorio.** En LAB-07, una batería de pruebas generada puede calcular el valor esperado con la misma fórmula que el código bajo prueba (`precioBase * 0.95m`). Compila, pasa y no prueba nada. La única forma de detectarlo es leer las pruebas y hacer el experimento: estropear el código a propósito y comprobar que la prueba se pone en rojo.

**Señal de incumplimiento.** Una suite verde que no falla cuando se introduce un defecto deliberado.

### P3 — Nunca introducir secretos ni datos de cliente en el prompt

**La regla.** No se pegan en el chat credenciales, tokens, cadenas de conexión, datos personales, datos de cliente, fragmentos de logs de producción ni capturas que los contengan. La regla aplica a la Chat view, al inline chat, a Copilot CLI, a los prompt files y a cualquier archivo que se adjunte como contexto.

**Cómo se verifica.**

- Antes de adjuntar un archivo o pegar una salida de terminal, mirar lo que se está adjuntando. `#terminalLastCommand` envía la salida completa del último comando, que puede contener rutas locales, nombres de usuario o cadenas de conexión que aparecieran en un log.
- Barrido periódico del repositorio: `git grep -n -i -E "(api[_-]?key|secret|password|token|bearer|connectionstring)"`.
- Configuración sensible en variables de entorno (`LegacyPricing__ApiKey`) o `dotnet user-secrets`, nunca en `appsettings.json`.

**En el repositorio.** El único secreto del material es `sk-FAKE-DEMO-NOT-A-REAL-SECRET-0000`, **deliberadamente falso**, presente en `Infrastructure/LegacyPricingClient.cs` línea 9, en `appsettings.json` y **escrito en el log** en `ObtenerPrecioReferenciaAsync`. Es el objeto del ejercicio de seguridad de la jornada 4 y no debe sustituirse por una credencial real bajo ninguna circunstancia.

**Matiz importante.** El riesgo no desaparece por usar la cuenta corporativa. Con Business y Enterprise los datos no se usan para entrenar modelos, pero un secreto enviado en un prompt sigue siendo un secreto que salió del perímetro de la organización, queda fuera de su control de retención y puede acabar en un log de servicio. La regla es no enviarlo, no confiar en que el destino lo trate bien.

**Señal de incumplimiento.** Un participante que pega un `appsettings.json` de un proyecto real "para que Copilot entienda el contexto".

### P4 — Declarar el uso de IA en el Pull Request

**La regla.** El PR indica explícitamente qué se generó con asistencia y qué decisiones se tomaron sobre las propuestas. No es una confesión: es información que el revisor necesita para calibrar dónde poner la atención.

**Cómo se verifica.**

- La sección "Decisiones sobre sugerencias de Copilot" de `.github/pull_request_template.md` está rellenada, con motivo y verificación por fila.
- Hay coherencia entre la tabla del PR y la bitácora individual.
- Un PR con código generado y **ninguna** decisión de tipo "modificado" o "descartado" se devuelve: significa que no hubo criterio de selección.

**En el repositorio.** La plantilla del repositorio ya incluye la tabla con las tres filas (Aceptada, Modificada, Descartada). Dejarla vacía o escribir "N/A" es un incumplimiento del entregable de LAB-08.

### P5 — No delegar decisiones de arquitectura ni de negocio

**La regla.** Copilot puede enumerar alternativas, explicar sus consecuencias y estimar esfuerzos. **No** decide dónde vive una responsabilidad, qué contrato se rompe, qué se persiste, qué se expone al exterior ni qué regla de negocio es correcta. Esas decisiones las toma una persona, se documentan en un ADR y se firman.

**Cómo se verifica.**

- Toda decisión de diseño con consecuencias tiene un ADR breve con alternativas consideradas y criterio de elección.
- Los prompts de decisión piden **alternativas comparadas**, no una recomendación única (`P-PLA-07` de la biblioteca).
- El plan producido por el rol `Plan` incluye una sección "Decisiones que debe tomar una persona" y esa sección no está vacía.

**En el repositorio.** En HU-02, el producto 10 de la semilla tiene `Activo = false` y ni el contrato ni el DTO dicen qué debe hacer la búsqueda con los productos inactivos. Copilot elegirá una de las dos opciones **sin avisar**. Detectar la ambigüedad, decidir, documentar la decisión y escribir la prueba que la fija es trabajo humano, y es el punto pedagógico más valioso de esa historia.

**Señal de incumplimiento.** Un ADR cuyo apartado de alternativas dice "Copilot recomendó esta opción".

### P6 — Verificar antes de afirmar

**La regla.** Ninguna afirmación producida por el modelo se repite como hecho sin comprobarla. Aplica a números de línea, nombres de API, versiones, comportamientos del producto y datos de política.

**Cómo se verifica.**

- Toda cita de `archivo:línea` se abre antes de incluirla en un informe.
- Toda afirmación sobre el producto se contrasta con la documentación oficial del **dominio correcto**: para la mecánica de VS Code, `code.visualstudio.com/docs/agents/` y `/docs/agent-customization/`; para planes, políticas, code review, cloud agent, CLI, privacidad y métricas, `docs.github.com`; para Visual Studio, `learn.microsoft.com`.
- Uso sistemático del prompt de clasificación de afirmaciones (`P-COM-06`): evidencia directa frente a inferencia.
- Toda afirmación sobre vulnerabilidades de dependencias se sustituye por la salida de `dotnet list package --vulnerable --include-transitive`.

**En el repositorio.** El número de línea es el dato que el modelo equivoca con más frecuencia, incluso cuando el resto de la explicación es correcta. Una nota de módulo con líneas inventadas es peor que no tener nota, porque la siguiente persona confía en ella.

**Señal de incumplimiento.** Un informe de seguridad que cita `LegacyPricingClient.cs:14` cuando la constante está en la línea 9.

---

## 2. Qué se puede delegar y qué no

La frontera no es "código sí, decisiones no". Es más fina: depende de si la salida se puede **verificar objetivamente** y de si el coste de un error es reversible.

### Se puede delegar (con revisión)

| Tarea | Ejemplo en el repositorio | Cómo se verifica | Riesgo residual |
|---|---|---|---|
| Explicar código ajeno | `P-COM-01` sobre `DescuentoCalculator.cs` | Abrir cada línea citada | Números de línea inventados |
| Generar pruebas a partir de reglas escritas | `P-TST-01` con las reglas R1 a R8 enumeradas en el prompt | Estropear el código y comprobar que la prueba se pone en rojo | Pruebas escritas contra el código en vez de contra la regla |
| Documentación XML de miembros públicos | TODO-03 en las tres interfaces | `dotnet build` con `GenerateDocumentationFile` activado: sin CS1591 | Documentar el comportamiento deseado en lugar del real |
| Refactorización mecánica con pruebas verdes de respaldo | Extraer métodos en `ProductoService` | `dotnet test` antes y después, sin tocar pruebas | Cambios en el texto de logs, invisibles para las pruebas |
| Redacción de descripciones de PR y mensajes de commit | `P-PRR-01` a partir del diff | Contrastar con `git diff --stat` | Afirmar que las pruebas pasan sin evidencia |
| Análisis de un mensaje de error o de un log | `P-DEP-02` y `P-DEP-03` | Ejecutar el paso propuesto y comparar con el resultado anunciado | Diagnóstico plausible pero equivocado |
| Andamiaje repetitivo | Atributos `[ProducesResponseType]`, `[InlineData]` de una `[Theory]` | Compilación y revisión visual | Casos borde omitidos |
| Enumerar alternativas de diseño | `P-PLA-07` sobre dónde validar la entrada | Comprobar que las alternativas son reales y no de paja | Recomendación sesgada hacia lo más común |

### No se puede delegar

| Decisión | Ejemplo en el repositorio | Por qué no | Quién decide |
|---|---|---|---|
| **La regla de negocio correcta** | Si el tope del 20 % se aplica antes o después de sumar el 8 % del cliente preferente | El modelo no tiene acceso a la política comercial; solo a lo que parece razonable | Área de negocio, registrado en la historia |
| **Dónde vive una responsabilidad** | Si el cálculo del descuento pertenece a `IDescuentoCalculator` o a `ProductoService` | Es una decisión de arquitectura con consecuencias de mantenimiento y de prueba | Equipo, en un ADR |
| **Qué contrato público se rompe** | Endurecer la validación de `POST /api/productos` (HU-03) rompe a los clientes que hoy envían datos inválidos | Requiere conocer a los consumidores | Responsable del servicio |
| **Qué se persiste y qué se expone** | Si `ActualizarPrecioAsync` sobrescribe `PrecioBase` o guarda el precio calculado en un campo aparte | Afecta a la integridad de los datos y es difícilmente reversible | Equipo, en un ADR |
| **Ambigüedades del requisito** | Qué hace la búsqueda con los productos que tienen `Activo = false` | No hay respuesta en el código; hay que decidirla | Persona, documentada y con prueba |
| **La aprobación de un Pull Request** | Aprobar el PR de HU-01 | Copilot code review deja una revisión de tipo "Comment": no aprueba ni bloquea | Revisor humano |
| **Clasificar un comentario de revisión como incorrecto** | Descartar la sugerencia de envolver `ActualizarAsync` en un `try/catch` | Exige contrastar con las convenciones y con las consecuencias reales | Autor, con evidencia escrita |
| **Qué es un secreto y qué se puede compartir** | Si un `appsettings.json` se puede adjuntar como contexto | El modelo no conoce la clasificación de información de la organización | Persona, según la política de la organización |
| **Priorizar seguridad frente a plazo** | Corregir BUG-03 antes de fusionar o abrir una incidencia | Es una decisión de riesgo organizativo | Responsable del equipo |

### La zona gris

Tres casos que no son ni lo uno ni lo otro y que conviene tratar explícitamente en la guía de equipo:

1. **Código de producción generado íntegramente.** Es delegable **solo si** existe una prueba que falle antes y pase después, el autor entiende cada línea y la revisión humana lo confirma. Sin esas tres condiciones, no.
2. **Refactorizaciones amplias en modo agente.** El agente puede tocar decenas de archivos en un turno. La condición es un plan aprobado antes de ejecutar y un diff revisable después. Si el diff no se puede leer en una sesión, la refactorización era demasiado grande.
3. **Corrección de un defecto de seguridad.** La corrección puede proponerla el modelo; la **verificación** de que el defecto ya no es explotable la hace una persona, con una prueba de regresión que falle sin la corrección.

---

## 3. Privacidad y datos

Datos verificados en la documentación oficial con fecha de consulta 2026-08-14.

### 3.1 Business y Enterprise: los datos no se usan para entrenar `[GA]`

> *"GitHub does not use Copilot Business or Copilot Enterprise customer data to train AI models. Copilot Business and Copilot Enterprise customers' data is protected under GitHub's Data Protection Agreement, which prohibits such use without customer authorization."*

Consecuencia observable: en una cuenta con asiento Business o Enterprise, el ajuste **"Allow GitHub to use my data for AI model training"** **no aparece** en la configuración personal. Si aparece, la cuenta activa no tiene un asiento Business o Enterprise. Es la comprobación más rápida.

### 3.2 Planes individuales: cambio crítico desde el 24 de abril de 2026 `[GA]`

> *"Starting on April 24, 2026, if you have a Copilot Free, Copilot Pro, Copilot Pro+, or Copilot Max plan, GitHub may use your interactions with GitHub features and services—including inputs, outputs, code snippets, and associated context—to train and improve AI models… You can opt-out from allowing your data to be used for training in your personal settings for GitHub Copilot."*

Ruta de opt-out individual: Copilot settings → **"Allow GitHub to use my data for AI model training"** → **Disabled**.

**Consecuencia práctica y regla de gobernanza del curso: usar siempre la cuenta corporativa con el asiento Enterprise.** Si un participante trabaja con su cuenta personal Pro o Pro+, sus prompts, sus respuestas y los fragmentos de código enviados **sí pueden usarse para entrenamiento por defecto**, y el opt-out es una acción que debe realizar él y que nadie puede auditar desde la organización.

**Verificación operativa.** Al inicio del Día 1 cada participante comprueba en `https://github.com/settings/copilot` que el plan mostrado es **Copilot Enterprise concedido por la organización**, y en VS Code que la cuenta activa es la corporativa. Con varias cuentas de GitHub configuradas, la que manda es la activa, no la última que se usó en el navegador.

### 3.3 Compromisos de los proveedores de modelos `[GA]`

Página "Hosting of models for GitHub Copilot":

- OpenAI: *"We [OpenAI] do not train models on customer business data."* GitHub mantiene además un **acuerdo de retención cero de datos** con OpenAI.
- Los modelos alojados por Amazon Web Services, Anthropic PBC y Google Cloud Platform están cubiertos por acuerdos de proveedor *"to ensure data is not used for training"*.
- Gemini: *"Gemini doesn't use your prompts, or its responses, as data to train its models."*
- Grok 4.5: el contenido *"will not be … Used for model training"*.

### 3.4 Excepción a vigilar: Claude Fable 5 `[GA]` `[Depende del plan/política]`

**Es el único modelo del catálogo listado como no cubierto por el acuerdo de retención de datos de GitHub.**

> *"Anthropic retains data, including prompts and outputs, to operate safety classifiers… Enterprise and business users need to enable the Claude Fable 5 model to make it available for your organization."*

Consecuencias prácticas:

- No es elegible para habilitación por defecto: requiere **habilitación explícita** por parte de la organización o la empresa.
- Si la organización lo habilita, el equipo debe saber que las condiciones de retención de ese modelo son distintas de las del resto del catálogo.
- **Recomendación del curso:** no usarlo con código de cliente hasta que la organización se pronuncie por escrito. Si aparece en el model picker, es que alguien lo habilitó; conviene preguntar quién y con qué criterio.

Los otros grupos no elegibles para habilitación por defecto son: los modelos explícitamente deshabilitados, los modelos pre-GA, los modelos **open weight** (DeepSeek, Kimi K2.7 Code, Kimi K3) y los restringidos por residencia de datos o FedRAMP.

### 3.5 Lo que la privacidad **no** resuelve

Que los datos no se usen para entrenar no significa que enviarlos sea gratis. Tres límites que conviene tener presentes:

1. El contenido enviado **sale del perímetro** de la organización y su tratamiento se rige por el acuerdo con el proveedor, no por las políticas internas.
2. Los ajustes de privacidad **no impiden** que un secreto pegado en un prompt quede registrado en algún sistema intermedio.
3. La retención cero de datos aplica al proveedor del modelo, no a la trazabilidad interna de GitHub ni a los registros de auditoría de la organización.

---

## 4. Code referencing y sugerencias que coinciden con código público

### 4.1 Tres nombres para lo mismo

Conviene no confundirlos, porque aparecen en sitios distintos de la interfaz:

| Contexto | Nombre exacto |
|---|---|
| Concepto o funcionalidad | **GitHub Copilot code referencing** |
| Ajuste o política | **Suggestions matching public code** — opciones **Allow** / **Block** |
| Fila en la tabla comparativa de planes | **Block suggestions matching public code** |

### 4.2 Cómo funciona `[GA]`

Datos verificados, citados literalmente de la documentación:

- *"Copilot code referencing compares potential code suggestions and the surrounding code of about **150 characters** against an index of **all public repositories on GitHub.com**."*
- *"**Code in private GitHub repositories, or code outside of GitHub, is not included** in the search process."*
- *"Code referencing for inline suggestions only occurs for matches of **accepted** Copilot suggestions. **Code you have written, and Copilot suggestions you have altered, are not checked** for matches to public code."*
- *"Typically, matches to public code occur in **less than one percent** of Copilot suggestions."*
- *"The search index is refreshed **every few months**."*
- Superficies donde se muestran las referencias: JetBrains IDEs, Visual Studio, Visual Studio Code, Copilot cloud agent y el sitio web de GitHub.

En modo **Block**: *"GitHub Copilot checks code suggestions with their surrounding code of about 150 characters against public code on GitHub. If there is a match, or a near match, **the suggestion is not shown to you**."*

### 4.3 Estado por defecto y quién lo controla

- **"Suggestions matching public code" está en Blocked por defecto para los usuarios de Copilot Business.** El ajuste se cambia en la sección **Privacy** de la página de políticas de Copilot.
- El estado por defecto **para Copilot Enterprise y para los planes individuales no está documentado oficialmente**. No lo afirmes en clase: solo está publicado el valor por defecto de Business.
- El ajuste **se hereda de la organización o de la empresa**: *"If you are a member of an organization on GitHub Enterprise Cloud who has been assigned a GitHub Copilot seat through your organization, you will not be able to configure suggestions matching public code in your personal account settings."*
- En conflictos entre varias organizaciones, para esta política **gana la más restrictiva**.

### 4.4 Qué significa esto en la práctica

Cuatro consecuencias que conviene entender antes de sacar conclusiones:

1. **Con Block activo, el filtro reduce el riesgo pero no lo elimina**, porque solo se comprueban las sugerencias aceptadas sin modificar. Una sugerencia que el desarrollador retoca queda fuera de la comprobación.
2. **El índice se refresca cada pocos meses**, así que el código público muy reciente puede no estar en él.
3. **Menos del 1 % de las sugerencias coincide.** Es una cifra baja, pero sobre un volumen alto de sugerencias no es cero. La conclusión correcta no es "no pasa nunca", sino "hay que saber qué hacer cuando pasa".
4. **La revisión humana sigue siendo la barrera principal.** La señal a la que hay que atender es un fragmento cuyo estilo, idioma o convenciones de nombres no encajan con el resto del archivo: es la marca típica de código traído de otro contexto.

---

## 5. Content exclusion y sus límites reales

### 5.1 Qué hace `[GA]` en IDEs · `[Preview]` en GitHub.com y GitHub Mobile · `[Depende del plan/política]`

Requiere una organización con plan **Copilot Business o Copilot Enterprise**. Pueden gestionarla los administradores de repositorio, los propietarios de organización y los propietarios de empresa; quien tiene el rol "Maintain" puede ver la configuración pero no editarla.

Efectos documentados sobre los archivos excluidos:

- *"Inline suggestions will not be available in the affected files."*
- *"The content in affected files will not inform inline suggestions in other files."*
- *"The content in affected files will not inform GitHub Copilot Chat's responses."*
- *"Affected files will not be reviewed in a Copilot code review."*

### 5.2 Los cinco límites que hay que conocer

Esta es la parte que se omite en la mayoría de las presentaciones y la que más consecuencias tiene para el gobierno.

| # | Límite verificado | Consecuencia práctica |
|---|---|---|
| 1 | *"**GitHub Copilot CLI and Agent mode in Copilot Chat in IDEs do not support content exclusion.**"* Y también: *"Content exclusion is currently **not supported in Edit and Agent modes** of Copilot Chat in Visual Studio Code and other editors."* | **La exclusión no protege en el flujo de trabajo agéntico ni en la CLI**, que son precisamente los modos donde más archivos se leen. Es el límite más importante de todos |
| 2 | *"Currently, content exclusions **do not apply to symbolic links (symlinks) and repositories located on remote filesystems**."* | Un repositorio montado en red o accedido por symlink queda fuera de la protección sin que nada lo indique |
| 3 | *"It's possible that Copilot may use **semantic information** from an excluded file if the information is provided by the IDE indirectly. Examples: type information and hover-over definitions for symbols used in code, as well as general project properties such as build configuration information."* | La exclusión oculta el contenido del archivo, **no la información semántica que el IDE expone sobre él**: firmas, tipos, configuración de compilación |
| 4 | La propagación tarda *"up to **30 minutes** to take effect in IDEs where the settings are already loaded."* | Un archivo recién excluido **sigue siendo visible durante media hora**. Recarga manual: en VS Code, Command Palette → **Developer: Reload Window**; en JetBrains y Visual Studio, reiniciar la aplicación |
| 5 | En GitHub.com y GitHub Mobile la funcionalidad está en **public preview** y sujeta a cambios | No se debe construir un control de cumplimiento sobre una funcionalidad en preview en esas superficies |

**Prueba recomendada para comprobar que funciona:** abrir un archivo excluido, adjuntarlo como contexto y pedir `explain this file`. Si Copilot lo explica, la exclusión no está surtiendo efecto (o no han pasado los 30 minutos).

### 5.3 La conclusión de gobierno

**Content exclusion es una medida de reducción de exposición, no un control de seguridad.** Si un archivo no puede salir del perímetro bajo ninguna circunstancia, la respuesta correcta no es excluirlo de Copilot: es que no esté en un repositorio al que Copilot tenga acceso, o que el secreto que contiene no exista como texto en ningún archivo.

Aplicado al repositorio del curso: excluir `appsettings.json` no resuelve BUG-03. La solución es que la credencial no esté ahí.

---

## 6. Señales de dependencia automática en un equipo

La dependencia automática no es usar mucho Copilot: es dejar de aplicar criterio. Aparece de forma gradual y por eso conviene tener señales objetivas.

| # | Señal observable | Cómo se detecta | Cómo se corrige |
|---|---|---|---|
| 1 | **Los PR llegan con código que el autor no sabe explicar** | En la revisión, preguntar por qué una línea concreta está ahí. Si la respuesta es "lo generó Copilot", la señal está activa | Regla explícita: quien no pueda explicar una línea la reescribe. Se aplica en la revisión, no como recordatorio genérico |
| 2 | **Las tablas de decisiones de los PR están vacías o solo tienen "Aceptada"** | Recuento mensual de PR con la tabla rellenada y proporción aceptado/modificado/descartado | Convertir la tabla en requisito de fusión. Revisar en retrospectiva los PR sin ninguna decisión de tipo "modificado" o "descartado" |
| 3 | **Las pruebas se generan después del código, siempre** | Revisar el orden de los commits: si el commit de pruebas es siempre posterior al de implementación, la prueba se escribió contra el código | Escribir primero la prueba que falla, al menos para los defectos y los umbrales. Es la práctica de `P-DEP-05` |
| 4 | **Los defectos que llegan a integración son de casos borde** | Clasificar los defectos por tipo durante un trimestre | Los casos borde son exactamente lo que el modelo omite. Reforzar `P-TST-03` y la revisión de umbrales por pares |
| 5 | **Nadie descarta nunca un comentario de Copilot code review** | Recuento de comentarios clasificados como "Incorrecto" con evidencia | Si el porcentaje es cero, no se está leyendo con criterio. Introducir la clasificación obligatoria de `P-PRR-03` |
| 6 | **El equipo ya no discute alternativas de diseño** | Recuento de ADR escritos por trimestre | Reinstaurar el ADR breve como requisito para toda decisión con consecuencias. Usar `P-PLA-07` para generar alternativas, no para elegirlas |
| 7 | **Aumenta el tiempo de revisión de los PR** | Tiempo medio entre apertura y primera revisión humana | Suele indicar PR más grandes, generados de una vez. Limitar el tamaño y exigir plan previo para cambios de más de cinco archivos |
| 8 | **Las personas más jóvenes del equipo no saben depurar sin asistencia** | Ejercicio periódico de depuración sin herramienta | Reservar tareas de aprendizaje explícitamente sin asistencia. El objetivo del equipo no es entregar rápido hoy, es tener criterio dentro de dos años |
| 9 | **Se aceptan afirmaciones sobre el producto sin verificar** | Aparición en documentos internos de datos que la documentación oficial no respalda | Regla de verificación con fuente y fecha de consulta, como la que aplica esta guía |
| 10 | **La cobertura de pruebas sube y los defectos no bajan** | Cruce de las dos métricas durante un trimestre | Señal de pruebas que ejecutan código sin afirmar nada relevante. Auditar con `P-TST-02` y con el experimento de estropear el código a propósito |

**Cómo se corrige una dependencia ya instalada.** No con prohibiciones generales, que se ignoran. Con tres cambios concretos: hacer obligatoria la declaración de decisiones en el PR, exigir que la prueba de un defecto se escriba **antes** que la corrección, y reservar de forma explícita algunas tareas para hacerlas sin asistencia con el objetivo declarado de mantener la competencia.

---

## 7. Decálogo de uso responsable

Página imprimible. Se recomienda tenerla visible durante los laboratorios.

---

### Decálogo de uso responsable de GitHub Copilot

**1. Firmo lo que entrego.** El código es mío aunque lo haya escrito una herramienta. "Lo generó Copilot" no es una explicación.

**2. Leo antes de aceptar.** Cada línea que llega al repositorio la he leído y la entiendo. Si no sé defenderla en una revisión, la reescribo.

**3. Ejecuto antes de afirmar.** `dotnet build` sin warnings y `dotnet test` en verde, después del último cambio y no antes. Y pruebo al menos un escenario contra la aplicación en marcha.

**4. No pego secretos.** Ni credenciales, ni tokens, ni datos de cliente, ni logs de producción. Ni en el chat, ni como archivo adjunto, ni en la terminal que después envío como contexto.

**5. Uso la cuenta corporativa.** Con el asiento Enterprise mis datos no se usan para entrenar modelos. Con una cuenta personal, desde el 24 de abril de 2026, sí lo hacen por defecto.

**6. Escribo los criterios de aceptación antes del prompt.** Si no sé qué respuesta espero, no puedo juzgar la que reciba, y acabaré aceptando la primera que compile.

**7. Verifico las afirmaciones.** Abro cada línea citada. Contrasto cada dato de producto con la documentación oficial del dominio correcto y anoto la fecha de consulta.

**8. No delego las decisiones.** La arquitectura, la regla de negocio, el contrato que se rompe y la aprobación del PR son mías. Copilot enumera alternativas; yo elijo y lo documento.

**9. Registro por qué.** Cada decisión relevante va a la bitácora con su motivo y con la forma concreta en que la verifiqué. Aceptado, modificado o descartado: los tres cuentan.

**10. Sé cuándo parar.** Si dos iteraciones no mejoran la salida, lo escribo a mano. Reconocerlo también es una decisión técnica, y también se registra.

---

## 8. Trazabilidad

| Sección | Requisito | Resultado de aprendizaje | Módulo |
|---|---|---|---|
| 1. Principios | R-093, R-160 | RA-5.5 | M1, M4, M5 |
| 2. Qué delegar | R-093 | RA-5.3, RA-5.5 | M3, M5 |
| 3. Privacidad y datos | R-046, R-093 | RA-1.5 | M1, M5 |
| 4. Code referencing | R-052, R-093 | RA-1.5 | M1 |
| 5. Content exclusion | R-051, R-093 | RA-1.5, RA-5.2 | M1, M5 |
| 6. Dependencia automática | R-093, R-160 | RA-5.4 | M5 |
| 7. Decálogo | R-160 | Todos | Todos |

**Documentos relacionados:** `06_recursos/plantilla_bitacora_decisiones.md` (instrumento de los principios P1 y P4), `06_recursos/checklist_pull_request.md` (verificación de P2 y P4), `06_recursos/plantilla_guia_equipo.md` (traslado de estos principios a la guía del equipo), `06_recursos/ficha_tecnica_copilot_verificada.md` (fuente de todos los datos de producto citados).
