using DataApplication.Calculations;
using DataApplication.Database;
using DataApplication.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WPFApplication.ViewModels;
using WPFApplication.Views;

namespace WPFApplication.ProcessFlows
{
   public class BookCarProcessFlow
    {
        private readonly Customer _customer;
        private BookCarWindow window;

        public BookCarProcessFlow(Customer customer)
        {
            this._customer = customer;

            window = new BookCarWindow();
            var vm = (BookCarViewModel)window.DataContext;
            vm.PropertyChanged += Vm_PropertyChanged;
            window.ShowDialog();
        }

        private async void Vm_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            var dbMgr = new DatabaseManager();
            var vm = sender as BookCarViewModel;
            if (e.PropertyName.Equals(nameof(BookCarViewModel.SubmitCommand)))
            {
                vm.Booking.CustId = _customer.CustomerId;

                var isValid = ValidateBookingData(vm.Booking);
                if (!isValid)
                    return;
                var result = await BookCar(vm.Booking);

                if (result)
                {
                    MessageBox.Show("Car booked Successfully.", "Success");
                    dbMgr.UpdateCustomerBookingCount(_customer.CustomerId);
                    vm.InitializeData();
                    window.Close();
                }
            }
            if (e.PropertyName.Equals(nameof(BookCarViewModel.UpdateCostCommand)))
            {
                var car = dbMgr.GetCar(vm.Booking.CarId);

                vm.Amount = Infrastructure.CalculateCost(_customer.CustomerId, vm.Booking, car);

                vm.TotalAmountWithTax = Infrastructure.CalculateWithTax(vm.Amount);
            }
        }

        private bool ValidateBookingData(Booking booking)
        {
            if(!Infrastructure.CheckCarAvailability(booking.CarId, booking.Time))
            {
                MessageBox.Show("Selected car is Not available for selected time");
                return false;
            }
            if(!Infrastructure.IsInArrangement(booking))
            {
                MessageBox.Show("Selcted arrangement is not available to start in selected time");
                return false;
            }
            Infrastructure.PopulateTimings(booking);
            return true;
        }

        private async Task<bool> BookCar(Booking booking)
        {
            return await Task.Run(() =>
            {
                // Wait to demonstrate Asynchronous behaviour of WPF.
                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(5));

                var dbMgr = new DatabaseManager();
                return dbMgr.AddBooking(booking);
            });
        }
    }
}
