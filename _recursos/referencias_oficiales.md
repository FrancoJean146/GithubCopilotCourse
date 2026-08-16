# Referencias oficiales de GitHub Copilot

**Propósito.** Catálogo único de enlaces oficiales usados para construir y sostener este curso, organizado por los 17 temas de la ficha técnica (`06_recursos/ficha_tecnica_copilot_verificada.md`). Cada afirmación técnica del material debe poder rastrearse hasta una de estas páginas.

**Fecha de verificación de todas las entradas: 2026-08-14.**

**Advertencia de vigencia (riesgo RG-07 del registro de decisiones).** GitHub Copilot evoluciona muy rápido: nombres de producto, rutas de personalización, catálogo de modelos y superficies de política cambian en semanas, no en años. Ninguna entrada de este catálogo debe darse por vigente sin comprobación. **Revisión obligatoria de este archivo y de la ficha técnica 48 horas antes del Día 1.** Si algo cambió, se anota la discrepancia en `00_control/decisiones_y_supuestos.md` y se corrige el material antes de dictar.

**Convención de etiquetas en la columna Nota:** `[GA]` disponible en general, `[Preview]` en vista previa, `[Depende del plan/política]` sujeto al plan contratado o a políticas de organización o empresa.

---

## 1. Planes de Copilot y diferencia Business vs Enterprise

| Título de la página | URL | Fecha de consulta | Nota |
|---|---|---|---|
| Plans for GitHub Copilot | https://docs.github.com/en/copilot/get-started/plans | 2026-08-14 | Tabla maestra de capacidades por plan; base para contrastar Business vs Enterprise. `[Depende del plan/política]` |
| About billing for GitHub Copilot in organizations and enterprises | https://docs.github.com/en/copilot/concepts/billing/organizations-and-enterprises | 2026-08-14 | Asignación de asientos, AI credits mensuales y facturación de excedentes. `[Depende del plan/política]` |
| Choosing your enterprise's plan for GitHub Copilot | https://docs.github.com/en/copilot/tutorials/roll-out-at-scale/assign-licenses/choose-enterprise-plan | 2026-08-14 | Criterio oficial de decisión entre Business y Enterprise. `[Depende del plan/política]` |
| About GitHub Spark | https://docs.github.com/en/copilot/concepts/spark | 2026-08-14 | Diferenciador real de Enterprise. `[Preview]` |
| GitHub Copilot features | https://docs.github.com/en/copilot/get-started/features | 2026-08-14 | Inventario de funcionalidades por superficie; útil para delimitar el alcance del curso. |
| Copilot feature matrix | https://docs.github.com/en/copilot/reference/copilot-feature-matrix | 2026-08-14 | Matriz funcionalidad por IDE; resuelve qué existe en VS Code y qué no. |

---

## 2. Modos de chat en VS Code: Session Target y agent roles

| Título de la página | URL | Fecha de consulta | Nota |
|---|---|---|---|
| Choose and use an agent harness | https://code.visualstudio.com/docs/agents/run/agent-harnesses | 2026-08-14 | Define **Session Target** (harness) y sustituye al antiguo selector Ask/Edit/Agent. `[GA]` |
| Use the Chat view | https://code.visualstudio.com/docs/agents/run/chat-view | 2026-08-14 | Anatomía actual de la vista de chat y selección de **agent role**. `[GA]` |
| Use chat in VS Code | https://code.visualstudio.com/docs/chat/chat-overview | 2026-08-14 | Panorama de entrada del chat; contexto conceptual para la primera jornada. |
| Agents | https://code.visualstudio.com/docs/agents/concepts/agents | 2026-08-14 | Concepto de agente y diferencia entre roles `Agent` y `Plan`. `[GA]` |
| AI features in VS Code cheat sheet | https://code.visualstudio.com/docs/agents/reference/ai-features-cheat-sheet | 2026-08-14 | Referencia autoritativa de atajos, slash commands y variables de VS Code. `[GA]` |

---

## 3. Code completions y next edit suggestions

