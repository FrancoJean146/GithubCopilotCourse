# Guía de instalación y verificación del entorno — participantes

**Curso:** GitHub Copilot para Desarrollo de Software · **Stack:** C# / .NET 8 (ASP.NET Core Web API + xUnit) · **IDE principal:** Visual Studio Code
**Fecha de verificación técnica del material:** 14 de agosto de 2026 · **Requisito cubierto:** R-151

> Esta capacitación es una propuesta independiente de carácter corporativo y práctico. No corresponde a un curso oficial de Microsoft, no utiliza su denominación de certificación y no incluye examen ni acreditación oficial.

## 1. Propósito, tiempo y cuándo ejecutar esta guía

Esta guía es **autosuficiente**: al terminarla debes tener un entorno verificado, sin necesidad de que el instructor intervenga. El Día 1 arranca con el laboratorio LAB-01 asumiendo que todo lo de aquí ya funciona.

| Dato | Valor |
|---|---|
| Tiempo estimado | **40 a 60 minutos** (más tiempo de descarga en redes lentas) |
| Cuándo ejecutarla | **Al menos 3 días antes del Día 1** |
| Por qué con 3 días de margen | Los bloqueos típicos (asiento de Copilot sin asignar, proxy corporativo, permisos de instalación, feed NuGet interno) requieren un ticket a TI o a la administración de GitHub, y esos tickets no se resuelven el mismo día |
| Qué debes enviar al terminar | El mensaje de estado de la sección 10, con tu color de semáforo |
| Privilegios necesarios | Ninguno de administrador para la verificación; sí para instalar software (ver sección 2) |

**No se atenderán instalaciones durante el Día 1.** Si llegas en rojo, se te asignará una sesión de soporte previa o trabajarás en modo observador.

## 2. Prerrequisitos y permisos

### 2.1 Cuenta y licencia

| Prerrequisito | Detalle | Cómo se confirma |
|---|---|---|
| Cuenta de GitHub **corporativa** | La cuenta de la organización, no una cuenta personal | Puedes iniciar sesión en `https://github.com` con ella |
| Asiento de **GitHub Copilot Enterprise** asignado por la organización `[Depende del plan/política]` | El curso está construido sobre Copilot Enterprise. No lo compras tú: lo concede la organización | `https://github.com/settings/copilot` muestra el plan **Copilot Enterprise** concedido por la organización |
| Permiso de escritura (push) en `copilot-lab-catalogo` | Cada participante trabaja en su propia rama `feat/<iniciales>-precio-descuento` y abre un Pull Request | `git push` de una rama de prueba funciona |
| Alternativa si no hay permiso de escritura | **Fork + Pull Request** desde tu fork hacia el repositorio original | Pide la URL del repositorio al instructor y usa el botón **Fork** |
| Permisos de instalación de software | Necesarios para Git, VS Code y el .NET 8 SDK | Si tu equipo está bloqueado, abre el ticket a TI **ahora**, no el Día 1 |

### 2.2 Salida a internet requerida

Estos destinos deben estar accesibles desde tu equipo (HTTPS, puerto 443). Si tu organización usa proxy o inspección TLS, entrégale esta lista a TI.

| Destino | Para qué |
|---|---|
| `github.com` | Clonar el repositorio, autenticación, Pull Requests |
| `api.github.com` | API de GitHub usada por VS Code y por la extensión de Pull Requests |
| `api.githubcopilot.com` | **Tráfico de Copilot Chat y de las inline suggestions.** Si está bloqueado, Copilot no responde |
| `api.nuget.org` | `dotnet restore` de los paquetes del repositorio |
| `marketplace.visualstudio.com` | Instalación de extensiones de VS Code |
| `update.code.visualstudio.com` | Actualización de VS Code |

Comprobación rápida de conectividad (no requiere administrador):

```powershell
'github.com','api.github.com','api.githubcopilot.com','api.nuget.org','marketplace.visualstudio.com','update.code.visualstudio.com' |
  ForEach-Object { [pscustomobject]@{ Destino = $_; Alcanzable = (Test-NetConnection -ComputerName $_ -Port 443 -InformationLevel Quiet) } }
```

```bash
for h in github.com api.github.com api.githubcopilot.com api.nuget.org marketplace.visualstudio.com update.code.visualstudio.com; do
  curl -s -o /dev/null -m 10 "https://$h" && echo "OK    $h" || echo "FALLA $h"
done
```

## 3. Instalación de herramientas

Instala las cuatro filas. Las versiones mínimas no son negociables.

| Herramienta | Versión mínima | Windows (winget) | macOS / Linux |
|---|---|---|---|
| **Git** | Cualquiera actual (2.40+) | `winget install --id Git.Git -e` | macOS: `brew install git` · Ubuntu/Debian: `sudo apt-get update && sudo apt-get install git` · Fedora/RHEL: `sudo dnf install git` |
| **Visual Studio Code** | **1.116** (estable en la fecha de verificación: **1.133**) | `winget install --id Microsoft.VisualStudioCode -e` | macOS: `brew install --cask visual-studio-code` · Ubuntu/Debian: repositorio oficial de Microsoft o `sudo snap install code --classic` · Fedora/RHEL: repositorio oficial de Microsoft y `sudo dnf install code` |
| **.NET 8 SDK** | `8.0.x` | `winget install --id Microsoft.DotNet.SDK.8 -e` | macOS: `brew install --cask dotnet-sdk` (**verifica que sea 8.x**; si no, usa el instalador oficial de Microsoft) · Ubuntu/Debian: `sudo apt-get install dotnet-sdk-8.0` o el script `dotnet-install.sh` con `--channel 8.0` · Fedora/RHEL: `sudo dnf install dotnet-sdk-8.0` |
| **GitHub Pull Requests and Issues** (`GitHub.vscode-pull-request-github`) | Última | `code --install-extension GitHub.vscode-pull-request-github` | Idéntico: `code --install-extension GitHub.vscode-pull-request-github` |

Notas de instalación:

