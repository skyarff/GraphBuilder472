using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Drawing;
using System.Collections.Generic;
using System.Xml.Linq;

namespace GraphBuilder472
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        //private static List<Node> nodes;

        private static Dictionary<string, int> state = new Dictionary<string, int>()
        {
            ["MouseDown"] = 0,
            ["MouseUp"] = 0,
        };
        private static NodeStack nodeStack = new NodeStack();
        private static bool _lock = false;

        private static Graph graph;


        private void FormMain_Load(object sender, EventArgs e)
        {
            timer1.Start();
            comboBox1.SelectedIndex = 0;



            //nodes = new Dictionary<Node, int>
            //{
            //    [new Node(100, 100)] = 0,
            //    [new Node(200, 200)] = 1,
            //    [new Node(300, 300)] = 2,
            //    [new Node(400, 400)] = 3,
            //};

            //graph = new Graph(nodes);

            graph = new Graph();

            graph.dataLink = new Dictionary<Node, int>
            {
                [new Node(100, 100)] = 0,
                [new Node(200, 200)] = 1,
                [new Node(300, 300)] = 2,
                [new Node(400, 400)] = 3,
            };

            graph.CreateGraphTable();
            graph.ResetDijkstra();



        }


        private static void DrawNode(Node node, PictureBox pictureBox)
        {
            using (Graphics graphics = pictureBox.CreateGraphics())
            {
                graphics.FillEllipse(Brushes.Black, node.X - node.D / 2, node.Y - node.D / 2, node.D, node.D);

                if (node.isSelected)
                {
                    graphics.FillEllipse(Brushes.Red, node.X - node.D / 2, node.Y - node.D / 2, node.D, node.D);
                    graphics.DrawEllipse(Pens.Yellow, node.X - node.D / 2, node.Y - node.D / 2, node.D, node.D);
                }
                else
                {
                    graphics.FillEllipse(Brushes.Red, node.X - node.D / 2, node.Y - node.D / 2, node.D, node.D);
                    graphics.DrawEllipse(Pens.Red, node.X - node.D / 2, node.Y - node.D / 2, node.D, node.D);
                }
            }
        }




        private static void StateHandler(int x, int y, PictureBox pictureBox, Dictionary<string, int> state, TextBox textbox, int mode, Graph graph)
        {

            
            foreach (int value in state.Values) mode += value;

            textbox.Text = mode.ToString();

            Node node = null;
            foreach (Node _node in graph.dataLink.Keys)
            {
                if (_node.X - _node.D / 2 <= x && _node.X + _node.D / 2 >= x)
                {
                    if (_node.Y -   _node.D / 2 <= y && _node.Y + _node.D / 2 >= y)
                    {
                        node = _node;
                    }
                }
            }

            

            switch (mode)
            {

                case 0:
                    break;
                case 1:
                    break;
                case 2:
                    break;
                case 3:

                    if (node != null)
                    {
                        node.X = x;
                        node.Y = y;

                        DrawMap(pictureBox, ref _lock);
                    }


                    break;
                case 4:
                    break;
                case 5:

                    if (node != null)
                    {
                        DrawMap(pictureBox, ref _lock);

                        if (!node.isSelected)
                        {
                            nodeStack.Add(node);
                        }
                        else
                        {
                            nodeStack.Remove(node);
                        }
                    }

                    break;
                case 6:
                    break;
                case 9:

                    if (node == null)
                    {
                        Node newNode = new Node(x, y);

                        graph.dataLink.Add(newNode, 0);
 
                    }
                    else
                    { 
                        graph.dataLink.Remove(node);
                    }


                    foreach (Node _node in graph.dataLink.Keys)
                    {
                        _node.relatedNodes = new List<Node>();
                    }


                    graph.CreateGraphTable();
                    graph.ResetDijkstra();



                    DrawMap(pictureBox, ref _lock);

                    break;

            }
        }
        

        private static void DrawMap(PictureBox pictureBox, ref bool _lock)
        {
            if (_lock) return;
            _lock = true;
            (new Thread(() =>
            {

                using (Graphics graphics = pictureBox.CreateGraphics())
                {
                    graphics.Clear(Color.Black);



                    foreach (Node _node in graph.dataLink.Keys)
                    {
                        for (int j = 0; j < _node.RelatedNodes.Count; j++)
                        {
                            graphics.DrawLine(Pens.White, _node.X, _node.Y, _node.RelatedNodes[j].X, _node.RelatedNodes[j].Y);
                        }
                    }


                    foreach (Node _node in graph.dataLink.Keys)
                    {
                        if (_node.isSelected)
                        {
                            graphics.FillEllipse(Brushes.Red, _node.X - _node.D / 2, _node.Y - _node.D / 2, _node.D, _node.D);
                            graphics.DrawEllipse(Pens.Yellow, _node.X - _node.D / 2, _node.Y - _node.D / 2, _node.D, _node.D);
                        }
                        else
                        {
                            graphics.FillEllipse(Brushes.Red, _node.X - _node.D / 2, _node.Y - _node.D / 2, _node.D, _node.D);
                        }
                    }


        
                }

                
            })).Start();
            _lock = false;
        }



        private void timer1_Tick(object sender, EventArgs e)
        {
            DrawMap(pictureBox1, ref _lock);

            timer1.Stop();
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {

            //state["mouseMove"] = 4;
            int mode = comboBox1.SelectedIndex == 0 ? 2 :
                (comboBox1.SelectedIndex == 1 ? 4 :
                (comboBox1.SelectedIndex == 2 ? 8 : -15));


            StateHandler(e.X, e.Y, pictureBox1, state, textBox1, mode, graph);
            //DrawMap(pictureBox1, ref _lock);
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            //state["mouseUp"] = 2;
            state["mouseDown"] = 0;
            //StateHandler(e.X, e.Y, pictureBox1, state);
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {

            state["mouseDown"] = 1;
            int mode = comboBox1.SelectedIndex == 0 ? 2 : 
                (comboBox1.SelectedIndex == 1 ? 4 : 
                (comboBox1.SelectedIndex == 2 ? 8 : -15));

            StateHandler(e.X, e.Y, pictureBox1, state, textBox1, mode, graph);
            //DrawMap(pictureBox1, ref _lock);
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (nodeStack.Length == 2)
            {

                int a = graph.dataLink[nodeStack[0]];
                int b = graph.dataLink[nodeStack[1]];
                int c = textBox2.Text.Length > 0 ? Convert.ToInt32(textBox2.Text) : 0;


                graph[nodeStack[0], nodeStack[1]] = c;
                graph[nodeStack[1], nodeStack[0]] = c;


                nodeStack[0].RelatedNodes.Add(nodeStack[1]);
                nodeStack[1].RelatedNodes.Add(nodeStack[0]);

                DrawMap(pictureBox1, ref _lock);
            }

        }


        private void button2_Click_1(object sender, EventArgs e)
        {
            if (nodeStack.Length == 1)
            {
                graph.Dijkstra(nodeStack[0]);
            }
        }


        private void button5_Click(object sender, EventArgs e)
        {
            if (nodeStack.Length == 1)
            {
                Node tempNodeOne = nodeStack[0];
                Node tempNodeTwo = graph.prevList[tempNodeOne];



                while (tempNodeTwo != graph.minIndexCalibr)
                {
                    

                    using (Graphics graphics = pictureBox1.CreateGraphics())
                    {
                        graphics.DrawLine(Pens.Blue, tempNodeOne.X, tempNodeOne.Y, tempNodeTwo.X, tempNodeTwo.Y);
                    }

                    tempNodeOne = tempNodeTwo;
                    tempNodeTwo = graph.prevList[tempNodeOne];

                }


            }
        }


        private void button4_Click(object sender, EventArgs e)
        {

            if (nodeStack.Length == 2)
            {
                graph.Dijkstra(nodeStack[0]);


                Node tempNodeOne = nodeStack[1];
                Node tempNodeTwo = graph.prevList[tempNodeOne];


                while (tempNodeTwo != graph.minIndexCalibr)
                {


                    using (Graphics graphics = pictureBox1.CreateGraphics())
                    {
                        graphics.DrawLine(Pens.Blue, tempNodeOne.X, tempNodeOne.Y, tempNodeTwo.X, tempNodeTwo.Y);
                    }

                    tempNodeOne = tempNodeTwo;
                    tempNodeTwo = graph.prevList[tempNodeOne];

                }
            }

        }

        
    }
}
