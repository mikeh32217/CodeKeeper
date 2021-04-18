using CodeKeeper.View;
using CodeKeeper.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CodeKeeper.Commands
{
    public class BCmd_OpenTagEditorCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public BCmd_OpenTagEditorCommand()
        {
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            TagEditorWindow tew = new TagEditorWindow();
            tew.ShowDialog();
        }
    }
}
