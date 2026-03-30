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

            // 2. Nos suscribimos al evento: "Avisame cuando cambie la temp"
            _controller.TempChanged += AlRecibirNuevaTemperatura;

            // 3. Arrancamos el motor
            _controller.IniciarSimulacion();
        }

        // 4. ESTA ES LA FUNCIÓN DEL PASO 4 (El puente seguro)
        private void AlRecibirNuevaTemperatura(SensorData data)
        {
            // ¿Estoy en el hilo equivocado?
            if (this.InvokeRequired)
            {
                // Si sí, me mando llamar a mí mismo pero en el hilo correcto
                this.Invoke(new Action(() => AlRecibirNuevaTemperatura(data)));
                return;
            }

            // --- AQUÍ TRABAJAN TUS COMPAÑEROS ---
            // Todo lo que escriban aquí abajo es SEGURO y no romperá el programa

           

            
        }
    }
}