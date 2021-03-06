﻿using System;
using System.Drawing;
using System.Collections.Generic;

namespace Graphs
{
    public class Node : GraphObject
    {
        public Point Position;   
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

        public Node(Point init_position, Color init_color, int init_size, int init_textSize)
        {
            Color = init_color;
            Text = "";
            Value = int.MaxValue;
            Position = init_position;
            NodeEdges = new List<Edge>();
            Visited = false;
            PartOfSubset = this;
            ImportantEdge = null;
            Size = init_size;
            TextSize = init_textSize;
        }

        public void DrawNode(Graphics g, int canvasStartX, int canvasStartY)
        {
            g.FillEllipse(new SolidBrush(Color), new Rectangle(Position.X - Size + canvasStartX, Position.Y - Size + canvasStartY, 2 * Size, 2 * Size));
        }

        public void DrawText(Graphics g, int canvasStartX, int canvasStartY)
        {
            Brush b = new SolidBrush(Color);
            g.DrawString(Text, new Font("Verdana", TextSize), b, Position.X + Size + canvasStartX, Position.Y + Size + canvasStartY);
        }

        public bool Clicked(Point click, int canvasStartX, int canvasStartY)
        {
            click = new Point(click.X - canvasStartX, click.Y - canvasStartY);
            int diffx = Position.X - click.X;
            int diffy = Position.Y - click.Y;
            if (Math.Sqrt(diffx * diffx + diffy * diffy) <= Size)
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

