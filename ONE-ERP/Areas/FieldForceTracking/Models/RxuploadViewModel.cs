using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.FieldForceTracking.Models
{
    public class RxuploadViewModel
    {
        public int rxUploadMasterID { get; set; }

        public string userID { get; set; }

        public int? doctorId { get; set; }
        public List<IFormFile> ImageUrls { get; set; }

        public List<int?> InvProductWiseSpecificationIds { get; set; }
    }

    public class NoticeUploadViewModel
    {
        public int UploadMasterID { get; set; }

        public int? status { get; set; }

        public DateTime?  startDate { get; set; }
        public DateTime?  endDate { get; set; }

        public List<IFormFile> ImageUrls { get; set; }
    }
}
