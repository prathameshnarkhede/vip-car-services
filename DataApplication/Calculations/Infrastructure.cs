using DataApplication.Database;
using DataApplication.Model;
using System;
using System.Linq;

namespace DataApplication.Calculations
{
    public class Infrastructure
    {
        private const double _taxPercent = 6;
        private const double hourlyDiscountPercent = 65;

        public static double CalculateWithTax(double basePrice)
        {
            return basePrice + (basePrice * (_taxPercent / 100));
        }

        public static double CalculateTotalFromHourlyRate(double hourlyRate, DateTime startTime, int hours)
        {
            var endTime = startTime.AddHours(hours);

            var nightStart = new DateTime(startTime.Year, startTime.Month, startTime.Day, 22, 0, 0);// 00:00;         
            var nightEnd = new DateTime(startTime.Year, startTime.Month, startTime.Day, 6, 0, 0); // 6:00;

            var start = new TimeSpan(22, 0, 0);
            var end = new TimeSpan(07, 0, 0);

            //if (start < end)
            //    return start <= now && now <= end;
            //// start is after end, so do the inverse comparison
            //return !(end < now && now < start);

            return hourlyRate + ((hours - 1) * hourlyRate * (hourlyDiscountPercent / 100));
        }

        public static double CalculateCost(int customerId, double price)
        {
            double updatedPrice = price;
            var dbMgr = new DatabaseManager();

            var customerType = dbMgr.GetCustomerTypeFromCustomer(customerId);
            if (customerType != -1)
            {
                var customerTypeName = dbMgr.GetCustomerTypeName(customerType);
                if (customerTypeName == "VIP" || customerTypeName == "Planner")
                {
                    int reservationCount = dbMgr.GetCustomerBookingsCountWithinYear(customerId);
                    if (customerTypeName == "VIP")
                    {
                        if (reservationCount >= 2 && reservationCount <= 6)
                        {
                            updatedPrice = price - (5/100 * price);
                        }
                        else if (reservationCount >= 7 && reservationCount <= 14)
                        {
                            updatedPrice = price - (7.5/100 * price);
                        }
                        else if (reservationCount >= 15)
                        {
                            updatedPrice = price - (10/100 * price);
                        }
                    }
                    else
                    {
                        if (reservationCount >= 5 && reservationCount <= 9)
                        {
                            updatedPrice = price - (5/100 * price);
                        }
                        else if (reservationCount >= 10 && reservationCount <= 14)
                        {
                            updatedPrice = price - (10/100 * price);
                        }
                        else if (reservationCount >= 15 && reservationCount <= 19)
                        {
                            updatedPrice = price - (12.5/100 * price);
                        }
                        else if (reservationCount >= 20 && reservationCount <= 24)
                        {
                            updatedPrice = price - (15/100 * price);
                        }
                        else if (reservationCount >= 25)
                        {
                            updatedPrice = price - (25/100 * price);
                        }
                    }
                }
            }

            return updatedPrice;
        }

        public static double CalculatePackagedCost(Booking booking, Car car)
        {
            if(booking.Type.ToLower() == "Hourly".ToLower())
            {
                return CalculateTotalFromHourlyRate(car.HourRate, booking.Time, booking.Hours);
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
    }
}
