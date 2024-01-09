using Demo.BL.Interface;
using Demo.BL.Mapper;
using Demo.BL.Repository;
using Demo.DAL.Database;
using Demo.DAL.Entity;
using Demo.PL.Language;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddNewtonsoftJson(opt =>
{
    opt.SerializerSettings.ContractResolver = new DefaultContractResolver();
}).AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
.AddDataAnnotationsLocalization(options =>
{
    options.DataAnnotationLocalizerProvider = (type, factory) =>
        factory.Create(typeof(SharedResource));
});



var connectionString = builder.Configuration.GetConnectionString("DemoConnection");
builder.Services.AddDbContext<DemoContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddAutoMapper(x => x.AddProfile(new DomainProfile()));

// Transient (Take Instance With Every Operation)
//builder.Services.AddTransient<IDepartment, DepartmentRep>();

// Scoped (Take One Instance With All Operation)
builder.Services.AddScoped<IDepartmentRep, DepartmentRep>();
builder.Services.AddScoped<IEmployeeRep, EmployeeRep>();
builder.Services.AddScoped<ICountryRep, CountryRep>();
builder.Services.AddScoped<ICityRep, CityRep>();
builder.Services.AddScoped<IDistrictRep, DistrictRep>();



// SingleTone (Take One Instance Shared Between All Users)
//builder.Services.AddSingleton<IDepartment, DepartmentRep>();



var app = builder.Build();


//Globalization
var supportedCultures = new[] {
                      new CultureInfo("ar-EG"),
                      new CultureInfo("en-US"),
                };

app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("en-US"),
    SupportedCultures = supportedCultures,
    SupportedUICultures = supportedCultures,
    RequestCultureProviders = new List<IRequestCultureProvider>
                {
                new QueryStringRequestCultureProvider(),
                new CookieRequestCultureProvider()
                }
});


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();
