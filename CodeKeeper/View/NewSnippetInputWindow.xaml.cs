using CodeKeeper.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CodeKeeper.View
{
    /// <summary>
    /// Interaction logic for NewSnippetInputWindow.xaml
    /// </summary>
    public partial class NewSnippetInputWindow : Window
    {
        public NewSnippetWindowViewModel ViewModel { get; set; }

        public NewSnippetInputWindow()
        {
            InitializeComponent();

            ViewModel = new NewSnippetWindowViewModel(this);
            DataContext = ViewModel;
        }
    }
}
