using Laboratory4.ViewModels;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Laboratory4.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.Closing += MainWindow_Closing;
        }

        private void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            if (DataContext is MainViewModel vm)
            {
                try
                {
                    vm.SaveStore();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        $"Failed to save data on exit:\n{ex.Message}",
                        "Save Error",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show(
                    $"Unexpected DataContext type: {DataContext?.GetType().FullName ?? "null"}",
                    "Debug",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
        }
    }
}