# Ficha técnica verificada — GitHub Copilot (consulta: 14 de agosto de 2026)

> **Contexto de uso:** curso corporativo, licencia **Copilot Enterprise**, IDE principal **Visual Studio Code**, stack **C#/.NET 8**.
> **Convención de etiquetas:** `[GA]` = generalmente disponible · `[Preview]` = public preview / experimental según la fuente · `[Depende del plan/política]` = sujeto a plan o a política de organización/empresa.
>
> ⚠️ **ADVERTENCIA CRÍTICA PARA EL DISEÑO DEL CURSO:** entre 2025 y agosto de 2026 GitHub y Microsoft ejecutaron dos renombrados masivos que invalidan la mayoría del material de formación preexistente:
> 1. En VS Code **desaparecieron los modos Ask / Edit / Agent** como selector de tres modos; ahora hay **Session Target** (harness) + **agent role** (Agent / Plan). Los *chat modes* pasaron a llamarse **custom agents** (`.chatmode.md` → `.agent.md`).
> 2. **"Copilot coding agent" se renombró a "Copilot cloud agent"**.
> 3. La documentación de VS Code se movió de `/docs/copilot/...` a `/docs/agents/...` y `/docs/agent-customization/...`.

---

## 1. Planes de Copilot vigentes y diferencia real Business vs Enterprise

### 1.1 Planes vigentes (nombres y precios exactos)

| Plan (nombre exacto) | Precio |
|---|---|
| GitHub Copilot Free | Not applicable |
| **GitHub Copilot Student** | Free |
| GitHub Copilot Pro | $10 USD / mes |
| GitHub Copilot Pro+ | $39 USD / mes |
| **GitHub Copilot Max** | $100 USD / mes |
| GitHub Copilot Business | $19 USD / granted seat / mes |
| GitHub Copilot Enterprise | $39 USD / granted seat / mes |


Avisos vigentes en la misma página (verbatim):
- *"Starting April 22, 2026, new self-serve sign-ups for Copilot Business for organizations on GitHub Free and GitHub Team plans are temporarily paused."*
- *"Copilot is not currently available for GitHub Enterprise Server."*
- (En las páginas de Visual Studio) *"As of April 20, 2026, GitHub Copilot Pro trials have been paused."*

### 1.2 Qué incluye Enterprise que NO incluye Business

**Hallazgo importante y contraintuitivo: la brecha funcional Business↔Enterprise es hoy MUCHO menor que en 2024-2025.** Verificado icono por icono en las tablas comparativas de la página Plans: en las secciones **Agents** y **Chat**, las columnas Business y Enterprise son **idénticas**.

Diferenciadores confirmados (verbatim):

| Diferenciador | Business | Enterprise | Estado |
|---|---|---|---|
| **AI credits** por usuario/mes | **1,900** | **3,900** | `[GA]` |
| **Priority access to new models and features** | ✗ | ✓ | `[GA]` |
| **GitHub Spark** | ✗ | ✓ | `[Preview]` |
| Requisito de plataforma | GitHub Free / Team / GHEC | **solo GitHub Enterprise Cloud** | `[GA]` |

Citas literales:
- Plans: *"Copilot Enterprise … includes all the features of Copilot Business, **priority access to new models and features, a larger monthly pool of AI credits, plus additional enterprise-grade capabilities**."*
- Billing: *"Copilot Business at $19 USD per user per month, includes **1,900 AI credits** per user, and access to a broad model catalog."* / *"Copilot Enterprise at $39 USD per user per month, includes **3,900 AI credits** per user (GitHub Enterprise Cloud only), and priority access to new models and features."*
- Spark: *"Who can use this feature? — **Copilot Pro+ and Copilot Enterprise**."* `[Preview]`

**⚠️ Obsolescencias que hay que retirar del material de curso:**
- **"Knowledge bases" ya NO es un diferenciador de Enterprise.** La URL de knowledge bases hoy sirve la página **"About GitHub Copilot Spaces"**, y Spaces está disponible en cualquier licencia Copilot, incluida Free.
- **Las "coding guidelines" de code review, que eran exclusivas de Enterprise, fueron eliminadas** (ver §7).

### 1.3 Qué requiere Business **o** Enterprise (no distingue entre ambos) `[Depende del plan/política]`

- Organization custom instructions
- Content exclusion / exclude specified files from Copilot
- Organization-wide policy management
- Audit logs (también en Pro+ y Max)
- Copilot usage metrics / Metrics API

Están disponibles **en todos los planes, incluido Free**: Copilot CLI, MCP, prompt files, next edit suggestions, agent mode, repository & personal custom instructions, block suggestions matching public code.

---

## 2. Modos de chat en VS Code: el estado real en agosto de 2026

### 2.1 Ask / Edit / Agent: **ya no existen como selector de tres modos** `[GA]`

Las cadenas *"Ask mode"* y *"Edit mode"* **no aparecen en ninguna** de las páginas actuales de VS Code (chat-overview, custom-agents, concepts/agents, agent-harnesses, chat-view, cheat sheet). No hay frase oficial de migración: se confirma **por ausencia**, no por declaración.

### 2.2 Lo que existe hoy: modelo de dos ejes

**Eje 1 — Session Target (elige el *harness*, es decir, dónde corre el agente)** `[GA]`

> *"In Visual Studio Code, you choose an agent harness and where it runs with the **Session Target** control."*

Cómo se selecciona (verbatim):
1. Abrir Chat view: `Ctrl+Alt+I` (macOS `⌃⌘I`) — o la Agents window
2. Seleccionar **New Chat** (`+`)
3. Abrir el control **Session Target** y elegir el harness

Harnesses disponibles: **Local**, **Copilot**, **Claude** `[Preview]`, **Codex**, **Cloud**.

**Eje 2 — agent role (elige el rol / persona), vía el "agents dropdown"** `[GA]`

Solo hay **dos roles integrados**:
> *"Local sessions provide these built-in agent roles: **Agent**: autonomously plans and performs complex coding tasks, edits files, runs commands, and iterates on results. **Plan**: researches a task and creates a structured implementation plan before code changes. You can switch roles during a session from the agent picker."*

- Atajo documentado: `Ctrl+Shift+I` (Linux `Ctrl+Shift+Alt+I`, macOS `⇧⌘I`) — *"Switch to using agents in the Chat view"*.
- El rol **Plan** también se invoca con el slash command `/plan`.
- **No hay atajo documentado** para abrir el desplegable de agentes en sí (la celda de keybinding está vacía en el cheat sheet).

### 2.3 Rastro residual de "ask"

El identificador `ask` **sí sobrevive** como valor del frontmatter `agent:` en prompt files: valores válidos `ask`, `agent`, `plan`, o el nombre de un custom agent. Es el único lugar donde "ask" sigue documentado.

### 2.4 Custom agents (antes "chat modes") `[GA]`

> *"Custom agents were previously known as custom chat modes. The functionality remains the same, but the terminology has been updated… If you have existing `.chatmode.md` files, **rename them to `.agent.md`** to convert them to the new custom agent format."*

Ver §6 para sintaxis completa.

### 2.5 Otros atajos de chat confirmados `[GA]`

| Acción | Windows / Linux | macOS |
|---|---|---|
| Abrir Chat view | `Ctrl+Alt+I` | `⌃⌘I` |
| Inline chat (editor o terminal) | `Ctrl+I` | `⌘I` |
| Quick Chat | `Ctrl+Shift+Alt+L` | `⇧⌥⌘L` |
| Nueva sesión de chat | `Ctrl+N` | `⌘N` |
| Model picker | `Ctrl+Alt+.` | `⌥⌘.` |
| Prompt anterior / siguiente | `Ctrl+Alt+↑` / `Ctrl+Alt+↓` | `⌥⌘↑` / `⌥⌘↓` |
| Bloque de código anterior / siguiente | `Ctrl+Alt+PageUp` / `PageDown` | `⌥⌘PageUp` / `PageDown` |

Comandos de layout: **Chat: New Chat**, **Chat: New Chat Editor**, **Chat: New Chat Window**.

---

## 3. Code completions y next edit suggestions: atajos oficiales en VS Code

### 3.1 Inline suggestions (ghost text) `[GA]`

| Acción | Windows | Linux | macOS | Command ID |
|---|---|---|---|---|
| Aceptar sugerencia | `Tab` | `Tab` | `Tab` | `editor.action.inlineSuggest.commit` |
| Rechazar / descartar | `Esc` | `Esc` | `Esc` | `editor.action.inlineSuggest.hide` |
| Sugerencia alternativa siguiente | `Alt+]` | `Alt+]` | `⌥+]` | `editor.action.inlineSuggest.showNext` |
| Sugerencia alternativa anterior | `Alt+[` | `Alt+[` | `⌥+[` | `editor.action.inlineSuggest.showPrevious` |
| Forzar sugerencia | `Alt+\` | `Alt+\` | `⌥+\` | `editor.action.inlineSuggest.trigger` |
| Abrir panel con sugerencias adicionales | `Ctrl+Enter` | `Ctrl+Enter` | `Ctrl+Return` | `github.copilot.generate` |
| Activar/desactivar Copilot | *sin atajo por defecto* | — | — | `github.copilot.toggleCopilot` |
| **Aceptar la siguiente palabra / línea** | **`Ctrl+Right`** | `Ctrl+Right` | `⌘→` | *no publicado* |

Cita verbatim para el aceptado parcial: *"To partially accept a suggestion, use the `⌘→` (Windows, Linux `Ctrl+Right`) keyboard shortcut to accept either the next word of a suggestion, or the next line."*

### 3.2 Next Edit Suggestions (Copilot NES) `[GA]`

Mecánica (verbatim): *"You can quickly navigate to suggested code changes with the `Tab` key… You can then accept a suggestion with the `Tab` key again."* Una **flecha en el gutter** indica que hay una edición sugerida. En el cheat sheet: *"`Tab` — Accept inline suggestion or navigate to the next edit suggestion"*.

Settings exactos:
- `github.copilot.nextEditSuggestions.enabled` — **este es el setting que habilita NES**
- `github.copilot.nextEditSuggestions.fixes` — NES basado en diagnostics (squiggles), p.ej. imports faltantes
- `editor.inlineSuggest.edits.allowCodeShifting`
- `editor.inlineSuggest.edits.renderSideBySide` — `auto` (default) / `never`
- `editor.inlineSuggest.edits.showCollapsed`
- `editor.inlineSuggest.minShowDelay` — default `0`

Settings de ghost text: `github.copilot.enable`, `editor.inlineSuggest.fontFamily`, `editor.inlineSuggest.showToolbar`, `editor.inlineSuggest.syntaxHighlightingEnabled`.

Comandos útiles: **Snooze Inline Suggestions** / **Cancel Snooze Inline Suggestions**. Cambio de modelo de completions: menú Chat → **Configure Inline Suggestions...** → **Change Completions Model...**

> ⚠️ *"Custom instructions are **not** taken into account for inline suggestions as you type in the editor."* Las custom instructions NO afectan al autocompletado.

---

## 4. Slash commands y variables de contexto en VS Code

### 4.1 Slash commands actuales (lista completa del cheat sheet de VS Code) `[GA]` salvo lo indicado

**Generación / edición de código:** `/doc` · `/explain` · `/fix` · `/tests` · `/setupTests` · `/fixTestFailure` · `/new` (scaffold workspace o archivo) · `/newNotebook` · `/search` · `/startDebugging` `[Preview: (Experimental)]`

**Gestión de sesión:** `/clear` · `/compact` · `/fork` · `/plan` · `/init` (genera o actualiza `copilot-instructions.md` o `AGENTS.md`) · `/debug` · `/troubleshoot` (requiere `github.copilot.chat.agentDebugLog.enabled`)

**Configuración de customizaciones:** `/agents` · `/hooks` · `/instructions` · `/prompts` · `/skills`

**Generación asistida por IA de customizaciones:** `/create-agent` · `/create-prompt` · `/create-instruction` · `/create-skill` · `/create-hook`

**Auto-aprobación:** `/yolo` = `/autoApprove` (activa `chat.tools.global.autoApprove`, muestra diálogo de advertencia la primera vez) · `/disableYolo` = `/disableAutoApprove`

**Dinámicos:** `/<skill name>` · `/<prompt name>` — los prompt files y agent skills **aparecen como slash commands**.

**No-slash:** iniciar el mensaje con `!` ejecuta un comando de terminal directamente. *"The `!` command is only available in Agent Host sessions."*

Slash commands específicos del harness Claude: `/agents`, `/hooks`, `/memory`, `/init`, `/pr-comments`, `/review`, `/security-review`. Otros: `/research`, `/delegate`.

### 4.2 Estado de los slash commands del brief

| Comando | Estado en VS Code (ago-2026) |
|---|---|
| `/explain`, `/fix`, `/tests`, `/doc`, `/new`, `/clear` | **Vigentes** |
| `/newNotebook`, `/setupTests`, `/startDebugging`, `/fixTestFailure` | **Vigentes** |
| `/help` | **NO está** en el cheat sheet de VS Code. Solo aparece en la página (obsoleta) de GitHub Docs. **Conflicto entre fuentes oficiales.** |
| `/createWorkspace` | **No encontrado** en ninguna página actual. Su función la cubre `/new`. |
| `/terminal` | **No encontrado** como slash command. La función la cubre el participante `@terminal`. |

### 4.3 Variables de contexto: la sintaxis pasó a estar **namespaced** `[GA]`

Las páginas de prosa siguen usando la forma plana; la **tabla de "Chat tools"** del cheat sheet (la lista autoritativa de herramientas integradas) usa la forma con namespace. **Ambas formas están publicadas oficialmente; la documentación es internamente inconsistente.**

| Forma antigua / plana | Forma actual en la tabla de tools |
|---|---|
| `#codebase` | **`#search/codebase`** |
| `#terminalLastCommand` | **`#read/terminalLastCommand`** |
| `#terminalSelection` | **`#read/terminalSelection`** |
| `#problems` | **`#read/problems`** |
| `#changes` | **`#search/changes`** |
| `#fetch` | **`#web/fetch`** |
| `#file` | `#<file\|folder\|symbol>` |

**Tool sets (namespaces):** `#agent` · `#browser` · `#edit` · `#execute` · `#read` · `#search` · `#web` · `#vscode`

**Sin cambio (forma plana):** `#selection` · `#todos` · `#githubRepo` · `#githubTextSearch` · `#newWorkspace`

