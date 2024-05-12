# Dar de alta un expediente

Para testear el alta de un expediente, se debe generar una instancia de la clase
`ExpedienteAltaCasoDeUso`, la cual recibe 3 parámetros en su constructor:

    IExpedienteRepositorio expedienteRepositorio,
    IExpedienteValidador   expedienteValidador,
    IServicioAutorizacion  servicioAutorizacion

Donde:

- `expedienteRepositorio` es una instancia de una clase que implementa la interfaz `IExpedienteRepositorio`
- `expedienteValidador` es una instancia de una clase que implementa la interfaz `IExpedienteValidador`
- `servicioAutorizacion` es una instancia de una clase que implementa la interfaz `IServicioAutorizacion`

Para testear el caso de uso, se debe llamar al método `Ejecutar` de la instancia
de `ExpedienteAltaCasoDeUso`, el cual recibe los siguientes 2 parámetros:

    Expediente expediente,
    int idUsuario

Donde:

- `expediente` es una instancia de la clase `Expediente`
- `idUsuario` es el id del usuario que está dando de alta el expediente

Y retorna una instancia de la clase `Expediente`.

## Ejemplo de test

```csharp
Usuario                    usuario                    = new();
IExpedienteValidador       expedienteValidador        = new ExpedienteValidador();
IExpedienteRepositorio     expedienteRepositorio      = new RepositorioExpedienteTxt();
IServicioAutorizacion      servicioAutorizacion       = new ServicioAutorizacionProvisorio();

ExpedienteAltaCasoDeUso altaCasoDeUso = new(expedienteRepositorio, expedienteValidador, servicioAutorizacion);

Expediente expedienteNuevo = new()
{
    Caratula = "Caratula de prueba del expediente",
};

try
{
    Expediente expedienteGuardado = altaCasoDeUso.Ejecutar(expedienteNuevo, usuario.Id);
    Console.WriteLine($"Expediente guardado con id: {expedienteGuardado.Id}");
}
catch (Exception e)
{
    Console.WriteLine(e.Message);
}
```

En el ejemplo anterior, la salida por consola esperada es:

    Expediente guardado con id: 1

Si se vuelven a ejecutar las mismas líneas de código, la salida esperada es:

    Expediente guardado con id: 2

En ambos casos anteriores, los datos se guardarán en el archivo `Expedientes.txt`

Si se intenta guardar un expediente sin caratula, la salida esperada es:

    La carátula no puede estar vacía

En este caso, no se guardará ningún expediente en el archivo `Expedientes.txt` ya que no cumple con la validación requerida.

---
# Dar de baja un expediente
Para testear la baja de un expediente, se debe generar una instancia de la clase `ExpedienteBajaCasoDeUso`, la cual recibe 3 parámetros en su constructor:

	IExpedienteRepositorio expedienteRepositorio,
	ITramiteRepositorio    tramiteRepositorio,
	IServicioAutorizacion  servicioAutorizacion

Donde:

-  `expedienteRepositorio` es una instancia de una clase que implementa la interfaz `IExpedienteRepositorio`
-  `tramiteRepositorio` es una instancia de una clase que implementa la interfaz `ITramiteRepositorio`
- `servicioAutorizacion` es una instancia de una clase que implementa la interfaz `IServicioAutorizacion`

Para testear el caso de uso, se debe llamar al método `Ejecutar` de la instancia de `ExpedienteBajaCasoDeUso`, el cual recibe los siguientes 2 parámetros:

	int idExpediente,
	int idUsuario

Donde:

- `idExpediente` es el id del expediente al que se quiere dar de baja
- `idUsuario` es el id del usuario que está dando de baja al expediente
## Ejemplo de test

