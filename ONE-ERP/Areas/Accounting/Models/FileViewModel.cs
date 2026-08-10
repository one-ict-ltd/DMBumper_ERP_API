using Microsoft.AspNetCore.Http;

namespace ONEERP.Areas.Accounting.Models
{
    public class FileViewModel
    {
        public string fileString { get; set; }
        public string fileName { get; set; }
        public string contentType { get; set; }
    }
}
