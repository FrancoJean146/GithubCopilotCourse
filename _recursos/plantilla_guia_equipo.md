# Plantilla de guía de uso de GitHub Copilot para el equipo

**Entregable:** requisito contractual R-095 · Jornada 5 · **LAB-09** (18 minutos de laboratorio; se termina como tarea posterior). **Stack:** C# / .NET 8 (ASP.NET Core Web API + xUnit) · VS Code · GitHub Copilot Enterprise. **Repositorio de práctica:** `copilot-lab-catalogo` (`05_codigo_laboratorios/inicial/`).
**Fecha de verificación técnica del contenido de apoyo:** 2026-08-14. Fuente técnica: `06_recursos/ficha_tecnica_copilot_verificada.md`.

> Esta capacitación es una propuesta independiente de carácter corporativo y práctico. No corresponde a un curso oficial de Microsoft, no utiliza su denominación de certificación y no incluye examen ni acreditación oficial.

## Cómo se usa esta plantilla

El documento tiene dos partes separadas:

- **Parte A — La plantilla.** Once bloques. Cada bloque indica qué debe contener, cómo redactarlo, un ejemplo correcto, un ejemplo incorrecto y el espacio para rellenar, marcado con `<...>`.
- **Parte B — Ejemplo parcialmente rellenado** para `copilot-lab-catalogo`, completado al 60 % aproximadamente, con campos `<PENDIENTE: decidir en la sesión>` que el equipo debe resolver.

Reglas de redacción para quien complete la guía:

1. Toda capacidad mencionada se marca con `[GA]`, `[Preview]` o `[Depende del plan/política]`.
2. Nada de afirmaciones no verificadas. Si un dato no se pudo comprobar, se escribe **"no verificado"** y se deja la fuente pendiente.
3. Productos, comandos, rutas e identificadores en inglés y entre backticks. Prosa en español con acentuación correcta.
4. Terminología obligatoria: **Session Target** y **agent role** (`Agent` / `Plan`), nunca "modos Ask/Edit/Agent"; **Copilot cloud agent**, nunca "coding agent"; **custom agents** con `.agent.md`, nunca `.chatmode.md`; **custom instructions**, nunca "coding guidelines"; `#codebase` (namespaced `#search/codebase`), nunca `@workspace` en VS Code. Sin emojis y sin secretos reales: el único secreto de demostración del material es `sk-FAKE-DEMO-NOT-A-REAL-SECRET-0000`, evidentemente falso.

---

# PARTE A — La plantilla

## A1. Identificación y control del documento

**Qué debe contener:** versión, fecha de emisión, responsable nombrado (persona, no área), fecha de la próxima revisión y alcance de aplicación en una sola línea.

**Instrucción de redacción:** una tabla, sin prosa. El responsable es una persona con nombre y rol; "el equipo" no es un responsable.

**Ejemplo correcto**

| Campo | Valor |
|---|---|
| Versión | 1.0 |
| Fecha de emisión | 2026-08-14 |
| Responsable | Ana Restrepo, líder técnica de Catálogo |
| Próxima revisión | 2026-11-14 |
| Alcance de aplicación | Repositorios de la célula Catálogo en la organización `contoso-eng` |

**Ejemplo incorrecto**

> Documento vivo del área de desarrollo. Se actualiza cuando sea necesario. Responsable: el equipo.

Falla en tres puntos: no hay versión, no hay fecha de revisión y no hay una persona responsable.

**Espacio para rellenar**

| Campo | Valor |
|---|---|
| Versión | `<n.m>` |
| Fecha de emisión | `<AAAA-MM-DD>` |
| Responsable | `<nombre y rol>` |
| Próxima revisión | `<AAAA-MM-DD>` |
| Alcance de aplicación | `<organización, célula o repositorios>` |

## A2. Alcance y audiencia

**Qué debe contener:** a qué equipos, repositorios y superficies de Copilot aplica la guía, y qué queda explícitamente fuera.

**Instrucción de redacción:** enumera las superficies con su nombre de producto. Lo que queda fuera se escribe, no se sobreentiende.

**Ejemplo correcto**

- Aplica a: los repositorios `catalogo-api` y `copilot-lab-catalogo`, en las superficies Copilot Chat en VS Code `[GA]`, inline suggestions `[GA]` y Copilot code review en GitHub.com `[GA]`.
- Queda fuera: Copilot cloud agent `[Depende del plan/política: la política "Copilot cloud agent" está deshabilitada por defecto para licencias Business/Enterprise asignadas por la organización]` y los servidores MCP de terceros, hasta que exista una excepción aprobada.

**Ejemplo incorrecto**

> Aplica a todo el mundo que use inteligencia artificial en la empresa.

Falla porque no nombra repositorios ni superficies, y porque "inteligencia artificial" no es el objeto regulado.

**Espacio para rellenar**

- Equipos y repositorios alcanzados: `<lista>`
- Superficies alcanzadas: `<Chat en VS Code | inline suggestions | Copilot code review | Copilot CLI | Copilot cloud agent>`
- Queda expresamente fuera: `<lista con el motivo>`

## A3. Herramientas y versiones aprobadas

**Qué debe contener:** una tabla con herramienta, versión mínima, forma de instalación y quién aprueba el cambio de esa línea.

**Instrucción de redacción:** la columna "cómo se instala" contiene el comando o el paso exacto, no una descripción. Nunca escribas "instalar la extensión de GitHub Copilot": **Copilot Chat es built-in en VS Code desde la versión 1.116** y la extensión `GitHub.copilot` está en ruta de deprecación.

