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
        private readonly UsuarioController? _usuarioController = new UsuarioController();

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
            if (cmbUsuarios.Items.Count == 0)
            {
                ConfigurarComboUsuarios();
            }

            FiltrarMenusPorUsuario();

            this.ControlBox = false;
            this.FormBorderStyle = FormBorderStyle.None;
            this.Dock = DockStyle.Fill;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("El nombre del menú es requerido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbUsuarios.SelectedValue == null)
            {
                MessageBox.Show("Debe seleccionar un usuario para asignar el menú.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var nuevoMenu = new MenuDiario
            {
                Id = Guid.NewGuid(),
                Nombre = txtNombre.Text.Trim(),
                Fecha = dtpFecha.Value.Date,
                UsuarioId = (Guid)cmbUsuarios.SelectedValue
            };

            bool exito  = _controller?.GuardarNuevoMenu(nuevoMenu) ?? false;
            if (exito)
            {
                CargarMenus();
                LimpiarFormulario();
                MessageBox.Show("Menú creado correctamente. Ahora puede agregar productos.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
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

            bool exito = _controller?.ModificarMenu(_menuSeleccionado) ?? false;
            if (exito)
            {
                CargarMenus();
                LimpiarFormulario();
                MessageBox.Show("Menú actualizado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
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
            cmbUsuarios.SelectedIndex = -1;
            _menuSeleccionado = null;
            dgvMenus.ClearSelection();
        }

        private void btnLimpiar_Click(object sender, EventArgs e) => LimpiarFormulario();

        private void ConfigurarComboUsuarios()
        {
            var usuarios = _usuarioController?.GetUsuarios();
            cmbUsuarios.DataSource = usuarios;
            cmbUsuarios.DisplayMember = "Nombre";
            cmbUsuarios.ValueMember = "Id";
            cmbUsuarios.SelectedIndex = -1;
        }

        private void cmbUsuarios_SelectedIndexChanged(object sender, EventArgs e)
        {
            FiltrarMenusPorUsuario();
        }

        private void FiltrarMenusPorUsuario()
        {
            if (cmbUsuarios.SelectedValue is Guid usuarioId)
            {
                var todosLosMenus = _controller?.ObtenerTodosLosMenus();

                var menusFiltrados = todosLosMenus?.Where(m => m.UsuarioId == usuarioId).ToList();

                dgvMenus.DataSource = null;
                dgvMenus.DataSource = menusFiltrados;
            }
            else
            {
                dgvMenus.DataSource = null;
            }
        }
    }
}