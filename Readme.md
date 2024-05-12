### Todos los ejemplos asumen que los archivos `Expedientes.txt` y `Tramites.txt` no existen, o están vacíos.
### Las salidas de consola esperadas pueden variar si los archivos ya contienen datos, y al momento de imprimir expedientes o trámites, las fechas de creación y/o modificación pueden variar con respecto al resultado esperado que se detalla en cada caso de uso. Se recomienda borrar los archivos antes de ejecutar cada uno de los ejemplos para obtener las salidas esperadas lo más parecidas posibles.

---
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
Usuario                usuario               = new();
IExpedienteValidador   expedienteValidador   = new ExpedienteValidador();
IExpedienteRepositorio expedienteRepositorio = new RepositorioExpedienteTxt();
IServicioAutorizacion  servicioAutorizacion  = new ServicioAutorizacionProvisorio();

ExpedienteAltaCasoDeUso altaCasoDeUso = new(expedienteRepositorio, expedienteValidador, servicioAutorizacion);

Expediente expediente1 = new()
                             {
                                 Caratula = "Carátula de prueba del expediente",
                             };

Expediente expediente2 = new()
                             {
                                 Caratula = "",
                             };

Expediente expediente3 = new()
                             {
                                 Id = 999,
                                 Caratula = "Carátula de prueba del expediente",
                             };

Expediente expediente4 = new()
                             {
                                 Caratula = "Otra carátula de prueba",
                             };

try {
    altaCasoDeUso.Ejecutar(expediente1, usuario.Id);
    Console.WriteLine($"Expediente guardado con ID {expediente1.Id}");
} catch (AutorizacionException ex) {
    Console.WriteLine($"Error de autorización: {ex.Message}");
} catch (ValidacionException ex) {
    Console.WriteLine($"Error de validación: {ex.Message}");
} catch (RepositorioException ex) {
    Console.WriteLine($"Error de repositorio: {ex.Message}");
} catch (Exception ex) {
    Console.WriteLine($"Error inesperado: {ex.Message}");
}

try {
    altaCasoDeUso.Ejecutar(expediente2, usuario.Id);
    Console.WriteLine($"Expediente guardado con ID {expediente2.Id}");
} catch (AutorizacionException ex) {
    Console.WriteLine($"Error de autorización: {ex.Message}");
} catch (ValidacionException ex) {
    Console.WriteLine($"Error de validación: {ex.Message}");
} catch (RepositorioException ex) {
    Console.WriteLine($"Error de repositorio: {ex.Message}");
} catch (Exception ex) {
    Console.WriteLine($"Error inesperado: {ex.Message}");
}

try {
    altaCasoDeUso.Ejecutar(expediente3, usuario.Id);
    Console.WriteLine($"Expediente guardado con ID {expediente3.Id}");
} catch (AutorizacionException ex) {
    Console.WriteLine($"Error de autorización: {ex.Message}");
} catch (ValidacionException ex) {
    Console.WriteLine($"Error de validación: {ex.Message}");
} catch (RepositorioException ex) {
    Console.WriteLine($"Error de repositorio: {ex.Message}");
} catch (Exception ex) {
    Console.WriteLine($"Error inesperado: {ex.Message}");
}

