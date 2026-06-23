namespace DomainDrivenDesign.Domain.Dtos;

public sealed class PaginationDto<T>
    where T : class
{
    public List<T> Data { get; set; } = new();
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }
    public int TotalPageCount => (int)Math.Ceiling((decimal)TotalCount / PageSize);
}
