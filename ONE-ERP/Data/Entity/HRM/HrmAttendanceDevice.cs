using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ONEERP.Data.Entity.HRM
{
    public class HrmAttendanceDevice : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int deviceId { get; set; }       
        [MaxLength(300)]
        public string MachineAlias { get; set; }
        public int machineNumber { get; set; }
        [MaxLength(50)]
        public string iP { get; set; }
        public int? port { get; set; }
        public int? SerialPort { get; set; }
        public int ConnectType { get; set; }
        [MaxLength(100)]
        public string macAddress { get; set; }
    }
}
