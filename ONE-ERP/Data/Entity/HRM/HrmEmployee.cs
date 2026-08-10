using ONEERP.Data.Entity.Common;
using ONEERP.Data.Entity.HrmMaster;
using ONEERP.Data.Entity.Salary.MasterData;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ONEERP.Data.Entity.HRM
{
    public class HrmEmployee : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int employeeId { get; set; }
        [MaxLength(30)]
        public string employeeNo { get; set; }
        public int? employeeTypeId { get; set; }
        public HrmEmployeeType employeeType { get; set; }
        public int? employeeStatusId { get; set; }
        public HrmEmployeeStatus employeeStatus { get; set; }
        [MaxLength(150)]
        public string firstName { get; set; }
        [MaxLength(150)]
        public string middleName { get; set; }
        [MaxLength(150)]
        public string lastName { get; set; }
        [MaxLength(200)]
        public string fullName { get; set; }
        [MaxLength(250)]
        public string emailId { get; set; }
        [MaxLength(250)]
        public string skypeId { get; set; }
        [MaxLength(250)]
        public string facebookId { get; set; }
        [MaxLength(250)]
        public string whatsApp { get; set; }
        [MaxLength(250)]
        public string viber { get; set; }
        [MaxLength(250)]
        public string linkedIN { get; set; }
        [MaxLength(250)]
        public string fathersName { get; set; }
        [MaxLength(250)]
        public string mothersName { get; set; }
        public int? religionId { get; set; }
        public HrmReligion religion { get; set; }
        [MaxLength(50)]
        public string mobileNo { get; set; }
        [MaxLength(50)]
        public string phoneNo { get; set; }
        public int? uniqueIdentityId { get; set; }
        public HrmUniqueIdentity uniqueIdentity { get; set; }
        public int? bloodGroupId { get; set; }
        public HrmBloodGroup bloodGroup { get; set; }
        public decimal? height { get; set; }
        public DateTime? DOB { get; set; }
        [MaxLength(50)]
        public string passportNO { get; set; }
        [MaxLength(50)]
        public string NID { get; set; }
        [MaxLength(50)]
        public string officeId { get; set; }
        public int? genderId { get; set; }
        public HrmGender gender { get; set; }
        public DateTime? effectiveDate { get; set; }
        public int? companyId { get; set; }
        public CmnCompany company { get; set; }
        [MaxLength(50)]
        public string POSTING_LOCATION { get; set; }
        [MaxLength(50)]
        public string DEPOT_CODE { get; set; }
        [MaxLength(50)]
        public string ZONE_CODE { get; set; }
        [MaxLength(50)]
        public string REGION_CODE { get; set; }
        [MaxLength(50)]
        public string AREA_CODE { get; set; }
        [MaxLength(50)]
        public string TERRITORY_CODE { get; set; }
        public DateTime? joiningDate { get; set; }
        [MaxLength(50)]
        public string maritalStatus { get; set; }
        [MaxLength(50)]
        public string drivingLicense { get; set; }
        [MaxLength(50)]
        public string tinNo { get; set; }
        public string binNo { get; set; }
        public int? sbuId { get; set; }
        public CmnSpecialBranchUnit sbu { get; set; }
        [MaxLength(250)]
        public string currentDesignation { get; set; }
        [MaxLength(250)]
        public string currentDepartment { get; set; }
        [MaxLength(50)]
        public string nationality { get; set; }
        public string salaryLocation { get; set; }
        public int? isTopManagement { get; set; }
        public string firebaseToken { get; set; }
        public bool? isSalaryActive { get; set; }
        public bool? haveVehicle { get; set; }
        public DateTime? heldupDate { get; set; }
        public DateTime? resignDate { get; set; }
        public int? HrmSalaryLocationId { get; set; }
        public HrmSalaryLocation HrmSalaryLocation { get; set; }
        public int? isDA { get; set; }

        public int? HrmDesignationId { get; set; }
        public HrmDesignation HrmDesignation { get; set; }

        public int? HrmDepartmentId { get; set; }
        public HrmDepartment HrmDepartment { get; set; }

        public int? companyBankId { get; set; }
        public CmnCompanyBank companyBank { get; set; }


        public string salaryDepot { get; set; }
        public int? probationPeriodId { get; set; }
        public CmnProbationPeriod probationPeriod { get; set; }
        public DateTime? confirmationDueDate { get; set; }
        public int? separationTypeId { get; set; }
        public CmnSeparationType separationType { get; set; }
        public DateTime? separationEffectiveDate { get; set; }
        public decimal? mobileBillLimit { get; set; }
        public int? salaryGradeId { get; set; }
        public SalaryGrade salaryGrade { get; set; }
        public int? salarySlabId { get; set; }
        public SalarySlab salarySlab { get; set; }

        public string deviceNo { get; set; }
    }
}
