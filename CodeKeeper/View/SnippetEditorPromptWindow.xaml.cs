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
    /// Interaction logic for SnippetEditorPromptWindow.xaml
    /// </summary>
    public partial class SnippetEditorPromptWindow : Window
    {
        public SnippetEditorPromptWindowViewModel ViewModel { get; set; }

        public SnippetEditorPromptWindow(string prompt)
        {
            InitializeComponent();

             ViewModel = new SnippetEditorPromptWindowViewModel(this, prompt);

            DataContext = ViewModel;
        }
    }
}
