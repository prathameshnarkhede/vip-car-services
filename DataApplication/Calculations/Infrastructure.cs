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

        public static double CalculateCost(CustomerType customerType, int reservationsCount, double cost)
        {
            if (customerType.CustomerTypeName.ToLower() == "VIP".ToLower())
            {
                if (Enumerable.Range(2, 6).Contains(reservationsCount))
                {
                    return cost - (cost * 5 / 100);
                }
                if (Enumerable.Range(7, 14).Contains(reservationsCount))
                {
                    return cost - (cost * 7.5 / 100);
                }
                if (reservationsCount >= 15)
                {
                    return cost - (cost * 10 / 100);
                }
            }
            if (customerType.CustomerTypeName.ToLower() == "Planner".ToLower())
            {
                if (Enumerable.Range(5, 9).Contains(reservationsCount))
                {
                    return cost - (cost * 5 / 100);
                }
                if (Enumerable.Range(10, 14).Contains(reservationsCount))
                {
                    return cost - (cost * 10 / 100);
                }
                if (Enumerable.Range(15, 19).Contains(reservationsCount))
                {
                    return cost - (cost * 12.5 / 100);
                }
                if (Enumerable.Range(20, 24).Contains(reservationsCount))
                {
                    return cost - (cost * 15 / 100);
                }
                if (reservationsCount >= 25)
                {
                    return cost - (cost * 25 / 100);
                }
            }
            return cost;
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
