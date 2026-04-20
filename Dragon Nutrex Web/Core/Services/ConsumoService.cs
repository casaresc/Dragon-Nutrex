using Dragon_Nutrex_Web.Core.Interfaces;
using Dragon_Nutrex_Web.Core.Models;
using Dragon_Nutrex_Web.Infrastructure.Repositories;

namespace Dragon_Nutrex_Web.Core.Services
{
    /// <summary>
    /// Gestiona la lógica de negocio relacionada con consumos diarios y resúmenes estadísticos.
    /// </summary>
    public class ConsumoService
    {
        private readonly IConsumoDiarioRepository consumoRepository;

        /// <summary>
        /// Inicializa una nueva instancia de <see cref="ConsumoService"/>.
        /// </summary>
        /// <param name="consumoRepository">Repositorio de persistencia de consumos diarios.</param>
        public ConsumoService(IConsumoDiarioRepository consumoRepository)
        {
            this.consumoRepository = consumoRepository;
        }

        /// <summary>
        /// Registra múltiples consumos diarios.
        /// </summary>
        /// <param name="consumos">Lista de consumos a registrar.</param>
        public void RegistrarConsumosMasivos(List<ConsumoDiario> consumos)
        {
            foreach (var consumo in consumos)
            {
                if (consumo.Id == Guid.Empty)
                {
                    consumo.Id = Guid.NewGuid();
                }

                consumoRepository.Create(consumo);
            }
        }

        /// <summary>
        /// Registra un consumo diario individual.
        /// </summary>
        /// <param name="consumo">Consumo a registrar.</param>
        public void RegistrarConsumo(ConsumoDiario consumo)
        {
            ValidarConsumo(consumo);

            if (consumo.Id == Guid.Empty)
            {
                consumo.Id = Guid.NewGuid();
            }

            consumoRepository.Create(consumo);
        }

        /// <summary>
        /// Elimina un consumo diario por su identificador.
        /// </summary>
        /// <param name="consumoId">Identificador del consumo.</param>
        public void EliminarConsumo(Guid consumoId)
        {
            consumoRepository.Delete(consumoId);
        }

        /// <summary>
        /// Obtiene el resumen diario global para una fecha.
        /// </summary>
        /// <param name="fecha">Fecha a consultar.</param>
        /// <param name="metaCalorias">Meta calórica de referencia.</param>
        /// <returns>Resumen diario calculado.</returns>
        public ResumenDiario ObtenerResumenDiario(DateTime fecha, decimal metaCalorias)
        {
            var registros = consumoRepository.GetByDate(fecha);
            return ConstruirResumenDiario(registros, metaCalorias);
        }

        /// <summary>
        /// Obtiene el resumen diario para un usuario específico en una fecha.
        /// </summary>
        /// <param name="usuarioId">Identificador del usuario.</param>
        /// <param name="fecha">Fecha a consultar.</param>
        /// <param name="metaCalorias">Meta calórica de referencia.</param>
        /// <returns>Resumen diario calculado.</returns>
        public ResumenDiario ObtenerResumenDiario(Guid usuarioId, DateTime fecha, decimal metaCalorias)
        {
            var registros = consumoRepository.GetAll()
                .Where(consumo => consumo.UsuarioId == usuarioId && consumo.Fecha.Date == fecha.Date)
                .ToList();

            return ConstruirResumenDiario(registros, metaCalorias);
        }

        /// <summary>
        /// Obtiene el resumen por rango de fechas para un usuario específico.
        /// </summary>
        /// <param name="usuarioId">Identificador del usuario.</param>
        /// <param name="fechaInicio">Fecha inicial del rango.</param>
        /// <param name="fechaFin">Fecha final del rango.</param>
        /// <returns>Resumen por rango calculado.</returns>
        public ResumenRango ObtenerResumenPorRango(Guid usuarioId, DateTime fechaInicio, DateTime fechaFin)
        {
            ValidarRangoFechas(fechaInicio, fechaFin);

            var registros = consumoRepository.GetAll()
                .Where(consumo =>
                    consumo.UsuarioId == usuarioId &&
                    consumo.Fecha.Date >= fechaInicio.Date &&
                    consumo.Fecha.Date <= fechaFin.Date)
                .ToList();

            return ConstruirResumenRango(registros);
        }

