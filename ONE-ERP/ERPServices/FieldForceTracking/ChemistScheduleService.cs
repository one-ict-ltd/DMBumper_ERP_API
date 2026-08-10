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
    public class ChemistScheduleService : IChemistScheduleService
    {
        private readonly ERPDbContext _context;

        public ChemistScheduleService(ERPDbContext context)
        {
            _context = context;
        }

        //public async Task<bool> SaveChemistSchedule(CmnChemistSchedule cmnChemistSchedule)
        //{
        //    if (cmnChemistSchedule.ChemistScheduleID != 0)
        //        _context.CmnChemistSchedules.Update(cmnChemistSchedule);
        //    else
        //        _context.CmnChemistSchedules.Add(cmnChemistSchedule);
        //    return 1 == await _context.SaveChangesAsync();
        //}

        //public async Task<IEnumerable<CmnDoctor>> GetAllCmnDoctor()
        //{
        //    return await _context.CmnDoctor.AsNoTracking().ToListAsync();
        //}
        public async Task<bool> setPlanChemist(string Id, int RosterID, int ChemistID, DateTime visitDate, string VisitTime, string Opinion)
        {
            var result = await _context.saveScheduleViewModels.FromSql($"setPlanChemist {Id},{RosterID},{ChemistID},{visitDate},{VisitTime},{Opinion}").AsNoTracking().FirstOrDefaultAsync();

            return result.isSuccess;

        }
        public async Task<bool> PlanExecutionChemist(string Id, int RosterID, int ChemistID, int MarketID, string ImageUrl, DateTime VisitDate, string VisitTime, string Latitue, string Longitude, string Remarks, string LLAddress, decimal? InvoiceAmount, decimal? CollectionAmount)
        {
            var result = _context.saveScheduleViewModels.FromSql($"setPlanExecutionChemist {Id},{RosterID},{ChemistID},{MarketID},{ImageUrl},{VisitDate},{VisitTime},{Latitue},{Longitude},{Remarks},{LLAddress},{InvoiceAmount},{CollectionAmount}").AsNoTracking().FirstOrDefault();
            return result.isSuccess;
        }

        public async Task<int> updatePlanChemist(string Id, int PlanID, string ImageUrl, string VisitTime, string Latitue, string Longitude, string Remarks, string LLAddress, decimal? InvoiceAmount, decimal? CollectionAmount, int? paymentModeId, int ExecutionType ,string territoryCode)
        {
            try
            {
                var result = await _context.saveUpdateValueViewModels.FromSql($"updatePlanChemist {Id},{PlanID},{ImageUrl},{VisitTime},{Latitue},{Longitude},{Remarks},{LLAddress},{InvoiceAmount},{CollectionAmount},{paymentModeId},{ExecutionType},{territoryCode}").AsNoTracking().FirstOrDefaultAsync();

            var user = await _context.Users.Where(x => x.Id == Id).FirstOrDefaultAsync();
            var userId = user.employeeId.ToString();

            //decimal? grandTotal = 0;
            //foreach (ProductSubCatGetViewModel model in lstSalesModel)
            //{
            //    foreach (ProductGetViewModel detail in model.Product.Where(a => a.invoiceQty != 0))
            //    {
            //        grandTotal += detail.totalPrice;
            //    }
            //}

            //if (grandTotal != 0)
            //{
            //    var salesInvoiceId = await _context.saveUpdateValueViewModels.FromSql($"SalSpSetSalesInvoice {userId}, {0}, {""}, {DateTime.Now}, {DateTime.Now}, {null}, {null}, {""}, {""}, {""}, {grandTotal}, {0}, {0}, {0}, {0}, {grandTotal}, {""}, {1}, {PlanID}, {0},{""},{null}").AsNoTracking().FirstOrDefaultAsync();

            //    foreach (ProductSubCatGetViewModel model in lstSalesModel)
            //    {
            //        foreach (ProductGetViewModel detail in model.Product.Where(a => a.invoiceQty != 0))
            //        {
            //            await _context.saveUpdateValueViewModels.FromSql($"SalSpSetSalesInvoiceDetails {userId},{detail.salesInvDetailsId},{Convert.ToInt32(salesInvoiceId.isSuccess)},{detail.productId},{detail.productWiseSpecificationId},{detail.invoiceQty},{detail.price},{0},{0},{0},{detail.totalPrice},{1},{0},{null},{""},{detail.tradePrice},{detail.unitVat}").AsNoTracking().FirstOrDefaultAsync();
            //        }
            //    }
            //}

                return result.isSuccess;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public async Task<int> setChemExecutionDetails(string Id, int ChemScheduleID, List<chemExecutionDetailsModel> ExecutionDetailsModel, string territoryCode)
        {
            try
            {
                var result = new SaveUpdateValueViewModel();
                foreach (chemExecutionDetailsModel model in ExecutionDetailsModel)
                {

                    var chemExecutionDetailsId = await _context.saveUpdateValueViewModels.FromSql($"setChemExecutionDetails {Id},{ChemScheduleID},{model.jointMemberType}").AsNoTracking().FirstOrDefaultAsync();

                    if (model.jointMemberType == "ASM" || model.jointMemberType == "ZSM" || model.jointMemberType == "RSM" || model.jointMemberType == "MIO")
                    {
                        result = await _context.saveUpdateValueViewModels.FromSql($"setChemExecutionMembers {Id},{chemExecutionDetailsId.isSuccess},{model.jointMemberType},{territoryCode}").AsNoTracking().FirstOrDefaultAsync();
                    }
                    else
                    {
                        foreach (chemExecutionMembersModel detail in model.lstChemExecutionMembersModel)
                        {
                            result = await _context.saveUpdateValueViewModels.FromSql($"setChemExecutionMembersForPMDandOthers {Id},{chemExecutionDetailsId.isSuccess},{detail.MembersName}").AsNoTracking().FirstOrDefaultAsync();
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
        //public async Task<int> CreateSalesOrderByChemist(string Id, DateTime? visitDate, int chemistId, List<ProductSubCatGetViewModel> lstSalesModel)
        //{
        //    var user = await _context.Users.Where(x => x.Id == Id).FirstOrDefaultAsync();
        //    var userId = user.employeeId.ToString();

        //    decimal? grandTotal = 0;
        //    foreach (ProductSubCatGetViewModel model in lstSalesModel)
        //    {
        //        foreach (ProductGetViewModel detail in model.Product.Where(a => a.saleUnit != 0))
        //        {
        //            grandTotal += detail.totalPrice;
        //        }
        //    }

        //    var salesInvoiceId = await _context.saveUpdateValueViewModels.FromSql($"SalSpSetSalesInvoice {userId}, {0}, {""}, {DateTime.Now}, {DateTime.Now}, {null}, {null}, {""}, {""}, {""}, {grandTotal}, {0}, {0}, {0}, {0}, {grandTotal}, {""}, {1}, {0}, {chemistId}").AsNoTracking().FirstOrDefaultAsync();

        //    foreach (ProductSubCatGetViewModel model in lstSalesModel)
        //    {
        //        foreach (ProductGetViewModel detail in model.Product.Where(a => a.saleUnit != 0))
        //        {
        //            await _context.saveUpdateValueViewModels.FromSql($"SalSpSetSalesInvoiceDetails {userId},{0},{Convert.ToInt32(salesInvoiceId.isSuccess)},{detail.productId},{detail.productWiseSpecificationId},{detail.saleUnit},{detail.price},{0},{0},{0},{detail.totalPrice},{1},{0},{null},{""}").AsNoTracking().FirstOrDefaultAsync();
        //        }
        //    }

        //    return salesInvoiceId.isSuccess;
        //}

        public async Task<int> SaveSalesOrderMasterByChemist(string userId, ChemistSalesOrderCreateViewModel model)
        {
            decimal? grandTotal = 0;
            foreach (ProductSubCatGetViewModel subModel in model.OrderDetails)
            {
                foreach (ProductGetViewModel detail in subModel.Product.Where(a => a.invoiceQty != 0))
                {
                    grandTotal += detail.totalPrice;
                }
            }

            var result = await _context.saveUpdateValueViewModels.FromSql($"SalSpSetSalesInvoice {userId}, {model.salesInvoiceId}, {""}, {DateTime.Now}, {DateTime.Now}, {null}, {model.chemistId}, {""}, {""}, {""}, {grandTotal}, {0}, {0}, {0}, {0}, {grandTotal}, {""}, {1}, {0}, {null},{""},{null},{model.orderType} ").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        public async Task<int> SalesOrderDetailsByChemist(string userId, List<ProductSubCatGetViewModel> lstSalesModel, int salesInvoiceId)
        {
            var result = new SaveUpdateValueViewModel();
            foreach (ProductSubCatGetViewModel model in lstSalesModel)
            {
                foreach (ProductGetViewModel detail in model.Product.Where(a => a.invoiceQty != 0))
                {
                    result = await _context.saveUpdateValueViewModels.FromSql($"SalSpSetSalesInvoiceDetails {userId},{detail.salesInvDetailsId},{Convert.ToInt32(salesInvoiceId)},{detail.productId},{detail.productWiseSpecificationId},{detail.invoiceQty},{detail.price},{0},{0},{0},{detail.totalPrice},{1},{1},{null},{""},{detail.tradePrice},{detail.unitVat}").AsNoTracking().FirstOrDefaultAsync();
                }
            }
            return result.isSuccess;
        }

        //public async Task<IEnumerable<ChemistScheduleListViewModel>> getChListAfterSetPlan(string Id, string VisitDate, int rosterID)
        //{
        //    var result = await _context.chemistScheduleListViewModels.FromSql($"getChListAfterSetPlan {Id},{VisitDate},{rosterID}").AsNoTracking().ToListAsync();
        //    return result;
        //}

        public async Task<JsonViewModel> getChListAfterSetPlan(string Id, string VisitDate, int rosterID,string employeeNo)
        {
            var result = await _context.jsonViewModels.FromSql($"getChListAfterSetPlan {Id},{VisitDate},{rosterID},{employeeNo}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> getDashboardAttendanceDetails(string Id, string usertype, string type, string ZoneCode, string RegionCode, string AreaCode,DateTime date,string TerritoryCode)
        {
            var result = await _context.jsonViewModels.FromSql($"getDashboardAttendanceDetails {Id},{usertype},{type},{ZoneCode},{RegionCode},{AreaCode},{date},{TerritoryCode}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> getTADAByEmployeeCode(string Id)
        {
            var result = await _context.jsonViewModels.FromSql($"CmnGetEmployeeWiseTADA {Id}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> getTADAReportByEmployeeCode(string Id)
        {
            var result = await _context.jsonViewModels.FromSql($"CmnGetEmployeeWiseTADAN {Id}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> getVehicleBillByEmployeeCode(string Id)
        {
            var result = await _context.jsonViewModels.FromSql($"CmnGetEmployeeWiseVehicleBill {Id}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> getEmployeeWiseVehicleBillByEmployeeCode(string employeeCode)
        {
            var result = await _context.jsonViewModels.FromSql($"CmnGetEmployeeWiseVehicleBillByEmployeeCode {employeeCode}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> getCmnWeeklyPlanDocByStatus(string Id, string employeeCode)
        {
            var result = await _context.jsonViewModels.FromSql($"CmnGetCmnWeeklyPlanDocByStatus {Id},{employeeCode}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        
        public async Task<JsonViewModel> getCmnGetCmnWeeklyPlanChemistByStatus(string Id, string employeeCode)
        {
            var result = await _context.jsonViewModels.FromSql($"CmnGetCmnWeeklyPlanChemistByStatus {Id},{employeeCode}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        
        public async Task<JsonViewModel> GetSetsalesTargetIdJson(int employeeId, int month, int year)
        {
            var result = await _context.jsonViewModels.FromSql($"CmnSpGetSetsalesTargetIdJson {employeeId},{month},{year}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        
        public async Task<JsonViewModel> GetSetsalesTargetIdReportJson(int employeeId, int month, int year)
        {
            var result = await _context.jsonViewModels.FromSql($"CmnSpGetSetsalesTargetIdReportJson {employeeId},{month},{year}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        
        public async Task<JsonViewModel> getCmnDoctorUnderObserbationByStatus(string Id, string employeeCode, string RegionCode, string AreaCode)
        {
            var result = await _context.jsonViewModels.FromSql($"CmnDoctorUnderObserbationByStatus {Id},{employeeCode},{RegionCode},{AreaCode}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        
        public async Task<JsonViewModel> getCmnweeklyplanterritoryByempCode(string Id, string employeeCode)
        {
            var result = await _context.jsonViewModels.FromSql($"CmnweeklyplanterritoryByStatus {Id},{employeeCode}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        
        public async Task<JsonViewModel> CmnweeklyplanterritoryApprovedToday(string Id, string employeeCode)
        {
            var result = await _context.jsonViewModels.FromSql($"CmnweeklyplanterritoryApprovedToday {Id},{employeeCode}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        
        public async Task<JsonViewModel> getEmployeeTADAByStatus(string Id)
        {
            var result = await _context.jsonViewModels.FromSql($"CmnGetEmployeeTADAByStatus {Id}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        
        public async Task<JsonViewModel> getEmployeeWiseTADAByEmployeeCode(string Id)
        {
            var result = await _context.jsonViewModels.FromSql($"CmnGetEmployeeWiseTADAByEmployeeCode {Id}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        
        public async Task<JsonViewModel> getEmployeeByRegionZoneTerritory(string Id)
        {
            var result = await _context.jsonViewModels.FromSql($"CmnGetEmployeeByRegionZoneTerritory {Id}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        
        public async Task<JsonViewModel> getDoctorBasicDegree(string Id)
        {
            var result = await _context.jsonViewModels.FromSql($"CmnGetDoctorBasicDegree {Id}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        
        public async Task<JsonViewModel> getTerritoryWiseMonthlyPromoItem(string Id,int monthNo)
        {
            var result = await _context.jsonViewModels.FromSql($"CmnGetTerritoryWiseMonthlyPromoItem {Id},{monthNo}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        
        public async Task<JsonViewModel> getActionPlan(int userId)
        {
            var result = await _context.jsonViewModels.FromSql($"CmnSpGetActionPlan {userId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        
        public async Task<JsonViewModel> getExamContent()
        {
            var result = await _context.jsonViewModels.FromSql($"CmnSpGetExamContent").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> getExamContentNew()
        {
            var result = await _context.jsonViewModels.FromSql($"CmnSpGetExamContentNew").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> getExamContentById(int contentId)
        {
            var result = await _context.jsonViewModels.FromSql($"CmnSpGetExamContentById {contentId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> getAllExamContent()
        {
            var result = await _context.jsonViewModels.FromSql($"CmnSpGetAllExamContent").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        
        public async Task<JsonViewModel> getExamByContentId(int contentId,int employeeId)
        {
            var result = await _context.jsonViewModels.FromSql($"CmnSpGetExamByContentId {contentId},{employeeId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        
        public async Task<JsonViewModel> getExamById(int examId, int employeeId)
        {
            var result = await _context.jsonViewModels.FromSql($"CmnSpGetExamById {examId},{employeeId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        
        public async Task<JsonViewModel> GetExamQuestionSetByExamId(int examId, int employeeId)
        {
            var result = await _context.jsonViewModels.FromSql($"CmnSpGetExamQuestionSetByExamId {examId},{employeeId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        
        public async Task<JsonViewModel> getExamQuestionByexamId(int examId)
        {
            var result = await _context.jsonViewModels.FromSql($"CmnSpGetQuestionByexamId {examId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        
        public async Task<JsonViewModel> getExamResultByExamId(int examId)
        {
            var result = await _context.jsonViewModels.FromSql($"CmnSpGetExamResultByexamId {examId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        
        public async Task<JsonViewModel> getGetExamResultByexamId(int employeeId,int examId,int status)
        {
            var result = await _context.jsonViewModels.FromSql($"CmnSpGetExamResult {employeeId},{examId},{status}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetExamResult(int employeeId, int status)
        {
            var result = await _context.jsonViewModels.FromSql($"CmnSpGetExamResult {employeeId},{status}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> getKnowledgeSkill()
        {
            var result = await _context.jsonViewModels.FromSql($"CmnSpGetKnowledgeSkill").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        
        public async Task<JsonViewModel> getAppsversion(int userId)
        {
            var result = await _context.jsonViewModels.FromSql($"DboGetAppsVersionJson {userId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        
        public async Task<JsonViewModel> getActionCampain(int userId)
        {
            var result = await _context.jsonViewModels.FromSql($"CmnSpGetActionCampain {userId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<IEnumerable<VisitReportChemistViewModel>> VisitReportChemistViewModels(string ZoneCode, string DepotCode, string RegionCode, string AreaCode, string TerritoryCode, string EmpCode, string fromDate, string toDate)
        {
            var result = await _context.visitReportChemistViewModels.FromSql($"FftSpGetVisistReportChemist {ZoneCode},{DepotCode},{RegionCode},{AreaCode},{TerritoryCode},{EmpCode},{fromDate},{toDate}").AsNoTracking().ToListAsync();

            return result;

        }
        public async Task<int> setExamContent(int employeeId, ExamContentViewModel model)
        {
            try
            {
                string[] res = model.fileName.Split(',');
                if (!string.IsNullOrEmpty(model.fileName) && res.Length > 1)
                {
                    Byte[] bytes = Convert.FromBase64String(res[1]);

                    string[] extention = res[0].Split("/");
                    string servePath = ("./wwwroot/Exam");
                    string serveUrl = ("/Exam");
                    if (!System.IO.Directory.Exists(servePath)) System.IO.Directory.CreateDirectory(servePath);
                    string fileName = ($"{DateTime.Now.Ticks}.{extention[1].Replace(";base64", "")}");
                    string filePath = ($"{servePath}/{fileName}");
                    File.WriteAllBytes(filePath, bytes);
                    string file = ($"{serveUrl}/{fileName}");
                    model.fileName = file;//fileName
                }

                await _context.saveUpdateValueViewModels.FromSql($"CmnSpSetExamContent {employeeId},{model.CmnExamContentID},{model.fileName},{model.description},{model.fromDate},{model.endDate},{model.isActive}").AsNoTracking().ToListAsync();

                return 1;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }

        public async Task<int> setExam(int employeeId, CmnExamQuestionViewModel model)
        {
            try
            {
                var saveExam = await _context.saveUpdateValueViewModels.FromSql($"CmnSpSetExamCreate {employeeId},{model.productId},{model.productCode},{model.expiryDate},{model.productName},{model.productTypeId},{model.width}, {model.startTime}, {model.lastSubmitDate}, {model.endTime}").AsNoTracking().FirstOrDefaultAsync();
                int examId = saveExam.isSuccess;
                await _context.saveUpdateValueViewModels.FromSql($"CmnSpDeleteExamQuestionAnswer {employeeId},{examId}").AsNoTracking().FirstOrDefaultAsync();

                int QuestionId=0;
                for(int i = 0; i < model.Specificationdetail.Count(); i++) 
                {
                    if (model.Specificationdetail[i].skuName != "")
                    {
                        var saveQuestion = await _context.saveUpdateValueViewModels.FromSql($"CmnSpSetExamQuestion {employeeId},{examId},{model.Specificationdetail[i].skuName},{model.Specificationdetail[i].skuNumber}").AsNoTracking().FirstOrDefaultAsync();
                        QuestionId = saveQuestion.isSuccess;
                    }

                    await _context.saveUpdateValueViewModels.FromSql($"CmnSpSetExamQuestionOption {employeeId},{QuestionId},{model.Specificationdetail[i].specificationType},{model.Specificationdetail[i].value}").AsNoTracking().FirstOrDefaultAsync();

                }

                return 1;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public async Task<int> deleteExamContent(int employeeId, int examContentId)
        {
            try
            {
                var deleteExam = await _context.saveUpdateValueViewModels.FromSql($"CmnSpDeleteExamContent {employeeId}, {examContentId}").AsNoTracking().FirstOrDefaultAsync();
                return deleteExam.isSuccess;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<int> setExamPerform(int employeeId, CmnExamPerformViewModel model)
        {
            try
            {
                await _context.saveUpdateValueViewModels.FromSql($"CmnSpSetExamPerform {employeeId},{model.CmnExamPerformID},{model.CmnExamQuestionId},{employeeId},{model.CmnExamQuestionOptionId},{model.marks}").AsNoTracking().ToListAsync();

                return 1;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }
        public async Task<bool> updatePlanChemiststartTime(string Id, int PlanID, string startTime, string Latitue, string Longitude)
        {
            var result = await _context.saveScheduleViewModels.FromSql($"updatePlanChemiststartTime {Id},{PlanID},{startTime},{Latitue},{Longitude}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        public async Task<int> setRxUploadMaster(string Id, int rxUploadMasterID, int doctorId,DateTime date)
        {
            var result = await _context.saveUpdateValueViewModels.FromSql($"setRxUploadMaster {Id},{rxUploadMasterID},{doctorId},{date}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        public async Task<int> setRxUploadImage(string Id, int rxUploadMasterID, string imageUrl)
        {
            var result = await _context.saveUpdateValueViewModels.FromSql($"setRxUploadImage {Id},{rxUploadMasterID},{imageUrl}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        public async Task<int> setNoticeUploadImage(int Id, int UploadMasterID, int status,DateTime? fDate,DateTime? tDate,string imageUrl)
        {
            var result = await _context.saveUpdateValueViewModels.FromSql($"setNoticeUploadImage {Id},{UploadMasterID},{status},{fDate},{tDate},{imageUrl}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        public async Task<int> setRxUploadProduct(string Id, int rxUploadMasterID, int productId)
        {
            var result = await _context.saveUpdateValueViewModels.FromSql($"setRxUploadProduct {Id},{rxUploadMasterID},{productId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        public async Task<bool> deletePlanChemist(string Id, int PlanId)
        {
            var result = await _context.saveScheduleViewModels.FromSql($"deletePlanChemist {Id},{PlanId}").AsNoTracking().FirstOrDefaultAsync();


            return result.isSuccess;

        }
        public async Task<IEnumerable<VisitReportEmployeeViewModel>> VisitReportEmployeeViewModels(string ZoneCode, string DepotCode, string RegionCode, string AreaCode, string TerritoryCode, string EmpCode, string fromDate, string toDate)
        {
            var result = await _context.visitReportEmployeeViewModels.FromSql($"VisistReportEmployee {ZoneCode},{DepotCode},{RegionCode},{AreaCode},{TerritoryCode},{EmpCode},{fromDate},{toDate}").AsNoTracking().ToListAsync();

            return result;

        }
        public async Task<IEnumerable<VisitReportDoctorViewModel>> VisitReportDoctorViewModels(string ZoneCode, string DepotCode, string RegionCode, string AreaCode, string TerritoryCode, string EmpCode, string fromDate, string toDate)
        {
            var result = await _context.visitReportDoctorViewModels.FromSql($"FftSpGetVisitReportDoctor {ZoneCode},{DepotCode},{RegionCode},{AreaCode},{TerritoryCode},{EmpCode},{fromDate},{toDate}").AsNoTracking().ToListAsync();

            return result;

        }
        public async Task<IEnumerable<ChemistWiseVisitReportViewModel>> ChemistWiseVisitReportViewModels(string ZoneCode, string DepotCode, string RegionCode, string AreaCode, string TerritoryCode, string MarketCode, int Id, string fromDate, string toDate)
        {
            var result = await _context.chemistWiseVisitReportViewModels.FromSql($"getChemistWiseVisistReport {ZoneCode},{DepotCode},{RegionCode},{AreaCode},{TerritoryCode},{MarketCode},{Id},{fromDate},{toDate}").AsNoTracking().ToListAsync();

            return result;

        }
        public async Task<IEnumerable<ChemistWiseVisitReportViewModel>> ChemistWiseVisitReportDViewModels(int Id, string fromDate, string toDate)
        {
            var result = await _context.chemistWiseVisitReportViewModels.FromSql($"getChemistWiseVisistReportD {Id},{fromDate},{toDate}").AsNoTracking().ToListAsync();
            return result;
        }


        public async Task<IEnumerable<ChemistWiseVisitReportViewModel>> ChemistDataChartViewModels(int Id, string fromDate, string toDate)
        {
            var result = await _context.chemistWiseVisitReportViewModels.FromSql($"getChemistWiseVisistReportChart {Id},{fromDate},{toDate}").AsNoTracking().ToListAsync();
            return result;
        }

        public async Task<IEnumerable<DoctorWiseVisitReportViewModel>> DoctorWiseVisitReportViewModels(string ZoneCode, string DepotCode, string RegionCode, string AreaCode, string TerritoryCode, string MarketCode, int Id, string fromDate, string toDate)
        {
            var result = await _context.doctorWiseVisitReportViewModels.FromSql($"getDoctorWiseVisistReport {ZoneCode},{DepotCode},{RegionCode},{AreaCode},{TerritoryCode},{MarketCode},{Id},{fromDate},{toDate}").AsNoTracking().ToListAsync();

            return result;

        }
        public async Task<IEnumerable<DoctorWiseVisitReportViewModel>> DoctorWiseVisitReportDViewModels(int Id, string fromDate, string toDate)
        {
            var result = await _context.doctorWiseVisitReportViewModels.FromSql($"getDoctorWiseVisistReportD {Id},{fromDate},{toDate}").AsNoTracking().ToListAsync();

            return result;

        }




        public async Task<JsonViewModel> GetMIODoctorVisitReport(string ZoneId, string DepoId, string RegionId, string AreaId, string TerritoryID, string EmpCode, string fd, string td)
        {
            var result = await _context.jsonViewModels.FromSql($"VisistReportDoctor {ZoneId},{DepoId},{RegionId},{AreaId},{TerritoryID},{EmpCode},{fd},{td}").AsNoTracking().FirstOrDefaultAsync();
            return result;

        }

        public async Task<JsonViewModel> GetChemistVisitReport(string ZoneId, string DepoId, string RegionId, string AreaId, string TerritoryID, string EmpCode, string fd, string td)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"VisistReportChemist {ZoneId},{DepoId},{RegionId},{AreaId},{TerritoryID},{EmpCode},{fd},{td}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<JsonViewModel> GetChemistWiseVisitReport(string ZoneId, string DepoId, string RegionId, string AreaId, string TerritoryID, string MarketName, int? ChemistId, string fd, string td)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"getChemistWiseVisistReportJson {ZoneId},{DepoId},{RegionId},{AreaId},{TerritoryID},{MarketName},{ChemistId},{fd},{td}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<JsonViewModel> GetDoctorWiseVisitReport(string ZoneId, string DepoId, string RegionId, string AreaId, string TerritoryID, string MarketName, string DoctorId, string fd, string td)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"getDoctorWiseVisistReportJson {ZoneId},{DepoId},{RegionId},{AreaId},{TerritoryID},{MarketName},{DoctorId},{fd},{td}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<IEnumerable<StockSalesChartViewModel>> StockSalesChartViewModels(string ZoneCode, string DepotCode, string RegionCode, string AreaCode, string TerritoryCode, string EmpCode, string Date)
        {
            var result = new List<StockSalesChartViewModel>();
            try
            {
                result = await _context.stockSalesChartViewModels.FromSql($"getStockSaleDashBoard {ZoneCode},{DepotCode},{RegionCode},{AreaCode},{TerritoryCode},{EmpCode},{Date}").AsNoTracking().ToListAsync();
                return result;

            }
            catch (Exception ex)
            {
                return result;
            }



        }
        public async Task<JsonViewModel> AttendanceViewModels(string Type, string ZoneCode, string DepotCode, string RegionCode, string AreaCode, string TerritoryCode, string EmpCode, DateTime Date)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"getSumDataDetail {Type},{ZoneCode},{DepotCode},{RegionCode},{AreaCode},{TerritoryCode},{EmpCode},{Date}").AsNoTracking().FirstOrDefaultAsync();

                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<IEnumerable<StockSalesChartViewModel>> StockSalesChartViewModelsSale(string ZoneCode, string DepotCode, string RegionCode, string AreaCode, string TerritoryCode, string EmpCode, string Date)
        {
            var result = new List<StockSalesChartViewModel>();
            try
            {
                result = await _context.stockSalesChartViewModels.FromSql($"getStockSaleDashBoardS {ZoneCode},{DepotCode},{RegionCode},{AreaCode},{TerritoryCode},{EmpCode},{Date}").AsNoTracking().ToListAsync();
                return result;

            }
            catch (Exception ex)
            {
                return result;
            }
        }
        public async Task<IEnumerable<AttendenceReportViewModel>> AttendenceReportViewModels(string ZoneCode, string DepotCode, string RegionCode, string AreaCode, string TerritoryCode, string EmpCode, string fromDate, string toDate)
        {
            var result = await _context.attendenceReportViewModels.FromSql($"getAttendenceData {ZoneCode},{DepotCode},{RegionCode},{AreaCode},{TerritoryCode},{EmpCode},{fromDate},{toDate}").AsNoTracking().ToListAsync();

            return result;

        }
        public async Task<JsonViewModel> getEmployeeDataForMessage(string ZoneCode, string DepotCode, string RegionCode, string AreaCode, string TerritoryCode, string EmpCode)
        {
            var result = await _context.jsonViewModels.FromSql($"getEmployeeDataForMessage {ZoneCode},{DepotCode},{RegionCode},{AreaCode},{TerritoryCode},{EmpCode}").AsNoTracking().FirstOrDefaultAsync();

            return result;

        }

        public async Task<bool> updateWeeklyPaln(string EMP_ID)
        {

            try
            {
                var result = await _context.saveScheduleViewModels.FromSql($"spUpdateEmpVisitPlan {EMP_ID}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;
            }
            catch (Exception ex)
            {
                _context.Dispose();
                return false;
            }
        }

        public async Task<bool> updateWeeklyPalnDoc(string EMP_ID)
        {

            try
            {
                var result = await _context.saveScheduleViewModels.FromSql($"spUpdateEmpVisitPlanDoc {EMP_ID}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;
            }
            catch (Exception ex)
            {
              //  _context.Dispose();
                return false;
            }
        }

        public async Task<bool> setPlanExcel(string EmpCode, string Saturday, string StartTimeSaturday, string EndTimeSaturday, string RemarksSaturday, string Sunday, string StartTimeSunDay, string EndTimeSunday, string RemarksSunday, string Monday, string StartTimeMonDay, string EndTimeMonday, string RemarksMonday, string Tuesday, string StartTimeTuesDay, string EndTimeTuesday, string RemarksTuesday, string Wednesday, string StartTimeWednesDay, string EndTimeWednesday, string RemarksWednesday, string Thursday, string StartTimeThursDay, string EndTimeThursday, string RemarksThursday, string Friday, string StartTimeFriDay, string EndTimeFriday, string RemarksFriday)
        {


            try
            {
                var result = await _context.saveScheduleViewModels.FromSql($"setWeeklyPlan {EmpCode},{Saturday},{StartTimeSaturday},{EndTimeSaturday},{RemarksSaturday},{Sunday},{StartTimeSunDay},{EndTimeSunday},{RemarksSunday},{Monday},{StartTimeMonDay},{EndTimeMonday},{RemarksMonday},{Tuesday},{StartTimeTuesDay},{EndTimeTuesday},{RemarksTuesday},{Wednesday},{StartTimeWednesDay},{EndTimeWednesday},{RemarksWednesday},{Thursday},{StartTimeThursDay},{EndTimeThursday},{RemarksThursday},{Friday},{StartTimeFriDay},{EndTimeFriday},{RemarksFriday}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;
            }
            catch (Exception ex)
            {
              //  _context.Dispose();
                return false;
            }
        }

        public async Task<bool> PlanProcess(string fromDate, string toDate, string EmpCode)
        {
            var result = new SaveScheduleViewModel();
            result = await _context.saveScheduleViewModels.FromSql($"spProcessPlan {fromDate},{toDate},{EmpCode}").AsNoTracking().FirstOrDefaultAsync();

            return result.isSuccess;

        }

        public async Task<bool> setDailyPlanDoc(string EmpCode, string DoctorCode, string day, string StartTime, string EndTime, string Remarks)
        {
          var  result = await _context.saveScheduleViewModels.FromSql($"setDailyPlanDoc {EmpCode},{DoctorCode},{day},{StartTime},{EndTime},{Remarks}").AsNoTracking().FirstOrDefaultAsync();

            return result.isSuccess;

        }

        public async Task<bool> setDailyPlanTerritory(string EmpCode, string territoryCode, string day, string StartTime, string EndTime, string Remarks)
        {
          var  result = await _context.saveScheduleViewModels.FromSql($"setDailyPlanTerritory {EmpCode},{territoryCode},{day},{StartTime},{EndTime},{Remarks}").AsNoTracking().FirstOrDefaultAsync();

            return result.isSuccess;

        }
        

        public async Task<bool> setDailyPlanChemist(string EmpCode, string DoctorCode, string day, string StartTime, string EndTime, string Remarks)
        {
          var  result = await _context.saveScheduleViewModels.FromSql($"setDailyPlanChemist {EmpCode},{DoctorCode},{day},{StartTime},{EndTime},{Remarks}").AsNoTracking().FirstOrDefaultAsync();

            return result.isSuccess;

        }

        public async Task<bool> updateDailyPlanDoc(int Id, int status)
        {
          var  result = await _context.saveScheduleViewModels.FromSql($"updateDailyPlanDoc {Id},{status}").AsNoTracking().FirstOrDefaultAsync();

            return result.isSuccess;
        }

        public async Task<bool> updateDailyPlanTerritory(int Id, int status)
        {
          var  result = await _context.saveScheduleViewModels.FromSql($"updateDailyPlanTerritory {Id},{status}").AsNoTracking().FirstOrDefaultAsync();

            return result.isSuccess;
        }

        public async Task<bool> updateEmployeeMonthlyPromoItem(int Id, decimal? amount,int monthno)
        {
          var  result = await _context.saveScheduleViewModels.FromSql($"updateEmployeeMonthlyPromoItem {Id},{amount},{monthno}").AsNoTracking().FirstOrDefaultAsync();

            return result.isSuccess;
        }

        public async Task<bool> updateDailyPlanChemist(int Id, int status)
        {
          var  result = await _context.saveScheduleViewModels.FromSql($"updateDailyPlanChemist {Id},{status}").AsNoTracking().FirstOrDefaultAsync();

            return result.isSuccess;
        }

        public async Task<bool> updateDoctorUnderObservation(int Id, int status)
        {
          var  result = await _context.saveScheduleViewModels.FromSql($"updateDoctorUnderObservation {Id},{status}").AsNoTracking().FirstOrDefaultAsync();

            return result.isSuccess;
        }
        public async Task<bool> updatePartyUnderObservation(int Id, int status,decimal? creditLimit)
        {
          var  result = await _context.saveScheduleViewModels.FromSql($"updatePartyUnderObservation {Id},{status},{creditLimit}").AsNoTracking().FirstOrDefaultAsync();

            return result.isSuccess;
        }

        public async Task<bool> updateEmployeeTADA(int Id, int status,decimal? amount, string remarks)
        {
          var  result = await _context.saveScheduleViewModels.FromSql($"updateEmployeeTADA {Id},{status},{amount},{remarks}").AsNoTracking().FirstOrDefaultAsync();

            return result.isSuccess;
        }

        public async Task<bool> setPlanDocExcel(string EmpCode, string Saturday, string StartTimeSaturday, string EndTimeSaturday, string RemarksSaturday, string Sunday, string StartTimeSunDay, string EndTimeSunday, string RemarksSunday, string Monday, string StartTimeMonDay, string EndTimeMonday, string RemarksMonday, string Tuesday, string StartTimeTuesDay, string EndTimeTuesday, string RemarksTuesday, string Wednesday, string StartTimeWednesDay, string EndTimeWednesday, string RemarksWednesday, string Thursday, string StartTimeThursDay, string EndTimeThursday, string RemarksThursday, string Friday, string StartTimeFriDay, string EndTimeFriday, string RemarksFriday)
        {


            try
            {
                var t = $"setWeeklyPlanDoc {EmpCode},{Saturday},{StartTimeSaturday},{EndTimeSaturday},{RemarksSaturday},{Sunday},{StartTimeSunDay},{EndTimeSunday},{RemarksSunday},{Monday},{StartTimeMonDay},{EndTimeMonday},{RemarksMonday},{Tuesday},{StartTimeTuesDay},{EndTimeTuesday},{RemarksTuesday},{Wednesday},{StartTimeWednesDay},{EndTimeWednesday},{RemarksWednesday},{Thursday},{StartTimeThursDay},{EndTimeThursday},{RemarksThursday},{Friday},{StartTimeFriDay},{EndTimeFriday},{RemarksFriday}";

                var result = await _context.saveScheduleViewModels.FromSql($"setWeeklyPlanDoc {EmpCode},{Saturday},{StartTimeSaturday},{EndTimeSaturday},{RemarksSaturday},{Sunday},{StartTimeSunDay},{EndTimeSunday},{RemarksSunday},{Monday},{StartTimeMonDay},{EndTimeMonday},{RemarksMonday},{Tuesday},{StartTimeTuesDay},{EndTimeTuesday},{RemarksTuesday},{Wednesday},{StartTimeWednesDay},{EndTimeWednesday},{RemarksWednesday},{Thursday},{StartTimeThursDay},{EndTimeThursday},{RemarksThursday},{Friday},{StartTimeFriDay},{EndTimeFriday},{RemarksFriday}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;
            }
            catch (Exception ex)
            {
              //  _context.Dispose();
                return false;
            }
        }

        public async Task<bool> PlanProcessDoc(string startDate, string endDate, string userId)
        {
            var result = new SaveScheduleViewModel();
            var dfgf = $"spProcessPlanDoc {startDate},{endDate},{userId}";
            result = await _context.saveScheduleViewModels.FromSql($"spProcessPlanDoc {startDate},{endDate},{userId}").AsNoTracking().FirstOrDefaultAsync();

            return result.isSuccess;

        }

        public async Task<JsonViewModelForTwoData> getDcrSummaryReport(int? userId, string ZoneId, string RegionId, string AreaId, string TerritoryID, DateTime fromDate, DateTime toDate, string reportId)
        {
            //var zoneCode = ZoneId==null?"''": ZoneId;
            //var regionCode = RegionId == null?"''": RegionId;
            //var areaCode = AreaId == null?"''": AreaId;
            //var territoryCode = TerritoryID == null?"''": TerritoryID;
            string empCode = null;
            try
            {
                
                var sql = $"DCRReport {userId},{reportId},{ZoneId},{RegionId},{AreaId},{TerritoryID},{empCode},{empCode},{fromDate}, {toDate},{1}";
                var result = await _context.jsonViewModelForTwoData.FromSql($"DCRReport {userId},{reportId},{ZoneId},{RegionId},{AreaId},{TerritoryID},{empCode},{fromDate}, {toDate},{1}").AsNoTracking().FirstOrDefaultAsync();

                return result;
            }
            catch(Exception ex)
            {
                throw ex;            
            }
            
        }
    }
}
