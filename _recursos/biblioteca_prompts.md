# Biblioteca de prompts técnicos

**Curso:** GitHub Copilot para Desarrollo de Software
**Repositorio de referencia:** `copilot-lab-catalogo` — C# / .NET 8, ASP.NET Core Web API + xUnit
**Requisitos contractuales cubiertos:** R-063 y R-153
**Fecha de verificación técnica:** 2026-08-14

Esta biblioteca contiene **49 prompts técnicos** listos para copiar, específicos del repositorio y del stack del curso. No son prompts genéricos de internet: cada uno nombra archivos, tipos y reglas que existen en `05_codigo_laboratorios/inicial/`.

**Cómo se usa.** Cada prompt lleva un identificador `P-CAT-NN`. Los marcadores `<...>` señalan lo que el participante sustituye antes de enviar. Todo lo demás se copia literal. Los prompts están escritos para **Copilot Chat en VS Code** con el harness **Local** y el agent role indicado en cada caso; donde el agent role importa, se dice explícitamente.

> **Advertencia permanente.** La salida real de Copilot **variará** entre ejecuciones, entre modelos y entre versiones del producto. Ningún apartado "Qué verificar en la salida" describe lo que el modelo va a responder: describe lo que el participante debe comprobar antes de aceptar nada.

---

## Índice

| Categoría | Prefijo | Prompts | Módulo principal |
|---|---|---|---|
| Comprensión de código | `P-COM` | 6 | M2 |
| Documentación | `P-DOC` | 5 | M2 |
| Planificación e implementación | `P-PLA` | 7 | M3 |
| Refactorización | `P-REF` | 5 | M3 |
| Depuración | `P-DEP` | 5 | M3 |
| Pruebas | `P-TST` | 6 | M4 |
| Seguridad | `P-SEC` | 5 | M4 |
| Pull Requests y revisión | `P-PRR` | 5 | M4 |
| Personalización y gobierno | `P-GOB` | 5 | M5 |
| **Total** | | **49** | |

---

## Plantilla de prompt técnico de cinco componentes

Todo prompt técnico del curso se construye con estos cinco componentes, en este orden. Un prompt al que le falte alguno produce una salida que **no se puede evaluar**, y una salida que no se puede evaluar no se puede aceptar.

| # | Componente | Qué responde | Error típico si falta |
|---|---|---|---|
| 1 | **Contexto** | Sobre qué código, con qué stack, en qué estado | Copilot inventa un proyecto genérico y propone paquetes que no están en la solución |
| 2 | **Tarea** | Qué hay que hacer, con un verbo único y observable | La respuesta divaga entre explicar, proponer y editar sin hacer ninguna de las tres |
| 3 | **Restricciones** | Qué no se puede tocar, qué convenciones rigen, qué está prohibido | Se modifican pruebas existentes, se añaden paquetes NuGet, se cambia de framework |
| 4 | **Formato de salida** | Cómo debe venir la respuesta: tabla, diff, archivo completo, lista numerada | La respuesta es un ensayo imposible de aplicar o de comparar entre iteraciones |
| 5 | **Criterios de aceptación** | Cómo se sabrá si la respuesta sirve | Se acepta lo primero que compila |

### Esqueleto reutilizable

