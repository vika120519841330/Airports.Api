using Airports.Api.Configs;
using Airports.DataAccess.Contexts;
using Airports.DataAccess.Models.DbModels;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

var builder = WebApplication.CreateBuilder(args);


builder.Logging.ClearProviders();
builder.Logging.AddSerilog();
builder.Host.ConfigureSeqSerilog(builder.Environment);
builder.Environment.ConfigureSeqSerilog();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddDBContext(builder.Configuration);
builder.Services.AddRepositories();
builder.Services.AddServices();
builder.Services.AddRequestDecompression();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

PrimaryDataInit(app);

app.Run();

static void PrimaryDataInit(WebApplication app)
{
    using var context = app.Services.GetService<AppDbContext>();
    context.Database.EnsureDeleted();
    context.Database.EnsureCreated();

    // Countries
    var country1 = new Country { Iata = "BY", Name = "Belarus" };
    var country2 = new Country { Iata = "RU", Name = "Russia" };
    context.Countries.AddRange(country1, country2);
    context.SaveChanges();

    // Cities
    var city1 = new City { Iata = "MSQ", Name = "Minsk", Country = country1};
    var city2 = new City { Iata = "BQT", Name = "Brest", Country = country1 };
    var city3 = new City { Iata = "MOW", Name = "Moscow", Country = country2 };
    var city4 = new City { Iata = "SLY", Name = "Salekhard", Country = country2 };
    context.Cities.AddRange(city1, city2, city3, city4);
    context.SaveChanges();

    // Airports
    var airport1 = new Airport { Icao = "UMMS", Iata = "MSQ", Name = "Minsk National Airport", City = city1};
    var airport2 = new Airport { Icao = "UMBB", Iata = "BQT", Name = "Brest Airport", City = city2 };
    var airport3 = new Airport { Icao = "UUEE", Iata = "SVO", Name = "Sheremetyevo International Airport", City = city3 };
    var airport4 = new Airport { Icao = "UUDD", Iata = "DME", Name = "Domodedovo International Airport", City = city3 };
    var airport5 = new Airport { Icao = "USDD", Iata = "SLY", Name = "Salekhard Airport", City = city4 };
    context.Airports.AddRange(airport1, airport2, airport3, airport4, airport5);
    context.SaveChanges();
}