using Xunit;

namespace Automated.Testing.System.Test
{
    [CollectionDefinition("Service controllers")]
    public class ApplicationFactoryFixtureCollection : ICollectionFixture<ApplicationFactory>
    {
        // This class has no code, and is never created. Its purpose is simply
        // to be the place to apply [CollectionDefinition] and all the
        // ICollectionFixture<> interfaces.
    }
}