- Tras instalar con `winget`, **cierra y vuelve a abrir la terminal**: el `PATH` de la sesión actual no se actualiza solo.
- En macOS/Linux el `brew install --cask dotnet-sdk` puede entregar una versión mayor a la 8. El curso exige que `dotnet --version` empiece por `8.`. Si no coincide, instala el SDK 8 desde el instalador oficial de Microsoft o con `dotnet-install.sh --channel 8.0`.
- En Linux, el paquete `dotnet-sdk-8.0` puede venir del feed de la distribución o del de Microsoft. Ambos sirven; no mezcles los dos en el mismo equipo.

### 3.1 Extensiones adicionales recomendadas

El repositorio del curso trae `.vscode/extensions.json` con estas recomendaciones. VS Code te las ofrecerá al abrir la carpeta:

```text
ms-dotnettools.csdevkit
ms-dotnettools.csharp
GitHub.vscode-pull-request-github
```

| Extensión | Para qué | Advertencia |
|---|---|---|
| `ms-dotnettools.csdevkit` (C# Dev Kit) | Explorador de soluciones, ejecución y depuración de pruebas integradas | **Tiene licencia propia de Microsoft** y no todas las organizaciones la permiten. `[Depende del plan/política]` |
| `ms-dotnettools.csharp` (C#) | Lenguaje, IntelliSense, depuración | Suficiente para todos los laboratorios. **Si tu organización no permite C# Dev Kit, basta con esta** |
| `GitHub.vscode-pull-request-github` | Crear y revisar Pull Requests desde VS Code (Día 4) | Instalación manual, obligatoria |

Instalación en una línea:

```powershell
code --install-extension ms-dotnettools.csharp
code --install-extension ms-dotnettools.csdevkit
code --install-extension GitHub.vscode-pull-request-github
```

## 4. Importante: **no se instala ninguna extensión de Copilot** `[GA]`

Esta es la instrucción que más contradice a las guías que encontrarás buscando en internet. Léela completa.

**Copilot Chat es una extensión built-in de VS Code desde la versión 1.116** (release del **15 de abril de 2026**). Cita literal de las notas de esa release, sección "GitHub Copilot is now built-in":

> "GitHub Copilot Chat is now a built-in extension in VS Code. New users no longer need to install any extension… Copilot is available out of the box as part of the standard VS Code installation."

Y GitHub Docs, en "Installing the GitHub Copilot extension in your environment":

> "When you set up GitHub Copilot in Visual Studio Code for the first time, the required extensions are installed automatically. You don't need to download or install them manually."

| Extension ID | Display name | Qué debes hacer |
|---|---|---|
| `GitHub.copilot-chat` | GitHub Copilot Chat | **Nada.** Ya viene integrada en VS Code ≥ 1.116 |
| `GitHub.copilot` | GitHub Copilot | **Nada, y preferiblemente desinstálala.** Está **en ruta de deprecación** |
| `GitHub.vscode-pull-request-github` | GitHub Pull Requests and Issues | **Sí se instala manualmente** (sección 3) |

La deprecación de `GitHub.copilot` está declarada en el blog oficial "Open Source AI Editor: Second Milestone" (**6 de noviembre de 2025**):

> "We are working towards providing all Copilot functionality in a single VS Code extension: Copilot Chat" · "the GitHub Copilot extension will be deprecated by early 2026, which means it will be removed from the VS Code Marketplace."

La página oficial "Set up GitHub Copilot in VS Code" **ya no contiene ningún paso de instalación de extensión**. El flujo completo hoy es:

1. **Sign in** con tu cuenta de GitHub en VS Code.
2. **Hover** sobre el icono de Copilot en la **Status Bar**.
3. Clic en **Use AI Features**.

> **Nota — por qué muchas guías de internet todavía dicen lo contrario.** Tres razones concurrentes:
>
> 1. La inmensa mayoría de los tutoriales, cursos y vídeos publicados fueron escritos **antes de abril de 2026** y describen un flujo que ya no existe.
> 2. El propio **FAQ de VS Code, fechado el 8 de diciembre de 2026**, aún menciona mantener actualizadas "ambas" extensiones — una frase que **parece obsoleta** frente a la 1.116, pero que sigue publicada y se sigue citando.
> 3. **Ninguna página oficial nombra la release que efectivamente retiró `GitHub.copilot` del Marketplace.** Mientras siga siendo instalable, la confusión se perpetúa. Este punto está marcado como **no verificado** en la ficha técnica del curso.
>
> Regla del curso: si una guía te pide instalar una extensión de Copilot, esa guía está desactualizada. No la sigas.

Instalar `GitHub.copilot` no rompe nada de inmediato ("built-in" no significa "core": la extensión de chat sigue siendo instalable por separado y las instalaciones existentes no se ven afectadas), pero introduce ruido, duplica iconos en la interfaz y hace que las capturas del curso no coincidan con tu pantalla.

## 5. Inicio de sesión en GitHub desde VS Code y verificación del asiento

### 5.1 Iniciar sesión

1. Abre VS Code.
2. En la **Activity Bar** (barra vertical izquierda), en la parte inferior, haz clic en el **icono de cuentas** (Accounts).
3. Elige **Sign in with GitHub to use GitHub Copilot** (o **Sign in**) y completa el flujo en el navegador con tu **cuenta corporativa**.
4. Vuelve a VS Code. En la **Status Bar** (barra inferior) aparecerá el **icono de Copilot**.
5. Haz **hover** sobre ese icono y, si aparece, pulsa **Use AI Features**.

### 5.2 Verificar el asiento en GitHub

Abre `https://github.com/settings/copilot` y comprueba que:

| Debe mostrar | No debe mostrar |
|---|---|
| Plan **Copilot Enterprise**, con indicación de que fue **concedido por la organización** | Un plan personal (Free, Pro, Pro+, Max) como origen de tu acceso |
| Políticas gestionadas por la organización (algunos ajustes aparecerán bloqueados o heredados) | Todos los ajustes editables libremente por ti |

Que algunos ajustes te aparezcan **bloqueados o heredados es lo correcto**: significa que la organización está aplicando sus políticas sobre tu asiento. `[Depende del plan/política]`

### 5.3 Prueba funcional de Copilot Chat `[GA]`

1. Abre la **Chat view** con `Ctrl+Alt+I` (macOS: `⌃⌘I`).
2. Envía un prompt trivial:

```text
Responde con una sola frase: confirma que estas operativo y di que modelo estas usando.
```

3. Debes recibir una respuesta en pocos segundos. **La salida real variará**: no compares literalmente con la de nadie más.

### 5.4 Prueba funcional de las inline suggestions `[GA]`

1. Crea un archivo temporal `prueba.cs` en cualquier carpeta y escribe:

```csharp
// Devuelve el mayor de dos enteros
public static int Mayor(int a, int b)
```

2. Espera al **ghost text** (texto gris) y pulsa `Tab` para **aceptar** la sugerencia.
3. Escribe otra línea, espera otra sugerencia y pulsa `Esc` para **descartarla**.
4. Borra `prueba.cs`. No debe quedar dentro del repositorio del curso.

| Acción | Windows / Linux | macOS |
|---|---|---|
| Abrir Chat view | `Ctrl+Alt+I` | `⌃⌘I` |
| Inline chat | `Ctrl+I` | `⌘I` |
| Aceptar inline suggestion | `Tab` | `Tab` |
| Descartar sugerencia | `Esc` | `Esc` |

### 5.5 Advertencia crítica si tienes varias cuentas de GitHub `[Depende del plan/política]`

Si en tu equipo conviven una cuenta personal y la corporativa, **debe quedar activa la corporativa**. El motivo no es cosmético:

| Plan activo | Uso de tus datos para entrenar modelos |
|---|---|
| **Copilot Business / Copilot Enterprise** | **No.** GitHub Docs: *"GitHub does not use Copilot Business or Copilot Enterprise customer data to train AI models."* El ajuste de opt-out ni siquiera se muestra para estos planes |
| **Copilot Free / Pro / Pro+ / Max** | **Sí, por defecto, desde el 24 de abril de 2026.** GitHub *"may use your interactions… including inputs, outputs, code snippets, and associated context"* para entrenar y mejorar modelos. Existe **opt-out** en Copilot settings → *"Allow GitHub to use my data for AI model training"* → **Disabled** |

Consecuencia práctica: si trabajas el material del curso con una cuenta personal Pro o Pro+ sin haber hecho opt-out, el código del laboratorio pasa a ser material de entrenamiento por defecto. **Usa siempre el asiento corporativo.**

Para comprobar y cambiar la cuenta activa: icono de cuentas en la Activity Bar → verifica el nombre de usuario mostrado → **Sign Out** de la cuenta que no corresponda.

## 6. Configuración de Git

Ejecuta estos comandos en tu terminal (PowerShell, Terminal de macOS o shell de Linux). Son de usuario, no requieren administrador.

```bash
git config --global user.name "Nombre Apellido"
git config --global user.email "correo@empresa.com"
git config --global init.defaultBranch main
```

Y **solo uno** de estos dos, según tu sistema operativo:

```powershell
git config --global core.autocrlf true
```

```bash
git config --global core.autocrlf input
```

| Ajuste | Para qué sirve |
|---|---|
| `user.name` | Es el nombre que aparece como autor en cada commit. Si está vacío, `git commit` falla |
| `user.email` | Vincula tus commits con tu cuenta de GitHub. **Debe coincidir con un correo verificado en tu cuenta de GitHub**; si no, los commits aparecerán sin atribuir y no contarán como tuyos en el Pull Request |
| `init.defaultBranch main` | Hace que los repositorios nuevos usen `main` en lugar de `master`, que es la convención del repositorio del curso |
| `core.autocrlf true` (Windows) | Convierte los finales de línea a CRLF en tu disco y a LF al hacer commit. Evita que un archivo aparezca modificado por completo solo por los saltos de línea |
| `core.autocrlf input` (macOS/Linux) | Deja LF en disco y normaliza a LF en el commit. El equivalente correcto para sistemas Unix |

Verificación:

```bash
git config --global --list
```

## 7. Clonar y verificar el repositorio

Pide al instructor la URL exacta de `copilot-lab-catalogo` y ejecuta, desde una carpeta de trabajo **que no esté sincronizada por OneDrive, Dropbox ni iCloud** (ver sección 9):

```bash
git clone <URL-del-repositorio>
cd copilot-lab-catalogo
dotnet restore CopilotLabCatalogo.sln
dotnet build CopilotLabCatalogo.sln --no-restore
dotnet test CopilotLabCatalogo.sln
```

### 7.1 Resultado esperado exacto

| Comando | Resultado esperado |
|---|---|
| `dotnet restore` | Termina sin error. La primera vez descarga paquetes de `api.nuget.org` |
| `dotnet build` | **Compila con 0 errores y 0 warnings** |
| `dotnet test` | **15 pruebas superadas y 2 omitidas (skipped), 0 fallidas** |

**El conteo correcto es 15 superadas y 2 omitidas.** Dos aclaraciones necesarias, porque este número confunde:

1. **Las 2 omitidas son intencionales.** Están marcadas con `[Fact(Skip = "Habilitar en el Laboratorio 6")]` en el código y se habilitan durante los laboratorios quitando el argumento `Skip`. Una expone BUG-01 y otra cubre TODO-01. **No son un fallo de tu instalación.**
2. **15 es el número de casos de prueba, no de métodos de prueba.** Hay **8 métodos activos**, y **tres** de ellos son `[Theory]` con varios `[InlineData]`; cada `[InlineData]` produce un caso independiente. De ahí 8 métodos → 15 casos ejecutados, más 2 métodos con `Skip`. Si en algún lugar ves la cifra de "8 pruebas", se refiere a métodos; **el resumen de `dotnet test` dirá 15 superadas y 2 omitidas**, que es el número declarado en todo el material.

### 7.2 Comportamientos que NO son fallos de instalación

El repositorio se entrega deliberadamente incompleto: ese es el material de trabajo del curso.

| Observación | Explicación |
|---|---|
| `GET /api/productos` devuelve **500** | Intencional. Es **TODO-02**: `BuscarAsync` lanza `NotImplementedException`. Se implementa en el Día 3 |
| `PATCH /api/productos/{id}/precio` devuelve **404** | Intencional. Es **TODO-01**: el endpoint aún no existe. Es la historia de usuario HU-01 |
| Aparece la cadena `sk-FAKE-DEMO-NOT-A-REAL-SECRET-0000` | Es un secreto **evidentemente falso**, parte del ejercicio de seguridad del Día 4. No da acceso a nada y **no debe reemplazarse por una credencial real** |

Si `dotnet build` produce **algún warning**, algo está mal en tu entorno (versión de SDK equivocada, por ejemplo). Repórtalo. Opcionalmente puedes levantar la API con `dotnet run --project src/Catalogo.Api/Catalogo.Api.csproj`: escucha en `http://localhost:5080` y expone Swagger UI en `http://localhost:5080/swagger`.

## 8. Script de autoverificación

Ejecuta el script desde **la raíz del repositorio clonado** (la carpeta donde está `CopilotLabCatalogo.sln`). No requiere permisos de administrador. Guarda la salida completa: es lo que reportarás en la sección 10.

### 8.1 Windows — PowerShell

Guarda como `verificar-entorno.ps1` y ejecútalo con `powershell -ExecutionPolicy Bypass -File .\verificar-entorno.ps1` (no requiere administrador), o pega el contenido directamente en la terminal.

```powershell
# Verificacion del entorno - Curso GitHub Copilot (C#/.NET 8)
# Ejecutar desde la raiz del repositorio clonado. No requiere administrador.

$script:Ok = 0; $script:Falla = 0
function Registrar { param([string]$Item, [bool]$Exito, [string]$Detalle)
    if ($Exito) { $script:Ok++;    Write-Host ("OK     | {0,-30} | {1}" -f $Item, $Detalle) }
    else        { $script:Falla++; Write-Host ("FALLA  | {0,-30} | {1}" -f $Item, $Detalle) } }
function Informar { param([string]$Item, [string]$Detalle)
    Write-Host ("INFO   | {0,-30} | {1}" -f $Item, $Detalle) }
Write-Host "=== Verificacion del entorno - Curso GitHub Copilot ===`n"

# 1. Git y su configuracion
if (Get-Command git -ErrorAction SilentlyContinue) { Registrar 'git instalado' $true (git --version | Select-Object -First 1) }
else { Registrar 'git instalado' $false 'git no se encuentra en el PATH' }
$gitUserName = git config --global user.name
Registrar 'git user.name' (-not [string]::IsNullOrWhiteSpace($gitUserName)) "$gitUserName"
$gitUserEmail = git config --global user.email
Registrar 'git user.email' (-not [string]::IsNullOrWhiteSpace($gitUserEmail)) "$gitUserEmail"
$gitDefaultBranch = git config --global init.defaultBranch
Registrar 'git init.defaultBranch = main' ($gitDefaultBranch -eq 'main') "valor actual: '$gitDefaultBranch'"
$gitAutoCrlf = git config --global core.autocrlf
Registrar 'git core.autocrlf = true' ($gitAutoCrlf -eq 'true') "valor actual: '$gitAutoCrlf' (en Windows debe ser true)"

# 2. .NET 8 SDK
if (Get-Command dotnet -ErrorAction SilentlyContinue) {
    $dotnetVersion = (dotnet --version | Select-Object -First 1)
    Registrar 'dotnet SDK 8.x' ($dotnetVersion -like '8.*') "dotnet --version = $dotnetVersion"
} else { Registrar 'dotnet SDK 8.x' $false 'dotnet no se encuentra en el PATH' }

# 3. VS Code >= 1.116 y extensiones
if (Get-Command code -ErrorAction SilentlyContinue) {
    $codeVersion = (code --version | Select-Object -First 1)
    $partes = $codeVersion.Split('.'); $mayor = 0; $menor = 0
    [void][int]::TryParse($partes[0], [ref]$mayor)
    if ($partes.Length -gt 1) { [void][int]::TryParse($partes[1], [ref]$menor) }
    Registrar 'VS Code >= 1.116' (($mayor -gt 1) -or (($mayor -eq 1) -and ($menor -ge 116))) "version detectada: $codeVersion"
    $extensiones = @(code --list-extensions)
    Registrar 'ext. vscode-pull-request-github' ($extensiones -contains 'GitHub.vscode-pull-request-github') 'requerida para el Dia 4'
    if ($extensiones -contains 'GitHub.copilot') { Informar 'ext. GitHub.copilot presente' 'Innecesaria (Chat es built-in desde 1.116) y en deprecacion; conviene desinstalarla. No cuenta como fallo' }
    else { Informar 'ext. GitHub.copilot ausente' 'Correcto: no se instala ninguna extension de Copilot' }
} else {
    Registrar 'VS Code >= 1.116' $false "el comando 'code' no esta en el PATH"
    Registrar 'ext. vscode-pull-request-github' $false "no verificable sin el comando 'code'"
}

# 4. Repositorio, compilacion y pruebas
$enRepositorio = Test-Path -Path '.\CopilotLabCatalogo.sln'
Registrar 'raiz del repositorio' $enRepositorio "se busco CopilotLabCatalogo.sln en $((Get-Location).Path)"
if ($enRepositorio) {
    $env:DOTNET_CLI_UI_LANGUAGE = 'en'
    $salidaBuild = dotnet build CopilotLabCatalogo.sln 2>&1 | Out-String
    $codigoBuild = $LASTEXITCODE
    Registrar 'dotnet build sin errores' ($codigoBuild -eq 0) "codigo de salida: $codigoBuild"
    if ($salidaBuild -match '(\d+)\s+Warning\(s\)') { Informar 'warnings de compilacion' "$($Matches[1]) warning(s); lo esperado es 0" }
    $salidaTest = dotnet test CopilotLabCatalogo.sln 2>&1 | Out-String
    $codigoTest = $LASTEXITCODE
    $resumen = [regex]::Match($salidaTest, 'Failed:\s*(\d+),\s*Passed:\s*(\d+),\s*Skipped:\s*(\d+)')
    if ($resumen.Success) {
        $f = [int]$resumen.Groups[1].Value; $p = [int]$resumen.Groups[2].Value; $s = [int]$resumen.Groups[3].Value
        Registrar 'dotnet test = 15 pasa / 2 omite' (($f -eq 0) -and ($p -eq 15) -and ($s -eq 2)) "obtenido: $p pasadas, $s omitidas, $f fallidas"
    } else {
        Registrar 'dotnet test = 15 pasa / 2 omite' ($codigoTest -eq 0) "no se leyo el resumen; verifica manualmente 15/2/0. Codigo de salida: $codigoTest"
    }
} else {
    Registrar 'dotnet build sin errores' $false 'no se ejecuto: no estas en la raiz del repositorio'
    Registrar 'dotnet test = 15 pasa / 2 omite' $false 'no se ejecuto: no estas en la raiz del repositorio'
}

# 5. Informe final
Write-Host "`n=== Informe ==="
Write-Host ("Elementos OK    : {0}" -f $script:Ok)
Write-Host ("Elementos FALLA : {0}" -f $script:Falla)
if ($script:Falla -eq 0)     { Write-Host 'VEREDICTO: VERDE - entorno listo para el Dia 1.' }
elseif ($script:Falla -le 2) { Write-Host "VEREDICTO: AMBAR - hay $($script:Falla) elemento(s) pendiente(s). Revisa la seccion 9 y reporta." }
else                         { Write-Host "VEREDICTO: ROJO  - hay $($script:Falla) elementos pendientes. Contacta al instructor antes del Dia 1." }
```

### 8.2 macOS / Linux — Bash

Guarda como `verificar-entorno.sh`, dale permiso con `chmod +x verificar-entorno.sh` y ejecútalo con `./verificar-entorno.sh`.

```bash
#!/usr/bin/env bash
# Verificacion del entorno - Curso GitHub Copilot (C#/.NET 8)
# Ejecutar desde la raiz del repositorio clonado. No requiere sudo.

OK=0; FALLA=0
registrar() {
  if [ "$2" = "1" ]; then OK=$((OK + 1)); printf 'OK     | %-30s | %s\n' "$1" "$3"
  else FALLA=$((FALLA + 1)); printf 'FALLA  | %-30s | %s\n' "$1" "$3"; fi
}
informar() { printf 'INFO   | %-30s | %s\n' "$1" "$2"; }
printf '=== Verificacion del entorno - Curso GitHub Copilot ===\n\n'

# 1. Git y su configuracion
if command -v git >/dev/null 2>&1; then registrar 'git instalado' 1 "$(git --version)"
else registrar 'git instalado' 0 'git no se encuentra en el PATH'; fi
GIT_NAME="$(git config --global user.name 2>/dev/null)"
[ -n "$GIT_NAME" ] && registrar 'git user.name' 1 "$GIT_NAME" || registrar 'git user.name' 0 'sin configurar'
GIT_EMAIL="$(git config --global user.email 2>/dev/null)"
[ -n "$GIT_EMAIL" ] && registrar 'git user.email' 1 "$GIT_EMAIL" || registrar 'git user.email' 0 'sin configurar'
GIT_BRANCH="$(git config --global init.defaultBranch 2>/dev/null)"
[ "$GIT_BRANCH" = "main" ] && registrar 'git init.defaultBranch = main' 1 "$GIT_BRANCH" || registrar 'git init.defaultBranch = main' 0 "valor actual: '$GIT_BRANCH'"
GIT_CRLF="$(git config --global core.autocrlf 2>/dev/null)"
[ "$GIT_CRLF" = "input" ] && registrar 'git core.autocrlf = input' 1 "$GIT_CRLF" || registrar 'git core.autocrlf = input' 0 "valor actual: '$GIT_CRLF' (en macOS/Linux debe ser input)"

# 2. .NET 8 SDK
if command -v dotnet >/dev/null 2>&1; then
  DOTNET_VERSION="$(dotnet --version 2>/dev/null | head -n 1)"
  case "$DOTNET_VERSION" in
    8.*) registrar 'dotnet SDK 8.x' 1 "dotnet --version = $DOTNET_VERSION" ;;
    *)   registrar 'dotnet SDK 8.x' 0 "dotnet --version = $DOTNET_VERSION (se requiere 8.x)" ;;
  esac