**Herramientas concretas:** `#edit/createFile`, `#edit/editFiles`, `#edit/editNotebook`, `#execute/runInTerminal`, `#execute/createAndRunTask`, `#execute/testFailure`, `#read/readFile`, `#search/fileSearch`, `#search/textSearch`, `#search/usages`, `#agent/runSubagent`, `#vscode/runCommand`, `#vscode/VSCodeAPI`, `#vscode/extensions`, `#vscode/installExtension`, `#vscode/askQuestions`, `#vscode/getProjectSetupInfo`

Dentro del **cuerpo** de un `.agent.md`, `.instructions.md` o `.prompt.md` la sintaxis es distinta: **`#tool:<tool-name>`**, p.ej. `#tool:web/fetch`.

### 4.4 Chat participants (`@`) `[GA]`

**Siguen soportados, NO están deprecados.** Verbatim:
> *"Chat participants are specialized assistants… You can invoke a chat participant by @-mentioning it: type `@` followed by the participant name. VS Code has built-in chat participants like `@vscode` or `@terminal`."*

Participantes integrados confirmados en VS Code: **`@github`**, **`@terminal`**, **`@vscode`**. Ejemplos oficiales: `@github What are all of the open PRs assigned to me?` · `@terminal list the 5 largest files in this workspace` · `@vscode how to enable word wrapping?` · `@terminal /explain`

> ⚠️ **`@workspace` ha DESAPARECIDO.** No aparece en ninguna página actual de VS Code ni en el cheat sheet de GitHub. Su función la cubre `#codebase` / `#search/codebase`. **Ninguna fuente oficial declara que fue deprecado o reemplazado** — se infiere solo por ausencia. (Nótese: `@workspace` **sí sigue vigente en Visual Studio**, ver §14.)

### 4.5 Otras formas de añadir contexto `[GA]`

**Add Context** en la Chat view → **Files & Folders** / **Symbols** (Quick Pick) · drag-and-drop desde Explorer, Search o pestañas del editor · adjuntar imágenes (vision) · URLs directas en el prompt · Browser context: botón **Add to Chat** → **Add Element to Chat** / **Add Screenshot to Chat** / **Add Console Logs to Chat**

---

## 5. Custom instructions: rutas, precedencia y alcance

### 5.1 Taxonomía de VS Code

VS Code distingue **always-on instructions** (siempre aplicadas) y **file-based instructions** (condicionales por glob).

> *"If you have multiple instruction files in your project, VS Code combines and adds them to the chat context, **no specific order is guaranteed**."*

### 5.2 Tabla consolidada: ámbito → ruta exacta `[GA]`

| Ámbito | Ruta / mecanismo exacto |
|---|---|
| **Repo-wide, always-on** | `.github/copilot-instructions.md` |
| **Path-specific** | `.github/instructions/**/*.instructions.md` (búsqueda **recursiva**; frontmatter `applyTo`) |
| Repo, formato Claude Rules | `.claude/rules/` (usa `paths`, **no** `applyTo`; default `**`) |
| **Agent instructions** | `AGENTS.md` — en **cualquier** directorio del repo; **el más cercano en el árbol de directorios gana** |
| Variantes Claude | `CLAUDE.md` · `.claude/CLAUDE.md` · `~/.claude/CLAUDE.md` · `CLAUDE.local.md` (no versionado) |
| Variante Gemini | `GEMINI.md` (raíz del repo) — documentado **solo** en GitHub Docs |
| **Personal / user (VS Code + Agent Host)** | `~/.copilot/instructions` · `~/.claude/rules` |
| **Personal / user (Copilot CLI)** | `~/.copilot/copilot-instructions.md` · `~/.copilot/instructions/**/*.instructions.md` |
| **Personal (GitHub.com)** | Popup en la página de Copilot Chat en GitHub.com (sin archivo) |
| **Organization** | UI de settings de Copilot de la organización `[Depende del plan/política: Business/Enterprise, org owner]` |

Rutas por defecto de `.instructions.md`:

| Scope | Default file location |
|---|---|
| Workspace | `.github/instructions` |
| Workspace (Claude format) | `.claude/rules` |
| User profile | `~/.copilot/instructions` o `~/.claude/rules` |

> **Importante (Agent Host):** *"When Agent Host is enabled, the agent reads user-level instructions from harness-agnostic folders like `~/.copilot/instructions` and `~/.claude/rules` and **not** from VS Code profile user data."*

### 5.3 Frontmatter de `*.instructions.md`

| Campo | Requerido | Descripción |
|---|---|---|
| `name` | No | Nombre visible; por defecto el nombre de archivo |
| `description` | No | Descripción corta (hover en Chat view) |
| `applyTo` | No | Glob relativo a la raíz del workspace. `**` = todos. **Si se omite, las instrucciones no se aplican automáticamente** (pero pueden añadirse manualmente) |
| `excludeAgent` | No | **Solo GitHub Docs.** Valores `"code-review"` o `"cloud-agent"`. Si se omite, ambos usan las instrucciones |

Múltiples patrones separados por coma: `applyTo: "**/*.cs,**/*.csproj"`

Ejemplos de glob documentados: `*` (directorio actual) · `**` o `**/*` (todo) · `*.cs` · `**/*.cs` · `src/*.cs` (no recursivo) · `src/**/*.cs` (recursivo) · `**/subdir/**/*.cs`

### 5.4 Precedencia (verbatim, GitHub Docs — fuente autoritativa) `[GA]`

> *"Personal instructions take the highest priority. Repository instructions come next, and then organization instructions are prioritized last. **However, all sets of relevant instructions are provided to Copilot.**"*
>
> Orden completo de precedencia, de mayor a menor:
> 1. **Personal** instructions
> 2. **Repository** custom instructions:
>    - **Path-specific** — `.github/instructions/**/*.instructions.md`
>    - **Repository-wide** — `.github/copilot-instructions.md`
>    - **Agent** instructions (p.ej. `AGENTS.md`)
> 3. **Organization** custom instructions

Confirmado también en la página de VS Code ("Instruction priority"): 1) Personal (user-level, highest) · 2) Repository · 3) Organization (lowest).

Dos reglas adicionales:
- Dentro de un árbol: *"the nearest `AGENTS.md` file in the directory tree will take precedence."*
- En code review: *"Copilot reads repository custom instructions, agent instructions, and agent skills from the **head branch** (the branch with your changes), **not the base branch**."*

### 5.5 Alcance: qué superficie respeta qué tipo de instrucción `[GA]`

Leyenda: 👤 Personal · 📦 Repository-wide (`.github/copilot-instructions.md`) · 📂 Path-specific · 🤖 Agent (`AGENTS.md`/`CLAUDE.md`/`GEMINI.md`) · 🏢 Organization

| Entorno | Copilot Chat | Copilot cloud agent | Copilot code review |
|---|---|---|---|
| **GitHub.com** | 👤 📦 🏢 | 📦 📂 🤖 (AGENTS/CLAUDE/GEMINI) 🏢 | 📦 📂 🤖 (**solo AGENTS.md**) 🏢 |
| **Visual Studio Code** | 📦 📂 🤖 (**solo AGENTS.md**) | 📦 📂 🤖 (AGENTS/CLAUDE/GEMINI) | 📦 **solamente** |
| **Visual Studio** | 📦 📂 | *(sin fila)* | 📦 **solamente** |
| **JetBrains** | 👤 📦 📂 | 📦 📂 🤖 | 📦 📂 |
| **Eclipse** | 📦 solamente | 📦 📂 🤖 | **"Custom instructions are currently not supported."** |
| **Xcode** | 📦 📂 | 📦 📂 🤖 | 📦 📂 |
| **Copilot CLI** | 📦 📂 🤖 👤 (`~/.copilot/copilot-instructions.md`, `~/.copilot/instructions/**/*.instructions.md`) | | |

Notas críticas:
- **Personal instructions** solo se soportan en: GitHub.com Chat, JetBrains Chat y Copilot CLI. **No en VS Code, Visual Studio, Eclipse ni Xcode.**
- **Organization instructions**: *"currently only supported for Copilot Chat on GitHub.com, Copilot code review on GitHub.com and Copilot cloud agent on GitHub.com."*
- Las custom instructions **no** afectan a las inline suggestions.

### 5.6 Settings de VS Code relevantes `[GA]` salvo lo indicado

| Setting | Función |
|---|---|
| `chat.instructionsFilesLocations` | Ubicaciones extra de `*.instructions.md` |
| `chat.useAgentsMdFile` | Habilita/deshabilita `AGENTS.md` |
| `chat.useNestedAgentsMdFiles` | `[Preview: experimental]` `AGENTS.md` en subcarpetas |
| `chat.useClaudeMdFile` | Habilita/deshabilita `CLAUDE.md` |
| `chat.useCustomizationsInParentRepositories` | Descubrimiento en monorepo (**deshabilitado por defecto**) |
| `chat.includeApplyingInstructions` | Inclusión por patrón |
| `chat.includeReferencedInstructions` | Instrucciones referenciadas vía enlaces Markdown |
| `github.copilot.chat.organizationInstructions.enabled` | `true` para descubrir instrucciones de organización |
| `github.copilot.chat.reviewSelection.instructions` | Instrucciones para code review (settings-based, aún soportado) |
| `github.copilot.chat.commitMessageGeneration.instructions` | Mensajes de commit |
| `github.copilot.chat.pullRequestDescriptionGeneration.instructions` | Descripciones de PR |

Los tres últimos aceptan un array de objetos con `text` (inline) o `file` (ruta a un `.md`).

> **Deprecado:** *"Settings-based code generation and test generation instructions are **deprecated as of VS Code 1.102**. Use file-based instructions instead."*

Monorepo (verbatim): *"VS Code walks up the folder hierarchy from each workspace folder until it finds a `.git` folder. If found, it collects customizations from all folders between the workspace folder and the repository root (inclusive). This applies to all customization types."*

---

## 6. Prompt files y custom agents (antes chat modes)

### 6.1 Prompt files `[Preview]` según GitHub Docs, `[GA]` de facto en la doc de VS Code

> *"Prompt files, **also known as slash commands**, let you simplify prompting for common tasks by encoding them as standalone Markdown files that you can invoke directly in chat."*
>
> GitHub Docs: *"**Prompt files** (public preview) … Prompt files are only available in VS Code, Visual Studio, and JetBrains IDEs."* / *"Prompt files are public preview and subject to change."*

**Ubicaciones:**

| Scope | Default file location |
|---|---|
| Workspace | **`.github/prompts`** |
| User profile | "Your user data (specific to your VS Code profile)" — **sin ruta absoluta publicada** |

Extensión: **`.prompt.md`**. Ubicaciones extra: `chat.promptFilesLocations`.

**Frontmatter completo (conjunto documentado):**

| Campo | Req. | Descripción |
|---|---|---|
| `description` | No | Descripción corta |
| `name` | No | Nombre usado tras teclear `/`; por defecto el nombre de archivo |
| `argument-hint` | No | Texto guía en el campo de chat |
| **`agent`** | No | Agente para ejecutar el prompt: **`ask`**, **`agent`**, **`plan`**, o el nombre de un custom agent. Por defecto, el agente actual. **Si se especifican `tools`, el agente por defecto es `agent`** |
| `model` | No | Modelo de lenguaje; si se omite, el del model picker |
| `tools` | No | Lista de tools o tool sets. Para todos los tools de un servidor MCP: `<server name>/*` |

> ⚠️ **`mode:` ya no está documentado.** La única clave de selección de agente es **`agent:`**. **No existe ninguna frase oficial de renombrado** `mode` → `agent`; se infiere por ausencia.

**Cómo se invocan (tres métodos documentados):**
1. En el chat, `/` + nombre del prompt. Con argumentos inline: `/create-react-form formName=MyForm`. *"Agent skills also appear as slash commands alongside prompt files."*
2. Command Palette (`Ctrl+Shift+P`) → **Chat: Run Prompt** → Quick Pick
3. Abrir el archivo en el editor y pulsar el botón **play** en el título del editor

`/prompts` abre el menú **Configure Prompt Files**. Creación: **Chat: New Prompt File**, **Chat: New Untitled Prompt File**, o `/create-prompt`.

**Cuerpo:** enlaces Markdown relativos a archivos del workspace · `#tool:<tool-name>` · inputs con `vscode/askQuestion` o `${input:variableName}` / `${input:variableName:placeholder}` · variable `${selection}`

**Prioridad de tools:** 1) tools del prompt file → 2) tools del custom agent referenciado → 3) tools por defecto del agente seleccionado

> ⚠️ **Incompatibilidad crítica:** *"Agents running on the **Agent Host don't use prompt files**. To use an existing prompt with the Copilot agent, convert it to an agent skill."* Existe una migración one-time prompt files → skills `[Preview: experimental]` con `chat.customizations.promptMigration.enabled`. Los prompt files siguen funcionando con local agents (extension host).

### 6.2 Custom agents — `*.agent.md` (reemplaza `*.chatmode.md`) `[GA]`

**Ubicaciones:**

| Scope | Default file location |
|---|---|
| Workspace | **`.github/agents`** |
| Workspace (formato Claude) | `.claude/agents` |
| User profile | **`~/.copilot/agents`** |

*"VS Code detects any `.md` files in the `.github/agents` folder of your workspace as custom agents."* Ubicaciones extra: `chat.agentFilesLocations`.

Nivel organización/empresa (del customization cheat sheet): `/agents/AGENT-NAME.md` en el repo `.github` o `.github-private` de la organización (org-level), o en un repo `.github-private` designado (enterprise-level).

**Frontmatter completo:**

| Campo | Descripción |
|---|---|
| `description` | Descripción breve (placeholder en el campo de chat) |
| `name` | Nombre; por defecto el nombre de archivo |
| `argument-hint` | Texto guía |
| `tools` | Lista de tools / tool sets. `<server name>/*` para todo un servidor MCP |
| `agents` | Agentes disponibles como **subagents**. `*` = todos, `[]` = ninguno. Requiere incluir el tool `agent` en `tools` |
| `model` | Un modelo (string) o **lista priorizada** (array): se intentan en orden |
| `user-invocable` | bool, default `true`. `false` = oculto del dropdown, solo subagent/programático |
| `disable-model-invocation` | bool, default `false`. `true` = impide invocación como subagent |
| `infer` | **Deprecado.** Usar `user-invocable` y `disable-model-invocation` |
| `target` | `vscode` o `github-copilot` |
| `mcp-servers` | Lista de config JSON de servidores MCP (para `target: github-copilot`) |
| `handoffs` | Lista de transiciones sugeridas entre agentes |
| `handoffs.label` / `.agent` / `.prompt` / `.send` / `.model` | Etiqueta del botón / agente destino / prompt / auto-envío (default `false`) / modelo cualificado, formato `Model Name (vendor)` |
| `hooks` | `[Preview]` Hooks acotados al agente. Requiere `chat.useCustomAgentHooks` |

