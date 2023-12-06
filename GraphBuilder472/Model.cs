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

        //internal List<Node> dataLink;

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

       


        public void ResetDijkstra()
        {

            shortWayList = new Dictionary<Node, int>();
            prevList = new Dictionary<Node, Node>();
            reviewList = new Dictionary<Node, bool>();


            foreach (Node node in graphTable.Keys)
            {
                shortWayList.Add(node, -1);
                prevList.Add(node, minIndexCalibr);
                reviewList.Add(node, false);
            }

        }


        public Graph() { }

        public Graph(List<Node> nodes)
        {

            graphTable = new Dictionary<Node, Dictionary<Node, int>>();
            for (int i = 0; i < nodes.Count; i++)
            {
                Dictionary<Node, int> inner = new Dictionary<Node, int>();
                graphTable.Add(nodes[i], inner);
            }

            ResetDijkstra();
        }

        public void AddNode(int _x, int _y)
        {
            Node newNode = null;
            int i = 1;
            bool flag = true;
            while (true)
            {
                foreach (Node _node in graphTable.Keys)
                {
                    if (_node.name == i.ToString())
                    {
                        flag = false;
                        break;
                    }
                }

                if (flag)
                {
                    newNode = new Node(_x, _y, i.ToString());
                    break;
                }
                flag = true;
                i++;
            }


            Dictionary<Node, int> temp = new Dictionary<Node, int>();
            graphTable.Add(newNode, temp);


            shortWayList.Add(newNode, -1);
            prevList.Add(newNode, minIndexCalibr);
            reviewList.Add(newNode, false);

        }

        public void RemoveNode(Node node)
        {
            graphTable.Remove(node);


            foreach (Node _node in graphTable.Keys)
            {
                graphTable[_node].Remove(node);
            }


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

                    if (graphTable[minIndex].ContainsKey(node))
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

        internal string name {  get; }
        internal int X { get; set; }
        internal int Y { get; set; }
        internal int Z { get; set; }
        internal int D { get; set; }

        internal bool isSelected = false;


        public Node(int _x, int _y)
        {
            name = "0";
            X = _x;
            Y = _y;
            Z = 0;
            D = 33;
        }

        public Node(int _x, int _y, string _name)
        {
            name = _name;
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
