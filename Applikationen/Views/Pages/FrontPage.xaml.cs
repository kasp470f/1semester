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
using Applikationen.CoronaData;
using Microsoft.WindowsAPICodePack.Dialogs;
using System.Globalization;
using System.Diagnostics;
using Applikationen.DatabaseClasses;

namespace Applikationen.Views.Pages
{
    public partial class FrontPage : Page
    {
        public string FolderPath { get; set; }

        public bool IndicatorStatus = true;
        public FrontPage()
        {
            InitializeComponent();
            Restriction restriction = new Restriction();
            restriction.GetRestriction();
            if (IndicatorStatus == true) Indicator.Style = FindResource("IndicatorGood") as Style;
            else Indicator.Style = FindResource("IndicatorBad") as Style;
        }

        private void UploadButton_Click(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.InitialDirectory = (FolderPath == "") ? "C:\\Users" : FolderPath;
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                FolderPath = dialog.FileName;
                csvPath.Text = FolderPath;
            }

            RegionDataBinding();
            MunicipalityDataBinding();
        }

        private void RegionDataBinding()
        {
            var regionDataCSV = RegionData.ReadCSV(FolderPath + "\\Region_summary.csv");

            var coronaDataUsed = regionDataCSV.Last();

            double positive = coronaDataUsed.Positive;
            positiveBox.Text = string.Format(CultureInfo.CreateSpecificCulture("da-DK"), "{0:n}", positive);


            double tested = coronaDataUsed.Tested;
            testedBox.Text = string.Format(CultureInfo.CreateSpecificCulture("da-DK"), "{0:n}", tested);

            double percentagePositive = coronaDataUsed.PercentageOfData(coronaDataUsed.Positive, coronaDataUsed.Tested);
            percentagePositiveBox.Text = string.Format("{0:n}%", percentagePositive);

            double hospitalized = coronaDataUsed.Hospitalized;
            hospitalizedBox.Text = string.Format("{0}", hospitalized);

            double deaths = coronaDataUsed.Deaths;
            deathsBox.Text = string.Format("{0}", deaths);
        }

        private void MunicipalityDataBinding()
        {
            var MunicipalityDataCSV = municipalityPositive.ReadCSV(FolderPath + "\\Municipality_test_pos.csv");
        }
    }
}
