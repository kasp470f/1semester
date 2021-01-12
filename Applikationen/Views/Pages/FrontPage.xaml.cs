using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using Applikationen.CoronaDataFunctions;
using Applikationen.CoronaData;
using System.Globalization;

namespace Applikationen.Views.Pages
{
    /// <summary>
    /// Interaction logic for FrontPage.xaml
    /// </summary>
    public partial class FrontPage : Page
    {
        public FrontPage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            
            var regionDataCSV = RegionData.ReadCSV("C:\\Users\\Keemon\\Desktop\\coronadata\\Region_summary.csv");

            //var municipalityTestPosDataCSV = municipalityTestedTS.ReadCSV("C:\\Users\\Keemon\\Desktop\\coronadata\\Municipality_test_pos.csv");

            //var admittedNewDataCSV = RegionData.ReadCSV("C:\\Users\\Keemon\\Desktop\\coronadata\\Newly_admitted_over_time.csv");

            //var deathsTimeDataCSV = RegionData.ReadCSV("C:\\Users\\Keemon\\Desktop\\coronadata\\Deaths_over_time.csv");

            //var municipalityCTSregionDataCSV = RegionData.ReadCSV("C:\\Users\\Keemon\\Desktop\\coronadata\\Municipality_cases_time_series.csv");
            

            var coronaDataUsed = regionDataCSV.First();

            double positive = coronaDataUsed.Positive;
            positiveBox.Text = string.Format(CultureInfo.CreateSpecificCulture("da-DK"), "{0:N}", positive);

            double tested = coronaDataUsed.Tested;
            testedBox.Text = string.Format(CultureInfo.CreateSpecificCulture("da-DK"), "{0:N}", tested);

            double percentagePositive = coronaDataUsed.PercentageOfData(coronaDataUsed.Positive, coronaDataUsed.Tested);
            percentagePositiveBox.Text = string.Format(CultureInfo.CreateSpecificCulture("da-DK"), "{0:N}", percentagePositive);

            double hospitalized = coronaDataUsed.Hospitalized;
            hospitalizedBox.Text = string.Format(CultureInfo.CreateSpecificCulture("da-DK"), "{0:N}", hospitalized);

            double deaths = coronaDataUsed.Deaths;
            deathsBox.Text = string.Format(CultureInfo.CreateSpecificCulture("da-DK"), "{0:N}", deaths);
        }
    }
}
