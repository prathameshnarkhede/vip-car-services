using DataApplication.Model;
using System.Windows;
using WPFApplication.ProcessFlows;
using WPFApplication.ViewModels;

namespace WPFApplication
{
    public class ProcessFlow
    {
        private Customer _selectedCustomer = null;

        public ProcessFlow()
        {
            var mainWindow = new MainWindow();

            var mainWindowVM = (MainWindowViewModel)mainWindow.DataContext;

            mainWindowVM.PropertyChanged += MainWindowVM_PropertyChanged;

            mainWindow.Show();
        }

        private void MainWindowVM_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals(nameof(MainWindowViewModel.SelectedCustomer)))
            {
                _selectedCustomer = (sender as MainWindowViewModel).SelectedCustomer;
            }

            if (e.PropertyName.Equals(nameof(MainWindowViewModel.RegisterCustomerCommand)))
            {
                new RegisterCustomerProcessFlow();
                (sender as MainWindowViewModel).InitializeData();
            }

            if (e.PropertyName.Equals(nameof(MainWindowViewModel.BookCarCommand)))
            {
                if (_selectedCustomer is null)
                {
                    MessageBox.Show("Please Select Customer from top right corner", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                new BookCarProcessFlow(_selectedCustomer);
            }

            if (e.PropertyName.Equals(nameof(MainWindowViewModel.ShowBookingsCommand)))
            {
                if (_selectedCustomer is null)
                {
                    MessageBox.Show("Please Select Customer from top right corner", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                new ShowBookingProcessFlow(_selectedCustomer);
            }
        }
    }
}
