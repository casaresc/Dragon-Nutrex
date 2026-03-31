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
            this.Load += MenuDetalleForm_Load;
        }

        private void MenuDetalleForm_Load(object? sender, EventArgs e)
        {
            lblMenuNombre.Text = $"Menu seleccionado: {_menu.Nombre}";
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
            var detalles = _controller.ObtenerDetallesDelMenu(_menu.Id);


            var catalogo = _controller.ObtenerCatalogoProductos();

            var vistaDetalles = detalles.Select(d => {
                var producto = catalogo.Find(p => p.Id == d.ProductoId);
                return new
                {
                    d.Id,
                    Alimento = producto?.Nombre ?? "Desconocido",
                    Cantidad = d.Porcion,
                    Calorias = producto != null ? (producto.Calorias * d.Porcion / 100) : 0,
                    Carbos = producto != null ? (producto.Carbohidratos * d.Porcion / 100) : 0
                };
            }).ToList();

            dgvProductosMenu.DataSource = null;
            dgvProductosMenu.DataSource = vistaDetalles;

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
            if (dgvProductosMenu.CurrentRow?.Cells["Id"]?.Value is Guid idDetalle)
            {
                var confirm = MessageBox.Show("¿Quitar este alimento del menú?", "Confirmar",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (confirm == DialogResult.Yes)
                {
                    _controller.QuitarAlimentoDelMenu(idDetalle);
                    CargarDetallesActuales();
                }
            }
            else
            {
                MessageBox.Show("Por favor, seleccione un producto válido de la lista.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e) => this.Close();
    }
}