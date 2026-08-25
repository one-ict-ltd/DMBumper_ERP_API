using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ONEERP.Areas.Accounting.Models;
using ONEERP.Areas.Auth.Models;
using ONEERP.Areas.FieldForceTracking.Models;
using ONEERP.Areas.Hrm.Models;
using ONEERP.Areas.Inventory.Models;
using ONEERP.Areas.MasterData.Models;
using ONEERP.Areas.Salary.Models;

//using ONEERP.Areas.Sales.Models;
using ONEERP.Areas.Schedule.Models;
using ONEERP.Data.Entity;
using ONEERP.Data.Entity.Accounting;
using ONEERP.Data.Entity.Auth;
using ONEERP.Data.Entity.Common;
using ONEERP.Data.Entity.DigitalGift;
using ONEERP.Data.Entity.FieldForceTracking;
using ONEERP.Data.Entity.HRM;
using ONEERP.Data.Entity.HrmMaster;
using ONEERP.Data.Entity.Inventory;
using ONEERP.Data.Entity.Production;
using ONEERP.Data.Entity.PromoInventory;
using ONEERP.Data.Entity.Purchase;
using ONEERP.Data.Entity.Salary.MasterData;
using ONEERP.Data.Entity.Salary.SalaryProcess;
using ONEERP.Data.Entity.Salary.TaxProcess;
using ONEERP.Data.Entity.Sales;
using ONEERP.Data.Entity.TaskManagement;
using ONEERP.Models;
using ONEERP.Models.Dashboard;
using ONEICT.Areas.Schedule.Models;

namespace ONEERP.Data
{
    public class ERPDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ERPDbContext(DbContextOptions<ERPDbContext> options, IHttpContextAccessor _httpContextAccessor) : base(options)
        {
            this._httpContextAccessor = _httpContextAccessor;
            //Database.SetCommandTimeout(150000);
        }

        #region ERP User Manage Old

        public DbQuery<NavbarOldViewModel> navbarViewModels { get; set; }
        public DbQuery<UserAccessPageViewModel> userAccessPageViewModels { get; set; }
        public DbQuery<UserLoginViewModel> userLoginViewModels { get; set; }
        public DbQuery<AspNetUsersViewModel> aspNetUsersViews { get; set; }
        public DbQuery<AspNetUsersApproverViewModel> aspNetUsersApproverViews { get; set; }
        public DbQuery<RegisterViewModel> registerViewModels { get; set; }
        public DbQuery<UserAccessPageListViewModel> userAccessPageListViewModels { get; set; }
        public DbQuery<AspNetUsersProfileViewModel> aspNetUsersProfileViewModels { get; set; }
        public DbQuery<LoginInfoDataViewModel> loginInfoDataViewModels { get; set; }
        public DbSet<Navbar> Navbars { get; set; }
        public DbSet<ERPModule> ERPModules { get; set; }
        public DbSet<UserAccessPage> UserAccessPages { get; set; }
        #endregion

        #region ERP Common DBSet/Query

        #region Common DBSet -------------
        public DbSet<CmnCompany> CmnCompany { get; set; }
        public DbSet<CmnCompanyCategory> CmnCompanyCategory { get; set; }
        public DbSet<CmnDropDown> CmnDropDown { get; set; }
        public DbSet<CmnDropDownType> CmnDropDownType { get; set; }
        public DbSet<CmnHelpDetail> CmnHelpDetail { get; set; }
        public DbSet<CmnHelpImage> CmnHelpImage { get; set; }
        public DbSet<CmnHelpMaster> CmnHelpMaster { get; set; }
        public DbSet<CmnHelpMulti> CmnHelpMulti { get; set; }
        public DbSet<CmnMenuPermission> CmnMenuPermission { get; set; }
        public DbSet<CmnMenus> CmnMenus { get; set; }
        public DbSet<CmnMenuTypes> CmnMenuTypes { get; set; }
        public DbSet<CmnModule> CmnModule { get; set; }
        public DbSet<CmnModulePermissions> CmnModulePermissions { get; set; }
        public DbSet<CmnOriginCountry> CmnOriginCountry { get; set; }
        public DbSet<CmnSpecialBranchUnit> CmnSpecialBranchUnit { get; set; }
        public DbSet<CmnStore> CmnStore { get; set; }
        public DbSet<CmnStoreType> CmnStoreType { get; set; }
        public DbSet<CmnUserAccessPage> CmnUserAccessPage { get; set; }
        public DbSet<CmnUserGroup> CmnUserGroup { get; set; }
        public DbSet<CmnUserLoginInfo> CmnUserLoginInfo { get; set; }
        public DbSet<CmnUserPermissionGroup> CmnUserPermissionGroup { get; set; }
        public DbSet<CmnUserWiseCompany> CmnUserWiseCompany { get; set; }
        public DbSet<CmnReportType> CmnReportType { get; set; }
        public DbSet<CmnReport> CmnReport { get; set; }
        public DbSet<CmnReportPermission> CmnReportPermission { get; set; }
        public DbSet<CmnDistricts> CmnDistricts { get; set; }
        public DbSet<CmnDivisions> CmnDivisions { get; set; }
        public DbSet<CmnThanas> CmnThanas { get; set; }
        public DbSet<CmnMunicipilityLocation> CmnMunicipilityLocation { get; set; }
        public DbSet<CmnApprovalType> CmnApprovalType { get; set; }
        public DbSet<CmnApproverType> CmnApproverType { get; set; }
        public DbSet<CmnApprovalMatrix> CmnApprovalMatrix { get; set; }
        public DbSet<CmnApprovalLog> CmnApprovalLog { get; set; }
        public DbSet<CmnBankType> CmnBankType { get; set; }
        public DbSet<CmnBank> CmnBank { get; set; }
        public DbSet<CmnAutoStockInOutSetting> CmnAutoStockInOutSetting { get; set; }
        public DbSet<CmnTransactionType> CmnTransactionType { get; set; }
        public DbSet<CmnCompanyBank> CmnCompanyBank { get; set; }
        public DbSet<CmnProbationPeriod> CmnProbationPeriod { get; set; }
        public DbSet<CmnSeparationType> CmnSeparationType { get; set; }
        public DbSet<CmnMenuWiseTransactionDateUnlock> CmnMenuWiseTransactionDateUnlock { get; set; }
        #endregion

        #region ERP Master Query
        public DbQuery<CompanyListViewModel> companyListViewModels { get; set; }
        public DbQuery<SBUListViewModel> sbuListViewModels { get; set; }
        public DbQuery<HelpMasterListViewModel> helpMasterListViewModels { get; set; }
        public DbQuery<HelpDetailListViewModel> helpDetailListViewModels { get; set; }
        public DbQuery<HelpImageListViewModel> helpImageListViewModels { get; set; }
        public DbQuery<HelpMultiListViewModel> helpMultiListViewModels { get; set; }
        public DbQuery<SaveUpdateViewModel> saveUpdateViewModels { get; set; }
        public DbQuery<SaveUpdateValueViewModel> saveUpdateValueViewModels { get; set; }
        public DbQuery<JsonViewModel> jsonViewModels { get; set; }
        public DbQuery<JsonViewModel3> jsonViewModels3 { get; set; }
        public DbQuery<JsonViewModel4> jsonViewModels4 { get; set; }
        public DbQuery<JsonViewModelForTwoData> jsonViewModelForTwoData { get; set; }
        public DbQuery<SalesBatchsViewModel> salesBatchViewModels { get; set; }
        public DbQuery<EmployeeInfoViewModelcs> employeeInfoViewModelcs { get; set; }
        public DbQuery<EmployeeListViewModelDropdown> employeeListViewModelDropdown { get; set; }
        public DbQuery<ONEERP.Areas.Sales.Models.PartyVM> partyModel { get; set; }
        #endregion

