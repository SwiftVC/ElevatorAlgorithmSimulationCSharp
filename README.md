# ElevatorAlgorithmSimulationCSharp

Directions:
1. Compile with VisualStudio
2. Wait for "Simulation finished at XXX seconds"
3. type any-key & [ENTER], to print output.csv.
4. Look in .\bin\Debug\net6.0\output.csv

Opposed to timing the simulation with the systemclock as in https://github.com/SwiftVC/ElevatorAlgorithmSimulation,
TimerThread is used to iterate through the elevator actions, returning a consistent result at high simulation speeds.

Edit app.config/SimulationMillisecondsPerRealSecond to change simulation speed.

Caution: "output.csv" is reset each simulation.
