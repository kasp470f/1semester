using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Applikationen.CoronaData
{
    public class MunicipalityPositive
    {
        public string Municipality { get; set; }

        public long Positive { get; set; }

        public long Tested { get; set; }

        // For the Municipality_test_pos.csv data constructor
        public MunicipalityPositive(string municipality, long tested, long positive)
        {
            Municipality = municipality;
            Positive = positive;
            Tested = tested;
        }

        // For loading Municipality_test_pos.csv data
        public static IEnumerable<MunicipalityPositive> ReadCSV(string fileName)
        {
            List<MunicipalityPositive> listCSV = new List<MunicipalityPositive>();
            // We try to open a file and check whether it is a csv
            string lines = File.ReadAllText(fileName);

            // We split the csv data at each break line to make all rows be a single index in a array to split it later into more easier to deal with components
            string[] dataLines = lines.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

            // We split the lines into fields and add them to a list.
            for (int i = 1; i < dataLines.Length - 1; i++)
            {
                string[] data = dataLines[i].Split(';');
                for (int j = 0; j < data.Length; j++)
                {
                    data[j] = data[j].Replace(".", string.Empty);
                }
                Debug.WriteLine(data[1] + data[2] + data[3]);
                listCSV.Add(new MunicipalityPositive(data[1], long.Parse(data[2]), long.Parse(data[3])));
            }

            return listCSV;
        }


        // ----------------
        // Helper functions
        // ----------------

        // Calculate percentages, meant for use with percentagePositive
        public double PercentageOfData(double newData, double totalData)
        {
            // We take the newData and find which percentage of the totalData it is
            double percentComplete = (double)Math.Round((double)(100 * newData) / totalData);
            return percentComplete;
        }
    }
}
