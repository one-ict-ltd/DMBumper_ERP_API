using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.MasterData.Models
{
    public class HelpImageViewModel
    {
        

        public int? helpImageId { get; set; }
        public int? helpId { get; set; }
        public string imageUrl { get; set; }
       
        public int? isActive { get; set; }
        public int? isDelete { get; set; }

    }
}
