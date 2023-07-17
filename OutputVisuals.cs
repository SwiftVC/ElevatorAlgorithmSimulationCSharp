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
                Console.Write(bld.GetBuildingRepresentation());
                Thread.Sleep(TerminalRefreshRateMilliSec);
            }
        }
    }
}
