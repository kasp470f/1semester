using System;
using System.Collections.Generic;
using System.IO;

namespace Applikationen.CoronaData
{

    public class RegionData
    {   
        public string Region { get; set; }
        public double Positive { get; set; }
        public double Tested { get; set; } 
        public double PercentagePositive { get; set; }
        public double Hospitalized { get; set; }
        public double Deaths { get; set; }

        /// <summary>
        /// For processing Region_summary.csv data.
        /// <para>Made by Keemon, Natasha, and Nichlas</para>
        /// </summary>
        /// <param name="region">What part of the country is the data from.</param>
        /// <param name="tested">The amount of people that was tested for COVID19.</param>
        /// <param name="positive">The amount of people that has been tested positive for COVID19.</param>
        /// <param name="hospitalized">The amount of people that has been hospitalized with/for COVID19.</param>
        /// <param name="deaths">The amount of people who have succumbed with COVID19.</param>
        public RegionData(string region, double tested, double positive, double hospitalized, double deaths)
        {
            Region = region;
            Tested = tested;
            Positive = positive;
            Hospitalized = hospitalized;
            Deaths = deaths;
        }

        /// <summary>
        /// For loading Region_summary.csv data
        /// <para>Made by Kasper</para>
        /// </summary>
        /// <param name="fileName">The file/path to the file that is needed to read Region_summary.csv.</param>
        /// <returns>A list of RegionData.</returns>
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

        #region Helper Functions
        /// <summary>
        /// Calculate percentages, meant for use with percentagePositive.
        /// <para>Made by Kasper, Natasha and Keemon</para>
        /// </summary>
        /// <param name="positive">The amount of people tested postive for COVID19.</param>
        /// <param name="totalTested">The total amount of people tested for COVID19, regardless of them being positive.</param>
        /// <returns>The percentage of people tested positive for COVID19.</returns>
        public double PercentageOfData(double positive, double totalTested)
        {
            // We take the newData and find which percentage of the totalData it is
            double percentComplete = (double)Math.Round((double)(100 * positive) / totalTested);
            return percentComplete;
        }
        #endregion
    }
}