**Ejemplo correcto**

| Herramienta | Versión mínima | Cómo se instala | Quién aprueba |
|---|---|---|---|
| Visual Studio Code con **Copilot Chat built-in** `[GA]` | 1.116 (recomendado 1.133) | Instalación estándar de VS Code; después iniciar sesión con la cuenta corporativa y activar **Use AI Features** desde el icono de Copilot en la Status Bar. **No se instala ninguna extensión de Copilot** | `<rol>` |
| `GitHub.vscode-pull-request-github` `[GA]` | La publicada en el Marketplace | Instalación manual separada desde la vista **Extensions** | `<rol>` |
| .NET SDK | 8.0 (`net8.0`) | `winget install Microsoft.DotNet.SDK.8` | `<rol>` |
| Git | 2.40 | `winget install Git.Git` | `<rol>` |
| GitHub Copilot CLI `[GA]` `[Depende del plan/política: requiere la política "Copilot CLI" habilitada en la organización]` | La instalada por el gestor | `winget install GitHub.Copilot` (evita la dependencia de Node.js 22+). En Windows requiere PowerShell 6 o superior | `<rol>` |
| Visual Studio 2022 / VS 2026 (alternativa documentada) | 17.14 o superior | Instalador de Visual Studio; Copilot Chat viene incluido con todas las workloads desde 17.10 | `<rol>` |

**Ejemplo incorrecto**

> Instalar la extensión de GitHub Copilot y la extensión de Copilot Chat desde el Marketplace, y usar la última versión de todo.

Falla porque instruye a instalar extensiones innecesarias, una de ellas en deprecación (`GitHub.copilot`), y porque "la última versión" no es una versión mínima verificable.

**Espacio para rellenar**

| Herramienta | Versión mínima | Cómo se instala | Quién aprueba |
|---|---|---|---|
| `<herramienta>` `<[GA] / [Preview] / [Depende del plan/política]>` | `<versión>` | `<comando o paso exacto>` | `<rol>` |

## A4. Usos permitidos, condicionados y prohibidos

**Qué debe contener:** una tabla de tres columnas, con al menos seis filas, específica del stack .NET. Todo uso condicionado lleva su condición escrita.

**Instrucción de redacción:** cada celda describe una acción concreta sobre este código, no una categoría abstracta. Una condición que no se puede comprobar no es una condición.

**Ejemplo correcto**

| Permitido | Condicionado (condición explícita) | Prohibido |
|---|---|---|
| Generar pruebas xUnit para `DescuentoCalculator` y `ProductoService` | Generar código de producción — **condición:** revisión línea a línea por la persona autora y `dotnet test` en verde antes del commit | Pegar secretos, credenciales, cadenas de conexión, datos de cliente o PII en el prompt |
| Explicar código heredado, por ejemplo `Infrastructure/LegacyPricingClient.cs` | Cambios que tocan más de cinco archivos — **condición:** plan aprobado antes de editar (agent role `Plan` o `/plan`), registrado en el PR | Aceptar código generado sin ejecutar `dotnet build` y `dotnet test` |
| Generar documentación XML (`///`) en español para miembros públicos | Uso del agent role `Agent` con auto-aprobación de herramientas — **condición:** solo sobre una rama de trabajo desechable y nunca con `--allow-all-tools` / `--yolo` sobre `main` | Delegar decisiones de arquitectura o de negocio a Copilot |
| Refactorizar con la suite de pruebas en verde antes y después del cambio | Uso de servidores MCP de terceros — **condición:** servidor en la lista aprobada, confianza concedida explícitamente y `[Depende del plan/política: la política "MCP servers in Copilot" está deshabilitada por defecto]` | Usar una cuenta personal de Copilot en lugar del asiento corporativo |
| Redactar la descripción del Pull Request a partir del diff | Uso de **Copilot cloud agent** — **condición:** issue con criterios de aceptación escritos y `[Depende del plan/política: política "Copilot cloud agent" deshabilitada por defecto en Business/Enterprise]` | Modificar o eliminar pruebas existentes para que la suite pase |
| Analizar logs y trazas de error para formular hipótesis de depuración | Uso de modelos no cubiertos por el acuerdo de retención de datos de GitHub — **condición:** aprobación previa del responsable de la guía; hoy el único caso listado es **Claude Fable 5**, donde el proveedor retiene prompts y salidas para operar clasificadores de seguridad | Introducir dependencias NuGet nuevas sin justificación escrita en el PR |

**Ejemplo incorrecto**

> Se permite usar Copilot con sentido común y evitando información sensible.

Falla porque no es verificable: no dice qué es sensible, ni quién comprueba, ni qué ocurre si no se cumple.

**Espacio para rellenar**

| Permitido | Condicionado (condición explícita) | Prohibido |
|---|---|---|
| `<acción concreta sobre este código>` | `<acción>` — **condición:** `<condición comprobable>` | `<acción concreta>` |

## A5. Convenciones de código que Copilot debe respetar, y cómo se expresan

**Qué debe contener:** el mecanismo real por el que una convención llega al modelo, la precedencia oficial y una tabla convención → dónde se declara → cómo se comprueba que se aplicó.

**Instrucción de redacción:** una convención que no está declarada en un archivo de custom instructions no es una convención para Copilot; es una expectativa. Cada fila necesita una comprobación observable.

**Precedencia oficial `[GA]`** (de mayor a menor prioridad):

