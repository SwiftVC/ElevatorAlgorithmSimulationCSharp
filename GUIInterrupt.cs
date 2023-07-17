﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Configuration;

namespace ConsoleApp1
{
    internal class GUIInterrupt
    {
        public static void KeyboardInterruptThreadFunct(ref bool interrupt, ref TimerThread timerThread)
        {
            Console.ReadLine(); // Waits for keyboard input
            interrupt = true;
            timerThread.Interrupt();
        }

    }
}
