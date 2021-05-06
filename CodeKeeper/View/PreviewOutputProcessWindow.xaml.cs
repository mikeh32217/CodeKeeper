using CodeKeeper.Model;
using CodeKeeper.ViewModel;
using System.Windows;

namespace CodeKeeper.View
{
    /// <summary>
    /// Interaction logic for PreviewOutputProcessWindow.xaml
    /// </summary>
    public partial class PreviewOutputProcessWindow : Window
    {
        public PreviewOutputProcessWindowViewModel ViewModal { get; set; }

        public PreviewOutputProcessWindow(string data, FileData info)
        {
            InitializeComponent();

            ViewModal = new PreviewOutputProcessWindowViewModel(this, data, info);

            DataContext = ViewModal;
        }
    }
}
