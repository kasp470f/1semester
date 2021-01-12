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
            

            var coronaData = CoronaData.ReadCSV("C:\\Users\\Keemon\\Desktop\\coronadata\\Region_summary.csv");

            var coronaDataUsed = coronaData.First();

            double positive = coronaDataUsed.positive;
            positiveBox.Text = positive.ToString();

            double tested = coronaDataUsed.tested;
            testedBox.Text = tested.ToString();

            double percentagePositive = coronaDataUsed.PercentageOfData(coronaDataUsed.positive, coronaDataUsed.tested);
            percentagePositiveBox.Text = percentagePositive.ToString();

            double hospitalized = coronaDataUsed.hospitalized;
            hospitalizedBox.Text = hospitalized.ToString();

            double icu = 0;
            icuBox.Text = icu.ToString();

            double deaths = coronaDataUsed.deaths;
            deathsBox.Text = deaths.ToString();
        }
    }
}
