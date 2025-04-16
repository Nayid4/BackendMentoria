using Microsoft.EntityFrameworkCore;

namespace Mentoria.Services.Mentoring.Application.Common.DataList
{
    public class DataList<T>
    {
        public List<T> Elements { get; }
        public int Page { get; }
        public int SizePage { get; }
        public int TotalAmount { get; }
        public bool HasNextPage => (Page * SizePage) < TotalAmount;
        public bool HasPreviousPage => Page > 1;

        public DataList(List<T> elements, int page, int sizePage, int totalAmount)
        {
            Elements = elements;
            Page = page;
            SizePage = sizePage;
            TotalAmount = totalAmount;
        }

        public static async Task<DataList<T>> CreateAsync(IQueryable<T> query, int page, int sizePage)
        {
            var totalAmount = await query.CountAsync();
            var elements = await query.Skip((page - 1) * sizePage).ToListAsync();

            return new(elements, page, sizePage, totalAmount);
        }
    }
}
