using System;
using Telerik.OpenAccess.Metadata;
using Telerik.OpenAccess.Metadata.Fluent;

namespace MayLily.DataAccess.ContextExtensions
{
    public class MetadataFluentConfigurator : IFluentConfigurator<MetadataContainer>, IMetadataFluentConfigurator
    {
        private MetadataContainer container;
        private MetadataSource source;
        private bool nullForeignKey;

        public MetadataContainer Build()
        {
            if (this.container != null)
            {
                return this.ApplyOptions(this.container);
            }

            if (this.source != null)
            {
                return this.ApplyOptions(this.source.GetModel());
            }

            throw new InvalidOperationException("No metadata container is specified.");
        }

        private MetadataContainer ApplyOptions(MetadataContainer container)
        {
            container.DefaultMapping.NullForeignKey = this.nullForeignKey;

            return container;
        }

        public IMetadataFluentConfigurator Container(MetadataContainer container)
        {
            this.container = container;

            return this;
        }

        public IMetadataFluentConfigurator Source(MetadataSource source)
        {
            this.source = source;

            return this;
        }

        public IMetadataFluentConfigurator Source<TSource>() where TSource : FluentMetadataSource, new()
        {
            this.source = Activator.CreateInstance<TSource>();

            return this;
        }

        public IMetadataFluentConfigurator NullForeignKey()
        {
            this.nullForeignKey = true;

            return this;
        }
    }
}
