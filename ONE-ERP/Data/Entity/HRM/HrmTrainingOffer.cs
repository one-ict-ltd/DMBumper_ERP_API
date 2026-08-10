using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.HRM
{
    public class HrmTrainingOffer:NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int trainingOfferId { get; set; }
        public string name { get; set; }
        public string content { get; set; }
        public string syllabus { get; set; }
        public string benifits { get; set; }
        public string duration { get; set; }
        public string units { get; set; }
        public int? moduleTrainingCategoryId { get; set; }
        public string type { get; set; }
    }
}
