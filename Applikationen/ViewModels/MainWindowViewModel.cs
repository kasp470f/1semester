using Applikationen.ViewModels.Commands;
using Applikationen.Views.Pages;
using System.ComponentModel;
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


        /// <summary>
        /// Allows for the swap of pages in the MainView Frame
        /// <para>Made by Keemon</para>
        /// </summary>
        /// <param name="name">The name of the page</param>
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