```text
# Contexto
Repositorio `copilot-lab-catalogo`: API REST de catalogo de productos en .NET 8
(ASP.NET Core Web API con controllers). Capas: Controllers -> Services -> Repositories
(en memoria, 12 productos de semilla). Pruebas con xUnit, FluentAssertions y Moq.
Archivo o area de trabajo: <ruta exacta>.
Estado actual: <que compila, que falla, que esta pendiente>.

# Tarea
<Un solo verbo en imperativo y su objeto: "Explica", "Genera", "Refactoriza",
"Corrige", "Documenta", "Analiza". Una sola tarea por prompt.>

# Restricciones
- Framework de destino: net8.0. No propongas APIs de versiones posteriores.
- No modifiques ni elimines pruebas existentes. Si una prueba falla, el defecto esta
  en el codigo de produccion.
- No agregues paquetes NuGet nuevos. Si crees que hace falta uno, dilo y detente.
- Respeta `.github/copilot-instructions.md` y `.github/instructions/*.instructions.md`.
- <restricciones especificas de esta tarea>

# Formato de salida
<tabla con estas columnas | diff unificado | archivo completo con su ruta |
lista numerada de pasos | informe con estos titulos exactos>

# Criterios de aceptacion
1. <criterio verificable con un comando o una inspeccion concreta>
2. <criterio verificable>
3. Si algo no se puede determinar con el contexto disponible, escribelo
   explicitamente en lugar de suponerlo.
```

### Refinamiento iterativo

Un prompt técnico casi nunca acierta a la primera. El ciclo del curso es:

1. **Enviar** el prompt de cinco componentes.
2. **Evaluar** la salida contra los criterios de aceptación que se escribieron antes.
3. **Refinar** señalando la desviación concreta, no repitiendo la petición entera. Ejemplo: *"La implementación aplica el tope antes de sumar el descuento de cliente preferente. El tope se aplica después de sumar. Corrígelo y muéstrame solo el método afectado."*
4. **Detenerse** cuando dos iteraciones seguidas no mejoren: en ese punto es más rápido escribirlo a mano, y esa también es una decisión que se registra en la bitácora.

---

## Categoría 1 — Comprensión de código (`P-COM`)

### P-COM-01 — Explicación estructurada de un módulo

- **Objetivo:** obtener una descripción verificable de un archivo desconocido, con estructura fija y comparable entre módulos.
- **Cuándo usarlo:** primera aproximación a un archivo que no se escribió uno mismo. Módulo 2, LAB-03 y LAB-04.
- **Contexto que hay que adjuntar:** el archivo objetivo (arrástralo al chat o usa `#<archivo>`). No adjuntes toda la solución: encarece la respuesta y la diluye.
- **Agent role:** `Agent` en modo de solo lectura, o el prompt file `explicar-modulo`.

```text
# Contexto
Repositorio `copilot-lab-catalogo`, API REST de catalogo en .NET 8.
Archivo a analizar: <src/Catalogo.Api/Services/DescuentoCalculator.cs>.

# Tarea
Explica este archivo respetando exactamente los cinco titulos indicados abajo.

# Restricciones
- No inventes tipos, metodos ni archivos: cita solo lo que existe en el codigo adjunto.
- Si algo no se puede determinar con el contexto disponible, escribe
  "No determinable con el contexto actual" en lugar de suponerlo.
- No propongas cambios de framework ni de arquitectura general.
- No apliques ninguna edicion: este prompt solo produce texto.

# Formato de salida
## 1. Proposito (una o dos frases)
## 2. Dependencias (tabla: Dependencia | Entrante/Saliente | Para que se usa)
## 3. Flujo principal (lista numerada de una llamada tipica, con los tipos que intervienen)
## 4. Riesgos (maximo cinco; cada uno con archivo y linea aproximada)
## 5. Oportunidades de mejora (maximo cinco, ordenadas por relacion valor/esfuerzo)

# Criterios de aceptacion
1. Cada linea citada existe realmente en el archivo.
2. La seccion de riesgos cubre al menos validacion de entradas y manejo de errores.
3. Cada oportunidad de mejora propone un cambio concreto y verificable.
```

- **Qué verificar en la salida:** abre el archivo y comprueba **una por una** las líneas citadas. Comprueba que los nombres de métodos existen tal cual. Si la explicación menciona un tipo que no está en el archivo ni en sus `using`, es una alucinación y hay que descartar la respuesta completa, no solo esa frase.
- **Advertencia:** el número de línea es el dato que Copilot equivoca con más frecuencia, incluso cuando el resto de la explicación es correcta. Nunca cites en un informe una línea que no hayas abierto.

### P-COM-02 — Reconstruir las reglas de negocio a partir del código

- **Objetivo:** extraer del código las reglas de negocio implícitas y contrastarlas con la especificación escrita.
- **Cuándo usarlo:** antes de tocar `DescuentoCalculator`, y en general antes de modificar cualquier cálculo heredado. Módulo 2 y 3.
- **Contexto que hay que adjuntar:** el archivo con la lógica y, en un segundo mensaje, el fragmento del `README.md` con las reglas declaradas.

```text
# Contexto
Archivo: <src/Catalogo.Api/Services/DescuentoCalculator.cs>.

# Tarea
Deduce, exclusivamente a partir del codigo adjunto, las reglas de negocio que
implementa. No consultes ninguna otra fuente y no uses lo que "deberia" hacer un
calculador de descuentos.

# Restricciones
- Describe lo que el codigo hace, no lo que parece que quiere hacer.
- Presta atencion especial a los operadores de comparacion y a su inclusividad.
- No propongas correcciones en esta respuesta.

# Formato de salida
Tabla: Regla | Condicion exacta en el codigo | Linea | Valor resultante
Despues de la tabla, una seccion "Comportamiento en los valores frontera" con el
resultado para cantidad = 9, 10, 11, 49, 50, 51, 99, 100 y 101, con precioBase = 100
y esClientePreferente = false, y otra vez con esClientePreferente = true.

# Criterios de aceptacion
1. Cada fila indica el operador literal (`>`, `>=`) que aparece en el codigo.
2. La tabla de valores frontera esta calculada, no descrita en prosa.
3. Si el codigo no aplica algun limite o tope, se declara explicitamente su ausencia.
```

- **Qué verificar en la salida:** calcula a mano dos de los valores frontera y compáralos. Comprueba que la tabla distingue `>` de `>=`. **Después** contrasta la tabla con las reglas del `README.md`: las diferencias que aparezcan son BUG-01.
- **Advertencia:** si el prompt menciona la especificación correcta, el modelo tiende a describir la especificación en lugar del código y el defecto desaparece de la respuesta. Por eso este prompt prohíbe explícitamente consultar otra fuente. Es el prompt que hace visible BUG-01.

### P-COM-03 — Mapa de la solución con `#codebase`

- **Objetivo:** orientarse en el repositorio completo antes de decidir dónde tocar.
- **Cuándo usarlo:** al inicio del Módulo 2 y siempre que una tarea afecte a más de una capa.
- **Contexto que hay que adjuntar:** `#codebase` (forma namespaced `#search/codebase`). En Visual Studio el equivalente es `@workspace`; en VS Code `@workspace` ya no existe.

```text
#codebase

# Contexto
Repositorio `copilot-lab-catalogo`, solucion `CopilotLabCatalogo.sln` con dos proyectos:
`src/Catalogo.Api` y `tests/Catalogo.Api.Tests`.

# Tarea
Construye un mapa de la solucion que me permita decidir donde implementar
<PATCH /api/productos/{id}/precio>.

# Restricciones
- Cita rutas de archivo reales; no describas carpetas que no existan.
- No edites nada.

# Formato de salida
1. Tabla: Capa | Carpeta | Tipos publicos que contiene | Responsabilidad en una frase
2. Tabla: Interfaz | Implementacion | Donde se registra en la inyeccion de dependencias
3. Lista de los puntos del codigo marcados con `TODO-` o pendientes de implementar,
   con archivo y linea
4. Recomendacion: en que archivos habria que tocar para la tarea indicada y en que orden

# Criterios de aceptacion
1. Los registros de inyeccion de dependencias citados coinciden con `Program.cs`.
2. La lista de pendientes incluye los que existen y ninguno inventado.
```

- **Qué verificar en la salida:** abre `Program.cs` y compara los registros. Ejecuta `git grep -n "TODO-"` y compara la lista.
- **Advertencia:** `#codebase` hace una búsqueda sobre el workspace, no lee el repositorio entero: en soluciones grandes puede omitir archivos relevantes. Su resultado es una hipótesis de partida, no un inventario.

### P-COM-04 — Traza de una petición de extremo a extremo

- **Objetivo:** entender el recorrido completo de una llamada HTTP antes de modificarla.
- **Cuándo usarlo:** Módulo 2 y 3, antes de LAB-05.
- **Contexto que hay que adjuntar:** `ProductosController.cs`, `ProductoService.cs`, `InMemoryProductoRepository.cs` y `Program.cs`.

```text
# Contexto
Archivos adjuntos: ProductosController.cs, ProductoService.cs,
InMemoryProductoRepository.cs y Program.cs del repositorio `copilot-lab-catalogo`.

# Tarea
Traza el recorrido completo de la peticion <GET /api/productos/1> desde que entra
en el pipeline de ASP.NET Core hasta que se serializa la respuesta.

# Restricciones
- Nombra el tipo y el metodo exactos en cada salto.
- Indica donde se decide cada codigo de estado posible.
- No asumas la existencia de middlewares que no esten registrados en `Program.cs`.

# Formato de salida
Tabla: Paso | Tipo.Metodo | Archivo:linea | Que ocurre | Que puede fallar aqui
Al final, una seccion "Codigos de estado posibles y su origen".

# Criterios de aceptacion
1. Cada paso corresponde a codigo que existe.
2. La seccion de codigos de estado distingue los que produce el codigo propio de
   los que produce el framework.
3. Se declara explicitamente si NO hay middleware de manejo de errores registrado.
```

- **Qué verificar en la salida:** el punto 3 es la prueba. En `inicial/` **no** hay middleware de errores registrado (es el TODO-03 de `Program.cs`). Si la respuesta afirma que existe uno, está alucinando.
- **Advertencia:** describir un pipeline de ASP.NET Core "típico" es exactamente lo que un modelo hace bien y lo que aquí sería falso. Este prompt sirve para practicar el contraste entre lo plausible y lo verdadero.

### P-COM-05 — Diferencias entre dos implementaciones

- **Objetivo:** comparar el estado actual con una versión propuesta o con la rama de referencia.
- **Cuándo usarlo:** después de una refactorización, o al comparar la propia solución con la del instructor.
- **Contexto que hay que adjuntar:** `#changes` (namespaced `#search/changes`) para los cambios sin commitear, o los dos archivos.

```text
#changes

# Contexto
Repositorio `copilot-lab-catalogo`. Cambios sin commitear en la rama
<feat/xx-precio-descuento>.

# Tarea
Analiza los cambios y dime si alteran el comportamiento observable de la API,
mas alla de lo que la tarea pedia.

# Restricciones
- No propongas mejoras de estilo en esta respuesta.
- Considera comportamiento observable: codigos de estado, cuerpos de respuesta,
  excepciones lanzadas, contenido de los logs y firmas publicas.

# Formato de salida
Tabla: Cambio | Archivo | Cambia el comportamiento observable (Si/No) | Cual y como
Al final: "Cambios no solicitados detectados" con una lista, o la frase
"No se detectaron cambios fuera del alcance".

# Criterios de aceptacion
1. Toda firma publica modificada aparece listada.
2. Los cambios de formato o de espacios se agrupan aparte y no se mezclan con
   los cambios funcionales.
```

- **Qué verificar en la salida:** contrasta con `git diff --stat` y con `dotnet test`. Un cambio de comportamiento no declarado que las pruebas no detectan es la señal de que falta una prueba.
- **Advertencia:** `#changes` solo ve el diff de trabajo. Si ya hiciste commit, usa `git diff main...HEAD` en la terminal y pega el resultado, o adjunta los archivos.

### P-COM-06 — Interrogatorio de verificación

- **Objetivo:** forzar al modelo a distinguir entre lo que leyó y lo que infirió.
- **Cuándo usarlo:** siempre que una explicación previa vaya a usarse como base de una decisión. Módulo 2, es el prompt central de RA-3.1.
- **Contexto que hay que adjuntar:** el mismo del prompt anterior, en la misma sesión de chat.

```text
# Tarea
Revisa tu respuesta anterior y clasifica cada afirmacion que hiciste.

# Formato de salida
Tabla con estas columnas exactas:
Afirmacion | Evidencia directa en el codigo (archivo:linea) | Inferencia sin evidencia | Nivel de confianza (Alto/Medio/Bajo)

# Restricciones
- Toda afirmacion sin una referencia archivo:linea va obligatoriamente en la
  columna de inferencia.
- No corrijas ni amplies la respuesta anterior: solo clasificala.
- Si al clasificar descubres que una afirmacion era incorrecta, marcala como
  "Incorrecta" y explica en una linea por que.

# Criterios de aceptacion
1. Todas las afirmaciones de la respuesta anterior aparecen en la tabla.
2. Ninguna fila tiene a la vez evidencia directa e inferencia.
```

- **Qué verificar en la salida:** abre las referencias de la columna de evidencia. Las que no se sostengan indican que la explicación original no sirve como base de decisión.
- **Advertencia:** este prompt reduce el riesgo pero **no lo elimina**: el modelo puede clasificar como "evidencia directa" algo que no lo es. La verificación humana de las referencias sigue siendo obligatoria. Es un filtro, no una garantía.

---

## Categoría 2 — Documentación (`P-DOC`)

### P-DOC-01 — Documentación XML de una interfaz pública

- **Objetivo:** cubrir TODO-03 con documentación XML en español que compile sin CS1591.
- **Cuándo usarlo:** Módulo 2, LAB-04. Equivale al prompt file `documentar-xml` del repositorio.
- **Contexto que hay que adjuntar:** la interfaz objetivo y su implementación (para que las excepciones documentadas sean las reales).
- **Agent role:** `Agent`, porque edita archivos.

```text
# Contexto
Repositorio `copilot-lab-catalogo`, .NET 8. Archivo a documentar:
<src/Catalogo.Api/Services/IDescuentoCalculator.cs>. Adjunto tambien su
implementacion <DescuentoCalculator.cs> para que las excepciones documentadas
sean las que realmente se lanzan.

# Tarea
Agrega documentacion XML (`///`) en espanol a todos los miembros publicos del
archivo indicado.

# Restricciones
- No modifiques la firma ni el cuerpo de ningun miembro: solo agregas comentarios.
- Idioma de los textos: espanol. Identificadores: sin acentos ni caracteres especiales.
- `<summary>` en una frase que NO repita el nombre del miembro.
- `<param>` por cada parametro, indicando significado y rango valido.
- `<returns>` explicando que devuelve y, si puede ser null, que significa null.
- `<exception cref="...">` por cada excepcion que el miembro lanza realmente.
- `<remarks>` para las reglas de negocio no evidentes: topes, redondeos, inclusividad
  de los umbrales.
- No documentes miembros privados.

# Formato de salida
El archivo completo, con su ruta, en un unico bloque de codigo C#.

# Criterios de aceptacion
1. Ningun miembro publico queda sin `<summary>`.
2. Cada `<exception cref="...">` corresponde a un `throw` que existe en la implementacion.
3. `dotnet build` no produce advertencias CS1591 tras activar
   `<GenerateDocumentationFile>true</GenerateDocumentationFile>`.
```

- **Qué verificar en la salida:** que los `<exception>` documentados existan en el código (`ArgumentOutOfRangeException` sí; cualquier otro, sospechoso). Que `<remarks>` describa las reglas **correctas**, no las que implementa el código defectuoso. Compila con `GenerateDocumentationFile` activado.
- **Advertencia:** la documentación generada describe con frecuencia el comportamiento **deseado** en lugar del real. Si se documenta `IDescuentoCalculator` antes de corregir BUG-01, la documentación quedará correcta y el código incorrecto: es una divergencia peligrosa que hay que declarar explícitamente.

### P-DOC-02 — Nota de módulo para un tercero

- **Objetivo:** producir la nota de módulo que otra persona pueda leer sin abrir el código.
- **Cuándo usarlo:** Módulo 2, LAB-04; entregable de documentación.
- **Contexto que hay que adjuntar:** el archivo o la carpeta objetivo.

```text
# Contexto
Modulo: <src/Catalogo.Api/Services/>. Repositorio `copilot-lab-catalogo`, .NET 8.
Lector previsto: una persona del equipo que no ha visto este codigo nunca y que
tiene que modificarlo la semana que viene.

# Tarea
Redacta la nota de modulo siguiendo la plantilla del curso.

# Restricciones
- Espanol tecnico, sin adjetivos valorativos ("robusto", "elegante", "potente").
- Cada riesgo indica archivo y linea aproximada.
- Cada mejora propone un cambio concreto y su forma de verificarlo.
- Nada de generalidades: si no se puede afirmar con el codigo a la vista, se omite.

# Formato de salida
## Que hace
## Que NO es responsabilidad de este modulo
## Dependencias (tabla: Dependencia | Entrante/Saliente | Para que)
## Flujo principal (lista numerada)
## Invariantes
## Riesgos (maximo 5, con archivo:linea)
## Oportunidades de mejora (maximo 5, ordenadas por valor/esfuerzo)
## Deuda tecnica conocida

# Criterios de aceptacion
1. La seccion "Que NO es responsabilidad" tiene al menos tres entradas.
2. Los pendientes TODO-01 y TODO-02 aparecen en la deuda tecnica.
```

- **Qué verificar en la salida:** que la sección de invariantes contenga afirmaciones falsables (por ejemplo, "el precio final nunca es mayor que el precio base") y no descripciones.
- **Advertencia:** la sección "Qué NO es responsabilidad de este módulo" es la que más valor aporta y la que el modelo rellena peor, porque exige conocer las fronteras del diseño y no solo el archivo.

### P-DOC-03 — Documentar un endpoint para consumidores externos

- **Objetivo:** producir la nota de endpoint completa, incluidos códigos de estado y casos borde.
- **Cuándo usarlo:** Módulo 3, después de implementar HU-01.

```text
# Contexto
Endpoint: <PATCH /api/productos/{id:int}/precio> del repositorio `copilot-lab-catalogo`.
Adjunto el controller y el servicio que lo implementan.
Reglas de descuento vigentes: volumen (cantidad >= 10 -> 5 %, >= 50 -> 10 %,
>= 100 -> 15 %), cliente preferente +8 %, los descuentos se SUMAN y el total se
TOPA en 20 %, redondeo a 2 decimales con MidpointRounding.AwayFromZero.

# Tarea
Redacta la documentacion del endpoint para un equipo que va a consumirlo y que
no tiene acceso al codigo fuente.

# Restricciones
- Los codigos de estado documentados deben ser los que el codigo produce realmente.
- Los ejemplos numericos deben estar calculados, no inventados.
- Sin credenciales ni datos reales en los ejemplos.

# Formato de salida
1. Verbo, ruta y proposito de negocio
2. Tabla de parametros: Nombre | Ubicacion (ruta/query/cuerpo) | Tipo | Obligatorio | Restricciones | Valor por defecto
3. Cuerpo de la peticion (ejemplo JSON)
4. Tabla de respuestas: Codigo | Cuando se produce | Cuerpo
5. Reglas de negocio aplicadas
6. Ejemplos `curl` de al menos tres escenarios: exito, 400 y 404
7. Casos borde y su resultado
8. Efectos secundarios e idempotencia

# Criterios de aceptacion
1. Los tres ejemplos `curl` son ejecutables tal cual contra http://localhost:5080.
2. Al menos un caso borde corresponde a un umbral exacto (cantidad = 10, 50 o 100).
3. Al menos un caso borde corresponde al tope del 20 %.
```

- **Qué verificar en la salida:** ejecuta los tres `curl` con la API en marcha. Recalcula a mano los ejemplos numéricos: es el punto donde más falla la salida.
- **Advertencia:** PATCH no es idempotente en esta implementación si el cuerpo lleva un `precioBase` fijo y el resultado se persiste; la respuesta debe declararlo. Si afirma que sí lo es sin justificarlo, está copiando una generalidad sobre PATCH.

### P-DOC-04 — Mensaje de commit y descripción a partir del diff

- **Objetivo:** redactar mensajes de commit que expliquen el porqué.
- **Cuándo usarlo:** en cada commit de los laboratorios 5 a 8.

```text
# Contexto
Salida de `git diff --staged` adjunta. Repositorio `copilot-lab-catalogo`.
Historia asociada: <HU-01>.

# Tarea
Redacta el mensaje de commit.

# Restricciones
- Idioma: espanol.
- Primera linea: imperativo, maximo 72 caracteres, sin punto final,
  con el prefijo de la historia.
- Cuerpo: por que se hizo el cambio, no que archivos se tocaron (eso ya esta en el diff).
- Si el diff contiene cambios no relacionados entre si, dilo y propone dividir el commit
  en lugar de redactar uno solo.
- No afirmes que las pruebas pasan si el diff no lo demuestra.

# Formato de salida
Texto plano, sin bloques de codigo, con esta estructura:
- Linea 1: el asunto.
- Linea 2: en blanco.
- Lineas 3 en adelante: el cuerpo, maximo cinco lineas.

# Criterios de aceptacion
1. La linea de asunto no supera 72 caracteres.
2. El cuerpo responde "por que", no "que".
```

- **Qué verificar en la salida:** cuenta los caracteres de la primera línea. Comprueba que ninguna afirmación sobre pruebas sea inventada.
- **Advertencia:** VS Code puede generar mensajes de commit desde la vista Source Control, configurables con `github.copilot.chat.commitMessageGeneration.instructions` `[GA]`. Ese camino es más rápido pero produce mensajes descriptivos ("actualiza ProductoService"), no explicativos. Para los commits que se van a revisar, este prompt da mejor resultado.

### P-DOC-05 — Nota de causa raíz de un defecto

- **Objetivo:** documentar un defecto distinguiendo síntoma de causa raíz.
- **Cuándo usarlo:** Módulo 3, LAB-06, tras corregir BUG-01. También en LAB-08 para BUG-02 y BUG-03.

```text
# Contexto
Defecto: <BUG-01, reglas de descuento incorrectas en DescuentoCalculator.cs>.
Sintoma observado: al quitar el `Skip` de la prueba
`CalcularPrecioFinal_CantidadCienYClientePreferente_TopaElDescuentoEnVeintePorCiento`
el resultado es 82,00 cuando se esperaba 80,00.
Adjunto el archivo antes y despues de la correccion.

# Tarea
Redacta la nota de causa raiz.

# Restricciones
- Distingue explicitamente sintoma de causa raiz. La causa raiz se expresa en una
  sola frase y responde a "por que ocurrio", no a "que se veia".
- Si hay mas de una causa raiz independiente, enumeralas por separado.
- No propongas acciones preventivas genericas ("mejorar la calidad"): cada accion
  debe ser verificable.

# Formato de salida
## Identificador y titulo
## Sintoma observable (con el comando o la prueba que lo reproduce)
## Impacto y alcance
## Investigacion (hipotesis probadas, cual se descarto y por que)
## Causa raiz (una frase por causa)
## Correccion aplicada
## Prueba que impide la regresion
## Por que no se detecto antes
## Acciones preventivas verificables

# Criterios de aceptacion
1. Se identifican las DOS causas raiz independientes de BUG-01.
2. La seccion "Por que no se detecto antes" nombra la ausencia concreta de una
   prueba de umbral.
```

- **Qué verificar en la salida:** BUG-01 tiene **dos** causas raíz independientes: los comparadores `>` en lugar de `>=` en los tres umbrales, y la ausencia del tope del 20 % tras sumar. Una respuesta que solo mencione una está incompleta.
- **Advertencia:** el modelo tiende a fundir ambas causas en una sola frase ("la lógica de descuentos era incorrecta"), que no es una causa raíz sino una reformulación del síntoma.

---

## Categoría 3 — Planificación e implementación (`P-PLA`)

### P-PLA-01 — De historia de usuario a plan técnico

- **Objetivo:** descomponer HU-01 en tareas verificables antes de escribir código.
- **Cuándo usarlo:** Módulo 3, inicio de LAB-05. Es el prompt de RA-3.3.
- **Agent role:** **`Plan`** (o el slash command `/plan`): investiga y produce un plan estructurado **sin modificar código** `[GA]`.
- **Contexto que hay que adjuntar:** `#codebase`, más el controller y el servicio.

```text
/plan

# Contexto
Repositorio `copilot-lab-catalogo`, .NET 8. Historia HU-01:
"Como administrador del catalogo quiero actualizar el precio de un producto
aplicando las reglas de descuento vigentes para mantener los precios alineados
con la politica comercial."
Estado actual: `PATCH /api/productos/{id}/precio` no existe (TODO-01 en
ProductosController.cs linea 90) y `ProductoService.ActualizarPrecioAsync`
(linea 71) lanza NotImplementedException.

# Tarea
Produce un plan tecnico de implementacion. NO escribas ni modifiques codigo.

# Restricciones
- Reutiliza `IDescuentoCalculator`; no reimplementes las reglas de descuento.
- No modifiques pruebas existentes.
- No agregues paquetes NuGet.
- Cada tarea debe poder completarse y verificarse por separado.
- Respeta las convenciones de `.github/instructions/api.instructions.md`
  (controllers solo de transporte, `Task<ActionResult<T>>`, `ProblemDetails`,
  `[ProducesResponseType]`, restricciones de ruta tipadas).

# Formato de salida
Tabla: # | Tarea | Archivo | Que cambia | Como se verifica | Depende de
Despues de la tabla:
- "Decisiones que debe tomar una persona" (lista de ambiguedades que NO vas a resolver tu)
- "Riesgos de esta implementacion"

# Criterios de aceptacion
1. Cada tarea tiene un metodo de verificacion concreto (comando o prueba).
2. La lista de decisiones humanas incluye al menos donde se valida la entrada.
3. El plan cubre los ocho criterios de aceptacion CA-1 a CA-8 de HU-01.
```

- **Qué verificar en la salida:** que el plan cubra los ocho criterios de aceptación; que la sección de decisiones humanas no esté vacía; que no proponga reimplementar el cálculo dentro del servicio.
- **Advertencia:** el rol `Plan` no cambia código, pero **sí puede afirmar cosas falsas sobre el código existente**. El plan se revisa antes de ejecutarlo, no después.

### P-PLA-02 — Implementar el método del servicio

- **Objetivo:** implementar `ActualizarPrecioAsync` respetando las capas.
- **Cuándo usarlo:** Módulo 3, LAB-05, después de aprobar el plan.
- **Agent role:** `Agent`.

```text
# Contexto
Repositorio `copilot-lab-catalogo`, .NET 8.
Archivo: `src/Catalogo.Api/Services/ProductoService.cs`, metodo
`ActualizarPrecioAsync` (linea 71), que hoy lanza NotImplementedException.
Dependencias ya inyectadas en el constructor: `IProductoRepository _repositorio`,
`IDescuentoCalculator _calculadora`, `ILogger<ProductoService> _logger`.
Firma exacta que debe respetarse:
`Task<ProductoResponse?> ActualizarPrecioAsync(int id, ActualizarPrecioRequest solicitud, CancellationToken cancellationToken = default)`

# Tarea
Implementa el metodo.

# Restricciones
- No cambies la firma ni la interfaz `IProductoService`.
- El calculo del precio se delega en `_calculadora.CalcularPrecioFinal(...)`.
  No reimplementes ninguna regla de descuento aqui.
- Devuelve `null` cuando el producto no exista; el controller lo traducira a 404.
- Propaga `cancellationToken` a todas las llamadas asincronicas.
- Usa `ConfigureAwait(false)`, como el resto del archivo.
- Registra con `_logger` el cambio de precio, sin incluir datos sensibles.
- No agregues paquetes NuGet ni modifiques otros archivos.

# Formato de salida
Solo el metodo completo, en un bloque de codigo C#, listo para sustituir el actual.
Debajo, en tres lineas como maximo, que decisiones tomaste que el prompt no fijaba.

# Criterios de aceptacion
1. `dotnet build` sin errores ni advertencias.
2. La prueba `ActualizarPrecioAsync_ProductoExistente_DevuelveElPrecioConDescuentoAplicado`
   pasa al quitarle el `Skip` (espera 95,00 con precioBase 100, cantidad 11).
3. El metodo no contiene ninguna constante numerica de descuento.
```

- **Qué verificar en la salida:** que no aparezca ningún `0.05m`, `0.10m`, `0.15m`, `0.08m` ni `0.20m` en el método; que se llame a `ActualizarAsync` del repositorio (si no, el precio no se persiste y CA-6 falla); que `cancellationToken` se propague en las tres llamadas.
- **Advertencia:** el error más frecuente de la salida es **calcular sin persistir**: devuelve el `ProductoResponse` con el precio nuevo pero no guarda el `Producto`. La prueba que solo mira el valor devuelto no lo detecta; hace falta la prueba de CA-6.

### P-PLA-03 — Implementar la acción del controller

- **Objetivo:** exponer el endpoint respetando las convenciones de API del repositorio.
- **Cuándo usarlo:** Módulo 3, LAB-05, inmediatamente después de P-PLA-02.

```text
# Contexto
Archivo: `src/Catalogo.Api/Controllers/ProductosController.cs`. En la linea 90 hay
un comentario TODO-01 que marca donde falta la accion.
Convenciones obligatorias: `.github/instructions/api.instructions.md`.
El servicio `IProductoService.ActualizarPrecioAsync` ya esta implementado y
devuelve `null` si el producto no existe.

# Tarea
Agrega la accion `PATCH api/productos/{id:int}/precio`.

# Restricciones
- El controller es solo transporte: sin reglas de negocio, sin LINQ de dominio,
  sin acceso al repositorio.
- Firma: `Task<ActionResult<ProductoResponse>>`.
- Declara `[ProducesResponseType]` para 200 (ProductoResponse), 400 (ProblemDetails)
  y 404 (ProblemDetails).
- Restriccion de ruta tipada `{id:int}`.
- Recibe `CancellationToken` como parametro y lo propaga.
- Documentacion XML en espanol en la accion.
- Errores en formato `ProblemDetails`, con el mismo estilo que las acciones existentes
  del archivo.
- No toques ninguna otra accion del controller.

# Formato de salida
Solo la accion nueva, en un bloque de codigo C#, indicando entre que dos miembros
existentes debe insertarse.

# Criterios de aceptacion
1. `dotnet build` sin advertencias.
2. La accion aparece en Swagger con los tres codigos de respuesta declarados.
3. `curl -X PATCH http://localhost:5080/api/productos/9999/precio` devuelve 404 con
   un cuerpo `ProblemDetails`.
```

- **Qué verificar en la salida:** que use `[HttpPatch]` y no `[HttpPut]`; que la ruta sea exactamente `"{id:int}/precio"`; que los tres `[ProducesResponseType]` estén; que el `ProblemDetails` de 404 siga el estilo de las acciones existentes.
- **Advertencia:** con `[ApiController]`, la validación automática del modelo produce un 400 con `ValidationProblemDetails` **antes** de entrar en la acción. Si el DTO lleva DataAnnotations, parte del comportamiento de 400 no está en el código que se escribió: hay que probarlo, no deducirlo.

### P-PLA-04 — Implementar el filtrado y la paginación

- **Objetivo:** cubrir TODO-02 (HU-02), la variación opcional avanzada.
- **Cuándo usarlo:** Módulo 3, para quien termine LAB-05 antes de tiempo.

```text
# Contexto
Archivo: `src/Catalogo.Api/Services/ProductoService.cs`, metodo `BuscarAsync`
(linea 39), que hoy lanza NotImplementedException.
DTO de entrada: `BusquedaProductosQuery` con `Categoria?`, `PrecioMinimo`,
`PrecioMaximo`, `Texto`, `Pagina` (default 1) y `TamanoPagina` (default 20),
mas las constantes `TamanoPaginaPredeterminado = 20` y `TamanoPaginaMaximo = 100`.
El repositorio expone `ObtenerTodosAsync(CancellationToken)`.

# Tarea
Implementa `BuscarAsync` con filtros combinables y paginacion.

# Restricciones
- Todos los filtros son opcionales y se combinan con AND.
- Los rangos de precio son inclusivos en ambos extremos.
- El filtro de texto es una coincidencia parcial sobre `Nombre` que NO distingue
  mayusculas; usa `StringComparison.OrdinalIgnoreCase`, no `ToLower()`.
- Usa las constantes ya declaradas en el DTO; no repitas los numeros 20 ni 100.
- Ordena de forma determinista antes de paginar.
- Propaga `cancellationToken`.
- No agregues paquetes NuGet.

# Formato de salida
El metodo completo en un bloque de codigo C#. Debajo, una tabla:
Ambiguedad detectada | Como la resolviste | Por que

# Criterios de aceptacion
1. `GET /api/productos` sin parametros devuelve los 12 productos de la semilla.
2. `?tamanoPagina=500` no devuelve mas de 100 elementos.
3. `?pagina=99` devuelve una lista vacia y codigo 200, no un error.
4. La tabla de ambiguedades menciona explicitamente el tratamiento de los productos
   con `Activo = false`.
```

- **Qué verificar en la salida:** el criterio 4 es la prueba de fondo. El producto 10 de la semilla tiene `Activo = false` y ni el contrato ni el DTO dicen qué hacer con él. Si la tabla de ambigüedades no lo menciona, el modelo decidió en silencio.
- **Advertencia:** una implementación con `ToLower()` sobre cada nombre funciona y pasa las pruebas, pero asigna memoria por elemento y depende de la cultura del hilo. Es un buen caso para practicar la diferencia entre "pasa las pruebas" y "es correcto".

### P-PLA-05 — Añadir validación a un DTO y a su servicio

- **Objetivo:** corregir BUG-02 cumpliendo la regla 6 de las instrucciones del repositorio.
- **Cuándo usarlo:** Módulo 4, LAB-08.

```text
# Contexto
Repositorio `copilot-lab-catalogo`, .NET 8.
Archivos: `src/Catalogo.Api/Dtos/CrearProductoRequest.cs` (sin ninguna
DataAnnotation) y `src/Catalogo.Api/Services/ProductoService.cs`, metodo
`CrearAsync` (lineas 52 a 68, sin validacion).
Hoy `POST /api/productos` con {"nombre":"","categoria":1,"precioBase":-99,
"existencias":-5} responde 201 Created y persiste el producto.
Regla 6 de `.github/copilot-instructions.md`: toda entrada publica se valida con
DataAnnotations en el DTO y validacion explicita en el servicio; los datos
invalidos producen 400 con ProblemDetails.

# Tarea
Anade la validacion al DTO y al servicio.

# Restricciones
- `Nombre`: obligatorio, entre 3 y 100 caracteres, y NO puede estar formado solo
  por espacios en blanco.
- `Categoria`: debe ser un valor definido del enum `Categoria`.
- `PrecioBase`: mayor que cero.
- `Existencias`: mayor o igual que cero.
- La validacion del servicio debe ejecutarse aunque se invoque `CrearAsync`
  directamente, sin pasar por el controller.
- Usa `Validator.TryValidateObject` con `validateAllProperties: true`.
- Lanza `ValidationException` con un mensaje que nombre el campo invalido y que
  NO exponga detalles internos.
- El DTO sigue siendo un `record` inmutable.
- No modifiques pruebas existentes.

# Formato de salida
Los dos archivos afectados, completos, cada uno en su bloque de codigo con su ruta.
Debajo, una tabla: Regla | Donde se aplica (DTO / servicio / ambos) | Que ocurre si falla

# Criterios de aceptacion
1. `Nombre = "   "` (tres espacios) produce un error de validacion.
2. `Existencias = 0` es valido; `Existencias = -1` no lo es.
3. `PrecioBase = 0.01` es valido; `PrecioBase = 0` no lo es.
4. La prueba existente `CrearAsync_SolicitudValida_AsignaElSiguienteIdentificador`
   sigue pasando.
```

- **Qué verificar en la salida:** que `TryValidateObject` lleve el cuarto argumento `validateAllProperties: true` (sin él solo se evalúa `[Required]`); que exista una comprobación explícita de espacios en blanco además de `[StringLength]`; que el mensaje de error no incluya nombres de tipos internos.
- **Advertencia:** son dos omisiones que Copilot reproduce con mucha frecuencia porque abundan en el código público. La prueba con `Nombre = "   "` y la prueba con `PrecioBase = -99` son las que las detectan; escribe las pruebas antes de aceptar la implementación.

### P-PLA-06 — Middleware de traducción de excepciones

- **Objetivo:** cubrir la parte de TODO-03 relativa a `ProblemDetails`.
- **Cuándo usarlo:** Módulo 4, LAB-08.

```text
# Contexto
Repositorio `copilot-lab-catalogo`, .NET 8. `Program.cs` linea 22 marca el TODO-03:
no hay middleware de manejo centralizado de errores registrado, por lo que las
excepciones de dominio salen como 500 sin cuerpo util.
Excepciones que el codigo lanza hoy: `ArgumentOutOfRangeException` desde
`DescuentoCalculator`, `NotImplementedException` desde `BuscarAsync` y
`ActualizarPrecioAsync`, y `ValidationException` una vez implementada la
validacion de `CrearAsync`.

# Tarea
Crea un middleware `Infrastructure/ManejoErroresMiddleware.cs` que traduzca las
excepciones a respuestas `ProblemDetails`, y registralo en `Program.cs`.

# Restricciones
- Mapeo: `ValidationException` y `ArgumentOutOfRangeException` -> 400;
  `NotImplementedException` -> 501; cualquier otra -> 500.
- La respuesta NUNCA incluye `StackTrace`, nombres de tipos internos ni rutas
  de archivo.
- Registra la excepcion completa con `ILogger` (ahi si va el detalle) y devuelve
  al cliente solo el mensaje apto para consumo externo.
- Prohibido `catch { }` vacio.
- El middleware se registra antes de `app.MapControllers()`.
- No agregues paquetes NuGet.

# Formato de salida
Archivo completo del middleware con su ruta, mas el fragmento exacto que hay que
insertar en `Program.cs` indicando entre que lineas va.

# Criterios de aceptacion
1. `GET /api/productos` (con TODO-02 sin implementar) devuelve 501 con
   `ProblemDetails` en lugar de 500 sin cuerpo.
2. Ningun cuerpo de respuesta contiene la palabra "Catalogo.Api" ni "at ".
3. El log de servidor sigue conteniendo la excepcion completa.
```

- **Qué verificar en la salida:** provoca una excepción y mira el cuerpo con `curl -i`. El criterio 2 es el control de filtración de información. Comprueba también que el log del servidor **sí** conserva el detalle: un middleware que oculta el error al cliente y también al operador no sirve.
- **Advertencia:** el orden de registro importa. Si el middleware se registra después de `MapControllers`, no captura nada, y la única forma de detectarlo es probándolo.

### P-PLA-07 — Comparar dos alternativas de implementación

- **Objetivo:** obtener alternativas con criterios, no una única propuesta.
- **Cuándo usarlo:** siempre que una decisión tenga consecuencias de diseño. Módulo 3 y 5.

```text
# Contexto
Decision pendiente en el repositorio `copilot-lab-catalogo`:
<donde se valida `precioBase` y `cantidad` de `ActualizarPrecioRequest`>.
Restricciones del proyecto: capas Controllers -> Services -> Repositories;
regla 6 de `.github/copilot-instructions.md`; los errores se devuelven como
`ProblemDetails`; el calculo ya lanza `ArgumentOutOfRangeException`.

# Tarea
Propon exactamente tres alternativas y comparalas. NO implementes ninguna.

# Restricciones
- Las tres alternativas deben ser viables con .NET 8 y con la arquitectura actual.
- Nada de alternativas de paja: si una es claramente peor, no la propongas.
- No recomiendes una sin explicitar el criterio con el que la elegiste.

# Formato de salida
Tabla: Alternativa | Como funciona | Ventajas | Inconvenientes | Que se rompe si
cambia el requisito | Esfuerzo (Bajo/Medio/Alto)
Despues:
- "Recomendacion y criterio" (una alternativa, un criterio explicito)
- "Que decidiria distinto si el requisito fuera <X>"

# Criterios de aceptacion
1. Las tres alternativas son distintas en sustancia, no en estilo.
2. La recomendacion nombra el criterio (mantenibilidad, cobertura de pruebas,
   consistencia con el resto del codigo, coste de cambio).
3. Hay al menos un inconveniente real por alternativa.
```

- **Qué verificar en la salida:** que ninguna alternativa sea un hombre de paja; que la recomendación no sea siempre "la más moderna". Contrasta la recomendación con lo que ya hace el resto del repositorio: la consistencia interna suele pesar más que la elegancia local.
- **Advertencia:** la decisión de arquitectura **no se delega**. Este prompt produce insumos para decidir; quien decide y firma es la persona, y la decisión se registra en un ADR.

---

## Categoría 4 — Refactorización (`P-REF`)

### P-REF-01 — Refactorización con red de seguridad

- **Objetivo:** mejorar la estructura sin cambiar el comportamiento observable.
- **Cuándo usarlo:** Módulo 3, LAB-06. Es el prompt de RA-3.5.
- **Requisito previo innegociable:** `dotnet test` en verde **antes** de empezar. Sin pruebas que pasen no hay refactorización; hay reescritura a ciegas.

```text
# Contexto
Archivo: <src/Catalogo.Api/Services/ProductoService.cs>, metodo <BuscarAsync>.
Repositorio `copilot-lab-catalogo`, .NET 8. `dotnet test` esta en verde antes de
este cambio: 15 pruebas superadas y 2 omitidas.

# Tarea
Refactoriza el metodo indicado SIN cambiar su comportamiento observable.

# Restricciones
- Comportamiento observable = valores devueltos, excepciones lanzadas, codigos de
  estado, contenido de los logs y firmas publicas. Nada de eso puede cambiar.
- No cambies la firma ni la interfaz.
- No modifiques pruebas.
- No agregues paquetes NuGet.
- Cada cambio debe tener una justificacion concreta; si un cambio es solo cuestion
  de gusto, no lo hagas.
- Maximo tres cambios estructurales. Si crees que hacen falta mas, dilo y detente.

# Formato de salida
1. Tabla: Cambio | Motivo | Riesgo de alterar el comportamiento (Ninguno/Bajo/Medio/Alto)
2. El metodo refactorizado completo en un bloque de codigo C#
3. Seccion "Que NO refactorice y por que"

# Criterios de aceptacion
1. `dotnet test` sigue dando 15 superadas y 2 omitidas, sin tocar ninguna prueba.
2. Ningun cambio esta clasificado con riesgo Alto sin una prueba que lo cubra.
3. La seccion "Que NO refactorice" no esta vacia.
```

- **Qué verificar en la salida:** ejecuta `dotnet test` inmediatamente. Después lee la tabla: cualquier cambio marcado con riesgo Medio o Alto que no tenga una prueba que lo cubra es un cambio que hay que revertir o cubrir antes de continuar.
- **Advertencia:** el límite de la refactorización asistida es que el modelo **no sabe qué es comportamiento observable en tu sistema**. Un cambio en el texto de un log puede romper una alerta de producción, y ninguna prueba unitaria lo detecta. Por eso el prompt define explícitamente qué cuenta como observable.

### P-REF-02 — Extraer un método sin cambiar semántica

- **Objetivo:** reducir la complejidad de un método largo con una transformación acotada.
- **Cuándo usarlo:** Módulo 3, cuando un método supera lo que se lee de una vez.

```text
# Contexto
Archivo: <ruta>. Metodo: <nombre>. Repositorio `copilot-lab-catalogo`, .NET 8.

# Tarea
Extrae los bloques cohesivos de este metodo a metodos privados con nombres que
describan la INTENCION, no la implementacion.

# Restricciones
- Transformacion puramente mecanica: mismo orden de operaciones, mismas
  condiciones, mismos valores devueltos, mismas excepciones.
- Los metodos extraidos son `private static` si no usan estado de instancia.
- Nombres en ingles tecnico o en espanol sin acentos, nunca mezclando ambos.
- No cambies visibilidad, ni tipos de retorno, ni el orden de los parametros.
- No introduzcas variables locales nuevas salvo las estrictamente necesarias
  para la extraccion.

# Formato de salida
El archivo completo con su ruta, en un bloque de codigo C#.
Debajo, tabla: Metodo extraido | Lineas originales | Que representa

# Criterios de aceptacion
1. `dotnet test` sigue en verde sin tocar pruebas.
2. Ningun nombre de metodo extraido describe el "como" (`CalcularConIf`,
   `ProcesarBucle`): todos describen el "que".
3. El metodo original queda mas corto y se lee de arriba abajo.
```

- **Qué verificar en la salida:** compara con `git diff` que solo se han movido bloques. Comprueba especialmente los `return` tempranos y los `throw`: la extracción los cambia de sitio con facilidad.
- **Advertencia:** extraer un bloque que contiene un `return` del método padre exige transformar la lógica, no moverla. Si la salida hace eso sin advertirlo, ya no es una transformación mecánica y hay que revisarla línea a línea.

### P-REF-03 — Eliminar duplicación entre dos archivos

- **Objetivo:** unificar lógica repetida sin crear una abstracción prematura.
- **Cuándo usarlo:** Módulo 3, variación avanzada.

```text
# Contexto
Archivos: <archivo A> y <archivo B> del repositorio `copilot-lab-catalogo`.

# Tarea
Identifica la duplicacion real entre ambos y propon como eliminarla.

# Restricciones
- Distingue duplicacion ACCIDENTAL (dos cosas que hoy se parecen y evolucionaran
  por separado) de duplicacion ESENCIAL (una sola regla escrita dos veces).
  Solo se unifica la esencial.
- Si la unificacion exige un parametro booleano de control de flujo, no la
  propongas: es senal de que las dos cosas no son la misma.
- No crees interfaces ni clases base nuevas sin justificarlo con dos usos reales.
- No agregues paquetes NuGet.

# Formato de salida
1. Tabla: Fragmento duplicado | Archivo A:linea | Archivo B:linea | Accidental o esencial | Justificacion
2. Propuesta de unificacion solo para lo esencial, con el codigo resultante
3. Seccion "Duplicacion que conviene mantener y por que"

# Criterios de aceptacion
1. Cada fragmento esta clasificado con justificacion, no por parecido textual.
2. La seccion final no esta vacia.
```

- **Qué verificar en la salida:** desconfía de toda unificación que introduzca un `bool` como parámetro de control. Comprueba que la abstracción propuesta tenga al menos dos usos reales hoy, no usos hipotéticos.
- **Advertencia:** este es el prompt donde el modelo produce más abstracciones prematuras, porque el parecido textual es exactamente lo que sabe detectar. La distinción accidental/esencial requiere conocimiento del dominio que no está en el código.

### P-REF-04 — Modernizar a características de .NET 8

- **Objetivo:** aprovechar el lenguaje sin salirse del framework de destino.
- **Cuándo usarlo:** Módulo 3, variación avanzada.

```text
# Contexto
Archivo: <ruta>. Framework de destino: net8.0 con `Nullable` e `ImplicitUsings`
habilitados. Convenciones: `.github/instructions/csharp.instructions.md`.

# Tarea
Propon modernizaciones del codigo a caracteristicas disponibles en C# 12 / .NET 8.

# Restricciones
- Framework de destino net8.0. NO propongas nada introducido en .NET 9, .NET 10
  ni versiones posteriores. Si dudas de la version de una caracteristica, decl√°ralo
  y no la propongas.
- Cada propuesta debe mejorar la legibilidad o la seguridad, no solo la brevedad.
- No cambies el comportamiento observable.
- No conviertas clases en `record` si son entidades mutables de dominio.

# Formato de salida
Tabla: Codigo actual | Propuesta | Version del lenguaje que la introdujo | Beneficio
concreto | Riesgo
Despues, un diff unificado solo con los cambios aceptables.

# Criterios de aceptacion
1. Toda caracteristica propuesta existe en C# 12 / .NET 8.
2. `dotnet build` sin advertencias nuevas.
3. `dotnet test` en verde.
```

- **Qué verificar en la salida:** comprueba la columna de versión contra la documentación del lenguaje. Este es el prompt donde el modelo introduce con más facilidad APIs de versiones posteriores, porque el corpus público las contiene y no distingue el `TargetFramework` del proyecto.
- **Advertencia:** que algo compile en tu equipo no significa que esté disponible en el runtime de destino. Comprueba siempre `TargetFramework` en el `.csproj`, que aquí es `net8.0`.

### P-REF-05 — Revisión previa a la refactorización

- **Objetivo:** decidir si merece la pena refactorizar antes de hacerlo.
- **Cuándo usarlo:** Módulo 3, antes de P-REF-01, cuando la mejora no es evidente.

```text
# Contexto
Archivo: <ruta>. Repositorio `copilot-lab-catalogo`.
Cobertura de pruebas de este archivo: <describe que esta cubierto y que no>.

# Tarea
Evalua si este codigo debe refactorizarse ahora, mas adelante o nunca.

# Restricciones
- No refactorices nada en esta respuesta.
- Considera: frecuencia de cambio del archivo, cobertura de pruebas existente,
  riesgo de regresion, coste de la refactorizacion y coste de no hacerla.
- Si la cobertura de pruebas es insuficiente para refactorizar con seguridad,
  dilo antes que cualquier otra cosa.

# Formato de salida
1. Veredicto en una linea: Refactorizar ahora / Refactorizar despues de <X> / No refactorizar
2. Tabla: Criterio | Evaluacion | Peso en el veredicto
3. Si el veredicto es "despues": que hay que hacer primero
4. Si el veredicto es "no": que problema concreto hay que aceptar y documentar

# Criterios de aceptacion
1. El veredicto es uno de los tres, sin ambiguedad.
2. La cobertura de pruebas aparece evaluada explicitamente.
```

- **Qué verificar en la salida:** que el veredicto sea coherente con la tabla. Si la tabla dice que la cobertura es insuficiente y el veredicto es "refactorizar ahora", hay una contradicción y manda la tabla.
- **Advertencia:** el modelo tiende a recomendar refactorizar siempre, porque el corpus de código público premia la mejora continua. "No refactorizar" es una respuesta válida y con frecuencia la correcta.

---

## Categoría 5 — Depuración (`P-DEP`)

### P-DEP-01 — Método de depuración en cinco pasos

- **Objetivo:** convertir un fallo en una hipótesis comprobable, no en una lista de conjeturas.
- **Cuándo usarlo:** Módulo 3, LAB-06, ante cualquier prueba en rojo.
- **Contexto que hay que adjuntar:** la salida literal de `dotnet test` (o `#problems` / `#read/problems`), el archivo de la prueba y el archivo de producción implicado.

```text
# Contexto
Repositorio `copilot-lab-catalogo`, .NET 8.
Prueba que falla: `CalcularPrecioFinal_CantidadCienYClientePreferente_TopaElDescuentoEnVeintePorCiento`
Salida literal de `dotnet test`:
<pega aqui el mensaje completo, incluida la linea de esperado/obtenido>
Adjunto: DescuentoCalculatorTests.cs y DescuentoCalculator.cs.

# Tarea
Aplica el metodo de depuracion en cinco pasos. NO modifiques codigo todavia.

# Restricciones
- Paso 1: reformula el sintoma en una frase, con los numeros exactos.
- Paso 2: enumera de dos a cuatro hipotesis, ordenadas por probabilidad.
- Paso 3: por cada hipotesis, indica el experimento minimo que la confirma o
  la descarta (una linea de codigo, una prueba, un valor a inspeccionar).
- Paso 4: indica cual crees que es la causa raiz y con que evidencia del codigo
  adjunto la sostienes.
- Paso 5: propon la correccion minima, sin aplicarla.
- No propongas cambiar la prueba. Si una prueba falla, el defecto esta en el
  codigo de produccion.
- No propongas "revisar toda la logica": cada hipotesis debe apuntar a lineas
  concretas.

# Formato de salida
Los cinco pasos, con esos titulos, en ese orden.
El paso 3 en tabla: Hipotesis | Experimento | Que resultado la confirma | Que resultado la descarta

# Criterios de aceptacion
1. Cada hipotesis cita lineas concretas del codigo adjunto.
2. La correccion propuesta es minima: no toca nada ajeno a la causa raiz.
3. Si hay mas de una causa raiz independiente, se declaran por separado.
```

- **Qué verificar en la salida:** ejecuta el experimento del paso 3 antes de aceptar el paso 4. En BUG-01, la respuesta correcta identifica **dos** causas independientes; una respuesta que solo mencione una está incompleta aunque su corrección haga pasar esa prueba concreta.
- **Advertencia:** la tentación es saltar directo al paso 5. El valor pedagógico está en el paso 3: es el único que produce evidencia en lugar de opinión.

### P-DEP-02 — Interpretar un fallo de prueba

- **Objetivo:** leer correctamente el mensaje de una aserción de FluentAssertions.
- **Cuándo usarlo:** Módulo 3 y 4, en cuanto aparece el primer rojo.

```text
# Contexto
Salida literal de `dotnet test`:
<pega la salida completa, incluidas las lineas de "Expected" y "but found">
Framework de pruebas: xUnit con FluentAssertions.

# Tarea
Explicame que dice exactamente este fallo.

# Restricciones
- Distingue con claridad: valor esperado, valor obtenido, prueba que falla,
  linea de la asercion y linea del codigo de produccion implicada.
- No propongas correcciones en esta respuesta.
- Si el mensaje no permite determinar el origen, di que informacion adicional
  hace falta y como obtenerla.

# Formato de salida
Tabla: Elemento | Valor
Filas obligatorias: Prueba | Asercion que fallo | Valor esperado | Valor obtenido |
Diferencia | Archivo:linea de la asercion | Que se estaba ejercitando
Debajo: "Que NO se puede deducir de este mensaje".

# Criterios de aceptacion
1. Los valores esperado y obtenido se transcriben literalmente del mensaje.
2. La seccion "Que NO se puede deducir" no esta vacia.
```

- **Qué verificar en la salida:** compara los valores transcritos con la salida real, carácter a carácter. Un `82.00` transcrito como `82.0` cambia el análisis de redondeo.
- **Advertencia:** la mayoría de los fallos de este repositorio se leen en dos líneas del mensaje. Pedirle al modelo que corrija antes de haber leído el mensaje completo es la causa más común de correcciones que rompen otra cosa.

### P-DEP-03 — Análisis del último comando de terminal

- **Objetivo:** interpretar un error de compilación o de ejecución sin copiar y pegar a mano.
- **Cuándo usarlo:** Módulo 1 y 3, ante errores de `dotnet`.
- **Contexto que hay que adjuntar:** `#terminalLastCommand` (namespaced `#read/terminalLastCommand`).

```text
#terminalLastCommand

# Contexto
Repositorio `copilot-lab-catalogo`, .NET 8, solucion `CopilotLabCatalogo.sln`.

# Tarea
Explica el error del ultimo comando y dime el siguiente paso.

# Restricciones
- Identifica el codigo de error de C# (CS####) o el codigo de dotnet cuando exista.
- Distingue error de compilacion, de restauracion de paquetes, de configuracion
  y de ejecucion.
- Propon UN solo siguiente paso, el mas barato de comprobar.
- Si el error puede tener varias causas, di cual descartarias primero y como.

# Formato de salida
1. Diagnostico en una frase
2. Codigo de error y su significado
3. Causa mas probable, con la evidencia del mensaje que la sostiene
4. Siguiente paso (un solo comando o una sola edicion)
5. Como sabras si funciono

# Criterios de aceptacion
1. El paso 4 es un comando concreto, no una recomendacion general.
2. El paso 5 describe una salida observable.
```

- **Qué verificar en la salida:** que el código de error citado aparezca literalmente en la terminal. Ejecuta el paso 4 y compáralo con el paso 5.
- **Advertencia:** `#terminalLastCommand` envía al modelo la salida del último comando, que puede contener rutas locales, nombres de usuario o cadenas de conexión que aparecieran en un log. Mira lo que envías antes de enviarlo.

### P-DEP-04 — Análisis de un log de aplicación

- **Objetivo:** encontrar la señal en una traza larga.
- **Cuándo usarlo:** Módulo 3, DEMO-06 y LAB-06.

```text
# Contexto
Aplicacion: `Catalogo.Api` en .NET 8, ejecutandose en http://localhost:5080.
Fragmento de log adjunto, correspondiente a la peticion <descripcion>.

# Tarea
Analiza el log e identifica que ocurrio.

# Restricciones
- Ordena los eventos por tiempo y senala el primero anomalo, no solo el ultimo.
- Distingue causa de consecuencia: una excepcion posterior puede ser un efecto
  de un error anterior.
- Si el log contiene datos que parecen sensibles (claves, tokens, cabeceras de
  autorizacion, datos personales), senalalo como HALLAZGO DE SEGURIDAD en primer
  lugar y NO reproduzcas el valor completo.
- No propongas correcciones en esta respuesta.

# Formato de salida
1. "Hallazgos de seguridad en el propio log" (o "Ninguno")
2. Tabla cronologica: Marca de tiempo | Nivel | Componente | Que ocurrio | Normal o anomalo
3. Primer evento anomalo y por que lo es
4. Que informacion falta en el log para cerrar el diagnostico

# Criterios de aceptacion
1. El apartado 1 aparece siempre, aunque sea vacio.
2. El apartado 4 propone una mejora concreta de instrumentacion.
```

- **Qué verificar en la salida:** si el log procede de `LegacyPricingClient` con nivel Debug, el apartado 1 **debe** señalar la cabecera `Authorization: Bearer ...` (BUG-03, hallazgo H-3). Si no lo hace, la revisión de seguridad del log queda pendiente.
- **Advertencia:** nunca pegues logs de un entorno real en el chat. Para este ejercicio el log procede de la aplicación de práctica, cuyo único secreto es simulado.

### P-DEP-05 — Reproducir un defecto con una prueba

- **Objetivo:** convertir un informe de defecto en una prueba que falla.
- **Cuándo usarlo:** Módulo 3 y 4, antes de corregir cualquier defecto.

```text
# Contexto
Repositorio `copilot-lab-catalogo`, pruebas en `tests/Catalogo.Api.Tests` con
xUnit, FluentAssertions y Moq.
Defecto reportado: <descripcion en lenguaje natural, con los valores exactos>.
Ejemplo: "POST /api/productos con nombre vacio y precioBase -99 devuelve 201
Created y persiste el producto".

# Tarea
Escribe la prueba MINIMA que reproduce este defecto y que hoy falla.

# Restricciones
- La prueba debe fallar con el codigo actual. Si crees que pasaria, dilo y detente:
  significa que el defecto no esta donde se dice.
- No modifiques codigo de produccion.
- No modifiques pruebas existentes; anade una nueva.
- Nombre con el patron `Metodo_Escenario_ResultadoEsperado`.
- Estructura Arrange / Act / Assert con los tres comentarios visibles.
- Un assert logico. Sin logica condicional dentro de la prueba.
- Excepciones con `Should().ThrowExactly<T>()` o `Should().ThrowAsync<T>()`.

# Formato de salida
La prueba completa en un bloque de codigo C#, indicando el archivo donde debe
insertarse. Debajo, en una linea: que mensaje de fallo esperas ver al ejecutarla.

# Criterios de aceptacion
1. `dotnet test` muestra exactamente una prueba nueva en rojo.
2. El mensaje de fallo coincide con el anunciado.
3. Ninguna otra prueba cambia de estado.
```

- **Qué verificar en la salida:** ejecútala y confirma que **falla**. Una prueba de reproducción que pasa a la primera es una prueba que no reproduce nada, y es un error muy fácil de no notar.
- **Advertencia:** esta prueba es la que después se convierte en la barrera de regresión. Si se escribe demasiado específica (con los valores exactos del informe y nada más), protege poco; si se escribe demasiado general, deja de fallar por el motivo correcto.

---

## Categoría 6 — Pruebas (`P-TST`)

### P-TST-01 — Generar la batería de pruebas de un tipo

- **Objetivo:** producir la cobertura inicial de un tipo, con casos borde incluidos.
- **Cuándo usarlo:** Módulo 4, LAB-07. Equivale al prompt file `generar-pruebas-xunit` del repositorio.
- **Agent role:** `Agent`.

```text
# Contexto
Repositorio `copilot-lab-catalogo`, .NET 8. Tipo a probar:
<src/Catalogo.Api/Services/DescuentoCalculator.cs>.
Convenciones obligatorias: `.github/instructions/tests.instructions.md`.
Reglas de negocio que debe cumplir el tipo: volumen (cantidad >= 10 -> 5 %,
>= 50 -> 10 %, >= 100 -> 15 %), cliente preferente +8 %, los descuentos se SUMAN,
el total se TOPA en 20 %, precioBase > 0 y cantidad >= 1 o
ArgumentOutOfRangeException, redondeo a 2 decimales con MidpointRounding.AwayFromZero.

# Tarea
Genera las pruebas unitarias en `tests/Catalogo.Api.Tests/DescuentoCalculatorTests.cs`.

# Restricciones
- xUnit + FluentAssertions. Dobles con Moq o clases falsas escritas a mano.
- Nombres `Metodo_Escenario_ResultadoEsperado`.
- Arrange / Act / Assert con los tres comentarios visibles.
- Un assert logico por prueba. Sin logica condicional dentro de las pruebas.
- `[Theory]` + `[InlineData]` para los casos repetitivos.
- Las pruebas se escriben contra las REGLAS DE NEGOCIO indicadas arriba, no contra
  el comportamiento actual del codigo. Si el codigo no las cumple, la prueba debe
  fallar y eso es correcto.
- No modifiques ni elimines las pruebas existentes del archivo.
- No modifiques codigo de produccion.

# Cobertura minima obligatoria
- Umbrales EXACTOS por pares: 9 y 10, 49 y 50, 99 y 100.
- Tope del 20 % (cantidad 100 con cliente preferente -> 80,00 sobre 100,00).
- Suma frente a composicion (cantidad 50 con preferente -> 82,00, no 82,80).
- Redondeo AwayFromZero con al menos dos casos que lo distingan de ToEven:
  (249,50, 49, false) -> 237,03 y (129,90, 10, false) -> 123,41.
- Entradas invalidas: precioBase 0 y negativo, cantidad 0 y negativa.

# Formato de salida
Las pruebas nuevas en un bloque de codigo C#, indicando donde insertarlas.
Debajo: tabla de casos cubiertos y lista de casos que decidiste NO cubrir, con el motivo.

# Criterios de aceptacion
1. Existe una prueba por cada valor de la cobertura minima.
2. Al ejecutar `dotnet test`, las pruebas de umbral y de tope FALLAN con el
   codigo actual (BUG-01) y las demas pasan.
3. Ninguna prueba contiene `if`, `for`, `switch` ni `try`.
```

- **Qué verificar en la salida:** el criterio 2 es el corazón del ejercicio. Si todas las pruebas generadas pasan a la primera, es que se escribieron contra el comportamiento actual y no contra las reglas: hay que descartarlas y repetir el prompt insistiendo en la restricción.
- **Advertencia:** escribir pruebas a partir del código en lugar de a partir de la especificación es el fallo más frecuente y el más peligroso, porque produce una suite verde que **certifica el defecto**. Es la razón por la que este prompt enumera las reglas explícitamente en lugar de decir "prueba este archivo".

### P-TST-02 — Auditar una prueba generada

- **Objetivo:** detectar pruebas defectuosas antes de confiar en ellas.
- **Cuándo usarlo:** Módulo 4, LAB-07, inmediatamente después de P-TST-01. Es el prompt de RA-4.2.

```text
# Contexto
Pruebas adjuntas, generadas para <tipo>. Convenciones:
`.github/instructions/tests.instructions.md`.

# Tarea
Audita estas pruebas y dime cuales NO sirven.

# Restricciones
- No corrijas nada en esta respuesta: solo diagnostica.
- Busca especificamente:
  a) pruebas que pasarian aunque el codigo estuviera mal (tautologicas)
  b) pruebas que afirman sobre el resultado de la propia llamada bajo prueba
  c) pruebas con mas de un assert logico
  d) pruebas con logica condicional
  e) pruebas que dependen del orden de ejecucion o de estado compartido
  f) pruebas que dependen del reloj, de la red o del sistema de archivos
  g) nombres que no describen el escenario ni el resultado esperado
  h) casos borde que faltan
  i) valores esperados calculados por el propio codigo en lugar de escritos a mano

# Formato de salida
Tabla: Prueba | Problema (letra de la lista) | Por que es un problema | Que
deberia hacerse
Al final: "Casos borde ausentes" y "Pruebas que si son solidas y por que".

# Criterios de aceptacion
1. Cada problema citado se justifica con la linea concreta de la prueba.
2. La seccion de pruebas solidas no esta vacia si alguna lo es.
```

- **Qué verificar en la salida:** el patrón (i) es el más difícil de ver a simple vista: una prueba que calcula el valor esperado con la misma fórmula que el código bajo prueba siempre pasa y no prueba nada. Revísalo tú, aunque la auditoría no lo mencione.
- **Advertencia:** pedirle al mismo modelo que audite lo que acaba de generar tiene un rendimiento limitado. Es útil, pero no sustituye la lectura humana ni el experimento decisivo: **estropea el código a propósito y comprueba que la prueba se pone en rojo**.

### P-TST-03 — Pruebas de umbral y de valores frontera

- **Objetivo:** cubrir sistemáticamente los límites de una regla.
- **Cuándo usarlo:** Módulo 4, siempre que exista un umbral numérico.

```text
# Contexto
Regla a probar: <cantidad >= 10 -> 5 % de descuento por volumen>.
Metodo: `IDescuentoCalculator.CalcularPrecioFinal(decimal precioBase, int cantidad,
bool esClientePreferente)`, devuelve el precio unitario final.

# Tarea
Genera las pruebas de valores frontera de esta regla.

# Restricciones
- Por cada umbral, prueba SIEMPRE el par: el valor inmediatamente anterior y el
  valor del umbral.
- Usa `[Theory]` + `[InlineData]` con los valores esperados escritos a mano,
  calculados por ti, NUNCA derivados de una formula en la propia prueba.
- Incluye el limite inferior absoluto del dominio (cantidad = 1) y el valor
  invalido inmediatamente anterior (cantidad = 0).
- Un assert logico por caso.
- Muestra el calculo de cada valor esperado en un comentario de una linea.

# Formato de salida
1. Tabla previa: cantidad | descuento aplicado | calculo | precio final esperado
2. La `[Theory]` completa en un bloque de codigo C#

# Criterios de aceptacion
1. Cada umbral aparece como par (anterior, umbral).
2. Los valores de la tabla y los de `[InlineData]` coinciden exactamente.
3. Ningun valor esperado se calcula dentro de la prueba.
```

- **Qué verificar en la salida:** recalcula a mano al menos tres valores de la tabla. Comprueba que la tabla y los `[InlineData]` coinciden: divergen con más frecuencia de lo que parece.
- **Advertencia:** un valor esperado calculado dentro de la prueba (`precioBase * 0.95m`) reproduce el error del código si el código está mal. Los valores esperados se escriben como literales.

### P-TST-04 — Pruebas del servicio con dobles

- **Objetivo:** probar la lógica del servicio aislada del repositorio.
- **Cuándo usarlo:** Módulo 4, LAB-07.

```text
# Contexto
Tipo bajo prueba: `ProductoService`. Dependencias del constructor:
`IProductoRepository`, `IDescuentoCalculator`, `ILogger<ProductoService>`.
Convenciones: `.github/instructions/tests.instructions.md`.
Metodo a probar: <ActualizarPrecioAsync>.

# Tarea
Genera las pruebas del metodo indicado usando dobles de prueba.

# Restricciones
- Aisla el servicio: el repositorio y la calculadora se sustituyen por dobles.
  Para `ILogger` usa `NullLogger<ProductoService>.Instance`, como el resto del archivo.
- Verifica el COMPORTAMIENTO observable (valor devuelto, excepcion lanzada), y solo
  cuando forme parte del contrato, la interaccion con las dependencias.
- No verifiques llamadas cuya frecuencia u orden no forme parte del contrato:
  eso acopla la prueba a la implementacion.
- Metodos asincronicos con `await` y `Should().ThrowAsync<T>()`. Nunca `.Result`
  ni `.Wait()`.
- Un assert logico por prueba.

# Cobertura minima
- Producto existente: devuelve el `ProductoResponse` con el precio calculado.
- Producto inexistente: devuelve `null`.
- El precio se PERSISTE: se verifica que se llamo a `ActualizarAsync` con el
  producto que lleva el precio nuevo.
- El calculo se DELEGA en `IDescuentoCalculator` y no se reimplementa.

# Formato de salida
Las pruebas completas en un bloque de codigo C#, con el archivo destino.
Debajo, tabla: Prueba | Que verifica | Doble usado | Por que ese doble

# Criterios de aceptacion
1. Existe una prueba que falla si la implementacion calcula pero no persiste.
2. Existe una prueba que falla si la implementacion reimplementa el descuento en
   lugar de delegarlo.
3. Ninguna prueba usa `.Result` ni `.Wait()`.
```

- **Qué verificar en la salida:** los criterios 1 y 2 son los que distinguen una batería útil de una decorativa. Compruébalos estropeando la implementación a propósito.
- **Advertencia:** la sobreverificación de interacciones (`Verify(..., Times.Once)` sobre todo) produce pruebas que se rompen con cualquier refactorización legítima. Verifica el contrato, no la implementación.

### P-TST-05 — Analizar cobertura y sus límites

- **Objetivo:** interpretar una medida de cobertura sin sobrevalorarla.
- **Cuándo usarlo:** Módulo 4, tras `dotnet test --collect:"XPlat Code Coverage"`.

```text
# Contexto
Repositorio `copilot-lab-catalogo`. Informe de cobertura adjunto, generado con
`dotnet test --collect:"XPlat Code Coverage"` (coverlet).

# Tarea
Interpreta el informe.

# Restricciones
- Distingue cobertura de LINEAS de cobertura de RAMAS.
- Identifica el codigo cubierto por pruebas que NO afirman nada relevante sobre el:
  ejecutado no es lo mismo que probado.
- No recomiendes subir la cobertura como objetivo en si mismo.
- Prioriza por riesgo: que codigo sin cubrir puede causar mas dano si falla.

# Formato de salida
1. Tabla: Archivo | Cobertura de lineas | Cobertura de ramas | Riesgo si falla (Alto/Medio/Bajo)
2. "Las cinco lagunas mas importantes", con la prueba concreta que las cerraria
3. "Codigo cubierto pero no probado" con ejemplos
4. "Que NO dice este informe"

# Criterios de aceptacion
1. El apartado 4 menciona al menos: que la cobertura no mide la calidad de las
   aserciones, que no detecta casos borde ausentes y que no dice nada sobre el
   comportamiento no implementado.
2. La priorizacion es por riesgo, no por porcentaje.
```

- **Qué verificar en la salida:** que no recomiende un porcentaje objetivo. Que el apartado 3 exista: en este repositorio, `LegacyPricingClient` puede quedar cubierto sin que ninguna prueba afirme nada sobre el log, que es exactamente el hallazgo de seguridad.
- **Advertencia:** un 100 % de cobertura de líneas es compatible con no detectar BUG-01: las líneas del comparador se ejecutan igual con `>` que con `>=`. Solo el **valor** de la prueba lo detecta, no su paso por la línea.

### P-TST-06 — Convertir criterios de aceptación en pruebas

- **Objetivo:** trazar cada criterio de aceptación de una historia a una prueba concreta.
- **Cuándo usarlo:** Módulo 4, cierre de LAB-07 y preparación del reto integrador.

```text
# Contexto
Historia: <HU-01>. Criterios de aceptacion CA-1 a CA-8 adjuntos.
Repositorio `copilot-lab-catalogo`, pruebas con xUnit + FluentAssertions.

# Tarea
Traza cada criterio de aceptacion a una forma concreta de verificacion.

# Restricciones
- Un criterio puede verificarse con: una prueba unitaria, una prueba de
  integracion, un comando manual (`curl`) o una inspeccion de codigo. Indica cual
  y por que.
- Si un criterio NO es verificable como esta redactado, dilo y propon una
  redaccion verificable.
- No escribas todavia el codigo de las pruebas: primero la traza.

# Formato de salida
Tabla: Criterio | Tipo de verificacion | Nombre de la prueba o comando exacto |
Que hace que falle
Debajo: "Criterios no verificables tal como estan redactados".

# Criterios de aceptacion
1. Los ocho criterios aparecen en la tabla.
2. Cada fila indica que hace FALLAR la verificacion, no solo que la hace pasar.
3. Al menos un criterio se verifica con inspeccion de codigo y se declara como tal.
```

- **Qué verificar en la salida:** que CA-2 ("se calcula con `IDescuentoCalculator`") esté marcado como verificación por inspección o por doble de prueba, no como prueba de valor: una implementación que reimplemente la regla y devuelva el mismo número pasaría una prueba de valor.
- **Advertencia:** los criterios que solo se pueden verificar por inspección son los que se pierden en la revisión. Si un criterio importante cae en esa categoría, conviene rediseñarlo para poder automatizarlo.

---

## Categoría 7 — Seguridad (`P-SEC`)

### P-SEC-01 — Revisión de seguridad de un archivo

- **Objetivo:** informe de seguridad accionable sobre un archivo concreto.
- **Cuándo usarlo:** Módulo 4, LAB-08. Equivale al prompt file `revisar-seguridad` del repositorio.

```text
# Contexto
Archivo: <src/Catalogo.Api/Infrastructure/LegacyPricingClient.cs>.
Repositorio `copilot-lab-catalogo`, .NET 8.

# Tarea
Revisa este archivo desde el punto de vista de la seguridad y produce un informe.

# Restricciones
- NO apliques cambios: este prompt solo produce el informe.
- Si encuentras un secreto, NO lo reproduzcas completo en la respuesta: indica el
  archivo y la linea.
- Clasifica como Alta unicamente lo explotable desde fuera del proceso.
- Revisa como minimo: validacion de entradas, secretos y datos sensibles
  (incluidos los que se escriben en LOGS), manejo de errores y filtracion de
  detalles internos, y superficie de ataque de las dependencias.
- No senales como hallazgo lo que sea una decision de diseno documentada del
  repositorio: si dudas, preguntalo en la seccion de falsos positivos.

# Formato de salida
Tabla: # | Severidad (Alta/Media/Baja) | Hallazgo | Archivo:linea | Correccion propuesta
Despues de la tabla:
- "Correcciones prioritarias": las tres mas importantes, con el fragmento corregido
- "Falsos positivos descartados": que revise y por que no es un problema

# Criterios de aceptacion
1. Los hallazgos relativos a secretos aparecen SEPARADOS por ubicacion: codigo,
   configuracion y logs.
2. Ningun valor de secreto se reproduce completo en la respuesta.
3. Cada correccion propuesta es aplicable sin agregar paquetes NuGet.
```

- **Qué verificar en la salida:** en `LegacyPricingClient.cs` hay **tres** hallazgos, no uno: la constante de la línea 9, la duplicación en `appsettings.json` y **la escritura de la credencial en el log** (líneas 35 a 37). Cuenta los hallazgos antes de dar la revisión por buena. Comprueba además si la respuesta respetó la restricción de no reproducir el secreto: no siempre lo hace.
- **Advertencia:** el hallazgo del log es el que más se pasa por alto, tanto por Copilot como por las personas, porque la revisión se detiene en la constante. Un secreto en un log agregado suele tener mayor alcance que uno en el código.

### P-SEC-02 — Barrido de secretos en el repositorio

- **Objetivo:** buscar credenciales en todo el árbol, no solo en un archivo.
- **Cuándo usarlo:** Módulo 4, LAB-08, antes de abrir el PR.

```text
# Contexto
Repositorio `copilot-lab-catalogo`. Adjunto la salida de:
`git grep -n -i -E "(api[_-]?key|secret|password|token|bearer|connectionstring)"`

# Tarea
Clasifica cada coincidencia.

# Restricciones
- Distingue: secreto real, secreto simulado del ejercicio, nombre de variable de
  configuracion sin valor, texto de documentacion y falso positivo.
- NO reproduzcas ningun valor completo.
- Para cada secreto identificado, indica ademas si quedaria en el historial de Git.

# Formato de salida
Tabla: Archivo:linea | Tipo de coincidencia | Riesgo | Queda en el historial (Si/No) | Accion
Debajo: "Ubicaciones que este barrido NO cubre".

# Criterios de aceptacion
1. El apartado final menciona al menos: el historial de Git, los archivos binarios,
   los archivos ignorados por `.gitignore` y las variables de entorno del equipo
   de desarrollo.
2. Las coincidencias en documentacion no se clasifican como riesgo.
```

- **Qué verificar en la salida:** las coincidencias esperadas son la constante de `LegacyPricingClient.cs`, la sección de `appsettings.json`, la advertencia del `README.md` y la mención en `AGENTS.md`. Las dos últimas son documentación, no hallazgos.
- **Advertencia:** este barrido es una red de arrastre, no una garantía. Un secreto con un nombre de variable creativo no aparece. Marca `[Depende del plan/política]`: el escaneo real de secretos es GitHub secret scanning, que se configura a nivel de repositorio y de organización y es independiente de Copilot.

### P-SEC-03 — Validación de entradas de un contrato público

- **Objetivo:** verificar que toda entrada externa está validada antes de usarse.
- **Cuándo usarlo:** Módulo 4, LAB-08, sobre los DTO y el controller.

```text
# Contexto
Archivos: `src/Catalogo.Api/Dtos/` (los cuatro DTO) y
`src/Catalogo.Api/Controllers/ProductosController.cs`.
Regla 6 de `.github/copilot-instructions.md`: toda entrada publica se valida
(DataAnnotations en el DTO y validacion explicita en el servicio); los datos
invalidos producen 400 con `ProblemDetails`.

# Tarea
Comprueba, campo por campo, si la regla 6 se cumple.

# Restricciones
- Recorre TODOS los campos de TODOS los DTO adjuntos, sin excepcion.
- Para cada campo indica: la restriccion que deberia tener segun el dominio, la
  que tiene hoy, y que valor concreto pasaria sin ser rechazado.
- Presta atencion a los casos que las DataAnnotations NO cubren: cadenas formadas
  solo por espacios, valores de enum fuera del rango definido, y `decimal` con mas
  decimales de los admitidos.
- No propongas correcciones aqui: solo el diagnostico.

# Formato de salida
Tabla: DTO | Campo | Restriccion esperada | Restriccion actual | Valor que pasa hoy sin ser rechazado | Riesgo
Debajo: "Campos sin ninguna validacion" y "Validaciones que las DataAnnotations no
pueden expresar".

# Criterios de aceptacion
1. Aparecen los cuatro DTO del repositorio.
2. La columna "valor que pasa hoy" contiene valores concretos, no descripciones.
3. Se menciona explicitamente el caso del nombre formado solo por espacios.
```

- **Qué verificar en la salida:** `CrearProductoRequest` no tiene **ninguna** DataAnnotation; si el informe dice lo contrario, no leyó el archivo. Comprueba que aparezca `ActualizarPrecioRequest`, que también carece de validación declarativa.
- **Advertencia:** `[EnumDataType]` no impide que un valor numérico fuera de rango llegue al enum si la deserialización no lo rechaza. Es un caso que conviene probar, no deducir.

### P-SEC-04 — Riesgos de las dependencias

- **Objetivo:** evaluar la superficie de ataque introducida por los paquetes.
- **Cuándo usarlo:** Módulo 4, LAB-08, y en toda revisión de un PR que toque el `.csproj`.

```text
# Contexto
Archivos adjuntos: `src/Catalogo.Api/Catalogo.Api.csproj` y
`tests/Catalogo.Api.Tests/Catalogo.Api.Tests.csproj`.

# Tarea
Evalua las dependencias declaradas.

# Restricciones
- Para cada paquete: para que se usa realmente en ESTE repositorio, si es de
  produccion o solo de pruebas, y que superficie anade.
- NO afirmes que un paquete tiene o no tiene vulnerabilidades conocidas: no puedes
  saberlo con certeza y esa informacion caduca. Indica en su lugar como comprobarlo.
- Senala las dependencias que solo se usan en pruebas y estan referenciadas en el
  proyecto de produccion, si las hubiera.

# Formato de salida
Tabla: Paquete | Version | Proyecto | Para que se usa aqui | Superficie que anade | Como verificar su estado
Debajo: "Comandos para comprobar vulnerabilidades y paquetes obsoletos".

# Criterios de aceptacion
1. El apartado final incluye `dotnet list package --vulnerable --include-transitive`
   y `dotnet list package --outdated`.
2. No se afirma el estado de vulnerabilidad de ningun paquete concreto.
```

- **Qué verificar en la salida:** el criterio 2 es el importante. Un modelo no tiene información fiable ni actualizada sobre avisos de seguridad; cualquier afirmación categórica sobre vulnerabilidades es sospechosa por construcción.
- **Advertencia:** el conocimiento del modelo sobre versiones y avisos tiene fecha de corte. Para dependencias, la fuente son los comandos de `dotnet` y GitHub Dependabot, no el chat.

### P-SEC-05 — Riesgos del propio código generado

- **Objetivo:** revisar críticamente el código que produjo Copilot antes de integrarlo.
- **Cuándo usarlo:** Módulo 4, antes de cada commit con código generado. Es el prompt de RA-4.3.

```text
# Contexto
Codigo generado por Copilot en esta sesion, adjunto.
Repositorio `copilot-lab-catalogo`, .NET 8.

# Tarea
Revisa este codigo como si lo hubiera escrito una persona ajena al equipo y
tuvieras que aprobarlo o rechazarlo.

# Restricciones
- Busca especificamente los riesgos tipicos del codigo generado:
  a) manejo de errores ausente o `catch` que traga la excepcion
  b) validacion de entradas omitida
  c) valores por defecto peligrosos (permisivos en lugar de restrictivos)
  d) uso de APIs no disponibles en net8.0
  e) paquetes NuGet nuevos introducidos sin declararlo
  f) datos sensibles en logs o en mensajes de error
  g) concurrencia sobre estado compartido sin proteccion
  h) codigo muerto o rutas inalcanzables
  i) convenciones del repositorio incumplidas
  j) fragmentos que parecen copiados de un contexto distinto (nombres, idioma o
     estilo que no encajan con el resto del archivo)
- No reescribas el codigo: produce el veredicto y los hallazgos.

# Formato de salida
1. Veredicto: Aprobar / Aprobar con cambios / Rechazar
2. Tabla: Hallazgo | Letra | Archivo:linea | Severidad | Que hacer
3. "Que verificaria ejecutando, no leyendo"

# Criterios de aceptacion
1. El veredicto es uno de los tres.
2. El apartado 3 propone al menos dos comprobaciones ejecutables.
3. Se revisa explicitamente el punto (e): paquetes NuGet nuevos.
```

- **Qué verificar en la salida:** el punto (j) merece atención humana. Un fragmento con nombres en inglés en un archivo cuyo resto está en español, o con un estilo de llaves distinto, suele indicar código traído de otro contexto. Es también la señal que conviene mirar respecto a **code referencing**.
- **Advertencia:** `[Depende del plan/política]` — con **Suggestions matching public code** en **Blocked** (valor por defecto en Copilot Business), las sugerencias que coinciden con código público no llegan a mostrarse. El ajuste se hereda de la organización y no se puede cambiar en la cuenta personal si el asiento lo asigna la organización. Aun así, la comprobación solo se aplica a las sugerencias **aceptadas sin modificar**: el código propio y las sugerencias alteradas no se comprueban.

---

## Categoría 8 — Pull Requests y revisión (`P-PRR`)

### P-PRR-01 — Redactar la descripción del Pull Request

- **Objetivo:** producir una descripción de PR revisable a partir del diff real.
- **Cuándo usarlo:** Módulo 4, LAB-08. Equivale al prompt file `preparar-pull-request` del repositorio.

```text
# Contexto
Rama actual: <feat/xx-precio-descuento>. Rama base: `main`.
Adjunto la salida de `git diff main...HEAD` y de `git status`.
Historia asociada: <HU-01> con los criterios CA-1 a CA-8.
Plantilla obligatoria: `.github/pull_request_template.md`.

# Tarea
Redacta la descripcion del Pull Request siguiendo la plantilla del repositorio.

# Restricciones
- Basate UNICAMENTE en el diff: no inventes cambios ni resultados de pruebas.
- Si el diff no demuestra que las pruebas pasan, deja esa casilla sin marcar y
  dilo explicitamente.
- Titulo: una linea, imperativo, maximo 72 caracteres, con el prefijo de la historia.
- Resumen: dos o tres frases sobre el problema y la solucion, sin detalles de
  implementacion.
- Si el diff toca configuracion sensible, senalalo en Riesgos.
- Los archivos generados o con formateo masivo van senalados aparte.
- Idioma: espanol.

# Formato de salida
La plantilla completa rellenada, en un bloque de codigo Markdown, con estas
secciones: Resumen | Historia de usuario | Cambios | Pruebas | Riesgos |
Decisiones sobre sugerencias de Copilot | Checklist.
Deja la tabla de decisiones sobre sugerencias de Copilot con las columnas y las
filas vacias: esa la relleno yo.

# Criterios de aceptacion
1. Cada cambio listado corresponde a un archivo que aparece en el diff.
2. Ninguna casilla del checklist esta marcada sin evidencia en el diff.
3. La seccion de criterios de aceptacion cubiertos referencia los CA por su identificador.
```

- **Qué verificar en la salida:** compara la lista de cambios con `git diff --stat`. Comprueba una por una las casillas marcadas. La tabla de decisiones sobre sugerencias de Copilot **la rellena la persona**: es una declaración de autoría y de criterio, no un resumen del diff.
- **Advertencia:** marcar casillas del checklist que no se han verificado es la forma más rápida de convertir la plantilla en ruido. Si la casilla no se cumplió, se deja sin marcar y se explica.

### P-PRR-02 — Autorrevisión antes de solicitar revisión

- **Objetivo:** encontrar los problemas evidentes antes de ocupar el tiempo del revisor.
- **Cuándo usarlo:** Módulo 4, LAB-08, justo antes de abrir el PR.

```text
#changes

# Contexto
Repositorio `copilot-lab-catalogo`. Cambios de la rama <feat/xx-precio-descuento>
frente a `main`.
Convenciones: `.github/copilot-instructions.md` y `.github/instructions/*.instructions.md`.

# Tarea
Revisa estos cambios como lo haria un revisor exigente del equipo.

# Restricciones
- Ordena los hallazgos por severidad, no por orden de aparicion en el diff.
- Separa lo que bloquea el merge de lo que es una mejora opcional.
- Comprueba explicitamente: convenciones del repositorio, sufijo `Async` y
  propagacion de `CancellationToken`, uso de `decimal` para importes,
  `ProblemDetails` en los errores, ausencia de `Console.WriteLine`,
  documentacion XML de los miembros publicos nuevos, y que no se hayan modificado
  pruebas existentes.
- No reescribas el codigo.

# Formato de salida
1. "Bloqueantes" (lista; vacia si no hay)
2. "Mejoras recomendadas"
3. "Observaciones informativas"
4. Tabla: Convencion del repositorio | Se cumple (Si/No/No aplica) | Evidencia

# Criterios de aceptacion
1. La tabla del apartado 4 recorre las diez reglas obligatorias de
   `.github/copilot-instructions.md`.
2. Cada bloqueante indica archivo y linea.
```

- **Qué verificar en la salida:** ejecuta `git diff main...HEAD -- tests/` para confirmar que ninguna prueba existente fue modificada. Es el punto que más veces se incumple sin querer.
- **Advertencia:** esta autorrevisión no sustituye a Copilot code review ni a la revisión humana. Es un filtro previo para no gastar la revisión ajena en lo obvio.

### P-PRR-03 — Clasificar los comentarios de Copilot code review

- **Objetivo:** decidir qué hacer con cada comentario recibido, en lugar de aceptarlos o ignorarlos en bloque.
- **Cuándo usarlo:** Módulo 4, LAB-08, tras solicitar la revisión. Es el prompt de RA-4.5.
- **Contexto que hay que adjuntar:** los comentarios de la revisión, pegados literalmente, y el código al que se refieren.

```text
# Contexto
Pull Request <numero> del repositorio `copilot-lab-catalogo`.
Comentarios de Copilot code review, pegados literalmente:
<pega aqui cada comentario con el archivo y la linea a la que apunta>
Adjunto los archivos afectados.

# Tarea
Clasifica cada comentario y propon la respuesta.

# Restricciones
- Categorias exactas: "Accionable ahora", "Accionable despues", "Informativo",
  "Incorrecto".
- Un comentario es "Incorrecto" si contradice el codigo, las convenciones del
  repositorio o los criterios de aceptacion de la historia. Justificalo con la
  evidencia concreta.
- Para "Accionable despues" indica que hace falta para poder abordarlo.
- No apliques ningun cambio: solo clasifica y redacta las respuestas.
- Las respuestas van en espanol, en tono profesional, y explican el porque.

# Formato de salida
Tabla: # | Comentario (resumen) | Archivo:linea | Categoria | Justificacion |
Respuesta que se publicara en el PR

# Criterios de aceptacion
1. Todos los comentarios recibidos aparecen clasificados; ninguno se omite.
2. Cada "Incorrecto" cita la evidencia que lo contradice.
3. Las respuestas explican el criterio, no solo la decision.
```

- **Qué verificar en la salida:** lee tú cada comentario antes de aceptar la clasificación. Un comentario correcto clasificado como "Incorrecto" es la peor salida posible de este prompt y hay que buscarla activamente.
- **Advertencia verificada:** `[GA]` **Copilot code review siempre deja una revisión de tipo "Comment"**, nunca "Approve" ni "Request changes". **No bloquea el merge ni cuenta para aprobaciones requeridas.** Además, Copilot lee las custom instructions de la **head branch** (la rama con los cambios), no de la base: si acabas de añadir instrucciones en tu rama, la revisión ya las respeta. Está disponible en **todos los planes de pago**; no requiere Enterprise.

### P-PRR-04 — Preparar la respuesta a un comentario discrepante

- **Objetivo:** argumentar técnicamente un desacuerdo, con evidencia.
- **Cuándo usarlo:** Módulo 4, cuando se decide no aplicar una sugerencia de la revisión.

```text
# Contexto
Comentario recibido en el PR:
<pega el comentario literal>
Decision tomada: NO aplicarlo.
Motivo preliminar: <tu razon en una frase>
Adjunto el codigo afectado y las convenciones aplicables.

# Tarea
Ayudame a redactar la respuesta y, antes, a comprobar si mi decision se sostiene.

# Restricciones
- PRIMERO evalua honestamente si el comentario tiene razon. Si la tiene, dimelo
  claramente y no redactes ninguna defensa.
- Si mi decision se sostiene, la respuesta debe apoyarse en evidencia concreta:
  una convencion del repositorio, un criterio de aceptacion, una prueba, una
  restriccion tecnica.
- Tono profesional, sin condescendencia, en espanol.
- Maximo seis lineas.

# Formato de salida
1. "Evaluacion de tu decision": Se sostiene / No se sostiene / Se sostiene parcialmente,
   con la razon
2. La respuesta redactada, solo si se sostiene
3. "Que registrar en la bitacora"

# Criterios de aceptacion
1. El apartado 1 es una evaluacion real, no una confirmacion automatica.
2. La respuesta cita evidencia verificable, no opinion.
```

- **Qué verificar en la salida:** si el apartado 1 siempre confirma tu decisión, el prompt no está funcionando: reformúlalo pidiendo explícitamente el argumento contrario más fuerte.
- **Advertencia:** el modelo tiende a estar de acuerdo con quien pregunta. Es un sesgo conocido y es exactamente el que hay que contrarrestar en una discusión técnica.

### P-PRR-05 — Resumen del PR para la persona revisora

- **Objetivo:** reducir el tiempo de revisión indicando dónde mirar.
- **Cuándo usarlo:** Módulo 4, en PR con más de tres archivos.

```text
# Contexto
Adjunto `git diff main...HEAD` del PR <numero>.

# Tarea
Escribe una guia de revision para quien vaya a revisar este PR.

# Restricciones
- Ordena los archivos por el orden en que conviene leerlos, no alfabeticamente ni
  por tamano.
- Indica, por archivo, que hay que mirar con atencion y que se puede leer en diagonal.
- Senala los cambios mecanicos (formateo, renombrados masivos, archivos generados)
  para que no consuman atencion.
- Indica los tres puntos donde es mas probable que haya un error, con su motivo.
- No resumas el codigo linea a linea.

# Formato de salida
1. "Por donde empezar" (lista ordenada de archivos con una frase cada uno)
2. "Cambios mecanicos que se pueden hojear"
3. "Los tres puntos de mayor riesgo"
4. "Que probar manualmente antes de aprobar" (comandos concretos)

# Criterios de aceptacion
1. El apartado 4 contiene comandos ejecutables.
2. El apartado 3 justifica cada punto con una razon tecnica.
```

- **Qué verificar en la salida:** que los comandos del apartado 4 se puedan ejecutar tal cual. Que el orden de lectura tenga sentido (contrato antes que implementación, implementación antes que pruebas).
- **Advertencia:** este resumen se publica como comentario del PR, no sustituye a la descripción. Y no se marca ninguna casilla del checklist basándose en él.

---

## Categoría 9 — Personalización y gobierno (`P-GOB`)

### P-GOB-01 — Redactar las instrucciones del repositorio

- **Objetivo:** producir o revisar `.github/copilot-instructions.md`.
- **Cuándo usarlo:** Módulo 5, LAB-09. `[GA]`
- **Alternativa integrada:** el slash command `/init` genera o actualiza `copilot-instructions.md` o `AGENTS.md` `[GA]`.

```text
# Contexto
Repositorio `copilot-lab-catalogo`: API REST de catalogo en .NET 8, capas
Controllers -> Services -> Repositories, pruebas con xUnit + FluentAssertions + Moq.
Archivo objetivo: `.github/copilot-instructions.md` (aplica a TODO el repositorio,
siempre, sin condicion de ruta).

# Tarea
Redacta (o revisa) las instrucciones de repositorio.

# Restricciones
- Solo reglas TRANSVERSALES: lo que aplica a todo el repositorio. Lo especifico de
  un tipo de archivo va en `.github/instructions/*.instructions.md` con `applyTo`.
- Cada regla debe ser verificable: alguien tiene que poder decir si una respuesta
  la cumple o no. Nada de "escribe codigo de calidad".
- Formula en positivo lo que se espera y en negativo solo lo que este prohibido.
- Maximo 40 lineas de reglas: un archivo largo diluye su propio efecto.
- No incluyas secretos, rutas locales ni nombres de personas.
- Idioma del archivo: espanol.

# Formato de salida
El archivo completo en un bloque de codigo Markdown, con estas secciones:
Contexto del proyecto | Idioma | Reglas obligatorias (numeradas) | Estilo | Que NO hacer.
Debajo, tabla: Regla | Como se comprueba que Copilot la respeto

# Criterios de aceptacion
1. Ninguna regla es inverificable.
2. Ninguna regla especifica de un tipo de archivo esta aqui en lugar de en un
   `.instructions.md`.
3. La tabla de comprobacion tiene una fila por regla.
```

- **Qué verificar en la salida:** que cada regla se pueda comprobar objetivamente. Después, la comprobación real: envía el mismo prompt antes y después de aplicar el archivo y compara las dos salidas. Sin esa comparación no hay evidencia de que las instrucciones surtan efecto.
- **Advertencia verificada:** `[GA]` las **custom instructions NO se aplican a las inline suggestions** que aparecen al escribir en el editor. Si el participante espera que el autocompletado cambie tras editar el archivo, la expectativa es errónea. Además, VS Code combina todos los archivos de instrucciones del proyecto **sin orden garantizado**.

### P-GOB-02 — Instrucciones específicas por ruta con `applyTo`

- **Objetivo:** aplicar convenciones distintas por capa.
- **Cuándo usarlo:** Módulo 5, LAB-09. `[GA]`

```text
# Contexto
Repositorio `copilot-lab-catalogo`. Ya existen tres archivos en
`.github/instructions/`: `csharp.instructions.md` (applyTo `**/*.cs`),
`tests.instructions.md` (applyTo `tests/**/*.cs`) y `api.instructions.md`
(applyTo `src/Catalogo.Api/Controllers/**/*.cs`).

# Tarea
Redacta un archivo `.instructions.md` nuevo para <src/Catalogo.Api/Dtos/**/*.cs>.

# Restricciones
- Frontmatter con `description` y `applyTo`. El glob de `applyTo` es RELATIVO a la
  raiz del workspace. Si se omite `applyTo`, las instrucciones NO se aplican
  automaticamente.
- Solo reglas que apliquen a esa ruta y que no esten ya en los tres archivos
  existentes: no repitas lo que ya dice `csharp.instructions.md`.
- Maximo 20 lineas de reglas.
- Incluye al menos un ejemplo de codigo correcto.

# Formato de salida
El archivo completo con su ruta, incluido el frontmatter YAML.
Debajo: tabla "Regla | Por que va aqui y no en copilot-instructions.md".

# Criterios de aceptacion
1. El glob de `applyTo` coincide exactamente con los archivos previstos y con
   ningun otro.
2. Ninguna regla duplica una de los tres archivos existentes.
3. El frontmatter es YAML valido y `applyTo` esta presente.
```

