using CodeKeeper.ViewModel;
using System;
using System.Windows;
using System.Windows.Input;

namespace CodeKeeper.Commands
{
    public class BCmd_PW_Save : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public PreviewWindowViewModel ViewModel { get; set; }

        public BCmd_PW_Save(PreviewWindowViewModel vm)
        {
            ViewModel = vm;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (parameter != null)
                Utilities.DocumentUtils.SaveFile(null, parameter as string);
            else
                MessageBox.Show("There was a problem no file was specified to save",
                    "No can do",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
        }
    }
}
