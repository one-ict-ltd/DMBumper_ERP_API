using ONEERP.Data.Entity.HRM;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.Sales
{
    public class SalSalesDispatchMaster:NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int dispatchMasterId { get; set; }
        public string dispatchNo { get; set; }

        public DateTime? dispatchDate { get; set; }

        public int? dispatcherId { get; set; }
        public HrmEmployee dispatcher { get; set; }

        public int? status { get; set; }

    }
}
