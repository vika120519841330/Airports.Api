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
builder.Services.AddServices(builder.Configuration);

builder.Services.AddRequestDecompression();
builder.Services.AddControllers();

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
    var country2 = new Country { Iata = "RU", Name = "Russian Federation" };
    context.Countries.AddRange(country1, country2);
    context.SaveChanges();

    // Cities
    var city1 = new City { Iata = "MSQ", Name = "Minsk", Country = country1};
    var city2 = new City { Iata = "BQT", Name = "Brest", Country = country1 };
    var city3 = new City { Iata = "MOW", Name = "Moscow", Country = country2 };
    var city4 = new City { Iata = "SLY", Name = "Salekhard", Country = country2 };
    context.Cities.AddRange(city1, city2, city3, city4);
    context.SaveChanges();

    // Locations
    var location1 = new Location { Longitude = 28.032442M, Lattitude = 53.889725M };
    var location2 = new Location { Longitude = 23.898222M, Lattitude = 52.110065M };
    var location3 = new Location { Longitude = 37.416574M, Lattitude = 55.966324M };
    var location4 = new Location { Longitude = 37.899494M, Lattitude = 55.414566M };
    var location5 = new Location { Longitude = 66.591548M, Lattitude = 66.590358M };
    context.Locations.AddRange(location1, location2, location3, location4, location5);
    context.SaveChanges();

    // Airports
    var airport1 = new Airport { Icao = "UMMS", Iata = "MSQ", Name = "Minsk", City = city1, Location = location1 };
    var airport2 = new Airport { Icao = "UMBB", Iata = "BQT", Name = "Brest", City = city2, Location = location2 };
    var airport3 = new Airport { Icao = "UUEE", Iata = "SVO", Name = "Sheremetyevo", City = city3, Location = location3 };
    var airport4 = new Airport { Icao = "UUDD", Iata = "DME", Name = "Domodedovo", City = city3, Location = location4 };
    var airport5 = new Airport { Icao = "USDD", Iata = "SLY", Name = "Salekhard", City = city4, Location = location5 };
    context.Airports.AddRange(airport1, airport2, airport3, airport4, airport5);
    context.SaveChanges();
}