        #endregion

        #region Accounting DBSet/Query

        #region Accounting DBSet ----------------------

        public DbSet<AccAccountGroup> AccAccountGroup { get; set; }
        public DbSet<AccAutoVoucherDetail> AccAutoVoucherDetail { get; set; }
        public DbSet<AccAutoVoucherMaster> AccAutoVoucherMaster { get; set; }
        public DbSet<AccAutoVoucherName> AccAutoVoucherName { get; set; }
        public DbSet<AccBudgetDetails> AccBudgetDetails { get; set; }
        public DbSet<AccBudgetHeadDetails> AccBudgetHeadDetails { get; set; }
        public DbSet<AccBudgetHeadMaster> AccBudgetHeadMaster { get; set; }
        public DbSet<AccBudgetMainHead> AccBudgetMainHead { get; set; }
        public DbSet<AccBudgetMaster> AccBudgetMaster { get; set; }
        public DbSet<AccBudgetSubHead> AccBudgetSubHead { get; set; }
        public DbSet<AccChequeBookDetails> AccChequeBookDetails { get; set; }
        public DbSet<AccChequeBookMaster> AccChequeBookMaster { get; set; }
        public DbSet<AccCostCentre> AccCostCentre { get; set; }
        public DbSet<AccCostCentreAllocation> AccCostCentreAllocation { get; set; }
        public DbSet<AccCostCentreBranchMapping> AccCostCentreBranchMapping { get; set; }
        public DbSet<AccCurrency> AccCurrency { get; set; }
        public DbSet<AccFiscalYear> AccFiscalYear { get; set; }
        public DbSet<AccFundSource> AccFundSource { get; set; }
        public DbSet<AccGroupNature> AccGroupNature { get; set; }
        public DbSet<AccLedgers> AccLedgers { get; set; }
        public DbSet<AccLedgerType> AccLedgerType { get; set; }
        public DbSet<AccNoteDetails> AccNoteDetails { get; set; }
        public DbSet<AccNoteMaster> AccNoteMaster { get; set; }
        public DbSet<AccOpeningBalance> AccOpeningBalance { get; set; }
        public DbSet<AccParty> AccParty { get; set; }
        public DbSet<AccPartyObservation> AccPartyObservation { get; set; }
        public DbSet<AccPartyAddress> AccPartyAddress { get; set; }
        public DbSet<AccPartyContact> AccPartyContact { get; set; }
        public DbSet<AccTransactionMode> AccTransactionMode { get; set; }
        public DbSet<AccVisaGroup> AccVisaGroup { get; set; }
        public DbSet<AccVisaSales> AccVisaSales { get; set; }
        public DbSet<AccVisaWorkOrder> AccVisaWorkOrder { get; set; }
        public DbSet<AccVoucherApprovalLog> AccVoucherApprovalLog { get; set; }
        public DbSet<AccVoucherDetails> AccVoucherDetails { get; set; }
        public DbSet<AccVoucherMasters> AccVoucherMasters { get; set; }
        public DbSet<AccVoucherStatus> AccVoucherStatus { get; set; }
        public DbSet<AccVoucherTypes> AccVoucherTypes { get; set; }
        public DbSet<AccPartyBank> AccPartyBank { get; set; }
        public DbSet<AccPartyShareholder> AccPartyShareholder { get; set; }
        public DbSet<AccPartyDistributor> AccPartyDistributor { get; set; }
        public DbSet<AccPartyNominee> AccPartyNominee { get; set; }
        public DbSet<AccNoteParent> AccNoteParent { get; set; }
        public DbSet<AccCostSheetParentHead> AccCostSheetParentHead { get; set; }
        public DbSet<AccCostSheetHead> AccCostSheetHead { get; set; }
        public DbSet<AccCostSheetHeadAmount> AccCostSheetHeadAmount { get; set; }
        public DbSet<AccFormulaType> AccFormulaType { get; set; }
        public DbSet<AccCurrentYearBalanceSheet> AccCurrentYearBalanceSheet { get; set; }
        public DbSet<AccPreviousYearBalanceSheet> AccPreviousYearBalanceSheet { get; set; }
        public DbSet<AccCostCenterCategory> AccCostCenterCategory { get; set; }
        public DbSet<AccCostCenterLocation> AccCostCenterLocation { get; set; }
        public DbSet<AccUserWiseLedger> AccUserWiseLedger { get; set; }
        public DbSet<AccVoucherAttachment> AccVoucherAttachments { get; set; }
        #endregion

        #region Account Master Data Query
        public DbQuery<CurrencyListViewModel> currencyListViewModels { get; set; }
        public DbQuery<FundSourceListViewModel> fundSourceListViewModels { get; set; }
        public DbQuery<CostCentreListViewModel> costCentreListViewModels { get; set; }
        public DbQuery<LedgerTypeListViewModel> ledgerTypeListViewModels { get; set; }
        public DbQuery<TransactionModeListViewModel> transactionModeListViewModels { get; set; }
        public DbQuery<GroupNatureListViewModel> groupNatureListViewModels { get; set; }
        public DbQuery<AccountGroupListViewModel> accountGroupListViewModels { get; set; }
        public DbQuery<VoucherTypeListViewModel> voucherTypeListViewModels { get; set; }
        public DbQuery<VoucherStatusListViewModel> voucherStatusListViewModels { get; set; }
        public DbQuery<LedgersListViewModel> ledgersListViewModels { get; set; }
        public DbQuery<PartyListViewModel> partyListViewModels { get; set; }
        public DbQuery<OpeningBalanceListViewModel> openingBalanceListViewModels { get; set; }
        //public DbQuery<DuplicateCountViewModel> duplicateCountViewModels { get; set; }
        public DbQuery<LedgersForVoucherViewModel> ledgersForVoucherViewModels { get; set; }
        #endregion

        #region Voucher Master Query
        public DbQuery<CostCentreAllocationListViewModel> costCentreAllocationListViewModels { get; set; }
        public DbQuery<VoucherMasterListViewModel> voucherMasterListViewModels { get; set; }
        public DbQuery<VoucherDetailListViewModel> voucherDetailListViewModels { get; set; }
        public DbQuery<VoucherApprovalLogListViewModel> voucherApprovalLogListViewModels { get; set; }
        #endregion

        #endregion

        #region Inventory DBSet/Query

