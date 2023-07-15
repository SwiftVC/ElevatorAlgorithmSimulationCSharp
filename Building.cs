using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Building
    {
        private int floors;
        private QueuesAtFloors queuesAtFloors;
        private QueuesAtFloors output;
        private Lift lift;
        public Building(int Nfloors) //, floorcallsCSV)
        {
            this.floors = Nfloors;
        }

        public void incrementSimulation1Second(int currentTime)
        {
            // the timer thread cycles 

            // add people to queues at current time, incrementing through everyone, checking == current time

            // apply elevator's logic through processing time

        }

    }
}
