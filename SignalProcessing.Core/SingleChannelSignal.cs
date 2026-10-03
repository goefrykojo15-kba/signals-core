using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SignalParser1
{
    public class SingleChannelSignal
    {
        public float Frequency { get; set; }
        public float[] Values { get; set; }
        public float[] TimeTicks { get
            {
                float[] timeticks = new float[Values.Length];
                for (int i = 0; i < Values.Length; i++)
                    timeticks[i] = i / Frequency;
                return timeticks;
            }
        }

        public SingleChannelSignal(float frequencyVal, float[] valuesVal)
        {
            Frequency = frequencyVal;
            Values = valuesVal;
    }
    }
        
}
