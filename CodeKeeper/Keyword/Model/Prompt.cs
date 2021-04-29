using CodeKeeper.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeKeeper.Keyword.Model
{
    public class Prompt : iKeyword
    {
        public string Keyword { get; set; } = "Prompt";

        public string Execute(object param)
        {
            string rstr = string.Empty;

            SnippetEditorPromptWindow win = new SnippetEditorPromptWindow(rstr);
            win.ShowDialog();

            if (win.DialogResult == true)
                rstr = win.ViewModel.PromptTextBoxText;
            else
                rstr = string.Empty;

            return rstr;
        }
    }
}
