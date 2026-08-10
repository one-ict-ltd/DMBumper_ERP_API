using ONEERP.Data.Entity.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.Accounting
{
    public class AccVisaWorkOrder:NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int? visaWorkOrderId { get; set; }
        [MaxLength(250)]
        public string workOrderNo { get; set; }
        public int? countryId { get; set; }

        [MaxLength(250)]
        public string countryName { get; set; }
        public int? cityId { get; set; }
        [MaxLength(250)]
        public string cityName { get; set; }
        public int? companyId { get; set; }
        [MaxLength(250)]
        public string companyName { get; set; }
        public DateTime? issueDate { get; set; }
        public DateTime? expireDate { get; set; }
        public int? visaGroupQuantity { get; set; }
        public int? visaQuantity { get; set; }
        public int? visaAssigned { get; set; }
        public int? visaUnassigned { get; set; }        
        public bool? isProcessed { get; set; }
    }
}
