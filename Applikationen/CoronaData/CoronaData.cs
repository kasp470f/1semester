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
    public class CoronaData
    {   
        public string region { get; set; }
        public double positive { get; set; }
        public double tested { get; set; } 
        public double percentagePositive { get; set; }
        public double hospitalized { get; set; }
        public double icu { get; set; }
        public double deaths { get; set; }

        // For processing Region_summary.csv data
        public CoronaData(string Region, double Tested, double Positive, double Hospitalized, double Deaths)
        {
            region = Region;
            tested = Tested;
            positive = Positive;
            hospitalized = Hospitalized;
            deaths = Deaths;
        }

        // For loading Region_summary.csv data
        public static IEnumerable<CoronaData> ReadCSV(string fileName)
        {
            List<CoronaData> listCSV = new List<CoronaData>();
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
                Debug.WriteLine("New set");
                Debug.WriteLine(data[i]);
                Debug.WriteLine(data[i+1]);
                Debug.WriteLine(data[i+2]);
                Debug.WriteLine(data[i+3]);
                Debug.WriteLine(data[i+4]);

                i = i + 5;
            }
            

            for (int i = 0; i < 35;)
            {
                listCSV.Add(new CoronaData(data[i], double.Parse(data[i+1], System.Globalization.CultureInfo.InvariantCulture), double.Parse(data[i+2], System.Globalization.CultureInfo.InvariantCulture), double.Parse(data[i+3], System.Globalization.CultureInfo.InvariantCulture), double.Parse(data[i+4], System.Globalization.CultureInfo.InvariantCulture)));
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
