using CodeKeeper.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CodeKeeper.Commands
{
    public class BCmd_DeleteEditorCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        public EditorWindowViewModel ViewModel { get; set; }

        public BCmd_DeleteEditorCommand(EditorWindowViewModel vm)
        {
            ViewModel = vm;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            // TODO Delete that puppy
        }
    }
}
