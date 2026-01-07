using Microsoft.EntityFrameworkCore;
using WebApiAdvance.DAL.EFCore;
using WebApiAdvance.DAL.Repositories.Abstract;
using WebApiAdvance.DAL.Repositories.Concrete.EFCore;
using WebApiAdvance.DAL.UnitOfWork.Abstract;

namespace WebApiAdvance.DAL.UnitOfWork.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApiDbContext _context;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IProductRepository _productRepository;
        public UnitOfWork(ApiDbContext context)
        {
            _context = context;
        }

        public ICategoryRepository CategoryRepository => _categoryRepository?? new EfCategoryRepository(_context);
        public IProductRepository ProductRepository => _productRepository?? new EFProductRepository(_context);
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
