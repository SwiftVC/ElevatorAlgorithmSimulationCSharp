using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class OutputVisuals
    {
        static int TerminalRefreshRateMilliSec = int.Parse(ConfigurationManager.AppSettings["TerminalRefreshRateMilliSec"]);
        public static void OutputVisualsThreadFunct(ref bool interrupt, ref Building bld)
        {
            while (!interrupt)
            {
                Console.Clear();
                Console.WriteLine(bld.GetBuildingRepresentation());
                if (bld.SimulationFinished())
                {
                    Console.WriteLine("Simulation finished at "+ bld.GetFinishTime() + " seconds.");
                    Console.WriteLine("Enter any key to get output.csv");
                }
                
                Thread.Sleep(TerminalRefreshRateMilliSec);
            }
        }
    }
}
