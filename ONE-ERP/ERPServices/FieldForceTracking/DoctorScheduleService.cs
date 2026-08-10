using Microsoft.EntityFrameworkCore;
using ONEERP.Areas.Auth.Models;
using ONEERP.Areas.FieldForceTracking.Models;
using ONEERP.Data;
using ONEERP.ERPServices.FieldForceTracking.Interfaces;
using ONEERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ONEERP.ERPServices.FieldForceTracking
{
    public class DoctorScheduleService : IDoctorScheduleService
    {
        private readonly ERPDbContext _context;

        public DoctorScheduleService(ERPDbContext context)
        {
            _context = context;
        }

        //public async Task<bool> SaveDoctorSchedule(CmnDoctorSchedule cmnDoctorSchedule)
        //{
        //    if (cmnDoctorSchedule.DoctorScheduleID != 0)
        //        _context.CmnDoctorSchedules.Update(cmnDoctorSchedule);
        //    else
        //        _context.CmnDoctorSchedules.Add(cmnDoctorSchedule);
        //    return 1 == await _context.SaveChangesAsync();
        //}

        //public async Task<IEnumerable<CmnDoctor>> GetAllCmnDoctor()
        //{
        //    return await _context.CmnDoctor.AsNoTracking().ToListAsync();
        //}
        public async Task<bool> setPlanDoctor(string Id, int RosterID, int DoctorID, DateTime visitDate, string VisitTime, string Opinion)
        {
            var result = _context.saveScheduleViewModels.FromSql($"setPlanDoctor {Id},{RosterID},{DoctorID},{visitDate},{VisitTime},{Opinion}").AsNoTracking().FirstOrDefault();
            return result.isSuccess;
        }
        public async Task<bool> setPlanMarket(string Id, int RosterID, int MarketID, DateTime visitDate, string VisitTime, string Opinion, string ZoneCode, string DepotCode, string RegionCode, string AreaCode, string TerritoryCode, string MioCode)
        {
            var result = _context.saveScheduleViewModels.FromSql($"setPlanMarket {Id},{RosterID},{MarketID},{visitDate},{VisitTime},{Opinion},{ZoneCode},{DepotCode},{RegionCode},{AreaCode},{TerritoryCode},{MioCode}").AsNoTracking().FirstOrDefault();
            return result.isSuccess;
        }

        public async Task<int> updatePlanDoctor(string Id, int PlanID, string ImageUrl, string VisitTime, string Latitue, string Longitude, string Remarks, string LLAddress, int ExecutionType, string territoryCode,int DoctorId)
        {
            try
            {

                var result =  _context.saveUpdateValueViewModels.FromSql($"updatePlanDoctor {Id},{PlanID},{ImageUrl},{VisitTime},{Latitue},{Longitude},{Remarks},{LLAddress},{ExecutionType},{territoryCode},{DoctorId}").AsNoTracking().FirstOrDefault();

                var user = await _context.Users.Where(x => x.Id == Id).FirstOrDefaultAsync();
                var userId = user.employeeId.ToString();
                // await _context.saveUpdateViewModels.FromSql($"FftSpDeleteDoctorPromotionalItem {userId},{PlanID}").AsNoTracking().FirstOrDefaultAsync();

                //foreach (ProductSubCatGetViewModel model in lstSalesModel)
                //{
                //    foreach (ProductGetViewModel detail in model.Product.Where(a => a.invoiceQty != 0))
                //    {
                //        await _context.saveUpdateValueViewModels.FromSql($"FftSpSetDoctorPromotionalItem {userId},{PlanID},{detail.productId},{detail.productWiseSpecificationId},{detail.invoiceQty}").AsNoTracking().FirstOrDefaultAsync();
                //    }
                //}

                return result.isSuccess;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public async Task<int> setDocExecutionDetails(string Id, int DoctorScheduleID, List<docExecutionDetailsModel> ExecutionDetailsModel, string territoryCode)
        {
            try
            {
                var result = new SaveUpdateValueViewModel();
                foreach (docExecutionDetailsModel model in ExecutionDetailsModel)
                {

                    var docExecutionDetailsId = await _context.saveUpdateValueViewModels.FromSql($"setDocExecutionDetails {Id},{DoctorScheduleID},{model.jointMemberType}").AsNoTracking().FirstOrDefaultAsync();

                    if (model.jointMemberType == "ASM" || model.jointMemberType == "ZSM" || model.jointMemberType == "RSM" || model.jointMemberType == "MIO")
                    {
                        result = await _context.saveUpdateValueViewModels.FromSql($"setDocExecutionMembers {Id},{docExecutionDetailsId.isSuccess},{model.jointMemberType},{territoryCode}").AsNoTracking().FirstOrDefaultAsync();
                    }
                    else
                    {
                        foreach (docExecutionMembersModel detail in model.lstDocExecutionMembersModel)
                        {
                            result = await _context.saveUpdateValueViewModels.FromSql($"setDocExecutionMembersForPMDandOthers {Id},{docExecutionDetailsId.isSuccess},{detail.MembersName}").AsNoTracking().FirstOrDefaultAsync();
                        }
                    }
                }
                return result.isSuccess;

            }
            catch (Exception)
            {
                return 0;
            }
        }
        public async Task<bool> UpdatePlanDoctorstartTime(string Id, int PlanID, string startTime, string Latitue, string Longitude)
        {
            var result = await _context.saveScheduleViewModels.FromSql($"updatePlanDoctorStartTime {Id},{PlanID},{startTime},{Latitue},{Longitude}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        public async Task<bool> PlanExecutionDoctor(string Id, int RosterID, int DoctorID, int MarketID, string ImageUrl, DateTime VisitDate, string VisitTime, string Latitue, string Longitude, string Remarks, string LLAddress)
        {
            var result = _context.saveScheduleViewModels.FromSql($"setPlanExecutionDoctor {Id},{RosterID},{DoctorID},{MarketID},{ImageUrl},{VisitDate},{VisitTime},{Latitue},{Longitude},{Remarks},{LLAddress}").AsNoTracking().FirstOrDefault();
            return result.isSuccess;
        }

        public async Task<bool> PlanExecutionEmp(string Id, int RosterID, string EmpCode, int MarketID, string ImageUrl, DateTime VisitDate, string VisitTime, string Latitue, string Longitude, string Remarks, string LLAddress)
        {
            var result = _context.saveScheduleViewModels.FromSql($"setPlanExecutionEmp {Id},{RosterID},{EmpCode},{MarketID},{ImageUrl},{VisitDate},{VisitTime},{Latitue},{Longitude},{Remarks},{LLAddress}").AsNoTracking().FirstOrDefault();
            return result.isSuccess;
        }

        //public async Task<IEnumerable<DoctorScheduleListViewModel>> getDrListAfterSetPlan(string Id, string VisitDate, int rosterID)
        //{
        //    var result = _context.doctorScheduleListViewModels.FromSql($"getDrListAfterSetPlan {Id},{VisitDate},{rosterID}").AsNoTracking().ToList();
        //    return result;
        //}

        public async Task<JsonViewModel> getDrListAfterSetPlan(string Id, string VisitDate, int rosterID, string employeeNo)
        {
            var result = await _context.jsonViewModels.FromSql($"getDrListAfterSetPlan {Id},{VisitDate},{rosterID},{employeeNo}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> getDashBoardPlanApp(string Id, DateTime date, string ZoneCode, string RegionCode, string AreaCode, string TerritoryCode)
        {
            var result = await _context.jsonViewModels.FromSql($"CmnSpGetAppsDashBoardSchedule {Id},{date},{ZoneCode},{RegionCode},{AreaCode},{TerritoryCode}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel3> getDashBoardPlanApp3(string Id, DateTime date, string ZoneCode, string RegionCode, string AreaCode, string TerritoryCode)
        {
            var result = await _context.jsonViewModels3.FromSql($"CmnSpGetAppsDashBoardSchedule {Id},{date},{ZoneCode},{RegionCode},{AreaCode},{TerritoryCode}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
         public async Task<JsonViewModel4> getDashBoardPlanApp4(string Id, DateTime date, string ZoneCode, string RegionCode, string AreaCode, string TerritoryCode)
        {
            var result = await _context.jsonViewModels4.FromSql($"CmnSpGetAppsDashBoardScheduleAsOnProductivity {Id},{date},{ZoneCode},{RegionCode},{AreaCode},{TerritoryCode}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel4> getDashBoardPlanApp5(string Id, DateTime date, string ZoneCode, string RegionCode, string AreaCode, string TerritoryCode)
        {
            var result = await _context.jsonViewModels4.FromSql($"CmnSpGetAppsDashBoardScheduleAsOnSalesCollection {Id},{date},{ZoneCode},{RegionCode},{AreaCode},{TerritoryCode}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> getDashBoardPlanAppDaily(string Id, DateTime date, string ZoneCode, string RegionCode, string AreaCode, string TerritoryCode)
        {
            var result = await _context.jsonViewModels.FromSql($"CmnSpGetAppsDashBoardScheduleDaily {Id},{date},{ZoneCode},{RegionCode},{AreaCode},{TerritoryCode}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> getDashBoardAttnApp(string Id, DateTime date, string ZoneCode, string RegionCode, string AreaCode, string TerritoryCode)
        {
            var result = await _context.jsonViewModels.FromSql($"CmnSpGetAppsDashBoardAttendence {Id},{date},{ZoneCode},{RegionCode},{AreaCode},{TerritoryCode}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<bool> setCurrentLocation(string Id, string Latitude, string Longitude, string Address, string DateTime)
        {
            var result = _context.saveScheduleViewModels.FromSql($"setLocationData {Id},{Latitude},{Longitude},{Address},{DateTime}").AsNoTracking().FirstOrDefault();
            return result.isSuccess;
        }
        public async Task<bool> setCheckInOut(string Id, string Latitude, string Longitude, string DateTime, int Flag, string address, string opinion, string time, bool? isHQ, bool? isEHQ, bool? isOS, bool? isOther)
        {
            var result = _context.saveScheduleViewModels.FromSql($"setCheckinOut {Id},{Latitude},{Longitude},{DateTime},{Flag},{address},{opinion},{time},{isHQ},{isEHQ},{isOS},{isOther}").AsNoTracking().FirstOrDefault();
            return result.isSuccess;
        }

        public async Task<EmployeeLoadJsonViewModel> GetMarketScheduleJsonViewModels(string Id, string Date, int RosterId)
        {
            var result = await _context.employeeLoadJsonViewModels.FromSql($"getmarketplandata {Id},{Date},{RosterId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<EmployeeLoadJsonViewModel> GetEmployeeDynamicJsonViewModels(string Code, string Type, string EmpCode)
        {
            var result = await _context.employeeLoadJsonViewModels.FromSql($"getEmployeeListDynamics {Code},{Type},{EmpCode}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<EmployeeLoadJsonViewModel> GetDoctorsDynamicJsonViewModels(string Code, string Type)
        {
            var result = await _context.employeeLoadJsonViewModels.FromSql($"getDoctorListDynamics {Code},{Type}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<EmployeeLoadJsonViewModel> GetChemistsDynamicJsonViewModels(string Code, string Type)
        {
            var result = await _context.employeeLoadJsonViewModels.FromSql($"getChemistListDynamics {Code},{Type}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<EmployeeLoadJsonViewModel> GetEmployeeReportDynamicJsonViewModels(string Code, string CodeType, string Type, string EmpCode)
        {
            var result = await _context.employeeLoadJsonViewModels.FromSql($"getEmployeeListReportDynamics {Code},{CodeType},{Type},{EmpCode}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<bool> deletePlanDoctor(string userId, int PlanId)
        {
            var result = await _context.saveScheduleViewModels.FromSql($"FftSpDeletePlanDoctor {userId},{PlanId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }


    }
}
