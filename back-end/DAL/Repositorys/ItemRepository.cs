using System.Linq.Expressions;
using Shate.DAL.EF;
using Shate.DAL.Entities;
using Shate.DAL.Interfaces;

namespace Shate.DAL.Repositorys;

public class ItemRepository : BaseRepository<Item>, IItemRepository
{
	public ItemRepository(PostgreDbContext dbContext, UnitOfWork unitOfWork) : base(dbContext, unitOfWork)
	{
	}
}