Ejemplo oficial (Planner):
```yaml
---
description: Generate an implementation plan for new features or refactoring existing code.
name: Planner
tools: ['web/fetch', 'search/codebase', 'search/usages']
model: ['Claude Opus 4.5', 'GPT-5.2']  # Tries models in order
handoffs:
  - label: Implement Plan
    agent: agent
    prompt: Implement the plan outlined above.
    send: false
---
```

**Creación:** `/agents` → **Configure Custom Agents** · Chat view → **Configure Chat** (gear) → pestaña **Agents** → **New Agent (Workspace)** / **New Agent (User)** · Command Palette → **Chat: New Custom Agent** · `/create-agent` (generación con IA) · **Generate Agent** en el Agent Customizations editor `[Preview]`

**Formato Claude** (`.claude/agents/*.md`): `name` (requerido), `description`, `tools` (string separado por comas), `disallowedTools`.

**Nivel organización:** `github.copilot.chat.organizationCustomAgents.enabled = true` habilita el descubrimiento de custom agents definidos a nivel de organización de GitHub.

### 6.3 Agent skills `[GA]` (estándar abierto — recomendado por portabilidad)

| Tipo | Ubicación |
|---|---|
| Project skills | `.github/skills/` · `.claude/skills/` · `.agents/skills/` |
| Personal skills | `~/.copilot/skills/` · `~/.claude/skills/` · `~/.agents/skills/` |

Cada skill es un **directorio** con un archivo **`SKILL.md`**. Ubicaciones extra: `chat.agentSkillsLocations`. Estándar abierto: **agentskills.io**.

Frontmatter de `SKILL.md`:

| Campo | Req. | Notas |
|---|---|---|
| `name` | **Sí** | minúsculas/números/guiones; sin slashes, dos puntos, puntos ni prefijos de namespace; **debe coincidir con el nombre del directorio padre**; máx. 64 chars; caracteres inválidos → fallo silencioso de carga |
| `description` | **Sí** | qué hace **y cuándo usarlo**; máx. 1024 chars |
| `argument-hint` | No | |
| `user-invocable` | No | default `true`; `false` oculta del menú `/` pero el agente puede auto-cargarlo |
| `disable-model-invocation` | No | default `false`; `true` = solo invocación manual con `/` |
| `context` | No | `[Preview: experimental]` default inline; `fork` = subagente dedicado. Requiere `github.copilot.chat.skillTool.enabled` |

**Ventaja decisiva para el curso:** los agent skills *"work across VS Code, Copilot CLI, and Copilot cloud agent"*, mientras que las custom instructions son *"VS Code and GitHub.com only"*.

### 6.4 Matriz de soporte de customizaciones por superficie `[GA]`

Clave: ✓ soportado · ✗ no soportado · **P** en preview

| Feature | VS Code | Visual Studio | JetBrains | Eclipse | Xcode | GitHub.com | Copilot CLI |
|---|---|---|---|---|---|---|---|
| Custom instructions | ✓ | ✓ | P | P | P | ✓ | ✓ |
| Prompt files | ✓ | ✓ | P | ✗ | P | ✗ | ✗ |
| Custom agents | ✓ | ✓ | P | P | P | ✓ | ✓ |
| Subagents | ✓ | ✗ | P | P | P | ✗ | ✓ |
| Agent skills | ✓ | ✓ | P | ✗ | ✗ | ✓ | ✓ |
| Hooks | **P** | ✗ | ✗ | ✗ | ✗ | ✓ | ✓ |
| MCP servers | ✓ | ✓ | ✓ | ✓ | ✓ | ✓ | ✓ |

Rutas canónicas (customization cheat sheet): hooks → `.github/hooks/*.json`

---

## 7. Copilot code review

### 7.1 Plan requerido `[Depende del plan]`

> *"Who can use this feature? — **Available for all paid Copilot plans.**"*

**Copilot Enterprise NO es requisito.** Business, Pro, Pro+, Max y Enterprise lo incluyen. Free y Student no.

### 7.2 Solicitar revisión en un Pull Request (GitHub.com) `[GA]`

1. *"On GitHub.com, create a pull request or navigate to an existing pull request."*
2. *"Under **Reviewers** in the right sidebar, next to **Copilot**, click **Request**."*
3. *"Wait for Copilot to review your pull request. This usually takes less than 30 seconds."*
4. Leer los comentarios de Copilot.

- *"Copilot always leaves a **'Comment'** review, not an 'Approve' review or a 'Request changes' review."* → **no cuenta para aprobaciones requeridas ni bloquea el merge**.
- Re-revisión: botón junto al nombre de Copilot en el menú **Reviewers**.
- REST API: solicitar a **`copilot-pull-request-reviewer[bot]`** como reviewer.
- GitHub CLI: `gh pr create --reviewer @copilot` · `gh pr edit PR-NUMBER --add-reviewer @copilot`
- GitHub Mobile: PR → sección **Reviews** → **Request Reviews** → Copilot → **Done**

### 7.3 Revisión en el IDE (VS Code) `[GA]` / `[Preview]` según la acción

**Sobre una selección** `[Preview]` (**Review Selection** está marcado *(Preview)* en el cheat sheet):
1. Seleccionar el código
2. Clic derecho → **Generate Code** > **Review**
3. Los comentarios aparecen inline y en el panel **Comments**

**Sobre todos los cambios sin commitear:**
1. Botón **Source Control** en la Activity Bar
2. En el tope de la vista **Source Control**, hover sobre **CHANGES** → botón **Copilot Code Review - Uncommitted Changes**
3. Resultados inline y en la pestaña **Problems**

Aplicar sugerencias: **Apply and Go To Next** / **Discard and Go to Next**

**Visual Studio** (relevante para .NET): requiere **17.14 o posterior**; en la ventana **Git Changes** → **Review changes with Copilot**. JetBrains **Rider**: botón **Copilot: Review Code Changes** sobre el mensaje de commit.

### 7.4 Revisión automática — tres niveles `[GA]`

**a) Personal** — `[Depende del plan]` *"This is only available if you are on the Copilot Pro, Copilot Pro+, or Copilot Max plans."*
Foto de perfil → **Copilot settings** → **Automatic Copilot code review** → **Enabled**

**b) Un repositorio (ruleset de repo)**
Repo → **Settings** → sidebar "Code and automation" → **Rules** → **Rulesets** → **New ruleset** → **New branch ruleset** → nombre → Enforcement Status **Active** → **Target branches** / **Add target** → en "Branch rules" marcar **Automatically request Copilot code review** → opcionales **Review new pushes** y **Review draft pull requests** → **Create**

**c) Organización (ruleset de org)**
Perfil → **Organizations** → org → **Settings** → sidebar "Code, planning, and automation" → **Repository** → **Rulesets** → **New ruleset** → **New branch ruleset** → repos por **Include by pattern** / **Exclude by pattern** → branches → **Automatically request Copilot code review** → **Create**

> ⚠️ **No hay configuración de revisión automática a nivel enterprise** documentada. A nivel enterprise solo existen políticas.

**Review effort level:** org → Settings → "Code, planning, and automation" → **Copilot** → **Code review** → "Review effort level": **Lite** (default) / **Balanced**. El nivel de repo (Settings → **Copilot** → **Code review**) sobrescribe el de org.

Coste estimado: *"$0.05 USD to $1 USD worth of AI credits with 'Lite' effort, and $0.25 USD to $5 USD worth of AI credits with 'Balanced' effort."* Las capacidades agénticas corren en **GitHub Actions runners** (consumen minutos de Actions; los self-hosted no).

### 7.5 ⚠️ "Coding guidelines" — **ELIMINADO, reemplazado por custom instructions** `[GA]`

Verificado por redirecciones en docs.github.com: ambas URLs antiguas de coding-guidelines ahora sirven páginas de custom instructions, y la expresión *"coding guidelines"* **no aparece** en el destino.

Reemplazo (verbatim):
> *"You can customize Copilot code review by adding custom instructions to your repository… Use `.github/copilot-instructions.md` for repository-wide review guidance… Use an `AGENTS.md` file in the root of your repository… Use `.github/instructions/**/*.instructions.md` files for path-specific instructions that only apply when reviewing matching files."*

Activar/desactivar (ruta exacta): repo → **Settings** → sidebar "Code, planning, and automation" → **Copilot** → **Code review** → toggle **"Use custom instructions when reviewing pull requests"** (activado por defecto).

**Consecuencia práctica: la personalización de code review ya NO requiere Copilot Enterprise.**

Otros toggles: **"Allow Copilot to use MCP tools when reviewing pull requests"** (activado por defecto).

### 7.6 Otros datos

- Superficies soportadas: GitHub.com, GitHub CLI, GitHub Mobile, VS Code, Visual Studio, Xcode, JetBrains IDEs, **Azure DevOps** `[Preview]`
- *"Since Copilot code review is generally available, all model usage will be subject to the generally available terms."* `[GA]`
- *"Passing suggestions to Copilot cloud agent is in public preview and subject to change."* `[Preview]`
- Uso sin licencia por miembros de la org requiere **dos** políticas: **AI credits paid usage** + **Allow members without a Copilot license to use Copilot code review in GitHub.com** (deshabilitada por defecto, "most restrictive", solo Business/Enterprise, **no disponible en IDEs**) `[Depende del plan/política]`
- Archivos excluidos de la revisión: archivos de gestión de dependencias (ejemplos dados: `package.json`, `Gemfile.lock`), archivos de log, archivos SVG
- Entorno de build para la revisión: `.github/workflows/copilot-code-review.yml` (específico de review, gana si existe) o `.github/workflows/copilot-setup-steps.yml` (compartido con el cloud agent) — **aquí se instala el .NET 8 SDK**

---

## 8. Copilot cloud agent (antes "Copilot coding agent")

### 8.1 Renombrado confirmado `[GA]`

> github.blog, *"Research, plan, and code with Copilot cloud agent"* (1 de abril de 2026): *"**Copilot cloud agent (formerly known as Copilot coding agent)** is no longer limited to pull-request workflows, unlocking a broader range of ways to put Copilot to work."*

Título actual de la doc: **"About GitHub Copilot cloud agent"**. Las URLs con `coding-agent` sobreviven como redirecciones.

### 8.2 Plan requerido `[Depende del plan]`

> *"Copilot cloud agent is available for **all paid Copilot plans**. The agent is available in all repositories stored on GitHub, except repositories owned by managed user accounts and where it has been explicitly disabled."*

### 8.3 Asignar un issue a Copilot `[GA]`

*"Assigning an issue always creates a pull request."*
1. En el sidebar derecho del issue, clic en **Assignees**
2. Clic en **Copilot** en la lista de assignees
3. Opcional: contexto en el campo **Optional prompt**
4. Opcional: cambiar repo destino o base branch
5. Opcional: dropdown de agente → custom agent o **Create an agent**
6. Opcional: dropdown de modelo

> *"Copilot receives the issue title, description, and existing comments at assignment time. **It does not see comments added after assignment.**"*

**Otros puntos de entrada:** pestaña **Agents** de un repo · github.com/copilot/agents · panel de agentes · `/task` en Copilot Chat · campo **Prompt** al crear repo · mención `@copilot` en un PR existente · Copilot CLI `/delegate` · VS Code (Remote Agent Sessions) · GitHub Mobile · REST API · GitHub MCP Server · GitHub CLI · Raycast · integraciones con Jira, Slack, Teams, Linear, Azure Boards

### 8.4 Activación por administrador `[Depende del plan/política]`

> *"Copilot cloud agent and use of third-party MCP servers are **disabled by default** for organization members assigned a GitHub Copilot Enterprise or Copilot Business license by your organization."*
> - Política **"Copilot cloud agent"** → **Enabled**
> - Política **"MCP servers on GitHub.com"** → **Enabled**

> *"If you are a Copilot Pro, Copilot Pro+, or Copilot Max subscriber, Copilot cloud agent is **enabled by default**."*

Páginas enterprise relacionadas: "Enabling GitHub Copilot cloud agent in your enterprise", "Blocking agentic features in your enterprise". Excepción documentada: *"for Copilot cloud agent, enterprises can select exactly which organizations receive access."*

**Caveat operativo:** un ruleset o branch protection incompatible bloquea al agente → añadir Copilot como **bypass actor**.

### 8.5 Estado y límites `[GA]`

Sin etiqueta de preview. Límites duros: **un repositorio por tarea**, **una branch**, **exactamente un PR por tarea**, **máximo 59 minutos** de ejecución por sesión. Features adyacentes en preview: **Copilot Memory** `[Preview]`, **cloud/local sandboxes** `[Preview]`, **third-party coding agents** `[Preview]` (partner agents: Anthropic Claude, OpenAI Codex).

---

## 9. GitHub Copilot CLI

### 9.1 Nombre, estado y plan `[GA]`

Nombre oficial: **GitHub Copilot CLI** (abreviado "Copilot CLI"). Binario / comando: **`copilot`**.

> *"GitHub Copilot CLI is available with **all Copilot plans**. If you receive Copilot from an organization, the **Copilot CLI policy** must be enabled in the organization's settings."* `[Depende del plan/política]`

Las páginas de doc no llevan etiqueta de preview. GA confirmado por el título del changelog *"GitHub Copilot CLI is now generally available"* (2026-02-25) — **el post no fue abierto, solo su título vía búsqueda**.

### 9.2 Instalación (comandos exactos)

| Método | Comando |
|---|---|
| **npm** (requiere Node.js **22+**) | `npm install -g @github/copilot` · prerelease: `npm install -g @github/copilot@prerelease` · con `ignore-scripts=true`: `npm_config_ignore_scripts=false npm install -g @github/copilot` |
| **WinGet** (Windows) | `winget install GitHub.Copilot` · prerelease: `winget install GitHub.Copilot.Prerelease` |
| **Homebrew** (macOS/Linux) | `brew install --cask copilot-cli` · prerelease: `brew install --cask copilot-cli@prerelease` |
| **Script** | `curl -fsSL https://gh.io/copilot-install \| bash` o `wget -qO- https://gh.io/copilot-install \| bash` (vars `PREFIX`, `VERSION`; `\| sudo bash` para /usr/local/bin) |
| **Descarga directa** | https://github.com/github/copilot-cli/releases/ |

