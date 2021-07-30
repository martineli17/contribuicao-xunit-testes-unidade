using Xunit;

namespace Testes.Base
{
    public interface IClassFixtureBase<TFixture> : IClassFixture<TFixture> where TFixture : class
    {
    }
}
