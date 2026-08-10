using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;
using ONEERP.Data;
using ONEERP.Data.Entity;
using System;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ONEERP.ERPService.AuthService.Interfaces;
using ONEERP.ERPServices.AuthService;
using ONEERP.ERPServices.AuthService.Interfaces;
using ONEERP.ERPServices.Interfaces;
using ONEERP.ERPServices.Accounting.MasterData.Interfaces;
using ONEERP.ERPServices.Accounting.MasterData;
using ONEERP.ERPServices.MasterData;
using ONEERP.ERPServices.MasterData.Interfaces;
using ONEERP.ERPServices.Accounting.Transaction;
using ONEERP.ERPServices.Accounting.Transaction.Interfaces;
using ONEERP.Areas.Auth.Models;
using ONEERP.ERPServices.Hrm.EmployeesInfo;
using ONEERP.ERPServices.Hrm.EmployeesInfo.Interfaces;
using ONEERP.ERPServices.Inventory.Interfaces;
using ONEERP.ERPServices.Inventory;
using ONEERP.ERPServices.Purchase.Interfaces;
using ONEERP.ERPServices.Purchase;
using ONEERP.ERPServices.Sales;
using ONEERP.ERPServices.Sales.Interfaces;
using ONEERP.ERPServices.FieldForceTracking.Interfaces;
using ONEERP.ERPServices.FieldForceTracking;
using ONEERP.ERPServices.Production.Interfaces;
using ONEERP.ERPServices.Production;
using ONEERP.ERPServices.Schedule;
using ONEERP.ERPServices.Salary.Interfaces;
using ONEERP.ERPServices.Salary;
using ONEERP.ERPServices.Hrm.Leave.Interfaces;
using ONEERP.ERPServices.Hrm.Leave;
using ONEERP.ERPServices.TaskManagement.Interfaces;
using ONEERP.ERPServices.TaskManagement;
using ONEERP.ERPServices.EmailService;
using ONEERP.ERPServices.EmailService.Interfaces;
using ONEERP.ERPServices.DigitalGift.Interfaces;
using ONEERP.ERPServices.DigitalGift;
using ONEERP.ERPServices.ExpenseDashboard.Interfaces;
using ONEERP.ERPServices.ExpenseDashboard.Services;
//using System.Collections.Generic;
//using System.Globalization;

