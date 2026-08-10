using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.MasterData.Models
{
    public class ApproverTypeViewModel
    {
        public int approverTypeId { get; set; }
        public int? approvalTypeId { get; set; }
        public string approverTypeName { get; set; }
        public string approverEmpId { get; set; }
        public int? employeeId { get; set; }
        public bool? isActive { get; set; }
    }
}
