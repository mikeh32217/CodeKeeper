using CodeKeeper.Commands;
using CodeKeeper.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeKeeper.ViewModel
{
    public class NewSnippetWindowViewModel : ViewModelBase
    {
        public string NewSnippetName { get; set; }
        public Int64 NewSnippetId { get; set; }

        public BCmd_NewSnippetCommand BCmd_NewSnippetCommand { get; set; }

        public NewSnippetWindowViewModel(NewSnippetInputWindow win) : base(win)
        {
            BCmd_NewSnippetCommand = new BCmd_NewSnippetCommand(this, win);
        }
    }
}
