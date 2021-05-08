using CodeKeeper.Commands;
using CodeKeeper.Model;
using CodeKeeper.Repository;
using CodeKeeper.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CodeKeeper.ViewModel
{
    public class PreviewOutputWindowViewModel : ViewModelBase
    {
        public FileData FileInfo { get; set; }

        public BCmd_POW_Toolbar_Refresh BCmd_POW_Toolbar_Refresh { get; set; }
        public BCmd_POW_Toolbar_Process BCmd_POW_Toolbar_Process{ get; set; }
        public BCmd_POW_Toolbar_Validate BCmd_POW_Toolbar_Validate { get; set; }

        // Invoke commands
        public TagInfoSelectionChangedCommand TagInfoSelectionChangedCommand { get; set; }

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
               SetProperty(ref _tagInfoList, value);
            }
        }

        public TagInfo CurrentTagInfo { get; set; }

        public string RawContent { get; set; }

        public PreviewOutputWindowViewModel(PreviewOutputWindow win) : base(win)
        {
            FileInfo = win.FileInfo;

            RawContent = Utilities.DocumentUtils.LoadFile(FileInfo.Name);

            TagInfoSelectionChangedCommand = new TagInfoSelectionChangedCommand(this);
            BCmd_POW_Toolbar_Refresh = new BCmd_POW_Toolbar_Refresh(this);
            BCmd_POW_Toolbar_Process = new BCmd_POW_Toolbar_Process(this);
            BCmd_POW_Toolbar_Validate = new BCmd_POW_Toolbar_Validate(this);
        }
    }
}
