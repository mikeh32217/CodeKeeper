﻿using CodeKeeper.Model;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CodeKeeper.Model.TreeNode;

namespace CodeKeeper.Events
{
    public class DirectoryEvent : PubSubEvent<TreeNodeMessage>
    {

    }
}
