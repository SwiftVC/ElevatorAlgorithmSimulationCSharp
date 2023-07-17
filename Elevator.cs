using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Elevator
    {
        public static readonly int ELEVATORSTATEMAXFIELDWIDTH = 7;
        public enum ElevatorState
        {
            OPEN,
            CLOSED,
            OPENING,
            CLOSING,
            BROKEN
        };

        private List<bool> floorButtonPanel;
        private ElevatorState state = ElevatorState.OPEN;
        private ElevatorState stateWhenFinishedWaiting = ElevatorState.OPEN;
        private List<Person> capacity;
        private int currentFloorVar = 1;
        private const int CAPACITY = 8;
        private float TimeRemaining = 0; // remaining Time to finish: OPENING/CLOSING/MOVE UP/MOVE DOWN/RECIEVE PERSON/DELOAD PERSON

        public Elevator(int floors) {
            floorButtonPanel = Enumerable.Repeat(false, floors).ToList();
            capacity = new List<Person>();
        }

        private void SetState(ElevatorState newState) { state = newState; }
        private void SetStateWhenFinishedWaiting(ElevatorState newState) { stateWhenFinishedWaiting = newState; }
        public ElevatorState GetState() { return state; }

        public static string ElevatorStateToString(ElevatorState state)
        {
            switch (state)
            {
                case (ElevatorState.OPEN): { return "OPEN"; }
                case (ElevatorState.CLOSED): { return "CLOSED"; }
                case (ElevatorState.OPENING): { return "OPENING"; }
                case (ElevatorState.CLOSING): { return "CLOSING"; }
                case (ElevatorState.BROKEN): { return "BROKEN"; }
                default:
                    throw new Exception("Elevator instance in an invalid state");
            }
        }
        bool IsOpen() { return state == ElevatorState.OPEN; }
        void Open() { SetState(ElevatorState.OPENING); SetStateWhenFinishedWaiting(ElevatorState.OPEN); }
        void Close() { SetState(ElevatorState.CLOSING); SetStateWhenFinishedWaiting(ElevatorState.CLOSED); }

        public int CurrentFloor() { return currentFloorVar; }
        public int Occupants() { return capacity.Count(); }
        public int GetCapacity() { return CAPACITY; }
        public bool InternalRequestAtFloor(int floor) { return floorButtonPanel[floor - 1]; }
        public bool Full() { return Occupants() == CAPACITY; }
        public void AddPerson(Person p)
        {
            if (Full()) { throw new Exception("addPerson called on lift at capacity"); }
            capacity.Add(p);
        }

        public void Movedown() { if (IsOpen()) { Close(); } --currentFloorVar; }
        public void Moveup() { if (IsOpen()) { Close(); } ++currentFloorVar; }

        public List<Person> DeloadPeople()
        {
            if (!IsOpen()) { Open(); }
            List<Person> ret = new List<Person>();
            capacity.RemoveAll(pers =>
            {
                if (pers.TO_FLOOR == currentFloorVar)
                {
                    ret.Add(pers);
                    return true;
                }
                return false;
            });
            return ret;
        }

        public void DeactivateFloorButton(int floor){floorButtonPanel[floor - 1] = false;}
        public void ActivateFloorButton(int floor){floorButtonPanel[floor - 1] = true;}
        public List<bool> GetFloorButtonPanel() { return floorButtonPanel; }

        public List<int> GetIDsOfOccupants() { return capacity.Select(p => p.ID).ToList();}

    }
}
