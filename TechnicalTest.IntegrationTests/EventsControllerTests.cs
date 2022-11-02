using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using TechnicalTest.API.Models;

namespace TechnicalTest.IntegrationTests;

public class EventsControllerTests : IntegrationTest
{

    [Fact]
    public async Task Delete_event_and_response_not_found_status_code()
    {
        // Arrange
        int eventId = -1;

        // Act
        var response = await TestClient.DeleteAsync($"api/v1/Events/{eventId}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);

    }

    [Fact]
    public async Task Delete_event_and_response_conflict_status_code()
    {
        // Arrange
        int eventId = 1;

        // Act
        var response = await TestClient.DeleteAsync($"api/v1/Events/{eventId}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Conflict);

    }





}