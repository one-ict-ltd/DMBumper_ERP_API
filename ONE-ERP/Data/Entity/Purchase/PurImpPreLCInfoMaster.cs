using ONEERP.Data.Entity.Accounting;
using ONEERP.Data.Entity.Common;
using ONEERP.Data.Entity.Inventory;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.Purchase
{
    public class PurImpPreLCInfoMaster:NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ImpPreLCInfoMasterId { get; set; }
        public string lcPaymentType { get; set; }
        public int? UnitId { get; set; }
        public CmnSpecialBranchUnit Unit { get; set; }
        public string refNo { get; set; }
        public int? currencyId { get; set; }
        public AccCurrency currency { get; set; }
        public decimal? lcAmount { get; set; }
        public int? ImpModeOfTransportId { get; set; }
        public PurImpModeOfTransport ImpModeOfTransport { get; set; }
        public decimal? conversionRate { get; set; }
        public int? ImpLocalAgentId { get; set; }
        public PurImpLocalAgent ImpLocalAgent { get; set; }
        public int? ImpBenificiaryId { get; set; }
        public AccParty ImpBenificiary { get; set; }
        public string IndentNo { get; set; }
        public DateTime? IndentDate { get; set; }
        public DateTime? IndentReceiveDate { get; set; }
        public string proFormaInvoiceNo { get; set; }
        public DateTime? proFormaInvoiceDate { get; set; }
        public int? productTypeId { get; set; }
        public InvProductType productType { get; set; }
        public int? ManufacturerId { get; set; }
        public AccParty Manufacturer { get; set; }
        public string IRMReqNo { get; set; }
        public DateTime? IRMReqDate { get; set; }
        public string RFINo { get; set; }
        public string PartShipment { get; set; } 
        public string TransShipment { get; set; } 
        public string DockShiptStorage { get; set; }
        public string PSIStatus { get; set; }
        public string LCStatus { get; set; }
        public string PSINo { get; set; }
        public string PSICompany { get; set; }

        public DateTime? shortedDate { get; set; }
        public DateTime? mailReciveDate { get; set; }
        public DateTime? signedDate { get; set; }
        public DateTime? typedDate { get; set; }
        public DateTime? faxedOnDate { get; set; }
        public DateTime? appliedDate { get; set; }
        public DateTime? bankSubmissionDate { get; set; }
        public DateTime? amndCopyDate { get; set; }
        public string Remarks { get; set; }
        public int? csMasterId { get; set; }
        public PurCSMaster csMaster { get; set; }
    }
}