1. Instrucciones **personales**.
2. Instrucciones de **repositorio**: **path-specific** (`.github/instructions/**/*.instructions.md`) → **repository-wide** (`.github/copilot-instructions.md`) → **agent** (`AGENTS.md`).
3. Instrucciones de **organización** `[Depende del plan/política]`.

**Sin embargo, todas las instrucciones relevantes se entregan al modelo.** La precedencia resuelve conflictos de prioridad, no excluye archivos. Dentro de un árbol de directorios, gana el `AGENTS.md` más cercano.

**Advertencias verificadas que la guía debe reproducir**

| Advertencia | Consecuencia práctica |
|---|---|
| Si se omite `applyTo` en un `.instructions.md`, las instrucciones **no se aplican automáticamente** | Un archivo sin `applyTo` solo actúa si se adjunta a mano al contexto |
| Las custom instructions **no afectan a las inline suggestions** | Las convenciones no se aplican al escribir con `Tab`; solo en Chat, edición y agent roles |
| Las **instrucciones personales no se soportan en VS Code** | Solo existen en Copilot Chat de GitHub.com, JetBrains y Copilot CLI |
| En Copilot code review, Copilot lee las instrucciones de la **head branch**, no de la base | Si se cambian las instrucciones, hay que llevarlas en la rama del PR para que apliquen a ese PR |
| `AGENTS.md` **no está documentado como soportado en Visual Studio** | Quien trabaje en Visual Studio depende de `.github/copilot-instructions.md` y de las path-specific |
| VS Code combina los archivos de instrucciones **sin garantizar un orden** | No se puede depender de que una instrucción "llegue después" para sobrescribir a otra |

**Ejemplo correcto**

| Convención | Dónde se declara | Cómo se comprueba que se aplicó |
|---|---|---|
| `decimal` para importes, nunca `double`; redondeo `Math.Round(valor, 2, MidpointRounding.AwayFromZero)` | `.github/instructions/csharp.instructions.md` con `applyTo: "**/*.cs"` | Pedir en Chat la generación de un cálculo de precio y verificar el tipo devuelto; revisión en el PR |
| Nombre de prueba `Metodo_Escenario_ResultadoEsperado`, Arrange/Act/Assert visible, un assert lógico | `.github/instructions/tests.instructions.md` con `applyTo: "tests/**/*.cs"` | `dotnet test --list-tests` y lectura del nombre generado |
| Controller solo como transporte HTTP, errores en `ProblemDetails`, `[ProducesResponseType]` en cada acción | `.github/instructions/api.instructions.md` con `applyTo: "src/Catalogo.Api/Controllers/**/*.cs"` | Inspección del controller generado y de la página `/swagger` |
| Prohibido `Console.WriteLine`; DTO inmutables como `record`; nada de secretos en `appsettings.json` | `.github/copilot-instructions.md` (repo-wide, always-on) | Búsqueda en el diff y revisión humana en el PR |
| No modificar pruebas existentes; proponer plan si se tocan más de cinco archivos; reportar la salida real de `dotnet build` y `dotnet test` | `AGENTS.md` en la raíz | Comparación del diff con `git diff --stat` sobre `tests/` |

**Ejemplo incorrecto**

> El equipo acordó en la reunión que Copilot debe usar `decimal` para el dinero.

Falla porque el acuerdo no está en ningún archivo: Copilot no asiste a las reuniones.

**Espacio para rellenar**

| Convención | Dónde se declara (ruta y `applyTo`) | Cómo se comprueba que se aplicó |
|---|---|---|
| `<convención>` | `<ruta>` · `applyTo: "<glob>"` | `<comando o inspección>` |

## A6. Criterios de arquitectura

**Qué debe contener:** las decisiones estructurales que Copilot no puede alterar, y la regla de que las decisiones de arquitectura no se delegan.

**Instrucción de redacción:** cada criterio se escribe como una regla verificable en el diff. La regla de gobierno se escribe al final y en negrita.

**Ejemplo correcto**

| Criterio | Regla |
|---|---|
| Capas | `Controllers` → `Services` → `Repositories`. Un controller nunca llama al repositorio |
| DTO | Inmutables, declarados como `record` con propiedades `init` o posicionales |
| Dependencias | Inyección por constructor, validadas con `ArgumentNullException.ThrowIfNull(...)`. Sin `new` de servicios, sin service locator, sin singletons estáticos mutables |
| Dinero | `decimal` siempre; `double` prohibido para importes |
| Errores | Se traducen a `ProblemDetails`; prohibido `catch { }` |
| Asincronía | Sufijo `Async`, `CancellationToken cancellationToken = default` como último parámetro, propagado hacia abajo |
| Trazas | Prohibido `Console.WriteLine` en código de producción; se usa `ILogger<T>` |

**Las decisiones de arquitectura no se delegan en Copilot: se toman por personas y se registran en un ADR** (`docs/adr/NNNN-<titulo>.md`). Copilot puede usarse para redactar el borrador del ADR y para enumerar alternativas, nunca para elegir.

**Ejemplo incorrecto**

> Le pedimos a Copilot que evaluara si conviene CQRS y seguimos su recomendación.

Falla porque delega una decisión estructural y porque no deja rastro de las alternativas descartadas.

**Espacio para rellenar**

| Criterio | Regla |
|---|---|
| `<criterio>` | `<regla verificable en el diff>` |

Ubicación de los ADR del equipo: `<ruta>`. Quién los aprueba: `<rol>`.

## A7. Flujo de trabajo obligatorio