        #region Inventory DBSet-------------
        public DbSet<InvCurrentStock> InvCurrentStock { get; set; }
        public DbSet<InvProduct> InvProduct { get; set; }
        public DbSet<InvProductBrand> InvProductBrand { get; set; }
        public DbSet<InvProductCategory> InvProductCategory { get; set; }
        public DbSet<InvProductCategorySpecification> InvProductCategorySpecification { get; set; }
        public DbSet<InvCategoryProductWiseSpecificationMapping> InvCategoryProductWiseSpecificationMapping { get; set; }
        public DbSet<InvProductColor> InvProductColor { get; set; }
        public DbSet<InvProductDiscount> InvProductDiscount { get; set; }
        public DbSet<InvProductDiscountType> InvProductDiscountType { get; set; }
        public DbSet<InvProductGrade> InvProductGrade { get; set; }
        public DbSet<InvProductModel> InvProductModel { get; set; }
        public DbSet<InvProductPricing> InvProductPricing { get; set; }
        public DbSet<InvProductSize> InvProductSize { get; set; }
        public DbSet<InvProductSubCategory> InvProductSubCategory { get; set; }
        public DbSet<InvProductSupplier> InvProductSupplier { get; set; }
        public DbSet<InvProductTransfer> InvProductTransfer { get; set; }
        public DbSet<InvProductTransferDetails> InvProductTransferDetails { get; set; }
        public DbSet<InvProductType> InvProductType { get; set; }
        public DbSet<InvProductUOM> InvProductUOM { get; set; }
        public DbSet<InvProductWiseColor> InvProductWiseColor { get; set; }
        public DbSet<InvProductWiseSize> InvProductWiseSize { get; set; }
        public DbSet<InvProductWiseSpecification> InvProductWiseSpecification { get; set; }
        public DbSet<InvMake> InvMake { get; set; }
        public DbSet<InvMakeModel> InvMakeModel { get; set; }
        public DbSet<InvProductWiseSpecificationDetails> InvProductWiseSpecificationDetails { get; set; }
        public DbSet<InvProductSpecInfo> InvProductSpecInfo { get; set; }
        public DbSet<InvProductWiseUOM> InvProductWiseUOM { get; set; }
        public DbSet<InvStockCategory> InvStockCategory { get; set; }
        public DbSet<InvStockDetails> InvStockDetails { get; set; }
        public DbSet<InvStockMaster> InvStockMaster { get; set; }
        public DbSet<InvStockReceive> InvStockReceive { get; set; }
        public DbSet<InvStockReceiveDetails> InvStockReceiveDetails { get; set; }
        public DbSet<InvStockType> InvStockType { get; set; }
        public DbSet<InvStockInWithBarcode> InvStockInWithBarcode { get; set; }
        public DbSet<InvStockInWithBarcodeDetails> InvStockInWithBarcodeDetails { get; set; }
        public DbSet<InvProductSetMaster> InvProductSetMaster { get; set; }
        public DbSet<InvProductSetDetails> InvProductSetDetails { get; set; }
        public DbSet<InvDamageGoods> InvDamageGoods { get; set; }
        public DbSet<InvDamageGoodsDetails> InvDamageGoodsDetails { get; set; }
        public DbSet<InvProductWiseUOMConverter> InvProductWiseUOMConverter { get; set; }
        public DbSet<InvDamageExpireProductReturnMaster> InvDamageExpireProductReturnMaster { get; set; }
        public DbSet<InvDamageExpireProductReturnDetail> InvDamageExpireProductReturnDetail { get; set; }
        public DbSet<InvDestructionNoteReceiveMaster> InvDestructionNoteReceiveMaster { get; set; }
        public DbSet<InvDestructionNoteReceiveDetail> InvDestructionNoteReceiveDetail { get; set; }
        public DbSet<InvProductSpecListExcludedFromReports> InvProductSpecListExcludedFromReports { get; set; }
        public DbSet<InvFactoryProductionStockIn> InvFactoryProductionStockIn { get; set; }
        public DbSet<InvFactoryProductionStockInDetail> InvFactoryProductionStockInDetail { get; set; }

        public DbSet<InvRePackProductTransferMaster> InvRePackProductTransferMaster { get; set; }
        public DbSet<InvRePackProductTransferDetails> InvRePackProductTransferDetails { get; set; }
        public DbSet<InvBatchWiseSerialNo> InvBatchWiseSerialNo { get; set; }

        #endregion


        #region Product category Query
        public DbQuery<ProductCategoryViewModel> productCategoryViewModels { get; set; }
        public DbQuery<ProductViewModel> productViewModels { get; set; }
        public DbQuery<ProductWiseSpecificationViewModel> productWiseSpecificationViewModels { get; set; }
        #endregion



        #endregion

        #region Promo Inventory 

        public DbSet<PromoRequisitionUploadMaster> PromoRequisitionUploadMaster { get; set; }
        public DbSet<PromoRequisitionUploadDetails> PromoRequisitionUploadDetails { get; set; }


        public DbSet<PromoPacketingMaster> PromoPacketingMaster { get; set; }
        public DbSet<PromoPacketingDetails> PromoPacketingDetails { get; set; }
        public DbSet<PromoPacketNoDetails> PromoPacketNoDetails { get; set; }


        public DbSet<PromoPacketDistributionMaster> PromoPacketDistributionMaster { get; set; }
        public DbSet<PromoPacketDistributionDetails> PromoPacketDistributionDetails { get; set; }



        public DbSet<PromoStockMaster> PromoStockMaster { get; set; }
        public DbSet<PromoStockDetails> PromoStockDetails { get; set; }

        public DbSet<DepotPromoReceiveMaster> DepotPromoReceiveMaster { get; set; }
        public DbSet<DepotPromoReceiveDetails> DepotPromoReceiveDetails { get; set; }

        public DbSet<DepotPromoDistributionMaster> DepotPromoDistributionMasters { get; set; }
        public DbSet<DepotPromoDistributionDetails> DepotPromoDistributionDetails { get; set; }

        
        public DbSet<PromoStockReceiveMaster> PromoStockReceiveMaster { get; set; }
        public DbSet<PromoStockReceiveDetails> PromoStockReceiveDetails { get; set; }


        public DbSet<PromoTerritoryStockMaster> PromoTerritoryStockMaster { get; set; }
        public DbSet<PromoTerritoryStockDetails> PromoTerritoryStockDetails { get; set; }
        public DbSet<PromoTerritoryCurrentStock> PromoTerritoryCurrentStock { get; set; }


        #endregion


        #region Purchase DbSet -------------
        public DbSet<PurPOWiseTermsAndConditions> PurPOWiseTermsAndConditions { get; set; }
        public DbSet<PurProductReqDetails> PurProductReqDetails { get; set; }
        public DbSet<PurProductRequisition> PurProductRequisition { get; set; }
        public DbSet<PurPurchaseOrder> PurPurchaseOrder { get; set; }
        public DbSet<PurPurchaseOrderDetails> PurPurchaseOrderDetails { get; set; }
        public DbSet<PurPurchaseOrderReceive> PurPurchaseOrderReceive { get; set; }
        public DbSet<PurPurchaseOrderReceiveDetails> PurPurchaseOrderReceiveDetails { get; set; }
        public DbSet<PurRequisitionRevision> PurRequisitionRevision { get; set; }
        public DbSet<PurPurchaseReqDetails> PurPurchaseReqDetails { get; set; }
        public DbSet<PurPurchaseRequisition> PurPurchaseRequisition { get; set; }
        public DbSet<PurTermsAndConditions> PurTermsAndConditions { get; set; }
        public DbSet<PurPurchaseReturnMaster> PurPurchaseReturnMaster { get; set; }
        public DbSet<PurPurchaseReturnDetail> PurPurchaseReturnDetail { get; set; }

