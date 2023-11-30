using System.Drawing;
using System.Windows.Forms;

namespace GraphBuilder472
{
    internal class DrawItems
    {
        private static Bitmap bitmap;

        internal static void DrawMap(Graph graph, PictureBox pictureBox, bool draw, int k)
        {
            if (!draw) return;


            if (bitmap == null)
                bitmap = new Bitmap(pictureBox.Width, pictureBox.Height);


            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                graphics.Clear(Color.Black);


                foreach (Node _node in graph.graphTable.Keys)
                {
                    foreach (Node __node in graph.graphTable[_node].Keys)
                    {
                        int weight = graph.graphTable[_node][__node];
                        if (weight > 0)
                        {
                            graphics.DrawLine(Pens.White, _node.X, _node.Y, __node.X, __node.Y);

                            int _x = (_node.X + __node.X) / 2;
                            int _y = (_node.Y + __node.Y) / 2;


                            graphics.FillEllipse(Brushes.Black, _x - _node.D,
                            _y - _node.D,
                            _node.D * 2, _node.D * 2);


                            DrawNumber(bitmap, _x, _y, k, weight.ToString(), Pens.White);
                        }
                    }
                }


                for (int i = 0; i < graph.dataLink.Count; i++)
                {
                    if (graph.dataLink[i].isSelected)
                    {
                        graphics.FillEllipse(Brushes.Orange, graph.dataLink[i].X - graph.dataLink[i].D / 2,
                            graph.dataLink[i].Y - graph.dataLink[i].D / 2,
                            graph.dataLink[i].D, graph.dataLink[i].D);
                        //graphics.DrawEllipse(Pens.Orange, graph.dataLink[i].X - graph.dataLink[i].D / 2,
                        //    graph.dataLink[i].Y - graph.dataLink[i].D / 2,
                        //    graph.dataLink[i].D, graph.dataLink[i].D);
                    }
                    else
                    {
                        graphics.FillEllipse(Brushes.FloralWhite, graph.dataLink[i].X - graph.dataLink[i].D / 2,
                            graph.dataLink[i].Y - graph.dataLink[i].D / 2,
                            graph.dataLink[i].D, graph.dataLink[i].D);
                    }

                    DrawNumber(bitmap, graph.dataLink[i].X, graph.dataLink[i].Y, k, (i + 1).ToString(), Pens.Black);
                }
            }

            pictureBox.Image = bitmap;


        }

        

        private static void DrawDigit(Bitmap bitmap, int x, int y, int k, string digit, Pen pen)
        {
            Point[] points = null;

            switch (digit)
            {
                case "0":

                    points = new Point[]
                    {
                        new Point(x, y),
                        new Point(x + k, y),
                        new Point(x + k, y + 2 * k),
                        new Point(x, y + 2 * k),
                        new Point(x, y),
                    };

                    break;
                case "1":

                    points = new Point[]
                    {
                        new Point(x, y + k),
                        new Point(x + k, y),
                        new Point(x + k, y + 2 * k),
                    };

                    
                    break;
                case "2":

                    points = new Point[]
                    {
                        new Point(x, y),
                        new Point(x + k, y),
                        new Point(x + k, y + k),
                        new Point(x, y + k),
                        new Point(x, y + 2 * k),
                        new Point(x + k, y + 2 * k),
                    };

                    break;
                case "3":

                    points = new Point[]
                    {
                        new Point(x, y),
                        new Point(x + k, y),
                        new Point(x + k, y + k),
                        new Point(x, y + k),
                        new Point(x + k, y + k),
                        new Point(x + k, y + 2 * k),
                        new Point(x, y + 2 * k),
                        
                    };

                    break;
                case "4":

                    points = new Point[]
                    {
                        new Point(x, y),
                        new Point(x, y + k),
                        new Point(x + k, y + k),
                        new Point(x + k, y),
                        new Point(x + k, y + 2 * k),

                    };

                    break;
                case "5":

                    points = new Point[]
                    {
                        new Point(x + k, y),
                        new Point(x, y),
                        new Point(x, y + k),
                        new Point(x + k, y + k),
                        new Point(x + k, y + 2 * k),
                        new Point(x, y + 2 * k),

                    };

                    break;
                case "6":

                    points = new Point[]
                    {
                        new Point(x + k, y),
                        new Point(x, y),
                        new Point(x, y + 2 * k),
                        new Point(x + k, y + 2 * k),
                        new Point(x + k, y + k),
                        new Point(x, y + k),

                    };

                    break;
                case "7":

                    points = new Point[]
                    {
                        new Point(x, y),
                        new Point(x + k, y),
                        new Point(x, y + 2 * k),
                    };

                    break;
                case "8":

                    points = new Point[]
                    {
                        new Point(x, y + k),
                        new Point(x, y),
                        new Point(x + k, y),
                        new Point(x + k, y + k),
                        new Point(x, y + k),
                        new Point(x, y + 2 * k),
                        new Point(x + k, y + 2 * k),
                        new Point(x + k, y + k),
                        new Point(x, y + k),

                    };

                    break;
                case "9":

                    points = new Point[]
                    {
                        new Point(x + k, y + k),
                        new Point(x, y + k),
                        new Point(x, y),
                        new Point(x + k, y),
                        new Point(x + k, y + 2 * k),
                        new Point(x, y + 2 * k),

                    };

                    break;
            }


            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                graphics.DrawLines(pen, points);
            }
        }

        internal static void DrawNumber(Bitmap bitmap, int x, int y, int k, string number, Pen pen)
        {

            int n = number.Length;
            int dx = -k * (3 * n - 2) / 2;
            int dy = -k;

            for (int i = 0; i < n; i++)
            {
                DrawDigit(bitmap, x + dx + 2 * k * i, y + dy, k, number[i].ToString(), pen);
            }
        }

    }
}
