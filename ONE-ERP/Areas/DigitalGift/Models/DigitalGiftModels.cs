using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ONEERP.Areas.DigitalGift.Models
{
    public class DigitalGiftModels
    {
        [Required]
        [StringLength(100, ErrorMessage = "The {0} at most {1} characters long.")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [StringLength(11, ErrorMessage = "The {0} at most {1} characters long.")]
        [Display(Name = "MobileNumber")]
        public string MobileNumber { get; set; }

        [Required]
        [StringLength(8, ErrorMessage = "The {0} at most {1} characters long.")]
        [Display(Name = "CouponCode")]
        public string CouponCode { get; set; }

        [Display(Name = "Question")]
        public string Question { get; set; }

        [Display(Name = "Answer")]
        public string Answer { get; set; }
        [Display(Name = "TerritoryCode")]
        public string TerritoryCode { get; set; }
    }

    public class OAuthResponse
    {
        public string status { get; set; }
        public string accessToken { get; set; }
        public string expiresIn { get; set; }
        public string scope { get; set; }
        public string tokenType { get; set; }
    }
    public class GP_OAuthBody
    {
        public string client_id { get; set; }
        public string client_secret { get; set; }
        public string grant_type { get; set; }
    }

    public class PackListModel
    {
        /*
            "success": true,
            "message": "Ok",
            "data": {
                "pack_list": [
                    {
                        "pack_id": 34,
                        "pack_name": "10GB Bulk Data For 30 Days",
                        "volume_mb": 10240,
                        "channel_id": 1,
                        "current_balance": 2,
                        "status": "active"
                    }
                ]
            }
         */
        public bool success { get; set; }
        public string message { get; set; }
        public Data data { get; set; }
    }

    public class Data
    {
        public List<Pack_list> pack_list { get; set; }
    }

    public class Pack_list
    {
        public int pack_id { get; set; }
        public string pack_name { get; set; }
        public int volume_mb { get; set; }
        public int channel_id { get; set; }
        public int current_balance { get; set; }
        public string status { get; set; }
    }


    public class GiftPackDisburseViewModel
    {
        /*
        "id": "12243535",
        "externalId": 118,
        "description": "OnePharmaGift",
        "orderItem": [
            {
                "product": {
                    "characteristic": {
                        "name": "Pack",
                        "value": 34
                    }
                }
            }
        ]
         */

        public string id { get; set; }
        public string externalId { get; set; }
        public string description { get; set; }
        public List<OrderItem> orderItem { get; set; }
    }
    public class OrderItem
    {
        public Product product { get; set; }
    }
    public class Product
    {
        public Characteristic characteristic { get; set; }
    }
    public class Characteristic
    {
        public string name { get; set; }
        public int value { get; set; }
    }
    public class ProductOrderResponseModel
    {
        public string id { get; set; }
        public string href { get; set; }
        public string status { get; set; }
        public string message { get; set; }
    }
        /*
            "id": "1181636031572834582",
            "href": "https://apigw.grameenphone.com/bulkdata/v4/productOrderingManagement/productOrder/01737933939",
            "status": "pending",
            "message": "Request received successfully!"
         */

}
