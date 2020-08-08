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
            InitializeData();
            SelectPackageCommand = new RelayCommand(SelectPackage);
            UpdateCostCommand = new RelayCommand(CalculateCost);
        }

        public void InitializeData()
        {
            var dbMgr = new DatabaseManager();
            //Cars = new ObservableCollection<Car>(DataApplication.Calculations.Infrastructure.GetAvailableCars());
            Cars = new ObservableCollection<Car>(dbMgr.GetCars());
            Locations = new ObservableCollection<Location>(dbMgr.GetLocations());
        }

        private void CalculateCost(object obj)
        {
            RaisePropertyChanged(nameof(UpdateCostCommand));
        }

        private void SelectPackage(object obj)
        {
            Booking.Type = obj as string;
            UpdateCostCommand.Execute(null);
        }

        private bool _isWorkerBusy = true;

        public bool IsWorkerAvailable
        {
            get
            {
                return _isWorkerBusy;
            }
            set
            {
                _isWorkerBusy = value;
                RaisePropertyChanged(nameof(IsWorkerAvailable));
            }
        }


        public int Hours
        {
            get
            {
                return Booking.Hours;
            }
            set
            {
                Booking.Hours = value;
                RaisePropertyChanged(nameof(Hours));
                UpdateCostCommand.Execute(null);
            }
        }


        public double Amount
        {
            get
            {
                return Booking.Price;
            }
            set
            {
                Booking.Price = value;
                RaisePropertyChanged(nameof(Amount));
            }
        }

        public double TotalAmountWithTax
        {
            get
            {
                return Booking.PriceWithTax;
            }
            set
            {
                Booking.PriceWithTax = value;
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

        private Booking _booking = new Booking { Hours = 0, Time = DateTime.Now, Type = "Hourly", Price = 0, PriceWithTax = 0 };

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
