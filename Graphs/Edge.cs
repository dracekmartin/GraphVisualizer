using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Graphs
{
    public class Edge : GraphObject
    {
        public Vertex Start;
        public Vertex End;
        public bool Direction;
        public Color TextColor;
        public int Width;
        public int ResidualValue;
        private int hidden_value;
        public int Value
        {
            get
            {
                if (Canvas.euclidean)
                {
                    return ((int)Math.Floor(Math.Sqrt((Math.Pow(Start.Position.X - End.Position.X, 2) + (Math.Pow(Start.Position.Y - End.Position.Y, 2))))));
                }
                else
                {
                    return hidden_value;
                }
            }
            set
            {
                hidden_value = value;
            }
        }

        public Edge(FormMain init_canvas, Vertex init_start, Vertex init_end, bool init_direction, Color init_color, Color init_textColor, int init_value)
        {
            Color = init_color;
            Value = init_value;
            Text = init_value + "";

            Start = init_start;
            End = init_end;
            Direction = init_direction;
            TextColor = init_textColor;
            Width = 5;

            Canvas = init_canvas;
        }

        public void DrawEdge(Graphics g)
        {
            Pen p = new Pen(Color, Width);
            if (Direction == true)
            {
                AdjustableArrowCap aab = new AdjustableArrowCap(0.7f * Width, Width);
                p.CustomEndCap = aab;
                g.DrawLine(p, Start.Position, End.Position);
            }
            else
            {
                g.DrawLine(p, Start.Position, End.Position);
            }
        }

        public void DrawText(Graphics g)
        {
            g.DrawString(Text, new Font("Verdana", 10), new SolidBrush(TextColor), new Point((Start.Position.X + End.Position.X) / 2, (Start.Position.Y + End.Position.Y) / 2));
        }

        public bool Clicked(Point click)
        {
            if ((click.X > Start.Position.X && click.X > End.Position.X) ||
                (click.X < Start.Position.X && click.X < End.Position.X) ||
                (click.Y > Start.Position.Y && click.Y > End.Position.Y) ||
                (click.Y > Start.Position.Y && click.Y > End.Position.Y)) return false;
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
