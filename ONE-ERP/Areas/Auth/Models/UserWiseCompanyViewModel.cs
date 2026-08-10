namespace ONEERP.Areas.Auth.Models
{
    public class UserWiseCompanyViewModel
    {
        public int? userCompanyId { get; set; }
        public int? employeeId { get; set; }
        public int? companyId { get; set; }       
        public bool? isDefault { get; set; }
        public bool? isActive { get; set; }
    }
}
