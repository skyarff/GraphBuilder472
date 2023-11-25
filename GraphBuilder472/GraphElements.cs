using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphBuilder472
{
    internal class Node
    {
        internal int X { get; set; }
        internal int Y { get; set; }
        internal int Z { get; set; }
        internal int D { get; set; }

        internal bool isSelected = false;


        private List<Node> relatedNodes = new List<Node>();


        internal int Count
        {
            get { return relatedNodes.Count; }
        }


        internal List<Node> RelatedNodes
        {
            get
            {
                return relatedNodes;
            }
        }


        public Node(int _x, int _y)
        {
            X = _x;
            Y = _y;
            Z = 0;
            D = 35;
        }

    }


}
