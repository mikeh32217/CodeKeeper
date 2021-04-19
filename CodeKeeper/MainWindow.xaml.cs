using CodeKeeper.Configuration;
using CodeKeeper.ViewModel;
using System.Collections.Generic;
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

            MainWindowViewModel mwvm = new MainWindowViewModel();

            DataContext = mwvm;
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ConfigMgr.Instance.settingProvider.SetValue("MainWindowSize", "Width", e.NewSize.Width.ToString());
            ConfigMgr.Instance.settingProvider.SetValue("MainWindowSize", "Height", e.NewSize.Height.ToString());
            ConfigMgr.Instance.configMgr.SaveConfigChanges();
        }
    }
}
