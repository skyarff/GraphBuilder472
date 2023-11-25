using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace GraphBuilder472
{
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
                //twoNodes[count - 2].isSelected = false; //0

                ////twoNodes[count - 1] = twoNodes[count - 2]; //1

                //twoNodes[count - 2] = node; //0



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
