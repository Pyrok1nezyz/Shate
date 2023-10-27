using Microsoft.EntityFrameworkCore;
using Shate.DAL.Interfaces;
using System.Linq.Expressions;
using Shate.DAL.EF;
using System.Threading;
using Shate.DAL.Entities;

namespace Shate.DAL.Repositorys;

public abstract class BaseRepository<T> : IDisposable, IBaseRepository<T> where T : class
{
	private PostgreDbContext _context;
	private DbSet<T> _table;
	protected PostgreDbContext Context => _context;

	public IQueryable<T> Table => _table;

	public BaseRepository(PostgreDbContext dbContext)
	{
		_context = dbContext;
		_table = _context.Set<T>();
	}

	public IQueryable<T> FindAll() => _table.AsNoTracking();
	public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression) => _table.Where(expression).AsNoTracking();

	public async Task<T> GetRecord(int id, CancellationToken cancellationToken) => await _table.FindAsync(id, cancellationToken);
	public async Task<T> GetRecord(int id) => await _table.FindAsync(id);

	public async Task<T> AddRecord(T entity, CancellationToken cancellationToken)
	{
		await _table.AddAsync(entity);
		return entity;
	}

	public Task<T> AddRecord(T entity)
	{
		_table.Add(entity);
	}

	public void UpdateRecord(T entity) => _table.Update(entity);

	public void DeleteRecord(T entity) => _table.Remove(entity);

	public void Dispose()
	{
		_context?.Dispose();
	}

}