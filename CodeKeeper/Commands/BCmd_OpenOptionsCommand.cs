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
    public class BCmd_OpenOptionsCommand : ICommand
    {
        event EventHandler ICommand.CanExecuteChanged
        {
            add { }
            remove { }
        }

        bool ICommand.CanExecute(object parameter)
        {
            return true;
        }

        void ICommand.Execute(object parameter)
        {
            OptionsWindow ew = new OptionsWindow();

            Dictionary<string, string> sz = ConfigMgr.Instance.settingProvider.GetValues("OptionWindowSize");
            if (sz != null)
            {
                ew.Width = int.Parse(sz["Width"].ToLower());
                ew.Height = int.Parse(sz["Height"].ToLower());
            }

            ew.ShowDialog();
        }
    }
}
