using WebApiAdvance.Core.DAL.Repositories.Concrete.EFCore;
using WebApiAdvance.DAL.EFCore;
using WebApiAdvance.DAL.Repositories.Abstract;
using WebApiAdvance.Entities;

namespace WebApiAdvance.DAL.Repositories.Concrete.EFCore
{
    public class EFProductRepository: EFBaseRepository<Product,ApiDbContext>, IProductRepository
    {
        public EFProductRepository(ApiDbContext context) : base(context)
        {
        }
    }
}
