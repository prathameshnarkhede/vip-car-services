using DataApplication.Database;
using DataApplication.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataApplication.Calculations
{
    public class Middleware
    {
        public static IEnumerable<Car> GetAvailableCars(DateTime dateTime)
        {
            var dbMgr = new DatabaseManager();

            var cars = dbMgr.GetCars();

            var bookings = dbMgr.GetBookings();

            var groupedBookings = bookings.GroupBy(booking => booking.CarId);

            bookings.s
        }
    }
}
