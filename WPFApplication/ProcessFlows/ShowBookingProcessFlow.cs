using DataApplication.Database;
using DataApplication.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            //if (e.PropertyName.Equals(nameof(ShowBookingViewModel.SelectedCar)))
            //{
            //    var dbMgr = new DatabaseManager();
            //    var bookings = dbMgr.GetCarBookings((sender as ShowBookingViewModel).SelectedCar);
            //    (sender as ShowBookingViewModel).Bookings = bookings;
            //}
        }
    }
}
