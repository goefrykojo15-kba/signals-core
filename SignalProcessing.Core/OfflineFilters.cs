using System.Linq;

namespace SignalParser2
{
    // Moving-average filter that needs the whole signal at once
    public static class OfflineFilter
    {
        public static float[] Filter(float[] data, int windowSize)
        {
            if (data.Length == 0 || windowSize <= 1)
                return data.ToArray(); // nothing to do: return a copy

            int m = (windowSize - 1) / 2;

            // Extend: m copies of the first value + data + m copies of the last value
            float[] extended = Enumerable.Repeat(data[0], m)
            .Concat(data)
            .Concat(Enumerable.Repeat(data[data.Length - 1], m))
            .ToArray();

            // y[i] = average of extended[i .. i + windowSize - 1]
            return Enumerable.Range(0, data.Length)
            .Select(i => extended.Skip(i).Take(windowSize).Average())
            .ToArray();
        }
    }
}