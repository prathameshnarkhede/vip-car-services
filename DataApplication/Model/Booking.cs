using System;
using System.Collections.Generic;
using System.Text;

namespace DataApplication.Model
{
    public class Booking
    {
        [ColumnName("Booking ID")]
        public int BookingId { get; set; }

        [ColumnName("Customer ID")]
        public int CustId { get; set; }

        [ColumnName("Car ID")]
        public int CarId { get; set; }

        [ColumnName("Start Location")]
        public int StartLocation { get; set; }

        [ColumnName("End Location")]
        public int EndLocation { get; set; }

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

    public class ColumnNameAttribute : System.Attribute
    {
        public ColumnNameAttribute(string Name) { this.Name = Name; }
        public string Name { get; set; }
    }
}
