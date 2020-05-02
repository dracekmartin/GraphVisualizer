using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Graphs
{
    public class Edge : GraphObject
    {
        public Vertex Start;
        public Vertex End;
        public int Direction;
        public Color TextColor;
        public int Width;

        public Edge(Vertex init_start, Vertex init_end, int init_direction, Color init_color, Color init_textColor, int init_value)
        {
            Color = init_color;
            Value = init_value;
            Text = init_value + "";

            Start = init_start;
            End = init_end;
            Direction = init_direction;
            TextColor = init_textColor;
            Width = 5;
        }

        public void DrawEdge(Graphics g)
        {
            Pen p = new Pen(Color, Width);

            g.DrawLine(p, Start.Position, End.Position);

            AdjustableArrowCap aab = new AdjustableArrowCap(0.7f * Width, Width);
            if (Direction == 1)
            {
                p.CustomEndCap = aab;
                g.DrawLine(p, Start.Position, End.Position);
            }
            else if (Direction == -1)
            {
                p.CustomStartCap = aab;
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
