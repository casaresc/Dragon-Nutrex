namespace Dragon_Nutrex.Views
{
    partial class ProductosForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            dgvProductos = new DataGridView();
            lblNombre = new Label();
            lblCategoría = new Label();
            lblCalorias = new Label();
            lblProteina = new Label();
            lblCarbohidratos = new Label();
            lblGrasas = new Label();
            lblPorcion = new Label();
            nudCalorias = new NumericUpDown();
            nudProteina = new NumericUpDown();
            nudCarbohidratos = new NumericUpDown();
            nudGrasas = new NumericUpDown();
            nudPorcion = new NumericUpDown();
            txtNombre = new TextBox();
            cmbCategoria = new ComboBox();
            btnAgregar = new Button();
            btnEditar = new Button();
            btnEliminar = new Button();
            btnLimpiar = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvProductos).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudCalorias).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudProteina).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudCarbohidratos).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudGrasas).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudPorcion).BeginInit();
            SuspendLayout();
            // 
            // dgvProductos
            // 
            dgvProductos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvProductos.Dock = DockStyle.Top;
            dgvProductos.Location = new Point(0, 0);
            dgvProductos.MultiSelect = false;
            dgvProductos.Name = "dgvProductos";
            dgvProductos.ReadOnly = true;
            dgvProductos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProductos.Size = new Size(800, 250);
            dgvProductos.TabIndex = 0;
            dgvProductos.CellClick += dgvProductos_CellClick;
            // 
            // lblNombre
            // 
            lblNombre.AutoSize = true;
            lblNombre.Location = new Point(12, 261);
            lblNombre.Name = "lblNombre";
            lblNombre.Size = new Size(51, 15);
            lblNombre.TabIndex = 1;
            lblNombre.Text = "Nombre";
            // 
            // lblCategoría
            // 
            lblCategoría.AutoSize = true;
            lblCategoría.Location = new Point(169, 261);
            lblCategoría.Name = "lblCategoría";
            lblCategoría.Size = new Size(58, 15);
            lblCategoría.TabIndex = 2;
            lblCategoría.Text = "Categoría";
            // 
            // lblCalorias
            // 
            lblCalorias.AutoSize = true;
            lblCalorias.Location = new Point(327, 261);
            lblCalorias.Name = "lblCalorias";
            lblCalorias.Size = new Size(49, 15);
            lblCalorias.TabIndex = 3;
            lblCalorias.Text = "Calorias";
            // 
            // lblProteina
            // 
            lblProteina.AutoSize = true;
            lblProteina.Location = new Point(477, 261);
            lblProteina.Name = "lblProteina";
            lblProteina.Size = new Size(51, 15);
            lblProteina.TabIndex = 4;
            lblProteina.Text = "Proteina";
            // 
            // lblCarbohidratos
            // 
            lblCarbohidratos.AutoSize = true;
            lblCarbohidratos.Location = new Point(634, 261);
            lblCarbohidratos.Name = "lblCarbohidratos";
            lblCarbohidratos.Size = new Size(82, 15);
            lblCarbohidratos.TabIndex = 5;
            lblCarbohidratos.Text = "Carbohidratos";
            // 
            // lblGrasas
            // 
            lblGrasas.AutoSize = true;
            lblGrasas.Location = new Point(12, 318);
            lblGrasas.Name = "lblGrasas";
            lblGrasas.Size = new Size(41, 15);
            lblGrasas.TabIndex = 6;
            lblGrasas.Text = "Grasas";
            // 
            // lblPorcion
            // 
            lblPorcion.AutoSize = true;
            lblPorcion.Location = new Point(169, 318);
            lblPorcion.Name = "lblPorcion";
            lblPorcion.Size = new Size(48, 15);
            lblPorcion.TabIndex = 7;
            lblPorcion.Text = "Porcion";
            // 
            // nudCalorias
            // 
            nudCalorias.DecimalPlaces = 2;
            nudCalorias.Location = new Point(327, 279);
            nudCalorias.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            nudCalorias.Name = "nudCalorias";
            nudCalorias.Size = new Size(120, 23);
            nudCalorias.TabIndex = 10;
            // 
            // nudProteina
            // 
            nudProteina.DecimalPlaces = 2;
            nudProteina.Location = new Point(477, 279);
            nudProteina.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            nudProteina.Name = "nudProteina";
            nudProteina.Size = new Size(120, 23);
            nudProteina.TabIndex = 11;
            // 
            // nudCarbohidratos
            // 
            nudCarbohidratos.DecimalPlaces = 2;
            nudCarbohidratos.Location = new Point(634, 279);
            nudCarbohidratos.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            nudCarbohidratos.Name = "nudCarbohidratos";
            nudCarbohidratos.Size = new Size(120, 23);
            nudCarbohidratos.TabIndex = 12;
            // 
            // nudGrasas
            // 
            nudGrasas.DecimalPlaces = 2;
            nudGrasas.Location = new Point(12, 336);
            nudGrasas.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            nudGrasas.Name = "nudGrasas";
            nudGrasas.Size = new Size(120, 23);
            nudGrasas.TabIndex = 13;
            // 
            // nudPorcion
            // 
            nudPorcion.DecimalPlaces = 2;
            nudPorcion.Location = new Point(169, 336);
            nudPorcion.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            nudPorcion.Name = "nudPorcion";
            nudPorcion.Size = new Size(120, 23);
            nudPorcion.TabIndex = 14;
            // 
            // txtNombre
            // 
            txtNombre.Location = new Point(12, 278);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(100, 23);
            txtNombre.TabIndex = 15;
            // 
            // cmbCategoria
            // 
            cmbCategoria.FormattingEnabled = true;
            cmbCategoria.Location = new Point(169, 279);
            cmbCategoria.Name = "cmbCategoria";
            cmbCategoria.Size = new Size(121, 23);
            cmbCategoria.TabIndex = 16;
            // 
            // btnAgregar
            // 
            btnAgregar.Location = new Point(142, 403);
            btnAgregar.Name = "btnAgregar";
            btnAgregar.Size = new Size(75, 23);
            btnAgregar.TabIndex = 17;
            btnAgregar.Text = "Agregar";
            btnAgregar.UseVisualStyleBackColor = true;
            btnAgregar.Click += btnAgregar_Click;
            // 
            // btnEditar
            // 
            btnEditar.Location = new Point(269, 403);
            btnEditar.Name = "btnEditar";
            btnEditar.Size = new Size(75, 23);
            btnEditar.TabIndex = 18;
            btnEditar.Text = "Editar";
            btnEditar.UseVisualStyleBackColor = true;
            // 
            // btnEliminar
            // 
            btnEliminar.Location = new Point(391, 403);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(75, 23);
            btnEliminar.TabIndex = 19;
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseVisualStyleBackColor = true;
            btnEliminar.Click += btnEliminar_Click;
            // 
            // btnLimpiar
            // 
            btnLimpiar.Location = new Point(522, 403);
            btnLimpiar.Name = "btnLimpiar";
            btnLimpiar.Size = new Size(75, 23);
            btnLimpiar.TabIndex = 20;
            btnLimpiar.Text = "Limpiar";
            btnLimpiar.UseVisualStyleBackColor = true;
            // 
            // ProductosForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnLimpiar);
            Controls.Add(btnEliminar);
            Controls.Add(btnEditar);
            Controls.Add(btnAgregar);
            Controls.Add(cmbCategoria);
            Controls.Add(txtNombre);
            Controls.Add(nudPorcion);
            Controls.Add(nudGrasas);
            Controls.Add(nudCarbohidratos);
            Controls.Add(nudProteina);
            Controls.Add(nudCalorias);
            Controls.Add(lblPorcion);
            Controls.Add(lblGrasas);
            Controls.Add(lblCarbohidratos);
            Controls.Add(lblProteina);
            Controls.Add(lblCalorias);
            Controls.Add(lblCategoría);
            Controls.Add(lblNombre);
            Controls.Add(dgvProductos);
            Name = "ProductosForm";
            Text = "FormProductos";
            ((System.ComponentModel.ISupportInitialize)dgvProductos).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudCalorias).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudProteina).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudCarbohidratos).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudGrasas).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudPorcion).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvProductos;
        private Label lblNombre;
        private Label lblCategoría;
        private Label lblCalorias;
        private Label lblProteina;
        private Label lblCarbohidratos;
        private Label lblGrasas;
        private Label lblPorcion;
        private NumericUpDown nudCalorias;
        private NumericUpDown nudProteina;
        private NumericUpDown nudCarbohidratos;
        private NumericUpDown nudGrasas;
        private NumericUpDown nudPorcion;
        private TextBox txtNombre;
        private ComboBox cmbCategoria;
        private Button btnAgregar;
        private Button btnEditar;
        private Button btnEliminar;
        private Button btnLimpiar;
    }
}