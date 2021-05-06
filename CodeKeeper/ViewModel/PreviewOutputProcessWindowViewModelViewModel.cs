using CodeKeeper.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CodeKeeper.ViewModel
{
    public class PreviewOutputProcessWindowViewModel : ViewModelBase
    {
        public string RawContent { get; set; }

        public PreviewOutputProcessWindowViewModel(Window win, string content, FileData info) : base(win)
        {
            RawContent = Utilities.ParseUtils.ParseSnippet(content, info.Name);
        }
    }
}
