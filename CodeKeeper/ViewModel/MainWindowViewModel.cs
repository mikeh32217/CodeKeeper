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

        // For the directory TreeView
        public ObservableCollection<TreeNode> TreeNodeCollection { get; set; } = new ObservableCollection<TreeNode>();
        public TreeNode CurrentNode { get; set; }

        // For the File ListView
        public ObservableCollection<FileData> FileCollection { get; set; } = new ObservableCollection<FileData>();
        public FileData CurrentFile { get; set; }

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
            if (node.NodeMsg.Type == DF_TYPE.Directory)
            {
                TreeNodeCollection.Clear();

                TreeNode root = new TreeNode(node.NodeMsg.Name);
                TreeNodeCollection.Add(root);

                DirectoryInfo di = new DirectoryInfo(node.NodeMsg.Name);
                TraverseDirectory(di, root);
            }
            else
            {
                FileCollection.Clear();

                string[] info = Directory.GetFiles(node.NodeMsg.FullPath);
                foreach (string s in info)
                    FileCollection.Add(new FileData(s));
            }
        }

        // TEMP Data for testing
        private void TraverseDirectory(DirectoryInfo root, TreeNode node)
        {
            // TODO Create a ListView on the right side of the main window area
            //  that shall contain a list of all files in the directories an
            //  sudirectories with a filter and checkboxes to refine the list
            //  of files to process.
            DirectoryInfo[] subDirs = null;

            TreeNode dir = new TreeNode(root.Name, root.FullName);
            node.TreeNodeList.Add(dir);

            subDirs = root.GetDirectories();

            foreach (System.IO.DirectoryInfo dirInfo in subDirs)
            {
                TraverseDirectory(dirInfo, dir);
            }
        }

        private void  GetFileInfo(TreeNode node)
        {

        }
    }
}
