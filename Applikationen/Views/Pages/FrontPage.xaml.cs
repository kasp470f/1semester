using Applikationen.CoronaData;
using Applikationen.DatabaseClasses;
using Applikationen.MunicipalityFunctions;
using Microsoft.WindowsAPICodePack.Dialogs;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Applikationen.Views.Pages
{
    public partial class FrontPage : Page
    {
        public string FolderPath { get; set; }

        public string MunicipalityChoosen { get; set; }

        public bool IndicatorStatus = true;


        public FrontPage()
        {
            InitializeComponent();

            // Displays municipalities in dropdown menu/combobox
            DisplayMunicipalities();
            FolderPath = string.Empty;
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
            if (MunicipalityChoosen != null)
            {
                MunicipalityDataBinding();
            }
        }

        // Keemon & Natasha
        public void DisplayMunicipalities()
        {
            Municipality municipality = new Municipality();

            List<ComboBoxItem> items = municipality.GetMunicipalityList();

            foreach (var item in items)
            {
                municipalityBox.Items.Add(item);
            }
        }

        public void DisplayMunicipalityRestrictions()
        {
            Municipality municipality = new Municipality();
            List<IndustryRestriction> restrictions = municipality.DisplayMunicipalityRestrictions(MunicipalityChoosen);

            foreach (IndustryRestriction ir in restrictions)
            {
                FrontPageIR.Items.Add(ir);
            }
        }

        //Kasper 
        private void RegionDataBinding()
        {
            try
            {
                var regionDataCSV = RegionData.ReadCSV(FolderPath + "\\Region_summary.csv");

                var coronaDataUsed = regionDataCSV.Last();

                double positive = coronaDataUsed.Positive;
                DKpositiveBox.Text = string.Format("{0:n}", positive);

                double tested = coronaDataUsed.Tested;
                DKtestedBox.Text = string.Format("{0:n}", tested);

                double percentagePositive = coronaDataUsed.PercentageOfData(coronaDataUsed.Positive, coronaDataUsed.Tested);
                DKpercentagePositiveBox.Text = string.Format("{0:n}%", percentagePositive);

                double hospitalized = coronaDataUsed.Hospitalized;
                DKhospitalizedBox.Text = string.Format("{0}", hospitalized);

                double deaths = coronaDataUsed.Deaths;
                DKdeathsBox.Text = string.Format("{0}", deaths);
                MessageBox.Show("Files uploaded successfully");
            }
            catch (System.Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        //Kasper 
        private void MunicipalityDataBinding()
        {
            try
            {
                var MunicipalityDataCSV = MunicipalityPositive.ReadCSV(FolderPath + "\\Municipality_test_pos.csv");

                var coronaDataUsed = MunicipalityDataCSV.Single(Municipality => Municipality.Municipality == MunicipalityChoosen);

                long positive = coronaDataUsed.Positive;
                MCpositiveBox.Text = string.Format("{0}", positive);

                long tested = coronaDataUsed.Tested;
                MCTtestedBox.Text = string.Format("{0}", tested);

                double percentagePositive = coronaDataUsed.PercentageOfData(coronaDataUsed.Positive, coronaDataUsed.Tested);
                MCpercentagePositiveBox.Text = string.Format("{0:n}%", percentagePositive);

                Municipality municipality = new Municipality();
                List<Municipality> municipalitiesRegion = municipality.GetMunicipalityFullList();

                var coronaDataUsedRegion = municipalitiesRegion.Single(Municipality => Municipality.Name == MunicipalityChoosen);

                var regionCSV = RegionData.ReadCSV(FolderPath + "\\Region_summary.csv");

                var regionDataUsed = regionCSV.Single(Region => Region.Region == coronaDataUsedRegion.Region);

                double hospitalized = regionDataUsed.Hospitalized;
                MChospitalizedBox.Text = string.Format("{0}", hospitalized);

                double deaths = regionDataUsed.Deaths;
                MCdeathsBox.Text = string.Format("{0}", deaths);

            }
            catch (System.Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        //Kasper 
        private void MunicipalityBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MunicipalityChoosen = municipalityBox.SelectedValue.ToString();
            DisplayMunicipalityRestrictions();
            if (FolderPath != string.Empty)
            {
                MunicipalityDataBinding();
            }
        }
    }
}
