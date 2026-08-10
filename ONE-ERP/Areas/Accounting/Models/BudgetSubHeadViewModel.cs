namespace ONEERP.Areas.Accounting.Models
{
    public class BudgetSubHeadViewModel
    {
        public int? budgetSubHeadId { get; set; }
        public int? budgetMainHeadId { get; set; }       
        public string subHeadCode { get; set; }
        public string subHeadName { get; set; }
        public int? sortOrder { get; set; }       
        public bool? isActive { get; set; }     
    }
}
