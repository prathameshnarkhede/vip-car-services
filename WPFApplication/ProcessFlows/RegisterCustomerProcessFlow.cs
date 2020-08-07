﻿using DataApplication.Database;
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
        public RegisterCustomerProcessFlow()
        {
            var window = new RegisterCustomerWindow();
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
                } 
            }
        }

        private async Task<bool> AddCustomerToDb(Customer customer)
        {
            return await Task.Run(() =>
            {
                // Wait to demonstrate Asynchronous behaviour of WPF.
                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(5));

                var dbMgr = new DatabaseManager();
                return dbMgr.AddCustomer(customer);
            });
        }
    }
}