| Título de la página | URL | Fecha de consulta | Nota |
|---|---|---|---|
| Inline suggestions from GitHub Copilot in VS Code | https://code.visualstudio.com/docs/editing/ai-powered-suggestions | 2026-08-14 | Completions inline y next edit suggestions; aceptación parcial por palabra. `[GA]` |
| Keyboard shortcuts for GitHub Copilot in the IDE | https://docs.github.com/en/copilot/reference/keyboard-shortcuts | 2026-08-14 | Atajos por IDE según GitHub Docs; contrastar con el cheat sheet de VS Code. |
| AI features in VS Code cheat sheet | https://code.visualstudio.com/docs/agents/reference/ai-features-cheat-sheet | 2026-08-14 | Fuente que manda para los atajos reales de VS Code. `[GA]` |

---

## 4. Slash commands y variables de contexto

| Título de la página | URL | Fecha de consulta | Nota |
|---|---|---|---|
| AI features in VS Code cheat sheet | https://code.visualstudio.com/docs/agents/reference/ai-features-cheat-sheet | 2026-08-14 | Lista vigente de slash commands y variables de contexto en VS Code. `[GA]` |
| Add context to chat | https://code.visualstudio.com/docs/chat/copilot-chat-context | 2026-08-14 | Cómo se adjunta contexto y qué hace cada variable. `[GA]` |
| GitHub Copilot Chat cheat sheet | https://docs.github.com/en/copilot/reference/chat-cheat-sheet | 2026-08-14 | **Obsoleta para VS Code**; ver la sección de páginas obsoletas. Solo válida como referencia histórica. |

---

## 5. Custom instructions: rutas, precedencia y alcance

| Título de la página | URL | Fecha de consulta | Nota |
|---|---|---|---|
| Use custom instructions in VS Code | https://code.visualstudio.com/docs/agent-customization/custom-instructions | 2026-08-14 | Rutas exactas de archivos de instrucciones en VS Code y `applyTo`. `[GA]` |
| About customizing GitHub Copilot responses | https://docs.github.com/en/copilot/concepts/prompting/response-customization | 2026-08-14 | Concepto y **precedencia** personal, repositorio y organización. `[Depende del plan/política]` |
| Support for different types of custom instructions | https://docs.github.com/en/copilot/reference/custom-instructions-support | 2026-08-14 | Matriz de qué tipo de instrucción honra cada cliente. Fuente del conflicto `AGENTS.md` vs `CLAUDE.md`. |
| Adding repository custom instructions for GitHub Copilot | https://docs.github.com/en/copilot/how-tos/copilot-on-github/customize-copilot/add-custom-instructions/add-repository-instructions | 2026-08-14 | Procedimiento vigente para `.github/copilot-instructions.md`. `[GA]` |
| Adding repository custom instructions for GitHub Copilot in your IDE | https://docs.github.com/en/copilot/how-tos/configure-custom-instructions-in-your-ide/add-repository-instructions-in-your-ide | 2026-08-14 | **Obsoleta**; ver la sección de páginas obsoletas. |
| Copilot customization cheat sheet | https://docs.github.com/en/copilot/reference/customization-cheat-sheet | 2026-08-14 | Resumen de todos los mecanismos de personalización y su alcance. |
| Create and manage agent customizations | https://code.visualstudio.com/docs/agent-customization/overview | 2026-08-14 | Mapa de personalizaciones de VS Code: instructions, prompts, agents, skills. `[GA]` |

---

## 6. Prompt files, custom agents y agent skills

| Título de la página | URL | Fecha de consulta | Nota |
|---|---|---|---|
| Use prompt files in VS Code | https://code.visualstudio.com/docs/agent-customization/prompt-files | 2026-08-14 | Formato `.prompt.md`, frontmatter vigente e invocación. `[GA]` |
| Custom agents in VS Code | https://code.visualstudio.com/docs/agent-customization/custom-agents | 2026-08-14 | `.agent.md` en `.github/agents/`; declara el renombrado desde `.chatmode.md`. `[GA]` |
| Use Agent Skills in VS Code | https://code.visualstudio.com/docs/agent-customization/agent-skills | 2026-08-14 | Skills como workflows de tarea y su descubrimiento por el agente. `[GA]` |
| Create and manage agent customizations | https://code.visualstudio.com/docs/agent-customization/overview | 2026-08-14 | Cuándo usar instructions, prompt file, agent o skill. `[GA]` |
| Copilot customization cheat sheet | https://docs.github.com/en/copilot/reference/customization-cheat-sheet | 2026-08-14 | Vista de GitHub Docs sobre los mismos artefactos; útil para superficies fuera de VS Code. |

