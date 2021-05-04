using CodeKeeper.Repository;
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
    public class BCmd_POW_Toolbar_Validate : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public PreviewOutputWindowViewModel ViewModel { get; set; }

        public BCmd_POW_Toolbar_Validate(PreviewOutputWindowViewModel vm)
        {
            ViewModel = vm;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            DataView view = MasterRepository._Token.GetAllAsView();
        }
    }
}
