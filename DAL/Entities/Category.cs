namespace Shate.DAL.Entities
{
    public class Category : BaseEntity
    {
        public string? Name { get; set; }
        public int ParentId { get; set; }
        public bool IsMain { get; set; }
    }
}
