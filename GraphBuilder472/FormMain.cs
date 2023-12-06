using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Web.UI.WebControls;
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
        private static int mouseUp = 0;


        private void FormMain_Load(object sender, EventArgs e)
        {
            timer1.Start();
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;





            try
            {
                string jsonGraph = File.ReadAllText("graph.json");
                graph = JsonConvert.DeserializeObject<Graph>(jsonGraph);

            }
            catch
            {

                File.Delete("graph.json");
                List<Node> nodes = new List<Node>
                {
                    new Node(200, 200, "1"),
                    new Node(600, 200, "2"),
                    new Node(200, 400, "3"),
                    new Node(600, 400, "4"),
                };
                graph = new Graph(nodes);
            }



            




        }


        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = 1;
            int mode = comboBox1.SelectedIndex == 0 ? 4 :
                (comboBox1.SelectedIndex == 1 ? 8 :
                (comboBox1.SelectedIndex == 2 ? 16 : -31));

            bool draw = Controller.StateHandler(e.X, e.Y, mouseDown, mouseUp, mode, graph, nodeStack);
            DrawItems.DrawMap(graph, pictureBox1, draw, 4, nodeStack);
        }
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            int mode = comboBox1.SelectedIndex == 0 ? 4 :
                (comboBox1.SelectedIndex == 1 ? 8 :
                (comboBox1.SelectedIndex == 2 ? 16 : -31));


            bool draw = Controller.StateHandler(e.X, e.Y, mouseDown, mouseUp, mode, graph, nodeStack);
            DrawItems.DrawMap(graph, pictureBox1, draw, 4, nodeStack);
        }
        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = 0;
            mouseUp = 2;
            int mode = comboBox1.SelectedIndex == 0 ? 4 :
                (comboBox1.SelectedIndex == 1 ? 8 :
                (comboBox1.SelectedIndex == 2 ? 16 : -31));

            bool draw = Controller.StateHandler(e.X, e.Y, mouseDown, mouseUp, mode, graph, nodeStack);
            mouseUp = 0;
            DrawItems.DrawMap(graph, pictureBox1, draw, 4, nodeStack);

        }


        private void button1_Click(object sender, EventArgs e)
        {

            if (nodeStack.Length == 2)
            {

                int c = textBox2.Text.Length > 0 ? Convert.ToInt32(textBox2.Text) : 0;

                if (comboBox2.SelectedIndex == 0)
                {
                    if (c > 0)
                    {
                        graph[nodeStack[0].name, nodeStack[1].name] = c;
                    }
                    else
                    {
                        graph.graphTable[nodeStack[0].name].Remove(nodeStack[1].name);
                    }
                }
                else if (comboBox2.SelectedIndex == 1)
                {
                    if (c > 0)
                    {
                        graph[nodeStack[0].name, nodeStack[1].name] = c;
                        graph[nodeStack[1].name, nodeStack[0].name] = c;
                    }
                    else
                    {
                        graph.graphTable[nodeStack[0].name].Remove(nodeStack[1].name);
                        graph.graphTable[nodeStack[1].name].Remove(nodeStack[0].name);
                    }
                }



                

                
                DrawItems.DrawMap(graph, pictureBox1, true, 4, nodeStack);
            }


        }
        private void button4_Click(object sender, EventArgs e)
        {

            if (nodeStack.Length == 2)
            {
                graph.Dijkstra(nodeStack[0].name);


                Node tempNodeOne = nodeStack[1];

                if (!graph.dataLink.ContainsKey(graph.prevList[tempNodeOne.name]))
                {
                    textBox1.Text = "-1";
                    textBox3.Text = "";
                    return;
                }
                
                Node tempNodeTwo = graph.dataLink[graph.prevList[tempNodeOne.name]];
                string str = "";

               
                while (true)
                {

                    using (Graphics graphics = pictureBox1.CreateGraphics())
                    {
                        graphics.DrawLine(Pens.Blue, tempNodeOne.X, tempNodeOne.Y, tempNodeTwo.X, tempNodeTwo.Y);
                    }

                    str = $"{tempNodeOne.name}-" + str;



                    tempNodeOne = tempNodeTwo;

                    if (graph.prevList[tempNodeOne.name] == "-1") break;

                    tempNodeTwo = graph.dataLink[graph.prevList[tempNodeOne.name]];
 
                }

                textBox1.Text = graph.shortWayList[nodeStack[1].name].ToString();

                str = $"{tempNodeOne.name}-" + str;
                textBox3.Text = str.Substring(0, str.Length - 1);
            }
        } 

        private void timer1_Tick(object sender, EventArgs e)
        {
            DrawItems.DrawMap(graph, pictureBox1, true, 4, nodeStack);
            timer1.Stop();
        }

        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            string graphJSON = JsonConvert.SerializeObject(graph, Formatting.Indented);
            File.WriteAllText("graph.json", graphJSON);
        }

        private void button2_Click(object sender, EventArgs e)
        {

            textBox1.Text = "";
            textBox3.Text = "";

            graph = new Graph();

            DrawItems.DrawMap(graph, pictureBox1, true, 4, nodeStack);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string graphJSON = JsonConvert.SerializeObject(graph, Formatting.Indented);
            File.WriteAllText("graph.json", graphJSON);
        }
    }
}