namespace ONEERP
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.Configure<RequestLocalizationOptions>(options =>
            //{
            //    options.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture("en-US");
            //    options.SupportedCultures = new List<CultureInfo> { new CultureInfo("en-US") };
            //});
            services.AddCors();
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;

            });

            #region ERP Database Settings
            services.AddDbContext<ERPDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("ERPConnection"), op =>
        op.CommandTimeout(600)));

            services.AddMemoryCache();

            services.AddIdentity<ApplicationUser, ApplicationRole>()
               .AddEntityFrameworkStores<ERPDbContext>();
            //services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<ERPDbContext>();


            #endregion

            #region Auth Service
            services.AddScoped<IUserInfoes, UserInfoes>();
            services.AddScoped<INavbarService, NavbarService>();
            services.AddScoped<IMenuService, MenuService>();
            services.AddScoped<IJwtFactoryService, JwtFactoryService>();
            services.AddScoped<IPageAssignService, PageAssignService>();

            services.AddScoped<IEmailSenderService, EmailSenderService>();

            //services.AddScoped<ITokenAuthenticatorService, ITokenAuthenticatorService>();

            #endregion

            #region Master Data
            services.AddScoped<ICompanyService, CompanyService>();
            services.AddScoped<ISpecialBranchUnitService, SpecialBranchUnitService>();
            services.AddScoped<IHelpService, HelpService>();
            services.AddScoped<ICmnDropDownService, CmnDropDownService>();
            services.AddScoped<IApprovalMatrixService, ApprovalMatrixService>();
            #endregion

            #region Accounting Master Data
            services.AddScoped<IAccountGroupService, AccountGroupService>();
            services.AddScoped<ICostCentreService, CostCentreService>();
            services.AddScoped<ICurrencyService, CurrencyService>();
            services.AddScoped<IFundSourceService, FundSourceService>();
            services.AddScoped<IGroupNatureService, GroupNatureService>();
            services.AddScoped<ILedgersService, LedgersService>();
            services.AddScoped<ILedgerTypeService, LedgerTypeService>();
            services.AddScoped<IOpeningBalanceService, OpeningBalanceService>();
            services.AddScoped<IPartyService, PartyService>();
            services.AddScoped<ITransactionModeService, TransactionModeService>();
            services.AddScoped<IVoucherStatusService, VoucherStatusService>();
            services.AddScoped<IVoucherTypeService, VoucherTypeService>();
            services.AddScoped<IDahmashiService, DahmashiService>();
            #endregion

            #region Accounting Voucher
            services.AddScoped<ICostCentreAllocationService, CostCentreAllocationService>();
            services.AddScoped<IVoucherApprovalLogService, VoucherApprovalLogService>();
            services.AddScoped<IVoucherDetailService, VoucherDetailService>();
            services.AddScoped<IVoucherMasterService, VoucherMasterService>();
            services.AddScoped<IChequeBookService, ChequeBookService>();
            #endregion

            #region Inventory

            services.AddScoped<IProductCategoryService, ProductCategoryService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IStockService, StockService>();
            services.AddScoped<IProductTransferService, ProductTransferService>();
            services.AddScoped<IStockInWithBarcodeService, StockInWithBarcodeService>();
            services.AddScoped<IStockOutByBarcodeService, StockOutByBarcodeService>();
            services.AddScoped<IProductPricingService, ProductPricingService>();
            services.AddScoped<IDamageGoodsService, DamageGoodsService>();
            services.AddScoped<IPromo, PromoRequisitionService>();

            #endregion

            #region HRM Master----------------------
            services.AddScoped<IHrmMasterService, HrmMasterService>();
            #endregion

            #region Purchase Module

            services.AddScoped<IProductRequisitionService, ProductRequisitionService>();
            services.AddScoped<IPurchaseRequisitionService, PurchaseRequisitionService>();
            services.AddScoped<IPurchaseOrderService, PurchaseOrderService>();
            services.AddScoped<IPurchaseOrderReceiveService, PurchaseOrderReceiveService>();
            services.AddScoped<IPurCommonService, PurCommonService>();
            services.AddScoped<IPurchaseService, PurchaseService>();
            services.AddScoped<IPurchaseReturnService, PurchaseReturnService>();
            services.AddScoped<IAutoStockInOutSettingService, AutoStockInOutSettingService>();
            services.AddScoped<IImportService, ImportService>();
            services.AddScoped<IGRNService, GRNService>();
            services.AddScoped<ICmnSettingService, CmnSettingService>();

            #endregion

            #region Budget

            services.AddScoped<IBudgetService, BudgetService>();

            #endregion

            #region Accounting Report

            services.AddScoped<IAccountReportService, AccountReportService>();

            #endregion

            #region Hrm Employee

            services.AddScoped<IEmployeeInfoService, EmployeeInfoService>();
            services.AddScoped<IEmployeeRelatedOtherInfoService, EmployeeRelatedOtherInfoService>();
            services.AddScoped<IAttendanceService, AttendanceService>();

            #endregion

            #region Hrm Leave
            services.AddScoped<ILeaveTypeService, LeaveTypeService>();
            services.AddScoped<ILeaveYearService, LeaveYearService>();
            services.AddScoped<ILeavePolicyService, LeavePolicyService>();
            services.AddScoped<ILeaveOpeningBalanceService, LeaveOpeningBalanceService>();
            services.AddScoped<ILeaveApprovalMatrixService, LeaveApprovalMatrixService>();
            services.AddScoped<ILeaveRegisterService, LeaveRegisterService>();
            #endregion

            #region Auth JWT
            //services.AddSingleton<IJwtFactoryService, JwtFactoryService>();
            var jwtAppsettingsOptions = Configuration.GetSection(nameof(JwtIssuerOptions));

            SymmetricSecurityKey _signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtAppsettingsOptions["SecreatKey"]));

            services.Configure<JwtIssuerOptions>(Options =>
            {
                Options.Issuer = jwtAppsettingsOptions[nameof(JwtIssuerOptions.Issuer)];
                Options.Audience = jwtAppsettingsOptions[nameof(JwtIssuerOptions.Audience)];
                Options.SigningCredentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256);
            });

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = jwtAppsettingsOptions[nameof(JwtIssuerOptions.Issuer)],

                ValidateAudience = true,
                ValidAudience = jwtAppsettingsOptions[nameof(JwtIssuerOptions.Audience)],

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = _signingKey,

                RequireExpirationTime = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };
            #endregion

            #region Auth Related Settings
            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 4;
                options.Password.RequiredUniqueChars = 1;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = false;
            });

            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromHours(1);

                options.LoginPath = "/Auth/Account/Login";
                options.AccessDeniedPath = "/Auth/Account/AccessDenied";
                options.SlidingExpiration = true;
            });
            #endregion

            #region Areas Config
            services.Configure<RazorViewEngineOptions>(options =>
            {
                options.AreaViewLocationFormats.Clear();
                options.AreaViewLocationFormats.Add("/areas/{2}/Views/{1}/{0}.cshtml");
                options.AreaViewLocationFormats.Add("/areas/{2}/Views/Shared/{0}.cshtml");
                options.AreaViewLocationFormats.Add("/Views/Shared/{0}.cshtml");
            });
            #endregion

            #region PDF
            services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));
            #endregion

            #region Cookies
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddJsonOptions(options =>
                {
                    var resolver = options.SerializerSettings.ContractResolver;
                    if (resolver != null)
                        (resolver as DefaultContractResolver).NamingStrategy = null;
                });
            services.AddSession(options =>
            {
                options.Cookie.Name = ".AdventureWorks.Session";
                options.IdleTimeout = TimeSpan.FromHours(2400000);
                options.Cookie.IsEssential = true;

            });
            services.AddHttpContextAccessor();
            //services.AddIdentity<ApplicationUser, ApplicationRole>()
            // .AddEntityFrameworkStores<ERPDbContext>();

            #endregion

            #region Sales

            services.AddScoped<ISalesInvoiceService, SalesInvoiceService>();
            services.AddScoped<ISalesCollectionService, SalesCollectionService>();
            services.AddScoped<IPaymentModeService, PaymentModeService>();
            services.AddScoped<ISalesOfferService, SalesOfferService>();
            services.AddScoped<ISalesRemittanceService, SalesRemittanceService>();
            services.AddScoped<ISalesReturnService, SalesReturnService>();
            services.AddScoped<ISalesDistributionService, SalesDistributionService>();
            services.AddScoped<ISalesBonusAndIncentivePolicyService, SalesBonusAndIncentivePolicyService>();
            services.AddScoped<ISalesMonitorAndTargetService, SalesMonitorAndTargetService>();
            #endregion

            #region Task Management
            services.AddScoped<IMyTaskService, MyTaskService>();
            services.AddScoped<ITaskInfoService, TaskInfoService>();
            #endregion

            #region Field Force Tracking------

            services.AddScoped<IChemistScheduleService, ChemistScheduleService>();
            services.AddScoped<IChemistService, ChemistService>();
            services.AddScoped<IDoctorScheduleService, DoctorScheduleService>();
            services.AddScoped<IDoctorService, DoctorService>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IERPCompanyService, ERPCompanyService>();
            services.AddScoped<IMapBoxService, MapBoxService>();
            services.AddScoped<IDashboardService, DashboardService>();
            services.AddScoped<IReportService, ReportService>();
            #endregion

            #region Production
            services.AddScoped<IBomMasterService, BomMasterService>();
            services.AddScoped<IBomFinishGoodStockInService, BomFinishGoodStockInService>();
            services.AddScoped<IProductionProcessService, ProductionProcessService>();
            services.AddScoped<IProductionPlanService, ProductionPlanService>();
            services.AddScoped<IBomRequisitionService, BomRequisitionService>();
            services.AddScoped<IProductionIssueAndReceived, ProductionIssueAndReceivedService>();
            services.AddScoped<IReagentReqService, ReagentReqService>();
            services.AddScoped<IReagentIssueService, ReagentIssueService>();
            services.AddScoped<IReagentReceiveService, ReagentReceiveService>();
            #endregion

            #region Salary
            services.AddScoped<ISalaryMasterService, SalaryMasterService>();
            services.AddScoped<ISalaryStructureService, SalaryStructureService>();
            services.AddScoped<ISalaryReportService, SalaryReportService>();
            #endregion

            #region Digital Gift
            services.AddScoped<IDigitalGiftService, DigitalGiftService>();
            #endregion
            #region Expense Dashboard
            services.AddScoped<IExpenseDashboardService, ExpenseDashboardService>();
            #endregion

            #region Executive Member
            services.AddScoped<ISalesExecutiveMemberService, SalesExecutiveMemberService>();
            #endregion
        }

        #region Use policy, cookies
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            //app.Run(async (context) =>
            //    {
            //        await context.Response.WriteAsync(System.Diagnostics.Process.GetCurrentProcess().ProcessName);
            //    });
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseSession();
            //app.UseStaticFiles();
            app.UseAuthentication();

            //app.UseMvc(routes =>
            //{
            //    routes.MapRoute(
            //      name: "areas",
            //      template: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
            //    );
            //});

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                      name: "areas",
                      template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        #endregion
    }
}
