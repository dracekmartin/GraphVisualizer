using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs
{
    class ValueChangingStep : Step
    {

        public int value { get; set; }

        public ValueChangingStep(GraphObject init_go, int init_value)
        {
            go = init_go;
            value = init_value;
        }

        public void Complete()
        {
            go.value = value;
        }
    }
}
