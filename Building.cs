using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
        private float bldCurrentTime;
        private List<Person> dataRef;
        public Building(int Nfloors){
            this.floors = Nfloors;
            this.lift = new Lift(Nfloors);
            this.queuesAtFloors = new QueuesAtFloors(Nfloors);
            this.output = new QueuesAtFloors(floors);
        }

        public void AddInputData(ref List<Person> data){
            dataRef = data;
        }

        void AddPeopleByFloorAndTime(float currentTime) {
            foreach (Person pers in dataRef){
                if(pers.CALL_TIME == currentTime){
                    queuesAtFloors.addPersonToFloor(pers, pers.FROM_FLOOR);
                }
            }
        }

        public void incrementSimulation1Second(float currentTime)
        {
            bldCurrentTime = currentTime;
            // the timer thread cycles 

            // add people to queues at current time, incrementing through everyone, checking == current time
            AddPeopleByFloorAndTime(currentTime);

            // apply elevator's logic through processing time
            lift.IncrementSimulation1Second(currentTime);
        }

        public string GetBuildingRepresentation(){
            string ret = new string("");
            for (int floorIndex = floors - 1; floorIndex > -1; floorIndex--){
                int floor = floorIndex + 1;
                ret += "Floor: " + floor.ToString().PadLeft(4) + "|";
                ret += "Queue: " + queuesAtFloors.PeopleAtFloor(floor).ToString().PadLeft(3) + "| ";
                if (lift.CurrentFloor() == floor) { ret += Elevator.ElevatorStateToString(lift.GetState()).PadLeft(Elevator.ELEVATORSTATEMAXFIELDWIDTH) + ":" + lift.Occupants().ToString().PadLeft(3) + "| "; }
                else { ret += " ".PadLeft(Elevator.ELEVATORSTATEMAXFIELDWIDTH) + " " + "".PadLeft(3) + "| "; };

                ret += "Output: ";
                ret += output.PeopleAtFloor(floor).ToString().PadLeft(3) + "| ";
                ret += '\n';
            }
            ret += "Current Time: " + bldCurrentTime.ToString().PadLeft(5) + " seconds.";
            
            return ret;
        }

    }
}