Prerrequisitos: suscripción activa a Copilot; **en Windows, PowerShell v6 o superior** (o WSL). SO: Linux, macOS, Windows (desde PowerShell) y WSL. Actualización: `copilot update` o `/update`.

**Recomendación para un equipo .NET en Windows:** `winget install GitHub.Copilot` es la ruta más limpia (evita la dependencia de Node.js 22+).

### 9.3 Autenticación

- Primer arranque: slash command **`/login`**
- Comando: `copilot login [--host HOST | --web-flow | --device-code]` — OAuth; web flow por defecto en local, device-code por defecto en terminales remotas/CI
- Token en el credential store del sistema; si no, en texto plano bajo `~/.copilot/` (o `COPILOT_HOME`)
- Variables de entorno en orden de precedencia: `COPILOT_GITHUB_TOKEN` → `GH_TOKEN` → `GITHUB_TOKEN`
- PAT fine-grained con permiso **"Copilot Requests"** (pestaña Account, solo tokens propiedad de usuario). *"Classic personal access tokens (`ghp_`) are **not supported**."*

### 9.4 Comandos de la CLI

`copilot` · `copilot completion SHELL` (bash/zsh/fish) · `copilot help [TOPIC]` (billing, config, commands, environment, logging, monitoring, permissions, providers, sandbox) · `copilot init` · `copilot login` · `copilot mcp` · `copilot plugin` · `copilot plugins list|enable|disable|remove` · `copilot skill` · `copilot update` · `copilot version`

### 9.5 Slash commands principales (dentro de la sesión)

`/login` `/logout` `/model` `/models` `/plan` `/init` `/instructions` `/context` `/compact` `/clear` (`/new`, `/reset`) `/diff` `[Preview: experimental]` `/review` `/security-review` `/pr [view|create|fix|auto|automerge]` `/delegate` `/research` `/rubber-duck` `/fleet` `/agent` `/agents` (`/subagents`) `/skills` `/plugins` `/mcp` `/lsp` `/sandbox [config|status|policy|enable|disable]` `/permissions [default|assisted|allow-all|show]` (alias `/allow-all`, `/yolo`) `/add-dir` `/list-dirs` `/reset-allowed-tools` `/session(s)` `/resume` (`/continue`) `/rename` `/undo` (`/rewind`) `/share` (`/export`) `/settings` (`/config`) `/usage` `/limits` `/ide` `/remote` `/voice` `/theme` `/terminal-setup` `/statusline` `/tasks` `/env` `/copy` `/cwd` `/help` `/feedback` `/exit`
Solo en modo experimental: `/after` `/every` `/autopilot` (`/goal`) `/search` `/fork`

### 9.6 Flags principales

`-p` / `--prompt` (modo programático) · `--allow-all-tools` (env `COPILOT_ALLOW_ALL`) · `--allow-tool=TOOL` / `--deny-tool=TOOL` · `--allow-all` · `--allow-all-paths` · `--allow-all-urls` · `--allow-url` / `--deny-url` · `--yolo` · `--model=MODEL` (admite `auto`) · `--effort=LEVEL` / `--reasoning-effort=LEVEL` (low|medium|high) · `--mode=MODE` (interactive|plan|autopilot) · `--plan` · `--autopilot` · `--max-autopilot-continues` · `--continue` · `--connect[=SESSION-ID]` · `--cloud` · `--add-dir=PATH` · `--additional-mcp-config=JSON` · `--agent=AGENT` · `--available-tools` / `--excluded-tools` · `--disable-builtin-mcps` · `--disable-mcp-server` · `--add-github-mcp-tool` / `--add-github-mcp-toolset` / `--enable-all-github-mcp-tools` · `--output-format=text|json` · `--stream=on|off` · `--log-dir` · `--log-level` · `--no-custom-instructions` · `--no-auto-update` · `--no-color` · `--remote` / `--no-remote` · `--share=PATH` · `--share-gist` · `--secret-env-vars` · `--screen-reader` · `--experimental` / `--no-experimental` · `--plugin-dir` · `--config-dir` (deprecado → `COPILOT_HOME`)

Sintaxis de tool spec: `'shell(COMMAND)'`, `'write'`, `'MCP_SERVER_NAME(tool_name)'`. Los modos se ciclan con **`Shift`+`Tab`** (standard → plan → autopilot). Proveedores de modelo propios: `COPILOT_PROVIDER_BASE_URL`, `COPILOT_PROVIDER_TYPE` (openai|azure|anthropic), `COPILOT_PROVIDER_API_KEY`, `COPILOT_MODEL`.

---

## 10. MCP (Model Context Protocol) en VS Code y Copilot

### 10.1 Archivo de configuración `[GA]`

**Sigue llamándose `mcp.json`.**
> *"MCP server configuration is stored in the `mcp.json` JSON file. This file can be in your workspace (`.vscode/mcp.json`) or in your user profile."*

| Ámbito | Ruta / mecanismo |
|---|---|
| **Workspace** | **`.vscode/mcp.json`** — *"Include this file in source control to share MCP server configurations with your team."* |
| User profile | Comando **MCP: Open User Configuration** (**la ruta absoluta NO está publicada**) |
| Remoto | Comando **MCP: Open Remote User Configuration** |
| Dev Container | `devcontainer.json` → `customizations.vscode.mcp.servers` |
| **Agent Host (portable)** | workspace `.mcp.json` · user `~/.copilot/mcp-config.json` |

> ⚠️ *"The Agent Host **doesn't read `.vscode/mcp.json`** directly; for portable configuration, use a workspace `.mcp.json` or user `~/.copilot/mcp-config.json` file."*

### 10.2 Esquema JSON

Tres secciones de primer nivel: `"servers": {}`, `"inputs": []`, `"sandbox": {}`

- **stdio:** `type` ("stdio", req.), `command` (req.), `args`, `cwd`, `env`, `envFile`, `dev` (`{watch, debug}`), `sandboxEnabled`
- **HTTP/SSE:** `type` ("http"/"sse", req.), `url` (req.), `headers`, `oauth` (`clientId` req.; `enterpriseManaged` bool `[Preview]`, ligado a `mcp.enterpriseManagedAuth.idp`)
- URLs de socket Unix / named pipe: `unix:///path/to/server.sock`, `pipe:///pipe/named-pipe`
- **`inputs`:** tipos `promptString` (`description`, `default`, `password`), `pickString` (`description`, `options`, `default`), `command` (`command`, `args`); se referencian como `${input:variable-id}`
- **`sandbox`:** `filesystem.allowWrite`, `filesystem.denyRead`, `filesystem.denyWrite`, `network.allowedDomains`, `network.deniedDomains`. **Solo macOS/Linux** — *"Sandboxing is currently not available on Windows."* Cuando está en sandbox, *"tool calls from the server are auto-approved."*

### 10.3 Comandos del Command Palette (lista completa)

MCP: Add Server · MCP: Browse MCP Servers · MCP: Browse Resources · MCP: Install Server from Manifest · MCP: List Servers · MCP: Open Remote User Configuration · MCP: Open User Configuration · MCP: Open Workspace Folder MCP Configuration · MCP: Reset Cached Tools · MCP: Reset Trust · MCP: Show Installed Servers

**MCP: Add Server** ofrece destino **Workspace** o **Global**. También por línea de comandos:
`code --add-mcp "{\"name\":\"my-server\",\"command\": \"uvx\",\"args\": [\"mcp-server-fetch\"]}"`

### 10.4 Confianza y arranque

> *"When you add an MCP server to your workspace or change its configuration, you need to confirm that you **trust** the server and its capabilities before starting it."* / *"If you don't trust the MCP server, it will not be started, and chat requests will continue without using the tools."*
>
> ⚠️ *"If you start the MCP server **directly from the `mcp.json` file**, you will **not** be prompted to trust the server configuration."*

Reset: **MCP: Reset Trust**. El estado enabled/disabled *"is stored separately from the server configuration in `mcp.json`"*.

### 10.5 Settings de VS Code

- **`chat.mcp.access`** — *"Manage which MCP servers can be used in VS Code"* ✅ existe
- **`chat.mcp.discovery.enabled`** — *"Configure automatic discovery of MCP server configuration from other applications"* ✅ existe
- `chat.mcp.autostart` `[Preview: Experimental]` · `chat.mcp.serverSampling` · `chat.mcp.apps.enabled` `[Preview: Experimental]`

### 10.6 Galería / registry `[Preview]`

Vista **Extensions** con el filtro **`@mcp`** = lista de servidores MCP de la galería; clic derecho → **Install in Workspace** *"updates the `.vscode/mcp.json` file"*. También **MCP: Browse MCP Servers**.
**GitHub MCP Registry** en https://github.com/mcp — *"currently in public preview and subject to change."*

Capacidades adicionales: **Resources** (Add Context > MCP Resources) · **Prompts** (`/<MCP server>.<prompt>`) · **MCP Apps**

### 10.7 Políticas de organización/empresa que controlan MCP `[Depende del plan/política]`

**Política 1 — `MCP servers in Copilot`**
> *"Enterprises and organizations can choose to enable or disable use of MCP … with the **MCP servers in Copilot** policy. **The policy is disabled by default.** … The MCP policy **only** applies to users who have a Copilot Business or Copilot Enterprise subscription… Copilot Free, Copilot Pro, Copilot Pro+, or Copilot Max **do not** have their MCP access governed by this policy."*
>
> *"The MCP servers in Copilot policy controls use where MCP server support is **generally available (GA)**. This policy does not control access and permissions for the GitHub MCP server in third-party host applications (like Cursor, Windsurf or Claude)."*

**Política 2 — `Restrict MCP access to registry servers` + `MCP Registry URL`** `[Preview]`
- **Enterprise:** Enterprise → **AI controls** → sidebar **MCP** → **MCP servers in Copilot** = **Enabled everywhere** → **MCP Registry URL** → dropdown **Restrict MCP access to registry servers**: **Allow all** (*"No restrictions. All MCP servers can be used."*) / **Registry only** (*"Only servers from the registry may run."*)
- **Organización:** Org Settings → "Code, planning, and automation" → **Copilot** → **Policies** → sección "Features" → **MCP servers in Copilot** = **Enabled** → **MCP Registry URL (optional)** → mismo dropdown
- ⚠️ *"This feature is in public preview and is **not the recommended method** for restricting access to MCP servers. The more secure, generally available method is to define settings in your enterprise's `managed-settings.json` file."*
- Formato de URL de registry Azure API Center: `https://SERVICE-NAME.data.REGION.azure-apicenter.ms/workspaces/WORKSPACE-NAME`

**Aplicación desde VS Code (enterprise policies)** — método recomendado:
| Policy | Setting | Valores / notas |
|---|---|---|
| `ChatMCP` | `chat.mcp.access` | `all` / `registry` / `none` |
| `McpGalleryServiceUrl` | — | registry personalizado |
| `ChatAllowedMcpServers` | `chat.mcp.allowedServers` | min. VS Code 1.130 |
| `ChatDeniedMcpServers` | `chat.mcp.deniedServers` | min. VS Code 1.130 |
| `ChatAllowManagedMcpServersOnly` | `chat.mcp.allowManagedServersOnly` | min. VS Code 1.132 |

Claves de `managed-settings.json`: `allowedMcpServers`, `deniedMcpServers`, `allowManagedMcpServersOnly`. *"A deny entry takes precedence over an allow entry."*

**Ubicaciones de `managed-settings.json`:**
- Windows: `%ProgramFiles%\GitHubCopilot\managed-settings.json`
- macOS: `/Library/Application Support/GitHubCopilot/managed-settings.json`
- Linux: `/etc/github-copilot/managed-settings.json`

---

## 11. Selección de modelos y catálogo disponible hoy

### 11.1 Catálogo completo (página "Supported AI models in GitHub Copilot")

| Modelo | Proveedor | Estado |
|---|---|---|
| GPT-5 mini | OpenAI | `[GA]` |
| GPT-5.3-Codex | OpenAI | `[GA]` |
| GPT-5.4 | OpenAI | `[GA]` |
| GPT-5.4 mini | OpenAI | `[GA]` |
| GPT-5.4 nano | OpenAI | `[GA]` — solo en la extensión Codex de VS Code (Pro+); **no en Copilot Chat** |
| GPT-5.5 | OpenAI | `[GA]` |
| GPT-5.6 Luna / Sol / Terra | OpenAI | `[GA]` |
| Claude Fable 5 | Anthropic | `[GA]` — requiere habilitación explícita por Business/Enterprise |
| Claude Haiku 4.5 | Anthropic | `[GA]` |
| Claude Opus 4.5 / 4.6 / 4.7 / 4.8 | Anthropic | `[GA]` |
| Claude Opus 4.8 (fast mode) (preview) | Anthropic | `[GA]` (el "preview" está en el nombre) |
| Claude Opus 5 | Anthropic | `[GA]` |
| Claude Sonnet 4.5 / 4.6 / 5 | Anthropic | `[GA]` |
| **Gemini 3.1 Pro** | Google | **`[Preview]` (public preview)** |
| Gemini 3.5 / 3.6 / 3.7 Flash | Google | `[GA]` |
| MAI-Code-1-Flash / MAI-Code-1.1-Flash | Microsoft | `[GA]` |
| Raptor mini (GPT-5 mini fine-tuned) | — | `[GA]` |
| Kimi K2.7 Code / Kimi K3 | Moonshot AI | `[GA]` (open weight) |
| Grok 4.5 / Grok 4.6 | xAI | `[GA]` |

**Retiros anunciados para 2026-09-01:** Claude Opus 4.5, Claude Opus 4.6, Claude Sonnet 4.5, Claude Sonnet 4.6, Gemini 3.1 Pro, Raptor mini.

**Utility models** (no seleccionables en el picker): GPT-4o mini, GPT-4o, GPT-4.1, GPT-5.4 nano.

**Modelos de capacidad extendida** (1M de contexto y/o reasoning configurable): Claude Sonnet 4.6, Claude Opus 4.6/4.7/4.8, Claude Opus 5, Claude Sonnet 5, Claude Opus 4.8 (fast mode), Claude Fable 5, GPT-5.3-Codex, GPT-5.4, GPT-5.5, GPT-5.6 Luna/Sol/Terra, Kimi K3.
> *"The 1 million token context window is available in **Visual Studio Code and Copilot CLI only**. Configurable reasoning levels are available in **Visual Studio Code, Copilot CLI, and Copilot cloud agent**."*