**Qué debe contener:** la secuencia completa desde la rama hasta el merge, con el punto exacto en el que interviene Copilot code review y qué valor tiene esa revisión.

**Instrucción de redacción:** el flujo se dibuja, no se narra. Toda puerta de calidad se expresa con el comando que la comprueba.

**Advertencia verificada `[GA]`: Copilot code review siempre deja una revisión de tipo "Comment"**, nunca "Approve" ni "Request changes". En consecuencia **no bloquea el merge y no cuenta para las aprobaciones requeridas**. La aprobación humana sigue siendo obligatoria.

**Ejemplo correcto**

```text
1. Rama            git checkout -b feat/<iniciales>-<descripcion>
        |
2. Trabajo         Chat / agent role Agent o Plan · contexto con #codebase
        |
3. Verificación    dotnet build  -> 0 warnings
                   dotnet test   -> suite en verde
        |
4. Commit          mensaje que explica el porque, no solo el que
        |
5. Pull Request    se usa .github/pull_request_template.md
                   (incluida la tabla de decisiones sobre sugerencias de Copilot)
        |
6. Copilot code review   Reviewers -> Copilot -> Request
                         resultado: revision de tipo "Comment"
                         NO aprueba, NO bloquea el merge
        |
7. Revision humana  al menos 1 aprobacion de una persona del equipo
        |
8. Merge           solo con build y test en verde y aprobacion humana
```

**Ejemplo incorrecto**

> Se abre el PR, se pide la revisión de Copilot y, si no comenta nada, se hace merge.

Falla porque trata la ausencia de comentarios como una aprobación, y Copilot no aprueba nunca.

**Espacio para rellenar**

- Convención de rama: `<patrón>`
- Puertas de calidad antes del PR: `<comandos>`
- Plantilla de PR: `<ruta>`
- Aprobaciones humanas requeridas: `<n>`
- Quién puede hacer merge: `<rol>`

## A8. Reglas de seguridad y manejo de secretos

**Qué debe contener:** dónde viven los secretos, dónde no, qué se hace cuando uno se filtra, y los límites reales de content exclusion y de las sugerencias que coinciden con código público.

**Instrucción de redacción:** las reglas se escriben en imperativo y con el nombre exacto de la variable o del comando. El procedimiento de fuga tiene pasos numerados y un responsable.

**Ejemplo correcto**

- Nunca en el código ni en `appsettings.json`. En desarrollo local se usa `dotnet user-secrets set "LegacyPricing:ApiKey" "<valor>"`; en ejecución se usa la variable de entorno `LegacyPricing__ApiKey`.
- El valor de un secreto **nunca se escribe en el log**. Se registra su ausencia con un warning, no su contenido.
- Si un secreto llega a un commit: 1) rotar la credencial de inmediato con `<rol>`; 2) notificar al responsable de seguridad; 3) eliminar el valor del código y de la configuración; 4) reescribir el historial solo si lo autoriza `<rol>`; 5) registrar el incidente. **La rotación es obligatoria aunque el commit no se haya subido.**

**Content exclusion `[GA]` en IDEs / `[Preview]` en GitHub.com y GitHub Mobile / `[Depende del plan/política: requiere Copilot Business o Enterprise]`** — límites verificados que la guía debe declarar:

| Límite | Consecuencia |
|---|---|
| **No se aplica en agent mode ni en Copilot CLI** | Un archivo excluido sigue siendo legible por el agent role `Agent` y por `copilot` |
| No se aplica a **symlinks** ni a repositorios en **filesystems remotos** | Un enlace simbólico a un archivo excluido lo vuelve accesible |
| Puede filtrarse información **semántica** indirecta desde el IDE | Tipos, definiciones al pasar el cursor y propiedades de build pueden llegar al modelo |
| Propagación de **hasta 30 minutos** en IDEs con la configuración ya cargada | Tras cambiar la exclusión, forzar **Developer: Reload Window** en VS Code |

Conclusión operativa: **content exclusion reduce el riesgo, no lo elimina. No es un control suficiente para secretos.**

**Suggestions matching public code `[GA]`:** está **Blocked por defecto para usuarios de Copilot Business**. Si el asiento lo asigna la organización, el ajuste se **hereda de la organización o de la empresa** y **no se puede cambiar en la cuenta personal**. La comprobación de coincidencias solo cubre las sugerencias aceptadas sin modificar; el código escrito por la persona y las sugerencias alteradas no se comprueban.

**Ejemplo incorrecto**

> Los archivos con claves están en content exclusion, así que Copilot no puede verlos.

Falla porque ignora que la exclusión no rige en agent mode ni en Copilot CLI, y porque confunde una mitigación con una garantía.

**Espacio para rellenar**

- Mecanismo de secretos en local y en entornos desplegados: `<dotnet user-secrets | otro>` · `<gestor>`
- Rutas cubiertas por content exclusion: `<lista>`
- Responsable de rotación: `<nombre y rol>`
- Canal de notificación de incidentes: `<canal>`

## A9. Métricas de adopción

**Qué debe contener:** al menos tres métricas; cada una con nombre, definición operativa, fuente de datos exacta, responsable, cadencia, umbral objetivo y trampa de interpretación.

**Instrucción de redacción:** la fuente es el nombre exacto del dashboard, del endpoint o del artefacto, no "GitHub". Sin trampa de interpretación declarada, la métrica no se aprueba.

**Advertencia pedagógica obligatoria: la tasa de aceptación de sugerencias no es una medida de productividad ni de calidad.** Se puede aceptar mucho código malo y rechazar mucho código bueno.

