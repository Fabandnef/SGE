# Dar de alta un expediente

Para testear el alta de un expediente, se debe generar una instancia de la clase
`ExpedienteAltaCasoDeUso`, la cual recibe 3 parámetros en su constructor:

    IExpedienteRepositorio expedienteRepositorio,
    IExpedienteValidador   expedienteValidador,
    IServicioAutorizacion  servicioAutorizacion

Donde:

- `expedienteRepositorio` es una instancia de la interfaz `IExpedienteRepositorio`
- `expedienteValidador` es una instancia de la interfaz `IExpedienteValidador`
- `servicioAutorizacion` es una instancia de la interfaz `IServicioAutorizacion`

Para testear el caso de uso, se debe llamar al método `Ejecutar` de la instancia
de `ExpedienteAltaCasoDeUso`, el cual recibe los siguientes 2 parámetros:

    Expediente expediente, 
    int idUsuario

Donde:

- `expediente` es una instancia de la clase `Expediente`
- `idUsuario` es el id del usuario que está dando de alta el expediente

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

En ambos casos anteriores, los datos se guardarán en el archivo `Expedientes.txt`\
Si se intenta guardar un expediente sin caratula, la salida esperada es:

    La carátula no puede estar vacía

En este caso, no se guardará ningún expediente en el archivo `Expedientes.txt` ya que no cumple con la validación
requerida.