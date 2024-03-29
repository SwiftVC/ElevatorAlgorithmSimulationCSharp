﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ConsoleApp1
{
    internal class Lift
    {
        private int endDestination = -1;
        private Elevator elev;
        private LiftEnvironmentData liftEnvData;
        private int totalFloors = 10;
        private int defaultFloor = 1;
        private int moveDownProgress = 0; // %
        private int moveUpProgress = 0; // %
        private bool notBetweenFloors = true;
        private CSVBuilder csvBuilder = new CSVBuilder();
        private int highestIDServed = -1;
        private int HIGHESTID = 50;
        private bool simulationFinished = false;
        private int finishTime = -1;

        public Lift(int floors, int liftPreferredFloor, ref QueuesAtFloors queuesRef, ref QueuesAtFloors outputRef){
            elev = new Elevator(floors);
            totalFloors = floors;
            defaultFloor = liftPreferredFloor;
            liftEnvData = new LiftEnvironmentData(ref queuesRef, ref outputRef);
        }

        public void SetIDToFinishSim(int id) { HIGHESTID = id; }

        public bool SimulationFinished() { return simulationFinished; }
        public int GetFinishTime() { return finishTime; }

        private void SetFinishTime(int currentTime) { if (finishTime == -1) { finishTime = currentTime; } }
        private bool CanElevMoveUp() { return CurrentFloor() < totalFloors; }
        private bool CanElevMoveDown(){ return CurrentFloor() > 1; }
        private void MoveUp()
        {
            moveUpProgress += 10;
            moveDownProgress = 0;

            if (moveUpProgress == 100)
            {
                if (CanElevMoveUp()) { elev.Moveup(); moveUpProgress = 0; notBetweenFloors = true;  }
            }
            else { notBetweenFloors = false; }
        }
        private void MoveDown()
        {
            moveDownProgress += 10;
            moveUpProgress = 0;

            if (moveDownProgress == 100)
            {
                if (CanElevMoveDown()) { elev.Movedown(); moveDownProgress = 0; notBetweenFloors = true; }
            }
            else { notBetweenFloors = false; }
        }
        public int CurrentFloor(){return elev.CurrentFloor();}
        private bool AtCapacity() { return Occupants() == elev.GetCapacity(); }

        private void ActivateFloorButton(int floor){ elev.ActivateFloorButton(floor); }
        private void DeactivateFloorButton(int floor) { elev.DeactivateFloorButton(floor); }

        private int GetCallsAbove(List<bool> panelState)
        {
            int currFloor = CurrentFloor();
            int callsAbove = 0;
            for(int floor = currFloor + 1; floor != panelState.Count() + 1; floor++){
                int floorIndex = floor - 1;
                if (panelState[floorIndex] == true) { ++callsAbove; }
            }
            return callsAbove;
        }

        private int GetCallsBelow(List<bool> panelState)
        {
            int currFloor = CurrentFloor();
            int callsBelow = 0;
            for (int floor = currFloor - 1; floor != 0; floor--){
                int floorIndex = floor - 1;
                if (panelState[floorIndex] == true) { ++callsBelow; }
            }
            return callsBelow;
        }

        private int GetFurthestCallAbove(List<bool> panelState)
        {
            for(int floorIndex = panelState.Count() - 1; floorIndex != CurrentFloor() - 1; floorIndex--)
            {
                if (panelState[floorIndex]) { return floorIndex + 1; }
            }
            throw new Exception("GetFurthestCallAbove called with no calls above");
        }

        private int GetFurthestCallBelow(List<bool> panelState)
        {
            for(int floorIndex = 0; floorIndex != CurrentFloor() - 1; floorIndex++)
            {
                if (panelState[floorIndex]) { return floorIndex + 1; }
            }
            throw new Exception("GetFurthestCallBelow called with no calls below");
        }

        int PollFloorCalls(List<bool> panelState)
        {
            int internalCallsAbove = GetCallsAbove(panelState);
            int internalCallsBelow = GetCallsBelow(panelState);

            if(internalCallsAbove == internalCallsBelow && internalCallsAbove > 0) { return GetFurthestCallAbove(panelState); }
            if(internalCallsAbove > internalCallsBelow) { return GetFurthestCallAbove(panelState); }

            if(internalCallsAbove < internalCallsBelow) { return GetFurthestCallBelow(panelState); }

            return -1;
        }

        int PollExternalFloorCalls()
        {
            return PollFloorCalls(liftEnvData.GetExternalButtonPanel());
        }

        int PollInternalFloorCalls()
        {
            return PollFloorCalls(elev.GetFloorButtonPanel());
        }

        int PollForFloorCalls()
        {
            /*
            return -1 if no calls
            prioritise internal over external
            */
            int internalPoll = PollInternalFloorCalls();
            if (internalPoll != -1) { return internalPoll; }

            int externalPoll = PollExternalFloorCalls();
            if (externalPoll != -1) { return externalPoll; }

            return -1;
        }

        bool ExternalRequestAtFloor(int floor){return liftEnvData.ExternalRequestAtFloor(floor);}
        bool InternalRequestAtFloor(int floor){return elev.InternalRequestAtFloor(floor);}
        public Elevator.ElevatorState GetState() { return elev.GetState(); }
        public int Occupants() { return elev.Occupants(); }
        int GetEndDestination() { return endDestination; }

        void UpdateEndDestination(int newDestination) { endDestination = newDestination; }
        void EndDestinationServiced() { UpdateEndDestination(-1); }

        int GetDefaultFloor() { return defaultFloor; }

        void UpdateHighestIDServed(Person pers) { if (highestIDServed < pers.ID) { highestIDServed = pers.ID; } }
        public bool FinishedServingPeople() { return highestIDServed == HIGHESTID; }

        void ServeCurrentFloor()
        {
            int currentFloor = CurrentFloor();
            if (currentFloor == GetEndDestination()) { EndDestinationServiced(); }

            List<Person> peopleGettingOff = elev.DeloadPeople();
            foreach (Person pers in peopleGettingOff)
            {
                liftEnvData.DropoffPers(currentFloor, pers);
                UpdateHighestIDServed(pers);
            }

            while (liftEnvData.PeopleAtFloor(currentFloor) > 0 && !elev.Full())
            {
                Person p = liftEnvData.PickupPersIndiscriminately(currentFloor);
                ActivateFloorButton(p.TO_FLOOR);
                elev.AddPerson(p);
            }
            DeactivateFloorButton(currentFloor);
        }
        void TowardDestination()
        {
            int currFloor = CurrentFloor();
            if (currFloor < GetEndDestination())
            {
                MoveUp();
            }
            else
            {
                MoveDown();
            }
        }

        void TowardDefaultfloor()
        {
            int currFloor = CurrentFloor();
            if (currFloor < GetDefaultFloor())
            {
                MoveUp();
            }
            else
            {
                MoveDown();
            }
        }

        public void IncrementSimulation1Second(int currentTime){
            LiftLogicLoop(currentTime);
            return;
        }

        void LiftLogicLoop(int currentTime){
            int currFloor = CurrentFloor();
            if (notBetweenFloors) // Prevents serving current floor when in transit.
            { 
                
                if (ExternalRequestAtFloor(currFloor) &&
                    !AtCapacity() ||
                    InternalRequestAtFloor(currFloor) ||
                    currFloor == GetEndDestination())
                {
                    SaveOutputData(currentTime);
                    ServeCurrentFloor();

                    if (FinishedServingPeople()) {
                        simulationFinished = true;
                        SetFinishTime(currentTime);
                    }
                }

                if (GetEndDestination() == -1)
                {
                    UpdateEndDestination(PollForFloorCalls());
                }
            }
            if (GetEndDestination() != -1)
            {
                TowardDestination();
            }
            else if (currFloor != GetDefaultFloor())
            {
                TowardDefaultfloor();
            }
        }
        public void SaveOutputData(int currentTime)
        {
            int currFloor = CurrentFloor();
            List<int> occupants = elev.GetIDsOfOccupants();
            int endDestination = GetEndDestination();
            csvBuilder.AddEntry(currentTime, currFloor, occupants, endDestination);
        }

        public void WriteCSV(string filename){
            csvBuilder.WriteCSV(filename);
        }
    }
    
}