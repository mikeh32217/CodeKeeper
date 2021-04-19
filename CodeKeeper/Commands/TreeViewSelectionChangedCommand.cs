﻿using CodeKeeper.Events;
using CodeKeeper.Model;
using System;
using System.Windows.Input;

namespace CodeKeeper.Commands
{
    public class TreeViewSelectionChangedCommand : ICommand
    {
        event EventHandler ICommand.CanExecuteChanged
        {
            add{}
            remove{}
        }

        bool ICommand.CanExecute(object parameter)
        {
            return true;
        }

        void ICommand.Execute(object parameter)
        {DirectoryMessage msg = new DirectoryMessage((TreeNode)parameter);
            App.g_eventAggregator.GetEvent<DirectoryEvent>().Publish(msg);
        }
    }
}
