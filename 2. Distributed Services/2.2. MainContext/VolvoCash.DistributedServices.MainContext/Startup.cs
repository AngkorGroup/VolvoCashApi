using System.Text;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using VolvoCash.Application.MainContext.Authentication.Services;
using VolvoCash.Application.MainContext.BankAccountTypes.Services;
using VolvoCash.Application.MainContext.Banks.Services;
using VolvoCash.Application.MainContext.Batches.Services;
using VolvoCash.Application.MainContext.BusinessAreas.Services;
using VolvoCash.Application.MainContext.Cards.Services;
using VolvoCash.Application.MainContext.CardTypes.Services;
using VolvoCash.Application.MainContext.Cashiers.Services;
using VolvoCash.Application.MainContext.Charges.Services;
using VolvoCash.Application.MainContext.Clients.Services;
using VolvoCash.Application.MainContext.Contacts.Services;
using VolvoCash.Application.MainContext.Dealers.Services;
using VolvoCash.Application.MainContext.DocumentTypes.Services;
using VolvoCash.Application.MainContext.Movements.Services;
using VolvoCash.Application.MainContext.RechargeTypes.Services;
using VolvoCash.Application.MainContext.Sectors.Services;
using VolvoCash.Application.MainContext.Transfers.Services;
using VolvoCash.Application.MainContext.Users.Services;
using VolvoCash.CrossCutting.Adapter;
using VolvoCash.CrossCutting.Localization;
using VolvoCash.CrossCutting.NetFramework.Adapter;
using VolvoCash.CrossCutting.NetFramework.Identity;
using VolvoCash.CrossCutting.NetFramework.Localization;
using VolvoCash.CrossCutting.NetFramework.Utils;
using VolvoCash.CrossCutting.NetFramework.Validator;
using VolvoCash.CrossCutting.Pushs;
using VolvoCash.CrossCutting.Validator;
using VolvoCash.Data.MainContext;
using VolvoCash.Data.MainContext.Repositories;
using VolvoCash.DistributedServices.Seedwork.Filters;
using VolvoCash.DistributedServices.Seedwork.Settings;
using VolvoCash.DistributedServices.Seedwork.Utils;
using VolvoCash.Domain.MainContext.Aggregates.BankAgg;
using VolvoCash.Domain.MainContext.Aggregates.BatchAgg;
using VolvoCash.Domain.MainContext.Aggregates.BusinessAreaAgg;
using VolvoCash.Domain.MainContext.Aggregates.CardAgg;
using VolvoCash.Domain.MainContext.Aggregates.ClientAgg;
using VolvoCash.Domain.MainContext.Aggregates.ContactAgg;
using VolvoCash.Domain.MainContext.Aggregates.CurrencyAgg;
using VolvoCash.Domain.MainContext.Aggregates.DealerAgg;
using VolvoCash.Domain.MainContext.Aggregates.DocumentTypeAgg;
using VolvoCash.Domain.MainContext.Aggregates.RechargeTypeAgg;
using VolvoCash.Domain.MainContext.Aggregates.SectorAgg;
using VolvoCash.Domain.MainContext.Aggregates.SMSCodeAgg;
using VolvoCash.Domain.MainContext.Aggregates.UserAgg;
using VolvoCash.Domain.MainContext.Services.CardService;

