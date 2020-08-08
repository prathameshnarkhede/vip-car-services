using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace WPFApplication.Views
{
    /// <summary>
    /// Interaction logic for ShowBookingWindow.xaml
    /// </summary>
    public partial class ShowBookingWindow : Window
    {
        public ShowBookingWindow()
        {
            InitializeComponent();
        }

        private void DataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            var desc = e.PropertyDescriptor as PropertyDescriptor;
            if (desc.Attributes[typeof(Models.ColumnNameAttribute)] is Models.ColumnNameAttribute att)
            {
                e.Column.Header = att.Name;
            }
        }
    }
}
