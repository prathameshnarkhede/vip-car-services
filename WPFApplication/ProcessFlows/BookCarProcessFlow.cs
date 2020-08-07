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

        public BookCarProcessFlow(Customer customer)
        {
            this._customer = customer;

            var window = new BookCarWindow();
            var vm = (BookCarViewModel)window.DataContext;
            vm.PropertyChanged += Vm_PropertyChanged;
            window.ShowDialog();
        }

        private async void Vm_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            var vm = sender as BookCarViewModel;
            if (e.PropertyName.Equals(nameof(BookCarViewModel.SubmitCommand)))
            {
                vm.Booking.CustId = _customer.CustomerId;

                var result = await BookCar(vm.Booking);

                if (result)
                {
                    MessageBox.Show("Customer added Successfully.", "Success");
                }
            }
            if (e.PropertyName.Equals(nameof(BookCarViewModel.UpdateCostCommand)))
            {
                var dbMgr = new DatabaseManager();
                var car = dbMgr.GetCar(vm.Booking.CarId);

                var packagedCost = Infrastructure.CalculatePackagedCost(vm.Booking, car);

                vm.Amount = Middleware.CalculatePriceAfterDiscount(_customer.CustomerId, packagedCost);

                vm.TotalAmountWithTax = Infrastructure.CalculateWithTax(vm.Amount);
            }
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
