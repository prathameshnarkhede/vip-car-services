using DataApplication.Database;
using DataApplication.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataApplication.Calculations
{
    public class Infrastructure
    {
        private const double _taxPercent = 6;
        private const double hourlyDiscountPercent = 65;

        public static double CalculateWithTax(double basePrice)
        {
            return basePrice + (basePrice * (_taxPercent / 100.0));
        }

        public static double CalculateCost(int customerId, Booking booking, Car car)
        {
            var updatedPrice = CalculatePackagedCost(booking, car);
            var dbMgr = new DatabaseManager();

            var customerType = dbMgr.GetCustomerTypeFromCustomer(customerId);
            if (customerType != -1)
            {
                var customerTypeName = dbMgr.GetCustomerTypeName(customerType);
                if (customerTypeName.ToLower() == "VIP".ToLower() || customerTypeName.ToLower() == "Planner".ToLower())
                {
                    int reservationCount = dbMgr.GetCustomerBookingsCountWithinYear(customerId);
                    if (customerTypeName.ToLower() == "VIP".ToLower())
                    {
                        if (Enumerable.Range(2, 6).Contains(reservationCount))
                        {
                            updatedPrice = updatedPrice - (5/ 100.0 * updatedPrice);
                        }
                        else if (Enumerable.Range(7, 14).Contains(reservationCount))
                        {
                            updatedPrice = updatedPrice - (7.5/ 100.0 * updatedPrice);
                        }
                        else if (reservationCount >= 15)
                        {
                            updatedPrice = updatedPrice - (10/ 100.0 * updatedPrice);
                        }
                    }
                    else
                    {
                        if (Enumerable.Range(5, 9).Contains(reservationCount))
                        {
                            updatedPrice = updatedPrice - (5/ 100.0 * updatedPrice);
                        }
                        else if (Enumerable.Range(10, 14).Contains(reservationCount))
                        {
                            updatedPrice = updatedPrice - (10/ 100.0 * updatedPrice);
                        }
                        else if (Enumerable.Range(15, 19).Contains(reservationCount))
                        {
                            updatedPrice = updatedPrice - (12.5/ 100.0 * updatedPrice);
                        }
                        else if (Enumerable.Range(20, 24).Contains(reservationCount))
                        {
                            updatedPrice = updatedPrice - (15/ 100.0 * updatedPrice);
                        }
                        else if (reservationCount >= 25)
                        {
                            updatedPrice = updatedPrice - (25/ 100.0 * updatedPrice);
                        }
                    }
                }
            }

            return updatedPrice;
        }

        private static double CalculatePackagedCost(Booking booking, Car car)
        {
            if(booking.Type.ToLower() == "Hourly".ToLower())
            {
                return CalculateHourlyCost(booking.Time, booking.Hours, booking.CarId);
            }
            if (booking.Type.ToLower() == "Nightlife".ToLower())
            {
                return car.NightlifeRate;
            }
            if (booking.Type.ToLower() == "Wedding".ToLower())
            {
                return car.WeddingRate;
            }
            if (booking.Type.ToLower() == "Wellness".ToLower())
            {
                return car.WellnessRate;
            }
            return 0;
        }

        private static double CalculateHourlyCost(DateTime startTime, int hours, int carId)
        {
            var price = 0.0;
            var dbMgr = new DatabaseManager();
            var car = dbMgr.GetCar(carId);
            var hourlyCostOfCar = car.HourRate;
            var endTime = startTime.AddHours(hours);

            while (DateTime.Compare(startTime, endTime) != 0)
            {
                if (Enumerable.Range(22, 23).Contains(startTime.Hour) || Enumerable.Range(0, 6).Contains(startTime.Hour) || (startTime.Hour is 7 && startTime.Minute is 0))
                {
                    price = price + (140 / 100.0 * hourlyCostOfCar);
                }
                else
                {
                    if (price is 0.0)
                        price = price + hourlyCostOfCar;
                    else
                        price = price + (65 / 100.0 * hourlyCostOfCar);
                }

                startTime = startTime.AddHours(1);
            }

            return price;
        }

        public static bool CheckCarAvailability(int carId, DateTime startTime)
        {
            var isAvailable = false;

            var dbMgr = new DatabaseManager();

            var carBookings = dbMgr.GetDescOrderedBookings(carId);
            if (carBookings == null || carBookings.ToList().Count == 0)
                isAvailable = true;
            else
            {
                var topCarBooking = carBookings.ToList().First();
                var bookingStartTime = topCarBooking.Time;
                var bookingHours = topCarBooking.Hours;
                var bookingEndTime = bookingStartTime.AddHours(bookingHours + 6);
                int res = DateTime.Compare(startTime, bookingEndTime);
                if (res >= 0)
                    isAvailable = true;
                else
                    isAvailable = false;
            }

            return isAvailable;
        }
    }
}
