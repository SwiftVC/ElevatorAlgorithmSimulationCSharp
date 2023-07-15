using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Lift
    {
        private Elevator elev;

        public Lift(int floors){
            elev = new Elevator(floors);
        }

        public int CurrentFloor(){
            return elev.CurrentFloor();
        }

        public void IncrementSimulation1Second(float currentTime)
        {
            elev.SetCurrentTime(currentTime);
        }
        public Elevator.ElevatorState GetState() { return elev.GetState(); }
        public int Occupants() { return elev.Occupants(); }


    }
}