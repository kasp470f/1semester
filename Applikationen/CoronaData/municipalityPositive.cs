using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Applikationen.CoronaData
{
    public class municipalityPositive
    {
        public string Municipality { get; set; }

        public double Positive { get; set; }

        public double Tested { get; set; }

        // For the Municipality_test_pos.csv data constructor
        public municipalityPositive(string municipality, double tested, double positive)
        {
            Municipality = municipality;
            Positive = positive;
            Tested = tested;
        }

        // For loading Municipality_test_pos.csv data
        public static IEnumerable<municipalityPositive> ReadCSV(string fileName)
        {
            List<municipalityPositive> listCSV = new List<municipalityPositive>();
            // We try to open a file and check whether it is a csv
            string lines = File.ReadAllText(fileName);

            // We split the csv data at each break line to make all rows be a single index in a array to split it later into more easier to deal with components
            string[] dataLines = lines.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

            // We split the lines into fields and add them to a list.
            for (int i = 1; i < dataLines.Length - 1; i++)
            {
                string[] data = dataLines[i].Split(';');
                listCSV.Add(new municipalityPositive(data[1], double.Parse(data[2]), double.Parse(data[3])));
            }

            return listCSV;
        }
    }
}
