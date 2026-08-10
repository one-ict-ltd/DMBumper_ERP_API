using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.FieldForceTracking.Models
{
    public class DoctorScheduleUpdateParamViewModel
    {
   
        public int PlanID { get; set; }
        public IFormFile ImageUrl { get; set; }        
        public string VisitTime { get; set; }
        public string territoryCode { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Remarks { get; set; }
        public string LLAddress { get; set; }
        public int ExecutionType { get; set; }
        //public string lstModel { get; set; }
        public int DoctorID { get; set; }
        public List<docExecutionDetailsModel> lstDocExecutionDetailsModel { get; set; }
    }
    public class docExecutionDetailsModel
    {
        public int? docExecutionDetailsId{ get; set; }
        public string jointMemberType { get; set; }
        public List<docExecutionMembersModel> lstDocExecutionMembersModel { get; set; }

    }
    public class docExecutionMembersModel
    {
        public int? docExecutionMembersId { get; set; }
        public string MembersName { get; set; }
        

    }
}
