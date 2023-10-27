using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Shate.DAL.Interfaces;

namespace Shate.DAL.Services;

public interface IRepositoryService
{
	UnitOfWork _context
	{
		get
		{
			return new UnitOfWork();
		}
	}
}