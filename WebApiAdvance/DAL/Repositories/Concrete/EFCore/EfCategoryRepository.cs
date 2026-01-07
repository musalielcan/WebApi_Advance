using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebApiAdvance.Core.DAL.Repositories.Concrete.EFCore;
using WebApiAdvance.DAL.EFCore;
using WebApiAdvance.DAL.Repositories.Abstract;
using WebApiAdvance.Entities;

namespace WebApiAdvance.DAL.Repositories.Concrete.EFCore
{
    public class EfCategoryRepository : EFBaseRepository<Category, ApiDbContext>, ICategoryRepository
    {
        public EfCategoryRepository(ApiDbContext context) : base(context)
        {

        }
    }
}