```csharp
Usuario                usuario               = new();
ITramiteRepositorio    tramiteRepositorio    = new RepositorioTramiteTxt();
IExpedienteValidador   expedienteValidador   = new ExpedienteValidador();
IExpedienteRepositorio expedienteRepositorio = new RepositorioExpedienteTxt();
IServicioAutorizacion  servicioAutorizacion  = new ServicioAutorizacionProvisorio();

ExpedienteAltaCasoDeUso altaCasoDeUso = new(expedienteRepositorio, expedienteValidador, servicioAutorizacion);
ExpedienteBajaCasoDeUso bajaCasoDeUso = new(expedienteRepositorio, tramiteRepositorio, servicioAutorizacion);

Expediente expedienteNuevo = new()
                                {
                                    Caratula = "Caratula de prueba del expediente",
                                };

// Añado el expediente
int expedienteId = 0; 
try
{
    Expediente expedienteGuardado = altaCasoDeUso.Ejecutar(expedienteNuevo, usuario.Id);
    Console.WriteLine($"Expediente guardado con id: {expedienteGuardado.Id}");
    expedienteId = expedienteGuardado.Id;
}
catch (Exception e)
{
	Console.WriteLine(e.Message);
}

// Elimino el expediente
try {
    if (expedienteId > 0) {
        bajaCasoDeUso.Ejecutar(expedienteId, usuario.Id);
        Console.WriteLine($"Expediente con id: {expedienteId} eliminado");
    }
}
catch (Exception e) 
{
    Console.WriteLine(e.Message);
}
```

En el ejemplo anterior, la salida por consola esperada es:

    Expediente guardado con id: 1
    Expediente con id: 1 eliminado

Si se vuelven a ejecutar las mismas líneas de código, la salida esperada es:

    Expediente guardado con id: 1
    Expediente con id: 1 eliminado

En ambos casos anteriores, los datos se guardarán en el archivo `Expedientes.txt`, el cuál al final estará vacío debido a la eliminación de los expedientes creados.

Si se intenta eliminar un expediente que no existe, la salida esperada es:

    El expediente a eliminar no existe.

---
# Buscar un expediente por ID sin sus trámites
Para testear el buscar un expediente por ID sin sus trámites, se debe generar una instancia de la clase `ExpedienteBuscarPorIdCasoDeUso`, la cual recibe el siguiente parámetro en su constructor:

    IExpedienteRepositorio expedienteRepositorio

Donde:

- `expedienteRepositorio` es una instancia de una clase que implementa la interfaz `IExpedienteRepositorio`

Para testear el caso de uso se debe llamar al método `Ejecutar` de la instancia de `ExpedienteBuscarPorIdCasoDeUso`, el cual recibe el siguiente parámetro:

    int idExpediente

Donde:

- `idExpediente` es el id del expediente al que se quiere buscar

Y retorna una instancia de la clase `Expediente`.
## Ejemplo de test

