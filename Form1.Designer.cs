using System.Windows.Forms;
using LiveCharts.WinForms;

namespace MonitoreoTemperaturaLab
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblTemperatura = new System.Windows.Forms.Label();
            this.pgbTemperatura = new System.Windows.Forms.ProgressBar();
            this.pnlAlerta = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.lblEstado = new System.Windows.Forms.Label();
            this.btnIniciar = new System.Windows.Forms.Button();
            this.btnFinalizar = new System.Windows.Forms.Button();
            this.btnExportar = new System.Windows.Forms.Button();
            this.formsPlot1 = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // lblTemperatura
            // 
            this.lblTemperatura.AutoSize = true;
            this.lblTemperatura.Font = new System.Drawing.Font("Modern No. 20", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTemperatura.Location = new System.Drawing.Point(463, 417);
            this.lblTemperatura.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTemperatura.Name = "lblTemperatura";
            this.lblTemperatura.Size = new System.Drawing.Size(55, 24);
            this.lblTemperatura.TabIndex = 0;
            this.lblTemperatura.Text = "0.0 °C";
            this.lblTemperatura.Click += new System.EventHandler(this.label1_Click);
            // 
            // pgbTemperatura
            // 
            this.pgbTemperatura.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.pgbTemperatura.Location = new System.Drawing.Point(274, 60);
            this.pgbTemperatura.Margin = new System.Windows.Forms.Padding(2);
            this.pgbTemperatura.Name = "pgbTemperatura";
            this.pgbTemperatura.Size = new System.Drawing.Size(200, 54);
            this.pgbTemperatura.TabIndex = 1;
            this.pgbTemperatura.Click += new System.EventHandler(this.pgbTemperatura_Click);
            // 
            // pnlAlerta
            // 
            this.pnlAlerta.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.pnlAlerta.Location = new System.Drawing.Point(366, 407);
            this.pnlAlerta.Margin = new System.Windows.Forms.Padding(2);
            this.pnlAlerta.Name = "pnlAlerta";
            this.pnlAlerta.Size = new System.Drawing.Size(64, 64);
            this.pnlAlerta.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(100, 9);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(354, 26);
            this.label1.TabIndex = 3;
            this.label1.Text = "MONITOREO DE TEMPERATURA";
            // 
            // lblEstado
            // 
            this.lblEstado.AutoSize = true;
            this.lblEstado.Location = new System.Drawing.Point(349, 126);
            this.lblEstado.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Size = new System.Drawing.Size(35, 13);
            this.lblEstado.TabIndex = 4;
            this.lblEstado.Text = "label2";
            this.lblEstado.Click += new System.EventHandler(this.lblEstado_Click);
            // 
            // btnIniciar
            // 
            this.btnIniciar.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnIniciar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnIniciar.ForeColor = System.Drawing.SystemColors.Desktop;
            this.btnIniciar.Location = new System.Drawing.Point(23, 60);
            this.btnIniciar.Margin = new System.Windows.Forms.Padding(2);
            this.btnIniciar.Name = "btnIniciar";
            this.btnIniciar.Size = new System.Drawing.Size(146, 54);
            this.btnIniciar.TabIndex = 5;
            this.btnIniciar.Text = "INICIAR";
            this.btnIniciar.UseVisualStyleBackColor = false;
            this.btnIniciar.Click += new System.EventHandler(this.btnIniciar_Click_1);
            // 
            // btnFinalizar
            // 
            this.btnFinalizar.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.btnFinalizar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnFinalizar.Location = new System.Drawing.Point(23, 418);
            this.btnFinalizar.Margin = new System.Windows.Forms.Padding(2);
            this.btnFinalizar.Name = "btnFinalizar";
            this.btnFinalizar.Size = new System.Drawing.Size(146, 53);
            this.btnFinalizar.TabIndex = 6;
            this.btnFinalizar.Text = "DETENER";
            this.btnFinalizar.UseVisualStyleBackColor = false;
            this.btnFinalizar.Click += new System.EventHandler(this.btnFinalizar_Click);
            // 
            // btnExportar
            // 
            this.btnExportar.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.btnExportar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnExportar.Location = new System.Drawing.Point(185, 418);
            this.btnExportar.Margin = new System.Windows.Forms.Padding(2);
            this.btnExportar.Name = "btnExportar";
            this.btnExportar.Size = new System.Drawing.Size(146, 53);
            this.btnExportar.TabIndex = 8;
            this.btnExportar.Text = "EXPORTAR";
            this.btnExportar.UseVisualStyleBackColor = false;
            this.btnExportar.Click += new System.EventHandler(this.btnExportar_Click);
            // 
            // formsPlot1
            // 
            this.formsPlot1.Location = new System.Drawing.Point(23, 142);
            this.formsPlot1.Name = "formsPlot1";
            this.formsPlot1.Size = new System.Drawing.Size(553, 259);
            this.formsPlot1.TabIndex = 7;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 497);
            this.Controls.Add(this.btnFinalizar);
            this.Controls.Add(this.btnExportar);
            this.Controls.Add(this.formsPlot1);
            this.Controls.Add(this.btnIniciar);
            this.Controls.Add(this.lblEstado);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pnlAlerta);
            this.Controls.Add(this.pgbTemperatura);
            this.Controls.Add(this.lblTemperatura);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTemperatura;
        private System.Windows.Forms.ProgressBar pgbTemperatura;
        private System.Windows.Forms.Panel pnlAlerta;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblEstado;
        private Button btnIniciar;
        private Button btnFinalizar;
        private Button btnExportar;
        private System.Windows.Forms.Panel formsPlot1;
    }
}

