# Glosario español/inglés del curso — GitHub Copilot para Desarrollo de Software

**Total de términos definidos: 134.**

Fuente de verdad técnica: `06_recursos/ficha_tecnica_copilot_verificada.md` (verificación: 14 de agosto de 2026). Especificación vinculante: `00_control/BRIEF_PRODUCCION.md`.

Convención de etiquetas: `[GA]` generalmente disponible · `[Preview]` vista previa pública o experimental · `[Depende del plan/política]` sujeto al plan de Copilot o a una política de organización o empresa. Cuando un dato no está verificado se indica expresamente.

Módulos: **M1** Instalación y configuración · **M2** Prompting y comprensión · **M3** Desarrollo, refactorización y depuración · **M4** Pruebas, seguridad y PR · **M5** Personalización y gobierno.

## a) Superficies y modos de trabajo

| Término (español) | Equivalente en inglés | Definición | Módulo |
|---|---|---|---|
| Superficie de Copilot | Copilot surface | Cada punto donde Copilot opera: sugerencias en el editor, chat, `Copilot CLI`, GitHub.com y el agente en la nube. Cada superficie respeta un subconjunto distinto de personalizaciones y políticas. `[GA]` | M1, M5 |
| Session Target | Session Target | Control de la vista de chat de VS Code con el que se elige el *harness* y dónde se ejecuta la sesión de agente. Sustituye al antiguo selector de tres modos Ask/Edit/Agent. `[GA]` | M1, M3 |
| Entorno de ejecución del agente | agent harness | Infraestructura que ejecuta al agente (proceso local, servicio en la nube o motor de un tercero). Se selecciona con el control `Session Target`. `[GA]` | M1, M5 |
| Harnesses disponibles | available harnesses | Los cinco valores documentados del `Session Target`: `Local`, `Copilot`, `Claude` `[Preview]`, `Codex` y `Cloud`. `[GA]` salvo `Claude` | M1, M5 |
| Rol de agente | agent role | Persona o comportamiento del agente dentro de una sesión, elegido en el desplegable de agentes. En sesiones locales solo hay dos roles integrados: `Agent` y `Plan`. `[GA]` | M1, M3 |
| Rol `Agent` | `Agent` role | Rol integrado que planifica y ejecuta tareas de forma autónoma: edita archivos, ejecuta comandos e itera sobre los resultados. `[GA]` | M3 |
| Rol `Plan` | `Plan` role | Rol integrado que investiga una tarea y produce un plan de implementación estructurado **sin modificar código**. También se invoca con el slash command `/plan`. `[GA]` | M3 |
| Agent Host | Agent Host | Ejecución de agentes en VS Code independiente del *extension host*. Lee instrucciones de usuario desde carpetas agnósticas (`~/.copilot/instructions`, `~/.claude/rules`) y **no usa prompt files**: para reutilizar un prompt hay que convertirlo en agent skill. `[GA]` | M5 |
| Vista de chat | Chat view | Panel principal de conversación con Copilot en VS Code. Se abre con `Ctrl+Alt+I` (macOS `⌃⌘I`). `[GA]` | M1 |
| Chat en línea | inline chat | Conversación abierta dentro del editor o de la terminal sobre el punto exacto del cursor, con `Ctrl+I` (macOS `⌘I`). `[GA]` | M1, M3 |
| Quick Chat | Quick Chat | Ventana emergente de consulta rápida sin abrir el panel de chat, con `Ctrl+Shift+Alt+L` (macOS `⇧⌥⌘L`). `[GA]` | M1 |
| Copilot CLI | GitHub Copilot CLI | Agente de terminal cuyo binario es `copilot`. Disponible en todos los planes, pero si la licencia la otorga una organización debe estar habilitada la política **Copilot CLI**. En Windows requiere PowerShell 6 o superior. `[GA]` `[Depende del plan/política]` | M5 |
| Copilot cloud agent | Copilot cloud agent | Agente asíncrono que ejecuta tareas en la infraestructura de GitHub y siempre abre exactamente un Pull Request por tarea, en un repositorio y una rama, con un tope de 59 minutos por sesión. Antes se llamaba *Copilot coding agent*. `[GA]` `[Depende del plan/política: deshabilitado por defecto en Business y Enterprise]` | M5 |
| Copilot Spaces | Copilot Spaces | Colecciones de contexto curado que sustituyen a las antiguas *knowledge bases*. Están disponibles en cualquier licencia de Copilot, incluida Free; **ya no son un diferenciador de Enterprise**. `[GA]` | M5 |
| Copilot Memory | Copilot Memory | Capacidad del cloud agent para retener contexto entre sesiones. Se controla con una política homónima. `[Preview]` `[Depende del plan/política]` | M5 |
| Subagente | subagent | Custom agent invocado por otro agente mediante el campo `agents` de su frontmatter, que requiere incluir el tool `agent`. Soportado en VS Code y `Copilot CLI`; no en GitHub.com ni Visual Studio. `[GA]` | M5 |
| Extensión integrada | built-in extension | Modalidad de distribución de `GitHub.copilot-chat`, incluida de fábrica en VS Code desde la versión 1.116 (15 de abril de 2026): no se instala nada, solo se inicia sesión. `[GA]` | M1 |

