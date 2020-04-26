using System.Drawing;

namespace Graphs
{
    class ColorChangingStep : Step
    {
        public Color color { get; set; }

        public ColorChangingStep(GraphObject init_go, Color init_color)
        {
            go = init_go;
            color = init_color;
        }

        public void Complete()
        {
            go.color = color;
        }
    }
}
