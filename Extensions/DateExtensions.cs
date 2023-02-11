using FinanceMyLife.Data.Enums;

namespace FinanceMyLife.Extensions;

public static class DateExtensions
{

    public static string ToShortDateWithValidation(this DateTime date)
    {
        return date.IsNotNullOrEmpty() ? date.ToShortDateString() : "";
    }
    public static bool IsNotNullOrEmpty(this DateTime? date)
    {
        return (date.HasValue && date != new DateTime().Date);
    }
    public static bool IsNotNullOrEmpty(this DateTime date)
    {
        return (date.Date != new System.DateTime().Date);
    }

    public static DateTime CalculateEndDate(DateTime startDate, PeriodsTypesEnum periodType, int numberOfTimes)
    {
        switch (periodType)
        {
            case PeriodsTypesEnum.Daily:
                return startDate.AddDays(numberOfTimes - 1);
            case PeriodsTypesEnum.Weekly:
                return startDate.AddDays((numberOfTimes - 1) * 7);
            case PeriodsTypesEnum.Monthly:
                return startDate.AddMonths(numberOfTimes - 1);
            case PeriodsTypesEnum.Anually:
                return startDate.AddYears(numberOfTimes - 1);
            default:
                throw new ArgumentException("Invalid period type: " + periodType);
        }
    }
}
