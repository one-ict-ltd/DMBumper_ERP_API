using System;
using System.ComponentModel.DataAnnotations;

namespace ONEERP.Areas.Hrm.Models
{
    public class EmployeeInformationViewModel
    {
        public int employeeId { get; set; }
        public string employeeNo { get; set; }
        public int? employeeTypeId { get; set; }
        public string firstName { get; set; }
        public string middleName { get; set; }
        public string lastName { get; set; }
        public string fullName { get; set; }
        public string emailId { get; set; }
        public string skypeId { get; set; }
        public string facebookId { get; set; }
        public string whatsApp { get; set; }
        public string viber { get; set; }
        public string linkedIN { get; set; }
        public string fathersName { get; set; }
        public string mothersName { get; set; }
        public int? employeeStatusId { get; set; }
        public int? bloodGroupId { get; set; }
        public int? religionId { get; set; }
        public string mobileNo { get; set; }
        public string phoneNo { get; set; }
        public int? uniqueIdentityId { get; set; }
        public decimal? height { get; set; }
        [DataType(DataType.Date)]
        public DateTime? DOB { get; set; }
        public string passportNO { get; set; }
        public string NID { get; set; }
        public string binNo { get; set; }
        public int? genderId { get; set; }
        public string officeId { get; set; }
        public DateTime? effectiveDate { get; set; }
        public int? companyId { get; set; }
        


        public DateTime? joiningDate { get; set; }
        public string maritalStatus { get; set; }
        public string drivingLicense { get; set; }
        public string tinNo { get; set; }
        public int? sbuId { get; set; }
        public string currentDesignation { get; set; }
        public string currentDepartment { get; set; }
        public string nationality { get; set; }
        public bool? isSalaryActive { get; set; }
        public bool? haveVehicle { get; set; }

        public string zoneId { get; set; }
        public string depoId { get; set; }
        public string regionId { get; set; }
        public string areaId { get; set; }
        public string territoryId { get; set; }
        public string postingLocation { get; set; }
        public string salaryLocation { get; set; }
        public int? isTopManagement { get; set; }
        public DateTime? heldUpDate { get; set; }
        public int? companyBankId { get; set; }
        public string salaryDepotId { get; set; }
        public int? probationPeriodId { get; set; }
        public DateTime? confirmationDate { get; set; }
        public int? separationTypeId { get; set; }
        public DateTime? separationEffectiveDate { get; set; }
        public int? salaryGradeId { get; set; }
        public int? salarySlabId { get; set; }
        public string password { get; set; }
        public string deviceNo { get; set; }

    }
    public class EmployeeFireBaseViewModel
    {
        public int employeeId { get; set; }
        public string firebaseToken { get; set; }
    }

    public class EmployeeOtherExpenseViewModel
    {
        public int? otherExpenseId { get; set; }

        public int? employeeId { get; set; }
        public int? fiscalYearId { get; set; }

        public decimal? amount { get; set; }

        public string remarks { get; set; }
        public string monthName { get; set; }
    }

    public class EmployeeTransferViewModel
    {
        public int employeeTransferId { get; set; }
        public int? employeeId { get; set; }
        public DateTime? transferDate { get; set; }
        public int? HrmSalaryLocationId { get; set; }
        public int? HrmNewSalaryLocationId { get; set; }
        public decimal? grossSalary { get; set; }
        public int? status { get; set; }
        public string remarks { get; set; }

    }

    public class EmployeePromotionViewModel
    {
        public int employeePromotionId { get; set; }
        public int? employeeId { get; set; }
        public DateTime? promotionDate { get; set; }
        public int? HrmSalaryLocationId { get; set; }
        public int? HrmNewSalaryLocationId { get; set; }
        public int? prevSalaryGradeId { get; set; }
        public int? NewGradeId { get; set; }
        public int? prevSalarySlabId { get; set; }
        public int? NewSalarySlabId { get; set; }
        public string previousDesignation { get; set; }
        public string currentDesignation { get; set; }
        public string previousDepartment { get; set; }
        public string currentDepartment { get; set; }
        public decimal? PreviousGrossSalary { get; set; }
        public decimal? NewGrossSalary { get; set; }
        public decimal? incrementSalary { get; set; }
        public int? status { get; set; }
        public string remarks { get; set; }
        public string type { get; set; }

    }


}
