using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.Production
{
    public class PrdMachineInfo:NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int machineInfoId { get; set; }
        public string machineName  { get; set; }
        public string machineCode  { get; set; }
        public string originCountry { get; set; }
        public DateTime? purchaseDate { get; set; }
        public DateTime? startDate { get; set; }
        public decimal? purchaseAmount { get; set; }
        public string remarks { get; set; }
        public int? status { get; set; }
        public int? companyId { get; set; }
    }
}
