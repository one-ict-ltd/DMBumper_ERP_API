using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Salary.Models
{
    public class SalaryEmployeeFixedHeadStructureViewModel
    {
        public int EmpFixedHeadStructureId { get; set; }
        public int? employeeId { get; set; }
        public int? salaryPeriodId { get; set; }
        public int? salaryHeadId { get; set; }
        public decimal? structureAmount { get; set; }
        public bool? isActive { get; set; }
    }
    public class SalaryEmployeeFixedHeadStructureVerifyViewModel
    {
        public int EmpFixedHeadStructureId { get; set; }
        public int? employeeId { get; set; }
        public int? salaryPeriodId { get; set; }
        public int? salaryHeadId { get; set; }
        public decimal? structureAmount { get; set; }
        public bool? isActive { get; set; }

        //start Optional property
        public string employeeNo { get; set; }
        public string employeeName { get; set; }
        public string salaryHead { get; set; }
        public string salaryPeriod { get; set; }
        public string status { get; set; }
        //end
    }

    public class SalaryEmployeeStructureVerifyViewModel
    {
        public int EmpFixedHeadStructureId { get; set; }
        public int EmpSalaryStructureId { get; set; }
        public int? employeeId { get; set; }
        public int? salaryGradeId { get; set; }
        public string joiningdate { get; set; }
        public int? salaryLocationId { get; set; }
        public int? salarySlabId { get; set; }
        public decimal? structureAmount { get; set; }
        public decimal? taxAmount { get; set; }
        public bool? isActive { get; set; }

        //start Optional property
        public string employeeNo { get; set; }
        public string employeeName { get; set; }
        public string salaryLocation { get; set; }
        public string salaryGrade { get; set; }
        public string salaryPeriod { get; set; }
        public string status { get; set; }
        //end
    }

    public class MobileBillVerifyViewModel
    {
        public int employeeMobileBillId { get; set; }
        public int? employeeId { get; set; }
        public int? salaryPeriodId { get; set; }
        public int? salaryHeadId { get; set; }
        public decimal? Limit { get; set; }
        public decimal? ActualBill { get; set; }
        public bool? isActive { get; set; }

        //start Optional property
        public string employeeNo { get; set; }
        public string MobileNumber { get; set; }
        public string employeeName { get; set; }
        public string salaryHead { get; set; }
        public string salaryPeriod { get; set; }
        public string status { get; set; }
        //end
    }
    public class VoucherUploadVerifyViewModel
    {
        public int VoucherMasterId { get; set; }
        public int? employeeId { get; set; }
        public int? ledgerId { get; set; }
        public int? partyId { get; set; }
        public int? costcentreId { get; set; }
        public bool? isActive { get; set; }

        //start Optional property
        public string accountCode { get; set; }
        public string accountName { get; set; }
        public string party { get; set; }
        public string costCentre { get; set; }
        public decimal? drAmount { get; set; }
        public decimal? crAmount { get; set; }
        public string remarks { get; set; }
        public string status { get; set; }
        //end
    }

    public class MioSalesTargetViewModel
    {
        public int targetDetailId { get; set; }
        public bool? isActive { get; set; }

        //start Optional property
        public int? salMIOSalesTargetMasterId { get; set; }
        public int? productWiseSpecificationId { get; set; }
        public decimal? targetQty { get; set; }
        public decimal? targetvalue { get; set; }
        public decimal? looseQty { get; set; }
        public decimal? CtnQty { get; set; }
        public string skuNumber { get; set; }
        public string productName { get; set; }
        public string status { get; set; }
        //end
    }


    public class MioSalesTargetMasterViewModel
    {
        public int? salMIOSalesTargetMasterId { get; set; }
        public string depotCode { get; set; }
        public string territoryCode { get; set; }
        public DateTime? startDate { get; set; }
        public DateTime? endDate { get; set; }
        public bool? isActive { get; set; }
        public List<MioSalesTargetViewModel> lstMaster { get; set; }
    }
    public class BatchWiseSerialNoVerifyViewModel
    {
        public int batchWiseSerialNoId { get; set; }
        public int? productWiseSpecificationId { get; set; }
        public string batchNo { get; set; }
        public string serialNo { get; set; }
        public bool? isChecked { get; set; }

        //start Optional property
        public string skuNumber { get; set; }
        public string status { get; set; }
        public bool? isActive { get; set; }
        //end
    }
}
