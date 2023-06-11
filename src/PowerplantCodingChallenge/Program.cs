using System.Text.Json;
using System.Text.Json.Serialization;
using PowerplantCodingChallenge.Application;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
// Add services to the container.
builder.WebHost.UseUrls("http://*:8888");
builder.Services.AddControllers()
	.AddJsonOptions(options =>
{
	// Global settings: use the defaults, but serialize enums as strings
	// (because it really should be the default)
	options.JsonSerializerOptions.Converters.Add(
		new JsonStringEnumConverter(JsonNamingPolicy.CamelCase, false));
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplication();

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

