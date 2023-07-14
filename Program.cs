using ConsoleApp1;
using System.Threading;

bool keyboardInterrupt = false;

Thread keyboardInterruptThread = new Thread(() => ConsoleApp1.GUIInterrupt.keyboardInterruptThreadFunct(ref keyboardInterrupt));
Thread guiThread = new Thread(() => ConsoleApp1.GUIInterrupt.outputVisualsThreadFunct(ref keyboardInterrupt));
Thread timerThread = new Thread(() => ConsoleApp1.TimerThread.TimerThreadFunct(ref keyboardInterrupt));

keyboardInterruptThread.Start();
guiThread.Start();
timerThread.Start();

keyboardInterruptThread.Join();
guiThread.Join();
timerThread.Join();