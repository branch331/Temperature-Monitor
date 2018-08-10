using System.Windows;
using System.Windows.Controls;

namespace NationalInstruments.Examples.CalibrationAudit
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            worker = new CalibrationAuditWorker();
            mainGrid.DataContext = worker;
        }

        private CalibrationAuditWorker worker;

        private void OnRunAuditClick(object sender, RoutedEventArgs e)
        {
            worker.StartRunAudit(passwordBox.Password);
        }

    }
}
