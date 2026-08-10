using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ONEERP.Data.Entity
{
    public class NewBase
    {
        public bool? isActive { get; set; }
        [DefaultValue(0)]
        public bool? isDelete { get; set; }
        public DateTime? createdAt { get; set; }
        public DateTime? updatedAt { get; set; }
        [MaxLength(250)]
        public string createdBy { get; set; }
        [MaxLength(250)]
        public string updatedBy { get; set; }
    }
}