        /// <summary>
        /// Obtiene el resumen global por rango de fechas.
        /// </summary>
        /// <param name="fechaInicio">Fecha inicial del rango.</param>
        /// <param name="fechaFin">Fecha final del rango.</param>
        /// <returns>Resumen por rango calculado.</returns>
        public ResumenRango ObtenerResumenPorRango(DateTime fechaInicio, DateTime fechaFin)
        {
            ValidarRangoFechas(fechaInicio, fechaFin);

            var registros = consumoRepository.GetByRange(fechaInicio, fechaFin);
            return ConstruirResumenRango(registros);
        }

        /// <summary>
        /// Obtiene todos los consumos registrados.
        /// </summary>
        /// <returns>Lista de consumos.</returns>
        public List<ConsumoDiario> ObtenerTodos()
        {
            return consumoRepository.GetAll();
        }

        /// <summary>
        /// Valida las reglas mínimas de negocio de un consumo.
        /// </summary>
        /// <param name="consumo">Consumo a validar.</param>
        private static void ValidarConsumo(ConsumoDiario consumo)
        {
            if (consumo.CaloriasConsumidas < 0)
                throw new ArgumentException("Las calorías consumidas no pueden ser valores negativos.", nameof(consumo));
        }

        /// <summary>
        /// Valida que el rango de fechas sea correcto.
        /// </summary>
        /// <param name="fechaInicio">Fecha inicial.</param>
        /// <param name="fechaFin">Fecha final.</param>
        private static void ValidarRangoFechas(DateTime fechaInicio, DateTime fechaFin)
        {
            if (fechaInicio > fechaFin)
                throw new ArgumentException("La fecha inicio no puede ser mayor que la fecha fin.");
        }

        /// <summary>
        /// Construye un resumen diario a partir de una colección de consumos.
        /// </summary>
        /// <param name="registros">Registros de consumo.</param>
        /// <param name="metaCalorias">Meta calórica de referencia.</param>
        /// <returns>Resumen diario calculado.</returns>
        private static ResumenDiario ConstruirResumenDiario(List<ConsumoDiario> registros, decimal metaCalorias)
        {
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

            var calorias = registros.Sum(registro => registro.CaloriasConsumidas);
            var carbohidratos = registros.Sum(registro => registro.CarbohidratosConsumidos);
            var proteinas = registros.Sum(registro => registro.ProteinasConsumidas);
            var grasas = registros.Sum(registro => registro.GrasasConsumidas);

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

        /// <summary>
        /// Construye un resumen por rango a partir de una colección de consumos.
        /// </summary>
        /// <param name="registros">Registros de consumo.</param>
        /// <returns>Resumen por rango calculado.</returns>
        private static ResumenRango ConstruirResumenRango(List<ConsumoDiario> registros)
        {
            if (!registros.Any())
            {
                return new ResumenRango();
            }

            var totalCalorias = registros.Sum(registro => registro.CaloriasConsumidas);
            var totalCarbohidratos = registros.Sum(registro => registro.CarbohidratosConsumidos);
            var totalProteinas = registros.Sum(registro => registro.ProteinasConsumidas);
            var totalGrasas = registros.Sum(registro => registro.GrasasConsumidas);
            var diasConRegistros = registros.Select(registro => registro.Fecha.Date).Distinct().Count();

            return new ResumenRango
            {
                TotalCalorias = totalCalorias,
                TotalCarbohidratos = totalCarbohidratos,
                TotalProteinas = totalProteinas,
                TotalGrasas = totalGrasas,
                PromedioCalorias = Math.Round(totalCalorias / (diasConRegistros == 0 ? 1 : diasConRegistros), 2),
                PromedioCarbohidratos = Math.Round(totalCarbohidratos / (diasConRegistros == 0 ? 1 : diasConRegistros), 2),
                PromedioProteinas = Math.Round(totalProteinas / (diasConRegistros == 0 ? 1 : diasConRegistros), 2),
                PromedioGrasas = Math.Round(totalGrasas / (diasConRegistros == 0 ? 1 : diasConRegistros), 2),
                DiasConRegistros = diasConRegistros
            };
        }
    }
}