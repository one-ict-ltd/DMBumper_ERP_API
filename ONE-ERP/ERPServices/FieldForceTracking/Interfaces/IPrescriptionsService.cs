
using ONEERP.Areas.FieldForceTracking.Models;
using ONEERP.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace ONEERP.ERPServices.FieldForceTracking.Interfaces
{
    public interface IPrescriptionsService
    {
        Task<bool> SetPrescriptions(int? userId, List<DoctorsPrescriptionsViewModel> models);
        Task<JsonViewModel> GetPrescriptions(int? prescriptioID, DateTime? date);
        
    }
}
