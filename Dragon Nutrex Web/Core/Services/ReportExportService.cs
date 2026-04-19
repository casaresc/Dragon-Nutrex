using System.Text;
using Dragon_Nutrex_Web.Core.Models;

namespace Dragon_Nutrex_Web.Core.Services
{
    public class ReportExportService
    {
        public string ExportarConsumoMetaCsv(ResumenDiario resumen)
        {
            var sb = new StringBuilder();
            sb.AppendLine("MetaCalorias,CaloriasConsumidas,CarbohidratosConsumidos,ProteinasConsumidas,GrasasConsumidas,DiferenciaCalorias,TieneRegistros");
            sb.AppendLine($"{resumen.MetaCalorias},{resumen.CaloriasConsumidas},{resumen.CarbohidratosConsumidos},{resumen.ProteinasConsumidas},{resumen.GrasasConsumidas},{resumen.DiferenciaCalorias},{resumen.TieneRegistros}");
            return sb.ToString();
        }

        public string ExportarConsumoMetaTxt(ResumenDiario resumen)
        {
            var sb = new StringBuilder();
            sb.AppendLine("REPORTE: CONSUMO VS META");
            sb.AppendLine($"Meta de calorías: {resumen.MetaCalorias}");
            sb.AppendLine($"Calorías consumidas: {resumen.CaloriasConsumidas}");
            sb.AppendLine($"Carbohidratos consumidos: {resumen.CarbohidratosConsumidos}");
            sb.AppendLine($"Proteínas consumidas: {resumen.ProteinasConsumidas}");
            sb.AppendLine($"Grasas consumidas: {resumen.GrasasConsumidas}");
            sb.AppendLine($"Diferencia calórica: {resumen.DiferenciaCalorias}");
            sb.AppendLine($"Tiene registros: {resumen.TieneRegistros}");
            return sb.ToString();
        }

        public string ExportarResumenRangoCsv(ResumenRango resumen)
        {
            var sb = new StringBuilder();
            sb.AppendLine("TotalCalorias,TotalCarbohidratos,TotalProteinas,TotalGrasas,PromedioCalorias,PromedioCarbohidratos,PromedioProteinas,PromedioGrasas,DiasConRegistros");
            sb.AppendLine($"{resumen.TotalCalorias},{resumen.TotalCarbohidratos},{resumen.TotalProteinas},{resumen.TotalGrasas},{resumen.PromedioCalorias},{resumen.PromedioCarbohidratos},{resumen.PromedioProteinas},{resumen.PromedioGrasas},{resumen.DiasConRegistros}");
            return sb.ToString();
        }

        public string ExportarResumenRangoTxt(ResumenRango resumen)
        {
            var sb = new StringBuilder();
            sb.AppendLine("REPORTE: ESTADÍSTICAS POR RANGO");
            sb.AppendLine($"Total calorías: {resumen.TotalCalorias}");
            sb.AppendLine($"Total carbohidratos: {resumen.TotalCarbohidratos}");
            sb.AppendLine($"Total proteínas: {resumen.TotalProteinas}");
            sb.AppendLine($"Total grasas: {resumen.TotalGrasas}");
            sb.AppendLine($"Promedio calorías: {resumen.PromedioCalorias}");
            sb.AppendLine($"Promedio carbohidratos: {resumen.PromedioCarbohidratos}");
            sb.AppendLine($"Promedio proteínas: {resumen.PromedioProteinas}");
            sb.AppendLine($"Promedio grasas: {resumen.PromedioGrasas}");
            sb.AppendLine($"Días con registros: {resumen.DiasConRegistros}");
            return sb.ToString();
        }

        public string ExportarProductoMasConsumidoCsv(ProductoMasConsumidoResultado? resultado)
        {
            var sb = new StringBuilder();
            sb.AppendLine("ProductoId,NombreProducto,TotalPorciones,TotalRegistros");

            if (resultado is not null)
            {
                sb.AppendLine($"{resultado.ProductoId},{EscapeCsv(resultado.NombreProducto)},{resultado.TotalPorciones},{resultado.TotalRegistros}");
            }

            return sb.ToString();
        }

        public string ExportarProductoMasConsumidoTxt(ProductoMasConsumidoResultado? resultado)
        {
            var sb = new StringBuilder();
            sb.AppendLine("REPORTE: PRODUCTO MÁS CONSUMIDO");

            if (resultado is null)
            {
                sb.AppendLine("No hay datos disponibles.");
                return sb.ToString();
            }

            sb.AppendLine($"Producto: {resultado.NombreProducto}");
            sb.AppendLine($"Total porciones: {resultado.TotalPorciones}");
            sb.AppendLine($"Total registros: {resultado.TotalRegistros}");
            return sb.ToString();
        }

        public string ExportarPorcentajeDietasCsv(List<PorcentajeTipoDietaResultado> resultados)
        {
            var sb = new StringBuilder();
            sb.AppendLine("TipoDieta,CantidadUsuarios,Porcentaje");

            foreach (var item in resultados)
            {
                sb.AppendLine($"{EscapeCsv(item.TipoDieta)},{item.CantidadUsuarios},{item.Porcentaje}");
            }

            return sb.ToString();
        }

        public string ExportarPorcentajeDietasTxt(List<PorcentajeTipoDietaResultado> resultados)
        {
            var sb = new StringBuilder();
            sb.AppendLine("REPORTE: PORCENTAJE DE TIPOS DE DIETA");

            if (!resultados.Any())
            {
                sb.AppendLine("No hay datos disponibles.");
                return sb.ToString();
            }

            foreach (var item in resultados)
            {
                sb.AppendLine($"{item.TipoDieta}: {item.CantidadUsuarios} usuario(s) - {item.Porcentaje}%");
            }

            return sb.ToString();
        }

        public string ExportarUsuariosConMasMenusCsv(List<UsuarioMenusResultado> resultados)
        {
            var sb = new StringBuilder();
            sb.AppendLine("UsuarioId,NombreUsuario,CantidadMenus");

            foreach (var item in resultados)
            {
                sb.AppendLine($"{item.UsuarioId},{EscapeCsv(item.NombreUsuario)},{item.CantidadMenus}");
            }

            return sb.ToString();
        }

        public string ExportarUsuariosConMasMenusTxt(List<UsuarioMenusResultado> resultados)
        {
            var sb = new StringBuilder();
            sb.AppendLine("REPORTE: USUARIOS CON MÁS MENÚS INGRESADOS");

            if (!resultados.Any())
            {
                sb.AppendLine("No hay datos disponibles.");
                return sb.ToString();
            }

            foreach (var item in resultados)
            {
                sb.AppendLine($"{item.NombreUsuario}: {item.CantidadMenus} menú(s)");
            }

            return sb.ToString();
        }

        private static string EscapeCsv(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return "";

            if (value.Contains(',') || value.Contains('"') || value.Contains('\n'))
            {
                return $"\"{value.Replace("\"", "\"\"")}\"";
            }

            return value;
        }
    }
}