using System.Threading; // Necesario para usar Threads [cite: 56]
using SimuladorSemaforo.Modelo;

namespace SimuladorSemaforo.Controlador
{
    public class SemaforoController
    {
        Thread hiloSemaforo; // Declaración del hilo [cite: 57]
        bool activo = false; // Variable de control [cite: 57]
        Semaforo datos = new Semaforo(); // Instancia del modelo

        // Este evento es el "cable" que conectará con la Vista
        public Action<string> AlCambiarColor;

        public void Iniciar()
        {
            // Evitar iniciar múltiples hilos si ya está activo
            if (activo) return;
            activo = true;
            hiloSemaforo = new Thread(CicloSemaforo);
            hiloSemaforo.IsBackground = true; // No bloqueará la salida de la app
            hiloSemaforo.Start();
        }

        public void Detener()
        {
            // Señalar detención
            activo = false;
            // Interrumpir el hilo si está en Sleep para detenerlo inmediatamente
            try
            {
                hiloSemaforo?.Interrupt();
                // Esperar un poco a que termine
                hiloSemaforo?.Join(500);
            }
            catch { }
            finally
            {
                hiloSemaforo = null;
            }
        }
        void CicloSemaforo()
        { // El ciclo lógico [cite: 58]
            try
            {
                while (activo)
                {
                    AlCambiarColor?.Invoke("verde");
                    Thread.Sleep(datos.Verde); // Pausa de 5s [cite: 58]

                    if (!activo) break;
                    AlCambiarColor?.Invoke("amarillo");
                    Thread.Sleep(datos.Amarillo); // Pausa de 2s [cite: 58]

                    if (!activo) break;
                    AlCambiarColor?.Invoke("rojo");
                    Thread.Sleep(datos.Rojo); // Pausa de 5s [cite: 58]
                }
            }
            catch (ThreadInterruptedException)
            {
                // Se interrumpió el hilo (Detener fue llamado), salir
            }
            finally
            {
                activo = false;
            }
        }
    }
}