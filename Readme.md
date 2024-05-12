# Sistema de Gestión de Expedientes SGE
En este proyecto se realizó un sistema de gestión de expedientes, el cual permite dar de alta, dar de baja, buscar y listar expedientes.
Cada expediente puede contener 0, 1 o más trámites que estén vinculados a él.

El sistema se encuentra dividido en 3 capas:
- **Capa de Aplicación**: Contiene las clases con la lógica de negocio y los casos de uso.
- **Capa de Repositorio**: Contiene las clases que interactúan con la base de datos.
- **Capa de Interfaz de Usuario**: Contiene las clases que interactúan con el usuario.

## Capa de Aplicación
La capa de aplicación contiene las clases con la lógica de negocio y los casos de uso.
Esta capa está completamente aislada de la capa de repositorio y de la capa de interfaz de usuario.
Las otras capas interactúan con ella, pero ella no interactúa con las otras capas, simplemente procesa datos
y devuelve resultados. Siguiendo el patrón de inyección de dependencias, las clases de esta capa reciben
como parámetros en sus constructores instancias de las clases de la capa de repositorio y de la capa de interfaz de usuario.
Las clases no dependen de implementaciones concretas, sino de interfaces, lo que permite que se puedan cambiar las implementaciones
de las clases de la capa de repositorio sin tener que modificar las clases de la capa de aplicación.
La capa aplicación está separada en diferentes carpetas, cada una con un propósito específico:
- **Casos de Uso**: Contiene los casos de uso de la aplicación con los que interactúa la capa de interfaz de usuario. Devuelven resultados y lanzan excepciones en caso de errores. Entre ellos se encuentran los casos de uso de dar de alta, dar de baja, modificar, buscar y listar expedientes con y sin trámites, y los casos de uso de dar de alta, baja, modificar y listar trámites.
- **Entidades**: Contiene las clases que representan las entidades de la aplicación. En este caso, las entidades son `Expediente`, `Tramite` y `Usuario`.
- **Enumerativos**: Contiene los enumerativos de la aplicación. En este caso, los enumerativos son `EtiquetaTramite`, que representa las distintas etiquetas que puede tener un trámite, `EstadoExpediente`, que representa los distintos estados que puede tener un expediente y `Permiso`, que representa los distintos permisos que puede tener un usuario.
- **Excepciones**: Contiene las excepciones personalizadas de la aplicación. En este caso, las excepciones son `AutorizacionException`, `RepositorioException` y `ValidacionException`.
- **Interfaces**: Contiene las interfaces de la aplicación. Las interfaces están divididas en distitos tipos. Las interfaces de repositorio son `IExpedienteRepositorio` e `ITramiteRepositorio`, la interfaz de servicio es `IServicioAutorizacion`, y las interfaces de validadores son `IExpedienteValidador` e `ITramiteValidador`.
- **Servicios**: Contiene las clases que implementan la lógica de negocio de la aplicación y/o automatización de tareas. En este caso, el servicio `EspeficicacionCambioEstado` se encarga de definir el estado de un expediente en función de los trámites que tenga asociados luego de que alguno sea agregado, eliminado o editado. El servicio `ServicioActualizacionEstado` se encarga de verificar si un expediente debe cambiar de estado y de actualizarlo en caso de ser necesario. Por último, el servicio `ServicioAutorizacionProvisorio` se encarga de autorizar o no la ejecución de un caso de uso, dependiendo del id del usuario que lo solicita y los permisos que tenga.
- **Validadores**: Contiene las clases que validan los datos de las entidades de la aplicación. Comprueban que los datos sean correctos antes de ser guardados en la base de datos, y en caso de errores, lanzan excepciones. En este caso, los validadores son `ExpedienteValidador` y `TramiteValidador`, que se encargan de comprobar que un Expediente tenga una carátula no vacía y que un Trámite tenga contenido no vacío, respectivamente.

Cada carpeta contiene las clases que cumplen con el propósito de la misma, para mantener la organización y la cohesión de la capa de aplicación.

La capa de aplicación desconoce por completo cómo se almacenan los datos y cómo se presentan al usuario, simplemente procesa datos y devuelve resultados.
No tiene ningún tipo de dependencia con las otras capas, lo que permite que sea fácilmente testeable y reutilizable.
Esta capa puede funcionar con cualquier tipo de base de datos y con cualquier tipo de interfaz de usuario, el único requisito es que las clases de la capa de repositorio
y de la capa de interfaz de usuario implementen las interfaces que las clases de la capa de aplicación esperan.

## Capa de Repositorio
La capa de repositorio contiene las clases que interactúan con la base de datos.
En esta versión, la base de datos es un archivo de texto, pero se puede cambiar fácilmente a una base de datos relacional o no relacional con 
sólo cambiar las implementaciones de las clases de la capa de repositorio.
Para una mayor consistencia en esta versión, las clases del repositorio extienden a una clase base.
Dicha clase base, se encarga de inicializar el archivo de texto si no existe para cada una de las clases que hereden de ella.
En caso de inconsistencia de datos post-validación, o problemas con el archivo, se lanzan excepciones.
En esta versión del sistema, se utilizan dos archivos de texto, uno para los expedientes y otro para los trámites, de los cuales
cada uno es manejado por una clase de repositorio distinta, `RepositorioExpedienteTxt` y `RepositorioTramiteTxt` respectivamente.
Los repositorios implementan las interfaces `IExpedienteRepositorio` e `ITramiteRepositorio`, las cuales contienen los métodos que las clases de la capa de aplicación esperan.

## Capa de Interfaz de Usuario