else registrar 'dotnet SDK 8.x' 0 'dotnet no se encuentra en el PATH'; fi

# 3. VS Code >= 1.116 y extensiones
if command -v code >/dev/null 2>&1; then
  CODE_VERSION="$(code --version 2>/dev/null | head -n 1)"
  MAYOR="${CODE_VERSION%%.*}"; RESTO="${CODE_VERSION#*.}"; MENOR="${RESTO%%.*}"
  if [[ "$MAYOR" =~ ^[0-9]+$ && "$MENOR" =~ ^[0-9]+$ ]] && { [ "$MAYOR" -gt 1 ] || { [ "$MAYOR" -eq 1 ] && [ "$MENOR" -ge 116 ]; }; }; then
    registrar 'VS Code >= 1.116' 1 "version detectada: $CODE_VERSION"
  else registrar 'VS Code >= 1.116' 0 "version detectada: $CODE_VERSION"; fi
  EXTENSIONES="$(code --list-extensions 2>/dev/null)"
  if printf '%s\n' "$EXTENSIONES" | grep -qix 'GitHub.vscode-pull-request-github'; then
    registrar 'ext. vscode-pull-request-github' 1 'presente'
  else registrar 'ext. vscode-pull-request-github' 0 'ausente: code --install-extension GitHub.vscode-pull-request-github'; fi
  if printf '%s\n' "$EXTENSIONES" | grep -qix 'GitHub.copilot'; then
    informar 'ext. GitHub.copilot presente' 'Innecesaria (Chat es built-in desde 1.116) y en deprecacion; conviene desinstalarla. No cuenta como fallo'
  else informar 'ext. GitHub.copilot ausente' 'Correcto: no se instala ninguna extension de Copilot'; fi
