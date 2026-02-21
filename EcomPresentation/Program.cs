using EcomInfrastructure.DataContext;
using EcomPresentation.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCustomService();
builder.Services.AddCustomDatabase(builder.Configuration);
builder.Services.AddCustomCors(builder.Configuration);
builder.Services.AddControllers();
builder.Services.RegisterJwtServices(builder.Configuration);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();
await app.SeedDataAsync();
await app.SeedProduct();


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
