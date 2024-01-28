using Sentinel.PolicyValidator.BackgroundServices;
using Sentinel.PolicyValidator.ValidationReaders;
using Sentinel.Core.K8s.Middlewares;
using Sentinel.Core.K8s;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<JsonValidationOptions>(
    builder.Configuration.GetSection(JsonValidationOptions.JsonValidationLocation));

builder.Services.AddCoreK8s(builder.Configuration);

builder.Services.AddSingleton<IServiceCollection>(builder.Services);
builder.Services.AddSingleton<IServiceProvider>(builder.Services.BuildServiceProvider());

builder.Services.AddSingleton<IValidationReader, JsonValidationReader>();
builder.Services.AddSingleton<ValidationRuleAggregator>();

builder.Services.AddValidationBackgroundServices();




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

//app.Services.GetService<ValidationBackgroundServiceFactory>();

app.Run();
