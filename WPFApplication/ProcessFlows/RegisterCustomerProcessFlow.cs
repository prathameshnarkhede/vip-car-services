using DataApplication.Database;
using DataApplication.Model;
using System;
using System.Threading.Tasks;
using System.Windows;
using WPFApplication.ViewModels;
using WPFApplication.Views;

namespace WPFApplication.ProcessFlows
{
    public class RegisterCustomerProcessFlow
    {
        private RegisterCustomerWindow window;
        public RegisterCustomerProcessFlow()
        {
            window = new RegisterCustomerWindow();
            var vm = (RegisterCustomerViewModel)window.DataContext;
            vm.PropertyChanged += Vm_PropertyChanged;
            window.ShowDialog();
        }

        private async void Vm_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals(nameof(RegisterCustomerViewModel.SubmitCommand)))
            {
                var vm = sender as RegisterCustomerViewModel;
                var result = await AddCustomerToDb(vm.Customer);

                if (result)
                {
                    MessageBox.Show("Customer Added Successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    window.Close();
                } 
            }
        }

        private async Task<bool> AddCustomerToDb(Customer customer)
        {
            return await Task.Run(() =>
            {
                var dbMgr = new DatabaseManager();
                return dbMgr.AddCustomer(customer);
            });
        }
    }
}
