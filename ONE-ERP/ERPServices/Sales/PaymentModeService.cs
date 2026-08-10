using Microsoft.EntityFrameworkCore;
using ONEERP.Areas.Auth.Models;
using ONEERP.Areas.Sales.Models;
using ONEERP.Data;
using ONEERP.ERPServices.Sales.Interfaces;
using ONEERP.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ONEERP.ERPServices.Sales
{
    public class PaymentModeService : IPaymentModeService
    {
        private readonly ERPDbContext _context;
        public PaymentModeService(ERPDbContext context)
        {
            _context = context;
        }

        #region Payment Mode

        

        public async Task<JsonViewModel> GetPaymentModeById(int? paymentModeId)
        {
            var result = await _context.jsonViewModels.FromSql($"SalSpGetPaymentModeJSON {paymentModeId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        
        #endregion

      

       
    }
}
