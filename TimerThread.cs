using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class TimerThread
    {
        static int SimulationMillisecondsPerRealSecond = int.Parse(ConfigurationManager.AppSettings["SimulationMillisecondsPerRealSecond"]);
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
                // applies current time to simulation, lift logic proceeds upto current time.
                building.incrementSimulation1Second(currentTimeSeconds);
                // progress time 1 sec.
                currentTimeSeconds++;
                // wait speedfactor */ second increment.
                Thread.Sleep(SimulationMillisecondsPerRealSecond);
            }
        }

        public void Start() { thread.Start(); }
        public void Join() { thread.Join(); }
        public void Interrupt() { interrupt = true; }
    }
}
