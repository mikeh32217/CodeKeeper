using CodeKeeper.Configuration;
using CodeKeeper.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for EditorWindow.xaml
    /// </summary>
    public partial class EditorWindow : Window
    {
        public EditorWindowViewModel ViewModel { get; set; }
        public EditorWindow()
        {
            InitializeComponent();

            ViewModel = new EditorWindowViewModel(this);

            DataContext = ViewModel;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            ConfigMgr.Instance.settingProvider.SetValue(this.Name, "width", Width.ToString());
            ConfigMgr.Instance.settingProvider.SetValue(this.Name, "height", Height.ToString());
            ConfigMgr.Instance.configMgr.SaveConfigChanges();
        }
    }
}
