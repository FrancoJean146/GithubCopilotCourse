#!/usr/bin/env bash
# Verifica las dos copias del repositorio de practica: restore, build (con advertencias como errores)
# y pruebas. Devuelve un codigo de salida distinto de 0 si alguna copia falla.
set -uo pipefail

RAIZ="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
COPIAS=("inicial" "resuelto")
declare -A RESULTADO
FALLOS=0

if ! command -v dotnet >/dev/null 2>&1; then
    echo "ERROR: no se encontro el comando 'dotnet'. Instala el .NET 8 SDK." >&2
    exit 127
fi

echo "SDK de .NET detectado: $(dotnet --version)"

for COPIA in "${COPIAS[@]}"; do
    RUTA="$RAIZ/$COPIA"
    SOLUCION="$RUTA/CopilotLabCatalogo.sln"

    echo ""
    echo "=================================================================="
    echo " Copia: $COPIA"
    echo "=================================================================="

    if [ ! -f "$SOLUCION" ]; then
        echo "ERROR: no existe $SOLUCION"
        RESULTADO[$COPIA]="FALLO (solucion no encontrada)"
        FALLOS=$((FALLOS + 1))
        continue
    fi

    ESTADO="OK"

    echo "--- dotnet restore ---"
    if ! dotnet restore "$SOLUCION"; then
        ESTADO="FALLO en restore"
    fi

    if [ "$ESTADO" = "OK" ]; then
        echo "--- dotnet build --no-restore -warnaserror ---"
        if ! dotnet build "$SOLUCION" --no-restore -warnaserror; then
            ESTADO="FALLO en build"
        fi
    fi

    if [ "$ESTADO" = "OK" ]; then
        echo "--- dotnet test --no-build ---"
        if ! dotnet test "$SOLUCION" --no-build; then
            ESTADO="FALLO en test"
        fi
    fi

    RESULTADO[$COPIA]="$ESTADO"
    if [ "$ESTADO" != "OK" ]; then
        FALLOS=$((FALLOS + 1))
    fi
done

echo ""
echo "=================================================================="
echo " RESUMEN"
echo "=================================================================="
for COPIA in "${COPIAS[@]}"; do
    printf ' %-10s %s\n' "$COPIA" "${RESULTADO[$COPIA]:-NO EJECUTADO}"
done

if [ "$FALLOS" -ne 0 ]; then
    echo ""
    echo "Resultado global: FALLO ($FALLOS copia(s) con problemas)"
    exit 1
fi

echo ""
echo "Resultado global: TODO CORRECTO"
echo "Recordatorio: en 'inicial' se esperan 15 pruebas superadas y 2 omitidas (8 metodos de prueba activos, tres de ellos [Theory] con varios casos, y 2 metodos con Skip)."
exit 0
