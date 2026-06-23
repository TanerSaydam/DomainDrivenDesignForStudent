namespace DomainDrivenDesign.Domain.Dtos;

public sealed record PaginationRequestDto(
    int PageNumber = 1,
    int PageSize = 10,
    string? OrderField = null,
    string? OrderDir = "asc",
    string? Search = null);