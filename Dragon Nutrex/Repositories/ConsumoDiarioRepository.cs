using Dragon_Nutrex.Models;
using Dragon_Nutrex.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dragon_Nutrex.Repositories
{
    public class ConsumoDiarioRepository
    {
        private readonly string _filePath =
            Path.Combine(
                "Data",
                "consumo_diario.json");

        public List<ConsumoDiario>
            ObtenerPorFecha(DateTime fecha)
        {
            var registros =
                FileStorage
                .Load<ConsumoDiario>(
                    _filePath);

            return registros
                .Where(r =>
                    r.Activo &&
                    r.Fecha.Date ==
                    fecha.Date)
                .ToList();
        }

        public List<DateTime>
        ObtenerFechasConRegistros()
        {
            var registros =
                FileStorage
                .Load<ConsumoDiario>(
                    _filePath);

            return registros
                .Where(r => r.Activo)
                .Select(r => r.Fecha.Date)
                .Distinct()
                .OrderBy(f => f)
                .ToList();
        }

        public void CrearRegistroPrueba()
        {
            var registros =
                FileStorage
                .Load<ConsumoDiario>(
                    _filePath);

            registros.Add(
                new ConsumoDiario
                {
                    Fecha =
                        DateTime.Today,

                    CaloriasConsumidas =
                        2100,

                    CarbohidratosConsumidos =
                        250,

                    Activo =
                        true
                });

            FileStorage
                .Save(
                    _filePath,
                    registros);
        }

        public List<ConsumoDiario>
    ObtenerPorRango(
        DateTime fechaInicio,
        DateTime fechaFin)
        {
            var registros =
                FileStorage
                .Load<ConsumoDiario>(
                    _filePath);

            return registros
                .Where(r =>
                    r.Activo &&
                    r.Fecha.Date >=
                        fechaInicio.Date &&
                    r.Fecha.Date <=
                        fechaFin.Date)
                .ToList();
        }
    }
}
