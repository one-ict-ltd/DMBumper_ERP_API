using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.HRM
{
    public class HrmEmployeeAttachment : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int employeeAttachmentId { get; set; }
        public int? employeeId { get; set; }
        public HrmEmployee employee { get; set; }
        public string imageUrl { get; set; }
        public string signatureUrl { get; set; }
        public string fingerUrl { get; set; }
    }
}
