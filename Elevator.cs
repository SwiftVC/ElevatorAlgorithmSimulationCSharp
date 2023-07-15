using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Elevator
    {
        float currentTime = 0;

        public static readonly int ELEVATORSTATEMAXFIELDWIDTH = 7;

        float LIFTTIMEOPENING = float.Parse(ConfigurationManager.AppSettings["LIFTTIMEOPENING"]);
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
        private List<Person> capacity;
        private int currentFloorVar = 1;
        private const int CAPACITY = 8;

        public Elevator(int floors) {
            floorButtonPanel = Enumerable.Repeat(false, floors).ToList();
            capacity = new List<Person>();
        }

        public void SetState(ElevatorState newState) { state = newState; }
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
        void Open() { SetState(ElevatorState.OPENING); Wait(LIFTTIMEOPENING); SetState(ElevatorState.OPEN); }
        void Wait(float TIME) {
            float startTime = currentTime;
            while (!(currentTime - startTime >= TIME)){
                ;
            }
            return;
        }

        public void SetCurrentTime(float time) { currentTime = time; }
        // How does timerThread speak to the elevator?
        public int CurrentFloor() { return currentFloorVar; }
        public int Occupants() { return capacity.Count(); }

    }
}