- **Qué verificar en la salida:** prueba el glob abriendo un archivo que **no** debería estar cubierto y comprobando que la instrucción no se aplica. Recuerda que se pueden dar varios patrones separados por coma: `applyTo: "**/*.cs,**/*.csproj"`.
- **Advertencia:** la precedencia oficial es **personal > repositorio (path-specific > repo-wide > agent) > organización**, pero **todas** las instrucciones relevantes se entregan al modelo. La precedencia ordena, no excluye. Y las **instrucciones personales no se soportan en VS Code**: solo en GitHub.com Chat, JetBrains y Copilot CLI.

### P-GOB-03 — Encapsular un prompt en un prompt file

- **Objetivo:** convertir un prompt que funcionó en un activo reutilizable del repositorio.
- **Cuándo usarlo:** Módulo 2, cierre de LAB-03 (parte B), y Módulo 5. `[Preview]` según GitHub Docs; documentado como disponible en VS Code, Visual Studio y JetBrains.
- **Alternativa integrada:** `/create-prompt`, o Command Palette → **Chat: New Prompt File**.

```text
# Contexto
Repositorio `copilot-lab-catalogo`. Ya existen cinco prompt files en
`.github/prompts/`: `explicar-modulo`, `generar-pruebas-xunit`, `revisar-seguridad`,
`documentar-xml` y `preparar-pull-request`.
Prompt que quiero encapsular:
<pega aqui el prompt que te funciono, completo>

# Tarea
Conviertelo en un prompt file reutilizable.

# Restricciones
- Extension `.prompt.md`, ubicacion `.github/prompts/`.
- Frontmatter con `name`, `description` y `agent`. Valores validos de `agent`:
  `ask`, `agent`, `plan`, o el nombre de un custom agent. Elige el que corresponda
  a lo que hace el prompt y justificalo.
- Parametriza lo que cambie entre usos con `${input:variable}` o pidiendolo
  explicitamente en el cuerpo.
- El prompt file debe funcionar SIN contexto adicional mas alla del archivo o la
  seleccion activa.
- No dupliques lo que ya hacen los cinco prompt files existentes: si se solapa,
  dilo y propon extender el existente en lugar de crear uno nuevo.
- Idioma: espanol.

# Formato de salida
El archivo completo con su ruta, incluido el frontmatter.
Debajo: como se invoca, y en que se diferencia de los cinco existentes.

# Criterios de aceptacion
1. El frontmatter es YAML valido con `name`, `description` y `agent`.
2. Se justifica el valor de `agent` elegido.
3. Se declara explicitamente si hay solapamiento con un prompt file existente.
```