try {
    altaCasoDeUso.Ejecutar(expediente4, usuario.Id);
    Console.WriteLine($"Expediente guardado con ID {expediente4.Id}");
} catch (AutorizacionException ex) {
    Console.WriteLine($"Error de autorización: {ex.Message}");
} catch (ValidacionException ex) {
    Console.WriteLine($"Error de validación: {ex.Message}");
} catch (RepositorioException ex) {
    Console.WriteLine($"Error de repositorio: {ex.Message}");
} catch (Exception ex) {
    Console.WriteLine($"Error inesperado: {ex.Message}");
}
```

En el ejemplo anterior, la salida por consola esperada es:

    Expediente guardado con ID 1
    Error de validación: La carátula no puede estar vacía.
    Error de repositorio: No se puede dar de alta un expediente que ya tiene ID.
    Expediente guardado con ID 2

Los datos se persistirán en el archivo `Expedientes.txt`.
Si se intenta guardar un expediente asignando un ID manualmente, se arrojará una excepción del repositorio
que es el encargado de asignar los ID automáticamente antes de persistir los datos.
Un alta de expediente con ID ya establecido, puede significar que se está intentando dar de alta un 
expediente que ya existe. Como no se puede garantizar que el ID sea único, se lanza una excepción.

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
int idExpediente = 0; 
try {
    altaCasoDeUso.Ejecutar(expedienteNuevo, usuario.Id);
    idExpediente = expedienteNuevo.Id;
    Console.WriteLine($"Expediente guardado con ID {idExpediente}");
} catch (AutorizacionException ex) {
    Console.WriteLine($"Error de autorización: {ex.Message}");
} catch (ValidacionException ex) {
    Console.WriteLine($"Error de validación: {ex.Message}");
} catch (RepositorioException ex) {
    Console.WriteLine($"Error de repositorio: {ex.Message}");
} catch (Exception ex) {
    Console.WriteLine($"Error inesperado: {ex.Message}");
}

// Elimino el expediente y trato de eliminar uno inexistente
try {
    bajaCasoDeUso.Ejecutar(idExpediente, usuario.Id);
    Console.WriteLine($"Expediente con ID {idExpediente} eliminado");
    idExpediente = 99999;
    bajaCasoDeUso.Ejecutar(idExpediente, usuario.Id);
    Console.WriteLine($"Expediente con ID {idExpediente} eliminado");
} catch (AutorizacionException ex) {
    Console.WriteLine($"Error de autorización: {ex.Message}");
} catch (ValidacionException ex) {
    Console.WriteLine($"Error de validación: {ex.Message}");
} catch (RepositorioException ex) {
    Console.WriteLine($"Error de repositorio: {ex.Message}");
} catch (Exception ex) {
    Console.WriteLine($"Error inesperado: {ex.Message}");
}
```

En el ejemplo anterior, la salida por consola esperada es:

    Expediente guardado con ID 1
    Expediente con ID 1 eliminado
    Error de repositorio: No se pudo eliminar el expediente con ID 99999. Expediente no encontrado.

Los datos se persistirán en el archivo `Expedientes.txt`.

---
# Buscar un expediente por ID sin sus trámites
Para testear el buscar un expediente por ID sin sus trámites, se debe generar una instancia de la clase `ExpedienteBuscarPorIdCasoDeUso`, la cual recibe el siguiente parámetro en su constructor:

    IExpedienteRepositorio expedienteRepositorio

Donde:

- `expedienteRepositorio` es una instancia de una clase que implementa la interfaz `IExpedienteRepositorio`

Para testear el caso de uso se debe llamar al método `Ejecutar` de la instancia de `ExpedienteBuscarPorIdCasoDeUso`, el cual recibe el siguiente parámetro:

    int idExpediente

Donde:

- `idExpediente` es el id del expediente que se quiere buscar

Y retorna una instancia de la clase `Expediente`.

## Ejemplo de test

