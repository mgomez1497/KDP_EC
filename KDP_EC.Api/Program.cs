using KDP_EC.Api.Helpers;
using KDP_EC.Core.Interfaces;
using KDP_EC.Core.Interfaces.Account;
using KDP_EC.Infraestructure.DBContext.SQLDBManager;
using KDP_EC.Infraestructure.Implementations.Account;
using KDP_EC.Infraestructure.Implementations.EC_KDP;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var jwtSettings = builder.Configuration.GetSection("Jwt").Get<JwtSettings>();
builder.Services.AddSingleton(jwtSettings);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings.Issuer,
            ValidAudience = jwtSettings.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key))
        };
    });



// Inyeccion de dependencias
builder.Services.AddScoped<IUsersLogin, UserLoginRepository>();
builder.Services.AddScoped<ICountries, CountriesRepository>();
builder.Services.AddScoped<IPerson, PersonRepository>();
builder.Services.AddScoped<ICompany, CompaniesRepository>();
builder.Services.AddScoped<IRol, RolRepository>();
builder.Services.AddScoped<IURC, URCRepositoritory>();
builder.Services.AddScoped<IFarms, FarmsRepository>();
builder.Services.AddScoped<ILots, LotsRepository>();
builder.Services.AddScoped<ILots_Type, Lots_TypeRepository>();
builder.Services.AddScoped<ILots_Varietys, Lots_VarietysRepository>();
builder.Services.AddScoped<IRenewal_Types, Renewal_TypesRepository>();
builder.Services.AddScoped<IExport_Tecnician, ExportTecnRepository>();
builder.Services.AddScoped<IVillages, VillagesRepository>();
builder.Services.AddScoped<IChains, ChainsRepository>();
builder.Services.AddScoped<IActivities, ActivitiesRepository>();
builder.Services.AddScoped<IActivityType, ActivityTypeRepository>();
builder.Services.AddScoped<IStageOfCult, StageOfCultRepository>();
builder.Services.AddScoped<ICostCenter, CostCenterRepository>();
builder.Services.AddScoped<IExpenses, ExpensesRepository>();
builder.Services.AddScoped<IIncomes, IncomesRepository>();
builder.Services.AddScoped<IIncomesTypes, Incomes_TypesRepository>();
builder.Services.AddScoped<ICities, CitiesRepository>();
builder.Services.AddScoped<IStates, StatesRepository>();
builder.Services.AddScoped<IVillages, VillagesRepository>();
builder.Services.AddScoped<IFloweringRecords, FloweringRecordsRepository>();
builder.Services.AddScoped<ICoffeeSalesRep, CoffeeSalesRepository>();
builder.Services.AddScoped<IProductivityReport, ProductivityReportRepository>();
builder.Services.AddScoped<IBalanceCostCenters,BalanceCostCentersRepository>();


builder.Services.AddSingleton<SqlDbManager>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();


app.MapControllers();

app.Run();


