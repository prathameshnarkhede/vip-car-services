using System;

namespace WPFApplication.Models
{
    public class ShowBooking
    {
        [ColumnName("Booking ID")]
        public int BookingId { get; set; }

        [ColumnName("Customer Name")]
        public string CustomerName { get; set; }

        [ColumnName("Car Name")]
        public string CarName { get; set; }

        [ColumnName("Start Location")]
        public string StartLocation { get; set; }

        [ColumnName("End Location")]
        public string EndLocation { get; set; }

        [ColumnName("Booking Time")]
        public DateTime Time { get; set; }

        [ColumnName("Package Type")]
        public string Type { get; set; }

        [ColumnName("Number of hours")]
        public int Hours { get; set; }

        [ColumnName("Price")]
        public double Price { get; set; }

        [ColumnName("Price with Tax")]
        public double PriceWithTax { get; set; }
    }
    public class ColumnNameAttribute : Attribute
    {
        public ColumnNameAttribute(string Name) { this.Name = Name; }
        public string Name { get; set; }
    }
}
