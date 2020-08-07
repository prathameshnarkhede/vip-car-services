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
        //public static IEnumerable<Car> GetAvailableCars(DateTime dateTime)
        //{
        //    var dbMgr = new DatabaseManager();

        //    var cars = dbMgr.GetCars();

        //    var bookings = dbMgr.GetBookings();

        //    var groupedBookings = bookings.GroupBy(booking => booking.CarId);

        //    bookings.s
        //}

        public static double CalculatePriceAfterDiscount(int customerId, double price)
        {
            double updatedPrice = price;
            var dbMgr = new DatabaseManager();

            var customerType = dbMgr.GetCustomerTypeFromCustomer(customerId);
            if(customerType != -1)
            {
                var customerTypeName = dbMgr.GetCustomerTypeName(customerType);
                if(customerTypeName == "VIP" || customerTypeName == "Planner")
                {
                    int reservationCount = dbMgr.GetCustomerBookingsCountWithinYear(customerId);
                    if(customerTypeName == "VIP")
                    {
                        if(reservationCount >= 2 && reservationCount <= 6)
                        {
                            updatedPrice = price - (0.05 * price);
                        }
                        else if (reservationCount >= 7 && reservationCount <= 14)
                        {
                            updatedPrice = price - (0.75 * price);
                        }
                        else if (reservationCount >= 15)
                        {
                            updatedPrice = price - (0.1 * price);
                        }
                    }
                    else
                    {
                        if (reservationCount >= 5 && reservationCount <= 9)
                        {
                            updatedPrice = price - (0.05 * price);
                        }
                        else if (reservationCount >= 10 && reservationCount <= 14)
                        {
                            updatedPrice = price - (0.1 * price);
                        }
                        else if (reservationCount >= 15 && reservationCount <= 19)
                        {
                            updatedPrice = price - (0.125 * price);
                        }
                        else if (reservationCount >= 20 && reservationCount <= 24)
                        {
                            updatedPrice = price - (0.15 * price);
                        }
                        else if (reservationCount >= 25)
                        {
                            updatedPrice = price - (0.25 * price);
                        }
                    }
                }
            }

            return updatedPrice;
        }
    }
}
