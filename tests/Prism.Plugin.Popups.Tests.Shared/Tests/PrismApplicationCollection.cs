using Xunit;

namespace Prism.Plugin.Popups.Tests.Fixtures
{
    [CollectionDefinition(nameof(PrismApp), DisableParallelization = true)]
    public class PrismApplicationCollection : ICollectionFixture<PrismApp>
    {

    }
}
