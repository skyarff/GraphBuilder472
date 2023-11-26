

using Microsoft.VisualBasic.Devices;
using System.Windows.Forms;

namespace GraphBuilder472
{

    internal class Controller
    {

        internal static bool StateHandler(int x, int y, int mouseDown, int mode, Graph graph, NodeStack nodeStack)
        {

            mode += mouseDown;
            Node node = null;

            for (int i = 0; i < graph.dataLink.Count; i++)
            {
                if (graph.dataLink[i].X - graph.dataLink[i].D / 2 <= x && graph.dataLink[i].X + graph.dataLink[i].D / 2 >= x)
                {
                    if (graph.dataLink[i].Y - graph.dataLink[i].D / 2 <= y && graph.dataLink[i].Y + graph.dataLink[i].D / 2 >= y)
                    {
                        node = graph.dataLink[i];
                    }
                }
            }

            switch (mode)
            {
                case 3:

                    if (node != null)
                    {
                        node.X = x;
                        node.Y = y;

                        return true;
                    }
                    break;
                case 5:

                    if (node != null)
                    {
                        if (!node.isSelected)
                        {
                            nodeStack.Add(node);
                        }
                        else
                        {
                            nodeStack.Remove(node);
                        }
                        return true;
                    }
                    break;
                case 9:

                    if (node == null)
                    {
                        Node newNode = new Node(x, y);
                        graph.AddNode(newNode);

                    }
                    else
                    {
                        graph.RemoveNode(node);
                    }
                    return true;
            }

            return false;
        }
    }
}