        public DbSet<PurRequisitionFinalizeMaster> PurRequisitionFinalizeMaster { get; set; }
        public DbSet<PurRequisitionFinalizeDetail> PurRequisitionFinalizeDetail { get; set; }
        public DbSet<PurQuotationCollectionMaster> PurQuotationCollectionMaster { get; set; }
        public DbSet<PurQuotationCollectionDetail> PurQuotationCollectionDetail { get; set; }
        public DbSet<PurCSMaster> PurCSMaster { get; set; }
        public DbSet<PurCSDetail> PurCSDetail { get; set; }

        public DbSet<PurGRNMaster> PurGRNMaster { get; set; }
        public DbSet<PurGRNDetail> PurGRNDetail { get; set; }

        public DbSet<PurBillMaster> PurBillMaster { get; set; }
        public DbSet<PurBillDetail> PurBillDetail { get; set; }
        public DbSet<PurPaymentMaster> PurPaymentMaster { get; set; }
        public DbSet<PurBudgetCreate> PurBudgetCreate { get; set; }
        public DbSet<PurBudgetCategory> PurBudgetCategory { get; set; }
        public DbSet<PurGrnLogtbl> PurGrnLogtbl { get; set; }
        public DbSet<PurUserWiseProductCategory> PurUserWiseProductCategory { get; set; }

        #region Import
        public DbSet<PurImpChargeHead> PurImpChargeHead { get; set; }
        public DbSet<PurImpModeOfTransport> PurImpModeOfTransport { get; set; }
        public DbSet<PurImpLocalAgent> PurImpLocalAgent { get; set; }
        public DbSet<PurImpBenificiary> PurImpBenificiary { get; set; }
        public DbSet<PurImpPortInfo> PurImpPortInfo { get; set; }
        public DbSet<PurImpAdviceBank> PurImpAdviceBank { get; set; }
        public DbSet<PurImpPreLCInfoMaster> PurImpPreLCInfoMaster { get; set; }
        public DbSet<PurImpPreLCInfoDetail> PurImpPreLCInfoDetail { get; set; }
        public DbSet<PurImpLCInfoMaster> PurImpLCInfoMaster { get; set; }
        public DbSet<PurInsuranceCompany> PurInsuranceCompany { get; set; }
        public DbSet<PurImpBankInsuranceChargeMaster> PurImpBankInsuranceChargeMaster { get; set; }
        public DbSet<PurImpBankInsuranceChargeDetail> PurImpBankInsuranceChargeDetail { get; set; }
        public DbSet<PurImpLCAmendment> PurImpLCAmendment { get; set; }
        public DbSet<PurImpLCAmendmentCharge> PurImpLCAmendmentCharge { get; set; }
        public DbSet<PurImpShipmentInformation> PurImpShipmentInformation { get; set; }
        public DbSet<PurImpClearenceInfo> PurImpClearenceInfo { get; set; }
        public DbSet<PurImpOtherCharge> PurImpOtherCharge { get; set; }
        public DbSet<PurImpOffshoreCharge> PurImpOffshoreCharge { get; set; }
        public DbSet<PurImpGRNMaster> PurImpGRNMaster { get; set; }
        public DbSet<PurImpGRNDetail> PurImpGRNDetail { get; set; }
        #endregion

        #endregion

        #region Sales DbSet -------------
        public DbSet<SalCollectionDetail> SalCollectionDetail { get; set; }
        public DbSet<SalCollectionMaster> SalCollectionMaster { get; set; }
        public DbSet<SalPaymentMode> SalPaymentMode { get; set; }
        public DbSet<SalPaymentType> SalPaymentType { get; set; }
        public DbSet<SalSalesInvoice> SalSalesInvoice { get; set; }
        public DbSet<SalSalesInvoiceDetails> SalSalesInvoiceDetails { get; set; }
        public DbSet<SalSalesInvoiceTC> SalSalesInvoiceTC { get; set; }
        public DbSet<SalSalesOfferMaster> SalSalesOfferMaster { get; set; }
        public DbSet<SalSalesOfferDetails> SalSalesOfferDetails { get; set; }
        public DbSet<SalSalesReturnMaster> SalSalesReturnMaster { get; set; }
        public DbSet<SalSalesReturnDetails> SalSalesReturnDetails { get; set; }
        public DbSet<SalDistributionMaster> SalDistributionMaster { get; set; }
        public DbSet<SalDistributionDetail> SalDistributionDetail { get; set; }
        public DbSet<SalSalesGrossRetun> SalSalesGrossRetun { get; set; }
        public DbSet<SalSalesProductExpireReturn> SalSalesProductExpireReturn { get; set; }
        public DbSet<SalSalesProductExpireReturnDetails> SalSalesProductExpireReturnDetails { get; set; }
        public DbSet<SalSalesProductExpireReturnMaster> SalSalesProductExpireRetunrMaster { get; set; }
        public DbSet<SalMIOSalesTargetMaster> SalMIOSalesTargetMaster { get; set; }
        public DbSet<SalMIOSalesTargetDetail> SalMIOSalesTargetDetail { get; set; }
        public DbSet<SalSalesGrossReturnMaster> SalSalesGrossReturnMaster { get; set; }
        public DbSet<SalSalesGrossReturnProduct> SalSalesGrossReturnProduct { get; set; }
        public DbSet<SalSalesInvoiceStatement> SalSalesInvoiceStatement { get; set; }

        public DbSet<SalGeneralCustomerBonusPolicy> SalGeneralCustomerBonusPolicy { get; set; }
        public DbSet<SalMangoCustomerBonusPolicy> SalMangoCustomerBonusPolicy { get; set; }
        public DbSet<SalProductSpecWiseIncentivePolicy> SalProductSpecWiseIncentivePolicy { get; set; }
        public DbSet<SalPickingMaster> SalPickingMaster { get; set; }
        public DbSet<SalPickingDetail> SalPickingDetail { get; set; }
        public DbSet<SalPickingSummary> SalPickingSummary { get; set; }
        public DbSet<SalSalesDispatchMaster> SalSalesDispatchMaster { get; set; }
        public DbSet<SalSalesDispatchDetail> SalSalesDispatchDetail { get; set; }

        public DbSet<SalDiscountRate> SalDiscountRate { get; set; }
        public DbSet<SalDiscountItem> SalDiscountItem { get; set; }
        public DbSet<SalFlatRateProduct> SalFlatRateProduct { get; set; }
        public DbSet<SalProductMonitor> SalProductMonitor { get; set; }
        public DbSet<SalWeeklyTargetPercentage> SalWeeklyTargetPercentage { get; set; }
        public DbSet<SalDealNotApplicableCustomerAndInstitute> SalDealNotApplicableCustomerAndInstitute { get; set; }
        public DbSet<SalExecutiveWiseProduct> SalExecutiveWiseProduct { get; set; }
        public DbSet<SalExecutiveTeam> SalExecutiveTeam { get; set; }