```csharp
Usuario                usuario               = new();
RepositorioTramiteTxt  tramiteRepositorio    = new();
IExpedienteValidador   expedienteValidador   = new ExpedienteValidador();
IExpedienteRepositorio expedienteRepositorio = new RepositorioExpedienteTxt();
IServicioAutorizacion  servicioAutorizacion  = new ServicioAutorizacionProvisorio();
EspecificacionCambioEstado especificacionCambioEstado = new(tramiteRepositorio);
ServicioActualizacionEstado servicioActualizacionEstado = new(expedienteRepositorio, especificacionCambioEstado);
ITramiteValidador      tramiteValidador      = new TramiteValidador();

ExpedienteAltaCasoDeUso expedienteAltaCasoDeUso = new(expedienteRepositorio, expedienteValidador, servicioAutorizacion);
TramiteAltaCasoDeUso tramiteAltaCasoDeUso = new(tramiteRepositorio, tramiteValidador, servicioAutorizacion, servicioActualizacionEstado);
ExpedienteBuscarPorIdCasoDeUso expedienteBuscarPorIdCasoDeUso = new(expedienteRepositorio);

Expediente expedienteNuevo = new()
                             {
                                 Caratula = "Caratula de prueba del expediente",
                             };

Tramite tramiteNuevo1 = new() {
                                 Contenido = "Contenido de prueba del trámite 1",
                                 IdExpediente = 1,
                             };

Tramite tramiteNuevo2 = new() {
                                 Contenido    = "Contenido de prueba del trámite 2",
                                 IdExpediente = 1,
                             };

// Añado el expediente
try
{
    Expediente expedienteGuardado = expedienteAltaCasoDeUso.Ejecutar(expedienteNuevo, usuario.Id);
    Console.WriteLine($"Expediente guardado con id: {expedienteGuardado.Id}");
}
catch (Exception e)
{
    Console.WriteLine(e.Message);
}

//Añado los trámites
try {
    tramiteAltaCasoDeUso.Ejecutar(tramiteNuevo1, usuario.Id);
    tramiteAltaCasoDeUso.Ejecutar(tramiteNuevo2, usuario.Id);
}
catch (Exception e) 
{
    Console.WriteLine(e.Message);
}

// Busco el expediente por ID sin sus trámites
try {
    Expediente? expedienteBuscado = expedienteBuscarPorIdCasoDeUso.Ejecutar(1);
    Console.WriteLine($"Expediente buscado: {expedienteBuscado?.Caratula}");
	
	if (expedienteBuscado?.Tramites.Count == 0) {  
	    Console.WriteLine("Sin trámites");  
	}
}
catch (Exception e) 
{
    Console.WriteLine(e.Message);
}
```

En el ejemplo anterior, la salida por consola esperada es:

    Expediente guardado con id: 1
    Expediente buscado: Caratula de prueba del expediente
	Sin trámites

Si se vuelven a ejecutar las mismas líneas de código, la salida esperada es:

    Expediente guardado con id: 2
    Expediente buscado: Caratula de prueba del expediente
    Sin trámites

En ambos casos anteriores, los datos se guardarán en el archivo `Expedientes.txt` y en `Tramites.txt`

---
# Buscar un expediente por ID con sus trámites
Para testear el buscar un expediente por ID con sus trámites, se debe generar una instancia de la clase `ExpedienteBuscarPorIdConTramitesCasoDeUso`, la cual recibe 2 parámetros en su constructor:

    IExpedienteRepositorio expedienteRepositorio
    ITramiteRepositorio    tramiteRepositorio

Donde:

- `expedienteRepositorio` es una instancia de una clase que implementa la interfaz `IExpedienteRepositorio`
- `tramiteRepositorio` es una instancia de una clase que implementa la interfaz `ITramiteRepositorio`

Para testear el caso de uso se debe llamar al método `Ejecutar` de la instancia de `ExpedienteBuscarPorIdConTramitesCasoDeUso`, el cual recibe el siguiente parámetro:

    int idExpediente

Donde:

- `idExpediente` es el id del expediente al que se quiere buscar

Y retorna una instancia de la clase `Expediente`.
## Ejemplo de test

