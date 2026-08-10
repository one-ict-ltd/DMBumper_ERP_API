using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Purchase.Models
{
    public class ShipmentViewModel
    {
        public int ImpShipmentInformationId { get; set; }
        public int ImpLCInfoMasterId { get; set; }
        public string invoiceNo { get; set; }
        public string shipmentNo { get; set; }
        public string carrierBillNo { get; set; }
        public string carrierName { get; set; }
        public string cagesDrumsItems { get; set; }
        public string transShipment { get; set; }
        public decimal? invoiceAmt { get; set; }
        public int? remainderDays { get; set; }
        public int? actualLoadingPortId { get; set; }
        public int? actualDestinationPortId { get; set; }
        public DateTime? shipmentDate { get; set; }
        public DateTime? invoiceDate { get; set; }
        public DateTime? expectedDurgCLrDate { get; set; }
        public DateTime? carrierBillDate { get; set; }
    }
}
