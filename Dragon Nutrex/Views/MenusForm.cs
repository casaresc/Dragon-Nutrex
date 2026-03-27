using Dragon_Nutrex.Models;
using Dragon_Nutrex.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Dragon_Nutrex.Views
{
    public partial class MenusForm : Form
    {
        private readonly MenuDiarioService _service;

        private Guid _menuSeleccionadoId = Guid.Empty;

        public MenusForm()
        {
            InitializeComponent();

            _service = new MenuDiarioService();
            MenusForm_Load();
        }

        private void MenusForm_Load()
        {
            CargarMenus();
        }

        private void CargarMenus()
        {
            dgvMenus.DataSource = null;

            dgvMenus.DataSource =
                _service.ObtenerMenus();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            var menu = ObtenerMenuDesdeFormulario();

            _service.CrearMenu(menu);


            MessageBox.Show(
                "Menu creado correctamente",
                "Éxito",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );

            CargarMenus();


            LimpiarFormulario();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (_menuSeleccionadoId == Guid.Empty)
            {
                MessageBox.Show(
                    "Seleccione un menú.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            var menu = ObtenerMenuDesdeFormulario();

            menu.Id = _menuSeleccionadoId;

            _service.ActualizarMenu(menu);

            CargarMenus();

            LimpiarFormulario();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (_menuSeleccionadoId == Guid.Empty)
            {
                MessageBox.Show(
                    "Seleccione un menú.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            var confirmacion = MessageBox.Show(
                "¿Desea eliminar este menú?",
                "Confirmación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmacion == DialogResult.Yes)
            {
                _service.EliminarMenu(
                    _menuSeleccionadoId);

                CargarMenus();

                LimpiarFormulario();
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
        }

        private void dgvMenus_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (dgvMenus.Rows[e.RowIndex].DataBoundItem is MenuDiario menu)
            {
                _menuSeleccionadoId = menu.Id;
                txtNombre.Text = menu.Nombre;
                dtpFecha.Value = menu.Fecha;
            }
        }

        private MenuDiario ObtenerMenuDesdeFormulario()
        {
            return new MenuDiario
            {
                Nombre =
                    txtNombre.Text.Trim(),

                Fecha =
                    dtpFecha.Value.Date
            };
        }

        private void LimpiarFormulario()
        {
            txtNombre.Clear();

            dtpFecha.Value =
                DateTime.Now;

            _menuSeleccionadoId =
                Guid.Empty;

            dgvMenus.ClearSelection();
        }

        private void btnAgregarProductos_Click(object? sender, EventArgs e)
        {

            if (dgvMenus.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione un menú de la lista.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dgvMenus.SelectedRows[0].DataBoundItem is MenuDiario menu)
            {

                var form = new MenuDetalleForm(menu);
                form.ShowDialog();

                CargarMenus();
            }
        }
    }
}
