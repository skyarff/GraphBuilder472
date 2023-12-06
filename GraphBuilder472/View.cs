using System;
using System.Drawing;
using System.Windows.Forms;

namespace GraphBuilder472
{
    internal class DrawItems
    {
        private static Bitmap bitmap;
        private static Pen penWhite = new Pen(Color.White, 2);
        private static Pen penBlack = new Pen(Color.Black, 2);

        internal static void DrawMap(Graph graph, PictureBox pictureBox, bool draw, int k, NodeStack nodeStack)
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

                            int _x = _node.X + (__node.X - _node.X) * 2 / 3;
                            int _y = _node.Y + (__node.Y - _node.Y) * 2 / 3;
                        }
                    }
                }


                foreach (Node _node in graph.graphTable.Keys)
                {
                    foreach (Node __node in graph.graphTable[_node].Keys)
                    {
                        int weight = graph.graphTable[_node][__node];
                        if (weight > 0)
                        {
                            int _x = _node.X + (__node.X - _node.X) * 2 / 3;
                            int _y = _node.Y + (__node.Y - _node.Y) * 2 / 3;

                            graphics.FillEllipse(Brushes.Black, _x - _node.D * 3 / 4,
                            _y - _node.D * 3 / 4,
                            _node.D * 3 / 2, _node.D * 3 / 2);

                            DrawNumber(bitmap, _x, _y, k, weight.ToString(), Pens.White);

                            if (true)
                            {
                                int c1 = 33;
                                int c2 = 6;

                                float xa = __node.X - _node.X;
                                float ya = __node.Y - _node.Y;

                                float r = (float)Math.Sqrt(xa * xa + ya * ya);

                                float deltaX = (xa * c1 - ya * c2) / r;
                                float deltaY = (xa * c2 + c1 * ya) / r;

                                float nx = xa * (_node.D / r) / 2;
                                float ny = ya * (_node.D / r) / 2;

                                graphics.DrawLine(Pens.White, __node.X - nx, __node.Y - ny, __node.X - deltaX, __node.Y - deltaY);

                                c2 = -c2;
                                deltaX = (xa * c1 - ya * c2) / r;
                                deltaY = (xa * c2 + c1 * ya) / r;

                                graphics.DrawLine(Pens.White, __node.X - nx, __node.Y - ny, __node.X - deltaX, __node.Y - deltaY);

                            }
                        }
                    }
                }



                foreach (Node _node in graph.graphTable.Keys)
                {
                    if (nodeStack.Length > 0 && _node == nodeStack[0])
                    {
                        graphics.FillEllipse(Brushes.Red, _node.X - _node.D / 2,
                             _node.Y - _node.D / 2,
                             _node.D, _node.D);

                        DrawNumber(bitmap, _node.X + _node.D / 3, _node.Y + _node.D / 3, 5, (1).ToString(), penWhite);
                    }
                    else if (nodeStack.Length > 1 && _node == nodeStack[1])
                    {
                        graphics.FillEllipse(Brushes.Red, _node.X - _node.D / 2,
                             _node.Y - _node.D / 2,
                             _node.D, _node.D);

                        DrawNumber(bitmap, _node.X + _node.D / 3, _node.Y + _node.D / 3, 5, (2).ToString(), penWhite);
                    }
                    else
                    {
                        graphics.FillEllipse(Brushes.Red, _node.X - _node.D / 2,
                            _node.Y - _node.D / 2,
                            _node.D, _node.D);
                    }


                    DrawNumber(bitmap, _node.X, _node.Y, 5, _node.name, penBlack);
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
            int dx = - k * (2 * n - 1) / 2;
            int dy = -k;


            for (int i = 0; i < n; i++)
            {
                DrawDigit(bitmap, x + dx + i * k * 2 , y + dy, k, number[i].ToString(), pen);
            }
        }




    }
}
