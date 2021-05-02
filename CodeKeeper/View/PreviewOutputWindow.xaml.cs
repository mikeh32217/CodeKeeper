using CodeKeeper.Model;
using CodeKeeper.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace CodeKeeper.View
{
    /// <summary>
    /// Interaction logic for PreviewOutputWindow.xaml
    /// </summary>
    public partial class PreviewOutputWindow : Window
    {
        public PreviewOutputWindowViewModal ViewModal { get; set; }
        public FileData FileInfo { get; set; }


        public PreviewOutputWindow(FileData file)
        {
            InitializeComponent();

            FileInfo = file;

            ViewModal = new PreviewOutputWindowViewModal(this);

            DataContext = ViewModal;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            textity.SelectionStart = 420;
            int index = textity.GetLineIndexFromCharacterIndex(textity.SelectionStart);
            textity.ScrollToLine(index);
            textity.Focus();
        }
    }
}
