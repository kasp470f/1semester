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
using Microsoft.WindowsAPICodePack.Dialogs;

namespace Applikationen.Views.Pages
{
    /// <summary>
    /// Interaction logic for FrontPage.xaml
    /// </summary>
    public partial class FrontPage : Page
    {
        public bool IndicatorStatus = true;
        public FrontPage()
        {
            InitializeComponent();

            if (IndicatorStatus == true) Indicator.Style = FindResource("IndicatorGood") as Style;
            else Indicator.Style = FindResource("IndicatorBad") as Style;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.InitialDirectory = "C:\\Users";
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                csvPath.Text = dialog.FileName;
            }
        }
    }
}
