using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MayLily.DataAccess.FluentMigrator.Sample
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
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
