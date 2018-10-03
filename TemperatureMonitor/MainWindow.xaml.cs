using System.Windows;
using System.Windows.Controls;

namespace NationalInstruments.Examples.BoardTemperatureMonitor
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            worker = new BoardTemperatureMonitorWorker();
            mainGrid.DataContext = worker;
            TemperatureLimitBox.Text = "50";
            TemperatureSlider.Value = 50;
        }

        private BoardTemperatureMonitorWorker worker;

        private void OnRunAuditClick(object sender, RoutedEventArgs e)
        {
            worker.StartRunAudit(passwordBox.Password);
        }

        private void OnStopButtonClick(object sender, RoutedEventArgs e)
        {
            worker.StopMonitor = true;
        }
    }
}
