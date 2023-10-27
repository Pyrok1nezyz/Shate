using System.ComponentModel.DataAnnotations.Schema;

namespace Shate.DAL.Entities
{
    public class Item : BaseEntity
    {
        public string? Name { get; set; }
        public Category? MainCategory { get; set; }
        public Category? SubCategory { get; set; }
        public int Price { get; set; }
        public int Count { get; set; }
        public bool IsForceBuy { get; set; }

        public List<int>? CountryId { get; set; }
        public bool IsDiscounted { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsHided { get; set; }
        public int? CustomerId { get; set; }
    }
}
