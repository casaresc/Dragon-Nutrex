using Dragon_Nutrex.Controllers;
using Dragon_Nutrex.Models;
using Dragon_Nutrex.Services;

namespace Dragon_Nutrex.Views
{
    public partial class ConsumoVsMetaForm : Form, IEstadisticaFiltrable
    {
        private readonly ConsumoController _controller = new ConsumoController();
        private readonly ConsumoService _consumoService = new ConsumoService();
        private Guid? _usuarioIdActual; 

        public ConsumoVsMetaForm()
        {
            InitializeComponent();


            dtpFecha.ValueChanged += (s, e) => ActualizarResumen();
        }

        public void FiltrarPorUsuario(Guid usuarioId)
        {
            _usuarioIdActual = usuarioId;
            ActualizarResumen();
        }
        public List<Guid> ObtenerUsuariosConRegistros()
        {
            var consumos = _consumoService.ObtenerTodos();
            return consumos.Select(c => c.UsuarioId).Distinct().ToList();
        }

        private void ActualizarResumen()
        {
            if (!_usuarioIdActual.HasValue) return;
            var resumen = _controller.ObtenerResumenParaUsuarioYFecha(_usuarioIdActual.Value, dtpFecha.Value);

            if (resumen != null)
            {
                lblMetaCalorias.Text = $"{resumen.MetaCalorias} kcal";
                lblCaloriasConsumidas.Text = $"{resumen.CaloriasConsumidas} kcal";
                lblCarbosConsumidos.Text = $"{resumen.CarbohidratosConsumidos} g";

                if (resumen.DiferenciaCalorias >= 0)
                {
                    lblDiferencia.Text = $"{resumen.DiferenciaCalorias} kcal restantes";
                    lblDiferencia.ForeColor = Color.Green;
                }
                else
                {
                    lblDiferencia.Text = $"{Math.Abs(resumen.DiferenciaCalorias)} kcal de exceso";
                    lblDiferencia.ForeColor = Color.Red;
                }
            }
            else
            {
                LimpiarPantalla();
            }
        }

        private void LimpiarPantalla()
        {
            lblMetaCalorias.Text = "0 kcal";
            lblCaloriasConsumidas.Text = "0 kcal";
            lblDiferencia.Text = "Sin registros para esta fecha";
            lblDiferencia.ForeColor = Color.Gray;
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            ActualizarResumen();
        }

        private void ConsumoVsMetaForm_Load(object sender, EventArgs e)
        {
            ActualizarResumen();
        }
    }
}