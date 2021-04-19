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
    /// Interaction logic for OptionsWindow.xaml
    /// </summary>
    public partial class OptionsWindow : Window
    {
        public OptionsWindowViewModel ViewModel { get; set; }

        public OptionsWindow()
        {
            InitializeComponent();

            ViewModel = new OptionsWindowViewModel();
            DataContext = ViewModel;
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ConfigMgr.Instance.settingProvider.SetValue("OptionWindowSize", "Width", e.NewSize.Width.ToString());
            ConfigMgr.Instance.settingProvider.SetValue("OptionWindowSize", "Height", e.NewSize.Height.ToString());
            ConfigMgr.Instance.configMgr.SaveConfigChanges();
        }
    }
}
