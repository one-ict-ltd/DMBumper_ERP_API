using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.FieldForceTracking.Models
{
    public class TaUploadViewModel
    {
        public DateTime? taDate { get; set; }
        public decimal? taAmount { get; set; }
        public List<IFormFile> ImageUrls { get; set; }
    }

    public class TAAmountUploadViewModel
    {
        public int? empId { get; set; }
        public int? status { get; set; }
        public DateTime? taDate { get; set; }
        public decimal? taAmount { get; set; }
        public List<IFormFile> ImageUrls { get; set; }
    }
}
