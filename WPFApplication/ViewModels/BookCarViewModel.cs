using DataApplication.Database;
using DataApplication.Model;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace WPFApplication.ViewModels
{
    public class BookCarViewModel : ViewModelBase
    {
        public ICommand SelectPackageCommand { get; set; }

        public ICommand UpdateCostCommand { get; set; }

        public BookCarViewModel()
        {
            var dbMgr = new DatabaseManager();
            Cars = new ObservableCollection<Car>(dbMgr.GetCars());
            Locations = new ObservableCollection<Location>(dbMgr.GetLocations());
            SelectPackageCommand = new RelayCommand(SelectPackage);
            UpdateCostCommand = new RelayCommand(CalculateCost);
        }

        private void CalculateCost(object obj)
        {
            RaisePropertyChanged(nameof(UpdateCostCommand));
        }

        private void SelectPackage(object obj)
        {
            Booking.Type = obj as string;
        }

        private double _amount = 0.0;

        public double Amount
        {
            get
            {
                return _amount;
            }
            set
            {
                _amount = value;
                RaisePropertyChanged(nameof(Amount));
            }
        }

        private double _totalAmountWithTax = 0.0;

        public double TotalAmountWithTax
        {
            get
            {
                return _totalAmountWithTax;
            }
            set
            {
                _totalAmountWithTax = value;
                RaisePropertyChanged(nameof(TotalAmountWithTax));
            }
        }

        private ObservableCollection<Car> _cars;

        public ObservableCollection<Car> Cars
        {
            get
            {
                return _cars;
            }
            set
            {
                _cars = value;
                RaisePropertyChanged(nameof(Cars));
            }
        }

        private ObservableCollection<Location> _locations;

        public ObservableCollection<Location> Locations
        {
            get
            {
                return _locations;
            }
            set
            {
                _locations = value;
                RaisePropertyChanged(nameof(Locations));
            }
        }

        private Booking _booking = new Booking { Hours = 0, Time = DateTime.Now, Type = "Hourly" };

        public Booking Booking
        {
            get
            {
                return _booking;
            }
            set
            {
                _booking = value;
                RaisePropertyChanged(nameof(Booking));
            }
        }

        public override void Submit(object obj)
        {
            RaisePropertyChanged(nameof(SubmitCommand));
        }
    }
}
