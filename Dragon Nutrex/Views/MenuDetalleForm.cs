using Dragon_Nutrex.Controllers;
using Dragon_Nutrex.Models;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Dragon_Nutrex.Views
{
    public partial class MenuDetalleForm : Form
    {
        private readonly MenuDiario _menu;
        private readonly MenuController _controller = new MenuController();

        public MenuDetalleForm(MenuDiario menu)
        {
            InitializeComponent();
            _menu = menu ?? throw new ArgumentNullException(nameof(menu));
        }

        private void MenuDetalleForm_Load(object sender, EventArgs e)
        {
            lblMenuNombre.Text = $"Editando: {_menu.Nombre}";
            ConfigurarGrid();
            CargarCatalogos();
            CargarDetallesActuales();
        }

        private void ConfigurarGrid()
        {
            dgvProductosMenu.AutoGenerateColumns = true;
            dgvProductosMenu.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProductosMenu.ReadOnly = true;
        }

        private void CargarCatalogos()
        {
            cmbProductos.DataSource = _controller.ObtenerCatalogoProductos();
            cmbProductos.DisplayMember = "Nombre";
            cmbProductos.ValueMember = "Id";
            cmbProductos.SelectedIndex = -1;
        }

        private void CargarDetallesActuales()
        {
            dgvProductosMenu.DataSource = null;
            dgvProductosMenu.DataSource = _controller.ObtenerDetallesDelMenu(_menu.Id);
        }

        private void btnAgregarProducto_Click(object sender, EventArgs e)
        {
            if (cmbProductos.SelectedValue == null)
            {
                MessageBox.Show("Seleccione un alimento de la lista.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(txtPorcion.Text, out decimal porcion) || porcion <= 0)
            {
                MessageBox.Show("Ingrese una cantidad/porción válida mayor a cero.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var nuevoDetalle = new MenuDetalle
            {
                Id = Guid.NewGuid(),
                MenuId = _menu.Id,
                ProductoId = (Guid)cmbProductos.SelectedValue,
                Porcion = porcion 
            };

            _controller.AgregarAlimentoAlMenu(nuevoDetalle);
            CargarDetallesActuales();
            txtPorcion.Clear();
        }

        private void btnEliminarProducto_Click(object sender, EventArgs e)
        {
            if (dgvProductosMenu.CurrentRow?.DataBoundItem is MenuDetalle detalle)
            {
                var confirm = MessageBox.Show("¿Quitar este alimento del menú?", "Confirmar",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (confirm == DialogResult.Yes)
                {
                    _controller.QuitarAlimentoDelMenu(detalle.Id);
                    CargarDetallesActuales();
                }
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e) => this.Close();
    }
}