        //Exam
        public DbSet<HrmSalaryLocation> HrmSalaryLocation { get; set; }
        public DbSet<CmnExamContent> CmnExamContent { get; set; }
        public DbSet<CmnExam> CmnExam { get; set; }
        public DbSet<CmnExamQuestion> CmnExamQuestion { get; set; }
        public DbSet<CmnExamQuestionOption> CmnExamQuestionOption { get; set; }
        public DbSet<CmnExamPerform> CmnExamPerform { get; set; }

        public DbSet<SalRemittance> SalRemittance { get; set; }
        public DbSet<SalRemittanceSlip> SalRemittanceSlip { get; set; }
        public DbSet<CmnBankBranch> CmnBankBranch { get; set; }
        public DbSet<SalOpeningRemittance> SalOpeningRemittance { get; set; }
        public DbSet<SalTerritoryCollectionTargetMaster> SalTerritoryCollectionTargetMaster { get; set; }
        public DbSet<SalTerritoryCollectionTargetDetail> SalTerritoryCollectionTargetDetail { get; set; }
        public DbSet<SalMiscellaneousItemDetails> SalMiscellaneousItemDetails { get; set; }
        public DbSet<SalMiscellaneousItem> SalMiscellaneousItem { get; set; }
        public DbSet<SalMiscellaneousItemDetailsDepot> SalMiscellaneousItemDetailsDepot { get; set; }
        public DbSet<SalMiscellaneousItemDepot> SalMiscellaneousItemDepot { get; set; }
        public DbSet<SalMiscellaneousItemFileDepot> SalMiscellaneousItemFileDepot { get; set; }
        public DbSet<SalMiscellaneousItemFile> SalMiscellaneousItemFile { get; set; }
        public DbSet<MoneyReceiptType> MoneyReceiptType { get; set; }
        public DbSet<MoneyReceiptNote> MoneyReceiptNote { get; set; }
        public DbSet<SalDMSReportMaster> SalDMSReportMaster { get; set; }
        public DbSet<SalDMSReportDetail> SalDMSReportDetail { get; set; }
        public DbSet<MoneyReceipt> MoneyReceipt { get; set; }
        public DbSet<MoneyReceiptDetails> MoneyReceiptDetails { get; set; }
        public DbSet<SalRemittanceAdjustment> SalRemittanceAdjustment { get; set; }
        public DbSet<SalGdnConfirmationLogs> SalGdnConfirmationLogs { get; set; }

        public DbSet<SalSalesOrder> SalSalesOrder { get; set; }
        public DbSet<SalSalesOrderDetails> SalSalesOrderDetails { get; set; }
        public DbSet<SalSalesOrderTC> SalSalesOrderTC { get; set; }
        public DbSet<SalRemittanceWiseCollection> SalRemittanceWiseCollection { get; set; }
        public DbSet<SalRemittanceMaster> SalRemittanceMaster { get; set; }
        public DbSet<SalMIOSalesTargetMasterYearly> SalMIOSalesTargetMasterYearly { get; set; }
        public DbSet<SalMIOSalesTargetDetailYearly> SalMIOSalesTargetDetailYearly { get; set; }
        public DbSet<SalMonthWiseBudgetPercent> SalMonthWiseBudgetPercent { get; set; }
        public DbSet<SalMIODailySalesForecast> SalMIODailySalesForecast { get; set; }


        #endregion

        #region HRMS DBSet----------------
        public DbSet<HrmEmployee> HrmEmployee { get; set; }
        public DbSet<HrmEmployeeAddress> HrmEmployeeAddress { get; set; }
        public DbSet<HrmEmployeeAttachment> HrmEmployeeAttachment { get; set; }
        public DbSet<HrmEmployeeEducation> HrmEmployeeEducation { get; set; }
        public DbSet<HrmEmployeeNominee> HrmEmployeeNominee { get; set; }
        public DbSet<HrmEmployeeDepartment> HrmEmployeeDepartment { get; set; }
        public DbSet<HrmEmployeeDesignation> HrmEmployeeDesignation { get; set; }
        public DbSet<HrmEmployeeTraining> HrmEmployeeTraining { get; set; }
        public DbSet<HrmEmployeeMobileBill> HrmEmployeeMobileBill { get; set; }
        public DbSet<HrmEmployeeJobDescription> HrmEmployeeJobDescription { get; set; }
        public DbSet<HrmOtherExpense> HrmOtherExpense { get; set; }
        public DbSet<SalCategorySales> SalCategorySales { get; set; }
        public DbSet<SalesCategoryWiseProductMaster> SalesCategoryWiseProductMaster { get; set; }
        public DbSet<SalesCategoryWiseProductDetails> SalesCategoryWiseProductDetails { get; set; }
        public DbSet<UpdateTransferLog> updateTransferLogs { get; set; }

        public DbSet<HrmEmployeeBasicLog> HrmEmployeeBasicLog { get; set; }

        #region Leave Module DB SET

        public DbSet<HrmLeaveType> HrmLeaveType { get; set; }
        public DbSet<HrmLeaveYear> HrmLeaveYear { get; set; }
        public DbSet<HrmLeavePolicy> HrmLeavePolicy { get; set; }
        public DbSet<HrmLeaveOpeningBalance> HrmLeaveOpeningBalance { get; set; }
        public DbSet<HrmLeaveApprovalMatrix> HrmLeaveApprovalMatrix { get; set; }
        public DbSet<HrmLeaveRegister> HrmLeaveRegister { get; set; }
        public DbSet<HrmLeaveApprovalLog> HrmLeaveApprovalLog { get; set; }


        #endregion

        #region Leave Module DB Query

        public DbQuery<LeaveTypeViewModel> leaveTypeViewModels { get; set; }
        public DbQuery<LeaveYearViewModel> leaveYearViewModels { get; set; }
        public DbQuery<LeavePolicyViewModel> leavePolicyViewModels { get; set; }
        public DbQuery<LeaveOpeningBalanceViewModel> leaveOpeningBalanceViewModels { get; set; }


        #endregion



        ////// -----------  Attendance Related Table
        public DbSet<HrmAttendanceDevice> HrmAttendanceDevice { get; set; }
        public DbSet<HrmAttendanceLog> HrmAttendanceLog { get; set; }        
        public DbSet<HrmAttendanceShiftGroupMaster> HrmAttendanceShiftGroupMaster { get; set; }
        public DbSet<HrmAttendanceShiftGroupDetail> HrmAttendanceShiftGroupDetail { get; set; }
        public DbSet<HrmAttendancePunchCard> HrmAttendancePunchCard { get; set; }
        public DbSet<HrmAttendanceDetails> HrmAttendanceDetails { get; set; }
        public DbSet<HrmAttendanceSummary> HrmAttendanceSummary { get; set; }
        public DbSet<HrnEmployeePromotion> HrnEmployeePromotion { get; set; }
        public DbSet<HrmEmployeeTransfer> HrmEmployeeTransfer { get; set; }
        public DbSet<HrmEmployeeLunchHistory> HrmEmployeeLunchHistory { get; set; }
        public DbSet<HrmEmployeeClarification> HrmEmployeeClarification { get; set; }
        public DbSet<HrmEmployeeClarificationApprovalLog> HrmEmployeeClarificationApprovalLog { get; set; }
        public DbSet<HrmAttandanceClarification> HrmAttandanceClarification { get; set; }
        public DbSet<HrmLateAttandanceApprovalLog> HrmLateAttandanceApprovalLog { get; set; }
        #endregion

