
using CodeKeeper.Repository;
using CodeKeeper.View;
using CodeKeeper.ViewModel;
using System;
using System.Data;
using System.Linq;
using System.Windows.Input;

namespace CodeKeeper.Commands
{
    public class BCmd_NewEditorCommand : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add { }
            remove { }
        }

        public EditorWindowViewModel ViewModel { get; set; }

        public BCmd_NewEditorCommand(EditorWindowViewModel vm)
        {
            ViewModel = vm;
        }


        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            NewSnippetInputWindow win = new NewSnippetInputWindow();
            win.ShowDialog();

            Int64 id = win.ViewModel.NewSnippetId;
            if (id > 0)
            {
                DataRow row = MasterRepository._Snippet.GetById(id.ToString());
                DataRowView selecRow = MasterRepository._Snippet.DefaultView.Cast<DataRowView>().FirstOrDefault(a => a.Row == row);

                ViewModel.CurrentSnippet = selecRow;
            }
        }
    }
}
