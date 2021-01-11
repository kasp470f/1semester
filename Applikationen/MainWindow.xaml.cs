using Applikationen.ViewModels;
using System.Windows;

namespace Applikationen
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainWindowViewModel mvvm = new MainWindowViewModel();
            DataContext = mvvm;
        }
    }
}
