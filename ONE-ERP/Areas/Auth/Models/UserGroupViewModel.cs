namespace ONEERP.Areas.Auth.Models
{
    public class UserGroupViewModel
    {
        public int? userGroupId { get; set; }
        public string groupName { get; set; }
        public string shortName { get; set; }       
        public bool? isActive { get; set; }
    }
}
