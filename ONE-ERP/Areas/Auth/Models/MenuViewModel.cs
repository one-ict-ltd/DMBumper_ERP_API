namespace ONEERP.Areas.Auth.Models
{
    public class MenuViewModel
    {
        public int? menuId { get; set; }
        public string menuName { get; set; }
        public string menuShortName { get; set; }
        public int? menuTypeId { get; set; }
        public int? moduleId { get; set; }
        public string menuPath { get; set; }
        public string reportName { get; set; }
        public string reportPath { get; set; }
        public bool? isParent { get; set; }
        public int? parentId { get; set; }       
        public int? sequence { get; set; }
        public string menuIcon { get; set; }
        public bool? isActive { get; set; }     
    }
}
