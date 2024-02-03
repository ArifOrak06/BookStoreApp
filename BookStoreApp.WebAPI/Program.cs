using BookStoreApp.Persistence.Extensions.Microsoft;
using BookStoreApp.Service.Extensions.Microsoft;
using NLog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson(); // json üzerinde maniplasyon yapabilmek için.


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//register of Persistence Repositories 
builder.Services.ConfigureDbContext(builder.Configuration);
builder.Services.ConfigurePersistenceRepositories();


//register of Servies Layer Services
builder.Services.ConfigureServicesForBusinesLayer();

// nlog configuration
LogManager.LoadConfiguration(String.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));


var app = builder.Build();

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