else
  registrar 'VS Code >= 1.116' 0 "el comando 'code' no esta en el PATH"
  registrar 'ext. vscode-pull-request-github' 0 "no verificable sin el comando 'code'"
fi

# 4. Repositorio, compilacion y pruebas
if [ -f "CopilotLabCatalogo.sln" ]; then
  registrar 'raiz del repositorio' 1 "$(pwd)"
  export DOTNET_CLI_UI_LANGUAGE=en
  SALIDA_BUILD="$(dotnet build CopilotLabCatalogo.sln 2>&1)"; CODIGO_BUILD=$?
  if [ "$CODIGO_BUILD" -eq 0 ]; then registrar 'dotnet build sin errores' 1 "codigo de salida: $CODIGO_BUILD"
  else registrar 'dotnet build sin errores' 0 "codigo de salida: $CODIGO_BUILD"; fi
  WARNINGS="$(printf '%s\n' "$SALIDA_BUILD" | grep -Eo '[0-9]+ Warning\(s\)' | tail -n 1)"
  [ -n "$WARNINGS" ] && informar 'warnings de compilacion' "$WARNINGS (lo esperado es 0)"
  SALIDA_TEST="$(dotnet test CopilotLabCatalogo.sln 2>&1)"; CODIGO_TEST=$?
  RESUMEN="$(printf '%s\n' "$SALIDA_TEST" | grep -Eo 'Failed:[[:space:]]*[0-9]+,[[:space:]]*Passed:[[:space:]]*[0-9]+,[[:space:]]*Skipped:[[:space:]]*[0-9]+' | tail -n 1)"
  if [ -n "$RESUMEN" ]; then
    FALLIDAS="$(printf '%s' "$RESUMEN" | sed -E 's/.*Failed:[[:space:]]*([0-9]+).*/\1/')"
    PASADAS="$(printf  '%s' "$RESUMEN" | sed -E 's/.*Passed:[[:space:]]*([0-9]+).*/\1/')"
    OMITIDAS="$(printf '%s' "$RESUMEN" | sed -E 's/.*Skipped:[[:space:]]*([0-9]+).*/\1/')"
    if [ "$FALLIDAS" -eq 0 ] && [ "$PASADAS" -eq 15 ] && [ "$OMITIDAS" -eq 2 ]; then
      registrar 'dotnet test = 15 pasa / 2 omite' 1 "obtenido: $PASADAS pasadas, $OMITIDAS omitidas, $FALLIDAS fallidas"
    else registrar 'dotnet test = 15 pasa / 2 omite' 0 "obtenido: $PASADAS pasadas, $OMITIDAS omitidas, $FALLIDAS fallidas"; fi
  elif [ "$CODIGO_TEST" -eq 0 ]; then
    registrar 'dotnet test = 15 pasa / 2 omite' 1 'no se leyo el resumen; verifica manualmente 15/2/0'
  else registrar 'dotnet test = 15 pasa / 2 omite' 0 "codigo de salida: $CODIGO_TEST"; fi
