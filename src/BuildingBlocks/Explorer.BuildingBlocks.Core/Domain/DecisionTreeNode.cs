using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.BuildingBlocks.Core.Domain
{
    public class DecisionTreeNode
    {
        public string Message { get; set; }
        public Dictionary<string, DecisionTreeNode> Responses { get; set; }
        public DecisionTreeNode Parent { get; set; }
        public DecisionTreeNode(string message, DecisionTreeNode parent = null)
        {
            Message = message;
            Responses = new Dictionary<string, DecisionTreeNode>(StringComparer.OrdinalIgnoreCase);
            Parent = parent;
        }
    }
}
