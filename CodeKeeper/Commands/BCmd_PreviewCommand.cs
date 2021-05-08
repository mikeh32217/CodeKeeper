using CodeKeeper.Configuration;
using CodeKeeper.Repository;
using CodeKeeper.View;
using CodeKeeper.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CodeKeeper.Commands
{
    public class BCmd_PreviewCommand : ICommand
    {
        public EditorWindowViewModel ViewModel { get; set; }

        public event EventHandler CanExecuteChanged
        {
            add { }
            remove { }
        }

        public BCmd_PreviewCommand(EditorWindowViewModel vm)
        {
            ViewModel = vm;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (ViewModel.CurrentItem == null)
                return;

            string res = DoParse();

            PreviewWindow pw = new PreviewWindow(res);

            Dictionary<string, string> sz = ConfigMgr.Instance.settingProvider.GetValues("PreviewWindowSize");
            if (sz != null)
            {
                pw.Width = int.Parse(sz["Width"].ToLower());
                pw.Height = int.Parse(sz["Height"].ToLower());
            }

            pw.ShowDialog();
        }

        private string DoParse()
        {
            DataRowView drv = ViewModel.CurrentItem;

            string content = drv["Content"].ToString();
            string str = Utilities.ParseUtils.ParseSnippet(content);

            return str;
        }
    }
}