```csharp
Usuario                    usuario                    = new();
RepositorioTramiteTxt      tramiteRepositorio         = new();
IExpedienteValidador       expedienteValidador        = new ExpedienteValidador();
ITramiteValidador          tramiteValidador          = new TramiteValidador();
IExpedienteRepositorio     expedienteRepositorio      = new RepositorioExpedienteTxt();
IServicioAutorizacion      servicioAutorizacion       = new ServicioAutorizacionProvisorio();
EspecificacionCambioEstado especificacionCambioEstado = new(tramiteRepositorio);
ServicioActualizacionEstado servicioActualizacionEstado = new(expedienteRepositorio, especificacionCambioEstado);
ExpedienteAltaCasoDeUso expedienteAltaCasoDeUso = new(expedienteRepositorio, expedienteValidador, servicioAutorizacion);

TramiteAltaCasoDeUso tramiteAltaCasoDeUso = new(tramiteRepositorio, tramiteValidador, servicioAutorizacion, servicioActualizacionEstado);
ExpedienteBuscarPorIdConTramitesCasoDeUso expedienteBuscarPorIdConTramitesCasoDeUso = new(expedienteRepositorio, tramiteRepositorio);

Expediente expedienteNuevo = new() {Caratula = "Caratula de prueba del expediente",};
Tramite tramiteNuevo1 = new() {Contenido = "Contenido de prueba del trámite 1", IdExpediente = 1,};
Tramite tramiteNuevo2 = new() {Contenido = "Contenido de prueba del trámite 2",IdExpediente = 1,};

// Añado el expediente
try {
    Expediente expedienteGuardado = expedienteAltaCasoDeUso.Ejecutar(expedienteNuevo, usuario.Id);
    Console.WriteLine($"Expediente guardado con id: {expedienteGuardado.Id}");
}
catch (Exception e) {
    Console.WriteLine(e.Message);
}

//Añado los trámites
try {
    tramiteAltaCasoDeUso.Ejecutar(tramiteNuevo1, usuario.Id);
    tramiteAltaCasoDeUso.Ejecutar(tramiteNuevo2, usuario.Id);
}
catch (Exception e) {
    Console.WriteLine(e.Message);
}

// Busco el expediente por ID con sus trámites
try {
    Expediente? expedienteBuscado = expedienteBuscarPorIdConTramitesCasoDeUso.Ejecutar(1);
    Console.WriteLine($"Expediente buscado: {expedienteBuscado?.Caratula}");
    Console.WriteLine("Tramites: ");

    if (expedienteBuscado?.Tramites != null) {  
	    foreach (Tramite tramite in expedienteBuscado.Tramites) {  
	        Console.WriteLine($"Id: {tramite.Id}, Contenido: {tramite.Contenido}");  
	    }  
	}
}
catch (Exception e) {
    Console.WriteLine(e.Message);
}
```

En el ejemplo anterior, la salida por consola esperada es:

    Expediente guardado con id: 1
	Expediente buscado: Caratula de prueba del expediente
	Tramites: 
	Id: 1, Contenido: Contenido de prueba del trámite 1
	Id: 2, Contenido: Contenido de prueba del trámite 2

Si se vuelven a ejecutar las mismas líneas de código, la salida esperada es:

    Expediente guardado con id: 2
	Expediente buscado: Caratula de prueba del expediente
	Tramites: 
	Id: 1, Contenido: Contenido de prueba del trámite 1
	Id: 2, Contenido: Contenido de prueba del trámite 2
	Id: 3, Contenido: Contenido de prueba del trámite 1
	Id: 4, Contenido: Contenido de prueba del trámite 2

En ambos casos anteriores, los datos se guardarán en el archivo `Expedientes.txt` y en `Tramites.txt`

---
# Listar todos los expedientes sin sus trámites
Para testear el listar todos los expedientes sin sus trámites, se debe generar una instancia de la clase `ExpedienteListarCasoDeUso`, la cual recibe el siguiente parámetro en su constructor:

    IExpedienteRepositorio expedienteRepositorio

Donde:

- `expedienteRepositorio` es una instancia de una clase que implementa la interfaz `IExpedienteRepositorio`

Para testear el caso de uso se debe llamar al método `Ejecutar` de la instancia de `ExpedienteListarCasoDeUso`, la cual retorna `List<Expediente>`
## Ejemplo de test

