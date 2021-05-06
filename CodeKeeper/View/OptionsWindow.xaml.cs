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
    /// Interaction logic for OptionsWindow.xaml
    /// </summary>
    public partial class OptionsWindow : Window
    {
        public OptionsWindowViewModel ViewModel { get; set; }

        public OptionsWindow()
        {
            InitializeComponent();

            ViewModel = new OptionsWindowViewModel(this);
            DataContext = ViewModel;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            ConfigMgr.Instance.settingProvider.SetValue(Name, "width", Width.ToString());
            ConfigMgr.Instance.settingProvider.SetValue(Name, "height", Height.ToString());
            ConfigMgr.Instance.configMgr.SaveConfigChanges();
        }
    }
}
