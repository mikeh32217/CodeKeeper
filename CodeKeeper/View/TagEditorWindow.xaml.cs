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
    /// Interaction logic for TagEditorWindow.xaml
    /// </summary>
    public partial class TagEditorWindow : Window
    {
        public TagEditorWindowViewModel ViewModel { get; set; }

        public TagEditorWindow()
        {
            InitializeComponent();

            ViewModel = new TagEditorWindowViewModel();

            DataContext = ViewModel;
        }
    }
}
