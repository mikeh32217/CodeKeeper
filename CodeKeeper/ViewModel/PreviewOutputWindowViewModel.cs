using CodeKeeper.Commands;
using CodeKeeper.Model;
using CodeKeeper.Repository;
using CodeKeeper.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeKeeper.ViewModel
{
    public class PreviewOutputWindowViewModel : ViewModelBase
    {
        private static DataView TokenView = null;
        private static DataView TagView = null;

        public FileData FileInfo { get; set; }

        public BCmd_POW_Toolbar_Refresh BCmd_POW_Toolbar_Refresh { get; set; }
        public BCmd_POW_Toolbar_Process BCmd_POW_Toolbar_Process{ get; set; }
        public BCmd_POW_Toolbar_Validate BCmd_POW_Toolbar_Validate { get; set; }

        // Invoke commands
        public TagInfoSelectionChangedCommand TagInfoSelectionChangedCommand { get; set; }
        public FilterTextChangedCommand FilterTextChangedCommand { get; set; }


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

        public PreviewOutputWindowViewModel(PreviewOutputWindow win) : base(win)
        {
            FileInfo = win.FileInfo;

            RawContent = Utilities.DocumentUtils.LoadFile(FileInfo.Name);

            TokenView = MasterRepository._Token.GetAllAsView();
            TagView = MasterRepository._Snippet.GetAllAsView();

            RefreshTagList();

            TagInfoSelectionChangedCommand = new TagInfoSelectionChangedCommand(this);
            BCmd_POW_Toolbar_Refresh = new BCmd_POW_Toolbar_Refresh(this);
            BCmd_POW_Toolbar_Process = new BCmd_POW_Toolbar_Process(this);
            BCmd_POW_Toolbar_Validate = new BCmd_POW_Toolbar_Validate(this);

            FilterTextChangedCommand = new FilterTextChangedCommand(this);

            // NOTE This loads the parsed file into the TextBox
            //  LongText = Utilities.DocumentUtils.PreviewFile(win.FileInfo.Name);
        }

        public void RefreshTagList()
        {
            ((PreviewOutputWindow)ParentWindow).TagInfoListView.ItemsSource = null;

            // At first go this will be null...duh!
            if (_tagInfoList != null)
                _tagInfoList.Clear();

            TagInfoList = Utilities.ParseUtils.GetTagInfo(RawContent);
            foreach (TagInfo ti in TagInfoList)
                GetValidTagInfo(ti);

            ((PreviewOutputWindow)ParentWindow).TagInfoListView.ItemsSource = TagInfoList;
        }

        private static TagInfo GetValidTagInfo(TagInfo ti)
        {
            bool fnd = false;

            if (ti.TagType == TagInfo.TokenType.Snippet)
            {
                // Look in the Snippet list
                MasterRepository._Snippet.GetSnippetByTag(ti.LinkTargetInnerText);
                if (MasterRepository._Snippet.WorkingView.Count > 0)
                {
                    ti.TagType = TagInfo.TokenType.Snippet;
                    ti.IsValid = true;
                    fnd = true;
                }
            }
            else
            {
                MasterRepository._Token.GetTokenByTag(ti.LinkTargetInnerText);
                if (MasterRepository._Token.WorkingView.Count > 0)
                {
                    ti.TagType = TagInfo.TokenType.Token;
                    ti.IsValid = true;
                    fnd = true;
                }
            }

            // If not found in either place it is undefined.
            if (!fnd)
            {
                ti.TagType = TagInfo.TokenType.Undefined;
                ti.IsValid = false;
            }

            return ti;
        }

    }
}
