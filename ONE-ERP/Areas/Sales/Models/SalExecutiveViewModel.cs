using System;

namespace ONEERP.Areas.Sales.Models
{
    public class SalExecutiveTeamViewModel
    {
        public int ExecutiveTeamId { get; set; }
        public int TeamLeaderId { get; set; }
        public int TeamMemberId { get; set; }
    }
    
    public class SalExecutiveWiseProductViewModel
    {
        public int ExecutiveWiseProductId { get; set; }
        public int EmployeeId { get; set; }
        public int ProductId { get; set; }
        public DateTime? EffectiveFromDate { get; set; }
        public DateTime? EffectiveToDate { get; set; }
    }

}
