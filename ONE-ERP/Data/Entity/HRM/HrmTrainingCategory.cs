using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.HRM
{
    public class HrmTrainingCategory:NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int trainingCategoryId { get; set; }
        public string trainingCategoryName { get; set; }
        public string trainingCategoryNameBn { get; set; }
        public string trainingCategoryShortName { get; set; }
    }
}