**Advertencias verificadas sobre las fuentes**

| Advertencia | Consecuencia |
|---|---|
| La política **"Copilot usage metrics" debe estar habilitada** `[Depende del plan/política]` | Sin ella no hay dashboards ni informes |
| Latencia de datos de **2 a 3 días UTC** (la documentación publica dos cifras oficiales distintas: "dos días completos" y "hasta tres días UTC") | No se toman decisiones sobre los últimos tres días |
| **Copilot Chat en GitHub.com y GitHub Mobile están excluidos de las métricas** | El uso en esas superficies no aparece; no se interpreta como falta de uso |
| Los datos de **licencias y asientos no vienen en los informes de uso** | Son otro recurso: `REST API endpoints for Copilot user management`, que expone `last_activity_at` |
| Las analíticas a nivel de organización existen **desde el 12 de diciembre de 2025** | No hay serie histórica anterior a esa fecha |

**Ejemplo correcto**

| Campo | M-01 Cobertura de asientos activos |
|---|---|
| Definición operativa | Porcentaje de asientos asignados al equipo con al menos dos días activos en la ventana móvil de 28 días |
| Fuente de datos exacta | **Copilot usage metrics dashboard** (ventana de 28 días), en Enterprise → pestaña **Insights** → **Copilot usage**. Automatizable con `GET /orgs/{org}/copilot/metrics/reports/organization-28-day/latest` y header `X-GitHub-Api-Version: 2026-03-10`. El denominador de asientos se toma de `REST API endpoints for Copilot user management` |
| Responsable | `<nombre y rol>` |
| Cadencia | Mensual |
| Umbral objetivo | `<porcentaje>` |
| Trampa de interpretación | Un asiento inactivo puede corresponder a alguien que trabaja en Copilot Chat de GitHub.com, superficie excluida de las métricas. Antes de retirar un asiento hay que preguntar |

| Campo | M-02 Distribución por cohorte de adopción |
|---|---|
| Definición operativa | Reparto del equipo entre **Passive users**, **Phase 1: Code first**, **Phase 2: Agent first** y **Phase 3: Multi-agent** |
| Fuente de datos exacta | **Copilot impact dashboard**, Enterprise → **Insights** → **Copilot impact**. Umbral de actividad de la propia herramienta: "al menos dos días activos en la ventana móvil de 28 días". La etiqueta de datos de Passive users es `No Cohort` |
| Responsable | `<nombre y rol>` |
| Cadencia | Trimestral |
| Umbral objetivo | `<distribución esperada>` |
| Trampa de interpretación | Una fase superior no significa mayor calidad. "Phase 3: Multi-agent" describe cómo se trabaja, no cómo de bueno es el resultado |

| Campo | M-03 Adoption multiplier |
|---|---|
| Definición operativa | Cociente entre los PRs mergeados promedio de los usuarios comprometidos (Phase 1, 2 y 3) y los de **Passive users** |
| Fuente de datos exacta | **Copilot impact dashboard** (métrica calculada por el propio dashboard). Exportación cruda para BI: **Copilot usage metrics NDJSON export** |
| Responsable | `<nombre y rol>` |
| Cadencia | Trimestral |
| Umbral objetivo | `<valor>` |
| Trampa de interpretación | Es una correlación, no una causa. Quien ya abría más PRs tiende a adoptar antes; el multiplicador puede reflejar el perfil de la persona y no el efecto de la herramienta. Además, contar PRs premia dividir el trabajo, no mejorarlo |

Métricas propias del equipo, que **no dependen de GitHub** y que el equipo puede medir desde el primer día:

| Campo | M-04 Trazabilidad de decisiones sobre sugerencias |
|---|---|
| Definición operativa | Porcentaje de PRs mergeados en el periodo cuya sección **"Decisiones sobre sugerencias de Copilot"** del `pull_request_template.md` está rellenada con al menos una fila con motivo |
| Fuente de datos exacta | Cuerpo de los PRs del repositorio, sección definida en `.github/pull_request_template.md`; recuento manual o con `gh pr list --state merged --json body` |
| Responsable | `<nombre y rol>` |
| Cadencia | Quincenal |
| Umbral objetivo | `<porcentaje>` |
| Trampa de interpretación | Mide disciplina documental, no calidad de criterio. Una tabla rellenada con "Aceptada / porque funciona" cuenta igual que una bien razonada: hay que leer una muestra |

| Campo | M-05 Proporción aceptado / modificado / descartado |
|---|---|
| Definición operativa | Reparto de las sugerencias registradas en las bitácoras individuales y en las tablas de decisión de los PRs entre las tres categorías |
| Fuente de datos exacta | Bitácoras del equipo (`<ruta>`) y tabla de decisiones del `pull_request_template.md` |
| Responsable | `<nombre y rol>` |
| Cadencia | Mensual |
| Umbral objetivo | `<reparto esperado>` |
| Trampa de interpretación | No es la tasa de aceptación de GitHub ni debe compararse con ella: es un registro declarado por personas y sirve para conversar, no para evaluar a nadie. Un 100 % de "aceptado" es una señal de alarma, no de éxito |

**Ejemplo incorrecto**

> Métrica: porcentaje de código escrito por Copilot. Objetivo: superar el 40 % este trimestre.

Falla en todo: la fuente no existe como tal, el objetivo premia el volumen y no la calidad, y no declara ninguna trampa de interpretación.

**Espacio para rellenar**

