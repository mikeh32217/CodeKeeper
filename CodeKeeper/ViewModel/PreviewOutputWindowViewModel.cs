using CodeKeeper.Commands;
using CodeKeeper.Model;
using CodeKeeper.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeKeeper.ViewModel
{
    public class PreviewOutputWindowViewModel : ViewModelBase
    {
        public PreviewOutputWindow ParentWindow { get; set; }

        public TagInfoSelectionChangedCommand TagInfoSelectionChangedCommand { get; set; }
        public BCmd_POW_Toolbar_Refresh BCmd_POW_Toolbar_Refresh { get; set; }

        private ObservableCollection<TagInfo> _tagInfoList;

        public ObservableCollection<TagInfo> TagInfoList
        {
            get
            {
                if (_tagInfoList == null)
                    _tagInfoList = new ObservableCollection<TagInfo>();

                return _tagInfoList;
            }
            set
            {
                _tagInfoList = value;
            }
        }

        public TagInfo CurrentTagInfo { get; set; }

        public string RawContent { get; set; }

        public PreviewOutputWindowViewModel(PreviewOutputWindow win)
        {
            ParentWindow = win;

            RawContent = Utilities.DocumentUtils.LoadFile(win.FileInfo.Name);
            RefreshTagList();

            TagInfoSelectionChangedCommand = new TagInfoSelectionChangedCommand(this);
            BCmd_POW_Toolbar_Refresh = new BCmd_POW_Toolbar_Refresh(this);

            // NOTE This loads the parsed file into the TextBox
            //  LongText = Utilities.DocumentUtils.PreviewFile(win.FileInfo.Name);
        }

        public void RefreshTagList()
        {
            ParentWindow.TagInfoListView.ItemsSource = null;
            // At first go this will be null...duh!
            if (_tagInfoList != null)
                _tagInfoList.Clear();
            TagInfoList = Utilities.ParseUtils.GetTagInfo(RawContent);
            ParentWindow.TagInfoListView.ItemsSource = TagInfoList;
        }
    }
}
