using System;

namespace FI.UnitTests.Utilities
{
    public static class DateUtilities
    {
        public static DateTime ComputeYesterday()
        {
            return DateTime.UtcNow.AddDays(-1);
        }

        public static DateTime ComputeTomorrow()
        {
            return DateTime.UtcNow.AddDays(1);
        }
    }
}
