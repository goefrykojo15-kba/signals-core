using System;

namespace SignalParser2
{
    // Detects pieces of a signal that leave the allowed range [min, max]
    public class SingleChannelRangeAnalyzer
    {
        private readonly float min;
        private readonly float max;
        private readonly int minLength; // minimum duration, in samples

        // Raised for every out-of-range piece that is long enough
        public event EventHandler<OutOfRangeEventArgs> OutOfRange;

        public SingleChannelRangeAnalyzer(float min, float max, int minLength)
        {
            this.min = min;
            this.max = max;
            this.minLength = minLength < 1 ? 1 : minLength;
        }

        public void Analyze(float[] data)
        {
            int start = -1; // -1 means "currently inside the range"

            for (int i = 0; i < data.Length; i++)
            {
                bool outside = data[i] < min || data[i] > max;

                if (outside && start < 0)
                {
                    start = i; // a piece outside the range begins
                }
                else if (!outside && start >= 0)
                {
                    Report(start, i - 1); // the piece ended at the previous sample
                    start = -1;
                }
            }

            // Signal ended while still outside the range
            if (start >= 0)
                Report(start, data.Length - 1);
        }

        private void Report(int start, int end)
        {
            int length = end - start + 1;
            if (length >= minLength)
                OutOfRange?.Invoke(this, new OutOfRangeEventArgs(start, end));
        }
    }
}