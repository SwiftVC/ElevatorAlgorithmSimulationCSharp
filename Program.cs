using ConsoleApp1;
using System.Threading;
using System.Configuration;
using System.Runtime.CompilerServices;
using System.Formats.Asn1;
using System.Globalization;

/*~~ Read app.config ~~*/
string csvNameSetting = ConfigurationManager.AppSettings["csvName"];

string simSpeedFactorSetting = ConfigurationManager.AppSettings["simSpeedFactor"];
float simSpeedFactor = float.Parse(simSpeedFactorSetting);

/*~~ Read Input Data ~~*/
List<Person> data = CSVReader.ReadCSV(csvNameSetting);

/*~~ Start Threads with Interrupt ~~*/
bool keyboardInterrupt = false;
Thread keyboardInterruptThread = new Thread(() => ConsoleApp1.GUIInterrupt.keyboardInterruptThreadFunct(ref keyboardInterrupt));
Thread guiThread = new Thread(() => ConsoleApp1.GUIInterrupt.outputVisualsThreadFunct(ref keyboardInterrupt));
Thread timerThread = new Thread(() => ConsoleApp1.TimerThread.TimerThreadFunct(ref keyboardInterrupt));
keyboardInterruptThread.Start();
guiThread.Start();
timerThread.Start();





void incrementSimulation1Second()
{
    // add people to queues at current time

    // apply elevator's logic through processing time

}

Building bldng = new Building(5);

/*~~ Join Threads ~~*/
keyboardInterruptThread.Join();
guiThread.Join();
timerThread.Join();