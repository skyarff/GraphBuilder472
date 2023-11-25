using System;
using System.Collections.Generic;
using System.IO;
using System.Web.UI.WebControls;

namespace GraphBuilder472
{


    class Graph
    {

        private int cursor = 0;
        private List<List<int>> adjacencyList;
        private List<bool> reviewList;
        
        internal List<int> shortWayList;
        internal List<int> prevList;
        internal Dictionary<Node, int> dataLink;
        



        public void Add()
        {

        }


        public int this[int row, int col]
        {
            get
            {
                return adjacencyList[row][col];
            }

            set
            {
                adjacencyList[row][col] = value;
            }
        }

       


        public void CreateGraphTable(Dictionary<Node, int> nodes)
        {
            adjacencyList = new List<List<int>>();
            dataLink = nodes;
            cursor = nodes.Keys.Count;

            for (int i = 0; i < nodes.Count; i++)
            {
                List<int> inner = new List<int>();
                adjacencyList.Add(inner);
                for (int j = 0; j < nodes.Count; j++)
                {
                    inner.Add(0);
                }
            }

        }

        public Graph(Dictionary<Node, int> nodes)
        {
            CreateGraphTable(nodes);
        
            shortWayList = new List<int>();
            prevList = new List<int>();
            reviewList = new List<bool>();

            for (int i = 0; i < nodes.Count; i++)
            {
                shortWayList.Add(-1);
                prevList.Add(-1);
                reviewList.Add(false);
            }
        }


        public void Dijkstra(int index)
        {

            shortWayList[index] = 0;
            reviewList[index] = true;

            for (int i = 0; i < adjacencyList.Count; i++)
            {
                int minIndex = -1;

                for (int k = 0; k < adjacencyList.Count; k++)
                {
                    if (reviewList[k])
                    {
                        if (minIndex == -1 || shortWayList[k] < shortWayList[minIndex])
                        {
                            minIndex = k;
                        }
                    }
                }


                for (int j = 0; j < adjacencyList.Count; j++)
                {
                    

                    if (adjacencyList[minIndex][j] > 0)
                    {
                        if (shortWayList[j] == -1 || shortWayList[j] > adjacencyList[minIndex][j] + shortWayList[minIndex])
                        {
                            shortWayList[j] = adjacencyList[minIndex][j] + shortWayList[minIndex];
                            prevList[j] = minIndex;
                            reviewList[j] = true;

                        }
                    }
                }
                reviewList[minIndex] = false;
            }
        }



    }
}
