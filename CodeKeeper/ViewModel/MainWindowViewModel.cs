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
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using static CodeKeeper.Model.TreeNode;

namespace CodeKeeper.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        public string Version { get; set; }

        private bool _initializing = true;

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
        public BCmd_ProcessCommand BCmd_ProcessCommand { get; set; }
        public BCmd_SandboxCommand BCmd_SandboxCommand { get; set; }

        public TreeViewSelectionChangedCommand TreeViewSelectionChangedCommand { get; set; }

        public MainWindowViewModel(Window win) : base(win)
        {
            // NOTE Version to be changed as needed
            Version = "CodeKeeper V 1.0";

            // NOTE TreeView SelectionChangeed notification from;
            //  BCmd_OpenProjectDirectoryCommand
            //  TreeViewSelectionChangedCommand
            App.g_eventAggregator.GetEvent<DirectoryEvent>().Subscribe(DirectoryMessageCallback);

            BCmd_OpenProjectDirectoryCommand = new BCmd_OpenProjectDirectoryCommand();
            BCmd_OpenFileCommand = new BCmd_OpenFileCommand();
            BCmd_OpenSnippetEditorCommand = new BCmd_OpenSnippetEditorCommand();
            BCmd_OpenTagEditorCommand = new BCmd_OpenTagEditorCommand();
            BCmd_OpenOptionsCommand = new BCmd_OpenOptionsCommand();
            BCmd_ProcessCommand = new BCmd_ProcessCommand(this);

            BCmd_SandboxCommand = new BCmd_SandboxCommand(this);

            TreeViewSelectionChangedCommand = new TreeViewSelectionChangedCommand();

            string path = ConfigMgr.Instance.settingProvider.GetSingleValue("DefaultDirectory", "path");
            DirectoryMessageCallback(new TreeNodeMessage(new TreeNode(path)));
        }

        private void DirectoryMessageCallback(TreeNodeMessage node)
        {
            bool res = CheckDirectoryAuthorization(node.Node.FullPath);
            if (!_initializing && !res)
                return;

            _initializing = false;

            if (node.Node.Type == DF_TYPE.Directory)
            {
                TreeNodeCollection.Clear();

                TreeNode root = new TreeNode(node.Node.Name);
                TreeNodeCollection.Add(root);

                DirectoryInfo di = new DirectoryInfo(node.Node.Name);
                TraverseDirectory(di, root);
            }
            else
            {
                FileCollection.Clear();

                string[] info = Directory.GetFiles(node.Node.FullPath);
                foreach (string s in info)
                    FileCollection.Add(new FileData(s));
            }
        }

        private void TraverseDirectory(DirectoryInfo root, TreeNode node)
        {
            DirectoryInfo[] subDirs = null;

            try
            {
                TreeNode dir = new TreeNode(root.Name, root.FullName);
                node.TreeNodeList.Add(dir);

                subDirs = root.GetDirectories();

                foreach (System.IO.DirectoryInfo dirInfo in subDirs)
                {
                    TraverseDirectory(dirInfo, dir);
                }
            }
            catch(UnauthorizedAccessException)
            {
                return;
            }
        }

        private bool CheckDirectoryAuthorization(string path)
        {
            try
            {
                DirectoryInfo dirInfo = new DirectoryInfo(path);
                dirInfo.GetDirectories();
                dirInfo.GetFiles();
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}
