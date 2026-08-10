using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ONEERP.Data.Entity.FieldForceTracking
{
    public class CmnMIOCurrentLocations
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LocationID { get; set; }
        public string MIOCode { get; set; }
        public string MIOName { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string LLAddress { get; set; }
        public DateTime? DateTime { get; set; }
        public int? MIOId { get; set; }
        public int? TerritoryID { get; set; }
        public string userId { get; set; }
        public int? iscurrent { get; set; }

    }

	public class CmnWeekendDay
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		public string EMP_ID { get; set; }
		public int? SaturDay { get; set; }
		public int? SunDay { get; set; }
		public int? MonDay { get; set; }
		public int? TuesDay { get; set; }
		public int? WednesDay { get; set; }
		public int? ThrusDay { get; set; }
		public int? FriDay { get; set; }
		public string CreateBy { get; set; }
		public DateTime? CreateOn { get; set; }
		public string CreatePc { get; set; }
		public int? UpdateBy { get; set; }
		public DateTime? UpdateOn { get; set; }
		public string UpdatePc { get; set; }
		public int IsDeleted { get; set; }
		public int IsActive { get; set; }
		public int? DeleteBy { get; set; }
		public DateTime? DeleteOn { get; set; }
		public string DeletePc { get; set; }
	}
}
