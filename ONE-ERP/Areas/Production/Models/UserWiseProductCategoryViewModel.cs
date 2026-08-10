using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Production.Models
{
    public class UserWiseProductCategoryViewModel
    {
        public int employeeId { get; set; }
        
        public List<SelectedItems> listViewModel { get; set; }
    }
    public class SelectedItems
    {
        public int userProductTypeId { get; set; }
        public int productTypeId { get; set; }
        public bool isSelect { get; set; }
    }
    
}