## b) Sugerencias en el editor

| Término (español) | Equivalente en inglés | Definición | Módulo |
|---|---|---|---|
| Sugerencia en línea | inline suggestion | Completado de código que Copilot propone mientras se escribe. Se acepta con `Tab` y se descarta con `Esc`. **Las custom instructions no la afectan.** `[GA]` | M1, M2 |
| Texto fantasma | ghost text | Representación visual atenuada de la sugerencia en línea antes de aceptarla. `[GA]` | M1 |
| Sugerencia de edición siguiente | next edit suggestion (NES) | Propuesta de la siguiente edición coherente en otro punto del archivo, señalada con una flecha en el *gutter*. Se navega y se acepta con `Tab`. Setting: `github.copilot.nextEditSuggestions.enabled`. `[GA]` | M1, M3 |
| Aceptación parcial | partial acceptance | Aceptar solo la siguiente palabra o la siguiente línea de una sugerencia con `Ctrl+Right` (macOS `⌘→`). `[GA]` | M1 |
| Modelo de completions | completions model | Modelo que genera las sugerencias en línea, **distinto** del modelo de chat. Se cambia en el menú Chat con **Configure Inline Suggestions...** > **Change Completions Model...**. `[GA]` | M1 |
| Aplazamiento de sugerencias | Snooze Inline Suggestions | Comando que silencia temporalmente el autocompletado; se revierte con **Cancel Snooze Inline Suggestions**. `[GA]` | M1 |

## c) Contexto y prompting

| Término (español) | Equivalente en inglés | Definición | Módulo |
|---|---|---|---|
| Prompt técnico | technical prompt | Instrucción estructurada en cinco componentes (rol, objetivo, contexto, restricciones y formato de salida) frente al prompt vago. Es el eje del Módulo 2. | M2 |
| Refinamiento iterativo | iterative refinement | Técnica de mejorar la respuesta mediante turnos sucesivos que corrigen, acotan o añaden contexto, en lugar de reescribir el prompt desde cero. | M2 |
| Slash command | slash command | Comando integrado o dinámico que se invoca escribiendo `/` en el chat, por ejemplo `/explain`, `/tests`, `/fix`, `/plan` o `/init`. Los prompt files y los agent skills aparecen también como slash commands propios. `[GA]` | M2, M5 |
| Participante de chat | chat participant | Asistente especializado que se invoca con `@`. En VS Code están integrados `@github`, `@terminal` y `@vscode`. **Siguen soportados y no están deprecados.** `[GA]` | M2 |
| Variable de contexto | context variable | Referencia escrita con `#` que adjunta un tipo de contexto concreto a la petición. La documentación publica dos formas vigentes: plana y *namespaced*. `[GA]` | M2 |
| `#codebase` | `#codebase` / `#search/codebase` | Variable que permite a Copilot buscar en el espacio de trabajo el contexto relevante. Reemplaza en VS Code la función del antiguo `@workspace`. `[GA]` | M2, M3 |
| `#selection` | `#selection` | Variable que adjunta el fragmento de código seleccionado en el editor. Conserva la forma plana, sin namespace. `[GA]` | M2, M3 |
| `#problems` | `#problems` / `#read/problems` | Variable que adjunta los diagnósticos del panel **Problems** (errores y advertencias del compilador o del analizador). `[GA]` | M2, M3 |
| `#changes` | `#changes` / `#search/changes` | Variable que adjunta los cambios pendientes del control de código fuente. Útil para redactar el resumen de un Pull Request. `[GA]` | M2, M4 |
| Forma con espacio de nombres | namespaced form | Sintaxis de variable de contexto con prefijo de conjunto de herramientas, por ejemplo `#search/codebase` o `#web/fetch`. Coexiste oficialmente con la forma plana; la documentación es internamente inconsistente. `[GA]` | M2, M5 |
| Conjunto de herramientas | tool set | Agrupación de herramientas bajo un espacio de nombres: `#agent`, `#browser`, `#edit`, `#execute`, `#read`, `#search`, `#web`, `#vscode`. Dentro de un archivo de personalización se referencian como `#tool:<nombre>`. `[GA]` | M2, M5 |
| Añadir contexto | Add Context | Acción de la vista de chat para adjuntar archivos, carpetas, símbolos, imágenes, URL o salida del navegador. También admite arrastrar y soltar. `[GA]` | M2 |
| Ventana de contexto | context window | Cantidad de tokens que el modelo puede considerar. La ventana de 1 millón de tokens solo está disponible en **VS Code y Copilot CLI**. `[GA]` | M2, M5 |

## d) Personalización y customizaciones

