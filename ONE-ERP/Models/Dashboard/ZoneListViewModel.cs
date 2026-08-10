using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Models.Dashboard
{
    public class ZoneListViewModel
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }        
        public bool? IsActive { get; set; } 
    }
}
