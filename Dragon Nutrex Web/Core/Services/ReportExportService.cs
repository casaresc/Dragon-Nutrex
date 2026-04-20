using System.Text;
using Dragon_Nutrex_Web.Core.Models;

namespace Dragon_Nutrex_Web.Core.Services
{
    /// <summary>
    /// Genera contenido exportable de reportes en formatos CSV y TXT.
    /// </summary>
    public class ReportExportService
    {
        /// <summary>
        /// Genera un reporte CSV para consumo versus meta.
        /// </summary>
        /// <param name="resumen">Resumen diario a exportar.</param>
        /// <returns>Contenido del reporte en formato CSV.</returns>
        public string ExportarConsumoMetaCsv(ResumenDiario resumen)
        {
            var stringBuilder = CrearStringBuilder();
            stringBuilder.AppendLine("MetaCalorias,CaloriasConsumidas,CarbohidratosConsumidos,ProteinasConsumidas,GrasasConsumidas,DiferenciaCalorias,TieneRegistros");
            stringBuilder.AppendLine($"{resumen.MetaCalorias},{resumen.CaloriasConsumidas},{resumen.CarbohidratosConsumidos},{resumen.ProteinasConsumidas},{resumen.GrasasConsumidas},{resumen.DiferenciaCalorias},{resumen.TieneRegistros}");

            return stringBuilder.ToString();
        }

        /// <summary>
        /// Genera un reporte TXT para consumo versus meta.
        /// </summary>
        /// <param name="resumen">Resumen diario a exportar.</param>
        /// <returns>Contenido del reporte en formato TXT.</returns>
        public string ExportarConsumoMetaTxt(ResumenDiario resumen)
        {
            var stringBuilder = CrearStringBuilder();
            stringBuilder.AppendLine("REPORTE: CONSUMO VS META");
            stringBuilder.AppendLine($"Meta de calorías: {resumen.MetaCalorias}");
            stringBuilder.AppendLine($"Calorías consumidas: {resumen.CaloriasConsumidas}");
            stringBuilder.AppendLine($"Carbohidratos consumidos: {resumen.CarbohidratosConsumidos}");
            stringBuilder.AppendLine($"Proteínas consumidas: {resumen.ProteinasConsumidas}");
            stringBuilder.AppendLine($"Grasas consumidas: {resumen.GrasasConsumidas}");
            stringBuilder.AppendLine($"Diferencia calórica: {resumen.DiferenciaCalorias}");
            stringBuilder.AppendLine($"Tiene registros: {resumen.TieneRegistros}");

            return stringBuilder.ToString();
        }

        /// <summary>
        /// Genera un reporte CSV para el resumen por rango.
        /// </summary>
        /// <param name="resumen">Resumen por rango a exportar.</param>
        /// <returns>Contenido del reporte en formato CSV.</returns>
        public string ExportarResumenRangoCsv(ResumenRango resumen)
        {
            var stringBuilder = CrearStringBuilder();
            stringBuilder.AppendLine("TotalCalorias,TotalCarbohidratos,TotalProteinas,TotalGrasas,PromedioCalorias,PromedioCarbohidratos,PromedioProteinas,PromedioGrasas,DiasConRegistros");
            stringBuilder.AppendLine($"{resumen.TotalCalorias},{resumen.TotalCarbohidratos},{resumen.TotalProteinas},{resumen.TotalGrasas},{resumen.PromedioCalorias},{resumen.PromedioCarbohidratos},{resumen.PromedioProteinas},{resumen.PromedioGrasas},{resumen.DiasConRegistros}");

            return stringBuilder.ToString();
        }

        /// <summary>
        /// Genera un reporte TXT para el resumen por rango.
        /// </summary>
        /// <param name="resumen">Resumen por rango a exportar.</param>
        /// <returns>Contenido del reporte en formato TXT.</returns>
        public string ExportarResumenRangoTxt(ResumenRango resumen)
        {
            var stringBuilder = CrearStringBuilder();
            stringBuilder.AppendLine("REPORTE: ESTADÍSTICAS POR RANGO");
            stringBuilder.AppendLine($"Total calorías: {resumen.TotalCalorias}");
            stringBuilder.AppendLine($"Total carbohidratos: {resumen.TotalCarbohidratos}");
            stringBuilder.AppendLine($"Total proteínas: {resumen.TotalProteinas}");
            stringBuilder.AppendLine($"Total grasas: {resumen.TotalGrasas}");
            stringBuilder.AppendLine($"Promedio calorías: {resumen.PromedioCalorias}");
            stringBuilder.AppendLine($"Promedio carbohidratos: {resumen.PromedioCarbohidratos}");
            stringBuilder.AppendLine($"Promedio proteínas: {resumen.PromedioProteinas}");
            stringBuilder.AppendLine($"Promedio grasas: {resumen.PromedioGrasas}");
            stringBuilder.AppendLine($"Días con registros: {resumen.DiasConRegistros}");

            return stringBuilder.ToString();
        }

        /// <summary>
        /// Genera un reporte CSV para el producto más consumido.
        /// </summary>
        /// <param name="resultado">Resultado del producto más consumido.</param>
        /// <returns>Contenido del reporte en formato CSV.</returns>
        public string ExportarProductoMasConsumidoCsv(ProductoMasConsumidoResultado? resultado)
        {
            var stringBuilder = CrearStringBuilder();
            stringBuilder.AppendLine("ProductoId,NombreProducto,TotalPorciones,TotalRegistros");

            if (resultado is not null)
            {
                stringBuilder.AppendLine($"{resultado.ProductoId},{EscapeCsv(resultado.NombreProducto)},{resultado.TotalPorciones},{resultado.TotalRegistros}");
            }

            return stringBuilder.ToString();
        }

