using MonitoreoTemperaturaLab.Controllers;
using MonitoreoTemperaturaLab.Models;
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using LiveCharts;
using LiveCharts.WinForms;
using LiveCharts.Wpf; // <-- agregar
using LiveCharts.Defaults;
using System.Runtime.InteropServices;
using System.IO;

namespace MonitoreoTemperaturaLab
{
    public partial class Form1 : Form
    {
        private readonly TemperatureController _controller = new TemperatureController();
        private readonly List<double> _temps = new List<double>();
        private LiveCharts.WinForms.CartesianChart cartesianChart;
        private ChartValues<ObservablePoint> normalValues;
        private ChartValues<ObservablePoint> warningValues;
        private ChartValues<ObservablePoint> criticalValues;
        private ChartValues<ObservablePoint> mainValues;
        private ChartValues<ObservablePoint> normalMarkers;
        private ChartValues<ObservablePoint> warningMarkers;
        private ChartValues<ObservablePoint> criticalMarkers;
        private LiveCharts.Wpf.LineSeries seriesMain;
        private LiveCharts.Wpf.ScatterSeries markersNormal;
        private LiveCharts.Wpf.ScatterSeries markersWarning;
        private LiveCharts.Wpf.ScatterSeries markersCritical;
        private LiveCharts.Wpf.LineSeries seriesNormal;
        private LiveCharts.Wpf.LineSeries seriesWarning;
        private LiveCharts.Wpf.LineSeries seriesCritical;
        private System.Windows.Forms.Timer progressTimer;
        private int targetProgress = 0;
        private const int ProgressStep = 1;
        private double sampleIndex = 0;
        private int lastState = -1;
        private bool isPaused = false;

        // P/Invoke para cambiar el estado de ProgressBar (color) en Windows
        private const uint PBM_SETSTATE = 0x0410;
        private const int PBST_NORMAL = 1; // verde
        private const int PBST_ERROR = 2;  // rojo
        private const int PBST_PAUSED = 3; // amarillo

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        private static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        public Form1()
        {
            InitializeComponent();

            // Conectar el evento del controlador
            _controller.TempChanged += AlRecibirNuevaTemperatura;

            // Inicializar gráfica LiveCharts
            TryInitPlot();

            // Iniciar el simulador
            //_controller.IniciarSimulacion();
        }

        // Mapea el valor de temperatura a un valor de progress más representativo por estado
        // Normal (<25) -> 0..60, Precaución (25..29.9) -> 61..85, Crítico (>=30) -> 86..100
        private int MapProgressByState(double temp)
        {
            if (temp < 25)
            {
                // map 0..24.9 -> 0..60
                double t = Math.Max(0, Math.Min(temp, 24.9));
                return (int)Math.Round(t / 24.9 * 60);
            }
            else if (temp >= 25 && temp < 30)
            {
                // map 25..29.9 -> 61..85
                double t = Math.Max(25, Math.Min(temp, 29.9)) - 25.0;
                return 61 + (int)Math.Round(t / 4.9 * 24);
            }
            else
            {
                // map 30..100 -> 86..100
                double t = Math.Max(30, Math.Min(temp, 100)) - 30.0;
                return 86 + (int)Math.Round(t / 70.0 * 14);
            }
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

            // 1. Actualizar el texto y la barra
            lblTemperatura.Text = data.Valor.ToString("N1") + " °C"; // Muestra 1 decimal

            // No establecemos el ProgressBar de forma inmediata para permitir animación suave.
            // Calculamos un objetivo mapeado según el estado para que cada estado llene más la barra.
            int valorEntero = (int)data.Valor;

            // 2. Lógica de Colores y Alertas Visuales
            if (data.Valor < 25)
            {
                pnlAlerta.BackColor = Color.LimeGreen;
                lblEstado.Text = "ESTADO: NORMAL"; // Agrega un label para esto
            }
            else if (data.Valor >= 25 && data.Valor < 30)
            {
                pnlAlerta.BackColor = Color.Gold;
                lblEstado.Text = "ESTADO: PRECAUCIÓN";
            }
            else
            {
                pnlAlerta.BackColor = Color.Crimson;
                lblEstado.Text = "ESTADO: ¡CRÍTICO!";

                // Alerta auditiva opcional
                System.Media.SystemSounds.Exclamation.Play();
            }

            // 3. Actualizar la gráfica en tiempo real (LiveCharts si está disponible)
            try
            {
                _temps.Add(data.Valor);
                const int maxPoints = 100;
                if (_temps.Count > maxPoints)
                    _temps.RemoveAt(0);

                // calcular target de progreso mapeado por estado (0-100)
                targetProgress = MapProgressByState(data.Valor);

                // cambiar color progresivo según estado
                int newState;
                if (data.Valor < 25)
                {
                    newState = PBST_NORMAL;
                }
                else if (data.Valor >= 25 && data.Valor < 30)
                {
                    newState = PBST_PAUSED;
                }
                else
                {
                    newState = PBST_ERROR;
                }

                // Solo actualizar si cambió el estado (evita llamadas redundantes) y usa lastState
                if (newState != lastState)
                {
                    SetProgressState(newState);
                    lastState = newState;
                }

                // añadir punto al chart de forma incremental para evitar parpadeo
                AddDataPoint(data.Valor);
            }
            catch
            {
                // Ignorar errores de la gráfica
            }

        }
        
        
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pgbTemperatura_Click(object sender, EventArgs e)
        {

        }

