using Contracts.Enums;

namespace Contracts.Models
{
    public class CW_Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public CW_CategoryType CategoryType { get; set; }
    }
}
