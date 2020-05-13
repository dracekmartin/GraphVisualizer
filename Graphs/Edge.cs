using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Graphs
{
    public class Edge : GraphObject
    {
        public Node Start;
        public Node End;
        public int Width;

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

        public bool Reverse;

        public bool Usable;

        

        public Edge(Node init_start, Node init_end, Color init_color, Color init_textColor, int init_value)
        {
            Color = init_color;
            Value = init_value;
            Text = init_value + "";

            Start = init_start;
            End = init_end;
            TextColor = init_textColor;
            Width = 5;
        }

        public void DrawEdge(Graphics g, bool directed, int canvasStartX, int canvasStartY)
        {
            Pen p = new Pen(Color, Width);
            if (directed == true)
            {
                AdjustableArrowCap aab = new AdjustableArrowCap(0.7f * Width, Width);
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
            g.DrawString(Text, new Font("Verdana", 12), new SolidBrush(TextColor), new Point((Start.Position.X + End.Position.X) / 2 + canvasStartX, (Start.Position.Y + End.Position.Y) / 2 + canvasStartY));
        }


        //WIP
        public bool Clicked(Point click, int canvasStartX, int canvasStartY)
        {
            click = new Point(click.X - canvasStartX, click.Y - canvasStartY);
            if ((click.X > Start.Position.X + Width && click.X > End.Position.X + Width) ||
                (click.X < Start.Position.X - Width && click.X < End.Position.X - Width) ||
                (click.Y > Start.Position.Y + Width && click.Y > End.Position.Y + Width) ||
                (click.Y < Start.Position.Y - Width && click.Y < End.Position.Y - Width)) return false;
            float diffX = End.Position.X - Start.Position.X;
            float diffY = End.Position.Y - Start.Position.Y;
            float distance = (float)
                (Math.Abs(diffY * click.X - diffX * click.Y + End.Position.X * Start.Position.Y - End.Position.Y * Start.Position.X)
                /
                Math.Sqrt(diffY * diffY + diffX * diffX));
            return distance <= Width / 2f;
        }
    }
}
