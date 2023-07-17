using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class CSVBuilder
    {
        private class CSVRow
        {
            public int Field1 { get; set; }
            public int Field2 { get; set; }
            public List<int> Field3 { get; set; }
            public int Field4 { get; set; }

            public CSVRow(int field1, int field2, List<int> field3, int field4)
            {
                Field1 = field1;
                Field2 = field2;
                Field3 = field3;
                Field4 = field4;
            }
        }

        List<CSVRow> CSVRows = new List<CSVRow>();
        public void AddEntry(int Field1, int Field2, List<int> Field3, int Field4)
        {
            CSVRows.Add(new CSVRow(Field1, Field2, Field3, Field4));
        }

        public void WriteCSV(string filename)
        {
            using (StreamWriter writer = new StreamWriter(filename))
            {
                writer.WriteLine("CurrentTime,CurrentFloor,OccupantIDs,EndDestinationFloor");

                foreach (CSVRow obj in CSVRows){
                    string field3Values = string.Join(";", obj.Field3);
                    writer.WriteLine($"{obj.Field1},{obj.Field2},{field3Values},{obj.Field4}");
                }
            }
        }
    }
}
