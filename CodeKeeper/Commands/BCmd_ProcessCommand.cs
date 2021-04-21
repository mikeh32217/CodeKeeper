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
            string path = @"K:\Projects - Visual Studio 2017\CodeKeeper\docs\test.txt";
            string opath = path + ".tmp";
            Utilities.DocumentUtils.ParseFile(path);
            Utilities.DocumentUtils.InsertTextInFile(opath, 0, "This is some really neat shit!\n");
            Utilities.DocumentUtils.InsertSnippetInFile(opath, 2, "!ahead");
            Utilities.DocumentUtils.InsertSnippetInFile(opath, 2, "filename");
        }
    }
}
