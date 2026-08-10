namespace ONEERP.Areas.Accounting.Models
{
    public class BudgetMainHeadViewModel
    {
        public int? budgetMainHeadId { get; set; }
        public int? companyId { get; set; }
        public int? sbuId { get; set; }        
        public string mainHeadCode { get; set; }
        public string mainHeadName { get; set; }
        public int? sortOrder { get; set; }       
        public bool? isActive { get; set; }     
    }
}
