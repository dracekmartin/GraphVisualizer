using System;
using System.Drawing;
using System.Windows.Forms;

namespace Graphs
{
    public class Step
    {
        public GraphObject Go;
        public Color Color;
        public string Text;
        

        public Step StepBefore;
        public Step StepAfter;
        public bool Reversed;

        public Step(GraphObject init_go, Color init_color)
        {
            Go = init_go;
            Color = init_color;
            Text = null;
            Reversed = false;
        }
        public Step(GraphObject init_go, string init_text)
        {
            Go = init_go;
            Text = init_text;
            Color = Color.Empty;
            Reversed = false;
        }
        public Step(GraphObject init_go, Color init_color, string init_text)
        {
            Go = init_go;
            Color = init_color;
            Text = init_text;
            Reversed = false;
        }

        public void Complete()
        {
            if(Text == null)
            {
                Color tempColor = Go.Color;
                Go.Color = Color;
                Color = tempColor;
            }
            else if (Color == Color.Empty)
            {
                string tempText = Go.Text;
                Go.Text = Text;
                Text = tempText;
            }
            else
            {
                Color tempColor = Go.Color;
                string tempText = Go.Text;
                Go.Color = Color;
                Go.Text = Text;
                Color = tempColor;
                Text = tempText;
            }
            Reversed = !Reversed;
        }
    }
}
