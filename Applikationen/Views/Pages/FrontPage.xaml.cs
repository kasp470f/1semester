using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Applikationen.CoronaData;
using Microsoft.WindowsAPICodePack.Dialogs;
using System.Globalization;
using Applikationen.DatabaseClasses;
using System.Diagnostics;

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
            DKpositiveBox.Text = string.Format(CultureInfo.CreateSpecificCulture("da-DK"), "{0:n}", positive);


            double tested = coronaDataUsed.Tested;
            DKtestedBox.Text = string.Format(CultureInfo.CreateSpecificCulture("da-DK"), "{0:n}", tested);

            double percentagePositive = coronaDataUsed.PercentageOfData(coronaDataUsed.Positive, coronaDataUsed.Tested);
            DKpercentagePositiveBox.Text = string.Format("{0:n}%", percentagePositive);

            double hospitalized = coronaDataUsed.Hospitalized;
            DKhospitalizedBox.Text = string.Format("{0}", hospitalized);

            double deaths = coronaDataUsed.Deaths;
            DKdeathsBox.Text = string.Format("{0}", deaths);
        }

        private void MunicipalityDataBinding()
        {
            var MunicipalityDataCSV = MunicipalityPositive.ReadCSV(FolderPath + "\\Municipality_test_pos.csv");

            var coronaDataUsed = MunicipalityDataCSV.Single(Municipality => Municipality.Municipality == "Herlev");

            double positive = coronaDataUsed.Positive;
            MCpositiveBox.Text = string.Format(CultureInfo.CreateSpecificCulture("da-DK"), "{0:n}", positive);

            double tested = coronaDataUsed.Tested;
            MCTtestedBox.Text = string.Format(CultureInfo.CreateSpecificCulture("da-DK"), "{0:n}", tested);

            double percentagePositive = coronaDataUsed.PercentageOfData(coronaDataUsed.Positive, coronaDataUsed.Tested);
            MCpercentagePositiveBox.Text = string.Format("{0:n}%", percentagePositive);
        }
    }
}
