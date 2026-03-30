using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using MonitoreoTemp.Models;

namespace MonitoreoTemp.Controllers
{
    public class TemperatureController
    {
        private bool _ejecutando = false;
        public delegate void OnTempChanged(SensorData data);
        public event OnTempChanged TempChanged; // Evento para avisar a la Vista

        public void IniciarSimulacion()
        {
            _ejecutando = true;
            Thread hiloSimulador = new Thread(Simular);
            hiloSimulador.Start();
        }

        private void Simular()
        {
            Random r = new Random();
            while (_ejecutando)
            {
                var data = new SensorData
                {
                    Valor = r.NextDouble() * (35 - 15) + 15, // Entre 15 y 35 grados
                    FechaHora = DateTime.Now
                };

                // Notificar a la Vista
                TempChanged?.Invoke(data);

                Thread.Sleep(2000); // Pausa de 2 segundos
            }
        }
    }
}