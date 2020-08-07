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
        public BookCarProcessFlow(Customer customer)
        {
            var window = new BookCarWindow();
            var vm = (BookCarViewModel)window.DataContext;
            vm.PropertyChanged += Vm_PropertyChanged;
            window.ShowDialog();
        }

        private async void Vm_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals(nameof(BookCarViewModel.SubmitCommand)))
            {
                var vm = sender as BookCarViewModel;

                var result = await BookCar(vm.Booking);

                if (result)
                {
                    MessageBox.Show("Customer added Successfully.", "Success");
                }
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
