using Microsoft.EntityFrameworkCore;
using ONEERP.Areas.Auth.Models;
using ONEERP.Areas.FieldForceTracking.Models;
using ONEERP.Areas.Sales.Models;
using ONEERP.Areas.Schedule.Models;
using ONEERP.Data;
using ONEERP.ERPServices.FieldForceTracking.Interfaces;
using ONEERP.Models;
using ONEERP.Models.Dashboard;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;


namespace ONEERP.ERPServices.Schedule
{
    public class PrescriptionsService : IPrescriptionsService
    {
        private readonly ERPDbContext _context;

        public PrescriptionsService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<bool> SetPrescriptions(int? userId, List<DoctorsPrescriptionsViewModel> models)
        {
            string fileName, location, imagePath;
            var result = new SaveUpdateViewModel();
            foreach (var item in models)
            {
                try
                {
                    //Save Image in folder
                    if (item.ImageUrl != null)
                    {
                        fileName = ""; location = ""; imagePath = "";

                        var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".webp" };
                        var extention = Path.GetExtension(item.ImageUrl.FileName);

                        fileName = DateTime.Now.Ticks + extention;
                        location = Path.Combine("PrescriptionImages", fileName);
                        imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", location);
                        using (var streams = new FileStream(imagePath, FileMode.Create))
                        {
                            item.ImageUrl.CopyTo(streams);
                        }

                        item.ImagePath = imagePath;
                    }

                }
                catch (Exception ex)
                {
                    throw;
                }

                result = await _context.saveUpdateViewModels.FromSql($"FftSpSetPrescriptions {userId},{item.PrescriptioID},{item.DoctorID},{item.Date},{item.ImagePath},{item.Remarks},{item.IsActive}").AsNoTracking().FirstOrDefaultAsync();
            }
            return result.isSuccess;
        }

        public async Task<JsonViewModel> GetPrescriptions(int? prescriptioID, DateTime? date)
        {
            var result = await _context.jsonViewModels.FromSql($"FftSpGetPrescriptionsJson {prescriptioID},{date}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
    }
}
