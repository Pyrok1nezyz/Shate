using Shate.DAL.Interfaces;

namespace Shate.DAL.Services;

public class UnitOfWorkService : IUnitOfWork
{
	public UnitOfWork _context;
}