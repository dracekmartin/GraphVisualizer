using System;
using System.Drawing;
using System.Collections.Generic;

namespace Graphs
{
    class Vertex : GraphObject
    {
        public List<Edge> vertexEdges { get; set; }
        public bool visited { get; set; }
        public Point position { get; set; }
        public int radius { get; set; }
        public Vertex subtreeParent { get; set; }
        public Edge smallestSubtreeEdge { get; set; }


        public Vertex(Point init_position, Color init_color, int init_value = int.MaxValue, int init_text = int.MaxValue, int init_radius = 5)
        {
            position = init_position;
            color = init_color;
            vertexEdges = new List<Edge>();
            value = init_value;
            text = init_text;
            radius = init_radius;
            subtreeParent = this;
            smallestSubtreeEdge = null;
        }

        public void DrawVertex(Graphics g)
        {
            g.FillEllipse(new SolidBrush(color), new Rectangle(position.X - radius, position.Y - radius, 2 * radius, 2 * radius));
        }

        public void DrawText(Graphics g)
        {
            Brush b = new SolidBrush(color);
            if (text != int.MaxValue)
            {
                g.DrawString(text + "", new Font("Verdana", 10), b, position.X + 2, position.Y + 2);
            }
            else
            {
                g.DrawString("∞", new Font("Verdana", 10), b, position.X + 2, position.Y + 2);
            }
        }

        public bool Clicked(Point click)
        {
            int diffx = position.X - click.X;
            int diffy = position.Y - click.Y;
            if (Math.Sqrt(diffx * diffx + diffy * diffy) <= radius)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Vertex getSubtreeParent()
        {
            if(subtreeParent == this)
            {
                return this;
            }
            else
            {
                subtreeParent = subtreeParent.getSubtreeParent();
                return subtreeParent;
            }
        }
    }
}

