using Flurl.Http;
using Workboard.WebApi.Tests.Common;
using Xunit;

namespace Workboard.WebApi.Tests;

[Collection("TestServer")]
public class WorkboardControllerTest
{
    private readonly FlurlClient _client;

    public WorkboardControllerTest(TestServerFixture factory)
    {
        _client = new FlurlClient(factory.CreateDefaultClient());
        _ = new DatabaseManager(factory.ConnectionString);
    }
}