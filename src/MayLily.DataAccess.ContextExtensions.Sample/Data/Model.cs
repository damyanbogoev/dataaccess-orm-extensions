using System.Collections.Generic;

namespace MayLily.DataAccess.ContextExtensions.Sample
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Category Category { get; set; }
    }

    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public IList<Product> Products { get; set; }
    }
}
