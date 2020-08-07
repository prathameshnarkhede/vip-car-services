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
            InitializeData();

            RegisterCustomerCommand = new RelayCommand(RegisterCustomer);

            BookCarCommand = new RelayCommand(BookCar);

            ShowBookingsCommand = new RelayCommand(ShowBooking);
        }

        public void InitializeData()
        {
            var dbMgr = new DatabaseManager();
            Customers.Clear();
            foreach (var item in dbMgr.GetCustomers())
            {
                Customers.Add(item);
            }
        }

        public ICommand RegisterCustomerCommand { get; set; }

        private void RegisterCustomer(object obj)
        {
            RaisePropertyChanged(nameof(RegisterCustomerCommand));
        }

        public ICommand BookCarCommand { get; set; }

        private void BookCar(object obj)
        {
            RaisePropertyChanged(nameof(BookCarCommand));
        }

        public ICommand ShowBookingsCommand { get; set; }

        private void ShowBooking(object obj)
        {
            RaisePropertyChanged(nameof(ShowBookingsCommand));
        }

        private ObservableCollection<Customer> _customers = new ObservableCollection<Customer>();
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

        private Customer _selectedCustomer;

        public Customer SelectedCustomer
        {
            get
            {
                return _selectedCustomer;
            }
            set
            {
                _selectedCustomer = value;
                RaisePropertyChanged(nameof(SelectedCustomer));
            }
        }


        public override void Submit(object obj)
        {
            throw new NotImplementedException();
        }
    }
}
