using System.Linq;
using Xunit;

namespace LocationExtreme
{
    public class LotteryNumberSetTests
    {

        /// ref: Req 6.1.1
        [Fact]
        public void LotteryNumberSet_Generates_SixValidNumbers()
        {
            // Setup
            LotteryNumberSet set;

            // Act
            set = LotteryNumberSet.Create();

            // Assert
            Assert.True(set.Balls.All(num => num >= 0), "NonZero numbers should be generated"); // See also: unsigned
            Assert.True(set.Balls.All(num => num <= LotteryNumberSet.MaxNumber), "All numbers should be less than MaxNumber");
            Assert.Equal(set.Balls.Count(), LotteryNumberSet.BallCount);
        }

        /// ref: Req 6.1.2
        [Fact]
        public void LotteryNumberSet_Generates_UniqueNumbers()
        {
            // Setup
            LotteryNumberSet set;

            // Act
            set = LotteryNumberSet.Create();

            // Assert
            foreach (var index in Enumerable.Range(1, LotteryNumberSet.BallCount))
            {
                // Balls are a monotonically increasing set
                //  That means pairwise uniqueness inductively guarantees total uniqueness
                Assert.True(set.Balls[index - 1] != set.Balls[index], "Balls should be unique");
            }
        }

        /// ref: Req 6.1.3
        [Fact]
        public void LotteryNumberSet_Generates_OrderedNumbers()
        {
            // Setup
            LotteryNumberSet set;

            // Act
            set = LotteryNumberSet.Create();

            // Assert
            foreach (var index in Enumerable.Range(1, set.Balls.Count()))
            {
                Assert.True(set.Balls[index - 1] < set.Balls[index], "Balls should be ordered");
            }
        }
    }
}
