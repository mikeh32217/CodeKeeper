﻿using CodeKeeper.Events;
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
            // NOTE Sent tot he ProcessOutputWindow code behind TagClickHandler
            App.g_eventAggregator.GetEvent<TagClickEvent>().Publish(parameter as TagInfo);
        }
    }
}
