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
    public class BCmd_NewTagCommand : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add { }
            remove { }
        }

        public NewTagInputWindow TopLevelWindow { get; set; }

        public NewTagInputWindowViewModel ViewModel { get; set; }

        public BCmd_NewTagCommand(NewTagInputWindowViewModel vm, NewTagInputWindow win)
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
            string tag = ViewModel.NewTagName;

            ViewModel.NewTagId = MasterRepository._Token.SaveTag(tag);

            TopLevelWindow.Close();
        }
    }
}
