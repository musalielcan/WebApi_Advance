using System.Linq.Expressions;
using WebApiAdvance.Controllers;
using WebApiAdvance.Core.DAL.Repositories.Abstract;
using WebApiAdvance.Entities;

namespace WebApiAdvance.DAL.Repositories.Abstract
{
    public interface ICategoryRepository: IRepository<Category>
    {
    }
}