### 11.2 Auto model selection `[GA]`

Modelos incluidos en **Auto**: GPT-5 mini, GPT-5.3-Codex, GPT-5.4, GPT-5.4 mini, Claude Haiku 4.5, Claude Sonnet 4.6, MAI-Code-1-Flash, MAI-Code-1.1-Flash, Raptor mini.
> *"Copilot Student and Copilot Free users have access to models **through auto model selection only**."*
> *"To use auto model selection, select **Auto** from the model picker in chat. You can see which model is used for generating a response by hovering over the chat response."*

**Restricted Mode:** *"the chat model picker only shows **Auto**. Trust the workspace to restore the full model list."*

### 11.3 Cómo se cambia el modelo (VS Code) `[GA]`

- **Model picker** en el campo de entrada del chat: *"Use the language model picker in the chat input field to change the model for chat conversations and code editing."*
- Atajo (del cheat sheet): **`Ctrl+Alt+.`** (macOS `⌥⌘.`)
- Gestionar la lista: model picker → **Manage Language Models** (icono de engranaje), o Command Palette → **Chat: Manage Language Models**
- Filtros: `@provider:"OpenAI"`, `@capability:tools|vision|agent`, `@visible:true/false`. Soporta pinning y visibilidad con el icono de ojo
- **Thinking Effort:** seleccionar un modelo de razonamiento → flecha **>** junto al nombre → submenú **Thinking Effort** (la etiqueta pasa a ser, p.ej., `Claude Sonnet 4.6 · High`)
- Modelo de completions (distinto del de chat): menú Chat → **Configure Inline Suggestions...** → **Change Completions Model...**

### 11.4 Control administrativo `[Depende del plan/política]`

> *"If you're an organization or enterprise owner, you can enable or restrict access to specific models for your members."*

Políticas: **Configure allowed models**, **Enable custom models**, **GitHub Models (una política por modelo)**, **Bring Your Own Language Model Key in VS Code**.

No elegibles para habilitación por defecto: modelos explícitamente deshabilitados, modelos pre-GA, **open weight** (DeepSeek, Kimi K2.7 Code, Kimi K3), modelos no cubiertos por el acuerdo de retención de datos de GitHub (**Claude Fable 5**), y restricciones de modelos data-resident / FedRAMP.

---

## 12. Content exclusions y políticas de organización/empresa

### 12.1 Content exclusion `[GA]` en IDEs · `[Preview]` en GitHub.com y Mobile

**Plan requerido:** *"Organizations with a **Copilot Business or Copilot Enterprise** plan."* Roles: *"Repository administrators, organization owners, and enterprise owners can manage content exclusion settings. People with the 'Maintain' role for a repository can **view, but not edit**."*

**Rutas exactas de configuración:**
- **Repo:** Repo → **Settings** → sidebar "Code, planning, and automation" → **Copilot** → **Content exclusion** → campo "Paths to exclude in this repository"
- **Organización:** Organizations → org → **Settings** → sidebar **Copilot** → **Content exclusion** → campo "Repositories and paths to exclude"
- **Enterprise:** Enterprise → **AI controls** → sidebar **Copilot** → **Content exclusion**
- REST API: `/rest/copilot/copilot-content-exclusion-management`

**Qué hace (verbatim):**
- *"Inline suggestions will not be available in the affected files."*
- *"The content in affected files will not inform inline suggestions in other files."*
- *"The content in affected files will not inform GitHub Copilot Chat's responses."*
- *"Affected files will not be reviewed in a Copilot code review."*

**Sintaxis YAML a nivel repo** (formato `- "/PATH"`, uno por línea, `#` para comentarios, fnmatch, insensible a mayúsculas):
```yaml
# Ignore the `/src/some-dir/kernel.rs` file in this repository.
- "/src/some-dir/kernel.rs"
# Ignore files called `secrets.json` anywhere in this repository.
- "secrets.json"
# Ignore all files whose names begin with `secret` anywhere in this repository.
- "secret*"
# Ignore files whose names end with `.cfg` anywhere in this repository.
- "*.cfg"
# Ignore all files in or below the `/scripts` directory of this repository.
- "/scripts/**"
```

**Sintaxis YAML a nivel org/enterprise** (`REPOSITORY-REFERENCE:` seguido de rutas indentadas; `"*":` = todas las raíces):
```yaml
"*":
  - "**/.env"
octo-repo:
  - "/src/some-dir/kernel.rs"
https://github.com/primer/react.git:
  - "secrets.json"
  - "/src/**/temp.rb"
git@github.com:*/copilot:
  - "/__tests__/**"
  - "/scripts/*"
git@gitlab.com:gitlab-org/gitlab-runner.git:
  - "/main_test.go"
  - "{server,session}*"
  - "*.m[dk]"
  - "**/package?/*"
  - "**/security/**"
```
Protocolos de referencia aceptados: `http[s]://host.xz[:port]/path/to/repo.git/`, `git://host.xz[:port]/path/to/repo.git/`, `[user@]host.xz:path/to/repo.git/`, `ssh://[user@]host.xz[:port]/path/to/repo.git/`

> *"Rules set at the enterprise level apply to all Copilot users in the enterprise, whereas the rules set by organization owners only apply to users who are assigned a Copilot seat by that organization."*

**⚠️ Limitaciones documentadas (verbatim) — CRÍTICAS para gobernanza:**
- *"**GitHub Copilot CLI and Agent mode in Copilot Chat in IDEs do not support content exclusion.**"*
- *"Content exclusion is currently **not supported in Edit and Agent modes** of Copilot Chat in Visual Studio Code and other editors."*
- *"Content exclusion is **in public preview** on the GitHub website and in GitHub Mobile and is subject to change."*
- *"It's possible that Copilot may use **semantic information** from an excluded file if the information is provided by the IDE indirectly. Examples: type information and hover-over definitions for symbols used in code, as well as general project properties such as build configuration information."*
- *"Currently, content exclusions **do not apply to symbolic links (symlinks) and repositories located on remote filesystems**."*
- *"The client sends the current repository URL to the GitHub server so that the server can return the correct policy… These URLs are not logged anywhere."*

**Propagación:** *"it can take up to **30 minutes** to take effect in IDEs where the settings are already loaded."* Recarga manual: JetBrains / Visual Studio = reiniciar la app; **VS Code = Command Palette → Developer: Reload Window**; Vim/Neovim = se recarga al abrir cada archivo.
**Test recomendado:** abrir un archivo excluido, adjuntarlo como contexto y pedir `explain this file`.

Superficies listadas en la tabla de soporte: Visual Studio · Visual Studio Code · JetBrains · Vim/Neovim · Xcode · Eclipse · Azure Data Studio · GitHub website · GitHub Mobile. Además: *"Content exclusions also apply to Copilot code review on the GitHub website."*

### 12.2 Lista completa de políticas de organización y empresa `[Depende del plan/política]`

**Navegación:**
- **Enterprise:** Enterprise → **AI controls** → sidebar: **Agents** · **Copilot** · **MCP**. *"For policies with a **dropdown menu**, select the menu and click an enforcement option. For policies with a **toggle**, click the toggle."*
- **Organización:** Org Settings → "Code, planning, and automation" → **Copilot** → **Policies** / **Models**

**Nombres exactos de políticas (compilados de las páginas de referencia):**

| Política | Notas |
|---|---|
| Copilot Chat in the IDE | |
| **Copilot Chat agent mode in the IDE** | control directo de agent mode |
| Copilot Chat in GitHub Mobile | |
| Copilot in GitHub.com | activa sub-opciones org: "Opt in to user feedback collection", "Opt in to preview features" |
| Copilot in GitHub Desktop | |
| **Copilot CLI** | requerida para que la CLI funcione con licencia de org |
| **Copilot cloud agent** | **deshabilitada por defecto** en Business/Enterprise. Enterprise puede elegir organizaciones concretas |
| **Copilot code review** | |
| Allow members without a Copilot license to use Copilot code review in GitHub.com | deshabilitada por defecto; "most restrictive" |
| **MCP servers in Copilot** | **deshabilitada por defecto** |
| **Restrict MCP access to registry servers** | `[Preview]` |
| MCP servers on GitHub.com | requerida junto a "Copilot cloud agent" |
| **Suggestions matching public code** (privacy policy) | **Blocked por defecto para usuarios Copilot Business** |
| **Content exclusion** | |
| **Semantic indexing for non-GitHub repositories** | (esto es la "indexación"; ver advertencias) |
| **Copilot Memory** | `[Preview]` |
| **Copilot Metrics API** | "most restrictive" en conflictos |
| **Copilot usage metrics** | requerida para los dashboards |
| Copilot can search the web | |
| Copilot-generated commit messages | |
| Editor preview features | |
| **Configure allowed models** / **Enable custom models** / GitHub Models (una por modelo) | |
| **Bring Your Own Language Model Key in VS Code** | en https://github.com/settings/copilot/features |
| Spark / GitHub Spark | `[Preview]` |
| AI credits paid usage | |
| Policies for enterprise-assigned users | determina si las políticas "Let organizations decide" default a enabled o disabled |
| Partner agents: **Anthropic Claude**, **OpenAI Codex** | Org Settings → Copilot → **Cloud agent** → "Partner agents" `[Preview]` |
| "agent apps" (política única) | *"Agent apps are not enabled here. They are controlled separately by a single 'agent apps' policy."* |
| Allow automations | requiere repos private/internal |

**Semántica de aplicación (verbatim):**
> *"In an enterprise, policies are set at the enterprise level first. For most policies, enterprise administrators can either explicitly enable or disable a policy, or **let organizations decide**. As an exception, for Copilot cloud agent, enterprises can select exactly which organizations receive access."*

- **Conflictos multi-organización:** normalmente gana la **menos restrictiva**; gana la **más restrictiva** para: Copilot Metrics API, Semantic indexing for non-GitHub repositories, Suggestions matching public code, Allow members without a Copilot license to use Copilot code review in GitHub.com.
- **Conflictos multi-empresa:** gana la **más restrictiva**, con excepciones "AI credit paid usage" y "GitHub Spark".
- **Roles:** enterprise owners o permiso de rol custom **"Manage enterprise AI controls"**; org owners o permisos granulares custom.

**Qué puede desactivar un administrador — respuesta directa a la pregunta del brief:** ✅ agentes (Copilot cloud agent, Copilot Chat agent mode in the IDE, agentic features vía "Block agentic features in your enterprise") · ✅ MCP (MCP servers in Copilot; restricción por registry; allow/deny por servidor vía `managed-settings.json`) · ✅ modelos (Configure allowed models, por-modelo, BYOK) · ✅ indexación (Semantic indexing for non-GitHub repositories) · ✅ sugerencias que coinciden con código público (Suggestions matching public code) · ✅ Copilot CLI · ✅ code review · ✅ métricas · ✅ Copilot Memory · ✅ plugins y marketplaces (vía `managed-settings.json`, ver §17)

---

## 13. Privacidad y uso de datos

### 13.1 Business / Enterprise: el código NO se usa para entrenar `[GA]`

Verbatim, GitHub Docs:
> *"**GitHub does not use Copilot Business or Copilot Enterprise customer data to train AI models.** Copilot Business and Copilot Enterprise customers' data is protected under GitHub's Data Protection Agreement, which prohibits such use without customer authorization."*
>
> *"If you don't see 'Allow GitHub to use my data for AI model training', verify that you are not signed in with an account that has a Copilot Business or Copilot Enterprise license. Copilot Business and Copilot Enterprise customers' data is protected under GitHub's Data Protection Agreement, so the setting is not displayed for these plans."*

### 13.2 ⚠️ CAMBIO CRÍTICO para planes individuales (abril 2026) `[GA]`

> *"**Starting on April 24, 2026**, if you have a **Copilot Free**, **Copilot Pro**, **Copilot Pro+**, or **Copilot Max** plan, GitHub **may use your interactions** with GitHub features and services—including **inputs, outputs, code snippets, and associated context**—to train and improve AI models… You can **opt-out** from allowing your data to be used for training in your personal settings for GitHub Copilot."*

Ruta de opt-out individual: Copilot settings → **"Allow GitHub to use my data for AI model training"** → **Disabled**.

**Implicación para el curso:** si algún participante usa su cuenta personal Pro/Pro+ en lugar del asiento Enterprise, **sus datos sí pueden usarse para entrenamiento por defecto**. Regla de gobernanza: usar siempre la cuenta corporativa con asiento Enterprise.

### 13.3 Compromisos de los proveedores de modelos `[GA]`

Página "Hosting of models for GitHub Copilot":
- *"OpenAI makes the following data commitment: We [OpenAI] do not train models on customer business data."*
- *"GitHub maintains a **zero data retention agreement** with OpenAI."*
- *"These models are hosted by Amazon Web Services, Anthropic PBC, and Google Cloud Platform. **GitHub has provider agreements in place to ensure data is not used for training.**"*
- *"Gemini doesn't use your prompts, or its responses, as data to train its models."*
- xAI / Grok 4.5: el contenido *"will not be … Used for model training"*.
- **Excepción a vigilar:** **Claude Fable 5** — *"Anthropic retains data, including prompts and outputs, to operate safety classifiers… Enterprise and business users need to enable the Claude Fable 5 model to make it available for your organization."* Es el único modelo listado como **no cubierto** por el acuerdo de retención de datos de GitHub.

### 13.4 Code referencing / duplicate detection / public code matching `[GA]`

**Tres nombres distintos que no hay que confundir:**
| Contexto | Nombre exacto |
|---|---|
| Concepto / feature | **GitHub Copilot code referencing** |
| Ajuste / política | **Suggestions matching public code** — opciones **Allow** / **Block** |
| Fila en la tabla de planes | **Block suggestions matching public code** |

Cómo funciona (verbatim):
> *"GitHub Copilot checks suggestions for matches with publicly available code. Any matches are discarded or suggested with a code reference."*
> *"Copilot code referencing compares potential code suggestions and the surrounding code of about **150 characters** against an index of **all public repositories on GitHub.com**."*
> *"**Code in private GitHub repositories, or code outside of GitHub, is not included** in the search process."*
> *"Code referencing for inline suggestions only occurs for matches of **accepted** Copilot suggestions. **Code you have written, and Copilot suggestions you have altered, are not checked** for matches to public code."*
> *"Typically, matches to public code occur in **less than one percent** of Copilot suggestions."*
> *"The search index is refreshed **every few months**."*
> *"References to matching code are currently available in **JetBrains IDEs, Visual Studio, Visual Studio Code, Copilot cloud agent, and on the GitHub website**."*

