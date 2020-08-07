using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace WPFApplication
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            //DataApplication.Calculations.Infrastructure.CalculateTotalFromHourlyRate(30, DateTime.Now, 5);
            new ProcessFlow();
        }
    }
}