- **Qué verificar en la salida:** invócalo con `/` seguido del nombre y comprueba que funciona sin contexto adicional. Los prompt files aparecen como slash commands propios, junto a los agent skills.
- **Advertencia verificada:** los agentes que corren sobre el **Agent Host no usan prompt files**; para usarlos ahí hay que convertirlos en **agent skills**. Los agent skills, además, funcionan en VS Code, Copilot CLI y Copilot cloud agent, mientras que las custom instructions son de VS Code y GitHub.com. Si el prompt tiene que ser portable, la respuesta correcta es un agent skill, no un prompt file.

### P-GOB-04 — Clasificar capacidades por dependencia de plan y política

- **Objetivo:** saber qué se puede usar realmente en la organización antes de diseñar un flujo de trabajo.
- **Cuándo usarlo:** Módulo 5, LAB-09 y DEMO-10. Es el prompt de RA-5.3.

```text
# Contexto
Organizacion con licencia **Copilot Enterprise** asignada por la organizacion.
IDE principal: Visual Studio Code. Stack: C#/.NET 8.
Capacidades que quiero clasificar: <Copilot cloud agent, Copilot CLI, servidores MCP,
Copilot code review automatico via rulesets, content exclusion, metricas de uso,
custom agents, agent skills, prompt files>.

# Tarea
Clasifica cada capacidad segun lo que hace falta para poder usarla.

# Restricciones
- Por cada capacidad indica: plan minimo requerido, politica de organizacion o
  empresa necesaria, estado por defecto de esa politica, y quien puede cambiarla.
- Si no puedes verificar un dato, escribe "No verificado" en lugar de suponerlo.
- Distingue "disponible en el plan" de "habilitado por politica": son cosas
  distintas y las dos tienen que cumplirse.
- Marca cada capacidad como [GA], [Preview] o [Depende del plan/politica].

# Formato de salida
Tabla: Capacidad | Plan minimo | Politica necesaria | Estado por defecto | Quien la
cambia | Etiqueta | Que hacer si esta deshabilitada
Debajo: "Capacidades que hay que verificar con el administrador ANTES de la sesion".

# Criterios de aceptacion
1. Aparecen las que estan deshabilitadas por defecto.
2. Ninguna afirmacion sin verificar se presenta como hecho.
```

