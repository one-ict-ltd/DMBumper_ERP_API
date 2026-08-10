using Microsoft.EntityFrameworkCore;
using ONEERP.Areas.Auth.Models;
using ONEERP.Areas.MasterData.Models;
using ONEERP.Data;
using ONEERP.ERPServices.MasterData.Interfaces;
using ONEERP.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.ERPServices.MasterData
{
    public class HelpService : IHelpService
    {
        private readonly ERPDbContext _context;

        public HelpService(ERPDbContext context)
        {
            _context = context;
        }
        public async Task<bool> SaveHelpMaster(string Id, HelpMasterViewModel helpMasterViewModel, List<HelpDetailViewModel> helpDetailViewModels, List<HelpMultiViewModel> helpMultiViewModels, List<HelpImageViewModel> helpImageViewModels)
        {

            var data = await _context.saveUpdateValueViewModels.FromSql($"CmnSpSetHelpMaster {Id},{helpMasterViewModel.helpId},{helpMasterViewModel.text},{helpMasterViewModel.dropDownId},{helpMasterViewModel.date},{helpMasterViewModel.popUp},{helpMasterViewModel.checkbox},{helpMasterViewModel.textArea},{helpMasterViewModel.radio},{helpMasterViewModel.isActive},{helpMasterViewModel.isDelete}").AsNoTracking().FirstOrDefaultAsync();
            var result = new SaveUpdateViewModel();
            if (helpDetailViewModels != null)
            {
                if (helpDetailViewModels.Count > 0)
                {
                    foreach (var datax in helpDetailViewModels)
                    {

                        result = await _context.saveUpdateViewModels.FromSql($"CmnSpSetHelpDetail {Id},{datax.helpDetailId},{data.isSuccess},{datax.dtext},{datax.ddropdownId},{datax.ddate},{datax.dpopup},{datax.dcheckbox},{datax.dradio},{datax.dImage},{datax.dtextArea},{datax.isActive},{datax.isDelete}").AsNoTracking().FirstOrDefaultAsync(); ;
                    }
                }
                else
                {
                    if (data.isSuccess > 0)
                    {
                        result.isSuccess = true;
                    }
                }
            }
            else
            {
                if (data.isSuccess > 0)
                {
                    result.isSuccess = true;
                }
            }
            if (helpMultiViewModels != null)
            {
                if (helpMultiViewModels.Count > 0)
                {
                    foreach (var datax in helpMultiViewModels)
                    {

                        result = await _context.saveUpdateViewModels.FromSql($"CmnSpSetHelpMulti {Id},{datax.multiId},{data.isSuccess},{datax.selectedId},{datax.isActive},{datax.isDelete}").AsNoTracking().FirstOrDefaultAsync(); ;
                    }
                }
                else
                {
                    if (data.isSuccess > 0)
                    {
                        result.isSuccess = true;
                    }
                }
            }
            else
            {
                if (data.isSuccess > 0)
                {
                    result.isSuccess = true;
                }
            }
            if (helpImageViewModels != null)
            {
                if (helpImageViewModels.Count > 0)
                {
                    foreach (var datax in helpImageViewModels)
                    {
                       

                        result = await _context.saveUpdateViewModels.FromSql($"CmnSpSetHelpImage {Id},{datax.helpImageId},{data.isSuccess},{datax.imageUrl},{datax.isActive},{datax.isDelete}").AsNoTracking().FirstOrDefaultAsync(); ;
                    }
                }
                else
                {
                    if (data.isSuccess > 0)
                    {
                        result.isSuccess = true;
                    }
                }
            }
            else
            {
                if (data.isSuccess > 0)
                {
                    result.isSuccess = true;
                }
            }
           
            return result.isSuccess;
        }
        public async Task<JsonViewModel> GetHelpMasterListbyId(int helpId)
        {
            var result = await _context.jsonViewModels.FromSql($"CmnSpGetHelpMasterJson {helpId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> GetHelpDetailListbyId(int helpId, int helpDetailId)
        {
            var result = await _context.jsonViewModels.FromSql($"CmnSpGetHelpDetailJson {helpId},{helpDetailId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> GetHelpMultiListbyId(int helpId, int multiId)
        {
            var result = await _context.jsonViewModels.FromSql($"CmnSpGetHelpMultiJson {helpId},{multiId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetHelpImageListbyId(int helpId, int helpImageId)
        {
            var result = await _context.jsonViewModels.FromSql($"CmnSpGetHelpImageJson {helpId},{helpImageId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }



        public async Task<bool> DeleteHelpMasterListbyId(string Id, int helpId)
        {
            //var helpDetailViewModels = await _context.helpDetailListViewModels.FromSql($"CmnSpGetHelpDetail {helpId},{0}").AsNoTracking().ToListAsync();
            //if (helpDetailViewModels.Count > 0)
            //{
            //    foreach (var datax in helpDetailViewModels)
            //    {

            //        await _context.saveUpdateViewModels.FromSql($"CmnSpDeleteHelpDeatil {Id},{datax.helpDetailId}").AsNoTracking().FirstOrDefaultAsync();
            //    }
            //}
            //var helpMultiViewModels = await _context.helpMultiListViewModels.FromSql($"CmnSpGetHelpMulti {helpId},{0}").AsNoTracking().ToListAsync();
            //if (helpMultiViewModels.Count > 0)
            //{
            //    foreach (var datax in helpMultiViewModels)
            //    {

            //        await _context.saveUpdateViewModels.FromSql($"CmnSpDeleteHelpMulti {Id},{datax.multiId}").AsNoTracking().FirstOrDefaultAsync();
            //    }
            //}
            //var helpImageViewModels = await _context.helpImageListViewModels.FromSql($"CmnSpGetHelpImage {helpId},{0}").AsNoTracking().ToListAsync();
            //if (helpImageViewModels.Count > 0)
            //{
            //    foreach (var datax in helpImageViewModels)
            //    {

            //        await _context.saveUpdateViewModels.FromSql($"CmnSpDeleteHelpImage {Id},{datax.helpImageId}").AsNoTracking().FirstOrDefaultAsync();
            //    }
            //}
            var result = await _context.saveUpdateViewModels.FromSql($"CmnSpDeleteHelpMaster {Id},{helpId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }
        public async Task<bool> DeleteHelpDetailListbyId(string Id, int helpDetailId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"CmnSpDeleteHelpDeatil {Id},{helpDetailId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }
        public async Task<bool> DeleteHelpMultiListbyId(string Id, int multiId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"CmnSpDeleteHelpMulti {Id},{multiId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }
        public async Task<bool> DeleteHelpImageListbyId(string Id, int imageId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"CmnSpDeleteHelpImage {Id},{imageId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }
    }
}