Comportamiento en modo **Block**:
> *"If you choose to block suggestions matching public code, in most GitHub Copilot products, GitHub Copilot checks code suggestions with their surrounding code of about 150 characters against public code on GitHub. If there is a match, or a near match, **the suggestion is not shown to you**."*

Quién lo controla:
> *"If you are a member of an organization on GitHub Enterprise Cloud who has been assigned a GitHub Copilot seat through your organization, **you will not be able to configure suggestions matching public code in your personal account settings**. Your setting … will be **inherited from your organization or enterprise**."*

Estado por defecto (verbatim):
> *"**Suggestions matching public code is set to Blocked by default for Copilot Business users.** You can change this setting in the **Privacy** section of the Copilot policy page."*

---

## 14. Copilot en Visual Studio (la IDE) — lo básico

### 14.1 Cómo se distribuye `[GA]`

> *"If you use Visual Studio version **17.10 or later**, GitHub Copilot Chat is included in the **unified GitHub Copilot experience**… Copilot Chat is **installed by default with all workloads**, unless you choose to exclude it during installation."*

Es un **componente opcional del Visual Studio Installer, seleccionado por defecto** — **no** una extensión del marketplace. En **17.9 o anterior** se gestionaba con el diálogo **Manage Extensions**.
> *"GitHub Copilot **isn't included in Visual Studio Subscriptions**. Instead, it's a separate subscription managed by GitHub."*

### 14.2 Versión mínima — ⚠️ la documentación es inconsistente; citar ambas

- Prerrequisito de chat: **VS 2022 17.8**
- Experiencia unificada / icono de estado / chat context: **17.10+**
- Páginas de completions y agents (baseline actual): **"Visual Studio 2026 or Visual Studio 2022 version 17.14"**
- Copilot Edits: **17.13+** · Agent mode: **17.14+** · Code review: **17.14+** · Custom agents: **VS 2026 18.4+**

El moniker por defecto de la doc es ahora `visualstudio` (= Visual Studio 2026), con variante `vs-2022`. **VS 2019 está excluido de toda la documentación.**

### 14.3 Iniciar sesión (cuatro rutas documentadas)

1. Al primer arranque (17.13+; 17.14+ ofrece **Activate Copilot Free**)
2. **Badge de GitHub Copilot en la esquina superior derecha** → **Sign in to use Copilot** / **Open Chat Window**
3. Tarjeta de perfil → **Sign in** → **GitHub**
4. **File > Account Settings…** → **All Accounts** → **+ Add** → **GitHub**

Copilot solo funciona si la cuenta de GitHub **activa** es la que tiene la suscripción. **`Ctrl+\`** abre la ventana de chat.

### 14.4 Modos de chat en Visual Studio: **Ask, Plan, Agent** `[GA]`

⚠️ **Distinto de VS Code.** El FAQ de la doc dice literalmente: *"I don't see **ask, plan, or agent** options in the Copilot Chat window."*

- **No existe un modo "Edit"** en el desplegable: Copilot Edits es un tipo de hilo aparte (**View > GitHub Copilot Chat → Create new Edit session**), y agent mode *"is an evolution of Copilot Edits"*.
- Agent mode requiere la opción **Enable Agent mode in the chat pane** en **Tools > Options > GitHub > Copilot > Copilot Chat**.
- **Agentes integrados exclusivos de VS**, invocados con `@`: **`@debugger`**, **`@git`**, **`@profiler`**, **`@test`**, **`@modernize`** (solo .NET/C++), más el agente **Plan** (guarda markdown en `.copilot/plans/`).

> **Para un equipo C#/.NET 8: `@modernize` solo existe en Visual Studio** (.NET y C++), no en VS Code. Es un argumento válido para que quien haga modernización de legacy .NET use VS.

### 14.5 Aceptar completions en Visual Studio `[GA]`

| Acción | Atajo | Command |
|---|---|---|
| Aceptar sugerencia | **`Tab`** (rebindable a Right Arrow) | `Edit.AcceptSuggestion` |
| Aceptar palabra siguiente | **`Ctrl`+`Right arrow`** | `Edit.AcceptNextWordinSuggestion` |
| Aceptar línea siguiente | **`Ctrl`+`Down arrow`** | `Edit.AcceptNextLineinSuggestion` |
| Rechazar | `Esc` | |
| Ciclar sugerencias | `Alt`+`.` / `Alt`+`,` | |

Scope de los comandos: **Inline Suggestion Active**.
> *"Visual Studio prioritizes IntelliSense over Copilot inline completions"* por defecto.

### 14.6 Slash commands en Visual Studio (VS 2026)

**`/doc` · `/explain` · `/fix` · `/generate` · `/generateInstructions` · `/help` · `/optimize` · `/savePrompt` · `/tests`** — más **`/compact`**.
El inline chat soporta todos excepto `/generateInstructions` y `/savePrompt`. La variante VS 2022 omite esos dos (7 comandos).

> Es **`/tests`**, no `/test`. **Ninguno** de los comandos exclusivos de VS Code (`/new`, `/setupTests`, `/startDebugging`, `/clear`, `/fixTestFailure`) existe en Visual Studio.

### 14.7 Referencias de contexto en Visual Studio

`#<filename>` (con rangos, p.ej. `#MyFile.cs: 66-72`) · `#<symbol>` (17.11+) · **`@workspace`** (la solución) · `@github` (**solo Copilot Enterprise**, 17.11+) · `#changes` (17.14+, requiere Git preview features) · `#commit:` · `#output` · `#prompt:` · `@<agent-name>`

> ⚠️ **`#solution` NO está documentado — usar `@workspace`.** Y **no existen** `#codebase`, `#file:`, `#selection` ni `#terminalSelection` en Visual Studio. Es decir: **`@workspace` sigue vivo en VS pero desapareció de VS Code.**

### 14.8 Custom instructions en Visual Studio — **SÍ se respetan** `[GA]`

- `.github/copilot-instructions.md` (raíz del repo) — **requiere el checkbox** *"Enable custom instructions to be loaded from .github/copilot-instructions.md files and added to requests"* en **Tools > Options > All Settings > GitHub > Copilot > Copilot Chat** (VS 2026) / **Tools > Options > GitHub > Copilot > Copilot Chat** (VS 2022)
- `.github/instructions/*.instructions.md` (frontmatter `applyTo` + `description` opcional)
- `%USERPROFILE%/copilot-instructions.md` (nivel usuario)
- `.github/prompts/*.prompt.md`
- `.github/agents/*.agent.md` (VS 2026 18.4+)

Las instrucciones no se muestran en la vista de chat, pero aparecen en la lista **References** de la respuesta.

> ⚠️ **`AGENTS.md` no está documentado como soportado en Visual Studio** — no lo afirmes en el curso.

---

## 15. Métricas de adopción

### 15.1 Cinco canales de entrega `[GA]`

> *"The Copilot usage metrics APIs… / The **Copilot usage metrics dashboard**, which visualizes 28-day usage trends… / The **code generation dashboard**, which breaks down how code is being generated by users and agents… / The **Copilot impact dashboard**, which groups users into adoption cohorts and connects that adoption to pull request output. / The **Copilot usage metrics NDJSON export**, which offers raw data for custom BI tools or long-term storage."*

### 15.2 Endpoints REST exactos

Header requerido: **`X-GitHub-Api-Version: 2026-03-10`**

**Enterprise:**
```http
GET /enterprises/{enterprise}/copilot/metrics/reports/enterprise-1-day?day=DAY
GET /enterprises/{enterprise}/copilot/metrics/reports/enterprise-28-day/latest
GET /enterprises/{enterprise}/copilot/metrics/reports/repos-1-day?day=DAY
GET /enterprises/{enterprise}/copilot/metrics/reports/user-teams-1-day?day=DAY
GET /enterprises/{enterprise}/copilot/metrics/reports/users-1-day?day=DAY
GET /enterprises/{enterprise}/copilot/metrics/reports/users-28-day/latest
```
**Organización:**
```http
GET /orgs/{org}/copilot/metrics/reports/organization-1-day?day=DAY
GET /orgs/{org}/copilot/metrics/reports/organization-28-day/latest
GET /orgs/{org}/copilot/metrics/reports/repos-1-day?day=DAY
GET /orgs/{org}/copilot/metrics/reports/user-teams-1-day?day=DAY
GET /orgs/{org}/copilot/metrics/reports/users-1-day?day=DAY
GET /orgs/{org}/copilot/metrics/reports/users-28-day/latest
```

**Permisos (verbatim):**
- Enterprise: *"Enterprise owners, billing managers, and authorized users with fine-grained **'View Enterprise Copilot Metrics'** permission… OAuth app tokens and personal access tokens (classic) need either the `manage_billing:copilot` or `read:enterprise` scopes."*
- Organización: *"Organization owners and authorized users with fine-grained **'View Organization Copilot Metrics'** permission… need the `read:org` scope."*

**Recurso separado y no intercambiable:** `REST API endpoints for Copilot user management` — *"the source of truth for license and seat information"*, expone `last_activity_at`. *"**License and seat management data are not included in Copilot usage metrics reports.**"*

### 15.3 Dashboards `[GA]` `[Depende del plan/política]`

Los tres se acceden igual: **Enterprise → pestaña Insights → sidebar izquierdo → Copilot usage / Code generation / Copilot impact**

**"Who can use this feature?" (idéntico en los tres):**
> *"Enterprise owners, organization administrators, billing managers, and people with an enterprise custom role with the **'View Enterprise Copilot Metrics'** permission."*

**Política prerrequisito (verbatim):** *"To access Copilot usage metrics, the **'Copilot usage metrics' policy must be enabled**."*
Visibilidad a nivel org: crear un **organization custom role** con el permiso *"View organization Copilot metrics"*.

### 15.4 Latencia — ⚠️ dos cifras oficiales distintas

- Página de conceptos: *"You can expect data to be available within **two full days**. This means that data for a given day is processed and made available within two full UTC days after that day closes."*
- Página del dashboard: *"Data may appear up to **three full UTC days** behind the current date."*

Ambas son oficiales. En el curso, citar el rango "2–3 días UTC".

### 15.5 Versiones mínimas de IDE/extensión para ser contabilizado

| IDE | Versión IDE / extensión |
|---|---|
| Eclipse | 4.31 / 0.9.3.202507240902 |
| JetBrains-IntelliJ | 2024.2.6 / 1.5.52-241 |
| **Visual Studio** | **17.14.13 / 18.0.471.29466** |
| **VS Code** | **1.107.1 / 0.35.3** |
| Xcode | 13.2.1 / 0.40.0 |

**Superficies excluidas de las métricas (verbatim):** *"Copilot Chat on GitHub.com"* y *"GitHub Mobile"*.

### 15.6 Cohortes de adopción (impact dashboard) `[GA]`

Nombres exactos de fase: **Passive users** (etiqueta de datos `No Cohort`) · **Phase 1: Code first** · **Phase 2: Agent first** · **Phase 3: Multi-agent**
Umbral de actividad: *"at least two active days out of the trailing 28-day window."*
**Adoption multiplier** = PRs mergeados promedio de usuarios comprometidos (Phase 1/2/3) ÷ Passive users.

Dato de calendario: *"Organization-level Copilot analytics are available **starting December 12, 2025**. This is the first date for which organization-level reports are provided."*

---

## 16. Extensiones de VS Code involucradas

### 16.1 ⚠️ CAMBIO ESTRUCTURAL: Copilot Chat es ahora **built-in** en VS Code `[GA]`

"Visual Studio Code 1.116" (release del **15 de abril de 2026**), sección **"GitHub Copilot is now built-in"**:
> *"**GitHub Copilot Chat is now a built-in extension in VS Code.** New users no longer need to install any extension… Copilot is available out of the box as part of the standard VS Code installation."*

Versión estable en el momento de la consulta: **1.133** (12 de agosto de 2026).

La página **"Set up GitHub Copilot in VS Code"** ya **no contiene ningún paso de instalación de extensión**: el flujo es solo sign-in → hover del icono de Copilot en la Status Bar → **Use AI Features**.

GitHub Docs, "Installing the GitHub Copilot extension in your environment":
> *"When you set up GitHub Copilot in Visual Studio Code for the first time, **the required extensions are installed automatically. You don't need to download or install them manually.**"*

### 16.2 Identificadores exactos (sin renombrados)

| Display name | Extension ID | Estado |
|---|---|---|
| **GitHub Copilot** | **`GitHub.copilot`** | ⚠️ **en ruta de deprecación** |
| **GitHub Copilot Chat** | **`GitHub.copilot-chat`** | **built-in desde VS Code 1.116** |
| **GitHub Pull Requests and Issues** | **`GitHub.vscode-pull-request-github`** | **instalación manual, sigue siendo separada** |

**Deprecación de `GitHub.copilot`** — blog oficial "Open Source AI Editor: Second Milestone" (6 de noviembre de 2025):
> *"We are working towards providing all Copilot functionality in a **single VS Code extension: Copilot Chat**"* y *"the **GitHub Copilot extension will be deprecated by early 2026**, which means it will be removed from the VS Code Marketplace."*

### 16.3 Guía práctica para el curso

- ❌ **NO** instruir a instalar `GitHub.copilot` ni `GitHub.copilot-chat` — son innecesarios en VS Code ≥ 1.116.
- ✅ **SÍ** instruir a instalar **`GitHub.vscode-pull-request-github`** — sigue siendo una instalación manual separada.
- ⚠️ "Built-in extension" ≠ core: la extensión de chat sigue siendo instalable por separado, y las instalaciones existentes no se ven afectadas.
- Control empresarial de extensiones: setting **`extensions.allowed`** (ver la página "Manage extensions in enterprise environments").

---

## 17. Agent Plugins 1.0 (changelog de agosto de 2026)

### 17.1 El changelog exacto

- **Título:** *"Agent Plugins 1.0 in VS Code, Copilot CLI, and the Copilot app"*
- **URL:** https://github.blog/changelog/2026-08-12-agent-plugins-1-0-in-vs-code-copilot-cli-and-the-copilot-app/
- **Fecha:** **12 de agosto de 2026** (etiqueta "Release")
- **Fecha de publicación de la spec:** *"We published Agent Plugins 1.0 on **August 6** with AWS, Anysphere, Microsoft, OpenAI, and Vercel. **Google also joined as a core maintainer** on the same day."*

### 17.2 Qué es `[GA]`

