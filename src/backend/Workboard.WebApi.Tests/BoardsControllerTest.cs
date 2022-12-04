using FluentAssertions;
using Flurl.Http;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Workboard.Application.Abstractions.Queries;
using Workboard.Application.Commands.Boards;
using Workboard.Application.Queries.BoardItems;
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
        var cmd = new RegisterBoard("test-board");


        var response = await _client.Request("workboard/boards")
            .PostJsonAsync(cmd)
            .ReceiveJson<Result<BoardDto>>();


        response.Should().NotBeNull();
        response.Data.Should().NotBeNull();
        response.Data!.Id.Should().BeGreaterThan(0);

        response.Data!.Columns.Count.Should().Be(3);
    }

    [Fact]
    public async Task AddBoardItemTest()
    {
        var addBoardCmd = new RegisterBoard("test-board");

        var board = (await _client.Request("workboard/boards")
            .PostJsonAsync(addBoardCmd)
            .ReceiveJson<Result<BoardDto>>())
            .Data!;

        var cmd = new AddBoardItem("card1", "", new[] { 1, 2 }.ToList(), 1, 8);


        await _client.Request($"workboard/boards/{board.Id}/items")
            .PostJsonAsync(cmd);

        var response = await _client.Request($"workboard/boards/{board.Id}/items")
            .GetJsonAsync<Result<IEnumerable<BoardItemDto>>>();


        response.Data.Should().NotBeNull();
        response.Data!.Count().Should().Be(1);
    }
}