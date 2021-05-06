using CodeKeeper.Configuration;
using CodeKeeper.Events;
using CodeKeeper.View;
using CodeKeeper.ViewModel;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;

namespace CodeKeeper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Dictionary<string, string> wsz = ConfigMgr.Instance.settingProvider.GetValues("MainWindowSize");
            if (wsz != null)
            {
                Width = int.Parse(wsz["Width"].ToString());
                Height = int.Parse(wsz["Height"].ToString());
            }

            MainWindowViewModel mwvm = new MainWindowViewModel(this);

            DataContext = mwvm;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            ConfigMgr.Instance.settingProvider.SetValue(this.Name, "width", Width.ToString());
            ConfigMgr.Instance.settingProvider.SetValue(this.Name, "height", Height.ToString());
            ConfigMgr.Instance.configMgr.SaveConfigChanges();
        }
    }
}
