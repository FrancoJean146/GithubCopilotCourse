<#
.SYNOPSIS
    Verifica las dos copias del repositorio de practica (inicial y resuelto).
.DESCRIPTION
    Ejecuta dotnet restore, dotnet build --no-restore -warnaserror y dotnet test --no-build en cada
    copia e imprime un resumen. Termina con codigo de salida 1 si alguna copia falla.
#>
$ErrorActionPreference = 'Continue'

$raiz = Split-Path -Parent $MyInvocation.MyCommand.Path
$copias = @('inicial', 'resuelto')
$resultados = [ordered]@{}
$fallos = 0

if (-not (Get-Command dotnet -ErrorAction SilentlyContinue)) {
    Write-Error "No se encontro el comando 'dotnet'. Instala el .NET 8 SDK."
    exit 127
}

Write-Host "SDK de .NET detectado: $(dotnet --version)"

foreach ($copia in $copias) {
    $solucion = Join-Path $raiz "$copia\CopilotLabCatalogo.sln"

    Write-Host ''
    Write-Host '=================================================================='
    Write-Host " Copia: $copia"
    Write-Host '=================================================================='

    if (-not (Test-Path $solucion)) {
        Write-Host "ERROR: no existe $solucion"
        $resultados[$copia] = 'FALLO (solucion no encontrada)'
        $fallos++
        continue
    }

    $estado = 'OK'

    Write-Host '--- dotnet restore ---'
    dotnet restore $solucion
    if ($LASTEXITCODE -ne 0) { $estado = 'FALLO en restore' }

    if ($estado -eq 'OK') {
        Write-Host '--- dotnet build --no-restore -warnaserror ---'
        dotnet build $solucion --no-restore -warnaserror
        if ($LASTEXITCODE -ne 0) { $estado = 'FALLO en build' }
    }

    if ($estado -eq 'OK') {
        Write-Host '--- dotnet test --no-build ---'
        dotnet test $solucion --no-build
        if ($LASTEXITCODE -ne 0) { $estado = 'FALLO en test' }
    }

    $resultados[$copia] = $estado
    if ($estado -ne 'OK') { $fallos++ }
}

Write-Host ''
Write-Host '=================================================================='
Write-Host ' RESUMEN'
Write-Host '=================================================================='
foreach ($copia in $copias) {
    $estado = if ($resultados.Contains($copia)) { $resultados[$copia] } else { 'NO EJECUTADO' }
    Write-Host (' {0,-10} {1}' -f $copia, $estado)
}

if ($fallos -ne 0) {
    Write-Host ''
    Write-Host "Resultado global: FALLO ($fallos copia(s) con problemas)"
    exit 1
}

Write-Host ''
Write-Host 'Resultado global: TODO CORRECTO'
Write-Host "Recordatorio: en 'inicial' se esperan 15 pruebas superadas y 2 omitidas (8 metodos de prueba activos, tres de ellos [Theory] con varios casos, y 2 metodos con Skip)."
exit 0
