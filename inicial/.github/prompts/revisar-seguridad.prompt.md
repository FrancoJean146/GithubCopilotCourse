---
name: "Revisar seguridad"
description: "Revisa un archivo buscando validación de entradas, secretos expuestos, manejo de errores y riesgos de dependencias."
agent: ask
---

# Revisión de seguridad de un archivo

Revisa el archivo indicado (selección activa o `#file`) y produce un informe **en español**.

## Qué debes buscar

1. **Validación de entradas**
   - Parámetros públicos sin validar, rangos, longitudes, valores nulos, enums fuera de rango.
   - Datos que llegan del exterior y se usan sin normalizar.
2. **Secretos y datos sensibles**
   - Claves, tokens, contraseñas o cadenas de conexión en código o en `appsettings.*.json`.
   - Datos sensibles escritos en logs.
3. **Manejo de errores**
   - `catch` vacíos o que ocultan la excepción, excepciones genéricas, mensajes de error que
     filtran detalles internos al cliente.
   - Ausencia de traducción a `ProblemDetails`.
4. **Dependencias y superficie de ataque**
   - Paquetes innecesarios, llamadas de red sin tiempo de espera, deserialización insegura,
     concurrencia sin protección sobre estado compartido.

## Formato de salida

| # | Severidad (Alta/Media/Baja) | Hallazgo | Archivo:línea | Corrección propuesta |
| --- | --- | --- | --- | --- |

Después de la tabla:

- **Correcciones prioritarias**: las tres más importantes, con el fragmento de código corregido.
- **Falsos positivos descartados**: qué revisó y por qué no es un problema.

## Reglas

- No apliques cambios: este prompt solo produce el informe.
- Si encuentras un secreto, **no lo repitas completo** en la respuesta: indica el archivo y la línea.
- Clasifica como Alta únicamente lo explotable desde fuera del proceso.