| Término (español) | Equivalente en inglés | Definición | Módulo |
|---|---|---|---|
| Instrucciones personalizadas | custom instructions | Archivos Markdown que fijan convenciones, arquitectura y estilo para que Copilot los aplique en cada petición. **No afectan a las sugerencias en línea.** `[GA]` | M2, M5 |
| Instrucciones del repositorio | repository-wide instructions | Archivo `.github/copilot-instructions.md`, siempre activo para todo el repositorio (*always-on*). `[GA]` | M5 |
| Instrucciones específicas de ruta | path-specific instructions | Instrucciones que solo se aplican a los archivos que coinciden con un glob, ubicadas en `.github/instructions/**/*.instructions.md` (búsqueda recursiva). `[GA]` | M5 |
| `.instructions.md` | `.instructions.md` | Extensión de los archivos de instrucciones condicionales. Frontmatter admitido: `name`, `description`, `applyTo` y `excludeAgent` (este último solo en GitHub Docs). `[GA]` | M5 |
| `applyTo` | `applyTo` | Campo de frontmatter con el glob, relativo a la raíz del workspace, que determina a qué archivos se aplican las instrucciones; `**` significa todos. **Si se omite, las instrucciones no se aplican automáticamente.** `[GA]` | M5 |
| `AGENTS.md` | `AGENTS.md` | Archivo de instrucciones para agentes, portable entre herramientas. Puede estar en cualquier directorio del repositorio y **gana el más cercano en el árbol**. Su soporte en Visual Studio no está documentado. `[GA]` | M5 |
| Instrucciones personales | personal instructions | Instrucciones de usuario, de máxima precedencia. **No se soportan en VS Code**: solo en Copilot Chat de GitHub.com, JetBrains y `Copilot CLI`. `[GA]` | M5 |
| Instrucciones de organización | organization custom instructions | Instrucciones definidas en la UI de settings de la organización. Solo se aplican en GitHub.com (Chat, code review y cloud agent). `[Depende del plan/política]` | M5 |
| Precedencia de instrucciones | instruction priority | Orden de prioridad: **personal > repositorio (específicas de ruta > repo-wide > agente) > organización**. Sin embargo, **todos los conjuntos relevantes se entregan al modelo**, no solo el de mayor prioridad. `[GA]` | M5 |
| Prompt file | prompt file | Archivo Markdown que encapsula un prompt reutilizable y se invoca como slash command. Se aloja en `.github/prompts`. `[Preview]` según GitHub Docs; la documentación de VS Code no le asigna etiqueta | M2, M5 |
| `.prompt.md` | `.prompt.md` | Extensión de los prompt files. Frontmatter: `description`, `name`, `argument-hint`, `agent`, `model` y `tools`. `[Preview]` | M2, M5 |
| Frontmatter `agent:` | `agent:` frontmatter key | Clave que indica con qué agente se ejecuta el prompt file. Valores válidos: `ask`, `agent`, `plan` o el nombre de un custom agent. Es el único lugar donde el identificador `ask` sigue documentado. `[GA]` | M5 |
| Custom agent | custom agent | Agente propio definido en un archivo Markdown, con su descripción, herramientas, modelo y traspasos. Antes se llamaba *chat mode*. Se aloja en `.github/agents`. `[GA]` | M5 |
| `.agent.md` | `.agent.md` | Extensión de los custom agents, que sustituye a `.chatmode.md`. Frontmatter: `description`, `name`, `tools`, `agents`, `model`, `user-invocable`, `disable-model-invocation`, `target`, `handoffs` y `hooks` `[Preview]`. `[GA]` | M5 |
| Agent skill | agent skill | Habilidad empaquetada como directorio, según un estándar abierto. Su ventaja decisiva es que **funciona en VS Code, Copilot CLI y Copilot cloud agent**, mientras que las custom instructions se limitan a VS Code y GitHub.com. `[GA]` | M5 |
| `SKILL.md` | `SKILL.md` | Archivo obligatorio en la raíz del directorio de un agent skill. `name` y `description` son requeridos; `name` debe coincidir con el nombre del directorio padre o la carga falla en silencio. `[GA]` | M5 |
| Agent plugin | agent plugin | Paquete instalable, conforme al estándar abierto Agent Plugins 1.0, que agrupa **agent skills y servidores MCP**; solo esos dos componentes son portables entre clientes. Se declara con un `plugin.json` en la raíz del directorio. `[GA]` | M5 |
| Hook | hook | Script que se dispara en un punto del ciclo de vida del agente, declarado en `.github/hooks/*.json`. `[Preview]` en VS Code; soportado en GitHub.com y `Copilot CLI` | M5 |

## e) Agentes y automatización