namespace VolvoCash.DistributedServices.MainContext
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // To add enviroment variable
            services.Configure<AppSettings>(Configuration.GetSection("ApplicationSettings"));

            //allow cors
            services.AddCors(o => o.AddPolicy("AllowAnyOriginPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader()
                       .WithExposedHeaders("Content-Disposition");
            }));

            //Inject Application User Identity
            services.AddHttpContextAccessor();
            services.AddScoped<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IApplicationUser, ApplicationUser>();

            // Controllers
            services.AddControllers();

            // Configure EntityFramework to use SQLServer.
            //services.AddDbContext<MainDbContext>(options =>
            //    options.UseSqlServer(
            //        Configuration["DatabaseSettings:SqlServerConnection"],
            //        x => x.MigrationsAssembly("VolvoCash.DistributedServices.MainContext")
            //    )
            //);

            // Configure EntityFramework to use Oracle.
            services.AddDbContext<MainDbContext>(options =>
                options.UseOracle(
                    Configuration["DatabaseSettings:OracleConnection"],
                    x => x.MigrationsAssembly("VolvoCash.DistributedServices.MainContext")
                )
            );

            // Add framework services.
            services.AddMvc(options =>
            {
                options.Filters.Add(new ValidateModelAttribute()); // an instance
                options.Filters.Add(typeof(LoggerAttribute));
                options.EnableEndpointRouting = false;
            })
            .AddFluentValidation(fv => { });

            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });

            // Custom Exception and validation Filter
            services.AddScoped<CustomExceptionFilterAttribute>();

            // Application Services
            services.AddScoped<IAuthenticationAppService, AuthenticationAppService>();
            services.AddScoped<ICardAppService, CardAppService>();
            services.AddScoped<ICardTypeAppService, CardTypeAppService>();
            services.AddScoped<IContactAppService, ContactAppService>();
            services.AddScoped<ITransferAppService, TransferAppService>();
            services.AddScoped<IMovementAppService, MovementAppService>();
            services.AddScoped<IChargeAppService, ChargeAppService>();
            services.AddScoped<IClientAppService, ClientAppService>();
            services.AddScoped<IBatchAppService, BatchAppService>();
            services.AddScoped<ICashierAppService, CashierAppService>();
            services.AddScoped<IDealerAppService, DealerAppService>();
            services.AddScoped<IUserAppService, UserAppService>();
            services.AddScoped<IBankAppService, BankAppService>();
            services.AddScoped<ISectorAppService, SectorAppService>();
            services.AddScoped<IBusinessAreaAppService, BusinessAreaAppService>();
            services.AddScoped<IRechargeTypeAppService, RechargeTypeAppService>();
            services.AddScoped<IDocumentTypeAppService, DocumentTypeAppService>();
            services.AddScoped<IBankAccountTypeAppService, BankAccountTypeAppService>();

            // Domain Services
            services.AddScoped<ICardTransferService, CardTransferService>();
            services.AddScoped<ICardRechargeService, CardRechargeService>();
            services.AddScoped<ICardChargeService, CardChargeService>();
            services.AddScoped<INotificationChargeService, NotificationChargeService>();

            // Repositories
            services.AddScoped<ISMSCodeRepository, SMSCodeRepository>();
            services.AddScoped<IContactRepository, ContactRepository>();
            services.AddScoped<ICardRepository, CardRepository>();
            services.AddScoped<ICardTypeRepository, CardTypeRepository>();
            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<ITransferRepository, TransferRepository>();
            services.AddScoped<IMovementRepository, MovementRepository>();
            services.AddScoped<IChargeRepository, ChargeRepository>();
            services.AddScoped<ICashierRepository, CashierRepository>();
            services.AddScoped<IDealerRepository, DealerRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAdminRepository, AdminRepository>();
            services.AddScoped<IBatchRepository, BatchRepository>();
            services.AddScoped<IBatchErrorRepository, BatchErrorRepository>();
            services.AddScoped<IBatchMovementRepository, BatchMovementRepository>();
            services.AddScoped<ICardBatchRepository, CardBatchRepository>();
            services.AddScoped<ISessionRepository, SessionRepository>();
            services.AddScoped<IBankRepository, BankRepository>();
            services.AddScoped<ISectorRepository, SectorRepository>();
            services.AddScoped<IBusinessAreaRepository, BusinessAreaRepository>();
            services.AddScoped<IRechargeTypeRepository, RechargeTypeRepository>();
            services.AddScoped<IDocumentTypeRepository, DocumentTypeRepository>();
            services.AddScoped<IBankAccountTypeRepository, BankAccountTypeRepository>();
            services.AddScoped<ICurrencyRepository, CurrencyRepository>();

            //Common Services
            services.AddScoped<IAmazonBucketService, AmazonBucketService>();
            services.AddScoped<IUrlManager, UrlManager>();
            services.AddScoped<IEmailManager, EmailManager>();
            services.AddScoped<ISMSManager, SMSManager>();
            services.AddScoped<ITokenManager, TokenManager>();
            services.AddScoped<IPushNotificationManager, OneSignalPushNotification>();
            services.AddScoped<IReportManager, ReportManager>();

            // Adapters
            services.AddScoped<ITypeAdapterFactory, AutomapperTypeAdapterFactory>();
            TypeAdapterFactory.SetCurrent(new AutomapperTypeAdapterFactory());

            // Configure JWTToken Authentication
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Configuration["JWT:Issuer"],
                        ValidAudience = Configuration["JWT:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(Configuration["JWT:SecretKey"])
                        )
                    };
                });

            // Validator
            EntityValidatorFactory.SetCurrent(new DataAnnotationsEntityValidatorFactory());

            // Localization
            LocalizationFactory.SetCurrent(new ResourcesManagerFactory());
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, MainDbContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("AllowAnyOriginPolicy");

            app.UseStaticFiles();

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            DbInitializer.Initialize(context);
        }
    }
}
