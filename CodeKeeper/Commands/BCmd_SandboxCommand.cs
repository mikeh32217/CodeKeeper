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
    public class BCmd_SandboxCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public MainWindowViewModel ViewModel { get; set; }

        public BCmd_SandboxCommand(MainWindowViewModel vm)
        {
            ViewModel = vm;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            SandboxWindow win = new SandboxWindow();
            win.ShowDialog();
        }
    }
}
