using System;
using System.Collections.Generic;
using System.Text;

namespace DataApplication.Model
{
    public class Booking
    {
        public int BookingId { get; set; }

        public int CustId { get; set; }

        public int CarId { get; set; }

        public int StartLocation { get; set; }

        public int EndLocation { get; set; }

        public DateTime Time { get; set; }

        public string Type { get; set; }

        public int Hours { get; set; }

        public double Price { get; set; }

        public double PriceWithTax { get; set; }
    }
}
