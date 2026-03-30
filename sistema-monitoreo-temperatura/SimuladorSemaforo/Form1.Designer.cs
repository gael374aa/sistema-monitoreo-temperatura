namespace SimuladorSemaforo
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            panelRojo = new Panel();
            pictureBox1 = new PictureBox();
            panelAmarillo = new Panel();
            panelVerde = new Panel();
            btnIniciar = new Button();
            btnDetener = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // panelRojo
            // 
            panelRojo.Location = new Point(58, 46);
            panelRojo.Name = "panelRojo";
            panelRojo.Size = new Size(98, 89);
            panelRojo.TabIndex = 0;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = SystemColors.ActiveCaptionText;
            pictureBox1.Location = new Point(33, 25);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(148, 351);
            pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
            pictureBox1.TabIndex = 7;
            pictureBox1.TabStop = false;
          
            // 
            // panelAmarillo
            // 
            panelAmarillo.Location = new Point(58, 158);
            panelAmarillo.Name = "panelAmarillo";
            panelAmarillo.Size = new Size(98, 89);
            panelAmarillo.TabIndex = 1;
            // 
            // panelVerde
            // 
            panelVerde.Location = new Point(58, 274);
            panelVerde.Name = "panelVerde";
            panelVerde.Size = new Size(98, 89);
            panelVerde.TabIndex = 2;
            // 
            // btnIniciar
            // 
            btnIniciar.Location = new Point(251, 117);
            btnIniciar.Name = "btnIniciar";
            btnIniciar.Size = new Size(117, 35);
            btnIniciar.TabIndex = 5;
            btnIniciar.Text = "Iniciar";
            btnIniciar.UseVisualStyleBackColor = true;
            btnIniciar.Click += btnIniciar_Click;
            // 
            // btnDetener
            // 
            btnDetener.Location = new Point(251, 212);
            btnDetener.Name = "btnDetener";
            btnDetener.Size = new Size(117, 35);
            btnDetener.TabIndex = 6;
            btnDetener.Text = "Detener";
            btnDetener.UseVisualStyleBackColor = true;
            btnDetener.Click += btnDetener_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(395, 399);
            Controls.Add(panelRojo);
            Controls.Add(btnDetener);
            Controls.Add(btnIniciar);
            Controls.Add(panelVerde);
            Controls.Add(panelAmarillo);
            Controls.Add(pictureBox1);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panelRojo;
        private Panel panelAmarillo;
        private Panel panelVerde;
        private Button btnIniciar;
        private Button btnDetener;
        private PictureBox pictureBox1;
    }
}
