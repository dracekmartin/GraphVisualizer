using System;
using System.Drawing;
using System.Collections.Generic;

namespace Graphs
{
    class Vertex : GraphObject
    {
        public List<Edge> VertexEdges;
        public bool Visited;
        public int Radius;
        public Point Position;

        public Vertex(Point init_position, Color init_color)
        {
            Color = init_color;
            Value = int.MaxValue;
            Text = "∞";

            Position = init_position;
            VertexEdges = new List<Edge>();
            Radius = 5;
            Visited = false;
        }

        public void DrawVertex(Graphics g)
        {
            g.FillEllipse(new SolidBrush(Color), new Rectangle(Position.X - Radius, Position.Y - Radius, 2 * Radius, 2 * Radius));
        }

        public void DrawText(Graphics g)
        {
            Brush b = new SolidBrush(Color);
            g.DrawString(Text, new Font("Verdana", 10), b, Position.X + 2, Position.Y + 2);
        }

        public bool Clicked(Point click)
        {
            int diffx = Position.X - click.X;
            int diffy = Position.Y - click.Y;
            if (Math.Sqrt(diffx * diffx + diffy * diffy) <= Radius)
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

