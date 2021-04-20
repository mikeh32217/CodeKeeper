using System.Collections.ObjectModel;

namespace CodeKeeper.Model
{
    public class TreeNode
    {
        public enum DF_TYPE
        {
            File,
            Directory
        };

        public DF_TYPE Type { get; set; }
        public string Name { get; set; }
        public string FullPath { get; set; }
        public ObservableCollection<TreeNode> TreeNodeList { get; set; }

        public TreeNode()
        {
            TreeNodeList = new ObservableCollection<TreeNode>();
        }


        public TreeNode(string nm, DF_TYPE type = DF_TYPE.Directory)
        {
            Name = nm;
            FullPath = nm;
            Type = type;
            TreeNodeList = new ObservableCollection<TreeNode>();
        }

        public TreeNode(string nm, string path, DF_TYPE type = DF_TYPE.Directory)
        {
            Name = nm;
            FullPath = path;
            Type = type;
            TreeNodeList = new ObservableCollection<TreeNode>();
        }
    }
}
