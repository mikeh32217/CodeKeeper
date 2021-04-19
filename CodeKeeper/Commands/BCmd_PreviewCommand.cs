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
            if (ViewModel.CurrentSnippet == null)
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
            // TEST temp testing Parse utility
            DataRowView drv = ViewModel.CurrentSnippet;

            string str = Utilities.ParseUtils.Parse(drv, @"K:\Projects - Visual Studio 2017\CodeKeeper\CodeKeeper\ViewModel\EditorWindowViewModel.cs");

            return str;
        }
    }
}