```csharp
Usuario                    usuario                    = new();
RepositorioTramiteTxt      tramiteRepositorio         = new();
IExpedienteValidador       expedienteValidador        = new ExpedienteValidador();
IExpedienteRepositorio     expedienteRepositorio      = new RepositorioExpedienteTxt();
IServicioAutorizacion      servicioAutorizacion       = new ServicioAutorizacionProvisorio();
EspecificacionCambioEstado especificacionCambioEstado = new(tramiteRepositorio);

ServicioActualizacionEstado servicioActualizacionEstado
    = new(expedienteRepositorio, especificacionCambioEstado);

ITramiteValidador tramiteValidador = new TramiteValidador();

ExpedienteAltaCasoDeUso expedienteAltaCasoDeUso
    = new(expedienteRepositorio, expedienteValidador, servicioAutorizacion);

TramiteAltaCasoDeUso tramiteAltaCasoDeUso
    = new(tramiteRepositorio, tramiteValidador, servicioAutorizacion, servicioActualizacionEstado);

ExpedienteBuscarPorIdCasoDeUso expedienteBuscarPorIdCasoDeUso = new(expedienteRepositorio);

Expediente expedienteNuevo = new() {
                                       Caratula = "Caratula de prueba del expediente",
                                   };

Tramite tramiteNuevo1 = new() {
                                  Contenido    = "Contenido de prueba del trámite 1",
                                  IdExpediente = 1,
                              };

Tramite tramiteNuevo2 = new() {
                                  Contenido    = "Contenido de prueba del trámite 2",
                                  IdExpediente = 1,
                              };

// Añado el expediente
try {
    expedienteAltaCasoDeUso.Ejecutar(expedienteNuevo, usuario.Id);
    Console.WriteLine($"Expediente guardado con ID {expedienteNuevo.Id}");
} catch (Exception e) {
    Console.WriteLine(e.Message);
}

//Añado los trámites
try {
    tramiteAltaCasoDeUso.Ejecutar(tramiteNuevo1, usuario.Id);
    tramiteAltaCasoDeUso.Ejecutar(tramiteNuevo2, usuario.Id);
} catch (Exception e) {
    Console.WriteLine(e.Message);
}

// Busco el expediente por ID sin sus trámites
try {
    int idExpediente = 1;
    Console.WriteLine("---");
    Console.WriteLine($"Buscando expediente con ID {idExpediente}...");
    Expediente? expedienteBuscado = expedienteBuscarPorIdCasoDeUso.Ejecutar(idExpediente);
    
    if (expedienteBuscado is not null) {
        Console.WriteLine("Expediente encontrado:");
        Console.WriteLine(expedienteBuscado.ToString());
    } else {
        Console.WriteLine("No se encontró el expediente.");
    }

    idExpediente = 2;
    Console.WriteLine("---");
    Console.WriteLine($"Buscando expediente con ID {idExpediente}...");
    expedienteBuscado = expedienteBuscarPorIdCasoDeUso.Ejecutar(idExpediente);
    
    if (expedienteBuscado is not null) {
        Console.WriteLine(expedienteBuscado.ToString());
    } else {
        Console.WriteLine("No se encontró el expediente.");
    }
} catch (RepositorioException ex) {
    Console.WriteLine($"Error de repositorio: {ex.Message}");
} catch (Exception ex) {
    Console.WriteLine($"Error inesperado: {ex.Message}");
}
```

En el ejemplo anterior, la salida por consola esperada es:

    Expediente guardado con ID 1
    ---
    Buscando expediente con ID 1...
    Expediente encontrado:
    Id: 1
    Estado: RecienIniciado
    Caratula: Caratula de prueba del expediente
    FechaCreacion: 12/5/2024 12:33:10
    UltimaModificacion: 12/5/2024 12:33:10
    IdUsuarioUltimaModificacion: 1
    ---
    Buscando expediente con ID 2...
    No se encontró el expediente.


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
ITramiteValidador          tramiteValidador           = new TramiteValidador();
IExpedienteRepositorio     expedienteRepositorio      = new RepositorioExpedienteTxt();
IServicioAutorizacion      servicioAutorizacion       = new ServicioAutorizacionProvisorio();
EspecificacionCambioEstado especificacionCambioEstado = new(tramiteRepositorio);

ServicioActualizacionEstado servicioActualizacionEstado
    = new(expedienteRepositorio, especificacionCambioEstado);

ExpedienteAltaCasoDeUso expedienteAltaCasoDeUso
    = new(expedienteRepositorio, expedienteValidador, servicioAutorizacion);

TramiteAltaCasoDeUso tramiteAltaCasoDeUso
    = new(tramiteRepositorio, tramiteValidador, servicioAutorizacion, servicioActualizacionEstado);

ExpedienteBuscarPorIdConTramitesCasoDeUso expedienteBuscarPorIdConTramitesCasoDeUso
    = new(expedienteRepositorio, tramiteRepositorio);

Expediente expediente = new() { Caratula = "Caratula de prueba del expediente", };

// Añado el expediente
try {
    expedienteAltaCasoDeUso.Ejecutar(expediente, usuario.Id);
    Console.WriteLine($"Expediente guardado con ID {expediente.Id}");
} catch (AutorizacionException ex) {
    Console.WriteLine($"Error de autorización: {ex.Message}");
} catch (ValidacionException ex) {
    Console.WriteLine($"Error de validación: {ex.Message}");
} catch (RepositorioException ex) {
    Console.WriteLine($"Error de repositorio: {ex.Message}");
} catch (Exception ex) {
    Console.WriteLine($"Error inesperado: {ex.Message}");
}

