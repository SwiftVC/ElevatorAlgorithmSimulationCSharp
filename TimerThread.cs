using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class TimerThread
    {
        public static void TimerThreadFunct(ref bool interrupt) {
            int currentTimeSeconds = 0;
            while (!interrupt)
            {
                // incrementSimulation1Second();
                // progress time 1 sec.
                currentTimeSeconds++;
                // wait speedfactor */ second increment.
            }
        }
    }
}