---

## 7. Copilot code review

| Título de la página | URL | Fecha de consulta | Nota |
|---|---|---|---|
| About GitHub Copilot code review | https://docs.github.com/en/copilot/concepts/agents/code-review | 2026-08-14 | Alcance, límites y por qué la revisión siempre es de tipo "Comment". `[GA]` |
| Using GitHub Copilot code review | https://docs.github.com/en/copilot/how-tos/use-copilot-agents/request-a-code-review/use-code-review | 2026-08-14 | Cómo solicitar una revisión y cómo se leen las instrucciones de la head branch. `[GA]` |
| Configuring automatic code review by GitHub Copilot | https://docs.github.com/en/copilot/how-tos/copilot-on-github/set-up-copilot/configure-automatic-review | 2026-08-14 | Rulesets de organización para revisión automática. `[Depende del plan/política]` |

---

## 8. Copilot cloud agent

| Título de la página | URL | Fecha de consulta | Nota |
|---|---|---|---|
| About GitHub Copilot cloud agent | https://docs.github.com/en/copilot/concepts/agents/cloud-agent/about-cloud-agent | 2026-08-14 | Concepto, límites de sesión y `copilot-setup-steps.yml`. `[GA]` |
| Kick off a task with Copilot agents on GitHub | https://docs.github.com/en/copilot/how-tos/copilot-on-github/use-copilot-agents/kick-off-a-task | 2026-08-14 | Asignación de issues y arranque de tareas desde GitHub.com. `[GA]` |
| Managing access to GitHub Copilot cloud agent | https://docs.github.com/en/copilot/concepts/agents/cloud-agent/access-management | 2026-08-14 | Está deshabilitado por defecto en licencias asignadas por la organización. `[Depende del plan/política]` |
| Adding GitHub Copilot cloud agent to your organization | https://docs.github.com/en/copilot/how-tos/administer-copilot/manage-for-organization/add-copilot-cloud-agent | 2026-08-14 | Habilitación administrativa paso a paso. `[Depende del plan/política]` |
| Research, plan, and code with Copilot cloud agent (changelog, 2026-04-01) | https://github.blog/changelog/2026-04-01-research-plan-and-code-with-copilot-cloud-agent/ | 2026-08-14 | Anuncio del renombrado desde "Copilot coding agent". |

---

## 9. GitHub Copilot CLI

| Título de la página | URL | Fecha de consulta | Nota |
|---|---|---|---|
| About GitHub Copilot CLI | https://docs.github.com/en/copilot/concepts/agents/copilot-cli/about-copilot-cli | 2026-08-14 | Qué es, qué contexto usa y sus límites frente al IDE. `[GA]` |
| Installing GitHub Copilot CLI | https://docs.github.com/en/copilot/how-tos/copilot-cli/set-up-copilot-cli/install-copilot-cli | 2026-08-14 | Requisitos en Windows: PowerShell v6+ o WSL; `winget` o npm con Node.js 22+. |
| GitHub Copilot CLI command reference | https://docs.github.com/en/copilot/reference/copilot-cli-reference/cli-command-reference | 2026-08-14 | Referencia de comandos y banderas. `[GA]` |

---

## 10. MCP (Model Context Protocol)

| Título de la página | URL | Fecha de consulta | Nota |
|---|---|---|---|
| Add and manage MCP servers in VS Code | https://code.visualstudio.com/docs/agent-customization/mcp-servers | 2026-08-14 | Alta de servidores y comando **MCP: Open User Configuration**. `[GA]` |
| MCP configuration reference | https://code.visualstudio.com/docs/agents/reference/mcp-configuration | 2026-08-14 | Esquema completo de `mcp.json`. `[GA]` |
| About Model Context Protocol (MCP) | https://docs.github.com/en/copilot/concepts/context/mcp | 2026-08-14 | Concepto y política "MCP servers on GitHub.com". `[Depende del plan/política]` |
| Restrict MCP server access to a custom registry | https://docs.github.com/en/copilot/how-tos/administer-copilot/configure-mcp-server-access | 2026-08-14 | Control corporativo del catálogo de servidores. `[Depende del plan/política]` |
| Manage AI settings in enterprise environments | https://code.visualstudio.com/docs/enterprise/ai-settings | 2026-08-14 | Bloqueo de settings de IA por política de dispositivo. `[Depende del plan/política]` |

