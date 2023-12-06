namespace GraphBuilder472
{

    internal class Controller
    {

        internal static bool StateHandler(int x, int y, int mouseDown, int mouseUp, int mode, Graph graph, NodeStack nodeStack)
        {

            mode += mouseDown + mouseUp;
            Node node = null;



            foreach (string _node in graph.graphTable.Keys)
            {
                //if (_node.X - _node.D / 2 <= x && _node.X + _node.D / 2 >= x)
                //{
                //    if (_node.Y - _node.D / 2 <= y && _node.Y + _node.D / 2 >= y)
                //    {
                //        node = _node;
                //    }
                //}

                if (graph.dataLink[_node].X - graph.dataLink[_node].D / 2 <= x && graph.dataLink[_node].X + graph.dataLink[_node].D / 2 >= x)
                {
                    if (graph.dataLink[_node].Y - graph.dataLink[_node].D / 2 <= y && graph.dataLink[_node].Y + graph.dataLink[_node].D / 2 >= y)
                    {
                        node = graph.dataLink[_node];
                    }
                }



            }


            //foreach (string _node in graph.graphTable.Keys)
            //{
            //    if (_node.X - _node.D / 2 <= x && _node.X + _node.D / 2 >= x)
            //    {
            //        if (_node.Y - _node.D / 2 <= y && _node.Y + _node.D / 2 >= y)
            //        {
            //            node = _node;
            //        }
            //    }
            //}



            switch (mode)
            {
                
                case 5:

                    if (node != null)
                    {
                        node.X = x;
                        node.Y = y;

                        return true;
                    }
                    break;
                case 10:
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
                case 18:
                    if (node == null)
                    {
                        graph.AddNode(x, y);
                    }
                    else
                    {
                        graph.RemoveNode(node.name);
                    }
                    return true;
            }

            return false;
        }
    }
}
