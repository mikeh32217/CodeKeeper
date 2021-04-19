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
        public ObservableCollection<TreeNode> TreeNodeList { get; set; }

        public TreeNode()
        {
            TreeNodeList = new ObservableCollection<TreeNode>();
        }

        public TreeNode(string nm, DF_TYPE type = DF_TYPE.Directory)
        {
            Name = nm;
            Type = type;
            TreeNodeList = new ObservableCollection<TreeNode>();
        }
    }
}
