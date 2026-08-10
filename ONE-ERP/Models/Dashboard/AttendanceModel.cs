namespace ONEERP.Models.Dashboard
{
    public class AttendanceModel
    {
     
        public string EMPID { get; set; }
        public string EMPLOYEENAME { get; set; }
        public string POSTINGLOCATION { get; set; }
        public string ZoneName { get; set; }
        public string DepotName { get; set; }
        public string RegionName { get; set; }
        public string AreaName { get; set; }
        public string TerritoryName { get; set; }
        public string InTime { get; set; }
        public string OutTime { get; set; }
        public string LateTime { get; set; }
        public string Duration { get; set; }
        public string  Address { get; set; }
       
    }

    public class AttendenceReportViewModel
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string DateTime { get; set; }
        public string ZoneName { get; set; }
        public string DepotName { get; set; }
        public string RegionName { get; set; }
        public string AreaName { get; set; }
        public string TerritoryName { get; set; }

        public string PunchIn { get; set; }
        public string PunchOut { get; set; }
        public string Duration { get; set; }
        public string punchinLocation { get; set; }
        public string punchoutLocation { get; set; }
        public string Status { get; set; }
        public string LateTime { get; set; }
        public string ZoneCode { get; set; }
        public string DepotCode { get; set; }
        public string RegionCode { get; set; }
        public string AreaCode { get; set; }
        public string TerritoryCode { get; set; }
    }
}
