using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.FieldForceTracking.Models
{
    public class WeekenDayEntryViewModel
    {
        public string EMP_ID { get; set; }
        public DateTime Date { get; set; }
        public int month { get; set; }
        public int year { get; set; }
        public int isHoliDay { get; set; }
    }
}
