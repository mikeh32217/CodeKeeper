using CodeKeeper.ViewModel;
using System.Windows;

namespace CodeKeeper.View
{
    /// <summary>
    /// Interaction logic for PreviewOutputWindow.xaml
    /// </summary>
    public partial class PreviewOutputWindow : Window
    {
        public PreviewOutputWindowViewModal ViewModal { get; set; }

        public PreviewOutputWindow()
        {
            InitializeComponent();

            ViewModal = new PreviewOutputWindowViewModal(this);

            DataContext = ViewModal;
        }
    }
}