        private void lblEstado_Click(object sender, EventArgs e)
        {

        }

        private void btnFinalizar_Click(object sender, EventArgs e)
        {
            // Toggle: si está pausado -> reanudar; si está corriendo -> detener
            try
            {
                if (!isPaused)
                {
                    // Detener la simulación
                    _controller.DetenerSimulacion();
                    isPaused = true;

                    // Actualizar UI para indicar pausa y ofrecer reanudar
                    btnIniciar.Enabled = true;
                    btnFinalizar.Text = "REANUDAR";
                    lblEstado.Text = "ESTADO: DETENIDO";
                    pnlAlerta.BackColor = Color.Gray;
                }
                else
                {
                    // Reanudar la simulación
                    _controller.IniciarSimulacion();
                    isPaused = false;

                    btnIniciar.Enabled = false;
                    btnFinalizar.Text = "DETENER";
                    lblEstado.Text = "ESTADO: ESCANEANDO...";
                }
            }
            catch
            {
                // Ignorar errores al cambiar estado
            }
        }

        private void btnIniciar_Click_1(object sender, EventArgs e)
        {
            _controller.IniciarSimulacion(); // Llama al método que ya tenías

            // Gestión de botones para evitar errores
            btnIniciar.Enabled = false;
            btnFinalizar.Enabled = true;
            lblEstado.Text = "ESTADO: ESCANEANDO...";

            // Limpiar la serie al iniciar para comenzar desde cero
            try
            {
                // Limpiar serie y reiniciar gráfica LiveCharts
                _temps.Clear();
                TryInitPlot();
            }
            catch
            {
            }
        }

        private void SetProgressState(int state)
        {
            try
            {
                if (pgbTemperatura == null) return;
                SendMessage(pgbTemperatura.Handle, PBM_SETSTATE, new IntPtr(state), IntPtr.Zero);
            }
            catch
            {
            }
        }

        // Intenta inicializar LiveCharts si el control está presente
        private void TryInitPlot()
        {
            try
            {
                if (formsPlot1 == null) return;

                // crear y alojar el chart en el Panel existente (formsPlot1)
                formsPlot1.Controls.Clear();

                cartesianChart = new LiveCharts.WinForms.CartesianChart { Dock = DockStyle.Fill };
                formsPlot1.Controls.Add(cartesianChart);

                // series y valores: una serie principal continua y marcadores por estado
                mainValues = new ChartValues<ObservablePoint>();
                normalValues = new ChartValues<ObservablePoint>();
                warningValues = new ChartValues<ObservablePoint>();
                criticalValues = new ChartValues<ObservablePoint>();

                normalMarkers = new ChartValues<ObservablePoint>();
                warningMarkers = new ChartValues<ObservablePoint>();
                criticalMarkers = new ChartValues<ObservablePoint>();

                // crear series persistentes (para evitar parpadeo) con valores vinculados
                seriesMain = new LiveCharts.Wpf.LineSeries
                {
                    Title = "Temperatura",
                    Values = mainValues,
                    PointGeometry = null,
                    Stroke = System.Windows.Media.Brushes.DodgerBlue,
                    Fill = System.Windows.Media.Brushes.Transparent,
                    StrokeThickness = 2
                };
               
                // scatter markers for each state
                markersNormal = new LiveCharts.Wpf.ScatterSeries
                {
                    Values = normalMarkers,
                    Fill = System.Windows.Media.Brushes.LimeGreen,
                    Stroke = System.Windows.Media.Brushes.LimeGreen,
                    MinPointShapeDiameter = 6,
                    MaxPointShapeDiameter = 6
                };

                markersWarning = new LiveCharts.Wpf.ScatterSeries
                {
                    Values = warningMarkers,
                    Fill = System.Windows.Media.Brushes.Gold,
                    Stroke = System.Windows.Media.Brushes.Gold,
                    MinPointShapeDiameter = 6,
                    MaxPointShapeDiameter = 6
                };

                markersCritical = new LiveCharts.Wpf.ScatterSeries
                {
                    Values = criticalMarkers,
                    Fill = System.Windows.Media.Brushes.Crimson,
                    Stroke = System.Windows.Media.Brushes.Crimson,
                    MinPointShapeDiameter = 6,
                    MaxPointShapeDiameter = 6
                };

                cartesianChart.Series = new SeriesCollection { seriesMain, markersNormal, markersWarning, markersCritical };
                // configurar para eje X continuo y desactivar animaciones para actualización más estable
                cartesianChart.DisableAnimations = true; // evitar parpadeo
                sampleIndex = 0;
                lastState = -1;



                cartesianChart.LegendLocation = LegendLocation.Right;

                // Refrescar gráfico
                cartesianChart.Refresh();
                // inicializar timer para progreso suave
                if (progressTimer == null)
                {
                    progressTimer = new System.Windows.Forms.Timer();
                    progressTimer.Interval = 30; // 30ms para animación suave
                    progressTimer.Tick += ProgressTimer_Tick;
                    progressTimer.Start();
                }
            }
            catch
            {
                // Ignorar si LiveCharts no está presente
            }
        }

