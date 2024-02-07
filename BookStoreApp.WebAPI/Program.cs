using BookStoreApp.Core.Services;
using BookStoreApp.Persistence.Extensions.Microsoft;
using BookStoreApp.Service.Extensions.Microsoft;
using BookStoreApp.WebAPI.ActionFilters;
using BookStoreApp.WebAPI.Extensions;
using BookStoreApp.WebAPI.Extensions.Microsoft;
using Microsoft.AspNetCore.Mvc;
using NLog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(config =>
{
    config.RespectBrowserAcceptHeader = true; // API uygulamam�z� i�erik pazarl���na a�t�k.
    config.ReturnHttpNotAcceptable = true; // Desteklemedi�imiz i�erik taleplerine Status code 406 Not Acceptable olarak d�n�� yapmas�n� belirttik.
}).AddXmlDataContractSerializerFormatters().AddNewtonsoftJson(); // json �zerinde maniplasyon yapabilmek i�in.

// Custom validation Confguring
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//register of Persistence Repositories 
builder.Services.ConfigureDbContext(builder.Configuration);
builder.Services.ConfigurePersistenceRepositories();


//register of Servies Layer Services
builder.Services.ConfigureServicesForBusinesLayer();

//ActionFilters Registration
builder.Services.ConfigureActionFilters();


// nlog configuration
LogManager.LoadConfiguration(String.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));


var app = builder.Build();
// Using global Exception Handler
// uygulamay� aya�a kald�rd�ktan sonra ihtiya� duyulan servisi a�a��daki gibi �a��rabiliriz.


var logger = app.Services.GetRequiredService<ILoggerService>();

app.ConfigureExceptionHandler(logger);


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


if (app.Environment.IsProduction())
{
    app.UseHsts();
}



app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
