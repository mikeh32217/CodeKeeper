using CodeKeeper.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeKeeper.Events
{
    public class StringMessage
    {
        public string Message { get; set; }

        public StringMessage(string msg)
        {
            Message = msg;
        }
    }

    public class TreeNodeMessage
    {
        public TreeNode Node { get; set; }

        public TreeNodeMessage(TreeNode node)
        {
            Node = node;
        }
    }
}
