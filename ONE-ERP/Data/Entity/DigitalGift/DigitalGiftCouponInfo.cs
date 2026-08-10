using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ONEERP.Data.Entity.DigitalGift
{
    public class DigitalGiftCouponInfo : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string CouponCode { get; set; }
        public string Name { get; set; }
        public string MobileNumber { get; set; }
        public string GiftName { get; set; }
        public DateTime? DisburseDate { get; set; }
        public string GP_TrxID { get; set; }
        public string GpGiftPackState { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public string TerritoryCode { get; set; }
        public string Remarks { get; set; }
    }
    public class DigitalGiftDisburseLog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int logId { get; set; }
        public DateTime? createdAt { get; set; }
        public string mobileNo { get; set; }
        public string responseLog { get; set; }
    }
}