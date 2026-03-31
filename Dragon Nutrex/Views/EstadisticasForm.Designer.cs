namespace Dragon_Nutrex.Views
{
    partial class EstadisticasForm
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
            panel1 = new Panel();
            btnRefrescar = new Button();
            label1 = new Label();
            cmbUsuarios = new ComboBox();
            tcEstadisticas = new TabControl();
            tpResumen = new TabPage();
            tpHistorico = new TabPage();
            tpRequerimientos = new TabPage();
            tpImc = new TabPage();
            panel1.SuspendLayout();
            tcEstadisticas.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(btnRefrescar);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(cmbUsuarios);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(800, 60);
            panel1.TabIndex = 0;
            // 
            // btnRefrescar
            // 
            btnRefrescar.Location = new Point(713, 24);
            btnRefrescar.Name = "btnRefrescar";
            btnRefrescar.Size = new Size(75, 23);
            btnRefrescar.TabIndex = 2;
            btnRefrescar.Text = "Refrescar";
            btnRefrescar.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(3, 6);
            label1.Name = "label1";
            label1.Size = new Size(52, 15);
            label1.TabIndex = 1;
            label1.Text = "Usuarios";
            // 
            // cmbUsuarios
            // 
            cmbUsuarios.FormattingEnabled = true;
            cmbUsuarios.Location = new Point(3, 24);
            cmbUsuarios.Name = "cmbUsuarios";
            cmbUsuarios.Size = new Size(213, 23);
            cmbUsuarios.TabIndex = 0;
            cmbUsuarios.SelectedIndexChanged += cmbUsuarios_SelectedIndexChanged;
            // 
            // tcEstadisticas
            // 
            tcEstadisticas.Controls.Add(tpResumen);
            tcEstadisticas.Controls.Add(tpHistorico);
            tcEstadisticas.Controls.Add(tpRequerimientos);
            tcEstadisticas.Controls.Add(tpImc);
            tcEstadisticas.Dock = DockStyle.Fill;
            tcEstadisticas.Location = new Point(0, 60);
            tcEstadisticas.Name = "tcEstadisticas";
            tcEstadisticas.SelectedIndex = 0;
            tcEstadisticas.Size = new Size(800, 390);
            tcEstadisticas.TabIndex = 1;
            // 
            // tpResumen
            // 
            tpResumen.Location = new Point(4, 24);
            tpResumen.Name = "tpResumen";
            tpResumen.Padding = new Padding(3);
            tpResumen.Size = new Size(792, 362);
            tpResumen.TabIndex = 0;
            tpResumen.Text = "Resumen";
            tpResumen.UseVisualStyleBackColor = true;
            // 
            // tpHistorico
            // 
            tpHistorico.Location = new Point(4, 24);
            tpHistorico.Name = "tpHistorico";
            tpHistorico.Padding = new Padding(3);
            tpHistorico.Size = new Size(792, 362);
            tpHistorico.TabIndex = 1;
            tpHistorico.Text = "Historico";
            tpHistorico.UseVisualStyleBackColor = true;
            // 
            // tpRequerimientos
            // 
            tpRequerimientos.Location = new Point(4, 24);
            tpRequerimientos.Name = "tpRequerimientos";
            tpRequerimientos.Padding = new Padding(3);
            tpRequerimientos.Size = new Size(792, 362);
            tpRequerimientos.TabIndex = 2;
            tpRequerimientos.Text = "Requerimientos";
            tpRequerimientos.UseVisualStyleBackColor = true;
            // 
            // tpImc
            // 
            tpImc.Location = new Point(4, 24);
            tpImc.Name = "tpImc";
            tpImc.Padding = new Padding(3);
            tpImc.Size = new Size(792, 362);
            tpImc.TabIndex = 3;
            tpImc.Text = "IMC";
            tpImc.UseVisualStyleBackColor = true;
            // 
            // EstadisticasForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(tcEstadisticas);
            Controls.Add(panel1);
            Name = "EstadisticasForm";
            Text = "EstadísticasForm";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            tcEstadisticas.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Label label1;
        private ComboBox cmbUsuarios;
        private Button btnRefrescar;
        private TabControl tcEstadisticas;
        private TabPage tpResumen;
        private TabPage tpHistorico;
        private TabPage tpRequerimientos;
        private TabPage tpImc;
    }
}