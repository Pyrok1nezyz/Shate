using Microsoft.EntityFrameworkCore;
using Shate.DAL.EF;
using Shate.DAL.Entities;
using Shate.DAL.Interfaces;

namespace Shate.DAL.Repositorys;

public class ComputerRepository : BaseRepository<Computer>, IComputerRepository
{
	public ComputerRepository(PostgreDbContext dbContext,UnitOfWork unitOfWork) : base(dbContext, unitOfWork)
	{
	}
}