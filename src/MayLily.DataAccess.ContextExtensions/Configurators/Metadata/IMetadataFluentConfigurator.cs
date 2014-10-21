using Telerik.OpenAccess.Metadata;
using Telerik.OpenAccess.Metadata.Fluent;

namespace MayLily.DataAccess.ContextExtensions
{
    public interface IMetadataFluentConfigurator
    {
        IMetadataFluentConfigurator Container(MetadataContainer container);
        IMetadataFluentConfigurator Source(MetadataSource source);
        IMetadataFluentConfigurator Source<TSource>() where TSource : FluentMetadataSource, new();
        IMetadataFluentConfigurator NullForeignKey();
    }
}
