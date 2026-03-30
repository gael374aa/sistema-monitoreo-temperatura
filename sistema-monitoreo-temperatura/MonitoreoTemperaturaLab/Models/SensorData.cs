using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitoreoTemp.Models
{
    public class SensorData
    {
        public double Valor { get; set; }
        public DateTime FechaHora { get; set; }
        public string Estado { get; set; } // "Normal", "Alerta", "Critico"
    }
}