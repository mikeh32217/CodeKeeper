using CodeKeeper.Events;
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
    public class TagInfoSelectionChangedCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public PreviewOutputWindowViewModel ViewModel { get; set; }

        public TagInfoSelectionChangedCommand(PreviewOutputWindowViewModel vm)
        {
            ViewModel = vm;
        }

        public TagInfoSelectionChangedCommand()
        { }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            // TODO Need to modify to call the appropriate handler
            App.g_eventAggregator.GetEvent<TagRefreshEvent>().Publish(new UpdateMessage(true));
        }
    }
}
