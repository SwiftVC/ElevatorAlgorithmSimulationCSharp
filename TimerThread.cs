using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class TimerThread
    {
        private int currentTimeSeconds = 0;
        private Thread thread;
        private bool interrupt = false;
        private Building building;

        public TimerThread(ref Building bldng) {
            thread = new Thread(() => TimerThreadFunct());
            building = bldng;
        }
        public void TimerThreadFunct() {

            while (!interrupt)
            {
                // incrementSimulation1Second();
                building.incrementSimulation1Second(currentTimeSeconds);
                // progress time 1 sec.
                currentTimeSeconds++;
                // wait speedfactor */ second increment.

            }
        }

        public void Start() { thread.Start(); }
        public void Join() { thread.Join(); }
        public void Interrupt() { interrupt = true; }
    }
}
