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
        public DataView SnippetView { get; set; }

        // TODO Impliment this!
        public bool Isirty { get; set; }

        private DataRowView _currentItem;

        public DataRowView CurrentItem
        {
            get { return _currentItem; }
            set { SetProperty(ref _currentItem, value); }
        }

        public BCmd_NewEditorCommand BCmd_NewEditorCommand { get; set; }
        public BCmd_SaveEditorCommand BCmd_SaveEditorCommand { get; set; }
        public BCmd_DeleteEditorCommand BCmd_DeleteEditorCommand { get; set; }
        public BCmd_PreviewCommand BCmd_PreviewCommand { get; set; }

        public SnippetListViewSelectionChangedCommand SnippetListViewSelectionChangedCommand { get; set; }

        public EditorWindowViewModel(Window win) : base(win)
        {
            SnippetView = MasterRepository._Snippet.GetAllAsView();
            if (SnippetView.Table.DefaultView.Count > 0)
                _currentItem = SnippetView[0];
            else
                MessageBox.Show("No snippets available");

            BCmd_NewEditorCommand = new BCmd_NewEditorCommand(this);
            BCmd_SaveEditorCommand = new BCmd_SaveEditorCommand(this);
            BCmd_DeleteEditorCommand = new BCmd_DeleteEditorCommand(this);
            BCmd_PreviewCommand = new BCmd_PreviewCommand(this);

            SnippetListViewSelectionChangedCommand = new SnippetListViewSelectionChangedCommand(this);
        }
    }
}
