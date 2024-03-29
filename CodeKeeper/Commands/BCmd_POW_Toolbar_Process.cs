﻿using CodeKeeper.View;
using CodeKeeper.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CodeKeeper.Commands
{
    public class BCmd_POW_Toolbar_Process : ICommand
    {
        public PreviewOutputWindowViewModel ViewModel { get; set; }

        public event EventHandler CanExecuteChanged;
  
        public BCmd_POW_Toolbar_Process(PreviewOutputWindowViewModel vm)
        {
            ViewModel = vm;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            PreviewOutputProcessWindow win = new PreviewOutputProcessWindow(ViewModel.RawContent, ViewModel.FileInfo);
            win.ShowDialog();
        }
    }
}
