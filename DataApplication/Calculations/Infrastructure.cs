namespace DataApplication.Calculations
{
    public class Infrastructure
    {
        private const double _taxPercent = 6;
        private const double hourlyDiscountPercent = 65;

        public static double CalculateTotalPrice(double basePrice)
        {
            return basePrice + (basePrice * (_taxPercent / 100));
        }

        public static double CalculateTotalFromHourlyRate(double hourlyRate, int hours)
        {
            return 0;
        }
    }
}