- **Qué verificar en la salida:** contrasta con la ficha técnica antes de repetir nada en clase. Los datos verificados de referencia: **Copilot cloud agent** y **MCP servers on GitHub.com** están **deshabilitados por defecto** para usuarios con licencia Business o Enterprise asignada por la organización; **Copilot CLI** requiere que la política "Copilot CLI" esté habilitada; **content exclusion** requiere Business o Enterprise; las **métricas de uso** requieren la política "Copilot usage metrics" habilitada; **Copilot code review** está disponible en **todos los planes de pago** y no requiere Enterprise.
- **Advertencia:** este es el prompt donde una respuesta plausible y desactualizada hace más daño, porque se convierte en una decisión de gobierno. Las políticas y sus valores por defecto cambian; la fuente es la documentación oficial con fecha de consulta, no el chat.

### P-GOB-05 — Definir métricas de adopción con su fuente

- **Objetivo:** proponer métricas que se puedan obtener realmente, no indicadores de deseo.
- **Cuándo usarlo:** Módulo 5, LAB-09. Es el prompt de RA-5.4.

```text
# Contexto
Equipo de <N> personas con licencia Copilot Enterprise, trabajando en repositorios
.NET 8 alojados en GitHub Enterprise Cloud.
Objetivo declarado: <que quiere conseguir la organizacion, en una frase>.

# Tarea
Propon entre tres y cinco metricas de adopcion.

# Restricciones
- Cada metrica debe indicar su FUENTE DE DATOS CONCRETA: dashboard, endpoint REST,
  exportacion, o un dato que produzca el propio equipo. Si no tiene fuente
  obtenible, no la propongas.
- Incluye al menos una metrica que NO dependa de GitHub y que el equipo pueda
  producir por si mismo.
- Por cada metrica: definicion operativa, fuente, responsable, cadencia, umbral
  objetivo y, obligatoriamente, "como se puede falsear o malinterpretar".
- No propongas la tasa de aceptacion de sugerencias como medida de productividad
  ni de calidad.
- Distingue metricas de ACTIVIDAD de metricas de RESULTADO.

# Formato de salida
Tabla: Metrica | Tipo (actividad/resultado) | Definicion operativa | Fuente exacta |
Responsable | Cadencia | Umbral | Como se falsea o se malinterpreta
Debajo: "Que NO miden estas metricas".

# Criterios de aceptacion
1. Toda metrica tiene una fuente nombrada y obtenible.
2. Al menos una metrica es independiente de GitHub.
3. La columna "como se falsea" esta rellenada en todas las filas.
```

