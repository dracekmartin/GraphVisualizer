using System;
using System.Drawing;
using System.Collections.Generic;
using System.Xml.Serialization;

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

        public int id;

        public Node(FormMain init_canvas, Point init_position, Color init_color)
        {
            Color = init_color;
            Text = "";

            Value = int.MaxValue;
            Position = init_position;
            NodeEdges = new List<Edge>();
            Radius = 5;
            Visited = false;

            PartOfSubset = this;
            ImportantEdge = null;        }

        public void DrawNode(Graphics g, int canvasStartX, int canvasStartY)
        {
            g.FillEllipse(new SolidBrush(Color), new Rectangle(Position.X - Radius + canvasStartX, Position.Y - Radius + canvasStartY, 2 * Radius, 2 * Radius));
        }

        public void DrawText(Graphics g, int canvasStartX, int canvasStartY)
        {
            Brush b = new SolidBrush(Color);
            g.DrawString(Text, new Font("Verdana", 10), b, Position.X + 2 + canvasStartX, Position.Y + 2 + canvasStartY);
        }

        public bool Clicked(Point click, int canvasStartX, int canvasStartY)
        {
            click = new Point(click.X - canvasStartX, click.Y - canvasStartY);
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

        public Node()
        {

        }
        
    }
}

