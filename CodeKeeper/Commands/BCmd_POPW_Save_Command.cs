using CodeKeeper.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CodeKeeper.Commands
{
    public class BCmd_POPW_Save_Command : ICommand
    {
        public PreviewOutputProcessWindowViewModel ViewModel { get; set; }

        public event EventHandler CanExecuteChanged;

        public BCmd_POPW_Save_Command(PreviewOutputProcessWindowViewModel vm)
        {
            ViewModel = vm;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            File.WriteAllText(ViewModel.FileInfo.Name, ViewModel.RawContent);
        }
    }
}