else
  registrar 'raiz del repositorio' 0 "no se encontro CopilotLabCatalogo.sln en $(pwd)"
  registrar 'dotnet build sin errores' 0 'no se ejecuto: no estas en la raiz del repositorio'
  registrar 'dotnet test = 15 pasa / 2 omite' 0 'no se ejecuto: no estas en la raiz del repositorio'
fi

# 5. Informe final
echo ""
echo "=== Informe ==="
echo "Elementos OK    : $OK"
echo "Elementos FALLA : $FALLA"
if [ "$FALLA" -eq 0 ]; then
  echo "VEREDICTO: VERDE  - entorno listo para el Dia 1."
elif [ "$FALLA" -le 2 ]; then
  echo "VEREDICTO: AMBAR  - hay $FALLA elemento(s) pendiente(s). Revisa la seccion 9 y reporta."
else
  echo "VEREDICTO: ROJO   - hay $FALLA elementos pendientes. Contacta al instructor antes del Dia 1."
fi
```

> Nota: los scripts fijan `DOTNET_CLI_UI_LANGUAGE=en` para que el resumen de `dotnet test` salga en inglés y el patrón `Failed / Passed / Skipped` sea legible. Sin esa variable, en un sistema en español la salida se localiza y el script no podrá interpretar el resumen automáticamente (te lo indicará y deberás verificarlo a ojo).

## 9. Resolución de problemas de instalación

| Síntoma exacto | Causa probable | Solución paso a paso |
|---|---|---|
| `fatal: unable to access 'https://github.com/...': Failed to connect to github.com port 443` o descargas que se quedan colgadas | Proxy corporativo no configurado en las herramientas | 1. Pide a TI la URL del proxy. 2. `setx HTTP_PROXY http://proxy:8080` y `setx HTTPS_PROXY http://proxy:8080` (macOS/Linux: `export` en `~/.bashrc` o `~/.zshrc`). 3. `git config --global http.proxy http://proxy:8080`. 4. NuGet: edita `%APPDATA%\NuGet\NuGet.Config` (macOS/Linux `~/.nuget/NuGet/NuGet.Config`) y añade en `<config>` las claves `http_proxy` y `http_proxy.user`. 5. VS Code: `Ctrl+,` → busca `http.proxy` y ponla. 6. Reinicia terminal y VS Code |
| `dotnet : El término 'dotnet' no se reconoce...` / `bash: dotnet: command not found` | El SDK no está instalado, o su directorio no está en el `PATH` de la sesión actual | 1. Cierra y reabre la terminal (tras `winget`, el `PATH` no se refresca en sesiones abiertas). 2. Si persiste, comprueba que exista `C:\Program Files\dotnet\dotnet.exe` (macOS/Linux `/usr/local/share/dotnet` o `~/.dotnet`). 3. Añade esa ruta al `PATH` del usuario. 4. Reinstala con el comando de la sección 3 si el directorio no existe |
| VS Code abierto y **no aparece el icono de Copilot en la Status Bar**; tampoco hay Chat view con `Ctrl+Alt+I` | VS Code es anterior a **1.116**, cuando Copilot Chat pasó a ser built-in | 1. **Help → About** y anota la versión. 2. Si es menor que 1.116: **Help → Check for Updates** o reinstala con `winget install --id Microsoft.VisualStudioCode -e`. 3. Reinicia VS Code. 4. **No** instales ninguna extensión de Copilot como sustituto: no es la solución y el curso no la contempla |
| Copilot responde, pero en `https://github.com/settings/copilot` ves un plan personal, o los laboratorios de gobierno no coinciden | En VS Code está activa una cuenta de GitHub distinta de la corporativa | 1. Activity Bar → icono de cuentas → mira el usuario mostrado. 2. **Sign Out** de la cuenta personal. 3. **Sign in with GitHub** con la cuenta corporativa. 4. Recarga con Command Palette → **Developer: Reload Window**. 5. Reverifica en `https://github.com/settings/copilot` que diga **Copilot Enterprise** concedido por la organización |
| `You don't have access to GitHub Copilot` o el icono de Copilot ofrece contratar un plan | La organización no te ha asignado un asiento de Copilot Enterprise `[Depende del plan/política]` | 1. Abre `https://github.com/settings/copilot` y confirma que no hay asiento. 2. Solicita la asignación al administrador de GitHub de tu organización indicando tu usuario de GitHub. 3. Tras la asignación, en VS Code haz **Sign Out** y **Sign in** de nuevo. **Esto puede tardar días: pídelo ya** |
| `fatal: unable to access '...': SSL certificate problem: unable to get local issuer certificate` | Inspección TLS corporativa: el proxy reemplaza el certificado y Git no confía en la CA interna | 1. Pide a TI el certificado raíz corporativo en formato `.pem`/`.crt`. 2. `git config --global http.sslCAInfo "C:/ruta/ca-corporativa.pem"` (macOS/Linux, ruta equivalente). 3. En Windows, alternativa: `git config --global http.sslBackend schannel` para que Git use el almacén de certificados del sistema. 4. **Nunca uses `git config --global http.sslVerify false`**: desactiva la validación de identidad del servidor en todos tus repositorios y te deja expuesto a interceptación; además enmascara el problema en lugar de resolverlo |
| `error NU1301: Unable to load the service index for source https://api.nuget.org/v3/index.json` | El feed público de NuGet está bloqueado o la organización impone un feed interno | 1. Comprueba la conectividad con `api.nuget.org` (sección 2.2). 2. Si hay proxy, aplícalo también a NuGet (`%APPDATA%\NuGet\NuGet.Config`). 3. Si la organización usa un feed interno (Azure Artifacts u otro), pide la URL y las credenciales y añádela: `dotnet nuget add source <URL> -n corporativo -u <usuario> -p <token> --store-password-in-clear-text`. 4. Repite `dotnet restore CopilotLabCatalogo.sln` |
| `winget : El término 'winget' no se reconoce...` | Falta **App Installer** o la versión de Windows no lo trae | 1. Instala **App Installer** desde Microsoft Store. 2. Si la Store está bloqueada, descarga los instaladores oficiales de Git, VS Code y .NET 8 SDK desde sus sitios y ejecútalos. 3. Si tampoco tienes permiso de instalación, escala el ticket a TI de inmediato |
| `code : El término 'code' no se reconoce...` (Windows) o `zsh: command not found: code` (macOS) | VS Code se instaló solo para el usuario o por máquina sin marcar la opción de `PATH`; en macOS el comando `code` no se registra automáticamente | 1. **macOS:** abre VS Code → Command Palette (`⇧⌘P`) → **Shell Command: Install 'code' command in PATH**. 2. **Windows:** reinstala VS Code marcando *"Add to PATH"*, o añade manualmente `%LOCALAPPDATA%\Programs\Microsoft VS Code\bin` (instalación por usuario) o `C:\Program Files\Microsoft VS Code\bin` (por máquina) al `PATH`. 3. Reabre la terminal |
| Copilot Chat responde con un error de red o queda cargando indefinidamente, pero `github.com` sí abre en el navegador | El firewall bloquea específicamente `api.githubcopilot.com` | 1. Verifica con la comprobación de conectividad de la sección 2.2. 2. Si ese destino falla, solicita a TI que lo añada a la lista de permitidos junto con `api.github.com`. 3. Si hay proxy con inspección TLS, verifica también que el certificado corporativo esté instalado en el almacén del sistema. 4. Reinicia VS Code tras el cambio |
| `dotnet test` informa `No test is available` o **0 pruebas ejecutadas** | El SDK 8 no está presente, o apuntaste a la solución/proyecto equivocado | 1. `dotnet --list-sdks` y confirma que aparece un `8.0.x`. 2. Confirma que estás en la raíz del repositorio (`CopilotLabCatalogo.sln` debe existir). 3. Ejecuta exactamente `dotnet test CopilotLabCatalogo.sln`. 4. Si sigue en 0, `dotnet clean` + `dotnet restore` + `dotnet build` y repite |
| `The current .NET SDK does not support targeting .NET 8.0` o se usa un SDK distinto del esperado | Hay varias versiones del SDK instaladas y un `global.json` (tuyo o heredado de una carpeta superior) fija otra | 1. `dotnet --list-sdks` para ver todas. 2. Busca `global.json` en la carpeta del repositorio y en todas sus carpetas padre. 3. Si es tuyo y sobra, elimínalo; si lo necesitas, ajusta `"version"` a la `8.0.x` instalada con `"rollForward": "latestFeature"`. 4. Mueve el repositorio fuera de un árbol que contenga un `global.json` ajeno |
| El icono de Copilot aparece pero la Chat view muestra que la funcionalidad está deshabilitada por la organización | Política de organización o empresa **"Copilot Chat in the IDE"** desactivada `[Depende del plan/política]` | 1. Confirma en `https://github.com/settings/copilot` qué políticas te aplican. 2. Solicita al administrador de GitHub que habilite la política **"Copilot Chat in the IDE"** para tu organización. 3. Recuerda que los cambios de política pueden tardar en propagarse a IDEs ya abiertos; usa Command Palette → **Developer: Reload Window** |
| `error MSB3021: Unable to copy file... The process cannot access the file because it is being used by another process` sobre archivos de `obj/` o `bin/` | El repositorio está dentro de una carpeta sincronizada por OneDrive, Dropbox o iCloud, y el cliente de sincronización bloquea los artefactos de compilación | 1. Cierra VS Code. 2. **Mueve el repositorio fuera de la carpeta sincronizada** (por ejemplo a `C:\dev\copilot-lab-catalogo`). 3. Borra las carpetas `obj/` y `bin/`. 4. Vuelve a ejecutar `dotnet restore` y `dotnet build`. Es la solución recomendada; pausar la sincronización solo lo mitiga temporalmente |
| `remote: Permission to <org>/copilot-lab-catalogo.git denied` al hacer `git push` | No tienes permiso de escritura en el repositorio | 1. Solicita permiso de escritura al instructor. 2. Alternativa inmediata: haz **Fork** del repositorio en GitHub, `git remote set-url origin <URL-de-tu-fork>`, trabaja ahí y abre el Pull Request desde tu fork hacia el repositorio original |
| Tus commits aparecen en GitHub sin foto ni enlace a tu usuario | El `user.email` de Git no coincide con ningún correo verificado en tu cuenta de GitHub | 1. Revisa tus correos en `https://github.com/settings/emails`. 2. Ajusta `git config --global user.email "<correo verificado>"`. 3. Los commits ya creados conservarán el correo anterior: rehazlos o continúa, según indique el instructor |
| `warning: LF will be replaced by CRLF` en cada `git add`, o un archivo aparece modificado por completo sin haberlo tocado | `core.autocrlf` mal configurado para tu sistema operativo | 1. Windows: `git config --global core.autocrlf true`. 2. macOS/Linux: `git config --global core.autocrlf input`. 3. Descarta los cambios espurios con `git checkout -- .` y vuelve a comprobar con `git status` |
| VS Code pide aceptar una licencia al activar **C# Dev Kit**, o TI lo prohíbe | `ms-dotnettools.csdevkit` tiene licencia propia de Microsoft `[Depende del plan/política]` | 1. Si tu organización no la permite, desinstala `ms-dotnettools.csdevkit`. 2. Deja instalada `ms-dotnettools.csharp`: es suficiente para todos los laboratorios del curso. 3. Las pruebas se ejecutarán desde la terminal con `dotnet test`, no desde el Test Explorer |

