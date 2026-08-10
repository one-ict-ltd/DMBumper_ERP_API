using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Models.Dashboard
{
    public class DepoListViewModel
    {
        public int Id { get; set; }        
        public string Code { get; set; }
        public string Name { get; set; }
        public string ZoneCode { get; set; }
        public bool IsActive { get; set; }
        public string ZoneName { get; set; }
    }
}
