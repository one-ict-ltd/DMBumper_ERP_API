using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.FieldForceTracking
{
    public class Chem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ChemID { get; set; }
        [MaxLength(255)]
        public string GROUP_CODE { get; set; }
        [MaxLength(255)]
        public string GROUP_NAME { get; set; }
        [MaxLength(255)]
        public string DEPOT_CODE { get; set; }
        [MaxLength(255)]
        public string DEPOT_NAME { get; set; }
        [MaxLength(255)]
        public string ZONE_CODE { get; set; }
        [MaxLength(255)]
        public string ZONE_NAME { get; set; }
        [MaxLength(255)]
        public string DSM_CODE { get; set; }
        [MaxLength(255)]
        public string DSM_NAME { get; set; }
        [MaxLength(255)]
        public string REGION_CODE { get; set; }
        [MaxLength(255)]
        public string REGION_NAME { get; set; }
        [MaxLength(255)]
        public string RSM_CODE { get; set; }
        [MaxLength(255)]
        public string RSM_NAME { get; set; }
        [MaxLength(255)]
        public string AREA_CODE { get; set; }
        [MaxLength(255)]
        public string AREA_NAME { get; set; }
        [MaxLength(255)]
        public string AM_CODE { get; set; }
        [MaxLength(255)]
        public string AM_NAME { get; set; }
        [MaxLength(255)]
        public string TERRITORY_CODE { get; set; }
        [MaxLength(255)]
        public string TERRITORY_NAME { get; set; }
        [MaxLength(255)]
        public string MIO_CODE { get; set; }
        [MaxLength(255)]
        public string MIO_NAME { get; set; }
        [MaxLength(255)]
        public string CUSTOMER_CODE { get; set; }
        [MaxLength(255)]
        public string CUSTOMER_NAME { get; set; }
        [MaxLength(255)]
        public string CUSTOMER_ADDRESS { get; set; }
        [MaxLength(255)]
        public string CUSTOMER_STATUS { get; set; }
        [MaxLength(255)]
        public string CUSTOMER_TYPE_CODE { get; set; }
        [MaxLength(255)]
        public string CUSTOMER_TYPE_NAME { get; set; }
        [MaxLength(255)]
        public string CUST_VERSION { get; set; }
    }
}
