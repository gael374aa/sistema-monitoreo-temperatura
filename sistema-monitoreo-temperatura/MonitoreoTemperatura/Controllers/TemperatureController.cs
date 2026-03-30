using System;
using System.Threading;
using MonitoreoTemperaturaLab.Models;

namespace MonitoreoTemperaturaLab.Controllers
{
    public class TemperatureController
    {
        private bool _ejecutando = false;
        public delegate void OnTempChanged(SensorData data);
        public event OnTempChanged TempChanged;

        public void IniciarSimulacion()
        {
            if (_ejecutando) return; // Evita que se inicien varios hilos
            _ejecutando = true;
            Thread hiloSimulador = new Thread(Simular);
            hiloSimulador.IsBackground = true; // Para que se cierre al cerrar la app
            hiloSimulador.Start();
        }

        private void Simular()
        {
            Random r = new Random();
            while (_ejecutando)
            {
                var data = new SensorData
                {
                    Valor = r.NextDouble() * (35 - 15) + 15, // Simula entre 15 y 35 grados
                    FechaHora = DateTime.Now
                };

                TempChanged?.Invoke(data);
                Thread.Sleep(2000); // Actualiza cada 2 segundos
            }
        }
    }
}