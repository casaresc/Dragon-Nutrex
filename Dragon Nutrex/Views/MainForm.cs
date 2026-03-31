using Dragon_Nutrex.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Dragon_Nutrex.Views
{
    public partial class MainForm : Form
    {
        private readonly MenuController _controller = new MenuController();

        public MainForm()
        {
            InitializeComponent();
        }

        private void btnUsuarios_Click(object sender, EventArgs e)
        {
            AbrirFormulario<UsuarioListForm>();
        }

        private void btnProductos_Click(object sender, EventArgs e)
        {
            AbrirFormulario<ProductosForm>();
        }

        private void btnMenús_Click(object sender, EventArgs e)
        {
            AbrirFormulario<MenusForm>();
        }

        private void btnIMC_Click(object sender, EventArgs e)
        {
            var form = new IMCForm();
            form.ShowDialog();
        }

        private void btnRequerimientos_Click(object sender, EventArgs e)
        {
            var form = new RequerimientosForm();
            form.ShowDialog();
        }

        private void btnConsumoVsMeta_Click(object sender, EventArgs e)
        {
            var form = new ConsumoVsMetaForm();
            form.ShowDialog();
        }

        private void btnEstadisticasPorRango_Click(object sender, EventArgs e)
        {
            var form = new EstadisticasRangoForm();
            form.ShowDialog();
        }

        private void btnGenerarDatos_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Cambiar el cursor a "Cargando"
                this.Cursor = Cursors.WaitCursor;
                btnGenerarDatos.Enabled = false; // Evitar doble clic

                // 2. Ejecutar el Seed
                var seed = new DataSeedController();
                seed.GenerarDatosIniciales();

                // 3. Informar al usuario
                MessageBox.Show("¡Datos de prueba generados con éxito!\n" +
                                "- 25 Usuarios creados.\n" +
                                "- 50 Productos registrados.\n" +
                                "- 375 Menús y Consumos históricos.",
                                "Data Seed", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al generar datos: {ex.Message}", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // 4. Restaurar el cursor siempre (incluso si falla)
                this.Cursor = Cursors.Default;
                btnGenerarDatos.Enabled = true;
            }
        }

        private void btnEstadísticas_Click(object sender, EventArgs e)
        {
            if (_controller.ObtenerTodosLosMenus().Count == 0)
            {
                MessageBox.Show("Aún no hay registros de consumo para generar estadísticas. ¡Empieza por crear un menú!",
                                "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            AbrirFormulario<EstadisticasForm>();
        }

        private void AbrirFormulario<T>() where T : Form, new()
        {
            Form? formularioEncontrado = this.MdiChildren.FirstOrDefault(f => f is T);
            if (formularioEncontrado != null)
            {
                formularioEncontrado.BringToFront();
                return;
            }

            T nuevoFormulario = new T();
            nuevoFormulario.MdiParent = this;
            nuevoFormulario.Show();
        }
    }
}
