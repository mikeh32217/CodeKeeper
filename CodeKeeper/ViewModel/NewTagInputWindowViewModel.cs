using CodeKeeper.Commands;
using CodeKeeper.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeKeeper.ViewModel
{
    public class NewTagInputWindowViewModel : ViewModelBase
    {
        public string NewTagName { get; set; }
        public Int64 NewTagId { get; set; }

        public BCmd_NewTagCommand BCmd_NewTagCommand { get; set; }

        public NewTagInputWindowViewModel(NewTagInputWindow win) : base(win)
        {
            BCmd_NewTagCommand = new BCmd_NewTagCommand(this, win);
        }
    }
}
