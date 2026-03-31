using Dragon_Nutrex.Controllers;
using Dragon_Nutrex.Models;
using System;
using System.Windows.Forms;

namespace Dragon_Nutrex.Views
{
    public partial class EstadisticasRangoForm : Form, IEstadisticaFiltrable
    {
        private readonly ConsumoController _controller = new ConsumoController();
        private Guid? _usuarioIdActual;

        public EstadisticasRangoForm()
        {
            InitializeComponent();
        }

        public void FiltrarPorUsuario(Guid usuarioId)
        {
            _usuarioIdActual = usuarioId;
            LimpiarPantalla();
            EjecutarCalculo();
        }
        private void LimpiarPantalla()
        {
            lblTotalCalorias.Text = "0 kcal";
            lblTotalCarbos.Text = "0 g";
            lblPromedioCalorias.Text = "0 kcal/día";
            lblPromedioCarbos.Text = "0 g/día";
            lblDias.Text = "0";
        }
        private void EjecutarCalculo()
        {
            if (!_usuarioIdActual.HasValue) return;

            var inicio = dtpFechaInicio.Value.Date;
            var fin = dtpFechaFin.Value.Date;

            // Validación básica
            if (inicio > fin) return;

            var resumen = _controller.ObtenerEstadisticasRangoPorUsuario(_usuarioIdActual.Value, inicio, fin);

            // Actualizar la interfaz
            lblTotalCalorias.Text = $"{resumen.TotalCalorias:N0} kcal";
            lblTotalCarbos.Text = $"{resumen.TotalCarbohidratos:N0} g";
            lblPromedioCalorias.Text = $"{resumen.PromedioCalorias:F2} kcal/día";
            lblPromedioCarbos.Text = $"{resumen.PromedioCarbohidratos:F2} g/día";
            lblDias.Text = resumen.DiasConRegistros.ToString();
        }
        private void btnCalcular_Click(object sender, EventArgs e)
        {
            EjecutarCalculo();
        }

        private void EstadisticasRangoForm_Load(object sender, EventArgs e)
        {

        }
    }
}