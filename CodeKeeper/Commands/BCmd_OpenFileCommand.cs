using CodeKeeper.Events;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CodeKeeper.Commands
{
    public class BCmd_OpenFileCommand : ICommand
    {
        public string FileName { get; set; }

        public BCmd_OpenFileCommand()
        {
        }

        public event EventHandler CanExecuteChanged
        {
            add { }
            remove { }
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            OpenFileDialog fd = new OpenFileDialog();
            fd.RestoreDirectory = true;
            if (fd.ShowDialog() == true)
            {
                FileName = fd.FileName;
                GenericMessage msg = new GenericMessage(FileName);
                App.g_eventAggregator.GetEvent<GenericEvent>().Publish(msg);
            }
            else
                FileName = string.Empty;
        }
    }
}