- **Qué verificar en la salida:** que las fuentes existan. Referencias verificadas: **Copilot usage metrics dashboard** (tendencias de 28 días), **code generation dashboard**, **Copilot impact dashboard** (cohortes **Passive users**, **Phase 1: Code first**, **Phase 2: Agent first**, **Phase 3: Multi-agent**; **adoption multiplier** = PRs mergeados promedio de usuarios comprometidos ÷ Passive users), **Metrics API** con header `X-GitHub-Api-Version: 2026-03-10` y la **exportación NDJSON**.
- **Advertencia verificada:** `[Depende del plan/política]` la política **"Copilot usage metrics"** debe estar habilitada; la latencia de los datos es de **2 a 3 días UTC** (la documentación oficial publica dos cifras distintas: "two full days" y "up to three full UTC days"); **Copilot Chat en GitHub.com y GitHub Mobile están excluidos** de las métricas; los datos de licencias y asientos **no** vienen en los informes de uso, sino en la API de gestión de usuarios; las analíticas a nivel de organización existen **desde el 12 de diciembre de 2025**.

---

## Antipatrones de prompting

Diez antipatrones observados de forma sistemática, cada uno con su versión corregida. La versión corregida no es más larga por decoración: es más larga porque contiene la información que el modelo necesitaba y no tenía.

