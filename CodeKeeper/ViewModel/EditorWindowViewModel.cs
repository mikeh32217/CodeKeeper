using CodeKeeper.Commands;
using CodeKeeper.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CodeKeeper.ViewModel
{
    public class EditorWindowViewModel : ViewModelBase
    {
        public Window ParentWindow { get; set; }

        public DataView SnippetView { get; set; }
        public DataRowView CurrentSnippet { get; set; }

        public BCmd_InsertTokenCommand BCmd_InsertTokenCommand { get; set; }

        public EditorWindowViewModel(Window pwin)
        {
            ParentWindow = pwin;

            SnippetView = MasterRepository._Snippet.GetAllAsView();
            CurrentSnippet = SnippetView[0];

            BCmd_InsertTokenCommand = new BCmd_InsertTokenCommand(this);
        }
    }
}
