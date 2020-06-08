using System;
using System.Linq;
using Xunit;

namespace LocationExtreme
{
    public class LotteryNumberSetPrinterTests
    {
        [Fact]
        public void SetPrinter_Prints_OnesSet()
        {
            // Setup
            LotteryNumberSetPrinter printer = new LotteryNumberSetPrinter();
            LotteryNumberSet set = new LotteryNumberSet
            {
                Balls = Enumerable.Range(0, LotteryNumberSet.BallCount)
                                  .Select(_ => 1u)
                                  .ToArray()
            };

            // Act
            var formatted = printer.Format(set);

            // Assert
            Assert.True(
                // Try to read each number from the corresponding print instruction,
                //  then check the values correspond, and the count is correct
                (from instruction in formatted
                 where instruction is LotteryNumberSetPrinter.WriteTextPrintInstruction writeText
                    && Int32.TryParse($"{writeText.Text[writeText.Text.Count() - 1]}", out var ballNumber)
                    && ballNumber == 1
                 select (bool?)null).Count() == set.Balls.Count()
            );
        }
    }
}