        #region HRM Master DBSet------------
        public DbSet<HrmActivityType> HrmActivityType { get; set; }
        public DbSet<HrmAttachmentType> HrmAttachmentType { get; set; }
        public DbSet<HrmDegree> HrmDegree { get; set; }
        public DbSet<HrmDepartment> HrmDepartment { get; set; }
        public DbSet<HrmDesignation> HrmDesignation { get; set; }
        public DbSet<HrmEmployeeStatus> HrmEmployeeStatus { get; set; }
        public DbSet<HrmEmployeeType> HrmEmployeeType { get; set; }
        public DbSet<HrmLevelofEducation> HrmLevelofEducation { get; set; }
        public DbSet<HrmNomineeDetail> HrmNomineeDetail { get; set; }
        public DbSet<HrmNomineeFund> HrmNomineeFund { get; set; }
        public DbSet<HrmOccupation> HrmOccupation { get; set; }
        public DbSet<HrmOtherQualification> HrmOtherQualification { get; set; }
        public DbSet<HrmOtherQualificationHead> HrmOtherQualificationHead { get; set; }
        public DbSet<HrmProfessionalQualifications> HrmProfessionalQualifications { get; set; }
        public DbSet<HrmRelation> HrmRelation { get; set; }
        public DbSet<HrmReligion> HrmReligion { get; set; }
        public DbSet<HrmServiceStatus> HrmServiceStatus { get; set; }
        public DbSet<HrmDegreeSubject> HrmDegreeSubject { get; set; }
        public DbSet<HrmResult> HrmResult { get; set; }
        public DbSet<HrmTrainingInstitute> HrmTrainingInstitute { get; set; }
        public DbSet<HrmUniqueIdentity> HrmUniqueIdentity { get; set; }
        public DbSet<HrmBloodGroup> HrmBloodGroup { get; set; }
        public DbSet<HrmGender> HrmGender { get; set; }
        public DbSet<HrmEducationOrganization> HrmEducationOrganization { get; set; }
        public DbSet<HrmAddressType> HrmAddressType { get; set; }
        public DbSet<HrmTrainingType> HrmTrainingType { get; set; }
        public DbSet<HrmEmployeeFamilyInfo> HrmEmployeeFamilyInfo { get; set; }
        public DbSet<HrmEmployeeExperience> HrmEmployeeExperience { get; set; }
        public DbSet<HrmFinalSettlementMaster> HrmFinalSettlementMaster { get; set; }
        public DbSet<HrmFinalSettlementDetails> HrmFinalSettlementDetails { get; set; }
        public DbSet<HrmFinalSettlementSignatory> HrmFinalSettlementSignatory { get; set; }
        public DbSet<HrmFinalSettlementHead> HrmFinalSettlementHead { get; set; }
        //Loan Tables

        public DbSet<HrmLoanCategory> HrmLoanCategory { get; set; }
        public DbSet<HrmLoanInterestType> HrmLoanInterestType { get; set; }
        public DbSet<HrmLoanEntry> HrmLoanEntry { get; set; }
        public DbSet<HrmLoanLogHistory> HrmLoanLogHistory { get; set; }
        #endregion

        #region Field Force Tracking------
        public DbSet<CmnWeekendDay> CmnWeekendDay { get; set; }
        public DbSet<Chem> Chem { get; set; }
        public DbSet<Chem2> Chem2 { get; set; }
        public DbSet<CmnArea> CmnArea { get; set; }
        public DbSet<CmnCalender> CmnCalender { get; set; }
        public DbSet<CmnCheckInOuts> CmnCheckInOuts { get; set; }
        public DbSet<CmnChemist> CmnChemist { get; set; }
        public DbSet<CmnChemistSchedules> CmnChemistSchedules { get; set; }
        public DbSet<CmnDepot> CmnDepot { get; set; }
        public DbSet<CmnDoctor> CmnDoctor { get; set; }
        public DbSet<CmnDoctorSchedules> CmnDoctorSchedules { get; set; }
        public DbSet<CmnMarket> CmnMarket { get; set; }
        public DbSet<CmnMarketSchedules> CmnMarketSchedules { get; set; }
        public DbSet<CmnMIOCurrentLocations> CmnMIOCurrentLocations { get; set; }
        public DbSet<CmnRegion> CmnRegion { get; set; }
        public DbSet<CmnRosters> CmnRosters { get; set; }
        public DbSet<CmnTerritorys> CmnTerritorys { get; set; }
        public DbSet<CmnUserConnectionInfo> CmnUserConnectionInfo { get; set; }
        public DbSet<CmnUsers> CmnUsers { get; set; }
        public DbSet<CmnUserTypes> CmnUserTypes { get; set; }
        public DbSet<CmnZone> CmnZone { get; set; }
        public DbSet<DocList> DocList { get; set; }
        public DbSet<RawData> RawData { get; set; }
        public DbSet<CmnCompany> CmnCompanys { get; set; }
        public DbSet<CmnChemist> cmnChemists { get; set; }
        public DbSet<CmnDoctor> doctors { get; set; }
        public DbSet<CmnEmpSchedules> CmnEmpSchedules { get; set; }
        public DbSet<CmnDoctorPromotionalItem> CmnDoctorPromotionalItem { get; set; }
        public DbSet<CmnDoctorsPrescriptions> CmnDoctorsPrescriptions { get; set; }
        public DbSet<CmnWeeklyPlan> CmnWeeklyPlan { get; set; }
        public DbSet<CmnWeeklyPlanDoc> CmnWeeklyPlanDoc { get; set; }
        public DbSet<CmnDoctorCategory> CmnDoctorCategory { get; set; }
        public DbSet<CmnDoctorRx> CmnDoctorRx { get; set; }
        public DbSet<CmnRxUpload> CmnRxUpload { get; set; }
        public DbSet<CmnRxUploadMaster> CmnRxUploadMaster { get; set; }
        public DbSet<CmnRxUploadProduct> CmnRxUploadProduct { get; set; }
        public DbSet<CmnTerritoryWiseTerget> CmnTerritoryWiseTerget { get; set; }
        public DbSet<CmnTADAForEmployee> CmnTADAForEmployee { get; set; }
        public DbSet<CmnBasicDegree> CmnBasicDegree { get; set; }

