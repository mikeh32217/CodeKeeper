using CodeKeeper.ViewModel;
using System;
using System.Windows;
using System.Windows.Input;

namespace CodeKeeper.Commands
{
    public class BCmd_PW_CopyToClipboard : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public PreviewWindowViewModel ViewModel { get; set; }

        public BCmd_PW_CopyToClipboard(PreviewWindowViewModel vm)
        {
            ViewModel = vm;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            Clipboard.SetText(parameter.ToString());

            // TEST Do something with this
            System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
        }


        // TEST This is part of timer 
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            // code goes here
        }
    }
}
