using System.Linq;
using Telerik.OpenAccess;

namespace MayLily.DataAccess.ContextExtensions.Sample
{
    public class MyContext : OpenAccessContext
    {
        public MyContext(OpenAccessContext context)
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
