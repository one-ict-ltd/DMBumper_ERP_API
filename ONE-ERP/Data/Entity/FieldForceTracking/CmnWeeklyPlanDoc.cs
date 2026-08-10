using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.FieldForceTracking
{
    public class CmnWeeklyPlanDoc
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int WeeklyPlanID { get; set; }
		[MaxLength(50)]
		public string EmpCode { get; set; }
		[MaxLength(50)]
		public string DoctorCode { get; set; }
		[MaxLength(50)]
		public string Day { get; set; }
		[MaxLength(50)]
		public string StartTime { get; set; }
		[MaxLength(50)]
		public string EndTime{ get; set; }
		
		public string Remarks { get; set; }
		public int IsActive { get; set; }

		public int? status { get; set; }
	}
}
