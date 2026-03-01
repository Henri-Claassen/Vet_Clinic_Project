using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PRG_281_Project
{
    internal class LoadingBar
    {
        #region PercentageLoader
        public static void percentageLoader()
        {
            Console.CursorVisible = false;
            Thread.Sleep(10);
            for (int i = 0; i <= 100; i += 2)
            {
                Thread.Sleep(50);
                Console.SetCursorPosition(59, 12);
                Console.WriteLine(i + "%");
            }
        }
        #endregion

        #region CircleBar
        public static void CircleBar()
        {
            Console.CursorVisible = false;

            for (int i = 8; i <= 16; i++)
            {
                Console.SetCursorPosition(50, i);
                Console.Write("██");
                Thread.Sleep(50);
            }
            for (int i = 68; i >= 52; i--)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.SetCursorPosition(i, 8);
                Console.Write("██");
                Thread.Sleep(50);
            }
            for (int i = 16; i >= 8; i--)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.SetCursorPosition(70, i);
                Console.Write("██");
                Thread.Sleep(50);
            }

            for (int i = 52; i <= 68; i++)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.SetCursorPosition(i, 16);
                Console.Write("██");
                Thread.Sleep(50);
            }
        }
        #endregion
    }//End of class
}//End of namespace
