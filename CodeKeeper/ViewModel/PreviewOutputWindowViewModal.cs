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

        public string LongText { get; set; }

        public PreviewOutputWindowViewModal(PreviewOutputWindow win)
        {
            ParentWindow = win;

            LongText = "This is a test with some really long text that will wrap around so I can see what it will do.";
        }
    }
}
