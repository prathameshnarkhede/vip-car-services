using DataApplication.Database;
using DataApplication.Model;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace WPFApplication.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel()
        {
            var dbMgr = new DatabaseManager();
            Customers = new ObservableCollection<Customer>(dbMgr.GetCustomers());

            RegisterCustomerCommand = new RelayCommand(RegisterCustomer);

            BookCarCommand = new RelayCommand(BookCar);

            ShowBookingsCommand = new RelayCommand(ShowBooking);
        }

        public ICommand RegisterCustomerCommand { get; set; }

        private void RegisterCustomer(object obj)
        {
            new Views.RegisterCustomerWindow().ShowDialog();
        }

        public ICommand BookCarCommand { get; set; }

        private void BookCar(object obj)
        {
            new Views.BookCarWindow().ShowDialog();
        }

        public ICommand ShowBookingsCommand { get; set; }

        private void ShowBooking(object obj)
        {
            new Views.ShowBookingWindow().ShowDialog();
        }

        private ObservableCollection<Customer> _customers;
        public ObservableCollection<Customer> Customers
        {
            get
            {
                return _customers;
            }
            set
            {
                _customers = value;
                RaisePropertyChanged(nameof(Customers));
            }
        }

        public override void Submit(object obj)
        {
            throw new NotImplementedException();
        }
    }
}
