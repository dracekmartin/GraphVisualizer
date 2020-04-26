using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Graphs
{
    class Edge : GraphObject
    {
        public Vertex start { get; set; }
        public Vertex end { get; set; }
        public int direction { get; set; }
        public Color textColor { get; set; }
        public int width { get; set; }

        public Edge(Vertex init_start, Vertex init_end, int init_direction, Color init_color, Color init_textColor, int init_value = 1, int init_text = 1, int init_width = 5)
        {
            start = init_start;
            end = init_end;
            direction = init_direction;
            color = init_color;
            textColor = init_textColor;
            value = init_value;
            text = init_text;
            width = init_width;
        }

        public void DrawEdge(Graphics g)
        {
            Pen p = new Pen(color, width);

            g.DrawLine(p, start.position, end.position);

            AdjustableArrowCap aab = new AdjustableArrowCap(0.7f * width, width);
            if (direction == 1)
            {
                p.CustomEndCap = aab;
                g.DrawLine(p, start.position, end.position);
            }
            else if (direction == -1)
            {
                p.CustomStartCap = aab;
                g.DrawLine(p, start.position, end.position);
            }
            else
            {
                g.DrawLine(p, start.position, end.position);
            }
        }

        public void DrawText(Graphics g)
        {
            g.DrawString(text + "", new Font("Verdana", 10), new SolidBrush(textColor), new Point((start.position.X + end.position.X) / 2, (start.position.Y + end.position.Y) / 2));
        }

        public bool Clicked(Point click)
        {
            if ((click.X > start.position.X && click.X > end.position.X) ||
                (click.X < start.position.X && click.X < end.position.X) ||
                (click.Y > start.position.Y && click.Y > end.position.Y) ||
                (click.Y > start.position.Y && click.Y > end.position.Y)) return false;
            float diffX = end.position.X - start.position.X;
            float diffY = end.position.Y - start.position.Y;
            float distance = (float)
                (Math.Abs(diffY * click.X - diffX * click.Y + end.position.X * start.position.Y - end.position.Y * start.position.X)
                /
                Math.Sqrt(diffY * diffY + diffX * diffX));
            return distance <= width / 2f;
        }
    }
}
