using System;
using System.Collections.Generic;

namespace LocationExtreme
{
    class Program
    {
        static void Main(string[] args)
        {
            LotteryNumberSet set;

            PrintHeader(); // Some TUI spam

            for (;;) switch (Console.ReadKey().KeyChar) // Wait for command
            {
                case 'g': // Something along the lines of "(g) generate"
                    set = LotteryNumberSet.Create();
                    IEnumerable<LotteryNumberSetPrinter.PrintInstruction> sheet;
                    sheet = new LotteryNumberSetPrinter().Format(set);

                    foreach (var instruction in sheet)
                    {
                        instruction.Execute();
                    }
                    break;
                case 'q': // Something along the lines of "(q) quit"
                    Environment.Exit(0);
                    break;
                default:
                    break;
            };
        }

        public static void PrintHeader()
        {
            // Print the intro and keyboard input request
        }
    }
}
