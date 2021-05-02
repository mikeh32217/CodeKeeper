using CodeKeeper.Model;
using CodeKeeper.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeKeeper.ViewModel
{
    public class PreviewOutputWindowViewModal : ViewModelBase
    {
        public PreviewOutputWindow ParentWindow { get; set; }

        public List<TagInfo> TagInfoList { get; set; }

        public string RawContent { get; set; }

        public PreviewOutputWindowViewModal(PreviewOutputWindow win)
        {
            ParentWindow = win;

            RawContent = Utilities.DocumentUtils.LoadFile(win.FileInfo.Name);
            TagInfoList = Utilities.ParseUtils.GetTagInfo(RawContent);

            // NOTE This loads the parsed file into the TextBox
            //  LongText = Utilities.DocumentUtils.PreviewFile(win.FileInfo.Name);
        }
    }
}
