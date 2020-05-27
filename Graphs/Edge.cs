using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Graphs
{
    public class Edge : GraphObject
    {
        public Node Start;
        public Node End;
        public float ArrowSize;
        public bool Reverse;
        public bool Usable;
        public bool Euclidean = false;
        private int hidden_Value;
        public int Value
        {
            get
            {
                if (Euclidean)
                {
                    return ((int)Math.Floor(Math.Sqrt((Math.Pow(Start.Position.X - End.Position.X, 2) + (Math.Pow(Start.Position.Y - End.Position.Y, 2))))));
                }
                else
                {
                    return hidden_Value;
                }
            }
            set
            {
                hidden_Value = value;
            }
        }
        public int ResidualValue;


        public Edge(Node init_start, Node init_end, Color init_color, Color init_textColor, int init_value, int init_size, int init_textSize, float init_arrowSize)
        {
            Color = init_color;
            Value = init_value;
            Text = init_value + "";
            Start = init_start;
            End = init_end;
            TextColor = init_textColor;
            Size = init_size;
            TextSize = init_textSize;
            ArrowSize = init_arrowSize;
        }

        public void DrawEdge(Graphics g, bool directed, int canvasStartX, int canvasStartY)
        {
            Pen p = new Pen(Color, Size);
            if (directed == true)
            {
                AdjustableArrowCap aab = new AdjustableArrowCap(0.7f * ArrowSize, ArrowSize);
                p.CustomEndCap = aab;
                g.DrawLine(p, Start.Position.X + canvasStartX, Start.Position.Y + canvasStartY, End.Position.X + canvasStartX, End.Position.Y + canvasStartY);
            }
            else
            {
                g.DrawLine(p, Start.Position.X + canvasStartX, Start.Position.Y + canvasStartY, End.Position.X + canvasStartX, End.Position.Y + canvasStartY);
            }
        }

        public void DrawText(Graphics g, int canvasStartX, int canvasStartY)
        {
            g.DrawString(Text, new Font("Verdana", TextSize), new SolidBrush(TextColor), new Point((Start.Position.X + End.Position.X) / 2 + canvasStartX, (Start.Position.Y + End.Position.Y) / 2 + canvasStartY));
        }

        public bool Clicked(Point click, int canvasStartX, int canvasStartY)
        {
            click = new Point(click.X - canvasStartX, click.Y - canvasStartY);
            if ((click.X > Start.Position.X + Size && click.X > End.Position.X + Size) ||
                (click.X < Start.Position.X - Size && click.X < End.Position.X - Size) ||
                (click.Y > Start.Position.Y + Size && click.Y > End.Position.Y + Size) ||
                (click.Y < Start.Position.Y - Size && click.Y < End.Position.Y - Size)) return false;
            float diffX = End.Position.X - Start.Position.X;
            float diffY = End.Position.Y - Start.Position.Y;
            float distance = (float)
                (Math.Abs(diffY * click.X - diffX * click.Y + End.Position.X * Start.Position.Y - End.Position.Y * Start.Position.X)
                /
                Math.Sqrt(diffY * diffY + diffX * diffX));
            return distance <= Size / 2f;
        }
    }
}
