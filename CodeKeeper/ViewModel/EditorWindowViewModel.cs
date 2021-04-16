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

        private DataRowView _currentSnippet;

        public DataRowView CurrentSnippet
        {
            get { return _currentSnippet; }
            set { SetProperty(ref _currentSnippet, value); }
        }

        public BCmd_NewEditorCommand BCmd_NewEditorCommand { get; set; }
        public BCmd_DeleteEditorCommand BCmd_DeleteEditorCommand { get; set; }
        public BCmd_PreviewCommand BCmd_PreviewCommand { get; set; }

        public SnippetListViewSelectionChangedCommand SnippetListViewSelectionChangedCommand { get; set; }

        public EditorWindowViewModel(Window pwin)
        {
            ParentWindow = pwin;

            SnippetView = MasterRepository._Snippet.GetAllAsView();
            CurrentSnippet = SnippetView[0];

            BCmd_NewEditorCommand = new BCmd_NewEditorCommand(this);
            BCmd_DeleteEditorCommand = new BCmd_DeleteEditorCommand(this);
            BCmd_PreviewCommand = new BCmd_PreviewCommand(this);

            SnippetListViewSelectionChangedCommand = new SnippetListViewSelectionChangedCommand(this);
        }
    }
}
