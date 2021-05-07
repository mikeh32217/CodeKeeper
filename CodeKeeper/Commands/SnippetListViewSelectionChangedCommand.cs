using CodeKeeper.Model;
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
    public class SnippetListViewSelectionChangedCommand : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add { }
            remove { }
        }

        public EditorWindowViewModel ViewModel { get; set; }

        public SnippetListViewSelectionChangedCommand(EditorWindowViewModel vm)
        {
            ViewModel = vm;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
 //           ViewModel.CurrentSnippet = parameter as DataRowView;
        }
    }
}
