using System.Windows;

namespace Dieta_somatypy_WPF.Repo
{
    public static class ExitWindow
    {
        private static int OnlyOneExitMessage { get; set; } = 0;
        public static bool Exit(int i = 0)
        {
            if (OnlyOneExitMessage == 0)
            {
                OnlyOneExitMessage = i;
                var result = MessageBox.Show("Czy na pewno chcesz zakończyć pracę?", "Koniec", MessageBoxButton.YesNo, 
                    MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    Application.Current.Shutdown();
                }
                else if (result == MessageBoxResult.No)
                {
                    OnlyOneExitMessage = 0;
                    return true;
                }
            }
            return false;
        }
    }
}
