using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class LiftEnvironmentData
    {
        private QueuesAtFloors queuesRef;
        private QueuesAtFloors outputsRef;

        public LiftEnvironmentData(ref QueuesAtFloors queues, ref QueuesAtFloors outputs)
        {
            queuesRef = queues;
            outputsRef = outputs;
        }

        public bool ExternalRequestAtFloor(int currFloor){
            return queuesRef.PeopleAtFloor(currFloor) != 0;
        }

        public void DropoffPers(int floor, Person pers)
        {
            if (pers.TO_FLOOR != floor)
            {
                throw new Exception("Person served to wrong floor");
            }
            //pers.reachedDestination();
            outputsRef.AddPersonToFloor(pers, floor);
        }
        public int PeopleAtFloor(int floor)
        {
            return queuesRef.PeopleAtFloor(floor);
        }

        public Person PickupPersIndiscriminately(int floor)
        {
            return queuesRef.RemoveFirstPersonIndiscriminately(floor);
        }
        public List<bool> GetExternalButtonPanel() { return queuesRef.GetExternalButtonPanel();
    }
}
}
