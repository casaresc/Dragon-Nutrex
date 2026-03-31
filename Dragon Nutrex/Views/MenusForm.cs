using Dragon_Nutrex.Controllers;
using Dragon_Nutrex.Models;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Dragon_Nutrex.Views
{
    public partial class MenusForm : Form
    {
        private readonly MenuController? _controller = new MenuController();
        private MenuDiario? _menuSeleccionado = null;

        public MenusForm()
        {
            InitializeComponent();
            ConfigurarGrid();
            this.Load += MenusForm_Load;
        }

        private void MenusForm_Load(object? sender, EventArgs e) => CargarMenus();

        private void ConfigurarGrid()
        {
            dgvMenus.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvMenus.MultiSelect = false;
            dgvMenus.ReadOnly = true;
        }

        private void CargarMenus()
        {
            this.ControlBox = false;
            this.FormBorderStyle = FormBorderStyle.None;
            this.Dock = DockStyle.Fill;
            dgvMenus.DataSource = null;
            dgvMenus.DataSource = _controller?.ObtenerTodosLosMenus();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("El nombre del menú es requerido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var nuevoMenu = new MenuDiario
            {
                Id = Guid.NewGuid(),
                Nombre = txtNombre.Text.Trim(),
                Fecha = dtpFecha.Value.Date
            };

            _controller?.GuardarNuevoMenu(nuevoMenu);
            CargarMenus();
            LimpiarFormulario();
            MessageBox.Show("Menú creado correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (_menuSeleccionado == null)
            {
                MessageBox.Show("Seleccione un menú de la lista para editar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _menuSeleccionado.Nombre = txtNombre.Text.Trim();
            _menuSeleccionado.Fecha = dtpFecha.Value.Date;

            _controller?.ModificarMenu(_menuSeleccionado);
            CargarMenus();
            LimpiarFormulario();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (_menuSeleccionado == null) return;

            var confirm = MessageBox.Show("¿Desea eliminar este menú y todos sus productos?", "Confirmación",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                _controller?.BorrarMenu(_menuSeleccionado.Id);
                CargarMenus();
                LimpiarFormulario();
            }
        }

        private void dgvMenus_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvMenus.CurrentRow?.DataBoundItem is MenuDiario menu)
            {
                _menuSeleccionado = menu;
                txtNombre.Text = menu.Nombre;
                dtpFecha.Value = menu.Fecha;
            }
        }

        private void btnAgregarProductos_Click(object sender, EventArgs e)
        {
            if (_menuSeleccionado == null)
            {
                MessageBox.Show("Seleccione un menú para gestionar sus alimentos.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var formDetalle = new MenuDetalleForm(_menuSeleccionado);
            formDetalle.ShowDialog();
            CargarMenus();
        }

        private void LimpiarFormulario()
        {
            txtNombre.Clear();
            dtpFecha.Value = DateTime.Now;
            _menuSeleccionado = null;
            dgvMenus.ClearSelection();
        }

        private void btnLimpiar_Click(object sender, EventArgs e) => LimpiarFormulario();
    }
}