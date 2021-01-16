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
        public string MunicipalityChoosen { get; set; }

        public RestrictionsPage()
        {
            InitializeComponent();

            DisplayMunicipalities();
            DisplayRestrictions();
            DisplayIndustries();
            DisplayMunicipalityRestrictions();
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
                    string startDateText = DateTime.Now.ToString("yyyy/MM/dd");
                    string endDateText = DateTime.Now.ToString("yyyy/MM/dd");
                    TextBlock restrictionText = RestrictionDataGrid.Columns[1].GetCellContent(RestrictionDataGrid.Items[i]) as TextBlock;
                    TextBlock industryText = IndustryDataGrid.Columns[1].GetCellContent(IndustryDataGrid.Items[i]) as TextBlock;
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
            IndustryRestriction irInsert = new IndustryRestriction();
            irInsert.InsertIndustryRestriction(industryRestrictions);

            RestrictionsPageIR.Items.Clear();
            RestrictionsPageIR.Items.Refresh();

            DisplayMunicipalityRestrictions();
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

        public void DisplayMunicipalityRestrictions()
        {
            Municipality municipality = new Municipality();
            List<IndustryRestriction> restrictions = municipality.DisplayMunicipalityRestrictions(MunicipalityChoosen);

            foreach (IndustryRestriction ir in restrictions)
            {
                RestrictionsPageIR.Items.Add(ir);
            }
        }

        //Kasper 
        private void MunicipalityBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MunicipalityChoosen = municipalityBox.SelectedValue.ToString();
            DisplayMunicipalityRestrictions();
        }

        public void DeleteIndustryRestrictions_Click()
        {
            IndustryRestriction iRes = new IndustryRestriction();
            List<IndustryRestriction> list = new List<IndustryRestriction>();

            Municipality municipality = new Municipality();
            List<IndustryRestriction> restrictions = municipality.DisplayMunicipalityRestrictions(MunicipalityChoosen);

            for (int i = 0; i < restrictions.Count; i++)
            {
                CheckBox isCheckedI = RestrictionsPageIR.Columns[0].GetCellContent(RestrictionsPageIR.Items[i]) as CheckBox;
                if (isCheckedI == null)
                {
                    isCheckedI = new CheckBox();
                }
                if (isCheckedI.IsChecked == true)
                {
                    TextBlock industryName = RestrictionsPageIR.Columns[4].GetCellContent(RestrictionsPageIR.Items[i]) as TextBlock;
                    TextBlock restrictionText = RestrictionsPageIR.Columns[1].GetCellContent(RestrictionsPageIR.Items[i]) as TextBlock;
                    list.Add(new IndustryRestriction()
                    {
                        R_Text = restrictionText.Text,
                        I_Name = industryName.Text,
                        M_Name = MunicipalityChoosen
                    });
                }
            }

            iRes.DeleteIndustryRestriction(list);

            RestrictionsPageIR.Columns.Clear();
            RestrictionsPageIR.Items.Clear();
            RestrictionsPageIR.Items.Refresh();

            DisplayMunicipalityRestrictions();
        }
    }
}
