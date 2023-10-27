using Shate.DAL.EF;
using Shate.DAL.Interfaces;
using Shate.DAL.Repositorys;

namespace Shate.DAL;

public class UnitOfWork : IDisposable
{
	private PostgreDbContext db;

    public UnitOfWork(PostgreDbContext db) => this.db = db;

    public void Save()
    {
        db.SaveChanges();
    }

    private bool disposed = false;

    public virtual void Dispose(bool disposing)
    {
        if (!disposed)
        {
            if (disposing)
            {
                db.Dispose();
            }
            disposed = true;
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}