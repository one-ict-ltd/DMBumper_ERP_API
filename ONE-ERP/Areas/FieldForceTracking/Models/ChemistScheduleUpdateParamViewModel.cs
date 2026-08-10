using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace ONEERP.Areas.FieldForceTracking.Models
{
    public class ChemistScheduleUpdateParamViewModel
    {
        public int PlanID { get; set; }
        public IFormFile ImageUrl { get; set; }
        public string VisitTime { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Remarks { get; set; }
        public string LLAddress { get; set; }
        public decimal? InvoiceAmount { get; set; }
        public decimal? CollectionAmount { get; set; }
        public int? paymentModeId { get; set; }
        public int ExecutionType { get; set; }
        public string territoryCode { get; set; }
        //public string lstModel { get; set; }
        public List<chemExecutionDetailsModel> lstChemExecutionDetailsModel { get; set; }
    }
    public class chemExecutionDetailsModel
    {
        public int? chemExecutionDetailsId { get; set; }
        public string jointMemberType { get; set; }
        public List<chemExecutionMembersModel> lstChemExecutionMembersModel { get; set; }

    }
    public class chemExecutionMembersModel
    {
        public int? chemExecutionMembersId { get; set; }
        public string MembersName { get; set; }


    }
}
