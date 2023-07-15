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
            this.lift = new Lift(Nfloors);
            this.queuesAtFloors = new QueuesAtFloors(Nfloors);
            this.output = new QueuesAtFloors(floors);
        }

        public void incrementSimulation1Second(float currentTime)
        {
            // the timer thread cycles 

            // add people to queues at current time, incrementing through everyone, checking == current time

            // apply elevator's logic through processing time
            lift.IncrementSimulation1Second(currentTime);
        }

        public string GetBuildingRepresentation(){
            string ret = new string("");
            for (int floorIndex = floors - 1; floorIndex > -1; floorIndex--){
                int floor = floorIndex + 1;
                ret += "Floor: " + floor.ToString().PadLeft(4) + "|";
                ret += "Queue: " + queuesAtFloors.PeopleAtFloor(floor).ToString().PadLeft(3) + " | ";
                if (lift.CurrentFloor() == floor) { ret += Elevator.ElevatorStateToString(lift.GetState()) + lift.Occupants().ToString().PadLeft(3); }
                else { };
                ret += '\n';
            }
            return ret;
        }

    }
}
