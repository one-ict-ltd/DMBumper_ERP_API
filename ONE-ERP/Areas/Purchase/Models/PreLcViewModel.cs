using ONEERP.Areas.Purchase.Models;
using ONEERP.Data.Entity.Accounting;
using ONEERP.Data.Entity.Common;
using ONEERP.Data.Entity.Inventory;
using ONEERP.Data.Entity.Purchase;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Purchase
{
    public class PreLcViewModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ImpPreLCInfoMasterId { get; set; }
        public string lcPaymentType { get; set; }
        public int? UnitId { get; set; }
       
        public string refNo { get; set; }
        public int? currencyId { get; set; }
        
        public decimal? lcAmount { get; set; }
        public int? ImpModeOfTransportId { get; set; }

        public decimal? conversionRate { get; set; }
        public int? ImpLocalAgentId { get; set; }

        public int? ImpBenificiaryId { get; set; }

        public string indentNo { get; set; }
        public DateTime? indentDate { get; set; }
        public DateTime? indentRecvDate { get; set; }
        public string proformaInvoiceNo { get; set; }
        public DateTime? proformaInvoiceDate { get; set; }
        public int? productTypeId { get; set; }
        public int? manufacturerId { get; set; }

        public string requisitionNo { get; set; }
        public DateTime? requisitionDate { get; set; }
        public string rfiNo { get; set; }
        public string partShipment { get; set; } //1=ALLOWED 0=NOT ALLOWED
        public string transShipment { get; set; } //1=ALLOWED 0=NOT ALLOWED
        public string dockShipt { get; set; } //1=ALLOWED 0=NOT ALLOWED
        public string psiStatus { get; set; } //1=Yes 0=NO
        public string psiNo { get; set; }
        public string psiCompany { get; set; }
        public DateTime? shortedDate { get; set; }
        public DateTime? mailReciveDate { get; set; }
        public DateTime? signedDate { get; set; }
        public DateTime? typedDate { get; set; }
        public DateTime? faxedOnDate { get; set; }
        public DateTime? appliedDate { get; set; }
        public DateTime? bankSubmissionDate { get; set; }
        public DateTime? amndCopyDate { get; set; }
        public string Remarks { get; set; }

        public List<PreLcDetailsViewModel> lstReqDetailsViewModel;

        public int? csMasterId { get; set; }
        public List<lcInfo> lcInfoData;
    }
    public class lcInfo
    {
        public int? ImpLCInfoMasterId { get; set; }
        public int? ImpPreLCInfoMasterId { get; set; }

        public DateTime? lcOpenDate { get; set; }

        public DateTime? validityDate { get; set; }
        public DateTime? exshiptDate { get; set; }
        public DateTime? expireDate { get; set; }

        public string lcNo { get; set; }

        public int? bankId { get; set; }
        public int? adviceBankId { get; set; }
        public int? loadingPortId { get; set; }
        public int? destinatinPortId { get; set; }
        public decimal? totalLcAmount { get; set; }
        public decimal? frightAmount { get; set; }
        public int? countryOriginId { get; set; }


    }
}