### A-01 — El prompt de una línea sin contexto

- **Por qué falla:** el modelo no sabe qué archivo, qué framework, qué convenciones ni qué estado. Rellena los huecos con lo estadísticamente más común, que aquí no es lo correcto.

| Versión | Prompt |
|---|---|
| Incorrecta | `arregla el bug del descuento` |
| Corregida | Usa **P-DEP-01**: archivo, prueba que falla, salida literal de `dotnet test`, prohibición de tocar pruebas y formato de cinco pasos |

### A-02 — Pedir varias tareas en el mismo mensaje

- **Por qué falla:** el modelo reparte el esfuerzo, hace las tres cosas a medias y no se puede evaluar ninguna por separado. Además, si una está mal, hay que descartar las tres.

| Versión | Prompt |
|---|---|
| Incorrecta | `documenta esta clase, corrige el bug del tope, agrega pruebas y prepara el PR` |
| Corregida | Cuatro mensajes: **P-DOC-01**, luego **P-DEP-01** y su corrección, luego **P-TST-01**, luego **P-PRR-01**. Verificando entre uno y otro |

### A-03 — No decir qué está prohibido

- **Por qué falla:** sin restricciones explícitas, la salida modifica pruebas para que pasen, añade paquetes NuGet o cambia firmas públicas, y todo eso compila.

