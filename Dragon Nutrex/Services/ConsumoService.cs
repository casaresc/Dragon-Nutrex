using Dragon_Nutrex.Models;
using Dragon_Nutrex.Repositories;
using System;
using System.Linq;

public class ConsumoService
{
    private ConsumoDiarioRepository _repository;

    public ConsumoService()
    {
        _repository =
            new ConsumoDiarioRepository();
    }

    public ResumenDiario ObtenerResumenDiario(
        DateTime fecha,
        decimal metaCalorias)
    {
        var registros =
            _repository
            .ObtenerPorFecha(fecha);

        if (registros == null
            || registros.Count == 0)
        {
            return new ResumenDiario
            {
                CaloriasConsumidas = 0,
                CarbohidratosConsumidos = 0,
                DiferenciaCalorias = metaCalorias
            };
        }

        var calorias =
            registros.Sum(r =>
                r.CaloriasConsumidas);

        var carbohidratos =
            registros.Sum(r =>
                r.CarbohidratosConsumidos);

        var diferencia =
            metaCalorias - calorias;

        var tieneRegistros =
            registros.Count > 0;

        return new ResumenDiario
        {
            CaloriasConsumidas =
                calorias,

            CarbohidratosConsumidos =
                carbohidratos,

            DiferenciaCalorias =
                diferencia,

            TieneRegistros = tieneRegistros
        };
    }

    public ResumenRango
    ObtenerResumenPorRango(
        DateTime fechaInicio,
        DateTime fechaFin)
    {
        if (fechaInicio > fechaFin)
            throw new ArgumentException(
                "La fecha inicio no puede ser mayor que la fecha fin");

        var registros =
            _repository
            .ObtenerPorRango(
                fechaInicio,
                fechaFin);

        if (registros == null
            || registros.Count == 0)
        {
            return new ResumenRango();
        }

        var totalCalorias =
            registros.Sum(r =>
                r.CaloriasConsumidas);

        var totalCarbohidratos =
            registros.Sum(r =>
                r.CarbohidratosConsumidos);

        var dias =
            registros
            .Select(r =>
                r.Fecha.Date)
            .Distinct()
            .Count();

        var promedioCalorias =
            totalCalorias / dias;

        var promedioCarbohidratos =
            totalCarbohidratos / dias;

        return new ResumenRango
        {
            TotalCalorias =
                totalCalorias,

            TotalCarbohidratos =
                totalCarbohidratos,

            PromedioCalorias =
                promedioCalorias,

            PromedioCarbohidratos =
                promedioCarbohidratos,

            DiasConRegistros =
                dias
        };
    }
}