---

## 11. Selección de modelos y catálogo

| Título de la página | URL | Fecha de consulta | Nota |
|---|---|---|---|
| Supported AI models in GitHub Copilot | https://docs.github.com/en/copilot/reference/ai-models/supported-models | 2026-08-14 | Catálogo por cliente y por plan; ver limitaciones metodológicas E.28 y E.29. `[Depende del plan/política]` |
| AI language models in VS Code | https://code.visualstudio.com/docs/agent-customization/language-models | 2026-08-14 | Selector de modelo en VS Code y modelos propios (BYOK). `[GA]` |

---

## 12. Content exclusions y políticas de organización/empresa

| Título de la página | URL | Fecha de consulta | Nota |
|---|---|---|---|
| Content exclusion for GitHub Copilot | https://docs.github.com/en/copilot/concepts/context/content-exclusion | 2026-08-14 | Alcance real: no aplica en agent mode ni en Copilot CLI. `[Depende del plan/política]` |
| Excluding content from GitHub Copilot | https://docs.github.com/en/copilot/how-tos/configure-content-exclusion/exclude-content-from-copilot | 2026-08-14 | Sintaxis de patrones y propagación de hasta 30 minutos. `[Depende del plan/política]` |
| Managing policies and features for GitHub Copilot in your organization | https://docs.github.com/en/copilot/how-tos/administer-copilot/manage-for-organization/manage-policies | 2026-08-14 | Interruptores de política a nivel de organización. `[Depende del plan/política]` |
| Managing policies and features for GitHub Copilot in your enterprise | https://docs.github.com/en/copilot/how-tos/administer-copilot/manage-for-enterprise/manage-enterprise-policies | 2026-08-14 | Interruptores a nivel de empresa y herencia hacia organizaciones. `[Depende del plan/política]` |
| Policy conflicts | https://docs.github.com/en/copilot/reference/enterprise-administrators/policy-conflicts | 2026-08-14 | Qué gana cuando empresa y organización discrepan. `[Depende del plan/política]` |
| Supported surfaces for policies | https://docs.github.com/en/copilot/reference/supported-surfaces-for-policies | 2026-08-14 | En qué cliente surte efecto cada política. `[Depende del plan/política]` |

---

## 13. Privacidad, uso de datos y code referencing

| Título de la página | URL | Fecha de consulta | Nota |
|---|---|---|---|
| Managing GitHub Copilot policies as an individual subscriber | https://docs.github.com/en/copilot/how-tos/manage-your-account/manage-policies | 2026-08-14 | Controles del suscriptor individual, incluido el uso de datos para entrenamiento. |
| GitHub Copilot code referencing | https://docs.github.com/en/copilot/concepts/completions/code-referencing | 2026-08-14 | "Suggestions matching public code"; default Blocked documentado solo para Business. `[Depende del plan/política]` |
| Hosting of models for GitHub Copilot | https://docs.github.com/en/copilot/reference/ai-models/model-hosting | 2026-08-14 | Dónde se alojan y procesan los modelos; insumo para la revisión legal. |

---

## 14. Copilot en Visual Studio

