using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MonitoreoTemp.Models; // Para que reconozca SensorData
using MonitoreoTemp.Controllers; // Para que reconozca el Controlador

namespace MonitoreoTemp.Views
{
    public partial class Form1 : Form
    {
        // 1. Instanciamos el controlador (el motor)
        private TemperatureController _controller = new TemperatureController();

        public Form1()
        {
            InitializeComponent();

            // CONEXIÓN DEL MOTOR:
            // Le decimos al controlador: "Cuando cambies la temp, avísale a mi método AlRecibirNuevaTemperatura"
            _controller.TempChanged += AlRecibirNuevaTemperatura;

            // Arrancamos los hilos
            _controller.IniciarSimulacion();
        }

        // EL PUENTE SEGURO (Para evitar que truene por los hilos)
        private void AlRecibirNuevaTemperatura(SensorData data)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => AlRecibirNuevaTemperatura(data)));
                return;
            }

            // --- AQUÍ ES DONDE ELLOS ESCRIBIRÁN SU CÓDIGO ---
        }
    }
}
