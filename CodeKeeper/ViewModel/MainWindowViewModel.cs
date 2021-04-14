using CodeKeeper.Commands;
using CodeKeeper.Events;
using CodeKeeper.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeKeeper.ViewModel
{
    public class MainWindowViewModel
    {
        public BCmd_OpenProjectDirectoryCommand BCmd_OpenProjectDirectoryCommand { get; set; }
        public BCmd_OpenFileCommand BCmd_OpenFileCommand { get; set; }
        public BCmd_OpenSnippetEditorCommand BCmd_OpenSnippetEditorCommand { get; set; }

        public DataView TokenViewView { get; set; }


        public MainWindowViewModel()
        {
            // TODO Other stuff here

            // TEMP
            TokenViewView = MasterRepository._Token.GetAllAsView();

            App.g_eventAggregator.GetEvent<GenericEvent>().Subscribe(MessageCallback);

            BCmd_OpenProjectDirectoryCommand = new BCmd_OpenProjectDirectoryCommand();
            BCmd_OpenFileCommand = new BCmd_OpenFileCommand();        
            BCmd_OpenSnippetEditorCommand = new BCmd_OpenSnippetEditorCommand();

    }

    private void MessageCallback(GenericMessage msg)
        {
            Utilities.DocumentHelpers.InsertTextInFile(msg.Message, 10, "Help Me\nI think that I am falling\n");
        }
    }
}