        //public DbSet<CmnTADAForEmployeeRemarksHistory> CmnTADAForEmployeeRemarksHistory { get; set; }
        public DbSet<CmnTAReceipt> CmnTAReceipt { get; set; }
        public DbSet<CmnTADACostPostingLocationWise> CmnTADACostPostingLocationWise { get; set; }
        public DbSet<CmnTerritoryWiseMonthlyPromoItem>  CmnTerritoryWiseMonthlyPromoItem { get; set; }
        public DbSet<CmnLocationWiseVehicleBill> CmnLocationWiseVehicleBill { get; set; }
        public DbSet<CmnActionPlan> CmnActionPlan { get; set; }
        public DbSet<CmnActionCampain> CmnActionCampain { get; set; }
        public DbSet<CmnDoctorUnterObservation> CmnDoctorUnterObservation { get; set; }
        public DbSet<CmnDoctorChemistDeleteHistory> CmnDoctorChemistDeleteHistory { get; set; }
        public DbSet<CmnKnowledgeSkill> CmnKnowledgeSkill { get; set; }
        public DbSet<CmnWeeklyPlanTerritory> CmnWeeklyPlanTerritory { get; set; }
        public DbSet<CmnSuperStarItem> CmnSuperStarItem { get; set; }
        public DbSet<CmnIncentiveCalculation> CmnIncentiveCalculation { get; set; }
        public DbSet<AppsVersion> AppsVersion { get; set; }
        public DbSet<CmnMessageInfo> CmnMessageInfo { get; set; }
        public DbSet<CmnDocExecutionDetails> CmnDocExecutionDetails { get; set; }
        public DbSet<CmnDocExecutionMembers> CmnDocExecutionMembers { get; set; }
        public DbSet<CmnChemExecutionDetails> CmnChemExecutionDetails { get; set; }
        public DbSet<CmnChemExecutionMembers> CmnChemExecutionMembers { get; set; }


        public DbQuery<DoctorListViewModel> doctorListViewModels { get; set; }
        public DbQuery<ChemistListViewModel> chemistListViewModels { get; set; }
        public DbQuery<ChemistListViewModelLoad> chemistListViewModelLoads { get; set; }
        public DbQuery<SaveScheduleViewModel> saveScheduleViewModels { get; set; }
        public DbQuery<DoctorScheduleListViewModel> doctorScheduleListViewModels { get; set; }
        public DbQuery<ChemistScheduleListViewModel> chemistScheduleListViewModels { get; set; }
        public DbQuery<VisitReportDoctorViewModel> visitReportDoctorViewModels { get; set; }
        public DbQuery<ChemistDataChartViewModel> chemistDataChartViewModels { get; set; }
        public DbQuery<VisitReportChemistViewModel> visitReportChemistViewModels { get; set; }
        public DbQuery<DoctorWiseVisitReportViewModel> doctorWiseVisitReportViewModels { get; set; }
        public DbQuery<MarketListViewModel> marketListViewModels { get; set; }
        public DbQuery<AreaListViewModel> areaListViewModels { get; set; }
        public DbQuery<RegionListViewModel> regionListViewModels { get; set; }
        public DbQuery<ZoneListViewModel> zoneListViewModels { get; set; }
        public DbQuery<DepoListViewModel> depoListViewModels { get; set; }
        public DbQuery<TeritoryListViewModel> teritoryListViewModels { get; set; }
        public DbQuery<MIOListViewModel> mIOListViewModels { get; set; }
        public DbQuery<MIOCurrentLocationViewModel> mIOCurrentLocationViewModels { get; set; }
        public DbQuery<MIOCurrentLocationNNViewModel> MIOCurrentLocationViewModels2 { get; set; }
        public DbQuery<MarketListAPIPlanViewModel> marketListAPIPlanViewModels { get; set; }
        public DbQuery<MarketListAPIViewModel> marketListAPIViewModels { get; set; }
        public DbQuery<DoctorListAPIViewModel> doctorListAPIViewModels { get; set; }
        public DbQuery<ChemistListAPIViewModel> chemistListAPIViewModels { get; set; }
        public DbQuery<ChemistDataViewModel> chemistDataViewModels { get; set; }
        public DbQuery<EmployeeViewModel> employeeLoadViewModels { get; set; }
        public DbQuery<EmployeeLoadJsonViewModel> employeeLoadJsonViewModels { get; set; }
        public DbQuery<VisitReportEmployeeViewModel> visitReportEmployeeViewModels { get; set; }
        public DbQuery<ChemistWiseVisitReportViewModel> chemistWiseVisitReportViewModels { get; set; }
        public DbQuery<SummaryDataViewModel> summaryDataViewModels { get; set; }
        public DbQuery<StockSalesChartViewModel> stockSalesChartViewModels { get; set; }
        public DbQuery<AttendanceModel> attendanceModels { get; set; }
        public DbQuery<AttendenceReportViewModel> attendenceReportViewModels { get; set; }
        public DbQuery<BrandListViewModel> brandListViewModels { get; set; }
        public DbQuery<MIOCurrentLocationNNViewModel> MIOCurrentLocationNNViewModels { get; set; }

        #endregion

        #region Production
        public DbSet<PrdBomMaster> PrdBomMaster { get; set; }
        public DbSet<PrdProcessHead> PrdProcessHead { get; set; }
        public DbSet<PrdMachineInfo> PrdMachineInfo { get; set; }
        public DbSet<PrdBatchType> PrdBatchType { get; set; }

        public DbSet<PrdBomDetails> PrdBomDetails { get; set; }
        public DbSet<PrdBomFinishGoodStockInMaster> PrdBomFinishGoodStockInMaster { get; set; }
        public DbSet<PrdBomFinishGoodStockInDetails> PrdBomFinishGoodStockInDetails { get; set; }

        public DbSet<PrdRequisitionMaster> PrdRequisitionMaster { get; set; }
        public DbSet<PrdRequisitionDetails> PrdRequisitionDetails { get; set; }
        public DbSet<PrdProductionPlan> PrdProductionPlan { get; set; }
        public DbSet<PrdProductionPlanProcess> PrdProductionPlanProcess { get; set; }
        public DbSet<PrdProductionPlanMachine> PrdProductionPlanMachine { get; set; }
        public DbSet<PrdProcessHeadGroupMaster> PrdProcessHeadGroupMaster { get; set; }
        public DbSet<PrdProcessHeadGroupDetails> PrdProcessHeadGroupDetails { get; set; }
        public DbSet<PrdProductGroupAssign> PrdProductGroupAssign { get; set; }

        public DbSet<PrdProductIssueMaster> PrdProductIssueMaster { get; set; }
        public DbSet<PrdProductIssueDetail> PrdProductIssueDetail { get; set; }

        public DbSet<PrdProductReceiveMaster> PrdProductReceiveMaster { get; set; }
        public DbSet<PrdProductReceiveDetail> PrdProductReceiveDetail { get; set; }
        public DbSet<PrdProductionQaMaster> PrdProductionQaMaster { get; set; }
        public DbSet<PrdProductionQaDetails> PrdProductionQaDetails { get; set; }
        public DbSet<PrdPendingBomMaster> PrdPendingBomMaster { get; set; }
        public DbSet<PrdPendingBomDetails> PrdPendingBomDetails { get; set; }
        public DbSet<PrdBomFor> PrdBomFor { get; set; }
        public DbSet<PrdTransferNote> PrdTransferNote { get; set; }
        public DbSet<PrdProductReceiveFromReturnMaster> PrdProductReceiveFromReturnMaster { get; set; }
        public DbSet<PrdProductReceiveFromReturnDetails> PrdProductReceiveFromReturnDetails { get; set; }

