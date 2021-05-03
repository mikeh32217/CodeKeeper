﻿using CodeKeeper.Model;
using CodeKeeper.ViewModel;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace CodeKeeper.View
{
    /// <summary>
    /// Interaction logic for PreviewOutputWindow.xaml
    /// </summary>
    public partial class PreviewOutputWindow : Window
    {
        public PreviewOutputWindowViewModel ViewModal { get; set; }
        public FileData FileInfo { get; set; }


        public PreviewOutputWindow(FileData file)
        {
            InitializeComponent();

            FileInfo = file;

            ViewModal = new PreviewOutputWindowViewModel(this);

            DataContext = ViewModal;
        }

        public void TagClickHandler(TagInfo tinfo)
        {
            // NOTE When the Refresh toolbar button is clicked it causes
            //  a selection changed event to be fired and in this case
            //  tinfo is null.
            if (tinfo == null)
                return;

            try
            {
                textity.SelectionStart = tinfo.SelectIndex;
                textity.SelectionLength = tinfo.SelectLength;

                textity.Select(tinfo.SelectIndex, tinfo.SelectLength);

                int line = textity.GetLineIndexFromCharacterIndex(textity.SelectionStart);
                textity.ScrollToLine(line);

                Action focusAction = () => textity.Focus();
                this.Dispatcher.BeginInvoke(focusAction, DispatcherPriority.ApplicationIdle);
            }
            catch(Exception x)
            {
                MessageBox.Show("TagClickHandler: " + msg);
            }
        }
    }
}