        /// <summary>
        /// Genera un reporte TXT para el producto más consumido.
        /// </summary>
        /// <param name="resultado">Resultado del producto más consumido.</param>
        /// <returns>Contenido del reporte en formato TXT.</returns>
        public string ExportarProductoMasConsumidoTxt(ProductoMasConsumidoResultado? resultado)
        {
            var stringBuilder = CrearStringBuilder();
            stringBuilder.AppendLine("REPORTE: PRODUCTO MÁS CONSUMIDO");

            if (resultado is null)
            {
                stringBuilder.AppendLine("No hay datos disponibles.");
                return stringBuilder.ToString();
            }

            stringBuilder.AppendLine($"Producto: {resultado.NombreProducto}");
            stringBuilder.AppendLine($"Total porciones: {resultado.TotalPorciones}");
            stringBuilder.AppendLine($"Total registros: {resultado.TotalRegistros}");

            return stringBuilder.ToString();
        }

        /// <summary>
        /// Genera un reporte CSV con el porcentaje de tipos de dieta.
        /// </summary>
        /// <param name="resultados">Lista de resultados por tipo de dieta.</param>
        /// <returns>Contenido del reporte en formato CSV.</returns>
        public string ExportarPorcentajeDietasCsv(List<PorcentajeTipoDietaResultado> resultados)
        {
            var stringBuilder = CrearStringBuilder();
            stringBuilder.AppendLine("TipoDieta,CantidadUsuarios,Porcentaje");

            foreach (var resultado in resultados)
            {
                stringBuilder.AppendLine($"{EscapeCsv(resultado.TipoDieta)},{resultado.CantidadUsuarios},{resultado.Porcentaje}");
            }

            return stringBuilder.ToString();
        }

        /// <summary>
        /// Genera un reporte TXT con el porcentaje de tipos de dieta.
        /// </summary>
        /// <param name="resultados">Lista de resultados por tipo de dieta.</param>
        /// <returns>Contenido del reporte en formato TXT.</returns>
        public string ExportarPorcentajeDietasTxt(List<PorcentajeTipoDietaResultado> resultados)
        {
            var stringBuilder = CrearStringBuilder();
            stringBuilder.AppendLine("REPORTE: PORCENTAJE DE TIPOS DE DIETA");

            if (!resultados.Any())
            {
                stringBuilder.AppendLine("No hay datos disponibles.");
                return stringBuilder.ToString();
            }

            foreach (var resultado in resultados)
            {
                stringBuilder.AppendLine($"{resultado.TipoDieta}: {resultado.CantidadUsuarios} usuario(s) - {resultado.Porcentaje}%");
            }

            return stringBuilder.ToString();
        }

        /// <summary>
        /// Genera un reporte CSV con los usuarios que tienen más menús ingresados.
        /// </summary>
        /// <param name="resultados">Lista de usuarios y cantidad de menús.</param>
        /// <returns>Contenido del reporte en formato CSV.</returns>
        public string ExportarUsuariosConMasMenusCsv(List<UsuarioMenusResultado> resultados)
        {
            var stringBuilder = CrearStringBuilder();
            stringBuilder.AppendLine("UsuarioId,NombreUsuario,CantidadMenus");

            foreach (var resultado in resultados)
            {
                stringBuilder.AppendLine($"{resultado.UsuarioId},{EscapeCsv(resultado.NombreUsuario)},{resultado.CantidadMenus}");
            }

            return stringBuilder.ToString();
        }

        /// <summary>
        /// Genera un reporte TXT con los usuarios que tienen más menús ingresados.
        /// </summary>
        /// <param name="resultados">Lista de usuarios y cantidad de menús.</param>
        /// <returns>Contenido del reporte en formato TXT.</returns>
        public string ExportarUsuariosConMasMenusTxt(List<UsuarioMenusResultado> resultados)
        {
            var stringBuilder = CrearStringBuilder();
            stringBuilder.AppendLine("REPORTE: USUARIOS CON MÁS MENÚS INGRESADOS");

            if (!resultados.Any())
            {
                stringBuilder.AppendLine("No hay datos disponibles.");
                return stringBuilder.ToString();
            }

            foreach (var resultado in resultados)
            {
                stringBuilder.AppendLine($"{resultado.NombreUsuario}: {resultado.CantidadMenus} menú(s)");
            }

            return stringBuilder.ToString();
        }

        /// <summary>
        /// Crea una nueva instancia de <see cref="StringBuilder"/> para construir reportes.
        /// </summary>
        /// <returns>Instancia de <see cref="StringBuilder"/>.</returns>
        private static StringBuilder CrearStringBuilder()
        {
            return new StringBuilder();
        }

        /// <summary>
        /// Escapa un valor de texto para formato CSV.
        /// </summary>
        /// <param name="valor">Valor a escapar.</param>
        /// <returns>Valor escapado para CSV.</returns>
        private static string EscapeCsv(string valor)
        {
            if (string.IsNullOrWhiteSpace(valor))
            {
                return string.Empty;
            }

            if (valor.Contains(',') || valor.Contains('"') || valor.Contains('\n'))
            {
                return $"\"{valor.Replace("\"", "\"\"")}\"";
            }

            return valor;
        }
    }
}