using CodeKeeper.Configuration;
using CodeKeeper.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CodeKeeper.Commands
{
    public class BCmd_OpenSnippetEditorCommand : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add { }
            remove { }
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            EditorWindow ew = new EditorWindow();

            Dictionary<string, string> sz = ConfigMgr.Instance.settingProvider.GetValues("EditorWindowSize");
            if (sz != null)
            {
                ew.Width = int.Parse(sz["Width"].ToLower());
                ew.Height = int.Parse(sz["Height"].ToLower());
            }

            ew.ShowDialog();
        }
    }
}
