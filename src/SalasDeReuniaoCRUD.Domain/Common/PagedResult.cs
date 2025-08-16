using SalasDeReuniaoCRUD.Domain.Core.Model;

namespace SalasDeReuniaoCRUD.Domain.Common
{
    public class PagedResult<T>
    {
        public IEnumerable<T> Items { get; set; } = Enumerable.Empty<T>();
        public int TotalPages { get; set; }
        public int TotalCount { get; set; }
    }
}
