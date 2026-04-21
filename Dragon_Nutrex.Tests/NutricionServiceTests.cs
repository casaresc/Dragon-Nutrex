using Dragon_Nutrex_Web.Core.Enums;
using Dragon_Nutrex_Web.Core.Models;
using Dragon_Nutrex_Web.Core.Services;
using Xunit;

namespace Dragon_Nutrex.Tests;

public class NutricionServiceTests
{
    private readonly NutricionService nutricionService = new();

    [Theory]
    [InlineData(70, 1.75, 25, 2594)]
    public void CalcularCaloriasObjetivo_CuandoDatosSonValidos_RetornaValorEsperado(
        decimal peso,
        decimal altura,
        int edad,
        decimal resultadoEsperado)
    {
        var resultado = NutricionService.CalcularCaloriasObjetivo(
            peso,
            altura,
            edad,
            NivelActividad.Moderado,
            ObjetivoNutricional.MantenerPeso);

        Assert.Equal(resultadoEsperado, resultado);
    }

    [Theory]
    [InlineData(0, 1.75, 25)]
    [InlineData(-70, 1.75, 25)]
    public void CalcularCaloriasObjetivo_CuandoPesoEsInvalido_LanzaExcepcion(
        decimal peso,
        decimal altura,
        int edad)
    {
        Assert.Throws<ArgumentException>(() =>
            NutricionService.CalcularCaloriasObjetivo(
                peso,
                altura,
                edad,
                NivelActividad.Moderado,
                ObjetivoNutricional.MantenerPeso));
    }

    [Theory]
    [InlineData(70, 0, 25)]
    [InlineData(70, -1.75, 25)]
    public void CalcularCaloriasObjetivo_CuandoAlturaEsInvalida_LanzaExcepcion(
        decimal peso,
        decimal altura,
        int edad)
    {
        Assert.Throws<ArgumentException>(() =>
            NutricionService.CalcularCaloriasObjetivo(
                peso,
                altura,
                edad,
                NivelActividad.Moderado,
                ObjetivoNutricional.MantenerPeso));
    }

    [Theory]
    [InlineData(70, 1.75, 0)]
    [InlineData(70, 1.75, -5)]
    public void CalcularCaloriasObjetivo_CuandoEdadEsInvalida_LanzaExcepcion(
        decimal peso,
        decimal altura,
        int edad)
    {
        Assert.Throws<ArgumentException>(() =>
            NutricionService.CalcularCaloriasObjetivo(
                peso,
                altura,
                edad,
                NivelActividad.Moderado,
                ObjetivoNutricional.MantenerPeso));
    }

    [Theory]
    [InlineData(2000, TipoDieta.Balanceada, 250, 100, 67)]
    [InlineData(2000, TipoDieta.Cetogenica, 25, 125, 156)]
    [InlineData(2000, TipoDieta.BajaEnCarbohidratos, 150, 200, 67)]
    [InlineData(2000, TipoDieta.AltaEnCarbohidratos, 300, 100, 44)]
    public void CalcularDistribucionMacros_CuandoDietaEsValida_RetornaDistribucionCorrecta(
        decimal calorias,
        TipoDieta dieta,
        decimal carbohidratosEsperados,
        decimal proteinasEsperadas,
        decimal grasasEsperadas)
    {
        var resultado = nutricionService.CalcularDistribucionMacros(calorias, dieta);

        Assert.Equal(calorias, resultado.CaloriasObjetivo);
        Assert.Equal(carbohidratosEsperados, resultado.CarbohidratosGramos);
        Assert.Equal(proteinasEsperadas, resultado.ProteinasGramos);
        Assert.Equal(grasasEsperadas, resultado.GrasasGramos);
    }

    [Theory]
    [InlineData(70, 1.75, 25)]
    public void CalcularRequerimientos_CuandoUsuarioEsValido_RetornaResultado(
        decimal peso,
        decimal altura,
        int edad)
    {
        var usuario = new Usuario
        {
            Id = Guid.NewGuid(),
            Nombre = "Test",
            Correo = "test@test.com",
            Contrasena = "1234",
            Rol = "Usuario",
            Activo = true,
            Peso = peso,
            Altura = altura,
            Edad = edad,
            NivelActividad = NivelActividad.Moderado,
            Objetivo = ObjetivoNutricional.MantenerPeso,
            TipoDieta = TipoDieta.Balanceada
        };

        var resultado = nutricionService.CalcularRequerimientos(usuario);

        Assert.NotNull(resultado);
        Assert.True(resultado.CaloriasObjetivo > 0);
    }

    [Theory]
    [InlineData(true)]
    public void CalcularRequerimientos_CuandoUsuarioEsNull_LanzaExcepcion(bool _)
    {
        Assert.Throws<ArgumentNullException>(() => nutricionService.CalcularRequerimientos(null!));
    }
}