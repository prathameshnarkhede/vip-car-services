using DataApplication.Database;
using DataApplication.Model;
using System.Collections.ObjectModel;

namespace WPFApplication.ViewModels
{
    public class RegisterCustomerViewModel : ViewModelBase
    {
        public RegisterCustomerViewModel()
        {
            var dbMgr = new DatabaseManager();
            CustomerTypes = new ObservableCollection<CustomerType>(dbMgr.GetCustomerTypes());
        }

        private ObservableCollection<CustomerType> _customerTypes;

        public ObservableCollection<CustomerType> CustomerTypes
        {
            get
            {
                return _customerTypes;
            }
            set
            {
                _customerTypes = value;
                RaisePropertyChanged(nameof(CustomerType));
            }
        }

        private Customer customer = new Customer() { BookingCount = 0 };

        public Customer Customer
        {
            get
            {
                return customer;
            }
            set
            {
                customer = value;
                RaisePropertyChanged(nameof(Customer));
            }
        }

        public override void Submit(object obj)
        {
            RaisePropertyChanged(nameof(SubmitCommand));
        }
    }
}
