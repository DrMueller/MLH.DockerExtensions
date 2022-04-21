using System;
using System.Transactions;
using Xunit;

namespace Mmu.Mlh.DockerExtensions.DatabaseTests.TestingInfrastructure.Fixtures
{
    [Collection(DatabaseCollectionFixture.CollectionName)]
    public abstract class DatabaseTestBase : IDisposable
    {
        private readonly TransactionScope _scope;

        // Called per test, aka 1 test = 1 TransactionScope
        protected DatabaseTestBase()
        {
            _scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        }

        public void Dispose()
        {
            _scope?.Dispose();
        }
    }
}