Un **estándar abierto, gobernado de forma independiente de cualquier proveedor único**, que *"packages **agent skills and MCP servers** into one installable plugin."* Solo esos **dos tipos de componente son portables** entre clientes.

- Sitio del estándar: https://agent-plugins.org/
- Spec: https://github.com/agentplugins/agent-plugins-spec/blob/main/spec/1.0.0.md
- Ejemplo/migración: https://github.com/agentplugins/agent-plugins-example

### 17.3 Estado: **GA confirmado**

> *"Support is **generally available** in **VS Code, Copilot CLI, the GitHub Copilot SDK, and the GitHub Copilot app**, on **all Copilot plans**."*

Sin etiquetas de preview. *"Existing non-1.0 Copilot plugins remain supported, with **no migration required**."*

### 17.4 Estructura de archivos

**No existe carpeta `.copilot/plugins`.** Un plugin es un **directorio con `plugin.json` en su raíz**.

Manifiesto Agent Plugins 1.0:
```json
{
  "$schema": "https://agent-plugins.org/schemas/1.0.0/plugin.schema.json",
  "name": "my-dev-tools",
  "description": "React development utilities",
  "version": "1.2.0"
}
```
Requeridos: `$schema`, `name`. Opcionales: `version`, `description`, `author` (`name`/`email`/`url`), `homepage`, `repository`, `license`, `keywords`, `extensions` (datos específicos de cliente, con clave de dominio inverso).

**Detección de formato:**

| Formato | Ruta del manifiesto |
|---|---|
| **Agent Plugins 1.0** | `plugin.json` con `$schema` = `https://agent-plugins.org/schemas/1.0.0/plugin.schema.json` |
| Copilot | `plugin.json` |
| Claude | `.claude-plugin/plugin.json` |
| Legacy OpenPlugin | `.plugin/plugin.json` |

Layout en formato Copilot (verbatim, doc de VS Code):
```text
my-testing-plugin/
  plugin.json
  skills/test-runner/SKILL.md
  skills/test-runner/run-tests.sh
  agents/test-reviewer.agent.md
  hooks/hooks.json
  scripts/validate-tests.sh
  .mcp.json
```
Layout de GitHub Docs (añade LSP):
```text
my-plugin/
├── plugin.json           # Required manifest
├── agents/helper.agent.md
├── skills/deploy/SKILL.md
├── hooks.json
├── .mcp.json
└── lsp.json
```
Ubicaciones de componentes: agents = `*.agent.md` en `agents/` · skills = subdirectorios de `skills/`, cada uno con `SKILL.md` · hooks = `hooks.json` en la raíz o en `hooks/` · MCP = `.mcp.json` en la raíz o `mcp.json` en `.github/` · LSP = `lsp.json` en la raíz o en `.github/`

**Diferencias por formato:**
- **MCP:** Agent Plugins 1.0 usa **`mcp.json`** en la raíz del plugin (formato portable); Copilot/Claude usan **`.mcp.json`** con un objeto de primer nivel `mcpServers`.
- **Hooks:** Claude = `hooks/hooks.json`; Copilot = `hooks.json` en la raíz.
- **Tokens de ruta:** Agent Plugins 1.0 = **`${PLUGIN_ROOT}`** (+ **`${PLUGIN_DATA}`** para estado escribible que persiste entre actualizaciones); Claude = `${CLAUDE_PLUGIN_ROOT}`; Copilot = cualquiera de los dos.

**Marketplace:** manifiesto **`marketplace.json`**, con un array `plugins` cuyas entradas llevan name, description, version y path al directorio del plugin. Alojable en GitHub.com, otro host Git, o un filesystem local/compartido.
Marketplaces registrados por defecto: https://github.com/github/copilot-plugins y https://github.com/github/awesome-copilot
Listados pero **no** por defecto: https://github.com/anthropics/claude-code, https://github.com/claudeforge/marketplace

### 17.5 Instalación y descubrimiento — comandos exactos

**Copilot CLI:** `copilot plugin install`, o el slash command `/plugin install` (**es `install`, no `add`**). Alternativa declarativa: añadir a `enabledPlugins` en `~/.copilot/settings.json` (usuario) o `.github/copilot/settings.json` (repo).

**Copilot cloud agent:** **solo declarativo** — `enabledPlugins` en `.github/copilot/settings.json`; marketplaces no registrados vía `extraKnownMarketplaces` en el mismo archivo.

**VS Code:** vista **Extensions** con el filtro **`@agentPlugins`** (y `@agentPlugins @recommended` para recomendaciones del workspace). Setting de habilitación: **`chat.plugins.enabled`**. Plugins locales desempaquetados: **`chat.pluginLocations`** (rutas → `true`/`false`). Recomendaciones de workspace en `.claude/settings.json` o `.github/copilot/settings.json` con `extraKnownMarketplaces` y `enabledPlugins`. Actualizaciones: **Extensions: Check for Extension Updates**, o automáticas cada 24 h si `extensions.autoUpdate` está activo (**los plugins de origen npm/PyPI nunca se auto-actualizan**).

### 17.6 Controles empresariales `[Depende del plan/política]` — Copilot Business y Enterprise

En **`managed-settings.json`**:
| Clave | Función |
|---|---|
| `enabledPlugins` | instalar automáticamente o bloquear plugins específicos |
| `extraKnownMarketplaces` | añadir marketplaces disponibles para los desarrolladores |
| `strictKnownMarketplaces` | restringir la instalación a marketplaces gestionados |

> *"Enterprise values establish a baseline, and plugin and marketplace settings combine **additively** with approved team-specific overrides."*
> *"**No separate Agent Plugins policy is required.**"*

Se recomienda combinarlo con allowlists de MCP (aprobar/bloquear servidores por URL, comando o nombre).

La página https://docs.github.com/en/copilot/concepts/agents/about-enterprise-plugin-standards tiene como H1 real **"About enterprise-managed plugin standards"**, define dos áreas de capacidad (**"Known marketplaces"**, **"Default-enabled plugins"**), indica que el cliente consulta `managed-settings.json` **en el momento de la autenticación**, y delega todos los nombres de campo a `/copilot/reference/enterprise-managed-settings-reference#supported-keys`. **No contiene ejemplo JSON ni nombres de plan.**

### 17.7 ⚠️ Contradicción entre fuentes oficiales

El **changelog** afirma que los archivos específicos de Copilot van en un directorio **`com.github.copilot/`** y que *"custom agents, commands, rules, and hooks load from there across VS Code, Copilot CLI, and the Copilot app."*
La **doc de VS Code** afirma lo contrario para VS Code: *"**VS Code currently ignores client extension data and directories in Agent Plugins 1.0 packages**"* y *"loads **only** the portable skills and MCP server configuration."*
→ El soporte de `com.github.copilot/` en VS Code queda **sin resolver**. No lo enseñes como hecho.

---

## Notas específicas para el stack C#/.NET 8

Ninguna de las páginas consultadas menciona .NET, dotnet ni C#. **No existe documentación oficial de Copilot específica para .NET** (el único activo por lenguaje es el tutorial "Modernize Java applications"). Lo aplicable, derivado de fuentes verificadas:

1. **Cobertura de lenguaje genérica:** *"Copilot code review reviews code written in **any language**, and provides feedback."*
2. **Instalar el .NET 8 SDK en el entorno del agente:** `.github/workflows/copilot-setup-steps.yml` (compartido con el cloud agent) y `.github/workflows/copilot-code-review.yml` (solo review; gana si existe). `timeout-minutes` se define aquí; tope duro **59 minutos** por sesión de cloud agent.
3. **Estrategia de custom instructions para un repo .NET:** `.github/copilot-instructions.md` (estándares transversales) + `.github/instructions/**/*.instructions.md` con `applyTo: "**/*.cs"` / `applyTo: "src/**"` (convenciones por capa) + `AGENTS.md` (cross-tool) + `.github/skills/…` (workflows de tarea; un directorio llamado `code-review` sesga al revisor a usarlo). En code review se leen desde la **head branch**.
4. **⚠️ Sin confirmar:** si `.csproj`, `Directory.Packages.props` o `packages.lock.json` cuentan como "dependency management files" y quedan **excluidos** de la revisión. La doc solo ejemplifica con `package.json` y `Gemfile.lock`. Consultar la página "Files excluded from GitHub Copilot code review".
5. **Windows / .NET dev boxes:** Copilot CLI requiere **PowerShell v6+** o WSL. `winget install GitHub.Copilot` es la ruta de instalación más limpia (la vía npm exige Node.js 22+).
6. **Visual Studio como complemento:** `@modernize` (solo .NET/C++) y `@debugger`, `@git`, `@profiler`, `@test` **solo existen en Visual Studio**. Code review en VS requiere 17.14+.
7. **Gobernanza para una org .NET en Business/Enterprise:** habilitar las políticas **"Copilot cloud agent"** y **"MCP servers on GitHub.com"**; **"Copilot code review"** activa; revisión automática vía rulesets de org (**Automatically request Copilot code review** + **Review new pushes** + **Review draft pull requests**); añadir Copilot como **bypass actor** si hay reglas de commit-author.
8. **Contexto extendido:** la ventana de **1M tokens** solo está disponible en **VS Code y Copilot CLI** — relevante para monorepos .NET grandes.

---

## Fuentes

