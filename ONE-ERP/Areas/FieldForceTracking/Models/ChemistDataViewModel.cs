using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.FieldForceTracking.Models
{
    public class ChemistDataViewModel
    {
        public int ChemistID { get; set; }
        public string ChemistNo { get; set; }
        public string ChemistName { get; set; }
        public int TerritoryID { get; set; }
    }
}
