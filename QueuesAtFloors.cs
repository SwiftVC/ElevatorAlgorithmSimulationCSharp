using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class QueuesAtFloors
    {
        private List<List<Person>> queuesAtFloors;

        public QueuesAtFloors(int floors) {
            queuesAtFloors = new List<List<Person>>();
            for (int i = 0; i < floors; i++)
            {
                queuesAtFloors.Add(new List<Person>());
            }
        }

        public void addPersonToFloor(Person person, int floor) {
            int floorIndex = floor - 1;
            queuesAtFloors[floorIndex].Add(person);
        }

        public int PeopleAtFloor(int floor)
        {
            int floorIndex = floor - 1;
            return queuesAtFloors[floorIndex].Count();
        }

        public Person removeFirstPersonIndiscriminately(int floor)
        {
            int floorIndex = floor - 1;
            Person ret = queuesAtFloors[floorIndex][0];
            queuesAtFloors[floorIndex].RemoveAt(0);
            return ret;
        }

        public int floorCount() { return queuesAtFloors.Count(); }

        public List<bool> getExternalButtonPanel(){
            List<bool> ret = new List<bool>();
            foreach (var queue in queuesAtFloors){ if (queue.Count() > 0) { ret.Add(true); } else { ret.Add(false); } }
            return ret;
        }

    }
}
