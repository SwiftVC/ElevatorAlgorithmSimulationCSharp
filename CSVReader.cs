using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Drawing;
using System.Globalization;
using System.IO;

namespace ConsoleApp1
{
    internal class CSVReader
    {
        public CSVReader(string csvName)
        {
            using (var reader = new StreamReader(csvName))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Context.RegisterClassMap<CSVHeadingsToPerson>();
                var records = csv.GetRecords<Person>().ToList();
                foreach (var record in records)
                {
                    Console.WriteLine(record.ID+","+ record.FROM_FLOOR + ","+ record.TO_FLOOR + "," + record.CALL_TIME);
                }
            }
        }
    }
    internal class CSVHeadingsToPerson : ClassMap<Person>
    {
        public CSVHeadingsToPerson()
        {
            Map(pers => pers.ID).Name("Person ID");
            Map(pers => pers.FROM_FLOOR).Name("At Floor");
            Map(pers => pers.TO_FLOOR).Name("Going to Floor");
            Map(pers => pers.CALL_TIME).Name("Time");
        }
    }
}
