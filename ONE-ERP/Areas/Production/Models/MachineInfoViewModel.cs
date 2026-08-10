using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Production.Models
{
    public class MachineInfoViewModel
    {
        public int machineInfoId { get; set; }
        public string machineName { get; set; }
        public string originCountry { get; set; }
        public DateTime purchaseDate { get; set; }
        public DateTime startDate { get; set; }
        public int? purchaseAmount { get; set; }
        public int? status { get; set; }
        public string machineCode { get; set; }
        public string remarks { get; set; }     
    }
}
