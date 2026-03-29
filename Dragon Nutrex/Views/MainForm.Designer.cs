namespace Dragon_Nutrex.Views
{
    partial class MainForm
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
            btnUsuarios = new Button();
            btnProductos = new Button();
            btnMenús = new Button();
            btnIMC = new Button();
            btnRequerimientos = new Button();
            btnConsumoVsMeta = new Button();
            btnEstadisticasPorRango = new Button();
            SuspendLayout();
            // 
            // btnUsuarios
            // 
            btnUsuarios.Location = new Point(105, 305);
            btnUsuarios.Name = "btnUsuarios";
            btnUsuarios.Size = new Size(75, 23);
            btnUsuarios.TabIndex = 0;
            btnUsuarios.Text = "Usuarios";
            btnUsuarios.UseVisualStyleBackColor = true;
            btnUsuarios.Click += btnUsuarios_Click;
            // 
            // btnProductos
            // 
            btnProductos.Location = new Point(229, 305);
            btnProductos.Name = "btnProductos";
            btnProductos.Size = new Size(75, 23);
            btnProductos.TabIndex = 1;
            btnProductos.Text = "Productos";
            btnProductos.UseVisualStyleBackColor = true;
            btnProductos.Click += btnProductos_Click;
            // 
            // btnMenús
            // 
            btnMenús.Location = new Point(357, 305);
            btnMenús.Name = "btnMenús";
            btnMenús.Size = new Size(75, 23);
            btnMenús.TabIndex = 2;
            btnMenús.Text = "Menús";
            btnMenús.UseVisualStyleBackColor = true;
            btnMenús.Click += btnMenús_Click;
            // 
            // btnIMC
            // 
            btnIMC.Location = new Point(473, 305);
            btnIMC.Name = "btnIMC";
            btnIMC.Size = new Size(75, 23);
            btnIMC.TabIndex = 3;
            btnIMC.Text = "IMC";
            btnIMC.UseVisualStyleBackColor = true;
            btnIMC.Click += btnIMC_Click;
            // 
            // btnRequerimientos
            // 
            btnRequerimientos.Location = new Point(589, 305);
            btnRequerimientos.Name = "btnRequerimientos";
            btnRequerimientos.Size = new Size(107, 23);
            btnRequerimientos.TabIndex = 4;
            btnRequerimientos.Text = "Requerimientos";
            btnRequerimientos.UseVisualStyleBackColor = true;
            btnRequerimientos.Click += btnRequerimientos_Click;
            // 
            // btnConsumoVsMeta
            // 
            btnConsumoVsMeta.Location = new Point(229, 356);
            btnConsumoVsMeta.Name = "btnConsumoVsMeta";
            btnConsumoVsMeta.Size = new Size(139, 23);
            btnConsumoVsMeta.TabIndex = 5;
            btnConsumoVsMeta.Text = "Consumo Vs Meta";
            btnConsumoVsMeta.UseVisualStyleBackColor = true;
            btnConsumoVsMeta.Click += btnConsumoVsMeta_Click;
            // 
            // btnEstadisticasPorRango
            // 
            btnEstadisticasPorRango.Location = new Point(409, 356);
            btnEstadisticasPorRango.Name = "btnEstadisticasPorRango";
            btnEstadisticasPorRango.Size = new Size(139, 23);
            btnEstadisticasPorRango.TabIndex = 6;
            btnEstadisticasPorRango.Text = "Estadísticas por rango";
            btnEstadisticasPorRango.UseVisualStyleBackColor = true;
            btnEstadisticasPorRango.Click += btnEstadisticasPorRango_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnEstadisticasPorRango);
            Controls.Add(btnConsumoVsMeta);
            Controls.Add(btnRequerimientos);
            Controls.Add(btnIMC);
            Controls.Add(btnMenús);
            Controls.Add(btnProductos);
            Controls.Add(btnUsuarios);
            Name = "MainForm";
            Text = "MainForm";
            ResumeLayout(false);
        }

        #endregion

        private Button btnUsuarios;
        private Button btnProductos;
        private Button btnMenús;
        private Button btnIMC;
        private Button btnRequerimientos;
        private Button btnConsumoVsMeta;
        private Button btnEstadisticasPorRango;
    }
}