| Campo | `<M-0n nombre>` |
|---|---|
| Definición operativa | `<definición>` |
| Fuente de datos exacta | `<dashboard, endpoint o artefacto>` |
| Responsable y cadencia | `<nombre y rol>` · `<periodicidad>` |
| Umbral objetivo | `<valor>` |
| Trampa de interpretación | `<qué NO se puede concluir>` |

## A10. Proceso de excepción

**Qué debe contener:** quién puede pedir una excepción, el formulario mínimo, quién aprueba, dónde se registra y cómo caduca.

**Instrucción de redacción:** toda excepción tiene fecha de vencimiento. Una excepción sin vigencia se convierte en la norma.

**Ejemplo correcto**

- **Quién puede pedirla:** cualquier persona del equipo, con el visto bueno de su líder técnica.
- **Formulario mínimo:** qué se pide · por qué · riesgo identificado · mitigación · vigencia (fecha de fin).
- **Quién aprueba:** `<rol>`, con consulta a seguridad cuando la excepción toca secretos, MCP o modelos.
- **Dónde se registra:** issue con la etiqueta `copilot-excepcion` en `<repositorio>`, enlazada desde el anexo de esta guía.
- **Cómo caduca:** vence en la fecha indicada, máximo 90 días. La renovación exige una solicitud nueva; no hay prórroga automática.

**Ejemplo incorrecto**

> Las excepciones se piden por chat al líder y quedan aprobadas si no hay objeción.

Falla porque el silencio no es aprobación, no hay registro y no hay caducidad.

**Espacio para rellenar**

| Campo | Valor |
|---|---|
| Quién puede solicitar | `<rol>` |
| Campos del formulario | `<qué · por qué · riesgo · mitigación · vigencia>` |
| Quién aprueba | `<rol>` |
| Dónde se registra | `<ubicación>` |
| Vigencia máxima | `<días>` |

## A11. Gobierno del documento

**Qué debe contener:** responsable, cadencia de revisión, mecanismo para proponer cambios e historial de versiones.

**Instrucción de redacción:** además de la cadencia ordinaria, hay que prever la revisión extraordinaria. Copilot cambia de nombres con frecuencia y esta guía cita nombres de producto.

**Ejemplo correcto**

- Responsable: `<nombre y rol>`.
- Cadencia ordinaria: **trimestral**.
- **Revisión extraordinaria obligatoria ante cualquier renombrado de producto** o cambio de política. Precedentes recientes: "Copilot coding agent" pasó a **Copilot cloud agent** (abril de 2026); `.chatmode.md` pasó a **`.agent.md`**; las "coding guidelines" de code review fueron eliminadas y reemplazadas por **custom instructions**; el selector de tres modos de VS Code fue sustituido por **Session Target** y **agent role**.
- Cómo se proponen cambios: Pull Request sobre este archivo, con la sección afectada y la fuente que justifica el cambio.

**Historial de versiones**

| Versión | Fecha | Autor | Cambio |
|---|---|---|---|
| `<n.m>` | `<AAAA-MM-DD>` | `<nombre>` | `<resumen>` |

**Ejemplo incorrecto**

> Este documento se revisa cuando haga falta.

Falla porque no hay cadencia, ni disparador, ni historial.

---

# PARTE B — Ejemplo parcialmente rellenado: `copilot-lab-catalogo`

Versión de la guía completada al 60 % aproximadamente. Los campos `<PENDIENTE: decidir en la sesión>` son los que el equipo debe resolver en LAB-09.

## B1. Identificación y control

| Campo | Valor |
|---|---|
| Versión | 0.9 (borrador de LAB-09) |
| Fecha de emisión | 2026-08-14 |
| Responsable | `<PENDIENTE: decidir en la sesión — designar una persona con nombre y rol>` |
| Próxima revisión | 2026-11-14 |
| Alcance de aplicación | Repositorio `copilot-lab-catalogo` (`05_codigo_laboratorios/inicial/`) y sus ramas `feat/*` |

## B2. Alcance y audiencia

- Equipos y repositorios alcanzados: participantes del curso (trabajo individual, una rama y un PR por persona) sobre `copilot-lab-catalogo`.
- Superficies alcanzadas: Copilot Chat en VS Code `[GA]`, inline suggestions `[GA]`, Copilot code review en GitHub.com `[GA]`.
- Queda expresamente fuera: Copilot cloud agent y Copilot CLI durante los laboratorios 1 a 8; se muestran únicamente en DEMO-10 `[Depende del plan/política]`.

## B3. Herramientas y versiones aprobadas

| Herramienta | Versión mínima | Cómo se instala | Quién aprueba |
|---|---|---|---|
| VS Code con **Copilot Chat built-in** `[GA]` | 1.116 | Instalación estándar; sign-in corporativo; **Use AI Features** desde la Status Bar. No se instala ninguna extensión de Copilot | Instructor |
| `GitHub.vscode-pull-request-github` `[GA]` | Marketplace | Instalación manual separada. Está recomendada en `.vscode/extensions.json` junto con `ms-dotnettools.csdevkit` y `ms-dotnettools.csharp` | Instructor |
| .NET SDK | 8.0 | `winget install Microsoft.DotNet.SDK.8` | Instructor |
| Git | 2.40 | `winget install Git.Git` | Instructor |
| Copilot CLI `[GA]` `[Depende del plan/política: requiere la política "Copilot CLI" habilitada]` | La instalada por WinGet | `winget install GitHub.Copilot` | `<PENDIENTE: decidir en la sesión — quién autoriza su uso fuera de la demostración>` |
| Visual Studio 2022 / VS 2026 (alternativa) | 17.14 | Instalador de Visual Studio | Instructor |

