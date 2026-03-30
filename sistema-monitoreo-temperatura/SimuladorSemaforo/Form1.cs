using SimuladorSemaforo.Controlador;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace SimuladorSemaforo
{
    public partial class Form1 : Form
    {
        private readonly SemaforoController _controlador = new SemaforoController();

        public Form1()
        {
            InitializeComponent();
            _controlador.AlCambiarColor = CambiarColor;

            var panels = new[] { panelRojo, panelAmarillo, panelVerde };
            foreach (var p in panels)
            {
                if (p == null) continue;
                HacerRedondo(p);
                p.Resize += Panel_Resize;
            }
        }

        private void Panel_Resize(object? sender, System.EventArgs e)
        {
            if (sender is Panel p) HacerRedondo(p);
        }

        private void HacerRedondo(Panel panel)
        {
            if (panel?.Width <= 0 || panel.Height <= 0) return;
            using var forma = new GraphicsPath();
            forma.AddEllipse(0, 0, panel.Width, panel.Height);
            panel.Region?.Dispose();
            panel.Region = new Region(forma);
        }

        private void CambiarColor(string color)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string>(CambiarColor), color);
                return;
            }

            panelRojo.BackColor = panelAmarillo.BackColor = panelVerde.BackColor = Color.Gray;

            switch (color)
            {
                case "rojo": panelRojo.BackColor = Color.Red; break;
                case "amarillo": panelAmarillo.BackColor = Color.Yellow; break;
                case "verde": panelVerde.BackColor = Color.Green; break;
            }
        }

        private void btnIniciar_Click(object sender, System.EventArgs e) => _controlador.Iniciar();
        private void btnDetener_Click(object sender, System.EventArgs e) => _controlador.Detener();
    }
}