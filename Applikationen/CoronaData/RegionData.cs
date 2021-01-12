using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace Applikationen.CoronaDataFunctions
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

            // We split the csv data at each semicolon to have separate fields/rows
            string[] data = lines.Split(new[] { ";", "\r\n", "\r", "\n" }, StringSplitOptions.None);

            var datalist = data.ToList();
            datalist.RemoveRange(0, 5);
            data = datalist.ToArray();
            
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = data[i].Trim();
                data[i] = data[i].Replace(".", "");
            }
            

            for (int i = 0; i < 35;)
            {
                listCSV.Add(new RegionData(data[i], double.Parse(data[i+1], System.Globalization.CultureInfo.InvariantCulture), double.Parse(data[i+2], System.Globalization.CultureInfo.InvariantCulture), double.Parse(data[i+3], System.Globalization.CultureInfo.InvariantCulture), double.Parse(data[i+4], System.Globalization.CultureInfo.InvariantCulture)));
                i = i + 5;
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
