using System;
using System.Drawing;
using System.Collections.Generic;

namespace Graphs
{
    public class Node : GraphObject
    {
        public Point Position;
        public int Radius;

        public int Value;
        public List<Edge> NodeEdges;
        public bool Visited;
        private Node hidden_PartOfSubset;
        public Node PartOfSubset
        {
            get
            {
                if (hidden_PartOfSubset.Equals(this))
                {
                    return hidden_PartOfSubset;
                }
                else
                {
                    hidden_PartOfSubset = hidden_PartOfSubset.PartOfSubset;
                    return hidden_PartOfSubset;
                }
            }
            set
            {
                hidden_PartOfSubset = value;
            }
        }
        public Edge ImportantEdge;

        public Node(FormMain init_canvas, Point init_position, Color init_color)
        {
            Color = init_color;
            Value = int.MaxValue;
            Text = "∞";

            Position = init_position;
            NodeEdges = new List<Edge>();
            Radius = 5;
            Visited = false;

            PartOfSubset = this;
            ImportantEdge = null;

            Canvas = init_canvas;
        }

        public void DrawNode(Graphics g)
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

