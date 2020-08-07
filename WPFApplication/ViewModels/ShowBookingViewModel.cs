using DataApplication.Database;
using DataApplication.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace WPFApplication.ViewModels
{
    public class ShowBookingViewModel : ViewModelBase
    {
        public ShowBookingViewModel()
        {
            var dbMgr = new DatabaseManager();
            Cars = new ObservableCollection<Car>(dbMgr.GetCars());
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

        private Car _selectedCar;

        public Car SelectedCar
        {
            get
            {
                return _selectedCar;
            }
            set
            {
                _selectedCar = value;
                RaisePropertyChanged(nameof(SelectedCar));
            }
        }

        private List<Booking> _booking;

        public List<Booking> Bookings
        {
            get
            {
                return _booking;
            }
            set
            {
                _booking = value;
                RaisePropertyChanged(nameof(Bookings));
            }
        }

        public override void Submit(object obj)
        {
            throw new NotImplementedException();
        }
    }
}
