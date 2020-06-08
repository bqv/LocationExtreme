using System;
using System.Linq;
using System.Collections.Generic;

namespace LocationExtreme
{
    public class LotteryNumberSetPrinter
    {
        public const string Prelude = "Your lottery numbers are:";

        public IDictionary<int, ConsoleColor> ColourMapping =>
            new Dictionary<int, ConsoleColor> {
                { 0, ConsoleColor.Gray },
                { 1, ConsoleColor.Blue },
                { 2, ConsoleColor.Magenta },
                { 3, ConsoleColor.Green },
                { 4, ConsoleColor.Yellow }
            };

        public IEnumerable<PrintInstruction> Format(LotteryNumberSet set)
        {
            var numbers = set.Balls
                             .OrderBy(x => x)
                             .GroupBy(num => (int)(num / 10))
                             .Select(g => (g.Key, g));

            var instructions = new List<PrintInstruction>();

            instructions.Add(new WriteTextPrintInstruction{Text = Prelude});
            instructions.Add(new NewLinePrintInstruction());
            instructions.Add(new NewLinePrintInstruction());

            bool firstNumber = true;
            foreach (var group in numbers)
            {
                if (ColourMapping.TryGetValue(group.Item1, out ConsoleColor colour))
                {
                    instructions.Add(new SetColourPrintInstruction{Colour = colour});
                }
                else
                {
                    throw new ArgumentOutOfRangeException();
                }

                foreach (var num in group.Item2)
                {
                    instructions.Add(new WriteTextPrintInstruction{Text = firstNumber ? $"{num}" : $", {num}"});
                    firstNumber = false;
                };

                instructions.Add(new SetColourPrintInstruction{Colour = null});
            };

            instructions.Add(new WriteTextPrintInstruction{Text = " !"});
            instructions.Add(new NewLinePrintInstruction());

            return instructions.ToArray();
        }

        public interface PrintInstruction
        {
            void Execute();
        }

        public class SetColourPrintInstruction : PrintInstruction
        {
            public ConsoleColor? Colour;

            public void Execute()
            {
                if (this.Colour is ConsoleColor colour)
                {
                    Console.ForegroundColor = colour;
                }
                else
                {
                    Console.ResetColor();
                }
            }
        }

        public class WriteTextPrintInstruction : PrintInstruction
        {
            public String Text;

            public void Execute()
            {
                Console.Write(this.Text);
            }
        }

        public class NewLinePrintInstruction : PrintInstruction
        {
            public void Execute()
            {
                Console.WriteLine();
            }
        }
    }
}
