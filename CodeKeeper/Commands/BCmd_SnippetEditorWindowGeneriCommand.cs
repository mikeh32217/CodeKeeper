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
    public class BCmd_SnippetEditorWindowGeneriCommand : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add { }
            remove { }
        }

        public Window ParentWindow { get; set; }
        public SnippetEditorPromptWindowViewModel ViewModel { get; set; }

        public BCmd_SnippetEditorWindowGeneriCommand(Window win, SnippetEditorPromptWindowViewModel vm)
        {
            ParentWindow = win;

            ViewModel = vm;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (parameter.ToString() == "Accept")
                ParentWindow.DialogResult = true;
            else
                ParentWindow.DialogResult = false;

            ParentWindow.Close();
        }
    }
}
