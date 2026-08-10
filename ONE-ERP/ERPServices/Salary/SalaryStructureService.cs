using Microsoft.EntityFrameworkCore;
using ONEERP.Areas.Auth.Models;
using ONEERP.Areas.Salary.Models;
using ONEERP.Data;
using ONEERP.ERPServices.Salary.Interfaces;
using ONEERP.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ONEERP.ERPServices.Salary
{
    public class SalaryStructureService : ISalaryStructureService
    {
        private readonly ERPDbContext _context;

        public SalaryStructureService(ERPDbContext context)
        {
            _context = context;
        }

        #region Salary Structure

        public async Task<int> SaveSalaryEmployeeStructure(string userId, SalaryEmployeeStructureViewModel model, decimal structureAmount, int salaryHeadId)
        {
            var result = await _context.saveUpdateValueViewModels.FromSql($"SalarySpSetEmployeeStructure {userId},{model.employeeStructureId},{model.employeeId},{model.salarySlabId},{salaryHeadId},{structureAmount},{model.effectiveDate}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }
        public async Task<int> SaveSalaryBankCashStructure(string userId, SalaryEmployeeStructureViewModel model)
        {
            var result = await _context.saveUpdateValueViewModels.FromSql($"SalarySpSetEmployeeBankCshStructure {userId},{model.employeeId},{model.bankAmount},{model.cashAmount}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }
        public async Task<JsonViewModel> GetSalaryAllEmployeeStructure(int employeeId)
        {
            var result = await _context.jsonViewModels.FromSql($"SalarySpGetAllEmployeeStructure {employeeId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetSalaryEmployeeStructureByEmpId(int employeeId, string salaryHeadType)
        {
            var result = await _context.jsonViewModels.FromSql($"SalarySpGetEmployeeStructure {employeeId},{salaryHeadType}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> GetDuplicateSalaryEmployeeStructure(int employeeId)
        {
            var result = await _context.jsonViewModels.FromSql($"SalarySpGetDuplicateEmployeeStructure {employeeId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<bool> DeleteSalaryEmployeeStructureByEmpId(string userId, int employeeId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"SalarySpDeleteEmployeeStructure {userId},{employeeId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        public async Task<bool> UpdateSalaryEmployeeStructure(string userId, int employeeStructureId, decimal? amount, bool? isActive)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"SalarySpUpdateEmployeeStructure {userId},{employeeStructureId},{amount},{isActive}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        public async Task<int> SaveSalaryEmployeeFixedHeadStructure(string userId, List<SalaryEmployeeFixedHeadStructureViewModel> models)
        {
            var result = new SaveUpdateValueViewModel();
            try
            {
                foreach (var model in models)
                {
                    var str = $"SalarySpSetEmployeeFixedHeadStructure {userId},{model.EmpFixedHeadStructureId},{model.employeeId},{model.salaryPeriodId},{model.salaryHeadId},{model.structureAmount},{model.isActive}";
                    result = await _context.saveUpdateValueViewModels.FromSql($"SalarySpSetEmployeeFixedHeadStructure {userId},{model.EmpFixedHeadStructureId},{model.employeeId},{model.salaryPeriodId},{model.salaryHeadId},{model.structureAmount},{model.isActive}").AsNoTracking().FirstOrDefaultAsync();
                }
            }
            catch (System.Exception ex)
            {
                throw;
            }
            return result.isSuccess;
        }

        public async Task<int> SaveEmployeeSalaryStructureUpload(string userId, List<SalaryEmployeeStructureVerifyViewModel> models)
        {
            var result = new SaveUpdateValueViewModel();
            try
            {
                foreach (var model in models)
                {
                    //var str = $"SalarySpUploadEmployeeStructure {userId}, {model.employeeId},{model.salarySlabId},{model.salaryLocationId},{model.structureAmount},{model.isActive}";

                    DateTime joiningDate = DateTime.Parse(model.joiningdate);
                    result = await _context.saveUpdateValueViewModels.FromSql($"SalarySpUploadEmployeeStructure {userId},{model.employeeId},{model.salarySlabId},{model.salaryLocationId},{model.taxAmount},{model.structureAmount},{model.joiningdate}").AsNoTracking().FirstOrDefaultAsync();
                }
            }
            catch (System.Exception ex)
            {
                throw;
            }
            return result.isSuccess;
        }

        public async Task<int> SaveBatchWiseSerialNoUpload(string userId, List<BatchWiseSerialNoVerifyViewModel> models)
        {
            var result = new SaveUpdateValueViewModel();
            try
            {
                foreach (var model in models)
                {
                    result = await _context.saveUpdateValueViewModels.FromSql($"InvSpUploadBatchWiseSerialNo {userId},{model.productWiseSpecificationId},{model.batchNo},{model.serialNo}").AsNoTracking().FirstOrDefaultAsync();
                }
            }
            catch (System.Exception ex)
            {
                throw;
            }
            return result.isSuccess;
        }

        public async Task<JsonViewModel> CheckBatchWiseSerialNo(string serialNo)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"InvSpCheckBatchWiseSerialNo {serialNo}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<int> SaveEmployeeMobileBill(string userId, List<MobileBillVerifyViewModel> models)
        {
            var result = new SaveUpdateValueViewModel();
            try
            {
                foreach (var model in models)
                {
                    var str = $"SaveEmployeeMobileBill {userId},{model.employeeMobileBillId},{model.employeeId},{model.salaryPeriodId},{model.Limit},{model.ActualBill},{model.isActive}";
                    result = await _context.saveUpdateValueViewModels.FromSql($"SaveEmployeeMobileBill {userId},{model.employeeMobileBillId},{model.employeeId},{model.salaryPeriodId},{model.Limit},{model.ActualBill},{model.isActive}").AsNoTracking().FirstOrDefaultAsync();
                }
            }
            catch (System.Exception ex)
            {
                throw;
            }
            return result.isSuccess;
        }
        
        public async Task<int> UploadEmployeeSalaryStructure(string userId, List<SalaryEmployeeStructureVerifyViewModel> models)
        {
            var result = new SaveUpdateValueViewModel();
            try
            {
                foreach (var model in models)
                {
                    result = await _context.saveUpdateValueViewModels.FromSql($"SalarySpUploadEmployeeStructure {userId},{model.employeeId},{model.salaryGradeId},{model.salaryLocationId},{model.taxAmount},{model.structureAmount}").AsNoTracking().FirstOrDefaultAsync();
                }
            }
            catch (System.Exception ex)
            {
                throw;
            }
            return result.isSuccess;
        }

        public async Task<bool> DeleteSalaryEmployeeFixedHeadStructure(string userId, int empFixedHeadStructureId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"SalarySpDeleteEmployeeFixedHeadStructure {userId},{empFixedHeadStructureId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        public async Task<JsonViewModel> GetSalaryEmployeeFixedHeadStructureById(int empFixedHeadStructureId,int salaryPeriodIdId,int userId)
        {
            var result = await _context.jsonViewModels.FromSql($"SalarySpGetSalaryEmployeeFixedHeadStructureById {empFixedHeadStructureId},{salaryPeriodIdId},{userId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> GetEmployeeMobileBillById(int salaryPeriodIdId,int userId)
        {
            var result = await _context.jsonViewModels.FromSql($"GetEmployeeMobileBillById {salaryPeriodIdId},{userId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> GetSalaryHeadByType(string salaryHeadType)
        {
            var result = await _context.jsonViewModels.FromSql($"SalarySpGetSalaryHeadByType {salaryHeadType}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> GetSalaryFixedHeadByEmpId(int employeeId)
        {
            var result = await _context.jsonViewModels.FromSql($"SalarySpGetSalaryFixedHeadByEmpId {employeeId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<List<SalaryEmployeeFixedHeadStructureVerifyViewModel>> GetEmployeeSalaryFixedHeadUploadDataVerify(List<SalaryEmployeeFixedHeadStructureVerifyViewModel> models)
        {
            JsonViewModel jr = new JsonViewModel();
            foreach (var m in models)
            {
                try
                {
                    var result = await _context.jsonViewModels.FromSql($"SalarySpGetEmployeeSalaryFixedHeadVerify {m.employeeNo},{m.salaryHead}").AsNoTracking().FirstOrDefaultAsync();
                    //[{"status":"0:Invalid employee code; Invalid salary head"}]
                    string[] res = result.data.Replace("[{\"status\":\"","").Replace("\"}]", "").Split(":");

                    int.TryParse(res[0], out int eid);
                    int.TryParse(res[1], out int shid);

                    m.employeeId = eid;
                    m.salaryHeadId = shid;
                    m.status = res[2];
                }
                catch (System.Exception ex)
                {
                    throw ex;
                }
            }
            return models;
        }
        
        public async Task<List<SalaryEmployeeStructureVerifyViewModel>> GetEmployeeSalaryStructureUploadDataVerify(List<SalaryEmployeeStructureVerifyViewModel> models)
        {
            JsonViewModel jr = new JsonViewModel();
            foreach (var m in models)
            {
                try
                {
                    var result = await _context.jsonViewModels.FromSql($"SalarySpGetEmployeeSalaryStructureVerify {m.employeeNo},{m.salaryLocation},{m.salaryGrade},{m.joiningdate}").AsNoTracking().FirstOrDefaultAsync();
                    string[] res = result.data.Replace("[{\"status\":\"","").Replace("\"}]", "").Split(":");

                    int.TryParse(res[0], out int eid);
                    int.TryParse(res[2], out int locId);
                    int.TryParse(res[3], out int gradeId);
                    int.TryParse(res[3], out int slabId);

                    m.employeeId = eid;
                    m.employeeName = res[1];
                    m.salaryLocationId = locId;
                    m.salaryGradeId = gradeId;
                    m.salarySlabId = slabId;
                    m.status = res[4];
                }
                catch (System.Exception ex)
                {
                    throw ex;
                }
            }
            return models;
        }

        public async Task<List<BatchWiseSerialNoVerifyViewModel>> GetBatchWiseSerialNoUploadDataVerify(List<BatchWiseSerialNoVerifyViewModel> models)
        {
            JsonViewModel jr = new JsonViewModel();
            foreach (var m in models)
            {
                try
                {
                    var result = await _context.jsonViewModels.FromSql($"InvSpGetBatchWiseSerialNoVerify {m.skuNumber},{m.serialNo}").AsNoTracking().FirstOrDefaultAsync();
                    string[] res = result.data.Replace("[{\"status\":\"", "").Replace("\"}]", "").Split(":");

                    int.TryParse(res[0], out int specid);

                    m.productWiseSpecificationId = specid;
                    m.status = res[1];
                }
                catch (System.Exception ex)
                {
                    throw ex;
                }
            }
            return models;
        }

        public async Task<List<MobileBillVerifyViewModel>> GetMobileBillUploadDataVerify(List<MobileBillVerifyViewModel> models)
        {
            JsonViewModel jr = new JsonViewModel();
            foreach (var m in models)
            {
                try
                {
                    var result = await _context.jsonViewModels.FromSql($"GetMobileBillUploadDataVerify {m.employeeNo},{m.MobileNumber}").AsNoTracking().FirstOrDefaultAsync();
                    //[{"status":"0:Invalid employee code; Invalid salary head"}]
                    string[] res = result.data.Replace("[{\"status\":\"", "").Replace("\"}]", "").Split(":");


                    m.employeeId = Convert.ToInt32(res[0]);
                    m.employeeName = res[1];
                    m.employeeNo = res[2];
                    m.status = res[3];
                }
                catch (System.Exception ex)
                {

                }
            }
            return models;
        }

        public async Task<List<VoucherUploadVerifyViewModel>> GetVoucherUploadDataVerify(List<VoucherUploadVerifyViewModel> models)
        {
            JsonViewModel jr = new JsonViewModel();
            foreach (var m in models)
            {
                try
                {
                    var result = await _context.jsonViewModels.FromSql($"AccountCodePartyCostCentreVerify {m.accountCode},{m.party},{m.costCentre}").AsNoTracking().FirstOrDefaultAsync();
                    //[{"status":"0:Invalid employee code; Invalid salary head"}]
                    string[] res = result.data.Replace("[{\"status\":\"","").Replace("\"}]", "").Split(":");

                    int.TryParse(res[0], out int eid);
                    int.TryParse(res[1], out int shid);
                    int.TryParse(res[2], out int ccid);

                    m.ledgerId = eid;
                    m.accountName = res[3];
                    m.party = res[4];
                    m.partyId = shid;
                    m.costcentreId = ccid;
                    m.status = res[5];
                }
                catch (System.Exception ex)
                {

                }
            }
            return models;
        }

        public async Task<int> UpdateEmployeeDesignationAndDepartment(string userId, SalaryEmployeeStructureViewModel model)
        {
            try
            {
                var employee = await _context.HrmEmployee.FindAsync(model.employeeId);
                if (!(employee is null))
                {
                    employee.currentDepartment = model.department;
                    employee.currentDesignation = model.designation;
                    employee.salaryGradeId = model.salaryGradeId;
                    employee.salarySlabId = model.salarySlabId;
                    employee.updatedAt = DateTime.Now;
                    employee.updatedBy = userId;
                }
                _context.HrmEmployee.Update(employee);
                return await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                return 0;
            }


        }

        #endregion

        #region Salary Process

        public async Task<bool> ProcessEmployeesSalary(string userId, int salaryPeriodId)
        {
            try
            {
                var result = await _context.saveUpdateViewModels.FromSql($"SalarySpEmployeeProcessSalary {userId},{salaryPeriodId}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        public async Task<bool> SaveSalaryProcessLog(string userId, SalaryEmployeeProcessViewModel model)
        {
            try
            {
                var result = await _context.saveUpdateViewModels.FromSql($"SalarySpSetSalaryProcessLog {userId},{model.salaryPeriodId},{model.processName},{model.processComments},{model.ipAddress}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;

            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            
        }

        public async Task<JsonViewModel> GetSalaryMasterByPeriodId(int salaryPeriodId,int userId, string salaryDepotName="")
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"SalarySpGetSalaryMaster {salaryPeriodId},{userId},{salaryDepotName}").AsNoTracking().FirstOrDefaultAsync();
               
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }

        #endregion


        #region Mio Target

        public async Task<List<MioSalesTargetViewModel>> GetMioSalesTargetUploadDataVerify(List<MioSalesTargetViewModel> models)
        {
            JsonViewModel jr = new JsonViewModel();
            foreach (var m in models)
            {
                try
                {
                    var result = await _context.jsonViewModels.FromSql($"SalarySpGetMIOTargetUploadVerify {m.skuNumber}").AsNoTracking().FirstOrDefaultAsync();
                    //[{"status":"0:Invalid employee code; Invalid salary head"}]
                    string[] res = result.data.Replace("[{\"status\":\"", "").Replace("\"}]", "").Split(":");

                    int.TryParse(res[0], out int eid);
                    int.TryParse(res[1], out int shid);

                    m.productWiseSpecificationId = eid;
                    m.targetvalue = shid * m.CtnQty;
                    m.status = res[2];
                    m.CtnQty = m.CtnQty;
                    m.targetQty = m.targetQty;
                    m.skuNumber = m.skuNumber;
                    m.productName = m.productName;

                }
                catch (System.Exception ex)
                {

                }
            }
            return models;
        }

        public async Task<int> SaveMioItemWiseSalesTarget(string userId, MioSalesTargetMasterViewModel models)
        {
            var result = new SaveUpdateValueViewModel();
            try
            {
                result = await _context.saveUpdateValueViewModels.FromSql($"SaveMioItemWiseSalesTarget {userId},{models.salMIOSalesTargetMasterId},{models.depotCode},{models.territoryCode},{models.startDate},{models.endDate},{models.isActive}").AsNoTracking().FirstOrDefaultAsync();

                foreach (var model in models.lstMaster)
                {
                    await _context.saveUpdateValueViewModels.FromSql($"SaveMioItemWiseSalesTargetDetails {userId},{result.isSuccess},{model.productWiseSpecificationId},{model.CtnQty},{model.targetvalue},{model.isActive}").AsNoTracking().FirstOrDefaultAsync();
                }
            }
            catch (System.Exception ex)
            {
                throw;
            }
            return 1;
        }

        public async Task<JsonViewModel> GetMiosalestargetmasterById(int targetMasterId)
        {
            var result = await _context.jsonViewModels.FromSql($"SpGetMiosalestargetmasterById {targetMasterId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> GetMiosalestargetmasterwithdetailsById(int targetMasterId)
        {
            var result = await _context.jsonViewModels.FromSql($"SpGetMiosalestargetmasterwithdetailsById {targetMasterId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<bool> DeleteMioItemWiseSalesTarget(string userId, int targetId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"SalSpDeleteMioItemWiseSalesTarget {userId},{targetId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        #endregion Mio Target

        #region increment
        public async Task<List<SalaryEmployeeIncrementVerifyViewModel>> GetEmployeeIncrementUploadDataVerify(List<SalaryEmployeeIncrementVerifyViewModel> models)
        {
            JsonViewModel jr = new JsonViewModel();
            foreach (var m in models)
            {
                try
                {
                    var result = await _context.jsonViewModels.FromSql($"SalarySpGetEmployeeSalaryIncrementVerify {m.employeeNo},{m.salaryGrade}, {m.increment}").AsNoTracking().FirstOrDefaultAsync();
                    string[] res = result.data.Replace("[{\"status\":\"", "").Replace("\"}]", "").Split(":");

                    int.TryParse(res[0], out int eid);
                    int.TryParse(res[4], out int locId);
                    int.TryParse(res[5], out int slabId);
                    decimal.TryParse(res[2], out decimal structureAmount);
                    decimal.TryParse(res[3], out decimal grossSalary);
                    m.employeeId = eid;
                    m.employeeName = res[1];
                    m.salarySlabId = slabId;
                    m.structureAmount = structureAmount;
                    m.status = res[6];
                    m.grossSalary = grossSalary;
                }
                catch (System.Exception ex)
                {
                    throw ex;
                }
            }
            return models;
        }

        public async Task<int> SaveEmployeeSalaryIncrementUpload(string userId, List<SalaryEmployeeIncrementVerifyViewModel> models)
        {
            var result = new SaveUpdateValueViewModel();
            try
            {
                foreach (var model in models)
                {
                    result = await _context.saveUpdateValueViewModels.FromSql($"SalarySpUploadEmployeeIncrement {userId},{0},{model.employeeId},{model.grossSalary},{model.structureAmount},{model.increment},{model.salarySlabId},{model.taxAmount}").AsNoTracking().FirstOrDefaultAsync();
                }
            }
            catch (System.Exception ex)
            {
                throw;
            }
            return result.isSuccess;
        }
        #endregion
    }
}
