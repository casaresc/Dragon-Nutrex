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
    public partial class ProductosForm : Form
    {
        private readonly ProductoService _service;

        private Guid _productoSeleccionadoId = Guid.Empty;

        public ProductosForm()
        {
            InitializeComponent();
            _service = new ProductoService();
            ProductosForm_Load();
            
        }

        private void ProductosForm_Load()
        {
            CargarCategorias();
            CargarProductos();
        }

        private void CargarCategorias()
        {
            cmbCategoria.DataSource =
                Enum.GetValues(typeof(CategoriaProducto));
        }

        private void CargarProductos()
        {
            dgvProductos.DataSource = null;

            dgvProductos.DataSource =
                _service.ObtenerProductos();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            var producto = ObtenerProductoDesdeFormulario();

            _service.CrearProducto(producto);

            MessageBox.Show(
                "Producto creado correctamente",
                "Éxito",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
            CargarProductos();

            LimpiarFormulario();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
        }

        private void dgvProductos_CellClick(
            object sender,
            DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            var producto =
                (Producto)dgvProductos.Rows[e.RowIndex].DataBoundItem;

            _productoSeleccionadoId = producto.Id;

            txtNombre.Text = producto.Nombre;

            cmbCategoria.SelectedItem =
                producto.Categoria;

            nudCalorias.Value =
                producto.Calorias;

            nudProteina.Value =
                producto.Proteina;

            nudCarbohidratos.Value =
                producto.Carbohidratos;

            nudGrasas.Value =
                producto.Grasas;

            nudPorcion.Value =
                producto.PorcionGramos;
        }

        private Producto ObtenerProductoDesdeFormulario()
        {
            return new Producto
            {
                Nombre = txtNombre.Text.Trim(),

                Categoria =
                    (CategoriaProducto)cmbCategoria.SelectedItem,

                Calorias =
                    nudCalorias.Value,

                Proteina =
                    nudProteina.Value,

                Carbohidratos =
                    nudCarbohidratos.Value,

                Grasas =
                    nudGrasas.Value,

                PorcionGramos =
                    nudPorcion.Value
            };
        }

        private void LimpiarFormulario()
        {
            txtNombre.Clear();

            cmbCategoria.SelectedIndex = 0;

            nudCalorias.Value = 0;
            nudProteina.Value = 0;
            nudCarbohidratos.Value = 0;
            nudGrasas.Value = 0;
            nudPorcion.Value = 0;

            _productoSeleccionadoId = Guid.Empty;

            dgvProductos.ClearSelection();
        }
    }
}
