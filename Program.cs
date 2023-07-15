using ConsoleApp1;
using System.Threading;
using System.Configuration;
using System.Runtime.CompilerServices;
using System.Formats.Asn1;
using System.Globalization;

/*~~ Building config ~~*/
Building bldng = new Building(10);

/*~~ Read app.config ~~*/
string csvNameSetting = ConfigurationManager.AppSettings["csvName"];

string simSpeedFactorSetting = ConfigurationManager.AppSettings["simSpeedFactor"];
float simSpeedFactor = float.Parse(simSpeedFactorSetting);


/*~~ Read Input Data ~~*/
List<Person> data = CSVReader.ReadCSV(csvNameSetting);

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