```csharp
Usuario                    usuario                    = new();
IExpedienteValidador       expedienteValidador        = new ExpedienteValidador();
IExpedienteRepositorio     expedienteRepositorio      = new RepositorioExpedienteTxt();
IServicioAutorizacion      servicioAutorizacion       = new ServicioAutorizacionProvisorio();

ExpedienteAltaCasoDeUso expedienteAltaCasoDeUso = new(expedienteRepositorio, expedienteValidador, servicioAutorizacion);
ExpedienteListarCasoDeUso expedienteListarCasoDeUso = new(expedienteRepositorio);

Expediente expedienteNuevo1 = new() { Caratula = "Caratula de prueba del expediente1",};
Expediente expedienteNuevo2 = new() {Caratula = "Caratula de prueba del expediente2",};

// Añado los expedientes
try {
    Expediente expedienteGuardado1 = expedienteAltaCasoDeUso.Ejecutar(expedienteNuevo1, usuario.Id);
    Console.WriteLine($"Expediente guardado con id: {expedienteGuardado1.Id}");
    Expediente expedienteGuardado2 = expedienteAltaCasoDeUso.Ejecutar(expedienteNuevo2, usuario.Id);
    Console.WriteLine($"Expediente guardado con id: {expedienteGuardado2.Id}");
}
catch (Exception e) {
    Console.WriteLine(e.Message);
}

// Listo los expedientes
List<Expediente> expedientes = expedienteListarCasoDeUso.Ejecutar().ToList();

foreach (Expediente expediente in expedientes) {
    Console.WriteLine(expediente.Caratula);
}
```

En el ejemplo anterior, la salida por consola esperada es:

    Expediente guardado con id: 1
	Expediente guardado con id: 2
	Caratula de prueba del expediente1
	Caratula de prueba del expediente2


Si se vuelven a ejecutar las mismas líneas de código, la salida esperada es:

    Expediente guardado con id: 3
	Expediente guardado con id: 4
	Caratula de prueba del expediente1
	Caratula de prueba del expediente2
	Caratula de prueba del expediente1
	Caratula de prueba del expediente2

En ambos casos anteriores, los datos se guardarán en el archivo `Expedientes.txt`

---

# Listar todos los expedientes con sus trámites
Para testear el listar todos los expedientes con sus trámites, se debe generar una instancia de la clase `ExpedienteListarConTramitesCasoDeUso`, la cual recibe 2 parámetros en su constructor:

    IExpedienteRepositorio expedienteRepositorio
    ITramiteRepositorio    tramiteRepositorio

Donde:

- `expedienteRepositorio` es una instancia de una clase que implementa la interfaz `IExpedienteRepositorio`
- `tramiteRepositorio` es una instancia de una clase que implementa la interfaz `ITramiteRepositorio`

Para testear el caso de uso se debe llamar al método `Ejecutar` de la instancia de `ExpedienteListarConTramitesCasoDeUso`, la cual retorna `List<Expediente>`
## Ejemplo de test
#TODO

---

# Modificar un expediente
Para testear el modificar un expediente, se debe generar una instancia de la clase
`ExpedienteModificarCasoDeUso`, la cual recibe 3 parámetros en su constructor:

    IExpedienteRepositorio expedienteRepositorio,
    IExpedienteValidador   expedienteValidador,
    IServicioAutorizacion  servicioAutorizacion

Donde:

- `expedienteRepositorio` es una instancia de una clase que implementa la interfaz `IExpedienteRepositorio`
- `expedienteValidador` es una instancia de una clase que implementa la interfaz `IExpedienteValidador`
- `servicioAutorizacion` es una instancia de una clase que implementa la interfaz `IServicioAutorizacion`

Para testear el caso de uso, se debe llamar al método `Ejecutar` de la instancia
de `ExpedienteModificarCasoDeUso`, el cual recibe los siguientes 2 parámetros:

    Expediente expediente,
    int idUsuario

Donde:

- `expediente` es una instancia de la clase `Expediente`
- `idUsuario` es el id del usuario que está modificando al expediente
## Ejemplo de test
#TODO

---

# Alta de un trámite
Para testear el alta de un trámite, se debe generar una instancia de la clase
`TramiteAltaCasoDeUso`, la cual recibe 4 parámetros en su constructor:

    ITramiteRepositorio         tramiteRepositorio,
    ITramiteValidador           tramiteValidador,
    IServicioAutorizacion       servicioAutorizacion,
    ServicioActualizacionEstado servicioActualizacionEstado

Donde:

