namespace ONEERP.Areas.Hrm.Models
{
    public class EmployeeAddressViewModel
    {
        public int employeeAddressId { get; set; }
        public int? employeeId { get; set; }
        public int? addressTypeId { get; set; }       
        public int? countryId { get; set; }
        public int? divisionId { get; set; }
        public int? districtId { get; set; }
        public int? thanaId { get; set; }
        public string address { get; set; }
        public bool? isActive { get; set; }
    }
}
