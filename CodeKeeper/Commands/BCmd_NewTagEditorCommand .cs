using CodeKeeper.Repository;
using CodeKeeper.View;
using CodeKeeper.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace CodeKeeper.Commands
{
    public class BCmd_NewTagEditorCommand : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add { }
            remove { }
        }

        public TagEditorWindowViewModel ViewModel { get; set; }

        public BCmd_NewTagEditorCommand(TagEditorWindowViewModel vm)
        {
            ViewModel = vm;
        }


        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            NewTagInputWindow win = new NewTagInputWindow();
            win.ShowDialog();

            Int64 id = win.ViewModel.NewTagId;
            if (id > 0)
            {
                DataRow row = MasterRepository._Token.GetById(id.ToString());
                DataRowView selecRow = MasterRepository._Snippet.DefaultView.Cast<DataRowView>().FirstOrDefault(a => a.Row == row);

                ViewModel.CurrentItem = selecRow;
            }
        }
    }
}