| Término (español) | Equivalente en inglés | Definición | Módulo |
|---|---|---|---|
| MCP | Model Context Protocol | Protocolo abierto que permite a Copilot consumir herramientas, recursos y prompts expuestos por servidores externos. `[GA]` `[Depende del plan/política: la política "MCP servers in Copilot" está deshabilitada por defecto para Business y Enterprise]` | M5 |
| Servidor MCP | MCP server | Proceso local (`stdio`) o servicio remoto (`http`/`sse`) que publica herramientas para el agente. Antes de arrancar hay que **confiar** explícitamente en él. `[GA]` | M5 |
| `.vscode/mcp.json` | `.vscode/mcp.json` | Archivo de configuración de servidores MCP del espacio de trabajo, versionable para compartirlo con el equipo. Secciones: `servers`, `inputs` y `sandbox`. El Agent Host **no lo lee**: usa `.mcp.json` o `~/.copilot/mcp-config.json`. `[GA]` | M5 |
| `copilot-setup-steps.yml` | `copilot-setup-steps.yml` | Workflow en `.github/workflows/` que prepara el entorno del cloud agent y de la revisión de código; es donde se instala el SDK de .NET 8. Si existe `copilot-code-review.yml`, este prevalece para la revisión. `[GA]` | M4, M5 |
| Asignación de una incidencia a Copilot | assigning an issue to Copilot | Punto de entrada del cloud agent: al asignar el issue a **Copilot** siempre se crea un Pull Request. Copilot recibe el título, la descripción y los comentarios existentes; **no ve los comentarios posteriores a la asignación**. `[GA]` | M5 |
| Auto-aprobación de herramientas | auto-approve / `/yolo` | Modo en que el agente ejecuta herramientas sin confirmación previa (`chat.tools.global.autoApprove`). Riesgo alto: no debe usarse en repositorios corporativos sin acotarlo. `[GA]` | M5 |
| Sesión de agente | agent session | Unidad de trabajo con un `Session Target`, un rol, un modelo y un historial propios. Se puede compactar (`/compact`) o reiniciar (`/clear`). `[GA]` | M3, M5 |

## f) Pull Requests y revisión de código

| Término (español) | Equivalente en inglés | Definición | Módulo |
|---|---|---|---|
| Solicitud de incorporación | Pull Request (PR) | Propuesta de fusionar los commits de una rama en otra, con descripción, revisión y comprobaciones automáticas. Es el entregable central del Módulo 4. | M4 |
| Rama de origen | head branch | Rama que contiene los cambios propuestos. En Copilot code review, **las instrucciones y skills se leen de la head branch**, no de la base. | M4 |
| Rama de destino | base branch | Rama sobre la que se integrarán los cambios del Pull Request. | M4 |
| Copilot code review | Copilot code review | Revisión automatizada de un Pull Request o de los cambios locales. Se personaliza con custom instructions y **ya no requiere Copilot Enterprise**. `[Depende del plan: disponible en todos los planes de pago]` | M4 |
| Revisión de tipo "Comment" | "Comment" review | Naturaleza de la revisión de Copilot: **siempre** deja una revisión de tipo `Comment`, nunca `Approve` ni `Request changes`. Por tanto no bloquea el *merge* ni cuenta para las aprobaciones requeridas. `[GA]` | M4 |
| Conjunto de reglas | ruleset | Configuración de repositorio u organización que impone reglas sobre ramas; entre ellas **Automatically request Copilot code review**, **Review new pushes** y **Review draft pull requests**. `[GA]` | M4, M5 |
| Actor con excepción | bypass actor | Identidad autorizada a saltarse un ruleset o una protección de rama. Si una regla incompatible bloquea al cloud agent, la solución documentada es añadir a Copilot como bypass actor. `[GA]` | M4, M5 |
| Nivel de esfuerzo de revisión | review effort level | Ajuste `Lite` (predeterminado) o `Balanced` que regula la profundidad y el coste en AI credits de la revisión. El valor del repositorio sobrescribe el de la organización. `[Depende del plan/política]` | M4, M5 |
| Plantilla de Pull Request | pull request template | Archivo `.github/pull_request_template.md` que precarga la estructura del PR revisable: qué cambia, por qué, cómo se probó y qué riesgos tiene. | M4 |
| Rama de trabajo | feature branch | Rama individual del participante, con la convención `feat/<iniciales>-precio-descuento`. Aísla el trabajo y hace revisable el resultado. | M3, M4 |

## g) Gobierno, privacidad y seguridad

