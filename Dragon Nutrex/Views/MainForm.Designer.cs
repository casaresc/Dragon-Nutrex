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
            btnGenerarDatos = new Button();
            panel1 = new Panel();
            btnMenús = new Button();
            btnProductos = new Button();
            btnEstadísticas = new Button();
            btnUsuarios = new Button();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // btnGenerarDatos
            // 
            btnGenerarDatos.Location = new Point(0, 427);
            btnGenerarDatos.Name = "btnGenerarDatos";
            btnGenerarDatos.Size = new Size(75, 23);
            btnGenerarDatos.TabIndex = 7;
            btnGenerarDatos.Text = "Generar Datos";
            btnGenerarDatos.UseVisualStyleBackColor = true;
            btnGenerarDatos.Click += btnGenerarDatos_Click;
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.ControlDarkDark;
            panel1.Controls.Add(btnMenús);
            panel1.Controls.Add(btnGenerarDatos);
            panel1.Controls.Add(btnProductos);
            panel1.Controls.Add(btnEstadísticas);
            panel1.Controls.Add(btnUsuarios);
            panel1.Dock = DockStyle.Left;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(200, 450);
            panel1.TabIndex = 9;
            // 
            // btnMenús
            // 
            btnMenús.Dock = DockStyle.Top;
            btnMenús.FlatStyle = FlatStyle.Flat;
            btnMenús.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnMenús.ForeColor = Color.White;
            btnMenús.Location = new Point(0, 150);
            btnMenús.MinimumSize = new Size(0, 50);
            btnMenús.Name = "btnMenús";
            btnMenús.Padding = new Padding(20, 0, 0, 0);
            btnMenús.Size = new Size(200, 50);
            btnMenús.TabIndex = 13;
            btnMenús.Text = "Menús";
            btnMenús.TextAlign = ContentAlignment.MiddleLeft;
            btnMenús.UseVisualStyleBackColor = true;
            btnMenús.Click += btnMenús_Click;
            // 
            // btnProductos
            // 
            btnProductos.Dock = DockStyle.Top;
            btnProductos.FlatStyle = FlatStyle.Flat;
            btnProductos.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnProductos.ForeColor = Color.White;
            btnProductos.Location = new Point(0, 100);
            btnProductos.MinimumSize = new Size(0, 50);
            btnProductos.Name = "btnProductos";
            btnProductos.Padding = new Padding(20, 0, 0, 0);
            btnProductos.Size = new Size(200, 50);
            btnProductos.TabIndex = 12;
            btnProductos.Text = "Productos";
            btnProductos.TextAlign = ContentAlignment.MiddleLeft;
            btnProductos.UseVisualStyleBackColor = true;
            btnProductos.Click += btnProductos_Click;
            // 
            // btnEstadísticas
            // 
            btnEstadísticas.Dock = DockStyle.Top;
            btnEstadísticas.FlatStyle = FlatStyle.Flat;
            btnEstadísticas.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnEstadísticas.ForeColor = Color.White;
            btnEstadísticas.Location = new Point(0, 50);
            btnEstadísticas.MinimumSize = new Size(0, 50);
            btnEstadísticas.Name = "btnEstadísticas";
            btnEstadísticas.Padding = new Padding(20, 0, 0, 0);
            btnEstadísticas.Size = new Size(200, 50);
            btnEstadísticas.TabIndex = 11;
            btnEstadísticas.Text = "Estadísticas";
            btnEstadísticas.TextAlign = ContentAlignment.MiddleLeft;
            btnEstadísticas.UseVisualStyleBackColor = true;
            btnEstadísticas.Click += btnEstadísticas_Click;
            // 
            // btnUsuarios
            // 
            btnUsuarios.Dock = DockStyle.Top;
            btnUsuarios.FlatStyle = FlatStyle.Flat;
            btnUsuarios.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnUsuarios.ForeColor = Color.White;
            btnUsuarios.Location = new Point(0, 0);
            btnUsuarios.MinimumSize = new Size(0, 50);
            btnUsuarios.Name = "btnUsuarios";
            btnUsuarios.Padding = new Padding(20, 0, 0, 0);
            btnUsuarios.Size = new Size(200, 50);
            btnUsuarios.TabIndex = 10;
            btnUsuarios.Text = "Usuarios";
            btnUsuarios.TextAlign = ContentAlignment.MiddleLeft;
            btnUsuarios.UseVisualStyleBackColor = true;
            btnUsuarios.Click += btnUsuarios_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(panel1);
            IsMdiContainer = true;
            Name = "MainForm";
            Text = "MainForm";
            WindowState = FormWindowState.Maximized;
            panel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion


        private Button btnGenerarDatos;
        private Panel panel1;
        private Button btnUsuarios;
        private Button btnEstadísticas;
        private Button btnProductos;
        internal Button btnMenús;
    }
}