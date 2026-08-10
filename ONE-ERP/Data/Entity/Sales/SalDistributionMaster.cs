using ONEERP.Data.Entity.Accounting;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ONEERP.Data.Entity.Sales
{
    public class SalDistributionMaster:NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int distributionMasterId { get; set; }       
        public int? partyId { get; set; }
        public AccParty party { get; set; }
        [MaxLength(20)]
        public string distributionNumber { get; set; }
        public DateTime? distributionDate { get; set; }
        [MaxLength(250)]
        public string deliveryManName { get; set; }
        [MaxLength(30)]
        public string deliveryManMobile { get; set; }
        [MaxLength(20)]
        public string vehicleNo { get; set; }
        [MaxLength(250)]
        public string driverName { get; set; }
        [MaxLength(30)]
        public string driverMobile { get; set; }
        [MaxLength(256)]
        public string deliveryAddress { get; set; }
        [MaxLength(256)]
        public string remarks { get; set; }
        public string approvalStatus{ get; set; }
    }
}
