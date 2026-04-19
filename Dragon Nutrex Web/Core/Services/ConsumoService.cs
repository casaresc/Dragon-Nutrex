using Dragon_Nutrex_Web.Core.Interfaces;
using Dragon_Nutrex_Web.Core.Models;
using Dragon_Nutrex_Web.Infrastructure.Repositories;

namespace Dragon_Nutrex_Web.Core.Services
{
    public class ConsumoService
    {
        private readonly IRepository<ConsumoDiario> repository;

        public ConsumoService(IRepository<ConsumoDiario> repository)
        {
            this.repository = repository;
        }

        public void RegistrarConsumosMasivos(List<ConsumoDiario> lista)
        {
            foreach (var item in lista)
            {
                if (item.Id == Guid.Empty)
                {
                    item.Id = Guid.NewGuid();
                }

                repository.Create(item);
            }
        }

        public void RegistrarConsumo(ConsumoDiario consumo)
        {
            if (consumo.CaloriasConsumidas < 0)
                throw new ArgumentException("Las calorías consumidas no pueden ser valores negativos.", nameof(consumo));

            if (consumo.Id == Guid.Empty)
            {
                consumo.Id = Guid.NewGuid();
            }

            repository.Create(consumo);
        }

        public void EliminarConsumo(Guid id)
        {
            repository.Delete(id);
        }

        public ResumenDiario ObtenerResumenDiario(DateTime fecha, decimal metaCalorias)
        {
            var repoConcreto = (ConsumoDiarioRepository)repository;
            var registros = repoConcreto.GetByDate(fecha);

            if (registros == null || !registros.Any())
            {
                return new ResumenDiario
                {
                    MetaCalorias = metaCalorias,
                    CaloriasConsumidas = 0,
                    CarbohidratosConsumidos = 0,
                    GrasasConsumidas = 0,
                    ProteinasConsumidas = 0,
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
                GrasasConsumidas = grasas,
                ProteinasConsumidas = proteinas,
                DiferenciaCalorias = metaCalorias - calorias,
                TieneRegistros = true
            };
        }

        public ResumenDiario ObtenerResumenDiario(Guid usuarioId, DateTime fecha, decimal metaCalorias)
        {
            var registros = repository.GetAll()
                .Where(r => r.UsuarioId == usuarioId && r.Fecha.Date == fecha.Date)
                .ToList();

            if (!registros.Any())
            {
                return new ResumenDiario
                {
                    MetaCalorias = metaCalorias,
                    CaloriasConsumidas = 0,
                    CarbohidratosConsumidos = 0,
                    ProteinasConsumidas = 0,
                    GrasasConsumidas = 0,
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

            var registros = repository.GetAll()
                .Where(r => r.UsuarioId == usuarioId &&
                            r.Fecha.Date >= fechaInicio.Date &&
                            r.Fecha.Date <= fechaFin.Date)
                .ToList();

            if (!registros.Any())
                return new ResumenRango();

            var totalCalorias = registros.Sum(r => r.CaloriasConsumidas);
            var totalCarbohidratos = registros.Sum(r => r.CarbohidratosConsumidos);
            var totalGrasas = registros.Sum(r => r.GrasasConsumidas);
            var totalProteinas = registros.Sum(r => r.ProteinasConsumidas);
            var dias = registros.Select(r => r.Fecha.Date).Distinct().Count();

            return new ResumenRango
            {
                TotalCalorias = totalCalorias,
                TotalCarbohidratos = totalCarbohidratos,
                TotalGrasas = totalGrasas,
                TotalProteinas = totalProteinas,
                PromedioCalorias = Math.Round(totalCalorias / (dias == 0 ? 1 : dias), 2),
                PromedioCarbohidratos = Math.Round(totalCarbohidratos / (dias == 0 ? 1 : dias), 2),
                PromedioGrasas = Math.Round(totalGrasas / (dias == 0 ? 1 : dias), 2),
                PromedioProteinas = Math.Round(totalProteinas / (dias == 0 ? 1 : dias), 2),
                DiasConRegistros = dias
            };
        }

        public ResumenRango ObtenerResumenPorRango(DateTime fechaInicio, DateTime fechaFin)
        {
            if (fechaInicio > fechaFin)
                throw new ArgumentException("La fecha inicio no puede ser mayor que la fecha fin");

            var repoConcreto = (ConsumoDiarioRepository)repository;
            var registros = repoConcreto.GetByRange(fechaInicio, fechaFin);

            if (registros == null || !registros.Any())
                return new ResumenRango();

            var totalCalorias = registros.Sum(r => r.CaloriasConsumidas);
            var totalCarbohidratos = registros.Sum(r => r.CarbohidratosConsumidos);
            var totalGrasas = registros.Sum(r => r.GrasasConsumidas);
            var totalProteinas = registros.Sum(r => r.ProteinasConsumidas);
            var dias = registros.Select(r => r.Fecha.Date).Distinct().Count();

            return new ResumenRango
            {
                TotalCalorias = totalCalorias,
                TotalCarbohidratos = totalCarbohidratos,
                TotalGrasas = totalGrasas,
                TotalProteinas = totalProteinas,
                PromedioCalorias = totalCalorias / (dias == 0 ? 1 : dias),
                PromedioCarbohidratos = totalCarbohidratos / (dias == 0 ? 1 : dias),
                PromedioGrasas = totalGrasas / (dias == 0 ? 1 : dias),
                PromedioProteinas = totalProteinas / (dias == 0 ? 1 : dias),
                DiasConRegistros = dias
            };
        }

        public List<ConsumoDiario> ObtenerTodos()
        {
            return repository.GetAll();
        }
    }
}