Tramite tramite1 = new() { Contenido = "Contenido de prueba del trámite 1", IdExpediente = expediente.Id, };
Tramite tramite2 = new() { Contenido = "Contenido de prueba del trámite 2", IdExpediente = expediente.Id, };

//Añado los trámites
try {
    tramiteAltaCasoDeUso.Ejecutar(tramite1, usuario.Id);
    Console.WriteLine($"Trámite 1 guardado con ID {tramite1.Id} (ID Expediente: {tramite1.IdExpediente})");
    tramiteAltaCasoDeUso.Ejecutar(tramite2, usuario.Id);
    Console.WriteLine($"Trámite 2 guardado con ID {tramite2.Id} (ID Expediente: {tramite2.IdExpediente})");
} catch (AutorizacionException ex) {
    Console.WriteLine($"Error de autorización: {ex.Message}");
} catch (ValidacionException ex) {
    Console.WriteLine($"Error de validación: {ex.Message}");
} catch (RepositorioException ex) {
    Console.WriteLine($"Error de repositorio: {ex.Message}");
} catch (Exception ex) {
    Console.WriteLine($"Error inesperado: {ex.Message}");
}

// Busco el expediente por ID con sus trámites
try {
    int idExpediente = 1;
    Console.WriteLine("---");
    Console.WriteLine($"Buscando expediente con ID {idExpediente}");
    Expediente? expedienteBuscado = expedienteBuscarPorIdConTramitesCasoDeUso.Ejecutar(idExpediente);

    if (expedienteBuscado is not null) {
        Console.WriteLine("Expediente encontrado:");
        Console.WriteLine(expedienteBuscado.ToString());
    } else {
        Console.WriteLine("No se encontró el expediente");
    }

    idExpediente = 9999;
    Console.WriteLine("---");
    Console.WriteLine($"Buscando expediente con ID {idExpediente}");
    expedienteBuscado = expedienteBuscarPorIdConTramitesCasoDeUso.Ejecutar(idExpediente);

    if (expedienteBuscado is not null) {
        Console.WriteLine("Expediente encontrado:");
        Console.WriteLine(expedienteBuscado.ToString());
    } else {
        Console.WriteLine("No se encontró el expediente");
    }
} catch (RepositorioException ex) {
    Console.WriteLine($"Error de repositorio: {ex.Message}");
} catch (Exception ex) {
    Console.WriteLine($"Error inesperado: {ex.Message}");
}
```

En el ejemplo anterior, la salida por consola esperada es:

    Expediente guardado con ID 1
    Trámite 1 guardado con ID 1 (ID Expediente: 1)
    Trámite 2 guardado con ID 2 (ID Expediente: 1)
    ---
    Buscando expediente con ID 1
    Expediente encontrado:
    Id: 1
    Estado: RecienIniciado
    Caratula: Caratula de prueba del expediente
    FechaCreacion: 12/5/2024 13:23:53
    UltimaModificacion: 12/5/2024 13:23:53
    IdUsuarioUltimaModificacion: 1
    --------------
    Tramites del expediente 1:
    --------------
    Id: 1
    IdExpediente: 1
    Etiqueta: EscritoPresentado
    Contenido: Contenido de prueba del trámite 1
    FechaCreacion: 12/5/2024 13:23:53
    UltimaModificacion: 12/5/2024 13:23:53
    IdUsuarioUltimaModificacion: 1
    --------------
    Id: 2
    IdExpediente: 1
    Etiqueta: EscritoPresentado
    Contenido: Contenido de prueba del trámite 2
    FechaCreacion: 12/5/2024 13:23:53
    UltimaModificacion: 12/5/2024 13:23:53
    IdUsuarioUltimaModificacion: 1
    --------------
    ---
    Buscando expediente con ID 9999
    No se encontró el expediente


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
IExpedienteRepositorio     expedienteRepositorio      = new RepositorioExpedienteTxt();
IExpedienteValidador       expedienteValidador        = new ExpedienteValidador();
ITramiteRepositorio        tramiteRepositorio         = new RepositorioTramiteTxt();
ITramiteValidador          tramiteValidador           = new TramiteValidador();
EspecificacionCambioEstado especificacionCambioEstado = new(tramiteRepositorio);
IServicioAutorizacion      servicioAutorizacion       = new ServicioAutorizacionProvisorio();

ServicioActualizacionEstado servicioActualizacionEstado
    = new(expedienteRepositorio, especificacionCambioEstado);

ExpedienteAltaCasoDeUso expedienteAltaCasoDeUso
    = new(expedienteRepositorio, expedienteValidador, servicioAutorizacion);

ExpedienteListarCasoDeUso expedienteListarCasoDeUso = new(expedienteRepositorio);

TramiteAltaCasoDeUso tramiteAltaCasoDeUso = new(tramiteRepositorio, tramiteValidador, servicioAutorizacion, servicioActualizacionEstado);

Expediente expediente1 = new() { Caratula = "Caratula de prueba del expediente1", };
Expediente expediente2 = new() { Caratula = "Caratula de prueba del expediente2", };

// Añado los expedientes
try {
    expedienteAltaCasoDeUso.Ejecutar(expediente1, usuario.Id);
    Console.WriteLine($"Expediente guardado con ID {expediente1.Id}");
    expedienteAltaCasoDeUso.Ejecutar(expediente2, usuario.Id);
    Console.WriteLine($"Expediente guardado con ID {expediente2.Id}");
} catch (AutorizacionException ex) {
    Console.WriteLine($"Error de autorización: {ex.Message}");
} catch (ValidacionException ex) {
    Console.WriteLine($"Error de validación: {ex.Message}");
} catch (RepositorioException ex) {
    Console.WriteLine($"Error de repositorio: {ex.Message}");
} catch (Exception ex) {
    Console.WriteLine($"Error inesperado: {ex.Message}");
}

Tramite tramite1 = new() { Contenido = "Contenido del trámite 1", IdExpediente = expediente1.Id, };

try {
    tramiteAltaCasoDeUso.Ejecutar(tramite1, usuario.Id);
    Console.WriteLine($"Trámite guardado con ID {expediente1.Id} (ID Expediente: {expediente1.Id})");
} catch (AutorizacionException ex) {
    Console.WriteLine($"Error de autorización: {ex.Message}");
} catch (ValidacionException ex) {
    Console.WriteLine($"Error de validación: {ex.Message}");
} catch (RepositorioException ex) {
    Console.WriteLine($"Error de repositorio: {ex.Message}");
} catch (Exception ex) {
    Console.WriteLine($"Error inesperado: {ex.Message}");
}

// Listo los expedientes
try {
    Console.WriteLine();
    Console.WriteLine("Buscando expedientes...");
    List<Expediente> expedientes = expedienteListarCasoDeUso.Ejecutar();

    Console.WriteLine("Expedientes encontrados:");
    foreach (Expediente expediente in expedientes) {
        Console.WriteLine();
        Console.WriteLine(expediente.ToString());
    }
} catch (RepositorioException ex) {
    Console.WriteLine($"Error de repositorio: {ex.Message}");
} catch (Exception ex) {
    Console.WriteLine($"Error inesperado: {ex.Message}");
}
```

