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
    public class BCmd_InsertTokenCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public EditorWindowViewModel ViewModel { get; set; }

        public BCmd_InsertTokenCommand(EditorWindowViewModel vm)
        {
            ViewModel = vm;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
        }

        public void Execute(object sender, EventArgs args)
        {

        }
    }
}
