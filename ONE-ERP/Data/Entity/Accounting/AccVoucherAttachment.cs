using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ONEERP.Data.Entity.Accounting
{
    public class AccVoucherAttachment : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int voucherAttachmentId { get; set; }
        public string attachmentUrl { get; set; }
        public string fileName { get; set; }
        public string remarks { get; set; }
        public int? voucherMasterId { get; set; }
        public AccVoucherMasters voucherMasters { get; set; }
    }
}