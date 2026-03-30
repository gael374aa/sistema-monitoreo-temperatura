using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace SimuladorSemaforo.Modelo
{
    public class Semaforo
    {
        // Tiempos en milisegundos (Fase 2 del ABP)
        public int Verde = 5000;    // 5 segundos [cite: 40]
        public int Amarillo = 2000; // 2 segundos [cite: 40]
        public int Rojo = 5000;     // 5 segundos [cite: 40]
    }
}