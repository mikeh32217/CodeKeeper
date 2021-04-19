using CodeKeeper.Repository;
using CodeKeeper.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CodeKeeper.Commands
{
    public class BCmd_DeleteEditorCommand : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add { }
            remove { }
        }

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
            if (ViewModel.CurrentSnippet != null)
            {
                if (MessageBox.Show("You are about to delete a snippet, continue?", "Continue?",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    MasterRepository._Snippet.Delete(ViewModel.CurrentSnippet["Id"].ToString());
                }
            }
        }
    }
}