        public DbSet<PrdProductReturnMaster> PrdProductReturnMaster { get; set; }
        public DbSet<PrdProductReturnDetail> PrdProductReturnDetail { get; set; }
        public DbSet<PrdRmPmMiscellaneousReq> PrdRmPmMiscellaneousReq { get; set; }
        public DbSet<PrdRmPmMiscellaneousReqDetails> PrdRmPmMiscellaneousReqDetails { get; set; }
        public DbSet<PrdRmPmMiscellaneousIssue> PrdRmPmMiscellaneousIssue { get; set; }
        public DbSet<PrdRmPmMiscellaneousIssueDetails> PrdRmPmMiscellaneousIssueDetails { get; set; }

        public DbSet<PrdProductionPlanProcessLog> PrdProductionPlanProcessLog { get; set; }

        public DbSet<PrdReagentReqMaster> PrdReagentReqMaster { get; set; }
        public DbSet<PrdReagentReqDetails> PrdReagentReqDetails { get; set; }

        public DbSet<PrdReagentIssueMaster> PrdReagentIssueMasters { get; set; }
        public DbSet<PrdReagentIssueDetail> PrdReagentIssueDetails { get; set; }
        public DbSet<PrdReagentReceiveMaster> PrdReagentReceiveMasters { get; set; }
        public DbSet<PrdReagentReceiveDetail> PrdReagentReceiveDetails { get; set; }
        #endregion

        #region Salary


        public DbSet<SalaryCalulationType> SalaryCalulationType { get; set; }
        public DbSet<SalaryType> SalaryType { get; set; }
        public DbSet<SalaryBonusType> SalaryBonusType { get; set; }
        public DbSet<SalaryWalletType> SalaryWalletType { get; set; }
        public DbSet<SalaryHead> SalaryHead { get; set; }
        public DbSet<SalaryGrade> SalaryGrade { get; set; }
        public DbSet<SalarySlab> SalarySlab { get; set; }
        public DbSet<SalaryGradePercent> SalaryGradePercent { get; set; }
        public DbSet<SalaryBonusRules> SalaryBonusRules { get; set; }        
        public DbSet<SalaryBonusSubRules> SalaryBonusSubRules { get; set; }
        public DbSet<SalaryBonusStructure> SalaryBonusStructure { get; set; } 
        public DbSet<SalaryPeriod> SalaryPeriod { get; set; }              
       
        public DbSet<SalaryEmployeeBonusStructure> SalaryEmployeeBonusStructure { get; set; }
        public DbSet<SalaryEmployeeCashSetup> SalaryEmployeeCashSetup { get; set; }
        public DbSet<SalaryEmployeeCashSetupPeriodWise> SalaryEmployeeCashSetupPeriodWise { get; set; }
        public DbSet<SalaryEmployeeProcess> SalaryEmployeeProcess { get; set; }
        public DbSet<SalaryEmployeeProcessMaster> SalaryEmployeeProcessMaster { get; set; }
        public DbSet<SalaryEmployeeProcessRemarks> SalaryEmployeeProcessRemarks { get; set; }
        public DbSet<SalaryEmployeeStructure> SalaryEmployeeStructure { get; set; }
        public DbSet<SalaryEmployeeStructureHistory> SalaryEmployeeStructureHistory { get; set; }
        public DbSet<SalaryProcessLog> SalaryProcessLog { get; set; }

        public DbSet<SalarySlabType> SalarySlabType { get; set; }
        public DbSet<SalaryRebateSlabType> SalaryRebateSlabType { get; set; }
        public DbSet<SalarySlabRebate> SalarySlabRebate { get; set; }
        public DbSet<SalaryInvestmentRebateSettings> SalaryInvestmentRebateSettings { get; set; }
        public DbSet<SalarySlabIncomeTax> SalarySlabIncomeTax { get; set; }
        public DbSet<SalarySlabIncomeTaxAssign> SalarySlabIncomeTaxAssign { get; set; }
        public DbSet<SalaryTaxChallan> SalaryTaxChallan { get; set; }
        public DbSet<SalaryAdditionalTaxInfo> SalaryAdditionalTaxInfo { get; set; }
        public DbSet<SalaryIncomeTaxSetup> SalaryIncomeTaxSetup { get; set; }
        public DbSet<SalaryEmployeeFixedTax> SalaryEmployeeFixedTax { get; set; }
        public DbSet<SalaryEmployeeTax> SalaryEmployeeTax { get; set; }
        public DbSet<SalaryEmployeeFixedHeadStructure> SalaryEmployeeFixedHeadStructure { get; set; }
        public DbSet<SalaryDepot> SalaryDepot { get; set; }
        public DbSet<SalaryEmployeeProcessStructure> SalaryEmployeeProcessStructure { get; set; }
        public DbSet<SalarySlabDesignation> SalarySlabDesignation { get; set; }


        #region DB Query Salary

        public DbQuery<SalaryGradePercentViewModel> salaryGradePercentViewModels { get; set; }
        public DbQuery<SalaryHeadViewModel> salaryHeadViewModels { get; set; }

        #endregion


        #endregion

        #region Task MAnagement
        public DbSet<TaskType> TaskType { get; set; }
        public DbSet<TaskTeamMaster> TaskTeamMaster { get; set; }
        public DbSet<TaskTeamDetail> TaskTeamDetail { get; set; }
        public DbSet<TaskPriority> TaskPriority { get; set; }
        public DbSet<TaskStatus> TaskStatus { get; set; }
        public DbSet<TaskInfo> TaskInfo { get; set; }
        public DbSet<TaskStatusLog> TaskStatusLog { get; set; }

        public DbSet<HrmEmployeeTeam> HrmEmployeeTeam { get; set; }
        public DbSet<HrmCoreFunction> HrmCoreFunction { get; set; }
        public DbSet<HrmEmployeeMonthlyTaskAssign> HrmEmployeeMonthlyTaskAssign { get; set; }
        public DbSet<HrmEmployeeWeeklyTaskAssign> HrmEmployeeWeeklyTaskAssign { get; set; }

        #endregion

        #region DigitalGiftCouponInfo
        public DbSet<DigitalGiftCouponInfo> DigitalGiftCouponInfo { get; set; }
        public DbSet<DigitalGiftDisburseLog> DigitalGiftDisburseLog { get; set; }

        #endregion

        #region Tender Quotation DbSet -------------
        public DbSet<TndrQuotationMaster> TndrQuotationMaster { get; set; }
        public DbSet<TndrQuotationDetails> TndrQuotationDetails { get; set; }
        public DbSet<TndrChallanMaster> TndrChallanMaster { get; set; }
        public DbSet<TndrChallanDetails> TndrChallanDetails { get; set; }
        public DbSet<TndrFinalChallanDetails> TndrFinalChallanDetails { get; set; }
        public DbSet<TndrBillMaster> TndrBillMaster { get; set; }
        public DbSet<TndrBillDetails> TndrBillDetails { get; set; }

        #endregion

    }
}


