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
        public static List<Person> ReadCSV(string csvName)
        {
            var reader = new StreamReader(csvName);
            var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            csv.Context.RegisterClassMap<CSVHeadingsToPerson>();
            return csv.GetRecords<Person>().ToList();
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