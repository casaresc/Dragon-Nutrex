using Dragon_Nutrex.Controllers;
using Dragon_Nutrex.Models;

namespace Dragon_Nutrex.Views
{
    public partial class RequerimientosForm : Form, IEstadisticaFiltrable
    {
        private readonly NutricionController _nutricionController = new NutricionController();
        private readonly UsuarioController _usuarioController = new UsuarioController();
        private bool _isCargandoUsuario = false; // Flag para evitar recalcular mientras cargamos datos

        public RequerimientosForm()
        {
            InitializeComponent();
            ConfigurarComboBoxes();
            SuscribirEventos();
        }

        // Método de la interfaz para recibir el cambio de usuario
        public void FiltrarPorUsuario(Guid usuarioId)
        {
            var usuario = _usuarioController.ObtenerPorId(usuarioId);
            if (usuario != null)
            {
                CargarDatosUsuario(usuario);
            }
        }

        private void CargarDatosUsuario(Usuario user)
        {
            _isCargandoUsuario = true; // Bloqueamos el recalcular temporalmente

            txtPeso.Text = user.Peso.ToString();
            txtAltura.Text = user.Altura.ToString();
            txtEdad.Text = user.Edad.ToString();

            cmbActividad.SelectedItem = user.NivelActividad;
            cmbObjetivo.SelectedItem = user.Objetivo;
            cmbDieta.SelectedItem = user.TipoDieta;

            _isCargandoUsuario = false; // Desbloqueamos
            Recalcular(); // Forzamos el cálculo con los datos nuevos
        }

        private void ConfigurarComboBoxes()
        {
            cmbActividad.DataSource = Enum.GetValues(typeof(NivelActividad));
            cmbObjetivo.DataSource = Enum.GetValues(typeof(ObjetivoNutricional));
            cmbDieta.DataSource = Enum.GetValues(typeof(TipoDieta));

            lblCalorias.Visible = lblGrasa.Visible = lblCarbos.Visible = lblProteina.Visible = false;
        }

        private void SuscribirEventos()
        {
            // Agregamos la verificación de _isCargandoUsuario para evitar cálculos parciales
            txtPeso.TextChanged += (s, e) => { if (!_isCargandoUsuario) Recalcular(); };
            txtAltura.TextChanged += (s, e) => { if (!_isCargandoUsuario) Recalcular(); };
            txtEdad.TextChanged += (s, e) => { if (!_isCargandoUsuario) Recalcular(); };
            cmbActividad.SelectedIndexChanged += (s, e) => { if (!_isCargandoUsuario) Recalcular(); };
            cmbObjetivo.SelectedIndexChanged += (s, e) => { if (!_isCargandoUsuario) Recalcular(); };
            cmbDieta.SelectedIndexChanged += (s, e) => { if (!_isCargandoUsuario) Recalcular(); };
        }

        private void Recalcular()
        {
            if (_isCargandoUsuario) return;

            if (!decimal.TryParse(txtPeso.Text, out decimal peso) || peso <= 0) return;
            if (!decimal.TryParse(txtAltura.Text, out decimal altura) || altura <= 0) return;
            if (!int.TryParse(txtEdad.Text, out int edad) || edad <= 0) return;

            if (cmbActividad.SelectedItem is NivelActividad actividad &&
                cmbObjetivo.SelectedItem is ObjetivoNutricional objetivo &&
                cmbDieta.SelectedItem is TipoDieta dieta)
            {
                var resultado = _nutricionController.CalcularPlanNutricional(peso, altura, edad, actividad, objetivo, dieta);

                if (resultado != null)
                {
                    MostrarResultados(resultado);
                }
            }
        }

        private void MostrarResultados(RequerimientoNutricional res)
        {
            lblCalorias.Visible = lblGrasa.Visible = lblCarbos.Visible = lblProteina.Visible = true;

            lblCalorias.Text = $"{res.CaloriasObjetivo:N0} kcal";
            lblCarbos.Text = $"{res.CarbohidratosGramos:N1} g";
            lblProteina.Text = $"{res.ProteinasGramos:N1} g";
            lblGrasa.Text = $"{res.GrasasGramos:N1} g";
        }
    }
}