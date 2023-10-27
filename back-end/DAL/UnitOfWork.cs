using Shate.DAL.EF;
using Shate.DAL.Repositorys;
using Shate.DAL.Services;

namespace Shate.DAL;

public class UnitOfWork : IDisposable, IUnitOfWork
{
    private PostgreDbContext db = new();
    private ItemRepository itemRepository;
    private CategoryRepository categoryRepository;
    private ComputerRepository computerRepository;

	public ItemRepository Items
    {
        get
        {
            if (itemRepository == null)
	            itemRepository = new ItemRepository(db);
            return itemRepository;
        }
    }

    public CategoryRepository Categories
    {
        get
        {
            if (categoryRepository == null)
	            categoryRepository = new CategoryRepository(db);
            return categoryRepository;
        }
    }

    public ComputerRepository Computers
    {
	    get
	    {
            if(computerRepository == null)
                computerRepository = new ComputerRepository(db);
            return computerRepository;
	    }
    }
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

    public UnitOfWork GetUnitOfWork()
    {
	    return this;
    }
}