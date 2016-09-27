using System.Linq;
using MayLily.DataAccess.ContextExtensions;

namespace MayLily.DataAccess.FluentMigrator.Sample
{
    public class MyContext : DataAccessContext
    {
        public MyContext(DataAccessContext context)
            : base(context)
        {
        }

        public IQueryable<Product> Products
        {
            get
            {
                return this.GetAll<Product>();
            }
        }
    }
}
