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

/*~~ Start Threads with Interrupt ~~*/
bool keyboardInterrupt = false;
Thread guiThread = new Thread(() => ConsoleApp1.GUIInterrupt.OutputVisualsThreadFunct(ref keyboardInterrupt, ref bldng));
ConsoleApp1.TimerThread timerThread = new ConsoleApp1.TimerThread(ref bldng);
Thread keyboardInterruptThread = new Thread(() => ConsoleApp1.GUIInterrupt.KeyboardInterruptThreadFunct(ref keyboardInterrupt, ref timerThread));
keyboardInterruptThread.Start();
guiThread.Start();
timerThread.Start();

/*~~ Join Threads ~~*/
keyboardInterruptThread.Join();
guiThread.Join();
timerThread.Join();