## B4. Usos permitidos, condicionados y prohibidos (extracto acordado)

| Permitido | Condicionado | Prohibido |
|---|---|---|
| Generar pruebas xUnit para `DescuentoCalculator` (umbrales 9/10, 49/50, 99/100 y tope del 20 %) | Implementar `PATCH /api/productos/{id}/precio` — **condición:** revisión línea a línea y `dotnet test` en verde | Pegar en el prompt cualquier valor con aspecto de credencial, incluido `sk-FAKE-DEMO-NOT-A-REAL-SECRET-0000` fuera del ejercicio de seguridad |
| Explicar `Infrastructure/LegacyPricingClient.cs` con `#codebase` | Cambios que tocan más de cinco archivos — **condición:** plan previo con el agent role `Plan`, pegado en el PR (regla 7 de `AGENTS.md`) | Aceptar código sin ejecutar `dotnet build` y `dotnet test` |
| Generar documentación XML en español para los miembros públicos (TODO-03) | Servidor MCP de GitHub declarado en `.vscode/mcp.json` — **condición:** arrancarlo con **MCP: List Servers** y conceder la confianza explícitamente `[Depende del plan/política]` | Modificar las pruebas existentes para que pasen (regla 1 de `AGENTS.md`) |
| Corregir BUG-01 con la suite en verde antes y después | `<PENDIENTE: decidir en la sesión — condiciones para el agent role Agent con auto-aprobación de herramientas>` | Añadir paquetes NuGet nuevos sin declararlo y justificarlo en la respuesta y en el PR |

## B5. Convenciones y dónde se declaran (repositorio real)

| Convención | Dónde se declara | Cómo se comprueba |
|---|---|---|
| Convenciones de C#: `sealed` por defecto, `decimal` para importes, `Math.Round(valor, 2, MidpointRounding.AwayFromZero)`, XML docs en español | `.github/instructions/csharp.instructions.md` · `applyTo: "**/*.cs"` | Generar un cálculo en Chat y verificar tipo y redondeo |
| Pruebas: `Metodo_Escenario_ResultadoEsperado`, Arrange/Act/Assert visible, un assert lógico, sin lógica condicional | `.github/instructions/tests.instructions.md` · `applyTo: "tests/**/*.cs"` | `dotnet test` y lectura del nombre de la prueba generada |
| Controllers: `[ApiController]`, `[Route("api/productos")]`, `ProblemDetails`, `[ProducesResponseType]`, `{id:int}` | `.github/instructions/api.instructions.md` · `applyTo: "src/Catalogo.Api/Controllers/**/*.cs"` | Inspección del controller y de `/swagger` |
| .NET 8 como único target, sufijo `Async`, inyección por constructor, prohibido `Console.WriteLine`, DTO como `record`, sin secretos en `appsettings.json` | `.github/copilot-instructions.md` (repo-wide) | Revisión del diff en el PR |
| No modificar pruebas existentes; reportar la salida real de `dotnet build` y `dotnet test`; plan previo si se tocan más de cinco archivos | `AGENTS.md` (raíz) | `git diff --stat tests/` |

Recordatorios activos: las custom instructions **no afectan a las inline suggestions**; no hay instrucciones personales porque **no se soportan en VS Code**; si se editan las instrucciones durante el laboratorio hay que llevarlas en la **head branch** para que Copilot code review las lea.

## B6. Criterios de arquitectura

Los del bloque A6, sin cambios, más: ubicación de los ADR `<PENDIENTE: decidir en la sesión — el repositorio del curso no tiene carpeta de ADR>` y quién los aprueba `<PENDIENTE: decidir en la sesión>`.

## B7. Flujo de trabajo

- Rama: `feat/<iniciales>-precio-descuento`.
- Puertas de calidad: `dotnet build CopilotLabCatalogo.sln --no-restore` sin warnings y `dotnet test CopilotLabCatalogo.sln` en verde.
- Plantilla de PR: `.github/pull_request_template.md`, con las secciones Resumen, Historia de usuario, Cambios, Pruebas, Riesgos, **Decisiones sobre sugerencias de Copilot** y Checklist.
- Copilot code review: **Reviewers** → **Copilot** → **Request**. Deja una revisión de tipo **"Comment"**: no aprueba, no bloquea el merge.
- Aprobación humana: 1 revisor del equipo.

## B8. Seguridad y secretos

- Local: `dotnet user-secrets set "LegacyPricing:ApiKey" "<valor>"`. Ejecución: variable de entorno `LegacyPricing__ApiKey`. El valor nunca se escribe en el log; si falta la clave, se emite un warning sin el valor (corrección de BUG-03).
- Content exclusion: **no se usa en este repositorio de práctica**, y aunque se usara no cubriría el agent role `Agent` ni Copilot CLI.
- Suggestions matching public code: **Blocked por defecto para Copilot Business**; en asientos asignados por la organización el ajuste se hereda y no se cambia en la cuenta personal.

## B9. Métricas acordadas para el equipo del curso

| Métrica | Fuente | Cadencia | Umbral |
|---|---|---|---|
| M-01 Cobertura de asientos activos | Copilot usage metrics dashboard (28 días) + `REST API endpoints for Copilot user management` | Mensual | 80 % |
| M-04 Trazabilidad de decisiones sobre sugerencias | Sección "Decisiones sobre sugerencias de Copilot" de `.github/pull_request_template.md` | Al cierre del curso | 100 % de los PRs |
| M-05 Proporción aceptado / modificado / descartado | Bitácoras individuales del curso | Al cierre del curso | Ninguna categoría por encima del 80 % |

