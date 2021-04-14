using CodeKeeper.ViewModel;
using System.Windows;

namespace CodeKeeper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            MainWindowViewModel mwvm = new MainWindowViewModel();

            DataContext = mwvm;
        }
    }
}
