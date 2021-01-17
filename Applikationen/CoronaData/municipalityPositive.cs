using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace Applikationen.CoronaData
{
    public class MunicipalityPositive
    {
        public string Municipality { get; set; }

        public long Positive { get; set; }

        public long Tested { get; set; }

        /// <summary>
        /// For the Municipality_test_pos.csv data constructor.
        /// <para>Made by Kasper, Keemon and Natasha</para>
        /// </summary>
        /// <param name="municipality">A string for naming a municipality object</param>
        /// <param name="tested">A long for assigning amount tested people object</param>
        /// <param name="positive">A long for assigning amount infected people object</param>
        public MunicipalityPositive(string municipality, long tested, long positive)
        {
            Municipality = municipality;
            Positive = positive;
            Tested = tested;
        }


        /// <summary>
        /// For loading Municipality_test_pos.csv data
        /// <para>Made by Kasper</para>
        /// </summary>
        /// <param name="fileName">The name of the file and path to that file, that needs to be read.</param>
        /// <returns>A list that contains that information from the Municipality_test_pos.csv.</returns>
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
