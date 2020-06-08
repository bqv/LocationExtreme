using System;
using System.Linq;
using System.Collections.Generic;

namespace LocationExtreme
{
    public class LotteryNumberSet
    {
        public LotteryNumberSet() : this(new Random())
        {
        }

        internal LotteryNumberSet(Random rng)
        {
            Rng = rng;
        }

        public const short MaxNumber = 49;

        public const short BallCount = 6;

        public uint[] Balls;

        // Presumably singleton
        private static Random Rng;

        private static int GenerateIntegerWithMax(int max) => (int)(Rng.NextDouble() * (max - 1)) + 1;

        private static IEnumerable<uint> GenerateLotteryNumbers()
        {
            var pool = Enumerable.Range(0, MaxNumber).ToList();

            foreach (var index in Enumerable.Range(0, BallCount))
            {
                int selectedIndex = GenerateIntegerWithMax(MaxNumber - index);
                uint value = (uint)pool.ElementAt(selectedIndex);
                pool.Remove(selectedIndex);
                yield return value;
            }
        }

        public static LotteryNumberSet Create()
        {
            return new LotteryNumberSet {
                Balls = GenerateLotteryNumbers().ToArray()
            };
        }
    }
}
