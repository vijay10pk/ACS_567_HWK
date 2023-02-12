using HWK4;
using HWK4.Data;
using HWK4.Interfaces;
using HWK4.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json.Serialization;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddTransient<Seed>();
builder.Services.AddControllers().AddJsonOptions(x =>
    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Add connection to MySOL database
builder.Services.AddEntityFrameworkMySql()
    .AddDbContext<DataContext>(options =>
                                         options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
                                         new MySqlServerVersion(new Version()))
                                         );
//Add repository references
builder.Services.AddScoped<IExpenseAnalysisRepository, ExpenseAnalysisRepository>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

SeedData(app);

void SeedData(IHost app)
{
    var scopedFactory = app.Services.GetService<IServiceScopeFactory>();
    using (var scope = scopedFactory.CreateScope()) 
    {
        var service = scope.ServiceProvider.GetService<Seed>();
        service.SeedDataContext();

    }
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

