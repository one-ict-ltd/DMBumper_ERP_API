using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.HRM
{
    public class HrmTrainingResourcePerson:NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int trainingResourcePersonId { get; set; }
        public int? trainingInfoNewId { get; set; }
        public int? resourcePersonId { get; set; }
        public string comments { get; set; }
        public string remarks { get; set; }
    }
}
