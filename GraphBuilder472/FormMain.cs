using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace GraphBuilder472
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }


        private static NodeStack nodeStack = new NodeStack();
        private static Graph graph;
        private static int mouseDown = 0;

        


        private void FormMain_Load(object sender, EventArgs e)
        {
            timer1.Start();
            comboBox1.SelectedIndex = 0;


            List<Node> nodes = new List<Node>
            {
                new Node(200, 200),
                new Node(600, 200),
                new Node(200, 400),
                new Node(600, 400),
            };

            graph = new Graph(nodes);

            

        }


        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = 1;
            int mode = comboBox1.SelectedIndex == 0 ? 2 : 
                (comboBox1.SelectedIndex == 1 ? 4 : 
                (comboBox1.SelectedIndex == 2 ? 8 : -15));

            bool draw = Controller.StateHandler(e.X, e.Y, mouseDown, mode, graph, nodeStack);
            DrawItems.DrawMap(graph, pictureBox1, draw, 5);
        }
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            int mode = comboBox1.SelectedIndex == 0 ? 2 :
                (comboBox1.SelectedIndex == 1 ? 4 :
                (comboBox1.SelectedIndex == 2 ? 8 : -15));


            bool draw = Controller.StateHandler(e.X, e.Y, mouseDown, mode, graph, nodeStack);
            DrawItems.DrawMap(graph, pictureBox1, draw, 5);
        }
        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = 0;
        }


        private void button1_Click(object sender, EventArgs e)
        {

            if (nodeStack.Length == 2)
            {

                int c = textBox2.Text.Length > 0 ? Convert.ToInt32(textBox2.Text) : 0;


                if (c > 0)
                {
                    graph[nodeStack[0], nodeStack[1]] = c;
                    graph[nodeStack[1], nodeStack[0]] = c;
                }
                else
                {
                    graph.graphTable[nodeStack[0]].Remove(nodeStack[1]);
                    graph.graphTable[nodeStack[1]].Remove(nodeStack[0]);
                }

                
                DrawItems.DrawMap(graph, pictureBox1, true, 5);
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

        private void timer1_Tick(object sender, EventArgs e)
        {
            DrawItems.DrawMap(graph, pictureBox1, true, 5);
            timer1.Stop();
        }

    }
}