| Versión | Prompt |
|---|---|
| Incorrecta | `haz que pasen todas las pruebas` |
| Corregida | `Haz que pase la prueba <nombre>. Restricciones: no modifiques ni elimines pruebas existentes; si una prueba falla el defecto esta en el codigo de produccion. No agregues paquetes NuGet. No cambies firmas publicas. Framework de destino net8.0.` |

### A-04 — No definir el formato de salida

- **Por qué falla:** la respuesta llega como prosa, no se puede aplicar, no se puede comparar entre iteraciones y no se puede pegar en un documento.

| Versión | Prompt |
|---|---|
| Incorrecta | `dime qué problemas de seguridad tiene este archivo` |
| Corregida | `... Formato de salida: tabla con las columnas # \| Severidad (Alta/Media/Baja) \| Hallazgo \| Archivo:linea \| Correccion propuesta. Despues de la tabla, "Correcciones prioritarias" y "Falsos positivos descartados".` |

### A-05 — Preguntar sin criterios de aceptación

- **Por qué falla:** sin criterios escritos antes, el criterio real acaba siendo "compila", y ese criterio acepta código incorrecto.

| Versión | Prompt |
|---|---|
| Incorrecta | `implementa el cálculo de descuentos` |
| Corregida | `... Criterios de aceptacion: 1. cantidad = 10 con precioBase 100 devuelve 95,00. 2. cantidad = 100 con cliente preferente devuelve 80,00 (tope del 20 %). 3. (249,50, 49, false) devuelve 237,03 (redondeo AwayFromZero). 4. precioBase = 0 lanza ArgumentOutOfRangeException.` |

### A-06 — Preguntas cerradas que invitan a confirmar

- **Por qué falla:** el modelo tiende a coincidir con quien pregunta. Una pregunta que ya contiene la respuesta deseada obtiene esa respuesta.

| Versión | Prompt |
|---|---|
| Incorrecta | `este código está bien, ¿verdad?` |
| Corregida | `Enumera los tres problemas mas graves de este codigo, ordenados por severidad, cada uno con archivo:linea y con el impacto concreto. Si crees que no hay ninguno, dilo y explica que comprobaste para llegar a esa conclusion.` |

### A-07 — Aceptar la primera respuesta sin verificar

- **Por qué falla:** no es un defecto del prompt sino del proceso, y es el que más consecuencias tiene. Una salida plausible que no se ejecuta es una hipótesis, no un resultado.

| Versión | Práctica |
|---|---|
| Incorrecta | Copiar el código, hacer commit, abrir el PR |
| Corregida | Leer línea a línea, ejecutar `dotnet build` y `dotnet test`, ejecutar el escenario contra la API, registrar en la bitácora la decisión (aceptado, modificado o descartado) con su motivo y su verificación |

### A-08 — Pedirle al modelo que verifique su propia afirmación sin evidencia

- **Por qué falla:** "¿estás seguro?" produce una reafirmación o una retractación, ninguna de las dos basada en evidencia nueva.

| Versión | Prompt |
|---|---|
| Incorrecta | `¿estás seguro de que esa es la línea?` |
| Corregida | Usa **P-COM-06**: `Clasifica cada afirmacion de tu respuesta anterior en una tabla: Afirmacion \| Evidencia directa (archivo:linea) \| Inferencia sin evidencia \| Nivel de confianza. Toda afirmacion sin referencia archivo:linea va en la columna de inferencia.` Y después abre las referencias tú |

### A-09 — Dar contexto de más

- **Por qué falla:** adjuntar la solución entera para preguntar por un método diluye la atención del modelo, aumenta el coste y empeora la respuesta. Más contexto no es mejor contexto.

| Versión | Práctica |
|---|---|
| Incorrecta | Adjuntar `#codebase` más los 20 archivos del proyecto para preguntar por `CalcularPrecioFinal` |
| Corregida | Adjuntar `DescuentoCalculator.cs` y, si hace falta el contrato, `IDescuentoCalculator.cs`. Usar `#codebase` solo para orientarse al principio (**P-COM-03**), no para cada pregunta |

### A-10 — Pedir la especificación en lugar del código

- **Por qué falla:** si el prompt describe cómo debería comportarse el código, el modelo describe eso mismo y el defecto se vuelve invisible. Es el antipatrón que impide encontrar BUG-01.

| Versión | Prompt |
|---|---|
| Incorrecta | `este calculador aplica 5 % desde 10 unidades y topa en 20 %, ¿está bien implementado?` |
| Corregida | Usa **P-COM-02**: `Deduce, EXCLUSIVAMENTE a partir del codigo adjunto, las reglas que implementa. No consultes ninguna otra fuente. Describe lo que el codigo hace, no lo que parece que quiere hacer. Presta atencion a los operadores de comparacion y a su inclusividad.` Solo **después** se compara con la especificación |

---

## Relación con los prompt files del repositorio

El repositorio `copilot-lab-catalogo` incluye cinco prompt files en `.github/prompts/`. Se invocan escribiendo `/` seguido del nombre en la Chat view; también desde Command Palette → **Chat: Run Prompt**, o con el botón **play** al abrir el archivo en el editor `[Preview]` según GitHub Docs.

| Prompt file | `name` | `agent` | Prompts de esta biblioteca que cubre | Prompts que lo complementan | Cuándo conviene el prompt suelto en lugar del prompt file |
|---|---|---|---|---|---|
| `explicar-modulo.prompt.md` | Explicar modulo | `ask` | **P-COM-01** | P-COM-02, P-COM-04, P-COM-06, P-DOC-02 | Cuando hay que deducir las reglas de negocio sin la especificación delante (P-COM-02) o verificar afirmaciones previas (P-COM-06): el prompt file produce la estructura fija, no el contraste |
| `generar-pruebas-xunit.prompt.md` | Generar pruebas xUnit | `agent` | **P-TST-01** | P-TST-02, P-TST-03, P-TST-04, P-TST-06 | Cuando hay que probar un tipo con dependencias que exigen dobles (P-TST-04), o cubrir umbrales concretos con valores calculados a mano (P-TST-03) |
| `revisar-seguridad.prompt.md` | Revisar seguridad | `ask` | **P-SEC-01** | P-SEC-02, P-SEC-03, P-SEC-04, P-SEC-05 | Cuando el alcance no es un archivo sino todo el árbol (P-SEC-02), el conjunto de los DTO (P-SEC-03) o el código recién generado en la sesión (P-SEC-05) |
| `documentar-xml.prompt.md` | Documentar con XML | `agent` | **P-DOC-01** | P-DOC-02, P-DOC-03, P-DOC-05 | Cuando el entregable no es documentación en el código sino una nota de módulo, de endpoint o de causa raíz |
| `preparar-pull-request.prompt.md` | Preparar pull request | `agent` | **P-PRR-01** | P-PRR-02, P-PRR-03, P-PRR-04, P-PRR-05 | Antes de abrir el PR (autorrevisión, P-PRR-02) y después de recibir la revisión (clasificación de comentarios, P-PRR-03) |

**Categorías sin prompt file en el repositorio.** Planificación e implementación (`P-PLA`), refactorización (`P-REF`), depuración (`P-DEP`) y gobierno (`P-GOB`) no tienen prompt file. Es deliberado: son las tareas donde el contexto cambia más de una vez a otra y donde encapsular demasiado pronto produce una plantilla que nadie usa. Convertir uno de estos prompts en prompt file propio es precisamente la actividad de **LAB-03 parte B** (con **P-GOB-03**), y la decisión de cuál merece encapsularse forma parte del ejercicio.

**Antes de crear un prompt file nuevo**, comprueba estas tres cosas: que el prompt se haya usado con éxito al menos tres veces, que lo que cambia entre usos sea parametrizable, y que no se solape con uno de los cinco existentes. Si se solapa, extiende el existente en lugar de crear otro: dos prompt files parecidos se degradan hasta que nadie sabe cuál usar.

---

## Cómo se evalúa el uso de esta biblioteca

En los laboratorios no se evalúa que el participante copie el prompt correcto, sino que:

1. **Escriba los criterios de aceptación antes** de enviar el prompt.
2. **Verifique la salida** contra esos criterios, con el comando o la inspección indicados.
3. **Registre la decisión** (aceptado, modificado o descartado) en la bitácora, con motivo y método de verificación.
4. **Refine** cuando la salida no cumpla, señalando la desviación concreta en lugar de repetir la petición.
5. **Sepa parar**: reconocer cuándo es más rápido y más seguro escribirlo a mano también es una decisión que se registra.

Un participante que copie los 49 prompts sin verificar ninguna salida obtiene peor evaluación que uno que use tres y verifique las tres.

