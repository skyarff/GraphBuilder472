using System;
using System.Collections.Generic;
using System.IO;
using System.Web.UI.WebControls;

namespace GraphBuilder472
{


    class Graph
    {


        
        private Dictionary<Node, bool> reviewList;
        private Dictionary<Node, Dictionary<Node, int>> graphTable;

        internal Dictionary<Node, int> shortWayList;
        internal Dictionary<Node, Node> prevList;

        internal Dictionary<Node, int> dataLink;


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



            foreach (Node _node in dataLink.Keys)
            {
                Dictionary<Node, int> inner = new Dictionary<Node, int>();
                graphTable.Add(_node, inner);


                foreach (Node __node in dataLink.Keys)
                {
                    inner.Add(__node, 0);
                }
            }
        }


        public void ResetDijkstra()
        {

            shortWayList = new Dictionary<Node, int>();
            prevList = new Dictionary<Node, Node>();
            reviewList = new Dictionary<Node, bool>();


            foreach (Node _node in dataLink.Keys)
            {
                shortWayList.Add(_node, -1);
                prevList.Add(_node, minIndexCalibr);
                reviewList.Add(_node, false);
            }

        }


        public Graph()
        {
            //CreateGraphTable();
            //ResetDijkstra();
        }


        public void AddNode(Node node)
        {




            graphTable = new Dictionary<Node, Dictionary<Node, int>>();



            foreach (Node _node in dataLink.Keys)
            {
                Dictionary<Node, int> inner = new Dictionary<Node, int>();
                graphTable.Add(_node, inner);


                foreach (Node __node in dataLink.Keys)
                {
                    inner.Add(__node, 0);
                }
            }



            shortWayList = new Dictionary<Node, int>();
            prevList = new Dictionary<Node, Node>();
            reviewList = new Dictionary<Node, bool>();


            foreach (Node _node in dataLink.Keys)
            {
                shortWayList.Add(_node, -1);
                prevList.Add(_node, minIndexCalibr);
                reviewList.Add(_node, false);
            }

        }

        public void RemoveNode()
        {

        }

        public void Dijkstra(Node index)
         {
            
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
}
