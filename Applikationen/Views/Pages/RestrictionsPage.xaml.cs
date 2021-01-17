using Applikationen.DatabaseClasses;
using Applikationen.MunicipalityFunctions;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Applikationen.Views.Pages
{
    /// <summary>
    /// Interaction logic for RestrictionsPage.xaml
    /// </summary>
    public partial class RestrictionsPage : Page
    {
        public string MunicipalityChoosen { get; set; }

        public RestrictionsPage()
        {
            InitializeComponent();

            DisplayMunicipalities();
            DisplayRestrictions();
            DisplayIndustries();
            DisplayMunicipalityRestrictions();
        }

        /// <summary>
        /// The method to display the different municipalities from the database and inject it into combobox
        /// <para>Keemon and Natasha</para>
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
        /// The method to display the different restrictions from the database and inject it into combobox
        /// <para>Keemon and Natasha</para>
        /// </summary>
        public void DisplayRestrictions()
        {
            Restriction restriction = new Restriction();
            List<Restriction> restrictions = restriction.GetRestriction();

            // Insert the municipalities into a combobox
            foreach (Restriction r in restrictions)
            {
                RestrictionDataGrid.Items.Add(r);
            }
        }

        /// <summary>
        /// The method to display the different industries from the database and inject it into combobox
        /// <para>Keemon and Natasha</para>
        /// </summary>
        public void DisplayIndustries()
        {
            Industry industry = new Industry();
            List<Industry> industries = industry.GetIndustry();

            // Insert the industries into a combobox
            foreach (Industry i in industries)
            {
                IndustryDataGrid.Items.Add(i);
            }
        }

        /// <summary>
        /// Adds a restriction to the selected industries and makes a list.
        /// <para>Kasper og Natasha</para>
        /// </summary>
        private void AddRestrictions_Click(object sender, RoutedEventArgs e)
        {
            // Creates lists to go through in the for loops
            Industry industry = new Industry();
            List<Industry> industries = industry.GetIndustry();

            Restriction restriction = new Restriction();
            List<Restriction> restrictions = restriction.GetRestriction();


            // Make the final result lists
            List<IndustryRestriction> industryRestrictions = new List<IndustryRestriction>();

            List<string> restrictionsJoinText = new List<string>();
            List<string> industriesJoinText = new List<string>();

            // Loop through the restrictions and check which row is checked
            for (int i = 0; i < restrictions.Count; i++)
            {
                // Get Cell content of the row
                CheckBox isCheckedR = RestrictionDataGrid.Columns[0].GetCellContent(RestrictionDataGrid.Items[i]) as CheckBox;

                // Check if it is null because if the object is not touched it will by default be null, and only be false if first checked and then unchecked
                if (isCheckedR == null)
                {
                    isCheckedR = new CheckBox();
                }

                // Check if the box is checked if so add it to the list.
                if (isCheckedR.IsChecked == true)
                {
                    TextBlock restrictionText = RestrictionDataGrid.Columns[1].GetCellContent(RestrictionDataGrid.Items[i]) as TextBlock;
                    restrictionsJoinText.Add(restrictionText.Text);
                }

            }

            // Loop through the industries and check which row is checked
            for (int i = 0; i < industries.Count; i++)
            {
                // Get Cell content of the row
                CheckBox isCheckedI = IndustryDataGrid.Columns[0].GetCellContent(IndustryDataGrid.Items[i]) as CheckBox;

                // Check if it is null because if the object is not touched it will by default be null, and only be false if first checked and then unchecked
                if (isCheckedI == null)
                {
                    isCheckedI = new CheckBox();
                }
                // Check if the box is checked if so add it to the list.
                if (isCheckedI.IsChecked == true)
                {
                    // The user picked the date of start and end of the restriction, but ran out of time.
                    string startDateText = DateTime.Now.ToString("yyyy/MM/dd");
                    string endDateText = DateTime.Now.ToString("yyyy/MM/dd");
                    // Get the text 
                    TextBlock restrictionText = RestrictionDataGrid.Columns[1].GetCellContent(RestrictionDataGrid.Items[i]) as TextBlock;
                    TextBlock industryText = IndustryDataGrid.Columns[1].GetCellContent(IndustryDataGrid.Items[i]) as TextBlock;
                    industriesJoinText.Add(industryText.Text);
                    // Adds industryRestriction object to list
                    industryRestrictions.Add(new IndustryRestriction()
                    {
                        R_Text = restrictionText.Text,
                        RI_StartDate = startDateText,
                        RI_EndDate = endDateText,
                        I_Name = industryText.Text,
                        M_Name = MunicipalityChoosen
                    });
                }
            }
            // Inserts the industryRestriction into the database
            IndustryRestriction irInsert = new IndustryRestriction();
            irInsert.InsertIndustryRestriction(industryRestrictions);

            // Refesh the datagrid
            RestrictionsPageIR.Items.Clear();
            RestrictionsPageIR.Items.Refresh();

            DisplayMunicipalityRestrictions();
        
            // Display choice
            ResctrictionsChoosen.Text = string.Join(", ", restrictionsJoinText);
            IndustriesChoosen.Text = string.Join(", ", industriesJoinText);

        }

        /// <summary>
        /// Allows for the selection of a checkbox without trying to edit.
        /// Does introduce a bug where you have to unfocus the selected row to uncheck it.
        /// <para>Made by Kasper</para>
        /// </summary>
        private new void GotFocus(object sender, RoutedEventArgs e)
        {
            var sen = sender as DataGrid;
            DataGridCell cell = e.OriginalSource as DataGridCell;
            if (cell != null && cell.Column is DataGridCheckBoxColumn)
            {
                sen.BeginEdit();
                CheckBox chkBox = cell.Content as CheckBox;
                if (chkBox != null)
                {
                    chkBox.IsChecked = !chkBox.IsChecked;
                }
            }
        }

        /// <summary>
        /// The method to display the different municipalitiesRestrictions from the database and inject it into a datagrid
        /// <para>Made by Keemon and Natasha</para>
        /// </summary>
        public void DisplayMunicipalityRestrictions()
        {
            Municipality municipality = new Municipality();
            List<IndustryRestriction> restrictions = municipality.DisplayMunicipalityRestrictions(MunicipalityChoosen);

            // Loop through the industryRestrictions
            foreach (IndustryRestriction ir in restrictions)
            {
                RestrictionsPageIR.Items.Add(ir);
            }
        }

        /// <summary>
        /// Changes the public string so we can display the correct data from that municipality.
        /// <para>Made by Kasper</para>
        /// </summary>
        private void MunicipalityBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MunicipalityChoosen = municipalityBox.SelectedValue.ToString();
            RestrictionsPageIR.Items.Clear();
            RestrictionsPageIR.Items.Refresh();

            DisplayMunicipalityRestrictions();
        }

        /// <summary>
        /// Deletes the industryrestriction from the datagrid and sent that information gathered in a list ot the database.
        /// <para>Made by Natasha and Keemon</para>
        /// </summary>
        public void DeleteIndustryRestrictions_Click(object sender, RoutedEventArgs e)
        {
            IndustryRestriction iRes = new IndustryRestriction();
            List<IndustryRestriction> list = new List<IndustryRestriction>();

            Municipality municipality = new Municipality();
            List<IndustryRestriction> restrictions = municipality.DisplayMunicipalityRestrictions(MunicipalityChoosen);

            // Loop through restrictions
            for (int i = 0; i < restrictions.Count; i++)
            {
                // Get the cell content of that row
                CheckBox isCheckedI = RestrictionsPageIR.Columns[0].GetCellContent(RestrictionsPageIR.Items[i]) as CheckBox;

                // Check if it is null because if the object is not touched it will by default be null, and only be false if first checked and then unchecked
                if (isCheckedI == null)
                {
                    isCheckedI = new CheckBox();
                }

                // Check if the box is checked if so add it to the list.
                if (isCheckedI.IsChecked == true)
                {
                    TextBlock industryName = RestrictionsPageIR.Columns[4].GetCellContent(RestrictionsPageIR.Items[i]) as TextBlock;
                    TextBlock restrictionText = RestrictionsPageIR.Columns[1].GetCellContent(RestrictionsPageIR.Items[i]) as TextBlock;
                    TextBlock industryID = RestrictionsPageIR.Columns[5].GetCellContent(RestrictionsPageIR.Items[i]) as TextBlock;
                    // Adds to list.
                    list.Add(new IndustryRestriction()
                    {
                        R_Text = restrictionText.Text,
                        I_Name = industryName.Text,
                        M_Name = MunicipalityChoosen,
                        RI_I_ID = Convert.ToInt32(industryID.Text)
                    });
                }
            }

            // Sends list to the database to be deleted
            iRes.DeleteIndustryRestriction(list);

            RestrictionsPageIR.Items.Clear();
            RestrictionsPageIR.Items.Refresh();

            DisplayMunicipalityRestrictions();
        }
    }
}