En esta versión, la capa de interfaz de usuario es simplemente una consola. La misma se utiliza de manera estática
escribiendo código de los ejemplos de aquí abajo dentro de el método `Main`. La consola deberá mostrar
los resultados esperados de cada uno de los casos de uso.

---


### Todos los ejemplos asumen que los archivos `Expedientes.txt` y `Tramites.txt` no existen, o están vacíos.
### Las salidas de consola esperadas pueden variar si los archivos ya contienen datos, y al momento de imprimir expedientes o trámites, las fechas de creación y/o modificación pueden variar con respecto al resultado esperado que se detalla en cada caso de uso. Se recomienda borrar los archivos antes de ejecutar cada uno de los ejemplos para obtener las salidas esperadas lo más parecidas posibles.
### Los ejemplos no incluyen la importación de las clases, por lo que se debe importar cada clase que se utilice en los ejemplos. Tanto Visual Studio Code, como Visual Studio y Rider permiten importar clases automáticamente con un atajo de teclado. Este dato se omitió en los ejemplos para acortar la longitud de los mismos ya que el IDE se puede encargar automáticamente de eso. De todas maneras, el Program.cs de la consola ya tiene importadas todas las clases necesarias para ejecutar los ejemplos, para facilitar su ejecución.

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

Expediente expediente1 = new(){ Caratula = "Carátula de prueba del expediente" };

Expediente expediente2 = new() { Caratula = "" };

Expediente expediente3 = new() { Id = 999, Caratula = "Carátula de prueba del expediente", };

Expediente expediente4 = new() { Caratula = "Otra carátula de prueba" };

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

Expediente expedienteNuevo = new() { Caratula = "Caratula de prueba del expediente" };

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

Expediente expediente = new() { Caratula = "Caratula de prueba del expediente" };

try {
    expedienteAltaCasoDeUso.Ejecutar(expediente, usuario.Id);
    Console.WriteLine($"Expediente guardado con ID {expediente.Id}");
} catch (Exception e) {
    Console.WriteLine(e.Message);
}

Tramite tramite1 = new() { Contenido    = "Contenido de prueba del trámite 1", IdExpediente = expediente.Id };
Tramite tramite2 = new() { Contenido    = "Contenido de prueba del trámite 2", IdExpediente = expediente.Id };

try {
    tramiteAltaCasoDeUso.Ejecutar(tramite1, usuario.Id);
    tramiteAltaCasoDeUso.Ejecutar(tramite2, usuario.Id);
} catch (Exception e) {
    Console.WriteLine(e.Message);
}

try {
    int idExpediente = expediente.Id;
    Console.WriteLine("---");
    Console.WriteLine($"Buscando expediente con ID {idExpediente}...");
    Expediente? expedienteBuscado = expedienteBuscarPorIdCasoDeUso.Ejecutar(idExpediente);
    
    if (expedienteBuscado is not null) {
        Console.WriteLine("Expediente encontrado:");
        Console.WriteLine(expedienteBuscado.ToString());
    } else {
        Console.WriteLine("No se encontró el expediente.");
    }

    idExpediente = 999;
    Console.WriteLine("---");
    Console.WriteLine($"Buscando expediente con ID {idExpediente}...");
    expedienteBuscado = expedienteBuscarPorIdCasoDeUso.Ejecutar(idExpediente);
    
    if (expedienteBuscado is not null) {
        Console.WriteLine("Expediente encontrado:");
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

try {
    int idExpediente = expediente.Id;
    Console.WriteLine("---");
    Console.WriteLine($"Buscando expediente con ID {idExpediente}");
    Expediente? expedienteBuscado = expedienteBuscarPorIdConTramitesCasoDeUso.Ejecutar(idExpediente);

    if (expedienteBuscado is not null) {
        Console.WriteLine("Expediente encontrado:");
        Console.WriteLine(expedienteBuscado.ToString());
    } else {
        Console.WriteLine("No se encontró el expediente");
    }

    idExpediente = 999;
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
    Buscando expediente con ID 999
    No se encontró el expediente


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

```csharp
Usuario                usuario               = new();
IExpedienteValidador   expedienteValidador   = new ExpedienteValidador();
IExpedienteRepositorio expedienteRepositorio = new RepositorioExpedienteTxt();
IServicioAutorizacion  servicioAutorizacion  = new ServicioAutorizacionProvisorio();

ExpedienteAltaCasoDeUso expedienteAltaCasoDeUso
    = new(expedienteRepositorio, expedienteValidador, servicioAutorizacion);

ExpedienteModificarCasoDeUso expedienteModificarCasoDeUso
    = new(expedienteRepositorio, expedienteValidador, servicioAutorizacion);

ExpedienteBuscarPorIdCasoDeUso expedienteBuscarPorIdCasoDeUso
    = new(expedienteRepositorio);

Expediente expediente = new() { Caratula = "Caratula de prueba del expediente", };

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

expediente.Caratula = "Caratula modificada del expediente";

try {
    expedienteModificarCasoDeUso.Ejecutar(expediente, usuario.Id);
    Console.WriteLine($"Expediente modificado con ID {expediente.Id}");
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
    Console.WriteLine($"Buscando expediente con ID {expediente.Id}...");
    Expediente? expedienteBuscado = expedienteBuscarPorIdCasoDeUso.Ejecutar(expediente.Id);

    if (expedienteBuscado is not null) {
        Console.WriteLine("Expediente encontrado:");
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
    Expediente modificado con ID 1
    Buscando expediente con ID 1...
    Expediente encontrado:
    Id: 1
    Estado: RecienIniciado
    Caratula: Caratula modificada del expediente
    FechaCreacion: 12/5/2024 14:09:01
    UltimaModificacion: 12/5/2024 14:09:01
    IdUsuarioUltimaModificacion: 1

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