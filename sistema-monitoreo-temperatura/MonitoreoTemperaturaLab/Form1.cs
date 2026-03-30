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

       
            
        }
    }
}