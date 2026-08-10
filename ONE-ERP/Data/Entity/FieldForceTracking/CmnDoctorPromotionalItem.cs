using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ONEERP.Data.Entity.FieldForceTracking
{
    public class CmnDoctorPromotionalItem : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int doctorPromotionalItemId { get; set; }
        public int? doctorScheduleId { get; set; }
        public CmnDoctorSchedules doctorSchedule { get; set; }
        public int? productId { get; set; }
        public int? productWiseSpecificationId { get; set; }
        public decimal? invoiceQty { get; set; }

    }
}
