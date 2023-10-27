using System.ComponentModel.DataAnnotations;

namespace Shate.DAL;

public abstract class BaseEntity
{
    public Guid Id { get; set; }
}