using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs
{
    class Step
    {
        public GraphObject go { get; set; }
        public Color color { get; set; }
        public int text { get; set; }

        public Step(GraphObject init_go, Color init_color, int init_text)
        {
            go = init_go;
            color = init_color;
            text = init_text;
        }

        public void Complete()
        {
            Console.WriteLine(text);
            go.color = color;
            go.text = text;
        }
    }
}
