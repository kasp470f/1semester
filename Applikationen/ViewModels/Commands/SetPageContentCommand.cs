using System;
using System.Windows.Input;

namespace Applikationen.ViewModels.Commands
{
    class SetPageContentCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        MainWindowViewModel vm;
        public SetPageContentCommand(MainWindowViewModel mvvm)
        {
            vm = mvvm;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            vm.SwapPageContent(parameter.ToString());
        }
    }
}
