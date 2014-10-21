using System;
using Telerik.OpenAccess.FetchOptimization;

namespace MayLily.DataAccess.ContextExtensions
{
    public class FetchStrategyFluentConfigurator : IFluentConfigurator<FetchStrategy>, IFetchStrategyFluentConfigurator
    {
        private FetchStrategy strategy;

        public IFetchStrategyFluentConfigurator Set(FetchStrategy strategy)
        {
            this.strategy = strategy;

            return this;
        }

        public IFetchStrategyFluentConfigurator Options(Action<FetchStrategy> action)
        {
            if (this.strategy == null)
            {
                this.strategy = new FetchStrategy();
            }

            action(this.strategy);

            return this;
        }

        public FetchStrategy Build()
        {
            return this.strategy;
        }
    }
}
