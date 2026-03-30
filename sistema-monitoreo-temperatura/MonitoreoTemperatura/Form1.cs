using System;
using System.Windows.Forms;
using MonitoreoTemperaturaLab.Models;
using MonitoreoTemperaturaLab.Controllers;

namespace MonitoreoTemperaturaLab
{
    public partial class Form1 : Form
    {
        private TemperatureController _controller = new TemperatureController();

        public Form1()
        {
            InitializeComponent();

            // Conectar el evento del controlador
            _controller.TempChanged += AlRecibirNuevaTemperatura;

            // Iniciar el simulador
            _controller.IniciarSimulacion();
        }

        private void AlRecibirNuevaTemperatura(SensorData data)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => AlRecibirNuevaTemperatura(data)));
                return;
            }

            // --- ZONA DE TRABAJO PARA TUS COMPAÑEROS ---
            // Aquí ellos podrán escribir para actualizar Labels, Gráficas, etc.
        }
    }
}