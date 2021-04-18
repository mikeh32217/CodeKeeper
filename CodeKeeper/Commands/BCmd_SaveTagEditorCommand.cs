using CodeKeeper.Repository;
using CodeKeeper.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CodeKeeper.Commands
{
    public class BCmd_SaveTagEditorCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public TagEditorWindowViewModel ViewModel { get; set; }

        public BCmd_SaveTagEditorCommand(TagEditorWindowViewModel vm)
        {
            ViewModel = vm;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
 // TODO          MasterRepository._Snippet.UpdateTag(ViewModel.CurrentTag);
        }
    }
}
