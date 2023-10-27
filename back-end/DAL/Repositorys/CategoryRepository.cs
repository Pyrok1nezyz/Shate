using Shate.DAL.EF;
using Shate.DAL.Entities;
using Shate.DAL.Interfaces;

namespace Shate.DAL.Repositorys;

public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
{
	public CategoryRepository(PostgreDbContext dbContext, UnitOfWork unitOfWork) : base(dbContext, unitOfWork)
	{
	}
}