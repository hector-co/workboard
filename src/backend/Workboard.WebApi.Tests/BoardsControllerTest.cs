using FluentAssertions;
using Flurl.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using Workboard.Application.Abstractions.Queries;
using Workboard.Application.Commands.Boards;
using Workboard.Application.Queries.Boards;
using Workboard.WebApi.Tests.Common;
using Xunit;

namespace Workboard.WebApi.Tests;

[Collection("TestServer")]
public class BoardsControllerTest
{
    private readonly FlurlClient _client;

    public BoardsControllerTest(TestServerFixture factory)
    {
        _client = new FlurlClient(factory.CreateDefaultClient());
        _ = new DatabaseManager(factory.ConnectionString);
    }

    [Fact]
    public async Task RegisterBoardTest()
    {
        var cmd = new RegisterBoard(
            "test-board",
            new List<RegisterBoard.RegisterColumn>
            {
                new RegisterBoard.RegisterColumn("col1", 0),
                new RegisterBoard.RegisterColumn("col2", 1),
                new RegisterBoard.RegisterColumn("col3", 2)
            });

        var response = await _client.Request("workboard/boards")
            .PostJsonAsync(cmd)
            .ReceiveJson<Result<BoardDto>>();

        response.Should().NotBeNull();
        response.Data.Should().NotBeNull();
        response.Data!.Id.Should().BeGreaterThan(0);
    }
}