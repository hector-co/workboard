using Xunit;

namespace Workboard.WebApi.Tests.Common;

[CollectionDefinition("TestServer")]
public class TestServerCollection : ICollectionFixture<TestServerFixture>
{
}
