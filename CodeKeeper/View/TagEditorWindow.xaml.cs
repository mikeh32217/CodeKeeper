using CodeKeeper.Configuration;
using CodeKeeper.ViewModel;
using System.ComponentModel;
using System.Windows;

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

            ViewModel = new TagEditorWindowViewModel(this);

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
