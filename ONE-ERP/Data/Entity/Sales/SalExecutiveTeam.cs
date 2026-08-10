using ONEERP.Data.Entity.HRM;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ONEERP.Data.Entity.Sales
{
    public class SalExecutiveTeam: NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int executiveTeamId { get; set; }
        public int? teamLeaderId { get; set; }
        public HrmEmployee teamLeader { get; set; }
        public int? teamMemberId { get; set; }
        public HrmEmployee teamMember { get; set; }
    }
}
