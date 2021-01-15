using Applikationen.DatabaseClasses;
using Applikationen.MunicipalityFunctions;
using System.Collections.Generic;
using System.Linq;
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
    }
}
