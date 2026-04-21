using Dragon_Nutrex_Web.Core.Enums;
using Dragon_Nutrex_Web.Core.Interfaces;
using Dragon_Nutrex_Web.Core.Models;
using Dragon_Nutrex_Web.Core.Services;
using Moq;
using Xunit;

namespace Dragon_Nutrex.Tests;

public class UsuarioServiceTests
{
    private readonly Mock<IRepository<Usuario>> usuarioRepositoryMock;
    private readonly UsuarioService usuarioService;

    public UsuarioServiceTests()
    {
        usuarioRepositoryMock = new Mock<IRepository<Usuario>>();
        usuarioService = new UsuarioService(usuarioRepositoryMock.Object);
    }

    [Theory]
    [InlineData(2)]
    public void ObtenerTodos_CuandoSeInvoca_RetornaListaDeUsuarios(int cantidadEsperada)
    {
        var usuarios = new List<Usuario>
        {
            CrearUsuarioValido(),
            CrearUsuarioValido()
        };

        usuarioRepositoryMock
            .Setup(repository => repository.GetAll())
            .Returns(usuarios);

        var resultado = usuarioService.ObtenerTodos();

        Assert.Equal(cantidadEsperada, resultado.Count);
        usuarioRepositoryMock.Verify(repository => repository.GetAll(), Times.Once);
    }

    [Theory]
    [InlineData(true)]
    public void ObtenerPorId_CuandoUsuarioExiste_RetornaUsuario(bool _)
    {
        var usuario = CrearUsuarioValido();

        usuarioRepositoryMock
            .Setup(repository => repository.GetById(usuario.Id))
            .Returns(usuario);

        var resultado = usuarioService.ObtenerPorId(usuario.Id);

        Assert.NotNull(resultado);
        Assert.Equal(usuario.Id, resultado!.Id);
    }

    [Theory]
    [InlineData(true)]
    public void CrearUsuario_CuandoEsValido_YNoTieneId_GeneraIdYLoCrea(bool _)
    {
        var usuario = CrearUsuarioValido();
        usuario.Id = Guid.Empty;

        usuarioService.CrearUsuario(usuario);

        Assert.NotEqual(Guid.Empty, usuario.Id);
        usuarioRepositoryMock.Verify(repository => repository.Create(usuario), Times.Once);
    }

    [Theory]
    [InlineData("", "usuario@test.com", "1234", 70, 1.75, 25, "El nombre del usuario es obligatorio.")]
    [InlineData("Usuario Test", "", "1234", 70, 1.75, 25, "El correo del usuario es obligatorio.")]
    [InlineData("Usuario Test", "usuario@test.com", "", 70, 1.75, 25, "La contraseña del usuario es obligatoria.")]
    public void CrearUsuario_CuandoTextoEsInvalido_LanzaExcepcion(
        string nombre,
        string correo,
        string contrasena,
        decimal peso,
        decimal altura,
        int edad,
        string mensajeEsperado)
    {
        var usuario = CrearUsuarioValido();
        usuario.Nombre = nombre;
        usuario.Correo = correo;
        usuario.Contrasena = contrasena;
        usuario.Peso = peso;
        usuario.Altura = altura;
        usuario.Edad = edad;

        var ex = Assert.Throws<Exception>(() => usuarioService.CrearUsuario(usuario));

        Assert.Equal(mensajeEsperado, ex.Message);
        usuarioRepositoryMock.Verify(repository => repository.Create(It.IsAny<Usuario>()), Times.Never);
    }

