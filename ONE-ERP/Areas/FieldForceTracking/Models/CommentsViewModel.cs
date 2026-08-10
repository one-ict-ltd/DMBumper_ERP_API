
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.FieldForceTracking.Models
{
    [NotMapped]
    public class CommentsViewModel
    {
        public int? commentsId { get; set; }
        public int? actionTypeId { get; set; }
        public string title { get; set; }
        public string comment { get; set; }
        public int? moduleId { get; set; }
        public int? leadsId { get; set; }
        public string leadName { get; set; }

     
    }
}
