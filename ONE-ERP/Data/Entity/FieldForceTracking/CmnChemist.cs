using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.FieldForceTracking
{
    public class CmnChemist
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ChemistID { get; set; }
        public string ChemistNo { get; set; }
        public string ChemistName { get; set; }
        public string ChemistType { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string MobileNo { get; set; }
        public string TelephoneNo { get; set; }
        public string CreditLimit { get; set; }
        public int? credit_days { get; set; }
        public string OwnerName { get; set; }
        public string DrugLicense { get; set; }
        public string market_name { get; set; }
        public int CompanyID { get; set; }
        public int? IsActive { get; set; }
        public string Address { get; set; }
        public int? IsScheduled { get; set; }
        public string Propritor { get; set; }
        [MaxLength(50)]
        public string MarketID { get; set; }
        [MaxLength(50)]
        public string TerritoryID { get; set; }
        [MaxLength(50)]
        public string AreaId { get; set; }
        [MaxLength(50)]
        public string RegionId { get; set; }
        [MaxLength(50)]
        public string DepoId { get; set; }
        [MaxLength(50)]
        public string ZoneId { get; set; }

        public int? CreateBy { get; set; }
        public DateTime? CreateOn { get; set; }
        public string CreatePc { get; set; }
        public int? UpdateBy { get; set; }
        public DateTime? UpdateOn { get; set; }
        public string UpdatePc { get; set; }
        public int? IsDeleted { get; set; }
        public int? DeleteBy { get; set; }
        public DateTime? DeleteOn { get; set; }
        public string DeletePc { get; set; }
    }
}
