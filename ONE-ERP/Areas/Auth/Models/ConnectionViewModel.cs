using System;
using System.ComponentModel.DataAnnotations;

namespace ONEERP.Areas.Auth.Models
{
    public class ConnectionViewModel
    {
      
      


        public DateTime Date { get; set; }
        public string Time { get; set; }
        public int IsLocation { get; set; }
        public int IsDataConnected { get; set; }
    }
}