- `tramiteRepositorio` es una instancia de una clase que implementa la interfaz `ITramiteRepositorio`
- `tramiteValidador` es una instancia de una clase que implementa la interfaz `ITramiteValidador`
- `servicioAutorizacion` es una instancia de una clase que implementa la interfaz `IServicioAutorizacion`
- `servicioActualizacionEstado` es una instancia de la clase `ServicioActualizacionEstado`

Para testear el caso de uso, se debe llamar al método `Ejecutar` de la instancia
de `TramiteAltaCasoDeUso`, el cual recibe los siguientes 2 parámetros:

    Tramite tramite,
    int idUsuario

Donde:

- `tramite` es una instancia de la clase `Tramite`
- `idUsuario` es el id del usuario que está modificando al trámite

Y retorna una instancia de la clase `Tramite`.
## Ejemplo de test
#TODO

---

# Baja de un trámite
Para testear la baja de un trámite, se debe generar una instancia de la clase
`TramiteBajaCasoDeUso`, la cual recibe 3 parámetros en su constructor:

    ITramiteRepositorio         tramiteRepositorio,
	ServicioActualizacionEstado servicioActualizacionEstado,
	IServicioAutorizacion       servicioAutorizacion

Donde:

- `tramiteRepositorio` es una instancia de una clase que implementa la interfaz `ITramiteRepositorio`
- `servicioActualizacionEstado` es una instancia de la clase `ServicioActualizacionEstado`
- `servicioAutorizacion` es una instancia de una clase que implementa la interfaz `IServicioAutorizacion`

Para testear el caso de uso, se debe llamar al método `Ejecutar` de la instancia
de `TramiteBajaCasoDeUso`, el cual recibe los siguientes 2 parámetros:

    int idTramite,
    int idUsuarioActual

Donde:

- `idTramite` es el id del trámite a dar de baja
- `idUsuarioActual` es el id del usuario que está dando de baja al trámite
## Ejemplo de test
#TODO

---
# Buscar trámite por etiqueta
Para testear el buscar un trámite por etiqueta, se debe generar una instancia de la clase `TramiteConsultaPorEtiquetaCasoDeUso`, la cual recibe un parámetro en su constructor:

    ITramiteRepositorio    tramiteRepositorio

Donde:

- `tramiteRepositorio` es una instancia de una clase que implementa la interfaz `ITramiteRepositorio`

Para testear el caso de uso se debe llamar al método `Ejecutar` de la instancia de `TramiteConsultaPorEtiquetaCasoDeUso`, el cual recibe el siguiente parámetro:

    EtiquetaTramite etiquetaTramite

Donde:

- `etiquetaTramite` es el valor del enumerador `EtiquetaTramite` que se quiere buscar

Y retorna `IEnumerable<Tramite>`
## Ejemplo de test
#TODO

---

# Modificar un trámite
Para testear el modificar un trámite, se debe generar una instancia de la clase `TramiteModificacionCasoDeUso`, la cual recibe 4 parámetros en su constructor:

    ITramiteRepositorio         repositorioTramite,
    ITramiteValidador           tramiteValidador,
    ServicioActualizacionEstado servicioActualizacionEstado,
    IServicioAutorizacion       servicioAutorizacion

Donde:

- `repositorioTramite` es una instancia de una clase que implementa la interfaz `ITramiteRepositorio`
- `tramiteValidador` es una instancia de una clase que implementa la interfaz `ITramiteValidador`
- `servicioActualizacionEstado` es una instancia de la clase `ServicioActualizacionEstado`
- `servicioAutorizacion` es una instancia de una clase que implementa la interfaz `IServicioAutorizacion`

Para testear el caso de uso se debe llamar al método `Ejecutar` de la instancia de `TramiteModificacionCasoDeUso`, el cual recibe los siguientes 2 parámetros:

    Tramite tramite,
    int idUsuario

Donde:

- `tramite` es una instancia de la clase `Tramite`
- `idUsuario` es el id del usuario que está modificando al expediente
## Ejemplo de test
#TODO

---