using System.Collections.Generic;
using System.Linq;

namespace SignalParser2
{
    // Moving-average filter for data arriving in pieces
    public class OnlineFilter
    {
        private readonly int windowSize;

        // The last windowSize values seen so far (kept between calls)
        private readonly Queue<float> window = new Queue<float>();

        public OnlineFilter(int windowSize)
        {
            this.windowSize = windowSize < 1 ? 1 : windowSize;
        }

        public float[] Filter(float[] data)
        {
            float[] result = new float[data.Length];

            for (int i = 0; i < data.Length; i++)
            {
                // Very first sample of the signal: no history yet,
                // so pretend the past was equal to this value
                if (window.Count == 0)
                {
                    for (int j = 0; j < windowSize - 1; j++)
                        window.Enqueue(data[i]);
                }

                window.Enqueue(data[i]); // add newest value
                if (window.Count > windowSize)
                    window.Dequeue(); // drop oldest value

                result[i] = window.Average(); // average of the last windowSize values
            }

            return result;
        }
    }
}