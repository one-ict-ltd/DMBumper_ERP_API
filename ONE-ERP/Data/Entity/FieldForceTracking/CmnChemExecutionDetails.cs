using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;


namespace ONEERP.Data.Entity.FieldForceTracking
{
    public class CmnChemExecutionDetails : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ChemExecutionDetailsID { get; set; }
        public int ChemistScheduleID { get; set; }
        public string jointMemberType { get; set; }
    }
}
