using CodeKeeper.Configuration;
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
    /// Interaction logic for PreviewWindow.xaml
    /// </summary>
    public partial class PreviewWindow : Window
    {
        public PreviewWindow(string content)
        {
            InitializeComponent();

            PreviewWindowViewModel vm = new PreviewWindowViewModel(this, content);

            DataContext = vm;
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ConfigMgr.Instance.settingProvider.SetValue("PreviewWindowSize", "Width", e.NewSize.Width.ToString());
            ConfigMgr.Instance.settingProvider.SetValue("PreviewWindowSize", "Height", e.NewSize.Height.ToString());
            ConfigMgr.Instance.configMgr.SaveConfigChanges();
        }
    }
}