        // Intenta actualizar la gráfica LiveCharts si está disponible
        private void TryUpdatePlot(double[] xs, double[] ys)
        {
            try
            {
                if (cartesianChart == null || normalValues == null) return;

                // Reconstruir series principales a partir del buffer _temps para el eje X coherente
                mainValues.Clear();
                normalMarkers.Clear();
                warningMarkers.Clear();
                criticalMarkers.Clear();

                double idx = Math.Max(0, _temps.Count - 100);
                for (int i = 0; i < _temps.Count; i++)
                {
                    idx += 1.0;
                    double v = _temps[i];
                    mainValues.Add(new ObservablePoint(idx, v));
                    if (v < 25) normalMarkers.Add(new ObservablePoint(idx, v));
                    else if (v >= 25 && v < 30) warningMarkers.Add(new ObservablePoint(idx, v));
                    else criticalMarkers.Add(new ObservablePoint(idx, v));
                }

                cartesianChart.Invoke((Action)(() => cartesianChart.Update(true, false)));
            }
            catch
            {
                // Ignorar si LiveCharts no está presente
            }
        }

        // Añade un nuevo punto a las series de forma que las líneas se comporten como una sola trazada
        private void AddDataPoint(double value)
        {
            if (cartesianChart == null) return;
            // Agregar punto con índice X creciente para mantener línea continua
            sampleIndex += 1.0;

            if (mainValues == null) return;

            mainValues.Add(new ObservablePoint(sampleIndex, value));

            // markers según estado
            if (value < 25)
            {
                normalMarkers.Add(new ObservablePoint(sampleIndex, value));
            }
            else if (value >= 25 && value < 30)
            {
                warningMarkers.Add(new ObservablePoint(sampleIndex, value));
            }
            else
            {
                criticalMarkers.Add(new ObservablePoint(sampleIndex, value));
            }

            // mantener tamaño máximo
            const int maxPoints = 100;
            while (mainValues.Count > maxPoints) mainValues.RemoveAt(0);
            while (normalMarkers.Count > maxPoints) normalMarkers.RemoveAt(0);
            while (warningMarkers.Count > maxPoints) warningMarkers.RemoveAt(0);
            while (criticalMarkers.Count > maxPoints) criticalMarkers.RemoveAt(0);

            // actualizar gráfico sin animación para evitar parpadeo
            cartesianChart.Invoke((Action)(() => cartesianChart.Update(true, false)));
        }

        // Tick del timer para animar el progress bar suavemente hacia targetProgress
        private void ProgressTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                if (pgbTemperatura == null) return;
                if (pgbTemperatura.Value == targetProgress) return;

                if (pgbTemperatura.Value < targetProgress)
                    pgbTemperatura.Value = Math.Min(pgbTemperatura.Value + ProgressStep, targetProgress);
                else
                    pgbTemperatura.Value = Math.Max(pgbTemperatura.Value - ProgressStep, targetProgress);
            }
            catch
            {
            }
        }

        // Maneja clic en Exportar: exporta temperaturas registradas a CSV
        private void btnExportar_Click(object sender, EventArgs e)
        {
            try
            {
                if (_temps == null || _temps.Count == 0)
                {
                    MessageBox.Show("No hay datos para exportar.", "Exportar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                using (SaveFileDialog sfd = new SaveFileDialog())
                {
                    sfd.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
                    sfd.FileName = "reporte_temperaturas.csv";
                    if (sfd.ShowDialog() != DialogResult.OK) return;

                    using (var sw = new StreamWriter(sfd.FileName))
                    {
                        // Cabecera
                        sw.WriteLine("Index,Timestamp,Temperature,LabelState");
                        // Timestamps aproximados: usar ahora y retroceder según muestras (suponiendo intervalo constante)
                        DateTime now = DateTime.Now;
                        // suponer intervalo de muestreo de 1 segundo si no hay otra info
                        TimeSpan sampleInterval = TimeSpan.FromSeconds(1);
                        int count = _temps.Count;
                        for (int i = 0; i < count; i++)
                        {
                            int idx = i + 1;
                            DateTime ts = now - TimeSpan.FromSeconds(count - i - 1);
                            double v = _temps[i];
                            string state = v < 25 ? "NORMAL" : (v < 30 ? "PRECAUCION" : "CRITICO");
                            sw.WriteLine($"{idx},{ts:O},{v:F1},{state}");
                        }
                    }

                    MessageBox.Show("Exportación completada.", "Exportar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al exportar: " + ex.Message, "Exportar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            lblTemperatura.Font = new Font(lblTemperatura.Font.FontFamily,
                Math.Max(8, this.ClientSize.Width / 40));
        }
    }
}