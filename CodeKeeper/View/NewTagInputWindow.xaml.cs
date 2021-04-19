using CodeKeeper.ViewModel;
using System.Windows;

namespace CodeKeeper.View
{
    /// <summary>
    /// Interaction logic for NewTagInputWindow.xaml
    /// </summary>
    public partial class NewTagInputWindow : Window
    {
        public NewTagInputWindowViewModel ViewModel { get; set; }

        public NewTagInputWindow()
        {
            InitializeComponent();

            ViewModel = new NewTagInputWindowViewModel(this);
            DataContext = ViewModel;
        }
    }
}
