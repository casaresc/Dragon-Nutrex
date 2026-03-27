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
    public partial class MenuDetalleForm : Form
    {
        private readonly MenuDiario _menu;

        private readonly MenuDetalleService
            _detalleService;

        private readonly ProductoService
            _productoService;

        public MenuDetalleForm(
            MenuDiario menu)
        {
            InitializeComponent();

            _menu = menu
                ?? throw new ArgumentNullException(
                    nameof(menu),
                    "El menú no puede ser null.");

            _detalleService =
                new MenuDetalleService();

            _productoService =
                new ProductoService();

            this.Load += MenuDetalleForm_Load;
        }

        private void MenuDetalleForm_Load(object sender,
    EventArgs e)
        {
            lblMenuNombre.Text =
                $"Menú: {_menu.Nombre}";

            CargarProductos();

            CargarDetalles();
        }

        private void CargarProductos()
        {
            var productos =
                _productoService
                .ObtenerProductos();

            cmbProductos.DataSource =
                productos;

            cmbProductos.DisplayMember =
                "Nombre";

            cmbProductos.ValueMember =
                "Id";
        }

        private void CargarDetalles()
        {
            var detalles =
                _detalleService
                .ObtenerPorMenu(
                    _menu.Id);

            dgvProductosMenu.DataSource =
                detalles;
        }

        private void btnAgregarProducto_Click(
    object sender,
    EventArgs e)
        {
            if (cmbProductos.SelectedItem
                == null)
                throw new Exception(
                    "Seleccione un producto.");

            if (!decimal.TryParse(
                txtPorcion.Text,
                out decimal porcion))
                throw new Exception(
                    "Porción inválida.");

            var detalle =
                new MenuDetalle
                {
                    MenuId = _menu.Id,

                    ProductoId =
                        (Guid)cmbProductos
                        .SelectedValue,

                    Porcion = porcion
                };

            _detalleService
                .AgregarProducto(detalle);

            CargarDetalles();

            txtPorcion.Clear();
        }

        private void btnEliminarProducto_Click(
    object sender,
    EventArgs e)
        {
            if (dgvProductosMenu
                .SelectedRows.Count == 0)
                throw new Exception(
                    "Seleccione un producto.");

            var detalle =
                (MenuDetalle)
                dgvProductosMenu
                .SelectedRows[0]
                .DataBoundItem;

            var confirmacion =
                MessageBox.Show(
                    "¿Eliminar producto del menú?",
                    "Confirmar",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

            if (confirmacion
                == DialogResult.Yes)
            {
                _detalleService
                    .EliminarProducto(
                        detalle.Id);

                CargarDetalles();
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
