using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace Applikationen.CoronaData
{
    // Keemon, Natasha, Nichlas
    public class RegionData
    {   
        public string Region { get; set; }
        public double Positive { get; set; }
        public double Tested { get; set; } 
        public double PercentagePositive { get; set; }
        public double Hospitalized { get; set; }
        public double Deaths { get; set; }

        // For processing Region_summary.csv data
        public RegionData(string region, double tested, double positive, double hospitalized, double deaths)
        {
            Region = region;
            Tested = tested;
            Positive = positive;
            Hospitalized = hospitalized;
            Deaths = deaths;
        }

        // For loading Region_summary.csv data
        public static IEnumerable<RegionData> ReadCSV(string fileName)
        {
            List<RegionData> listCSV = new List<RegionData>();
            // We try to open a file and check whether it is a csv
            string lines = File.ReadAllText(fileName);

            // We split the csv data at each break line to make all rows be a single index in a array to split it later into more easier to deal with components
            string[] dataLines = lines.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

            // We split the lines into fields and add them to a list.
            for (int i = 1; i < dataLines.Length-1; i++)
            {
                string[] data = dataLines[i].Split(';');
                for (int j = 0; j < data.Length; j++)
                {
                    data[j] = data[j].Trim();
                    data[j] = data[j].Replace(".", "");
                }
                listCSV.Add(new RegionData(data[0], double.Parse(data[1]), double.Parse(data[2]), double.Parse(data[3]), double.Parse(data[4])));
            }

            // We return CoronaData with the data in the following order: Region, Tested (Total values), Positive, Hospitalized, Deaths
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

        // For getting data for all of Denmark
        public int SumOfData(int[] collectionOfData)
        {
            // Add up the collection data
            int total = collectionOfData.Sum();
            return total;
        }

        // For getting total data in time period
        public int SumOfDataTimespan(int[] collectionOfData, int timespanDays)
        {
            // Turn the array around to get newest data first instead of oldest
            Array.Reverse(collectionOfData);

            // Select only the indexes from the timespan needed
            int[] dataInTimespan = collectionOfData.Take(timespanDays).ToArray();

            // Add up the timespan data
            int total = dataInTimespan.Sum();
            return total;
        }
    }
}
