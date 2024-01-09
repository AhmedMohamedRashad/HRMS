using Demo.BL.Interface;
using Demo.BL.Mapper;
using Demo.BL.Repository;
using Demo.DAL.Database;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();


var connectionString = builder.Configuration.GetConnectionString("DemoConnection");
builder.Services.AddDbContext<DemoContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddAutoMapper(x => x.AddProfile(new DomainProfile()));

// Transient (Take Instance With Every Operation)
//builder.Services.AddTransient<IDepartment, DepartmentRep>();

// Scoped (Take One Instance With All Operation)
builder.Services.AddScoped<IDepartmentRep, DepartmentRep>();
builder.Services.AddScoped<IEmployeeRep, EmployeeRep>();

var app = builder.Build();

app.UseCors(options => options
.WithOrigins()
.AllowAnyMethod()
.AllowAnyHeader());


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
