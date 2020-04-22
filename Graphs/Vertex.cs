using System;
using System.Collections.Generic;
using System.Drawing;

namespace Graphs
{
    class Vertex : GraphObject
    {
        public Point position { get; set; }
        public List<Edge> vertexEdges { get; set; }
        public int radius { get; set; }
        public bool visited { get; set; }

        public Vertex(Point init_position, Color init_color, int init_value = int.MaxValue, int init_radius = 5)
        {
            position = init_position;
            color = init_color;
            vertexEdges = new List<Edge>();
            value = init_value;
            radius = init_radius;
        }

        public void DrawVertex(Graphics g)
        {
            g.FillEllipse(new SolidBrush(color), new Rectangle(position.X - radius, position.Y - radius, 2 * radius, 2 * radius));
        }

        public void DrawText(Graphics g)
        {
            Brush b = new SolidBrush(color);
            if (value != int.MaxValue)
            {
                g.DrawString(value + "", new Font("Verdana", 10), b, position.X + 2, position.Y + 2);
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
    }
}