## 10. Semáforo de autoevaluación y mensaje al instructor

### 10.1 Criterios objetivos

| Color | Criterios (todos los de la fila deben cumplirse) | Qué significa |
|---|---|---|
| **Verde** | `git --version` responde · `user.name`, `user.email`, `init.defaultBranch=main` y `core.autocrlf` configurados · `dotnet --version` empieza por `8.` · `code --version` ≥ **1.116** · `GitHub.vscode-pull-request-github` instalada · repositorio clonado · `dotnet build` con **0 errores y 0 warnings** · `dotnet test` = **15 superadas / 2 omitidas / 0 fallidas** · Chat view responde a un prompt trivial · inline suggestion aceptada con `Tab` · `https://github.com/settings/copilot` muestra **Copilot Enterprise** de la organización | Listo para el Día 1. No hay nada que hacer |
| **Ámbar** | Todo lo anterior **salvo uno o dos elementos**, y ninguno de los faltantes es el asiento de Copilot Enterprise ni el .NET 8 SDK | Puedes seguir el Día 1, pero avisa: se resolverá en los 10 primeros minutos o en soporte previo |
| **Rojo** | Falta el **asiento de Copilot Enterprise**, o falta el **.NET 8 SDK**, o `dotnet test` no llega a 15/2/0, o hay **3 o más** elementos en FALLA | No podrás seguir los laboratorios. **Contacta al instructor antes del Día 1** |

