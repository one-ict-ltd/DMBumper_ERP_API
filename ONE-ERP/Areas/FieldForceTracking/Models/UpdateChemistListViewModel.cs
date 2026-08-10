using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.FieldForceTracking.Models
{
    public class UpdateChemistListViewModel
    {
        public int ChemistID { get; set; }
        public string ConversionCode { get; set; }
        public int ChemistCodeApprovalId { get; set; }
        public List<UpdateChemistListViewModel> lstDetailsViewModel { get; set; }
       
    }


}
