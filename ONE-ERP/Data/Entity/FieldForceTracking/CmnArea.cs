using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.FieldForceTracking
{
    public class CmnArea
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AreaID { get; set; }
        [MaxLength(50)]
        public string ZoneCode { get; set; }
        [MaxLength(50)]
        public string DepotCode { get; set; }
        [MaxLength(50)]
        public string RegionCode { get; set; }
        [MaxLength(50)]
        public string AreaCode { get; set; }
        [MaxLength(50)]
        public string AreaName { get; set; }
        public int? CompanyId { get; set; }
        public bool? IsActive { get; set; }
        public int? sortOrder { get; set; }
        [MaxLength(50)]
        public string SquareRefCode { get; set; }
        public string mobileNo { get; set; }
        public DateTime? createdAt { get; set; }
        public DateTime? updatedAt { get; set; }
        [MaxLength(250)]
        public string createdBy { get; set; }
        [MaxLength(250)]
        public string updatedBy { get; set; }
    }
}