| Tema | Título de la página | URL | Fecha de consulta |
|---|---|---|---|
| 1 | Plans for GitHub Copilot | https://docs.github.com/en/copilot/get-started/plans | 2026-08-14 |
| 1 | About billing for GitHub Copilot in organizations and enterprises | https://docs.github.com/en/copilot/concepts/billing/organizations-and-enterprises | 2026-08-14 |
| 1 | Choosing your enterprise's plan for GitHub Copilot | https://docs.github.com/en/copilot/tutorials/roll-out-at-scale/assign-licenses/choose-enterprise-plan | 2026-08-14 |
| 1 | About GitHub Spark | https://docs.github.com/en/copilot/concepts/spark | 2026-08-14 |
| 1 | GitHub Copilot features | https://docs.github.com/en/copilot/get-started/features | 2026-08-14 |
| 1 | Copilot feature matrix | https://docs.github.com/en/copilot/reference/copilot-feature-matrix | 2026-08-14 |
| 2 | Choose and use an agent harness | https://code.visualstudio.com/docs/agents/run/agent-harnesses | 2026-08-14 |
| 2 | Use the Chat view | https://code.visualstudio.com/docs/agents/run/chat-view | 2026-08-14 |
| 2 | Use chat in VS Code | https://code.visualstudio.com/docs/chat/chat-overview | 2026-08-14 |
| 2 | Agents | https://code.visualstudio.com/docs/agents/concepts/agents | 2026-08-14 |
| 2, 3, 4 | AI features in VS Code cheat sheet | https://code.visualstudio.com/docs/agents/reference/ai-features-cheat-sheet | 2026-08-14 |
| 3 | Inline suggestions from GitHub Copilot in VS Code | https://code.visualstudio.com/docs/editing/ai-powered-suggestions | 2026-08-14 |
| 3 | Keyboard shortcuts for GitHub Copilot in the IDE | https://docs.github.com/en/copilot/reference/keyboard-shortcuts | 2026-08-14 |
| 4 | Add context to chat | https://code.visualstudio.com/docs/chat/copilot-chat-context | 2026-08-14 |
| 4 | GitHub Copilot Chat cheat sheet (⚠️ obsoleta para VS Code) | https://docs.github.com/en/copilot/reference/chat-cheat-sheet | 2026-08-14 |
| 5 | Use custom instructions in VS Code | https://code.visualstudio.com/docs/agent-customization/custom-instructions | 2026-08-14 |
| 5 | About customizing GitHub Copilot responses | https://docs.github.com/en/copilot/concepts/prompting/response-customization | 2026-08-14 |
| 5 | Support for different types of custom instructions | https://docs.github.com/en/copilot/reference/custom-instructions-support | 2026-08-14 |
| 5 | Adding repository custom instructions for GitHub Copilot | https://docs.github.com/en/copilot/how-tos/copilot-on-github/customize-copilot/add-custom-instructions/add-repository-instructions | 2026-08-14 |
| 5 | Adding repository custom instructions for GitHub Copilot in your IDE (⚠️ obsoleta) | https://docs.github.com/en/copilot/how-tos/configure-custom-instructions-in-your-ide/add-repository-instructions-in-your-ide | 2026-08-14 |
| 5, 6 | Copilot customization cheat sheet | https://docs.github.com/en/copilot/reference/customization-cheat-sheet | 2026-08-14 |
| 5, 6 | Create and manage agent customizations | https://code.visualstudio.com/docs/agent-customization/overview | 2026-08-14 |
| 6 | Use prompt files in VS Code | https://code.visualstudio.com/docs/agent-customization/prompt-files | 2026-08-14 |
| 6 | Custom agents in VS Code | https://code.visualstudio.com/docs/agent-customization/custom-agents | 2026-08-14 |
| 6 | Use Agent Skills in VS Code | https://code.visualstudio.com/docs/agent-customization/agent-skills | 2026-08-14 |
| 7 | About GitHub Copilot code review | https://docs.github.com/en/copilot/concepts/agents/code-review | 2026-08-14 |
| 7 | Using GitHub Copilot code review | https://docs.github.com/en/copilot/how-tos/use-copilot-agents/request-a-code-review/use-code-review | 2026-08-14 |
| 7 | Configuring automatic code review by GitHub Copilot | https://docs.github.com/en/copilot/how-tos/copilot-on-github/set-up-copilot/configure-automatic-review | 2026-08-14 |
| 8 | About GitHub Copilot cloud agent | https://docs.github.com/en/copilot/concepts/agents/cloud-agent/about-cloud-agent | 2026-08-14 |
| 8 | Kick off a task with Copilot agents on GitHub | https://docs.github.com/en/copilot/how-tos/copilot-on-github/use-copilot-agents/kick-off-a-task | 2026-08-14 |
| 8 | Managing access to GitHub Copilot cloud agent | https://docs.github.com/en/copilot/concepts/agents/cloud-agent/access-management | 2026-08-14 |
| 8 | Adding GitHub Copilot cloud agent to your organization | https://docs.github.com/en/copilot/how-tos/administer-copilot/manage-for-organization/add-copilot-cloud-agent | 2026-08-14 |
| 8 | Research, plan, and code with Copilot cloud agent (changelog, 2026-04-01) | https://github.blog/changelog/2026-04-01-research-plan-and-code-with-copilot-cloud-agent/ | 2026-08-14 |
| 9 | About GitHub Copilot CLI | https://docs.github.com/en/copilot/concepts/agents/copilot-cli/about-copilot-cli | 2026-08-14 |
| 9 | Installing GitHub Copilot CLI | https://docs.github.com/en/copilot/how-tos/copilot-cli/set-up-copilot-cli/install-copilot-cli | 2026-08-14 |
| 9 | GitHub Copilot CLI command reference | https://docs.github.com/en/copilot/reference/copilot-cli-reference/cli-command-reference | 2026-08-14 |
| 10 | Add and manage MCP servers in VS Code | https://code.visualstudio.com/docs/agent-customization/mcp-servers | 2026-08-14 |
| 10 | MCP configuration reference | https://code.visualstudio.com/docs/agents/reference/mcp-configuration | 2026-08-14 |
| 10 | About Model Context Protocol (MCP) | https://docs.github.com/en/copilot/concepts/context/mcp | 2026-08-14 |
| 10 | Restrict MCP server access to a custom registry | https://docs.github.com/en/copilot/how-tos/administer-copilot/configure-mcp-server-access | 2026-08-14 |
| 10, 17 | Manage AI settings in enterprise environments | https://code.visualstudio.com/docs/enterprise/ai-settings | 2026-08-14 |
| 11 | Supported AI models in GitHub Copilot | https://docs.github.com/en/copilot/reference/ai-models/supported-models | 2026-08-14 |
| 11 | AI language models in VS Code | https://code.visualstudio.com/docs/agent-customization/language-models | 2026-08-14 |
| 12 | Content exclusion for GitHub Copilot | https://docs.github.com/en/copilot/concepts/context/content-exclusion | 2026-08-14 |
| 12 | Excluding content from GitHub Copilot | https://docs.github.com/en/copilot/how-tos/configure-content-exclusion/exclude-content-from-copilot | 2026-08-14 |
| 12 | Managing policies and features for GitHub Copilot in your organization | https://docs.github.com/en/copilot/how-tos/administer-copilot/manage-for-organization/manage-policies | 2026-08-14 |
| 12 | Managing policies and features for GitHub Copilot in your enterprise | https://docs.github.com/en/copilot/how-tos/administer-copilot/manage-for-enterprise/manage-enterprise-policies | 2026-08-14 |
| 12 | Policy conflicts | https://docs.github.com/en/copilot/reference/enterprise-administrators/policy-conflicts | 2026-08-14 |
| 12 | Supported surfaces for policies | https://docs.github.com/en/copilot/reference/supported-surfaces-for-policies | 2026-08-14 |
| 13 | Managing GitHub Copilot policies as an individual subscriber | https://docs.github.com/en/copilot/how-tos/manage-your-account/manage-policies | 2026-08-14 |
| 13 | GitHub Copilot code referencing | https://docs.github.com/en/copilot/concepts/completions/code-referencing | 2026-08-14 |
| 13 | Hosting of models for GitHub Copilot | https://docs.github.com/en/copilot/reference/ai-models/model-hosting | 2026-08-14 |
| 14 | Manage GitHub Copilot installation and state | https://learn.microsoft.com/en-us/visualstudio/ide/visual-studio-github-copilot-install-and-states | 2026-08-14 |
| 14 | Customize chat responses (Visual Studio) | https://learn.microsoft.com/en-us/visualstudio/ide/copilot-chat-context | 2026-08-14 |
| 14 | Manage chat context in GitHub Copilot Chat | https://learn.microsoft.com/en-us/visualstudio/ide/copilot-chat-context-references | 2026-08-14 |
| 14 | Copilot Completions (Visual Studio) | https://learn.microsoft.com/en-us/visualstudio/ide/visual-studio-github-copilot-extension | 2026-08-14 |
| 14 | About GitHub Copilot Chat in Visual Studio | https://learn.microsoft.com/en-us/visualstudio/ide/visual-studio-github-copilot-chat | 2026-08-14 |
| 14 | Use Agent Mode (Visual Studio) | https://learn.microsoft.com/en-us/visualstudio/ide/copilot-agent-mode | 2026-08-14 |
| 14 | Use built-in and custom agents (Visual Studio) | https://learn.microsoft.com/en-us/visualstudio/ide/copilot-specialized-agents | 2026-08-14 |
| 14 | Add GitHub accounts to your keychain | https://learn.microsoft.com/en-us/visualstudio/ide/work-with-github-accounts | 2026-08-14 |
| 15 | GitHub Copilot usage metrics | https://docs.github.com/en/copilot/concepts/copilot-usage-metrics/copilot-metrics | 2026-08-14 |
| 15 | REST API endpoints for Copilot usage metrics | https://docs.github.com/en/rest/copilot/copilot-usage-metrics | 2026-08-14 |
| 15 | REST API endpoints for Copilot user management | https://docs.github.com/en/rest/copilot/copilot-user-management | 2026-08-14 |
| 15 | Viewing the Copilot usage metrics dashboard | https://docs.github.com/en/copilot/how-tos/administer-copilot/view-usage-and-adoption | 2026-08-14 |
| 15 | Viewing the code generation dashboard | https://docs.github.com/en/copilot/how-tos/administer-copilot/view-code-generation | 2026-08-14 |
| 15 | Viewing the Copilot impact dashboard | https://docs.github.com/en/copilot/how-tos/administer-copilot/view-impact-dashboard | 2026-08-14 |
| 16 | Visual Studio Code 1.116 (release notes, 2026-04-15) | https://code.visualstudio.com/updates/v1_116 | 2026-08-14 |
| 16 | Visual Studio Code 1.133 (release notes, 2026-08-12) | https://code.visualstudio.com/updates/v1_133 | 2026-08-14 |
| 16 | Set up GitHub Copilot in VS Code | https://code.visualstudio.com/docs/setup/copilot | 2026-08-14 |
| 16 | Installing the GitHub Copilot extension in your environment | https://docs.github.com/en/copilot/how-tos/set-up/install-copilot-extension | 2026-08-14 |
| 16 | Working with GitHub in VS Code | https://code.visualstudio.com/docs/sourcecontrol/github | 2026-08-14 |
| 16 | AI troubleshooting FAQ (VS Code) | https://code.visualstudio.com/docs/agents/agent-troubleshooting/faq | 2026-08-14 |
| 16 | Open Source AI Editor: Second Milestone (blog, 2025-11-06) | https://code.visualstudio.com/blogs/2025/11/04/openSourceAIEditorSecondMilestone | 2026-08-14 |
| 16 | Manage extensions in enterprise environments | https://code.visualstudio.com/docs/enterprise/extensions | 2026-08-14 |
| 17 | Agent Plugins 1.0 in VS Code, Copilot CLI, and the Copilot app (changelog, 2026-08-12) | https://github.blog/changelog/2026-08-12-agent-plugins-1-0-in-vs-code-copilot-cli-and-the-copilot-app/ | 2026-08-14 |
| 17 | Agent plugins in VS Code | https://code.visualstudio.com/docs/agent-customization/agent-plugins | 2026-08-14 |
| 17 | About GitHub Copilot plugins | https://docs.github.com/en/copilot/concepts/agents/about-plugins | 2026-08-14 |
| 17 | About enterprise-managed plugin standards | https://docs.github.com/en/copilot/concepts/agents/about-enterprise-plugin-standards | 2026-08-14 |

---

## No verificado / advertencias

### A. Inferido por ausencia, sin declaración oficial (NO presentar como hecho documentado)

1. **Que los modos Ask y Edit fueron "renombrados", fusionados o eliminados en VS Code.** Confirmado solo por su **ausencia total** en seis páginas actuales. No existe ninguna frase oficial de migración que los nombre.
2. **Que `@workspace` fue deprecado o reemplazado por `#codebase`.** Solo ausencia. (`@workspace` sí sigue documentado y vigente en **Visual Studio**.)
3. **Que el frontmatter `mode:` de prompt files fue renombrado a `agent:`.** `mode` simplemente ya no figura en la tabla de frontmatter. Contraste: el renombrado `.chatmode.md` → `.agent.md` **sí** está declarado explícitamente.
4. **Si los archivos `.chatmode.md` siguen cargándose por retrocompatibilidad.** La doc solo instruye a renombrarlos; nunca dice si los antiguos aún funcionan ni cuándo termina el soporte.
5. Los slash commands **`/createWorkspace`** y **`/terminal`** no aparecen en ninguna página actual de VS Code.

### B. Datos que la documentación oficial no publica

6. **La ruta absoluta del `mcp.json` de user profile** — solo se documenta el comando **MCP: Open User Configuration**.
7. **La ruta del filesystem para prompt files a nivel de user profile** — solo "Your user data (specific to your VS Code profile)". No existe un `~/.copilot/prompts` documentado (a diferencia de instructions, agents y skills, que sí tienen rutas `~/.copilot/...` explícitas).
8. **El ID exacto del setting `github.copilot.chat.codeGeneration.instructions`** no se imprime en ninguna página. Solo se documenta descriptivamente su deprecación ("settings-based code generation and test generation instructions… deprecated as of VS Code 1.102") y una etiqueta de UI "Code Generation: Use Instruction Files". **No lo cites como ID documentado.**
9. **Command ID / keybinding oficial para "accept next word"** en VS Code, más allá de la prosa `Ctrl+Right` / `⌘→`. `editor.action.inlineSuggest.acceptNextWord` **no** está publicado.
10. **Atajo de teclado para abrir el desplegable de agentes** (la celda de keybinding está vacía en el cheat sheet). Solo existe `Ctrl+Shift+I` = "Switch to using agents".
11. **Estado por defecto de "Suggestions matching public code" para Copilot Enterprise** y para los planes individuales (Free/Pro/Pro+/Max). Solo se documenta el default **Blocked para Copilot Business**.
12. **Umbral mínimo de asientos** para las métricas (históricamente "5 o más miembros con asiento activo"). **No aparece en ninguna página actual.** Tampoco se declara un requisito de plan en la página de la Metrics API; el gating de plan solo se infiere vía la política "Copilot usage metrics".
13. **Si `.csproj`, `Directory.Packages.props` o `packages.lock.json` cuentan como "dependency management files"** excluidos de code review. La doc solo ejemplifica con `package.json` y `Gemfile.lock`.
14. **La fecha de GA de Copilot CLI** (2026-02-25) y las **fechas de deprecación de coding guidelines** (playground 2025-08-06, deprecación total 2025-09-03) provienen de **títulos/snippets de changelog**, no de páginas abiertas y leídas.
15. **Soporte de `AGENTS.md` en Visual Studio** — ninguna página de learn.microsoft.com lo menciona. **No lo afirmes.**
16. **El token `#solution` en Visual Studio** no está documentado (usar `@workspace`).
17. **Si `GitHub.copilot` sigue publicado en el Marketplace.** El FAQ de VS Code fechado 8/12/2026 aún indica mantener "ambas" extensiones actualizadas — frase que parece obsoleta frente a la 1.116. Ninguna página oficial nombra la release que efectivamente lo retiró. Las páginas del Marketplace quedaron fuera del conjunto de dominios oficiales permitidos.

### C. Contradicciones entre fuentes oficiales (citar ambas o marcar como no resuelto)

18. **`#codebase` / `#fetch` (forma plana) vs `#search/codebase` / `#web/fetch` (namespaced).** Ambas formas están publicadas oficialmente, en páginas distintas y ambas vigentes. **La doc es internamente inconsistente.**
19. **Latencia de métricas: "two full days" vs "up to three full UTC days".** Dos cifras oficiales distintas.
20. **Versión mínima de Visual Studio: 17.8 vs 17.10 vs 17.14 / VS 2026.** Genuinamente inconsistente entre páginas de learn.microsoft.com.
21. **`AGENTS.md` vs `CLAUDE.md` en VS Code Chat.** La matriz de soporte de GitHub dice que VS Code Chat honra **solo `AGENTS.md`**; la página de VS Code documenta soporte completo de **`CLAUDE.md`** vía `chat.useClaudeMdFile`. **No alineadas.**
22. **Directorio `com.github.copilot/` en Agent Plugins 1.0.** El changelog dice que VS Code carga desde ahí; la doc de VS Code dice que **ignora** los datos y directorios de extensión de cliente. **Sin resolver.**
23. **`/help` en VS Code.** Está en el cheat sheet de GitHub Docs pero **no** en el de VS Code.
24. **`GEMINI.md`** aparece solo en GitHub Docs; la página de custom instructions de VS Code documenta únicamente `AGENTS.md` y `CLAUDE.md`.

### D. Páginas oficiales que están OBSOLETAS y no deben usarse como fuente

25. **`docs.github.com/en/copilot/reference/chat-cheat-sheet`** — su tabla de slash commands y variables para VS Code precede a la reorganización. Lista participantes `@azure`, `@github`, `@terminal`, `@vscode` y variables `#block`, `#class`, `#comment`, `#file`, `#function`, `#line`, `#path`, `#project`, `#selection`, `#sym`, sin `#codebase`, `#fetch`, `#changes` ni `#problems`.
26. **`docs.github.com/en/copilot/how-tos/configure-custom-instructions-in-your-ide/add-repository-instructions-in-your-ide`** — enlaza a URLs retiradas (`code.visualstudio.com/docs/copilot/customization/...`) y documenta el setting legacy `"chat.promptFiles": true` y el comando **Chat: Create Prompt**, ambos superados.
27. **Regla general:** para mecánica de VS Code, la fuente autoritativa es **code.visualstudio.com/docs/agents/** y **/docs/agent-customization/**, no docs.github.com.

### E. Limitación metodológica

28. **Los check marks (✓/✗) de varias tablas de docs.github.com son imágenes SVG (octicons) y no sobreviven a la extracción de texto.** Afecta a: la tabla de content exclusion por IDE, las tablas de modelos por cliente y por plan, la tabla de Auto model selection y la de capacidades extendidas. Los valores de las secciones Customization y Other features de la página Plans se recuperaron vía `plans.md` (atributos `aria-label="Included"/"Not included"`) y por triple corroboración con los bloques "Who can use this feature?" de cada página específica, pero **no son una captura literal del render en vivo**. Además, el repo público `github/docs` va **desfasado** respecto al sitio en vivo.
29. **No se pudo verificar en vivo la totalidad de las ~15 últimas filas de la tabla de modelos por plan** (Gemini 3.6/3.7, GPT-5.6 Luna/Sol/Terra, Grok, Kimi, MAI-Code, Raptor mini). No puedo afirmar con certeza absoluta que no exista algún modelo exclusivo de Enterprise entre ellas.
