using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace GraphBuilder472
{

    class Graph
    {
        [JsonProperty]
        private Dictionary<string, bool> reviewList;
        [JsonProperty]
        internal Dictionary<string, Dictionary<string, int>> graphTable;
        [JsonProperty]
        internal Dictionary<string, int> shortWayList;
        [JsonProperty]
        internal Dictionary<string, string> prevList;
        [JsonProperty]
        internal Dictionary<string, Node> dataLink;


        public int this[string row, string col]
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

            shortWayList = new Dictionary<string, int>();
            prevList = new Dictionary<string, string>();
            reviewList = new Dictionary<string, bool>();


            foreach (string name in graphTable.Keys)
            {
                shortWayList.Add(name, -1);
                prevList.Add(name, "-1");
                reviewList.Add(name, false);
            }

        }


        public Graph()
        {
            dataLink = new Dictionary<string, Node>();

            graphTable = new Dictionary<string, Dictionary<string, int>>();

            ResetDijkstra();
        }

        public Graph(List<Node> nodes)
        {
            dataLink = new Dictionary<string, Node>();

            for (int i = 0; i < nodes.Count; i++)
            {
                dataLink.Add(nodes[i].name, nodes[i]);
            }

            

            graphTable = new Dictionary<string, Dictionary<string, int>>();
            for (int i = 0; i < nodes.Count; i++)
            {
                Dictionary<string, int> inner = new Dictionary<string, int>();
                graphTable.Add(nodes[i].name, inner);
            }

            ResetDijkstra();
        }

        

        public void AddNode(int _x, int _y)
        {
            Node newNode = null;
            string _name = "";
            int i = 1;
            bool flag = true;
            while (true)
            {
                
                foreach (string name in graphTable.Keys)
                {
                    if (name == i.ToString())
                    {
                        flag = false;
                        break;
                    }
                }

                if (flag)
                {
                    newNode = new Node(_x, _y, i.ToString());
                    _name = i.ToString();
                    break;
                }
                flag = true;
                i++;
            }


            Dictionary<string, int> temp = new Dictionary<string, int>();
            graphTable.Add(_name, temp);
            dataLink.Add(_name, newNode);

            shortWayList.Add(_name, -1);
            prevList.Add(_name, "-1");
            reviewList.Add(_name, false);

        }

        public void RemoveNode(string name)
        {
            graphTable.Remove(name);


            foreach (string _name in graphTable.Keys)
            {
                graphTable[_name].Remove(name);
            }


            dataLink.Remove(name);

            shortWayList.Remove(name);
            prevList.Remove(name);
            reviewList.Remove(name);
        }

        public void Dijkstra(string index)
        {
            ResetDijkstra();


            shortWayList[index] = 0;
            reviewList[index] = true;

            for (int i = 0; i < graphTable.Count; i++)
            {

                string minIndex = "-1";


                foreach (string node in graphTable.Keys)
                {
                    if (reviewList[node])
                    {
                        if (minIndex == "-1" || shortWayList[node] < shortWayList[minIndex])
                        {
                            minIndex = dataLink[node].name;
                        }
                    }
                }


                if (minIndex == "-1") return;


                foreach (string node in graphTable.Keys)
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
        [JsonProperty]
        internal string name { get; set; }
        [JsonProperty]
        internal int X { get; set; }
        [JsonProperty]
        internal int Y { get; set; }
        [JsonProperty]
        internal int Z { get; set; }
        [JsonProperty]
        internal int D { get; set; }

        //[JsonProperty]
        internal bool isSelected = false;

        public Node()
        {
            name = "0";
            X = 0;
            Y = 0;
            Z = 0;
            D = 0;
        }

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
