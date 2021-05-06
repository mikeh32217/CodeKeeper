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

        public DataRowView CurrentSnippet { get; set; }

        public PreviewWindowViewModel(Window win, string content) : base(win)
        {
            Content = content;
        }
    }
}
