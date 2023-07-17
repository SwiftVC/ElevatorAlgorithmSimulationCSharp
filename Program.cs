using ConsoleApp1;
using System.Threading;
using System.Configuration;
using System.Runtime.CompilerServices;
using System.Formats.Asn1;
using System.Globalization;

/*~~ Building config ~~*/
int totalFloors = 10;
int liftPreferredFloor = 1;
Building bldng = new Building(totalFloors, liftPreferredFloor);

/*~~ Read app.config ~~*/
string csvNameSetting = ConfigurationManager.AppSettings["csvName"];

/*~~ Read Input Data ~~*/
List<Person> data = CSVReader.ReadCSV(csvNameSetting);
bldng.AddInputData(ref data);
int HIGHESTID = data[data.Count() - 1].ID;
bldng.SetIDToFinishSim(HIGHESTID);

/*~~ Start Threads with Interrupt ~~*/
bool simulationInterrupt = false;
bldng.AddInterruptBoolean(ref simulationInterrupt);
Thread guiThread = new Thread(() => ConsoleApp1.GUIInterrupt.OutputVisualsThreadFunct(ref simulationInterrupt, ref bldng));
ConsoleApp1.TimerThread timerThread = new ConsoleApp1.TimerThread(ref bldng);
Thread simulationInterruptThread = new Thread(() => ConsoleApp1.GUIInterrupt.KeyboardInterruptThreadFunct(ref simulationInterrupt, ref timerThread));

simulationInterruptThread.Start();
guiThread.Start();
timerThread.Start();

/*~~ Join Threads ~~*/
simulationInterruptThread.Join();
guiThread.Join();
timerThread.Join();

bldng.WriteCSV("output.csv");
Console.Write("output.csv written.");