using Applikationen.ViewModels.Commands;
using Applikationen.Views.Pages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace Applikationen.ViewModels
{
    class MainWindowViewModel : INotifyPropertyChanged
    {

        public Page PageContent { get; set; }

        private ICommand setPageContentCommand { get; set; }

        public ICommand SetPageContentCommand { get { return setPageContentCommand; } }
        public MainWindowViewModel()
        {
            Page frontPage = new FrontPage();
            PageContent = frontPage;

            // sætte SetPageContentCommand
            setPageContentCommand = new SetPageContentCommand(this);
        }



        public void SwapPageContent(string name)
        {
            switch (name)
            {
                case "FrontPage":
                    PageContent = new FrontPage();
                    break;
                case "RestrictionsPage":
                    PageContent = new RestrictionsPage();
                    break;
                default:
                    PageContent = new FrontPage();
                    break;
            }
            OnPropertyChanged("PageContent");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string x)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(x));
        }

    }
}
