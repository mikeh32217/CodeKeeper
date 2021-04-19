using CodeKeeper.Events;
using CodeKeeper.Model;
using Microsoft.Win32;
using Ookii.Dialogs.Wpf;
using System;
using System.Windows;
using System.Windows.Input;
using static CodeKeeper.Model.TreeNode;

namespace CodeKeeper.Commands
{
    public class BCmd_OpenProjectDirectoryCommand : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add { }
            remove { }
        }

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
                DirectoryMessage msg = new DirectoryMessage(new TreeNode(FullPath, DF_TYPE.Directory));
                App.g_eventAggregator.GetEvent<DirectoryEvent>().Publish(msg);
            }
            else
                FullPath = string.Empty;
        }
    }
}
