using CodeKeeper.Commands;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CodeKeeper.ViewModel
{
    public class PreviewWindowViewModel : ViewModelBase
    {
        public string Content { get; set; }

        public BCmd_PW_CopyToClipboard BCmd_PW_CopyToClipboard { get; set; }
        public BCmd_PW_Save BCmd_PW_Save { get; set; }


        public DataRowView CurrentItem { get; set; }

        public PreviewWindowViewModel(Window win, string content) : base(win)
        {
            Content = content;

            BCmd_PW_CopyToClipboard = new BCmd_PW_CopyToClipboard(this);
            BCmd_PW_Save = new BCmd_PW_Save(this);
        }
    }
}