| Término (español) | Equivalente en inglés | Definición | Módulo |
|---|---|---|---|
| Exclusión de contenido | content exclusion | Política que impide que archivos o rutas concretas informen a Copilot. **No se aplica en agent mode ni en Copilot CLI**, ni a symlinks ni a repositorios en sistemas de archivos remotos; la propagación tarda hasta 30 minutos. `[GA]` en IDEs · `[Preview]` en GitHub.com y Mobile · `[Depende del plan/política: Business o Enterprise]` | M1, M5 |
| Información semántica de un archivo excluido | semantic information | Fuga documentada de la exclusión de contenido: Copilot puede usar indirectamente datos que el IDE aporta sobre un archivo excluido (tipos, definiciones al pasar el cursor, propiedades de compilación). Excluir un archivo **no garantiza** ocultarlo por completo. `[GA]` | M1, M5 |
| Referenciación de código | code referencing | Comprobación de cada sugerencia aceptada, junto a unos 150 caracteres de contexto, contra un índice de todos los repositorios públicos de GitHub.com. Solo se comprueban las sugerencias **aceptadas y no alteradas**; los emparejamientos ocurren en menos del uno por ciento de los casos. `[GA]` | M1, M5 |
| Sugerencias que coinciden con código público | suggestions matching public code | Ajuste de política con valores `Allow` o `Block`. Está **bloqueado por defecto para usuarios de Copilot Business**, y quien recibe el asiento de una organización hereda el valor y no puede cambiarlo. `[Depende del plan/política]` | M1, M5 |
| Retención cero de datos | zero data retention | Acuerdo por el cual el proveedor del modelo no conserva las peticiones ni las respuestas. GitHub lo mantiene con OpenAI. Excepción documentada: **Claude Fable 5**, cuyo proveedor sí retiene datos para clasificadores de seguridad. `[GA]` | M1, M5 |
| Exclusión del entrenamiento | training opt-out | Ajuste personal **"Allow GitHub to use my data for AI model training"**. Desde el 24 de abril de 2026, los planes Free, Pro, Pro+ y Max se usan para entrenamiento salvo exclusión expresa; Business y Enterprise **nunca** se usan y por eso el ajuste no aparece. `[GA]` `[Depende del plan]` | M1, M5 |
| `managed-settings.json` | `managed-settings.json` | Archivo de configuración gestionada por la empresa (en Windows, `%ProgramFiles%\GitHubCopilot\`) con claves como `allowedMcpServers`, `deniedMcpServers`, `enabledPlugins` y `strictKnownMarketplaces`. Una entrada de denegación prevalece sobre una de permiso. `[Depende del plan/política]` | M5 |
| Política de organización o empresa | organization / enterprise policy | Interruptor administrativo que habilita o restringe una capacidad. En conflictos entre organizaciones suele ganar la menos restrictiva, salvo en Metrics API, indexación semántica, sugerencias que coinciden con código público y revisión sin licencia. `[Depende del plan/política]` | M5 |
| Métricas de uso de Copilot | Copilot usage metrics | Conjunto de cinco canales de entrega de datos de adopción: APIs, panel de uso, panel de generación de código, panel de impacto y exportación NDJSON. Requiere que la política homónima esté habilitada. Latencia oficial: entre dos y tres días UTC. `[Depende del plan/política]` | M5 |
| API de métricas | Copilot Metrics API | Endpoints REST de informes de uso por empresa y por organización, con el encabezado `X-GitHub-Api-Version: 2026-03-10`. **No incluye datos de licencias ni de asientos**, que viven en la API de gestión de usuarios. `[Depende del plan/política]` | M5 |
| Cohorte de adopción | adoption cohort | Clasificación del panel de impacto: **Passive users** (`No Cohort`), **Phase 1: Code first**, **Phase 2: Agent first** y **Phase 3: Multi-agent**. Umbral de actividad: al menos dos días activos en la ventana móvil de 28 días. `[GA]` | M5 |
| Multiplicador de adopción | adoption multiplier | Métrica del panel de impacto: promedio de Pull Requests fusionados por los usuarios comprometidos (fases 1, 2 y 3) dividido entre el de los usuarios pasivos. `[GA]` | M5 |
| Indexación semántica | semantic indexing | Indexación de repositorios que no están en GitHub para mejorar la búsqueda de contexto. Es una política independiente y, en conflicto entre organizaciones, gana la configuración más restrictiva. `[Depende del plan/política]` | M5 |
| Inyección de prompt | prompt injection | Ataque en el que contenido no confiable leído por el agente (un issue, una página web, la salida de una herramienta MCP) contiene instrucciones que el modelo puede obedecer. Es el riesgo principal al habilitar MCP y agentes autónomos. | M4, M5 |
| Alucinación | hallucination | Afirmación falsa formulada por el modelo con apariencia de certeza: APIs inexistentes, firmas inventadas o justificaciones incorrectas. Obliga a verificar toda salida contra el código y las pruebas. | M2, M4 |
| Secreto embebido en código | hardcoded secret | Credencial escrita literalmente en el código o en la configuración. En el repositorio de práctica es BUG-03: `sk-FAKE-DEMO-NOT-A-REAL-SECRET-0000`, un valor evidentemente falso que además se escribe en el log. | M4 |

## h) Modelos y créditos

| Término (español) | Equivalente en inglés | Definición | Módulo |
|---|---|---|---|
| Créditos de IA | AI credits | Unidad de consumo mensual por usuario. Copilot Business incluye 1.900 y Copilot Enterprise 3.900. Es uno de los tres diferenciadores reales entre ambos planes. `[GA]` `[Depende del plan]` | M1, M5 |
| Selector de modelo | model picker | Control del campo de entrada del chat para cambiar el modelo de la conversación, con atajo `Ctrl+Alt+.` (macOS `⌥⌘.`). En Restricted Mode solo muestra `Auto`. `[GA]` | M1, M3 |
| Esfuerzo de razonamiento | thinking effort | Submenú del selector de modelo, disponible en modelos de razonamiento, que regula cuánto delibera el modelo. La etiqueta pasa a mostrar el nivel, por ejemplo `Claude Sonnet 4.6 · High`. Configurable en VS Code, Copilot CLI y Copilot cloud agent. `[GA]` | M3, M5 |
| Selección automática de modelo | auto model selection | Opción `Auto` del selector: Copilot elige el modelo por petición dentro de un subconjunto documentado. Al pasar el cursor sobre la respuesta se ve cuál se utilizó. `[GA]` | M1, M3 |
| Modelo de utilidad | utility model | Modelo empleado internamente por el producto y **no seleccionable** en el selector, por ejemplo GPT-4o mini o GPT-4.1. `[GA]` | M5 |
| Modelo de capacidad extendida | extended-capability model | Modelo con ventana de contexto de 1 millón de tokens y/o niveles de razonamiento configurables. `[GA]` | M5 |
| Asiento de licencia | seat | Licencia de Copilot asignada a una persona. Su verificación es el primer criterio de aceptación del LAB-01, y los datos de asientos viven en la API de gestión de usuarios, no en la de métricas. `[Depende del plan]` | M1, M5 |

## i) Ingeniería de software y .NET

| Término (español) | Equivalente en inglés | Definición | Módulo |
|---|---|---|---|
| Objeto de transferencia de datos | DTO (Data Transfer Object) | Tipo cuyo único fin es transportar datos entre capas o a través de la API, desacoplando el contrato público de la entidad de dominio. | M3, M4 |
| `ProblemDetails` | `ProblemDetails` | Formato estándar de respuesta de error de ASP.NET Core (RFC 7807), con `type`, `title`, `status` y `detail`. Es lo que deben devolver los errores 400, 404 y 500 de la API del laboratorio. | M3, M4 |
| DataAnnotations | DataAnnotations | Atributos de validación declarativa (`[Required]`, `[Range]`, `[StringLength]`) que, combinados con `Validator.TryValidateObject`, corrigen BUG-02. | M4 |
| `ValidationException` | `ValidationException` | Excepción que se lanza cuando la validación de un DTO falla; el middleware de errores la traduce a una respuesta 400 con `ProblemDetails`. | M4 |
| Middleware | middleware | Componente de la canalización HTTP de ASP.NET Core que intercepta cada petición. `ManejoErroresMiddleware` (solo en `resuelto/`) traduce excepciones a 400, 501 y 500. | M4 |
| Inyección de dependencias | dependency injection | Patrón por el cual las dependencias se registran en el contenedor de `Program.cs` y se reciben por constructor, en lugar de instanciarse dentro de la clase. Es lo que hace comprobables a `ProductoService` y `DescuentoCalculator`. | M3 |
| `CancellationToken` | `CancellationToken` | Parámetro de cooperación en la cancelación de operaciones asíncronas. Todas las firmas asíncronas del repositorio lo declaran con valor predeterminado `default`. | M3 |
| `record` inmutable | immutable record | Tipo de referencia con igualdad por valor y propiedades `init`, usado en el repositorio para todos los DTO. Comunica que el objeto no debe mutar tras construirse. | M3 |
| `MidpointRounding.AwayFromZero` | `MidpointRounding.AwayFromZero` | Modo de redondeo que aleja del cero los valores intermedios (2,345 a 2,35). Es el modo obligatorio del cálculo de precios; el valor por omisión de .NET es distinto (`ToEven`) y produce resultados diferentes. | M3 |
| Swagger / OpenAPI | Swagger / OpenAPI | Especificación del contrato de la API y su interfaz de exploración. Se enriquece con documentación XML y atributos `[ProducesResponseType]`. | M2, M3 |
| Documentación XML | XML documentation comments | Comentarios `///` con `<summary>`, `<param>` y `<returns>`, que alimentan IntelliSense y OpenAPI. Su generación es TODO-03 y requiere `GenerateDocumentationFile`. | M2 |
| xUnit | xUnit | Marco de pruebas del proyecto `tests/Catalogo.Api.Tests`. Sus atributos principales son `[Fact]`, `[Theory]` e `[InlineData]`. | M4 |
| `[Fact]` | `[Fact]` | Atributo que marca una prueba sin parámetros: un único escenario fijo. | M4 |
| `[Theory]` | `[Theory]` | Atributo que marca una prueba parametrizada, que se ejecuta una vez por cada juego de datos suministrado. | M4 |
| `[InlineData]` | `[InlineData]` | Atributo que suministra un juego concreto de argumentos a un `[Theory]`. Es la forma natural de cubrir los umbrales 9/10, 49/50 y 99/100. | M4 |
| `[Fact(Skip = ...)]` | `[Fact(Skip = ...)]` | Prueba deliberadamente omitida. En `inicial/` hay dos: una expone BUG-01 y otra cubre TODO-01. Se habilitan quitando el argumento `Skip`. | M4 |
| Simulacro | mock | Doble de prueba que sustituye una dependencia y permite verificar interacciones. Debe distinguirse del *stub*, que solo devuelve valores fijos. | M4 |
| Cobertura | code coverage | Porcentaje de código ejecutado por las pruebas. Es un indicador necesario pero insuficiente: una cobertura alta con aserciones débiles no demuestra corrección. | M4 |
| Caso borde | edge case | Entrada situada en el límite de una regla (cantidad 9 frente a 10, precio cero, tope del 20 %). Es donde se concentran los defectos y donde las pruebas generadas suelen fallar. | M4 |
| Refactorización | refactoring | Cambio de la estructura interna del código **sin alterar su comportamiento observable**. Su condición previa es tener pruebas que lo demuestren. | M3 |
| Causa raíz | root cause | Origen real de un defecto, distinto del síntoma. El método de depuración del curso va de la observación a la hipótesis, la prueba que la confirma y la corrección. | M3 |
| Repositorio en memoria | in-memory repository | Implementación `InMemoryProductoRepository` que guarda los productos en una colección del proceso. Evita depender de una base de datos durante el curso. | M1, M3 |
| Bitácora de decisiones | decision log | Registro que el participante mantiene con cada sugerencia relevante de Copilot, la decisión tomada y su justificación. Es evidencia entregable en varios laboratorios. | M2, M3, M4, M5 |
| Aceptado / modificado / descartado | accepted / modified / discarded | Las tres clasificaciones obligatorias de la bitácora. Obligan a explicitar el juicio humano sobre la salida de la IA y sustentan el RA-5.5. | M2, M3, M4, M5 |

## j) Dominio del repositorio de práctica

| Término (español) | Equivalente en inglés | Definición | Módulo |
|---|---|---|---|
| `copilot-lab-catalogo` | `copilot-lab-catalogo` | Nombre del repositorio de práctica del curso, en `05_codigo_laboratorios/inicial/` y `.../resuelto/`. Es una Web API de ASP.NET Core sobre .NET 8 con pruebas xUnit. | M1 |
| `Producto` | `Producto` | Entidad de dominio mutable administrada por el repositorio, con `Id`, `Nombre`, `Categoria`, `PrecioBase`, `Existencias` y `Activo`. | M3 |
| `Categoria` | `Categoria` | Enumeración de clasificación del catálogo con cuatro valores: `Electronica`, `Hogar`, `Oficina` y `Consumible`. Los identificadores van sin acentos por decisión de codificación del repositorio. | M3 |
| `DescuentoCalculator` | `DescuentoCalculator` | Implementación de las reglas de descuento. Contiene BUG-01: los umbrales usan `>` en lugar de `>=` y el tope del 20 % no se aplica. Es el objetivo del LAB-06. | M3 |
| `IDescuentoCalculator.CalcularPrecioFinal` | `IDescuentoCalculator.CalcularPrecioFinal` | Firma `decimal CalcularPrecioFinal(decimal precioBase, int cantidad, bool esClientePreferente)`. Devuelve el **precio unitario** final, no el importe total. | M3 |
| `ProductoService` | `ProductoService` | Servicio de aplicación que orquesta repositorio y calculadora de descuentos. Contiene TODO-01 (`ActualizarPrecioAsync` lanza `NotImplementedException`) y TODO-02 (búsqueda que devuelve 500). | M3 |
| `IProductoRepository` | `IProductoRepository` | Contrato de persistencia con `ObtenerTodosAsync`, `ObtenerPorIdAsync`, `AgregarAsync`, `ActualizarAsync` y `EliminarAsync`, todos asíncronos y con `CancellationToken`. | M3 |
| `CrearProductoRequest` | `CrearProductoRequest` | DTO de entrada de `POST /api/productos` con `Nombre`, `Categoria`, `PrecioBase` y `Existencias`. Sin validación es BUG-02: acepta nombre vacío y precio negativo y devuelve 201. | M4 |
| `ActualizarPrecioRequest` | `ActualizarPrecioRequest` | DTO de entrada de `PATCH /api/productos/{id}/precio` con `PrecioBase`, `Cantidad` y `EsClientePreferente`. El endpoint que lo consume es la HU-01. | M3 |
| `BusquedaProductosQuery` | `BusquedaProductosQuery` | DTO de consulta con `Categoria`, `PrecioMinimo`, `PrecioMaximo`, `Texto`, `Pagina` y `TamanoPagina` (predeterminado 20, máximo 100). | M3 |
| `ProductoResponse` | `ProductoResponse` | `record` de salida de la API con `Id`, `Nombre`, `Categoria`, `PrecioBase`, `Existencias` y `Activo`. Separa el contrato público de la entidad `Producto`. | M3 |
| `LegacyPricingClient` | `LegacyPricingClient` | Cliente simulado de un servicio de precios legado. Alberga BUG-03: la clave de API está embebida, aparece en `appsettings.json` y además se escribe en el log. | M4 |
| Descuento por volumen | volume discount | Regla escalonada por cantidad: `>= 10` aplica 5 %, `>= 50` aplica 10 % y `>= 100` aplica 15 %. | M3 |
| Cliente preferente | preferred customer | Condición comercial que añade un 8 % de descuento, que **se suma** al de volumen en lugar de componerse con él. | M3 |
| Tope del 20 % | 20 % discount cap | Límite máximo del descuento total tras sumar volumen y cliente preferente. No aplicarlo es la segunda mitad de BUG-01. | M3 |
| Semilla | seed data | Los 12 productos precargados en el repositorio en memoria, repartidos entre las cuatro categorías; el producto 10 tiene `Activo = false`. | M1, M3 |

## Términos cuyo nombre cambió recientemente

Entre 2025 y agosto de 2026 se produjeron dos renombrados masivos que invalidan buena parte del material de formación anterior. La columna **Grado de confirmación** distingue lo declarado oficialmente de lo inferido.

| Nombre anterior | Nombre vigente | Qué pasó exactamente | Grado de confirmación |
|---|---|---|---|
| "modos Ask / Edit / Agent" | **Session Target** (harness) + **agent role** (`Agent` / `Plan`) | El selector de tres modos dejó de existir en VS Code. Hoy hay dos ejes independientes: dónde corre la sesión (`Session Target`) y con qué rol (`Agent` o `Plan`). Las cadenas "Ask mode" y "Edit mode" no aparecen en ninguna página actual de VS Code. El identificador `ask` **sobrevive** como valor del frontmatter `agent:` de los prompt files. | **Confirmado solo por ausencia.** No existe ninguna declaración oficial de migración ni frase que nombre los modos retirados. |
| "Copilot coding agent" | **Copilot cloud agent** | Renombrado de producto anunciado en github.blog el 1 de abril de 2026: *"Copilot cloud agent (formerly known as Copilot coding agent)"*. El H1 de la documentación ya dice "About GitHub Copilot cloud agent" y las URL con `coding-agent` sobreviven como redirecciones. | **Confirmado explícitamente** por el changelog y por el título de la documentación. |
| `.chatmode.md` / "chat mode" | `.agent.md` / **custom agent** | La documentación declara literalmente que los custom agents se llamaban antes custom chat modes, que la funcionalidad no cambia y que los archivos `.chatmode.md` existentes deben **renombrarse** a `.agent.md`. La carpeta pasa a ser `.github/agents/`. | **Declarado explícitamente** por la documentación. No se documenta si los `.chatmode.md` antiguos siguen cargándose por retrocompatibilidad. |
| "coding guidelines" de code review | **custom instructions** | Las coding guidelines, que eran exclusivas de Copilot Enterprise, fueron eliminadas. Ambas URL antiguas redirigen hoy a páginas de custom instructions y la expresión no aparece en el destino. Consecuencia práctica: **personalizar Copilot code review ya no requiere Enterprise**. | **Confirmado** por redirección y por ausencia de la expresión en la página destino. Las fechas exactas de deprecación provienen de títulos de changelog, no de páginas leídas. |
| "knowledge bases" | **Copilot Spaces** | La URL de knowledge bases sirve hoy la página "About GitHub Copilot Spaces". Spaces está disponible en **todas** las licencias de Copilot, incluida Free, de modo que **ya no es un diferenciador de Enterprise**. Los tres diferenciadores reales son AI credits, priority access y GitHub Spark `[Preview]`. | **Confirmado** por la redirección de URL y por la tabla de planes. |
| `@workspace` (en VS Code) | `#codebase` (`#search/codebase`) | `@workspace` desapareció de todas las páginas actuales de VS Code y de su cheat sheet; su función la cubre `#codebase`. **`@workspace` sigue plenamente vigente en Visual Studio**, donde además no existen `#codebase`, `#file:` ni `#selection`. | **Solo por ausencia.** Ninguna fuente oficial declara que `@workspace` fuera deprecado o reemplazado. |
| frontmatter `mode:` | frontmatter `agent:` | En los prompt files, `mode:` ya no figura en la tabla de frontmatter documentada; la única clave de selección de agente es `agent:`, con valores `ask`, `agent`, `plan` o el nombre de un custom agent. | **Inferido por ausencia.** No hay declaración oficial de renombrado, a diferencia del caso `.chatmode.md` → `.agent.md`. |
| Variables de contexto en forma plana | Forma con espacio de nombres | La tabla autoritativa de "Chat tools" usa `#search/codebase`, `#read/problems`, `#search/changes` y `#web/fetch`, mientras que las páginas de prosa siguen usando `#codebase`, `#problems`, `#changes` y `#fetch`. `#selection`, `#todos` y `#githubRepo` no cambian. | **Ambas formas están publicadas oficialmente y vigentes.** No es un renombrado cerrado: la documentación es internamente inconsistente y hay que aceptar las dos. |

## Notas de uso del glosario

- Cuando el material del curso escriba un identificador, un comando o un nombre de producto, debe hacerlo en inglés y entre backticks, tal como aparece en este glosario.
- Los identificadores del repositorio de práctica van **sin acentos** por decisión de codificación; en la prosa del material sí se acentúa con normalidad.
- Toda capacidad de producto citada en clase debe llevar su etiqueta `[GA]`, `[Preview]` o `[Depende del plan/política]`. Si un dato no figura en `06_recursos/ficha_tecnica_copilot_verificada.md`, debe presentarse como **no documentado oficialmente**, nunca como hecho.
- Los términos marcados como confirmados "solo por ausencia" no deben enseñarse como renombrados oficiales: enséñese el estado actual y adviértase de que la documentación no explica la transición.
