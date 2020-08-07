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

        public static List<Car> GetAvailableCars()
        {
            var dbMgr = new DatabaseManager();
            List<Car> carList = dbMgr.GetUniqueBookingsForCars();
            return carList;
        }

        public static double CalculateHourlyCost(DateTime startTime, int hours, int carId)
        {
            double price = 0.0;
            var dbMgr = new DatabaseManager();
            Car car = dbMgr.GetCarUsingCarId(carId);
            var hourlyCostOfCar = car.HourRate;
            var endTime = startTime.AddHours(hours);
            
            while(DateTime.Compare(startTime,endTime) != 0)
            {
                if(startTime.Hour == 22 || startTime.Hour == 23 || startTime.Hour == 0 || startTime.Hour == 1 || 
                    startTime.Hour == 2 || startTime.Hour == 3 || startTime.Hour == 4 || startTime.Hour == 5 || 
                    startTime.Hour == 6 || (startTime.Hour == 7 && startTime.Minute == 0))
                {
                    price = price + (140 / 100 * hourlyCostOfCar);
                }
                else
                {
                    if (price == 0.0)
                        price = price + hourlyCostOfCar;
                    else
                        price = price + (65 / 100 * hourlyCostOfCar);
                }

                startTime.AddHours(1);
            }

            return price;
        }

    }
}
