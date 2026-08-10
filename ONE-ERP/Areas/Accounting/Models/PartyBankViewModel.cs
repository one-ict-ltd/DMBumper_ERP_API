namespace ONEERP.Areas.Accounting.Models
{
    public class PartyBankViewModel
    {
        public int? partyBankId { get; set; }
        public int? partyId { get; set; }        
        public int? bankId { get; set; }        
        public string bankAccName { get; set; }        
        public string bankAccNo { get; set; }        
        public string bankBranchName { get; set; }
    }
}
