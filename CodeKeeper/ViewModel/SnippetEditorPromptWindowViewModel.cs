using CodeKeeper.Commands;
using CodeKeeper.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeKeeper.ViewModel
{
    public class SnippetEditorPromptWindowViewModel : ViewModelBase
    {
        public string PromptText { get; set; }
        public string PromptTextBoxText { get; set; }
        public BCmd_SnippetEditorWindowGeneriCommand BCmd_SnippetEditorWindowGeneriCommand { get; set; }

        public SnippetEditorPromptWindowViewModel(SnippetEditorPromptWindow win, string prompt)
        {
            PromptText = prompt;

            BCmd_SnippetEditorWindowGeneriCommand = new BCmd_SnippetEditorWindowGeneriCommand(ParentWindow, this);
        }
    }
}
