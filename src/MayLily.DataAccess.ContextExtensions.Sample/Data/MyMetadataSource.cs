using System.Collections.Generic;
using Telerik.OpenAccess.Metadata;
using Telerik.OpenAccess.Metadata.Fluent;

namespace MayLily.DataAccess.ContextExtensions.Sample
{
    public class MyMetadataSource : FluentMetadataSource
    {
        protected override IList<MappingConfiguration> PrepareMapping()
        {
            var result = new List<MappingConfiguration>();
            result.Add(GetProductMapping());
            result.Add(GetCategoryMapping());

            return result;
        }

        private MappingConfiguration<Product> GetProductMapping()
        {
            var result = new MappingConfiguration<Product>();
            result.MapType(x => new
            {
                Id = x.Id,
                ProductName = x.Name
            }).ToTable("Products");

            result.HasProperty(x => x.Id)
                .IsIdentity(KeyGenerator.Autoinc);

            return result;
        }

        private MappingConfiguration<Category> GetCategoryMapping()
        {
            var result = new MappingConfiguration<Category>();
            result.MapType(x => new
            {
                Id = x.Id,
                CategoryName = x.Name
            }).ToTable("Categories");

            result.HasProperty(x => x.Id).IsIdentity(KeyGenerator.Autoinc);
            result.HasAssociation(x => x.Products)
                .WithOpposite(y => y.Category)
                .IsManaged()
                .ToColumn("CategoryId")
                .HasConstraint();

            return result;
        }
    }
}
