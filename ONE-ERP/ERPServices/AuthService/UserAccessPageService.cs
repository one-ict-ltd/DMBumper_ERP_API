using Microsoft.EntityFrameworkCore;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Database;
using ONEERP.Areas.Auth.Models;

using ONEERP.Data;
using ONEERP.Data.Entity;


using ONEERP.ERPService.AuthService.Interfaces;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.ERPServices.AuthService
{
    public class UserAccessPageService : IUserAccessPageService
    {
        private readonly ERPDbContext _context;
        public UserAccessPageService(ERPDbContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<UserAccessPageListViewModel>> GetUserAccessPageList()
        {
            var result = await _context.userAccessPageListViewModels.FromSql($"CmnSpGetUserAccessPage").AsNoTracking().ToListAsync();
            return result;
        }





    }
}
