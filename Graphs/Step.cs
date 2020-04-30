using System;
using System.Drawing;

namespace Graphs
{
    class Step
    {
        public GraphObject Go;
        public Color Color;
        public String Text;
        private int StepType;

        public Step(GraphObject init_go, Color init_color)
        {
            Go = init_go;
            Color = init_color;
            StepType = 1;
        }
        public Step(GraphObject init_go, String init_text)
        {
            Go = init_go;
            Text = init_text;
            StepType = 2;
        }
        public Step(GraphObject init_go, Color init_color, String init_text)
        {
            Go = init_go;
            Color = init_color;
            Text = init_text;
            StepType = 3;
        }

        public void Complete()
        {
            if(StepType == 1)
            {
                Go.Color = Color;
            }
            else if (StepType == 2)
            {
                Go.Text = Text;
            }
            else
            {
                Go.Color = Color;
                Go.Text = Text;
            }
        }
    }
}