| Título de la página | URL | Fecha de consulta | Nota |
|---|---|---|---|
| Manage GitHub Copilot installation and state | https://learn.microsoft.com/en-us/visualstudio/ide/visual-studio-github-copilot-install-and-states | 2026-08-14 | Instalación y estados; aquí aparecen versiones mínimas en conflicto. |
| Customize chat responses (Visual Studio) | https://learn.microsoft.com/en-us/visualstudio/ide/copilot-chat-context | 2026-08-14 | Custom instructions en Visual Studio; no asumir paridad con VS Code. |
| Manage chat context in GitHub Copilot Chat | https://learn.microsoft.com/en-us/visualstudio/ide/copilot-chat-context-references | 2026-08-14 | `@workspace` sigue vigente aquí; `#solution` no está documentado. |
| Copilot Completions (Visual Studio) | https://learn.microsoft.com/en-us/visualstudio/ide/visual-studio-github-copilot-extension | 2026-08-14 | Completions y su configuración en la IDE. `[GA]` |
| About GitHub Copilot Chat in Visual Studio | https://learn.microsoft.com/en-us/visualstudio/ide/visual-studio-github-copilot-chat | 2026-08-14 | Panorama del chat en Visual Studio. `[GA]` |
| Use Agent Mode (Visual Studio) | https://learn.microsoft.com/en-us/visualstudio/ide/copilot-agent-mode | 2026-08-14 | El agent mode de Visual Studio conserva nomenclatura propia. `[GA]` |
| Use built-in and custom agents (Visual Studio) | https://learn.microsoft.com/en-us/visualstudio/ide/copilot-specialized-agents | 2026-08-14 | `@modernize`, `@debugger`, `@git`, `@profiler`, `@test`: exclusivos de Visual Studio. |
| Add GitHub accounts to your keychain | https://learn.microsoft.com/en-us/visualstudio/ide/work-with-github-accounts | 2026-08-14 | Inicio de sesión y cuentas; prerrequisito del laboratorio de instalación. |

---

## 15. Métricas de adopción

| Título de la página | URL | Fecha de consulta | Nota |
|---|---|---|---|
| GitHub Copilot usage metrics | https://docs.github.com/en/copilot/concepts/copilot-usage-metrics/copilot-metrics | 2026-08-14 | Qué se mide y con qué latencia; cifra en conflicto con la API. `[Depende del plan/política]` |
| REST API endpoints for Copilot usage metrics | https://docs.github.com/en/rest/copilot/copilot-usage-metrics | 2026-08-14 | Endpoints, ventanas de datos y latencia declarada. |
| REST API endpoints for Copilot user management | https://docs.github.com/en/rest/copilot/copilot-user-management | 2026-08-14 | Gestión programática de asientos. `[Depende del plan/política]` |
| Viewing the Copilot usage metrics dashboard | https://docs.github.com/en/copilot/how-tos/administer-copilot/view-usage-and-adoption | 2026-08-14 | Lectura del panel de adopción. `[Depende del plan/política]` |
| Viewing the code generation dashboard | https://docs.github.com/en/copilot/how-tos/administer-copilot/view-code-generation | 2026-08-14 | Panel de generación de código. `[Depende del plan/política]` |
| Viewing the Copilot impact dashboard | https://docs.github.com/en/copilot/how-tos/administer-copilot/view-impact-dashboard | 2026-08-14 | Panel de impacto. `[Depende del plan/política]` |

---

## 16. Extensiones de VS Code

| Título de la página | URL | Fecha de consulta | Nota |
|---|---|---|---|
| Visual Studio Code 1.116 (release notes, 2026-04-15) | https://code.visualstudio.com/updates/v1_116 | 2026-08-14 | Release que integra Copilot Chat como built-in. |
| Visual Studio Code 1.133 (release notes, 2026-08-12) | https://code.visualstudio.com/updates/v1_133 | 2026-08-14 | Versión de referencia del curso; comprobar si hay una posterior antes de dictar. |
| Set up GitHub Copilot in VS Code | https://code.visualstudio.com/docs/setup/copilot | 2026-08-14 | Puesta en marcha vigente: iniciar sesión, no instalar extensión. `[GA]` |
| Installing the GitHub Copilot extension in your environment | https://docs.github.com/en/copilot/how-tos/set-up/install-copilot-extension | 2026-08-14 | Instalación por IDE según GitHub Docs; para VS Code prevalece la página de VS Code. |
| Working with GitHub in VS Code | https://code.visualstudio.com/docs/sourcecontrol/github | 2026-08-14 | Integración con pull requests e issues. |
| AI troubleshooting FAQ (VS Code) | https://code.visualstudio.com/docs/agents/agent-troubleshooting/faq | 2026-08-14 | Diagnóstico de fallos en clase; contiene una frase aparentemente desactualizada sobre "ambas extensiones". |
| Open Source AI Editor: Second Milestone (blog, 2025-11-06) | https://code.visualstudio.com/blogs/2025/11/04/openSourceAIEditorSecondMilestone | 2026-08-14 | Contexto de la apertura del código del editor y de la integración de Copilot. |
| Manage extensions in enterprise environments | https://code.visualstudio.com/docs/enterprise/extensions | 2026-08-14 | Listas de permitidos y bloqueo de extensiones. `[Depende del plan/política]` |

