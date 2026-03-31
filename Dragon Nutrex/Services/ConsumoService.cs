using Dragon_Nutrex.Models;
using Dragon_Nutrex.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Dragon_Nutrex.Services
{
    public class ConsumoService
    {
        private readonly IRepository<ConsumoDiario> _repository;

        public ConsumoService()
        {
            _repository = new ConsumoDiarioRepository();
        }

        public void RegistrarConsumosMasivos(List<ConsumoDiario> lista)
        {
            foreach (var item in lista)
            {
                _repository.Create(item);
            }
        }
        public void RegistrarConsumo(ConsumoDiario consumo)
        {
            if (consumo.CaloriasConsumidas < 0)
                throw new ArgumentException("Las calorías consumidas no pueden ser valores negativos.", nameof(consumo));

            _repository.Create(consumo);
        }

        public void EliminarConsumo(Guid id)
        {
            _repository.Delete(id);
        }

        public ResumenDiario ObtenerResumenDiario(DateTime fecha, decimal metaCalorias)
        {
            var repoConcreto = (ConsumoDiarioRepository)_repository;
            var registros = repoConcreto.GetByDate(fecha);

            if (registros == null || !registros.Any())
            {
                return new ResumenDiario
                {
                    CaloriasConsumidas = 0,
                    CarbohidratosConsumidos = 0,
                    DiferenciaCalorias = metaCalorias,
                    TieneRegistros = false
                };
            }

            var calorias = registros.Sum(r => r.CaloriasConsumidas);
            var carbohidratos = registros.Sum(r => r.CarbohidratosConsumidos);

            return new ResumenDiario
            {
                CaloriasConsumidas = calorias,
                CarbohidratosConsumidos = carbohidratos,
                DiferenciaCalorias = metaCalorias - calorias,
                TieneRegistros = true
            };
        }

        public ResumenDiario ObtenerResumenDiario(Guid usuarioId, DateTime fecha, decimal metaCalorias)
        {
            var registros = _repository.GetAll()
                .Where(r => r.UsuarioId == usuarioId && r.Fecha.Date == fecha.Date)
                .ToList();

            if (!registros.Any())
            {
                return new ResumenDiario
                {
                    MetaCalorias = metaCalorias,
                    CaloriasConsumidas = 0,
                    CarbohidratosConsumidos = 0,
                    DiferenciaCalorias = metaCalorias,
                    TieneRegistros = false
                };
            }

            var calorias = registros.Sum(r => r.CaloriasConsumidas);
            var carbohidratos = registros.Sum(r => r.CarbohidratosConsumidos);
            var proteinas = registros.Sum(r => r.ProteinasConsumidas);
            var grasas = registros.Sum(r => r.GrasasConsumidas);

            return new ResumenDiario
            {
                MetaCalorias = metaCalorias,
                CaloriasConsumidas = calorias,
                CarbohidratosConsumidos = carbohidratos,
                ProteinasConsumidas = proteinas,
                GrasasConsumidas = grasas,
                DiferenciaCalorias = metaCalorias - calorias,
                TieneRegistros = true
            };
        }

        public ResumenRango ObtenerResumenPorRango(Guid usuarioId, DateTime fechaInicio, DateTime fechaFin)
        {
            if (fechaInicio > fechaFin)
                throw new ArgumentException("La fecha inicio no puede ser mayor que la fecha fin");

            var registros = _repository.GetAll()
                .Where(r => r.UsuarioId == usuarioId && r.Fecha.Date >= fechaInicio.Date && r.Fecha.Date <= fechaFin.Date)
                .ToList();

            if (!registros.Any())
                return new ResumenRango();

            var totalCalorias = registros.Sum(r => r.CaloriasConsumidas);
            var totalCarbohidratos = registros.Sum(r => r.CarbohidratosConsumidos);
            var dias = registros.Select(r => r.Fecha.Date).Distinct().Count();

            return new ResumenRango
            {
                TotalCalorias = totalCalorias,
                TotalCarbohidratos = totalCarbohidratos,
                PromedioCalorias = totalCalorias / (dias == 0 ? 1 : dias),
                PromedioCarbohidratos = totalCarbohidratos / (dias == 0 ? 1 : dias),
                DiasConRegistros = dias
            };
        }
        public ResumenRango ObtenerResumenPorRango(DateTime fechaInicio, DateTime fechaFin)
        {
            if (fechaInicio > fechaFin)
                throw new ArgumentException("La fecha inicio no puede ser mayor que la fecha fin");

            var repoConcreto = (ConsumoDiarioRepository)_repository;
            var registros = repoConcreto.GetByRange(fechaInicio, fechaFin);

            if (registros == null || !registros.Any())
                return new ResumenRango();

            var totalCalorias = registros.Sum(r => r.CaloriasConsumidas);
            var totalCarbohidratos = registros.Sum(r => r.CarbohidratosConsumidos);
            var dias = registros.Select(r => r.Fecha.Date).Distinct().Count();

            return new ResumenRango
            {
                TotalCalorias = totalCalorias,
                TotalCarbohidratos = totalCarbohidratos,
                PromedioCalorias = totalCalorias / dias,
                PromedioCarbohidratos = totalCarbohidratos / dias,
                DiasConRegistros = dias
            };
        }

        public List<ConsumoDiario> ObtenerTodos()
        {
            return _repository.GetAll().ToList();
        }
    }
}