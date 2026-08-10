using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ONEERP.Areas.Attendance.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Helpers
{
    public static class CalenderDay
    {
        public static List<CalenderViewModel> GetDates(int year, int month)
        {
            List<CalenderViewModel> calenderViewModels = new List<CalenderViewModel>();
            var data = Enumerable.Range(1, DateTime.DaysInMonth(year, month))  // Days: 1, 2 ... 31 etc.
                             .Select(day => new DateTime(year, month, day)) // Map each day to a date
                             .ToList(); // Load dates into a list
            foreach (var d in data)
            {
                calenderViewModels.Add(new CalenderViewModel
                {
                    Day = d.Day,
                    Date = d.Date,
                    DayName = d.DayOfWeek.ToString(),
                    isActive = false,
                });
            }
            return calenderViewModels;
        }
    }
}
