using System.ComponentModel.DataAnnotations;

namespace Shate.DAL.Entities;

public class User : BaseEntity
{
    public User(string name, string password)
    {
        Name = name;
        Password = password;
    }

    public string Name { get; set; }
    public string Password { get; set; }

    public Computer? Computer { get; set; }
    public bool IsWorker { get; set; }
}