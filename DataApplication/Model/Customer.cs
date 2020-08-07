using System;

namespace DataApplication.Model
{
    public class Customer
    {
        public int CustomerId { get; set; }

        public string Name { get; set; }

        public int Category { get; set; }

        public string Address { get; set; }

        public string TVAnummer { get; set; }

        public int BookingCount { get; set; }
    }
}