El script de la sección 8 imprime este veredicto automáticamente al final.

### 10.2 Mensaje que debes enviar antes del Día 1

Copia la plantilla, rellénala y envíala por el canal indicado en la sección 11. Envíala **aunque estés en verde**.

```text
ESTADO DE PREPARACION - Curso GitHub Copilot

Nombre completo      :
Correo corporativo   :
Usuario de GitHub    :
Sistema operativo    : (Windows 11 / macOS <version> / Ubuntu <version>)

git --version        :
dotnet --version     :
code --version       : (primera linea)
Extension GitHub.vscode-pull-request-github instalada: (si / no)
Extension GitHub.copilot presente:                     (si / no)

Repositorio clonado  : (si / no)
dotnet build         : (0 errores y 0 warnings / detalle del problema)
dotnet test          : (ej. 15 superadas, 2 omitidas, 0 fallidas)

Plan mostrado en https://github.com/settings/copilot :
  (Copilot Enterprise concedido por la organizacion / otro: cual)
Chat view responde a un prompt trivial : (si / no)
Inline suggestion aceptada con Tab     : (si / no)

COLOR DE SEMAFORO    : (VERDE / AMBAR / ROJO)
BLOQUEO (si lo hay)  :
  (describe el sintoma exacto y pega el mensaje de error literal)
```

