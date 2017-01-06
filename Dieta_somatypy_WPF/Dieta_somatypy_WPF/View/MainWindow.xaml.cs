using System.Windows;
using Dieta_somatypy_WPF.Repo;

namespace Dieta_somatypy_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            Frame1.NavigationService.Navigate(new Page1());
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = ExitWindow.Exit();
        }
    }
}