---

## 17. Agent Plugins 1.0

| Título de la página | URL | Fecha de consulta | Nota |
|---|---|---|---|
| Agent Plugins 1.0 in VS Code, Copilot CLI, and the Copilot app (changelog, 2026-08-12) | https://github.blog/changelog/2026-08-12-agent-plugins-1-0-in-vs-code-copilot-cli-and-the-copilot-app/ | 2026-08-14 | Anuncio fundacional; origen de la discrepancia sobre `com.github.copilot/`. |
| Agent plugins in VS Code | https://code.visualstudio.com/docs/agent-customization/agent-plugins | 2026-08-14 | Formato del plugin y qué carga VS Code realmente. |
| About GitHub Copilot plugins | https://docs.github.com/en/copilot/concepts/agents/about-plugins | 2026-08-14 | Concepto de plugin en el ecosistema Copilot. |
| About enterprise-managed plugin standards | https://docs.github.com/en/copilot/concepts/agents/about-enterprise-plugin-standards | 2026-08-14 | Distribución y control corporativo de plugins. `[Depende del plan/política]` |
| Manage AI settings in enterprise environments | https://code.visualstudio.com/docs/enterprise/ai-settings | 2026-08-14 | Restricción de plugins y settings de IA por dispositivo. `[Depende del plan/política]` |

---

## Páginas oficiales obsoletas que no deben usarse

| Página | URL | Motivo por el que está obsoleta | Qué usar en su lugar |
|---|---|---|---|
| GitHub Copilot Chat cheat sheet | https://docs.github.com/en/copilot/reference/chat-cheat-sheet | Su tabla de slash commands y variables para VS Code precede a la reorganización: lista los participantes `@azure`, `@github`, `@terminal`, `@vscode` y las variables `#block`, `#class`, `#comment`, `#file`, `#function`, `#line`, `#path`, `#project`, `#selection`, `#sym`, sin `#codebase`, `#fetch`, `#changes` ni `#problems` | AI features in VS Code cheat sheet: https://code.visualstudio.com/docs/agents/reference/ai-features-cheat-sheet |
| Adding repository custom instructions for GitHub Copilot in your IDE | https://docs.github.com/en/copilot/how-tos/configure-custom-instructions-in-your-ide/add-repository-instructions-in-your-ide | Enlaza a URLs retiradas bajo `code.visualstudio.com/docs/copilot/customization/...` y documenta el setting legacy `"chat.promptFiles": true` y el comando **Chat: Create Prompt**, ambos superados | Use custom instructions in VS Code: https://code.visualstudio.com/docs/agent-customization/custom-instructions y Use prompt files in VS Code: https://code.visualstudio.com/docs/agent-customization/prompt-files |
| Cualquier URL bajo `code.visualstudio.com/docs/copilot/...` | (patrón de URL, no una página concreta) | La documentación de VS Code se reorganizó: ese árbol se retiró | Los árboles vigentes `https://code.visualstudio.com/docs/agents/` y `https://code.visualstudio.com/docs/agent-customization/` |
| Cualquier URL que contenga `coding-agent` | (patrón de URL, no una página concreta) | Sobreviven solo como redirecciones tras el renombrado a **Copilot cloud agent** (abril de 2026); citarlas propaga terminología muerta | About GitHub Copilot cloud agent: https://docs.github.com/en/copilot/concepts/agents/cloud-agent/about-cloud-agent |
| Cualquier página que describa "coding guidelines" de Copilot code review | (patrón de contenido, no una página concreta) | La funcionalidad fue eliminada y reemplazada por custom instructions | About GitHub Copilot code review: https://docs.github.com/en/copilot/concepts/agents/code-review y Adding repository custom instructions for GitHub Copilot: https://docs.github.com/en/copilot/how-tos/copilot-on-github/customize-copilot/add-custom-instructions/add-repository-instructions |

