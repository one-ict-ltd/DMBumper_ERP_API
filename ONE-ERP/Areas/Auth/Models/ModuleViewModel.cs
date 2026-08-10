namespace ONEERP.Areas.Auth.Models
{
    public class ModuleViewModel
    {
        public int? moduleId { get; set; }
        public string moduleName { get; set; }
        public string description { get; set; }
        public string imageURL { get; set; }
        public string modulePath { get; set; }
        public int? sequence { get; set; }
        public bool? isActive { get; set; }
    }
}
