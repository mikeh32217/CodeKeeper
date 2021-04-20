using CodeKeeper.Commands;
using CodeKeeper.Configuration;
using CodeKeeper.Events;
using CodeKeeper.Model;
using CodeKeeper.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using static CodeKeeper.Model.TreeNode;

namespace CodeKeeper.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        public string Version { get; set; }

        // TEMP
        public ObservableCollection<TreeNode> TreeNodeCollection { get; set; }
        public TreeNode CurrentNode { get; set; }

        public BCmd_OpenProjectDirectoryCommand BCmd_OpenProjectDirectoryCommand { get; set; }
        public BCmd_OpenFileCommand BCmd_OpenFileCommand { get; set; }
        public BCmd_OpenSnippetEditorCommand BCmd_OpenSnippetEditorCommand { get; set; }
        public BCmd_OpenTagEditorCommand BCmd_OpenTagEditorCommand { get; set; }
        public BCmd_OpenOptionsCommand BCmd_OpenOptionsCommand { get; set; }
        public TreeViewSelectionChangedCommand TreeViewSelectionChangedCommand { get; set; }

        public MainWindowViewModel()
        {
            // NOTE Version to be changed as needed
            Version = "CodeKeeper V 1.0";

            // TreeView SelectionChangeed notification
            App.g_eventAggregator.GetEvent<DirectoryEvent>().Subscribe(MessageCallback);

            BCmd_OpenProjectDirectoryCommand = new BCmd_OpenProjectDirectoryCommand();
            BCmd_OpenFileCommand = new BCmd_OpenFileCommand();
            BCmd_OpenSnippetEditorCommand = new BCmd_OpenSnippetEditorCommand();
            BCmd_OpenTagEditorCommand = new BCmd_OpenTagEditorCommand();
            BCmd_OpenOptionsCommand = new BCmd_OpenOptionsCommand();
            TreeViewSelectionChangedCommand = new TreeViewSelectionChangedCommand();
        }

        private void MessageCallback(DirectoryMessage node)
        {
            TraverseDirectory(node);
        }

        // TEMP Data for testing
        private void TraverseDirectory(DirectoryMessage node)
        {
            // TODO Create a ListView on the right side of the main window area
            //  that shall contain a list of all files in the directories an
            //  sudirectories with a filter and checkboxes to refine the list
            //  of files to process.

            TreeNode root = new TreeNode("Root");
            TreeNode node1 = new TreeNode("Root1");
            node1.TreeNodeList.Add(new TreeNode("Sub1"));
            node1.TreeNodeList.Add(new TreeNode("Sub2"));
            root.TreeNodeList.Add(node1);
            root.TreeNodeList.Add(new TreeNode("Adrienne", DF_TYPE.File));

            TreeNodeCollection = root.TreeNodeList;
        }
    }
}
