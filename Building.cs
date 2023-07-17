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
        public Building(int Nfloors, int liftPreferredFloor){
            this.floors = Nfloors;
            this.queuesAtFloors = new QueuesAtFloors(Nfloors);
            this.output = new QueuesAtFloors(floors);
            this.lift = new Lift(Nfloors, liftPreferredFloor, ref this.queuesAtFloors, ref this.output);
        }

        public void SetIDToFinishSim(int ID) { lift.SetIDToFinishSim(ID); }

        public void AddInterruptBoolean(ref bool interrupt) { lift.AddInterruptBoolean(ref interrupt); }

        public void AddInputData(ref List<Person> data){
            dataRef = data;
        }

        void AddPeopleByFloorAndTime(float currentTime) {
            foreach (Person pers in dataRef){
                if(pers.CALL_TIME == currentTime){
                    queuesAtFloors.AddPersonToFloor(pers, pers.FROM_FLOOR);
                }
            }
        }

        public void incrementSimulation1Second(int currentTime){
            bldCurrentTime = currentTime;
            AddPeopleByFloorAndTime(currentTime);
            lift.IncrementSimulation1Second(currentTime); // apply lifts's logic
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

        public void WriteCSV(string filename){
            lift.WriteCSV(filename);
        }
    }
}
