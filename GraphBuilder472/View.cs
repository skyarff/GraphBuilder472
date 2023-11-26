using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace GraphBuilder472
{
    internal class DrawItems
    {
        internal static void DrawMap(Graph graph ,PictureBox pictureBox, bool draw, ref bool _lock)
        {
            if (!draw || _lock) return;
            _lock = true;
            (new Thread(() =>
            {

                using (Graphics graphics = pictureBox.CreateGraphics())
                {
                    graphics.Clear(Color.Black);


                    foreach (Node _node in graph.graphTable.Keys)
                    {
                        foreach (Node __node in graph.graphTable[_node].Keys)
                        {
                            if (graph.graphTable[_node][__node] > 0)
                            {
                                graphics.DrawLine(Pens.White, _node.X, _node.Y, __node.X, __node.Y);
                            }
                        }
                    }


                    for (int i = 0; i < graph.dataLink.Count; i++)
                    {
                        if (graph.dataLink[i].isSelected)
                        {
                            graphics.FillEllipse(Brushes.Red, graph.dataLink[i].X - graph.dataLink[i].D / 2,
                                graph.dataLink[i].Y - graph.dataLink[i].D / 2,
                                graph.dataLink[i].D, graph.dataLink[i].D);
                            graphics.DrawEllipse(Pens.Yellow, graph.dataLink[i].X - graph.dataLink[i].D / 2,
                                graph.dataLink[i].Y - graph.dataLink[i].D / 2,
                                graph.dataLink[i].D, graph.dataLink[i].D);
                        }
                        else
                        {
                            graphics.FillEllipse(Brushes.Red, graph.dataLink[i].X - graph.dataLink[i].D / 2,
                                graph.dataLink[i].Y - graph.dataLink[i].D / 2,
                                graph.dataLink[i].D, graph.dataLink[i].D);
                        }
                    }
                }

            })).Start();
            _lock = false;
        }



    }
}
