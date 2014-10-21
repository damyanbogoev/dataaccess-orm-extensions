using System;
using Telerik.OpenAccess;

namespace MayLily.DataAccess.ContextExtensions.Sample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var context = Db.Init()
                .ConnectionString(c => c.LoadFromConfig("MyConnection"))
                .BackendConfiguration(b => b.LoadFromConfig("MssqlConfiguration", ConfigurationMergeMode.ConfigFileDefinitionWins))
                .MetadataContainer(m => m.Source<MyMetadataSource>().NullForeignKey())
                .FetchStrategy(fs => { })
                .CacheKey("SampleKey")
                .Build<MyContext>(migrateSchema: true);

            using (context)
            {
                //context.Add(new Product { Name = "Chocolate" });
                //context.SaveChanges();

                foreach (var product in context.Products)
                {
                    Console.WriteLine(product.Name);
                }
            }
        }
    }
}
