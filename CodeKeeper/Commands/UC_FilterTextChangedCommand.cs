using CodeKeeper.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CodeKeeper.Commands
{
    public class UC_FilterTextChangedCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public UC_FilterTextChangedCommand()
        {

        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            App.g_eventAggregator.GetEvent<TagListRefreshEvent>().Publish(new GenericMessage(parameter.ToString()));
        }
    }
}