En el ejemplo anterior, la salida por consola esperada es:

    Expediente guardado con ID 1
    Expediente guardado con ID 2
    Trámite guardado con ID 1 (ID Expediente: 1)
    
    Buscando expedientes...
    Expedientes encontrados:
    
    Id: 1
    Estado: RecienIniciado
    Caratula: Caratula de prueba del expediente1
    FechaCreacion: 12/5/2024 13:34:23
    UltimaModificacion: 12/5/2024 13:34:23
    IdUsuarioUltimaModificacion: 1
    
    Id: 2
    Estado: RecienIniciado
    Caratula: Caratula de prueba del expediente2
    FechaCreacion: 12/5/2024 13:34:23
    UltimaModificacion: 12/5/2024 13:34:23
    IdUsuarioUltimaModificacion: 1

En ambos casos anteriores, los datos se guardarán en el archivo `Expedientes.txt` y en `Tramites.txt`

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

```csharp
Usuario                    usuario                    = new();
IExpedienteRepositorio     expedienteRepositorio      = new RepositorioExpedienteTxt();
IExpedienteValidador       expedienteValidador        = new ExpedienteValidador();
ITramiteRepositorio        tramiteRepositorio         = new RepositorioTramiteTxt();
ITramiteValidador          tramiteValidador           = new TramiteValidador();
EspecificacionCambioEstado especificacionCambioEstado = new(tramiteRepositorio);
IServicioAutorizacion      servicioAutorizacion       = new ServicioAutorizacionProvisorio();

ServicioActualizacionEstado servicioActualizacionEstado
    = new(expedienteRepositorio, especificacionCambioEstado);

ExpedienteAltaCasoDeUso expedienteAltaCasoDeUso
    = new(expedienteRepositorio, expedienteValidador, servicioAutorizacion);

ExpedienteListarConTramitesCasoDeUso expedienteListarConTramitesCasoDeUso = new(expedienteRepositorio, tramiteRepositorio);

TramiteAltaCasoDeUso tramiteAltaCasoDeUso = new(tramiteRepositorio, tramiteValidador, servicioAutorizacion, servicioActualizacionEstado);

Expediente expediente1 = new() { Caratula = "Caratula de prueba del expediente1", };
Expediente expediente2 = new() { Caratula = "Caratula de prueba del expediente2", };

// Añado los expedientes
try {
    expedienteAltaCasoDeUso.Ejecutar(expediente1, usuario.Id);
    Console.WriteLine($"Expediente guardado con ID {expediente1.Id}");
    expedienteAltaCasoDeUso.Ejecutar(expediente2, usuario.Id);
    Console.WriteLine($"Expediente guardado con ID {expediente2.Id}");
} catch (AutorizacionException ex) {
    Console.WriteLine($"Error de autorización: {ex.Message}");
} catch (ValidacionException ex) {
    Console.WriteLine($"Error de validación: {ex.Message}");
} catch (RepositorioException ex) {
    Console.WriteLine($"Error de repositorio: {ex.Message}");
} catch (Exception ex) {
    Console.WriteLine($"Error inesperado: {ex.Message}");
}

Tramite tramite1 = new() { Contenido = "Contenido del trámite 1", IdExpediente = expediente1.Id, };

try {
    tramiteAltaCasoDeUso.Ejecutar(tramite1, usuario.Id);
    Console.WriteLine($"Trámite guardado con ID {expediente1.Id} (ID Expediente: {expediente1.Id})");
} catch (AutorizacionException ex) {
    Console.WriteLine($"Error de autorización: {ex.Message}");
} catch (ValidacionException ex) {
    Console.WriteLine($"Error de validación: {ex.Message}");
} catch (RepositorioException ex) {
    Console.WriteLine($"Error de repositorio: {ex.Message}");
} catch (Exception ex) {
    Console.WriteLine($"Error inesperado: {ex.Message}");
}

// Listo los expedientes
try {
    Console.WriteLine();
    Console.WriteLine("Buscando expedientes...");
    List<Expediente> expedientes = expedienteListarConTramitesCasoDeUso.Ejecutar();

    Console.WriteLine("Expedientes encontrados:");
    foreach (Expediente expediente in expedientes) {
        Console.WriteLine();
        Console.WriteLine(expediente.ToString());
    }
} catch (RepositorioException ex) {
    Console.WriteLine($"Error de repositorio: {ex.Message}");
} catch (Exception ex) {
    Console.WriteLine($"Error inesperado: {ex.Message}");
}
```

En el ejemplo anterior, la salida esperada por consola es:

    Expediente guardado con ID 1
    Expediente guardado con ID 2
    Trámite guardado con ID 1 (ID Expediente: 1)
    
    Buscando expedientes...
    Expedientes encontrados:
    
    Id: 1
    Estado: RecienIniciado
    Caratula: Caratula de prueba del expediente1
    FechaCreacion: 12/5/2024 13:37:48
    UltimaModificacion: 12/5/2024 13:37:48
    IdUsuarioUltimaModificacion: 1
    --------------
    Tramites del expediente 1:
    --------------
    Id: 1
    IdExpediente: 1
    Etiqueta: EscritoPresentado
    Contenido: Contenido del trámite 1
    FechaCreacion: 12/5/2024 13:37:48
    UltimaModificacion: 12/5/2024 13:37:48
    IdUsuarioUltimaModificacion: 1
    --------------
    
    Id: 2
    Estado: RecienIniciado
    Caratula: Caratula de prueba del expediente2
    FechaCreacion: 12/5/2024 13:37:48
    UltimaModificacion: 12/5/2024 13:37:48
    IdUsuarioUltimaModificacion: 1


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