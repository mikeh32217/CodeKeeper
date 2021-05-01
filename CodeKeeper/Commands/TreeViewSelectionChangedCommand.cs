using CodeKeeper.Configuration;
using CodeKeeper.Events;
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
        {
            // NOTE Gets called twice, the second time with parameter = null
            if (parameter == null)
                return;

            DirectoryMessage msg = new DirectoryMessage((TreeNode)parameter);
            msg.NodeMsg.Type = TreeNode.DF_TYPE.File;
            App.g_eventAggregator.GetEvent<DirectoryEvent>().Publish(msg);
        }
    }
}
