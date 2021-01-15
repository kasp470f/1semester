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
