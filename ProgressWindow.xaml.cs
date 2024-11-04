using System.Windows;

namespace RealDebrid
{
    /// <summary>
    /// Logica di interazione per ProgressWindow.xaml
    /// </summary>
    public partial class ProgressWindow : Window
    {
        public ProgressWindow()
        {
            InitializeComponent();
        }

        public void UpdateProgress(int percentage)
        {
            progressBar.Value = percentage; // Aggiorna il valore della ProgressBar
        }
    }
}