---

## Regla de vigencia: qué fuente manda

Cuando dos dominios oficiales dicen cosas distintas, no gana el más reciente ni el más cómodo: gana el dueño de la superficie descrita.

1. **Mecánica de VS Code** (atajos de teclado, slash commands, variables de contexto, rutas de archivos de personalización, identificadores de settings, comportamiento del chat, harnesses y agent roles): la fuente autoritativa es **`https://code.visualstudio.com/docs/agents/`** y **`https://code.visualstudio.com/docs/agent-customization/`**. **No** `docs.github.com`, que documenta el producto de forma transversal y va por detrás en los detalles del editor.
2. **Planes, políticas de organización y empresa, precedencia de instrucciones, code review, cloud agent, CLI, privacidad, code referencing y métricas**: la fuente autoritativa es **`https://docs.github.com`**, porque son capacidades de la plataforma y no del editor.
3. **Visual Studio** (la IDE): la fuente autoritativa es **`https://learn.microsoft.com/en-us/visualstudio/ide/...`**. No extrapolar nada desde VS Code.

### Si la pregunta es sobre X, consulta Y, porque Z

| Si la pregunta es sobre… | Consulta… | Porque… |
|---|---|---|
| Un atajo de teclado en VS Code | https://code.visualstudio.com/docs/agents/reference/ai-features-cheat-sheet | El equipo de VS Code es el que implementa y publica los keybindings |
| El nombre o la existencia de un slash command o variable de contexto | https://code.visualstudio.com/docs/agents/reference/ai-features-cheat-sheet | La tabla equivalente de GitHub Docs quedó congelada antes de la reorganización |
| Dónde va un archivo de instructions, prompt, agent o skill | https://code.visualstudio.com/docs/agent-customization/overview | Las rutas de filesystem las define el cliente, no la plataforma |
| Qué instrucción gana sobre cuál | https://docs.github.com/en/copilot/concepts/prompting/response-customization | La precedencia es una regla de producto transversal a todos los clientes |
| Qué incluye Business y qué añade Enterprise | https://docs.github.com/en/copilot/get-started/plans | Es la tabla comercial de referencia, mantenida por GitHub |
| Si una política está disponible en mi cliente | https://docs.github.com/en/copilot/reference/supported-surfaces-for-policies | Es la única matriz oficial de política por superficie |
| Cómo se comporta code review o el cloud agent | https://docs.github.com/en/copilot/concepts/agents/code-review y https://docs.github.com/en/copilot/concepts/agents/cloud-agent/about-cloud-agent | Se ejecutan en la plataforma, no en el editor |
| Latencia y contenido de las métricas | https://docs.github.com/en/copilot/concepts/copilot-usage-metrics/copilot-metrics | Los dashboards son de GitHub; ver la contradicción de latencia más abajo |
| Cualquier cosa en Visual Studio | https://learn.microsoft.com/en-us/visualstudio/ide/visual-studio-github-copilot-chat | Visual Studio tiene su propio ciclo de release y su propia nomenclatura |
| Qué modelos puedo elegir | https://docs.github.com/en/copilot/reference/ai-models/supported-models y https://code.visualstudio.com/docs/agent-customization/language-models | El catálogo lo fija GitHub; el selector lo implementa VS Code |

### Contradicciones conocidas entre fuentes oficiales

Citar ambas fuentes en clase y declarar explícitamente que no están resueltas. **No elegir un bando.**

