using CodeKeeper.Model;
using CodeKeeper.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace CodeKeeper.Commands
{
    public class FilterTextChangedCommand : ICommand
    {
        public PreviewOutputWindowViewModel ViewModel { get; set; }

        private ICollectionView FilterView;


        public FilterTextChangedCommand(PreviewOutputWindowViewModel vm)
        {
            ViewModel = vm;

            FilterView = CollectionViewSource.GetDefaultView(ViewModel.TagInfoList);
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
       {
             string filter = parameter.ToString();

            // Need this here so that when I do a refresh it updates the view also.
            FilterView = CollectionViewSource.GetDefaultView(ViewModel.TagInfoList);

            FilterView.Filter = o =>
            {
                TagInfo ti = o as TagInfo;
                return ti.LinkTargetInnerText.Contains(filter);
            };
        }

    }
}
