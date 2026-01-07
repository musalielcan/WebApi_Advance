using WebApiAdvance.DAL.Repositories.Abstract;

namespace WebApiAdvance.DAL.UnitOfWork.Abstract
{
    public interface IUnitOfWork
    {
        public ICategoryRepository CategoryRepository { get; }
        public IProductRepository ProductRepository { get; }
        Task SaveAsync();
    }
}