## 11. Cierre: qué llevar al Día 1 y a quién escribir

Al conectarte al Día 1 debes tener, ya abierto y funcionando:

- VS Code **1.116 o superior**, con la sesión de GitHub iniciada con la **cuenta corporativa** y el icono de Copilot visible en la Status Bar.
- El repositorio `copilot-lab-catalogo` clonado, compilado (0 warnings) y con `dotnet test` en **15 superadas / 2 omitidas / 0 fallidas**.
- Una terminal abierta en la raíz del repositorio.
- La salida del script de autoverificación guardada, por si hay que diagnosticar algo en vivo.
- Tu color de semáforo ya reportado.

Si algo no funciona:

| Situación | A quién escribir |
|---|---|
| Asiento de Copilot Enterprise no asignado, o política de organización que bloquea Copilot Chat en el IDE | Administrador de GitHub de tu organización, con copia al instructor |
| Proxy, firewall, certificados, feed NuGet interno, permisos de instalación | Mesa de servicio de TI, adjuntando la lista de destinos de la sección 2.2 |
| Permiso de escritura en el repositorio, dudas sobre el material, resultados de `dotnet test` distintos de 15/2/0 | Instructor del curso, por el canal de dudas del curso |

Reporta con el mensaje de la sección 10.2 y **pega el mensaje de error literal**. Un síntoma exacto se diagnostica en minutos; "no me funciona" no.
