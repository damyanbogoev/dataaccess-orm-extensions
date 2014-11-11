using System;
using Telerik.OpenAccess;

namespace MayLily.DataAccess.ContextExtensions.Sample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var context = Db.Init()
                .ConnectionString(c => c.LoadFromConfig("NorthwindConnection"))
                .BackendConfiguration(b => b.LoadFromConfig("MssqlConfiguration", ConfigurationMergeMode.ConfigFileDefinitionWins))
                .MetadataContainer(m => m.Source<MyMetadataSource>().NullForeignKey())
                .FetchStrategy(fs => { })
                .CacheKey("SampleKey")
                .EnableValidation(new DataAnnotationsValidator())
                //.EnableValidation(new FluentValidationValidator(new FluentValidatorFactory()))
                .Build<MyContext>(migrateSchema: true);

            using (context)
            {
                context.Add(new Product { Name = null });
                //context.SaveChanges();

                //context.Validator = new DataAnnotationsValidator();

                //context.Add(new Product { Name = null });
                context.Add(new Category { Name = null });
                context.SaveChanges();

                //foreach (var product in context.Products)
                //{
                //    Console.WriteLine(product.Name);
                //}
            }
        }
    }
}
