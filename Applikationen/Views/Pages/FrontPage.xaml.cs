using Applikationen.CoronaData;
using Applikationen.DatabaseClasses;
using Applikationen.MunicipalityFunctions;
using Microsoft.WindowsAPICodePack.Dialogs;
using System.Collections.Generic;
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

        /// <summary>
        /// Allows for the creation of the front page look and the data to be injected into the right place.
        /// <para>Kasper, Keemon and Natasha</para>
        /// </summary>
        public FrontPage()
        {
            InitializeComponent();
            
            // Displays municipalities in dropdown menu/combobox
            DisplayMunicipalities();
            FolderPath = string.Empty;
            Indicator.Style = FindResource("IndicatorNoChoice") as Style;
        }

        /// <summary>
        /// This is the allow to upload button.
        /// <para>Made by Kasper</para>
        /// </summary>
        private void UploadButton_Click(object sender, RoutedEventArgs e)
        {
            // Creates a new select window and sends the user to the InitialDirectory
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


        /// <summary>
        /// The method to display the different municipalities from the database and inject it into combobox
        /// <para>Made by Keemon and Natasha</para>
        /// </summary>
        public void DisplayMunicipalities()
        {
            Municipality municipality = new Municipality();
            List<ComboBoxItem> items = municipality.GetMunicipalityList();

            // Insert the municipality into a combobox
            foreach (var item in items)
            {
                municipalityBox.Items.Add(item);
            }
        }


        /// <summary>
        /// The method to display the all the MunicipalityRestrictions for the choosen municipality, data is from the database and inject it into datagrid.
        /// <para>Made by Keemon and Natasha</para>
        /// </summary>
        public void DisplayMunicipalityRestrictions()
        {
            Municipality municipality = new Municipality();
            List<IndustryRestriction> restrictions = municipality.DisplayMunicipalityRestrictions(MunicipalityChoosen);

            // Insert the industryrestrictions into a datagrid
            foreach (IndustryRestriction ir in restrictions)
            {
                FrontPageIR.Items.Add(ir);
            }
        }

        /// <summary>
        /// Edits the TextBlock in the FrontPage.xaml to include the data from the Region_summary.csv when uploaded.
        /// <para>Made by Kasper</para>
        /// </summary>
        private void RegionDataBinding()
        {
            try
            {
                var regionDataCSV = RegionData.ReadCSV(FolderPath + "\\Region_summary.csv");

                // Takes the last element of the list because that is the total.
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

        /// <summary>
        /// Edits the TextBlock in the FrontPage.xaml to include the data from the Municipality_test_pos.csv when uploaded.
        /// <para>Made by Kasper</para>
        /// </summary>
        private void MunicipalityDataBinding()
        {
            try
            {
                var MunicipalityDataCSV = MunicipalityPositive.ReadCSV(FolderPath + "\\Municipality_test_pos.csv");

                // Searches for the closest looking string from that list.
                var coronaDataUsed = MunicipalityDataCSV.Single(Municipality => Municipality.Municipality == MunicipalityChoosen);

                long positive = coronaDataUsed.Positive;
                MCpositiveBox.Text = string.Format("{0}", positive);

                long tested = coronaDataUsed.Tested;
                MCTtestedBox.Text = string.Format("{0}", tested);

                double percentagePositive = coronaDataUsed.PercentageOfData(coronaDataUsed.Positive, coronaDataUsed.Tested);
                MCpercentagePositiveBox.Text = string.Format("{0:n}%", percentagePositive);

                // Indicator status ændres
                if (percentagePositive > 2)
                {
                    Indicator.Style = FindResource("IndicatorBad") as Style;
                }
                else if (percentagePositive < 2)
                {
                    Indicator.Style = FindResource("IndicatorGood") as Style;
                }

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

        /// <summary>
        /// Changes the public string so we that it can be used to look through csv files.
        /// <para>Made by Kasper</para>
        /// </summary>
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
