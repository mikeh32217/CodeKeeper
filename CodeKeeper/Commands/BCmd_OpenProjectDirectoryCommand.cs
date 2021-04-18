using CodeKeeper.Events;
using Microsoft.Win32;
using Ookii.Dialogs.Wpf;
using System;
using System.Windows;
using System.Windows.Input;

namespace CodeKeeper.Commands
{
    public class BCmd_OpenProjectDirectoryCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public string FullPath { get; set; }

        public BCmd_OpenProjectDirectoryCommand()
        {

        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            VistaFolderBrowserDialog vfbd = new VistaFolderBrowserDialog();
            if (vfbd.ShowDialog() == true)
            {
                FullPath = vfbd.SelectedPath;
                GenericMessage msg = new GenericMessage(FullPath);
                App.g_eventAggregator.GetEvent<GenericEvent>().Publish(msg);
            }
            else
                FullPath = string.Empty;
        }
    }
}
