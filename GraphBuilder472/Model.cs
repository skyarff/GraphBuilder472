using System;
using System.Collections.Generic;

namespace GraphBuilder472
{

    class Graph
    {


        
        private Dictionary<Node, bool> reviewList;
        internal Dictionary<Node, Dictionary<Node, int>> graphTable;
        internal Dictionary<Node, int> shortWayList;
        internal Dictionary<Node, Node> prevList;

        internal List<Node> dataLink;

        internal Node minIndexCalibr = new Node(-1, -1);


        public int this[Node row, Node col]
        {
            get
            {
                return graphTable[row][col];
            }

            set
            {
                graphTable[row][col] = value;
            }
        }

       
        public void CreateGraphTable()
        {
           
            graphTable = new Dictionary<Node, Dictionary<Node, int>>();


            for (int i = 0; i < dataLink.Count; i++)
            {
                Dictionary<Node, int> inner = new Dictionary<Node, int>();
                graphTable.Add(dataLink[i], inner);

                for (int j = 0; j < dataLink.Count; j++)
                {
                    inner.Add(dataLink[j], 0);
                }
            }

        }


        public void ResetDijkstra()
        {

            shortWayList = new Dictionary<Node, int>();
            prevList = new Dictionary<Node, Node>();
            reviewList = new Dictionary<Node, bool>();


            for (int i = 0; i < dataLink.Count; i++)
            {
                shortWayList.Add(dataLink[i], -1);
                prevList.Add(dataLink[i], minIndexCalibr);
                reviewList.Add(dataLink[i], false);
            }

        }


        public Graph()
        {
            //CreateGraphTable();
            //ResetDijkstra();
        }

        public Graph(List<Node> nodes)
        {
            dataLink = nodes;

            CreateGraphTable();
            ResetDijkstra();
        }


        public void AddNode(Node node)
        {

            Dictionary<Node, int> temp = new Dictionary<Node, int>();
            for (int i = 0; i < dataLink.Count; i++)
            {
                temp.Add(dataLink[i], 0);
            }
            graphTable.Add(node, temp);
            


            foreach (Node _node in graphTable.Keys)
            {
                graphTable[_node].Add(node, 0);
            }


            dataLink.Add(node);
            shortWayList.Add(node, -1);
            prevList.Add(node, minIndexCalibr);
            reviewList.Add(node, false);


        }

        public void RemoveNode(Node node)
        {
            graphTable.Remove(node);


            foreach (Node _node in graphTable.Keys)
            {
                graphTable[_node].Remove(node);
            }

            dataLink.Remove(node);
            shortWayList.Remove(node);
            prevList.Remove(node);
            reviewList.Remove(node);
        }

        public void Dijkstra(Node index)
        {
            ResetDijkstra();


            shortWayList[index] = 0;
            reviewList[index] = true;

            for (int i = 0; i < graphTable.Count; i++)
            {
                
                Node minIndex = minIndexCalibr;

 
                foreach (Node node in graphTable.Keys)
                {
                    if (reviewList[node])
                    {
                        if (minIndex == minIndexCalibr || shortWayList[node] < shortWayList[minIndex])
                        {
                            minIndex = node;
                        }
                    }
                }
                if (minIndex == minIndexCalibr) return;
               

                foreach (Node node in graphTable.Keys)
                {
                    if (graphTable[minIndex][node] > 0)
                    {
                        if (shortWayList[node] == -1 || shortWayList[node] > graphTable[minIndex][node] + shortWayList[minIndex])
                        {
                            shortWayList[node] = graphTable[minIndex][node] + shortWayList[minIndex];
                            prevList[node] = minIndex;
                            reviewList[node] = true;

                        }
                    }
                }

                reviewList[minIndex] = false;
            }
        }



    }

    internal class Node
    {
        internal int X { get; set; }
        internal int Y { get; set; }
        internal int Z { get; set; }
        internal int D { get; set; }

        internal bool isSelected = false;


        public Node(int _x, int _y)
        {
            X = _x;
            Y = _y;
            Z = 0;
            D = 33;
        }

    }

    internal class NodeStack
    {
        internal Node[] twoNodes = new Node[2];
        int count = 0;

        internal int Length { get { return count; } }


        public Node this[int index]
        {
            get
            {
                if (index >= 0 && index < count)
                {
                    return twoNodes[index];
                }
                throw new IndexOutOfRangeException();
            }
            set
            {
                if (index >= 0 && index < count)
                {
                    twoNodes[index] = value;
                }
                throw new IndexOutOfRangeException();
            }
        }



        internal void Add(Node node)
        {
            if (count == 0)
            {
                twoNodes[0] = node;
                count++;
            }
            else if (count == 1)
            {
                twoNodes[1] = node;
                count++;
            }
            else
            {
                twoNodes[0].isSelected = false;
                twoNodes[0] = twoNodes[1];
                twoNodes[1] = node;
            }

            node.isSelected = true;
        }

        internal void Remove(Node node)
        {
            if (node == twoNodes[0])
            {
                twoNodes[0] = twoNodes[1];
                twoNodes[1] = null;
            }
            else
            {
                twoNodes[1] = null;
            }
            node.isSelected = false;
            count--;
        }
    }
}
