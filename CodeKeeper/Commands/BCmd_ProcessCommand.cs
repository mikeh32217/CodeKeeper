using CodeKeeper.Model;
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
    public class BCmd_ProcessCommand : ICommand
    {
        public MainWindowViewModel ViewModel { get; set; }

        event EventHandler ICommand.CanExecuteChanged
        {
            add {}
            remove {}
        }

        public BCmd_ProcessCommand(MainWindowViewModel vm)
        {
            ViewModel = vm;
        }

        bool ICommand.CanExecute(object parameter)
        {
            return true;
        }

        void ICommand.Execute(object parameter)
        {
            string file = @"K:\Projects - Visual Studio 2017\CodeKeeper\CodeKeeper\TestData\class.txt";

            if (ViewModel.CurrentFile != null)
                file = ViewModel.CurrentFile.Name;

            // TODO Finish for testing
            PreviewOutputWindow win = new PreviewOutputWindow(new FileData(file));
            win.ShowDialog();
        }
    }
}
