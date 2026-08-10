using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.Purchase
{
    public class PurImpShipmentInformation : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ImpShipmentInformationId { get; set; }
        public int? ImpLCInfoMasterId { get; set; }
        public PurImpLCInfoMaster ImpLCInfoMaster { get; set; }

        public string ShipmentNo { get; set; }
        public string TransShipment { get; set; }
        public DateTime? ValidityShiptDate { get; set; }
        public DateTime? ShipmentDate { get; set; }
        public string InvoiceNo { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public decimal? InvoiceAmt { get; set; }
        public string CarrierBillNo { get; set; }
        public DateTime? CarrierBillDate { get; set; }
        public string NoCagesDrumsItems { get; set; }
        public DateTime? ExpectedDrugClrDate { get; set; }
        public int? ReminderDays { get; set; }
        public string CarrierName { get; set; }
        public int? actualLoadingPortInfoId { get; set; }
        public PurImpPortInfo actualLoadingPortInfo { get; set; }
        public int? actualDestinationPortInfoId { get; set; }
        public PurImpPortInfo actualDestinationPortInfo { get; set; }
    }
}
