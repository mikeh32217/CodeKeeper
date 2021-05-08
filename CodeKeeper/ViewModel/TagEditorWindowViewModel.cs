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
    public class TagEditorWindowViewModel : ViewModelBase
    {
        public DataView TagView { get; set; }

        public BCmd_NewTagEditorCommand BCmd_NewTagEditorCommand { get; set; }
        public BCmd_SaveTagEditorCommand BCmd_SaveTagEditorCommand { get; set; }
        public BCmd_DeleteTagEditorCommand BCmd_DeleteTagEditorCommand { get; set; }

        private DataRowView _currentItem;

        public DataRowView CurrentItem
        {
            get { return _currentItem; }
            set { SetProperty(ref _currentItem, value); }
        }

        public TagEditorWindowViewModel(Window win) : base(win)
        {
            TagView = MasterRepository._Token.GetAllAsView();
            CurrentItem = TagView[0];

            BCmd_NewTagEditorCommand = new BCmd_NewTagEditorCommand(this);
            BCmd_SaveTagEditorCommand = new BCmd_SaveTagEditorCommand(this);
            BCmd_DeleteTagEditorCommand = new BCmd_DeleteTagEditorCommand(this);
        }
    }
}
