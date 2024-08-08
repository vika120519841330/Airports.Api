using Airports.BusinessLayer.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit.Abstractions;

namespace Airports.Api.Test.ControllerTests;

public class TestAirportsController : TestServiceBase
{
    public TestAirportsController(ITestOutputHelper output) : base(output)
    {
    }

    private string firstAirport = string.Empty;
    private string secondAirport = string.Empty;
    private double? distanceWated = default;

    Func<double?, double?, bool> IsDistanceValid = (double? roundedValue, double? distanceWated)
        => (!distanceWated.HasValue && !roundedValue.HasValue)
        || (distanceWated.HasValue
        && roundedValue.HasValue
        && (roundedValue == distanceWated || roundedValue == distanceWated - 1 || roundedValue == distanceWated + 1));
    private async Task<AirportsService> GetAirportsService()
    {
        // Init AppRepositories
        var repo = GetAppRepositories();

        // Init IOption
        var options = GetTeleportHostConfigurations();

        // Init httpService
        var httpService = GetIHttpService();

        // Init ApiService
        var apiService = new Mock<AirportsService>(repo, options, httpService);
        return apiService.Object;
    }

    [Fact]
    public async Task GetEmployeeAssignmentsTest_ExistInFakeRepo()
    {
        firstAirport = "MSQ";
        secondAirport = "SVO";
        distanceWated = 400;

        var actionResult = await GetEmployeeAssignmentsTest();

        // Assert
        Assert.NotNull(actionResult);
        var roundedValue = Math.Round(actionResult.Value, 0);
        Assert.True(IsDistanceValid(roundedValue, distanceWated));

        // Write to console
        Output.WriteLine($"Distance between {firstAirport}-{secondAirport} {actionResult.Value} mile. ");
    }

    [Fact]
    public async Task GetEmployeeAssignmentsTest_NotExistInFakeRepo()
    {
        firstAirport = "MSQ";
        secondAirport = "bax";
        distanceWated = 2216;

        var actionResult = await GetEmployeeAssignmentsTest();

        // Assert
        Assert.NotNull(actionResult);
        var roundedValue = Math.Round(actionResult.Value, 0);
        Assert.True(IsDistanceValid(roundedValue, distanceWated));

        // Write to console
        Output.WriteLine($"Distance between {firstAirport}-{secondAirport} {actionResult.Value} mile. ");
    }

    private async Task<double?> GetEmployeeAssignmentsTest()
    {
        // Arrange
        var service = await GetAirportsService();

        // Act
        var action = async () => await service.GetDistance(firstAirport, secondAirport, default);
        return await action.Invoke();
    }
}
