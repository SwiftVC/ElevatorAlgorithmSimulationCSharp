using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ConsoleApp1
{
    internal class GUIInterrupt
    {
        public static void keyboardInterruptThreadFunct(ref bool interrupt)
        {
            Console.ReadLine(); // Waits for keyboard input
            interrupt = true;
        }

        public static void outputVisualsThreadFunct(ref bool interrupt)
        {
            while (!interrupt)
            {
                Console.WriteLine("test");
                Thread.Sleep(1000);
            }
        }

    }
}
