using CodeKeeper.Repository;
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
    public class BCmd_NewSnippetCommand : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add { }
            remove { }
        }

        public NewSnippetInputWindow TopLevelWindow { get; set; }

        public NewSnippetWindowViewModel ViewModel { get; set; }

        public BCmd_NewSnippetCommand(NewSnippetWindowViewModel vm, NewSnippetInputWindow win)
        {
            TopLevelWindow = win;

            ViewModel = vm;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            string tag = ViewModel.NewSnippetName;

            ViewModel.NewSnippetId = MasterRepository._Snippet.SaveSnippet(tag);

            TopLevelWindow.Close();
        }
    }
}
