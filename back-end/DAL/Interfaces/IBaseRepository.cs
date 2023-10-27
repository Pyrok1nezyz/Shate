using System.Linq.Expressions;
using Shate.DAL.Entities;

namespace Shate.DAL.Interfaces;

public interface IBaseRepository<T> where T : class
{
	IQueryable<T> Table { get; }

	IQueryable<T> FindAll();
	IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression);
	Task<T> GetRecord(int id, CancellationToken cancellationToken);
	Task<T> GetRecord(int id);
	Task<T> AddRecord(T entity, CancellationToken cancellationToken);
	Task<T> AddRecord(T entity);
	void UpdateRecord(T entity);
	void DeleteRecord(T entity);
}