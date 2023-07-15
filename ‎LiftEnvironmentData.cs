using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class LiftEnvironmentData
    {
        private QueuesAtFloors queuesRef;
        private QueuesAtFloors outputsRef;

        public LiftEnvironmentData(ref QueuesAtFloors queues, ref QueuesAtFloors outputs)
        {
            queuesRef = queues;
            outputsRef = outputs;
        }
    }
}
