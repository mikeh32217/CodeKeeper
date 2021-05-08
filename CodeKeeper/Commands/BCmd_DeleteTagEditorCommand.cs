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
    public class BCmd_DeleteTagEditorCommand : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add { }
            remove { }
        }

        public TagEditorWindowViewModel ViewModel { get; set; }

        public BCmd_DeleteTagEditorCommand(TagEditorWindowViewModel vm)
        {
            ViewModel = vm;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (ViewModel.CurrentItem != null)
            {
                if (!bool.Parse(ViewModel.CurrentItem["CanDelete"].ToString()))
                    MessageBox.Show("You cannot delete this Tag!", "No can do",
                        MessageBoxButton.OK,
                        MessageBoxImage.Hand);
                else
                {
                    if (MessageBox.Show("You are about to delete a Tag, continue?", "Continue?",
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        MasterRepository._Token.Delete(ViewModel.CurrentItem["TokenId"].ToString());
                    }
                }
            }
        }
    }
}
