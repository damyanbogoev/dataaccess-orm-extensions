using System.Linq;
using Telerik.OpenAccess;

namespace MayLily.DataAccess.ContextExtensions.Sample
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
