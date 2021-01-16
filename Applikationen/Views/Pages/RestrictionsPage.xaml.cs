using Applikationen.DatabaseClasses;
using Applikationen.MunicipalityFunctions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Applikationen.Views.Pages
{
    /// <summary>
    /// Interaction logic for RestrictionsPage.xaml
    /// </summary>
    public partial class RestrictionsPage : Page
    {
        public RestrictionsPage()
        {
            InitializeComponent();

            DisplayMunicipalities();
            DisplayRestrictions();
            DisplayIndustries();
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

        public void DisplayRestrictions()
        {
            Restriction restriction = new Restriction();
            List<Restriction> restrictions = restriction.GetRestriction();

            foreach (Restriction r in restrictions)
            {
                RestrictionDataGrid.Items.Add(r);
            }
        }

        public void DisplayIndustries()
        {
            Industry industry = new Industry();
            List<Industry> industries = industry.GetIndustry();

            foreach (Industry i in industries)
            {
                IndustryDataGrid.Items.Add(i);
            }
        }

        // Kasper og Natasha
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Industry industry = new Industry();
            List<Industry> industries = industry.GetIndustry();

            Restriction restriction = new Restriction();
            List<Restriction> restrictions = restriction.GetRestriction();

            List<IndustryRestriction> industryRestrictions = new List<IndustryRestriction>();

            List<string> restrictionsJoinText = new List<string>();

            for (int i = 0; i < restrictions.Count; i++)
            {
                CheckBox isCheckedR = RestrictionDataGrid.Columns[0].GetCellContent(RestrictionDataGrid.Items[i]) as CheckBox;
                if (isCheckedR == null)
                {
                    isCheckedR = new CheckBox();
                }
                if(isCheckedR.IsChecked == true)
                {
                    TextBlock restrictionText = RestrictionDataGrid.Columns[1].GetCellContent(RestrictionDataGrid.Items[i]) as TextBlock;
                    restrictionsJoinText.Add(restrictionText.Text);
                }

            }

            for (int i = 0; i < industries.Count; i++)
            {
                CheckBox isCheckedI = IndustryDataGrid.Columns[0].GetCellContent(IndustryDataGrid.Items[i]) as CheckBox;
                if (isCheckedI == null)
                {
                    isCheckedI = new CheckBox();
                }
                if (isCheckedI.IsChecked == true)
                {
                    DateTime startDateText = new DateTime(2008, 5, 1, 8, 30, 52);
                    DateTime endDateText = new DateTime(2008, 5, 1, 8, 30, 52);
                    TextBlock industryText = IndustryDataGrid.Columns[1].GetCellContent(IndustryDataGrid.Items[i]) as TextBlock;
                    industryRestrictions.Add(new IndustryRestriction()
                    {
                        R_Text = string.Join(", ", restrictionsJoinText),
                        RI_StartDate = startDateText,
                        RI_EndDate = endDateText,
                        I_Name = industryText.Text
                    });
                }

            }
        }

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

        //
        //
        // MANGLER KODE FOR UDHENTNING AF ID I GRIDVIEW
        //
        //
        public void DeleteIndustryRestrictions()
        {
            // SKAL UDHENTE LISTE FRA DATA VALGT TIL SLETNING
            IndustryRestriction iRes = new IndustryRestriction();
            List<IndustryRestriction> list = new List<IndustryRestriction>();
            iRes.DeleteIndustryRestriction(list);

            RestrictionsPageIR.Columns.Clear();
            RestrictionsPageIR.Items.Clear();
            RestrictionsPageIR.Items.Refresh();

            // NEED TO INSERT FUNCTION TO POPULATE RESTRICTIONSPAGEIR AGAIN
        }
    }
}
