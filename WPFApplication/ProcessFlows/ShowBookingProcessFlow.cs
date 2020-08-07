using DataApplication.Database;
using DataApplication.Model;
using System.Linq;
using WPFApplication.ViewModels;
using WPFApplication.Views;

namespace WPFApplication.ProcessFlows
{
    public class ShowBookingProcessFlow
    {
        public ShowBookingProcessFlow(Customer customer)
        {
            var window = new ShowBookingWindow();
            var vm = (ShowBookingViewModel)window.DataContext;
            vm.PropertyChanged += Vm_PropertyChanged;
            window.ShowDialog();
        }

        private void Vm_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals(nameof(ShowBookingViewModel.SelectedCar)))
            {
                var dbMgr = new DatabaseManager();
                var bookings = dbMgr.GetCarBookings((sender as ShowBookingViewModel).SelectedCar);
                (sender as ShowBookingViewModel).Bookings = bookings.Select(booking => 
                    new Models.ShowBooking
                    {
                        BookingId = booking.BookingId,
                        CarName = (sender as ShowBookingViewModel).SelectedCar.Name,
                        CustomerName = dbMgr.GetCustomer(booking.CustId).Name,
                        StartLocation = dbMgr.GetLocation(booking.StartLocation).LocationName,
                        EndLocation = dbMgr.GetLocation(booking.EndLocation).LocationName,
                        Hours = booking.Hours,
                        Price = booking.Price,
                        PriceWithTax = booking.PriceWithTax,
                        Time = booking.Time,
                        Type = booking.Type
                    }).ToList();
            }
        }
    }
}
