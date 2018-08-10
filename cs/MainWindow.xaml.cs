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
        }

        private BoardTemperatureMonitorWorker worker;

        private void OnRunAuditClick(object sender, RoutedEventArgs e)
        {
            worker.StartRunAudit(passwordBox.Password);
        }

    }
}