    [Theory]
    [InlineData(0, 1.75, 25, "El peso debe ser mayor a 0.")]
    [InlineData(70, 0, 25, "La altura debe ser mayor a 0.")]
    [InlineData(70, 1.75, 0, "La edad debe ser válida.")]
    public void CrearUsuario_CuandoValoresSonInvalidos_LanzaExcepcion(
        decimal peso,
        decimal altura,
        int edad,
        string mensajeEsperado)
    {
        var usuario = CrearUsuarioValido();
        usuario.Peso = peso;
        usuario.Altura = altura;
        usuario.Edad = edad;

        var ex = Assert.Throws<Exception>(() => usuarioService.CrearUsuario(usuario));

        Assert.Equal(mensajeEsperado, ex.Message);
        usuarioRepositoryMock.Verify(repository => repository.Create(It.IsAny<Usuario>()), Times.Never);
    }

    [Theory]
    [InlineData(true)]
    public void ActualizarUsuario_CuandoEsValido_InvocaUpdate(bool _)
    {
        var usuario = CrearUsuarioValido();

        usuarioService.ActualizarUsuario(usuario);

        usuarioRepositoryMock.Verify(repository => repository.Update(usuario), Times.Once);
    }

    [Theory]
    [InlineData(true)]
    public void EliminarUsuario_CuandoSeInvoca_EliminaPorId(bool _)
    {
        var usuarioId = Guid.NewGuid();

        usuarioService.EliminarUsuario(usuarioId);

        usuarioRepositoryMock.Verify(repository => repository.Delete(usuarioId), Times.Once);
    }

    [Theory]
    [InlineData(true)]
    public void ResetearContrasena_CuandoUsuarioExiste_ActualizaContrasenaTemporal(bool _)
    {
        var usuario = CrearUsuarioValido();

        usuarioRepositoryMock
            .Setup(repository => repository.GetById(usuario.Id))
            .Returns(usuario);

        usuarioService.ResetearContrasena(usuario.Id);

        Assert.Equal("1234", usuario.Contrasena);
        usuarioRepositoryMock.Verify(repository => repository.Update(usuario), Times.Once);
    }

    [Theory]
    [InlineData(true)]
    public void ResetearContrasena_CuandoUsuarioNoExiste_LanzaExcepcion(bool _)
    {
        var usuarioId = Guid.NewGuid();

        usuarioRepositoryMock
            .Setup(repository => repository.GetById(usuarioId))
            .Returns((Usuario?)null);

        var ex = Assert.Throws<Exception>(() => usuarioService.ResetearContrasena(usuarioId));

        Assert.Equal("Usuario no encontrado", ex.Message);
    }

    [Theory]
    [InlineData(true)]
    public void DesactivarUsuario_CuandoUsuarioExiste_CambiaActivoAFalso(bool _)
    {
        var usuario = CrearUsuarioValido();
        usuario.Activo = true;

        usuarioRepositoryMock
            .Setup(repository => repository.GetById(usuario.Id))
            .Returns(usuario);

        usuarioService.DesactivarUsuario(usuario.Id);

        Assert.False(usuario.Activo);
        usuarioRepositoryMock.Verify(repository => repository.Update(usuario), Times.Once);
    }

    [Theory]
    [InlineData(true)]
    public void ActivarUsuario_CuandoUsuarioExiste_CambiaActivoAVerdadero(bool _)
    {
        var usuario = CrearUsuarioValido();
        usuario.Activo = false;

        usuarioRepositoryMock
            .Setup(repository => repository.GetById(usuario.Id))
            .Returns(usuario);

        usuarioService.ActivarUsuario(usuario.Id);

        Assert.True(usuario.Activo);
        usuarioRepositoryMock.Verify(repository => repository.Update(usuario), Times.Once);
    }

    private static Usuario CrearUsuarioValido()
    {
        return new Usuario
        {
            Id = Guid.NewGuid(),
            Nombre = "Usuario Test",
            Correo = "usuario@test.com",
            Contrasena = "1234",
            Rol = "Usuario",
            Peso = 70m,
            Altura = 1.75m,
            Edad = 25,
            NivelActividad = NivelActividad.Moderado,
            Objetivo = ObjetivoNutricional.MantenerPeso,
            TipoDieta = TipoDieta.Balanceada,
            Activo = true
        };
    }
}