using ONEERP.Areas.Sales.Models;
using ONEERP.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ONEERP.ERPServices.Sales.Interfaces
{
    public interface IPaymentModeService
    {
        #region Payment Mode

       
        Task<JsonViewModel> GetPaymentModeById(int? paymentModeId);
       

        #endregion

       

      
    }
}