## B10 y B11. Excepciones y gobierno

- Solicitud de excepción: por issue, con qué, por qué, riesgo, mitigación y vigencia máxima de 90 días.
- Gobierno: cadencia de revisión trimestral, con revisión extraordinaria ante cualquier renombrado de producto. En el historial de versiones, la fila 0.9 corresponde a este borrador de LAB-09.

---

## Rúbrica de evaluación de la guía

Calificación formativa: la rúbrica orienta la mejora, no clasifica a las personas.

| Criterio | Cumple | Cumple parcialmente | No cumple |
|---|---|---|---|
| Identificación y control | Versión, fecha, responsable con nombre y próxima revisión, los cuatro presentes | Falta la próxima revisión o el responsable es un área | Faltan dos o más campos |
| Alcance | Enumera repositorios y superficies, y declara lo que queda fuera | Enumera el alcance pero no lo excluido | Alcance genérico o ausente |
| Herramientas y versiones | Tabla completa, sin instrucción de instalar extensiones de Copilot, con Copilot CLI etiquetado por política | Tabla presente con alguna versión mínima ausente | Instruye a instalar `GitHub.copilot` o no hay versiones |
| Usos permitidos, condicionados y prohibidos | Al menos seis filas por columna y toda condición es comprobable | Filas suficientes pero alguna condición no es comprobable | Menos de seis filas por columna o condiciones ausentes |
| Convenciones, mecanismo y precedencia | Cada convención tiene ruta, `applyTo` cuando corresponde y comprobación observable; declara la precedencia oficial, que todas las instrucciones se entregan al modelo, y al menos tres límites verificados | Rutas correctas pero sin comprobación observable, o precedencia sin límites | Convenciones sin archivo que las declare, o precedencia incorrecta |
| Arquitectura y no delegación | Criterios verificables en el diff y regla explícita de registro en ADR | Criterios presentes sin regla de ADR | Criterios genéricos o delegación de decisiones |
| Flujo de trabajo | Diagrama con puertas de calidad y advertencia de la revisión "Comment" | Diagrama presente sin la advertencia | Sin flujo o trata la revisión de Copilot como aprobación |
| Seguridad y secretos | Mecanismo de secretos, procedimiento de fuga numerado y límites reales de content exclusion | Mecanismo presente sin procedimiento de fuga o sin límites | Presenta content exclusion como garantía o admite secretos en configuración |
| Métricas | Tres o más métricas con los siete campos, fuentes exactas y trampa declarada | Tres métricas con alguna fuente imprecisa o sin trampa | Menos de tres, o mide tasa de aceptación como productividad |
| Excepciones | Solicitante, formulario, aprobador, registro y caducidad | Falta la caducidad o el registro | Sin proceso de excepción |
| Gobierno | Responsable, cadencia, revisión extraordinaria por renombrado e historial | Cadencia sin revisión extraordinaria | Sin cadencia ni historial |
| Terminología y etiquetado | Terminología vigente y toda capacidad etiquetada | Uno o dos deslices de terminología o etiqueta | Terminología obsoleta reiterada |

## Lista de verificación final (12 puntos)

Antes de dar la guía por terminada, comprobar que:

1. La tabla de control tiene versión, fecha, responsable con nombre y fecha de próxima revisión.
2. El alcance nombra repositorios y superficies, y declara explícitamente lo que queda fuera.
3. Ninguna instrucción pide instalar la extensión de GitHub Copilot; se declara que **Copilot Chat es built-in desde VS Code 1.116** y `GitHub.vscode-pull-request-github` figura como instalación manual separada.
4. La tabla de usos tiene al menos seis filas por columna y cada uso condicionado lleva una condición comprobable.
5. Cada convención remite a un archivo real y, cuando aplica, a su `applyTo`; se advierte que sin `applyTo` las instrucciones no se aplican automáticamente.
6. Está escrita la precedencia oficial (personal > repositorio: path-specific > repo-wide > agent > organización) y la aclaración de que **todas** las instrucciones se entregan al modelo.
7. Están las cuatro advertencias de alcance: las custom instructions no afectan a las inline suggestions; las instrucciones personales no se soportan en VS Code; code review lee la **head branch**; `AGENTS.md` no está documentado como soportado en Visual Studio.
8. Los criterios de arquitectura son verificables en el diff y consta que las decisiones de arquitectura se registran en un ADR y no se delegan.
9. El flujo de trabajo incluye el diagrama, las puertas `dotnet build` y `dotnet test`, y la advertencia de que Copilot code review deja siempre una revisión de tipo **"Comment"** que no bloquea el merge.
10. La sección de seguridad indica el mecanismo de secretos, el procedimiento de fuga numerado y los límites reales de content exclusion, incluida la propagación de hasta 30 minutos y su inaplicabilidad en agent mode y en Copilot CLI.
11. Hay al menos tres métricas con los siete campos completos, con la fuente exacta, y consta que la tasa de aceptación de sugerencias no mide productividad ni calidad.
12. Existen el proceso de excepción con caducidad y el gobierno del documento con cadencia trimestral y revisión extraordinaria ante renombrados de producto; toda capacidad citada lleva `[GA]`, `[Preview]` o `[Depende del plan/política]`.