1. **Forma plana vs namespaced de las variables de contexto.** `#codebase` y `#fetch` frente a `#search/codebase` y `#web/fetch`. Ambas formas están publicadas oficialmente en páginas distintas y ambas vigentes: la documentación es internamente inconsistente.
2. **Latencia de las métricas.** "Two full days" en https://docs.github.com/en/copilot/concepts/copilot-usage-metrics/copilot-metrics frente a "up to three full UTC days" en https://docs.github.com/en/rest/copilot/copilot-usage-metrics.
3. **Versión mínima de Visual Studio.** 17.8, 17.10 y 17.14 / VS 2026 conviven en distintas páginas de https://learn.microsoft.com/en-us/visualstudio/ide/.
4. **`AGENTS.md` vs `CLAUDE.md` en VS Code Chat.** https://docs.github.com/en/copilot/reference/custom-instructions-support afirma que VS Code Chat honra solo `AGENTS.md`; https://code.visualstudio.com/docs/agent-customization/custom-instructions documenta soporte completo de `CLAUDE.md` mediante `chat.useClaudeMdFile`. (Relacionado: `GEMINI.md` aparece solo en GitHub Docs.)
5. **Directorio `com.github.copilot/` en Agent Plugins 1.0.** El changelog https://github.blog/changelog/2026-08-12-agent-plugins-1-0-in-vs-code-copilot-cli-and-the-copilot-app/ dice que VS Code carga desde ahí; https://code.visualstudio.com/docs/agent-customization/agent-plugins dice que ignora los datos y directorios de extensión de cliente.
6. **`/help` en VS Code.** Presente en https://docs.github.com/en/copilot/reference/chat-cheat-sheet pero ausente en https://code.visualstudio.com/docs/agents/reference/ai-features-cheat-sheet.

---

## Cómo verificar una afirmación antes de repetirla en clase

Procedimiento obligatorio antes de enunciar cualquier dato técnico no cubierto explícitamente por la ficha técnica.

1. **Localizar la página autoritativa en el dominio correcto.** Aplicar la regla de vigencia anterior: mecánica de VS Code en `code.visualstudio.com/docs/agents/` o `/docs/agent-customization/`; plataforma, planes y políticas en `docs.github.com`; Visual Studio en `learn.microsoft.com/visualstudio/ide/`. Si la única página encontrada está en la lista de obsoletas, la afirmación no se enuncia.
2. **Buscar el bloque "Who can use this feature?"** al inicio de la página de GitHub Docs. Ahí está el gating real por plan y por rol. Si la capacidad exige Business o Enterprise, o un rol de administrador, la afirmación se marca `[Depende del plan/política]`.
3. **Comprobar etiquetas de estado.** Buscar en la página los distintivos "Preview", "Public preview", "Experimental" o notas de disponibilidad limitada. Marcar la capacidad como `[Preview]` o `[GA]` en consecuencia; nunca demostrar en vivo una capacidad `[Preview]` sin plan alterno.
4. **Anotar la fecha de consulta** junto a la URL en este archivo, con el formato `AAAA-MM-DD`. Si se añade una página nueva, se incorpora a la tabla del tema correspondiente con su fecha.
5. **Registrar la discrepancia.** Si lo verificado contradice la ficha técnica, el material del curso o este catálogo, abrir una entrada en `00_control/decisiones_y_supuestos.md` citando ambas URLs, la fecha y la decisión tomada. Corregir después el material afectado; no dejar dos versiones circulando.

---

## Limitaciones metodológicas de esta verificación

Resumen de las advertencias E.28 y E.29 de la ficha técnica. Deben conocerse antes de citar cualquier tabla comparativa como si fuera literal.

1. **Los check marks de varias tablas de `docs.github.com` son imágenes SVG (octicons) y no sobreviven a la extracción de texto.** Afecta a la tabla de content exclusion por IDE, a las tablas de modelos por cliente y por plan, a la tabla de Auto model selection y a la de capacidades extendidas. Los valores de las secciones Customization y Other features de la página Plans se recuperaron desde el fuente `plans.md` (atributos `aria-label="Included"` / `"Not included"`) y por triple corroboración con los bloques "Who can use this feature?" de cada página específica, pero **no constituyen una captura literal del render en vivo**.
2. **El repositorio público `github/docs` va desfasado respecto al sitio en vivo.** Usarlo como sustituto de la página publicada puede introducir datos ya corregidos en producción.
3. **No se pudo verificar en vivo la totalidad de las últimas filas de la tabla de modelos por plan** (Gemini 3.6 y 3.7, GPT-5.6 Luna, Sol y Terra, Grok, Kimi, MAI-Code, Raptor mini). No es posible afirmar con certeza absoluta que no exista algún modelo exclusivo de Enterprise entre ellas.

**Consecuencia práctica.** Cualquier afirmación del curso que dependa de un check mark de una tabla comparativa de `docs.github.com` debe presentarse en clase abriendo la página en vivo, no leyendo el material.
