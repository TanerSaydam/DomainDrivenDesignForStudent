using Carter;
using DomainDrivenDesign.Application.Categories;
using DomainDrivenDesign.Domain.Dtos;
using MediatR;
using TS.Result;

namespace DomainDrivenDesign.WebAPI.Modules;

public sealed class CategoryModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder group)
    {
        var app = group.MapGroup("categories").WithTags("Categories");

        app.MapPost(string.Empty,
            async (CategoryCreateCommand request, ISender sender, CancellationToken cancellationToken) =>
            {
                var res = await sender.Send(request, cancellationToken);
                return res.IsSuccessful ? Results.Ok(res) : Results.BadRequest(res);
            }).Produces<Result<string>>();

        app.MapPut(string.Empty,
            async (CategoryUpdateCommand request, ISender sender, CancellationToken cancellationToken) =>
            {
                var res = await sender.Send(request, cancellationToken);
                return res.IsSuccessful ? Results.Ok(res) : Results.BadRequest(res);
            }).Produces<Result<string>>();

        app.MapDelete(string.Empty,
            async (Guid id, ISender sender, CancellationToken cancellationToken) =>
            {
                var res = await sender.Send(new CategoryDeleteCommand(id), cancellationToken);
                return res.IsSuccessful ? Results.Ok(res) : Results.BadRequest(res);
            }).Produces<Result<string>>();

        app.MapGet("{id}",
            async (Guid id, ISender sender, CancellationToken cancellationToken) =>
            {
                var res = await sender.Send(new CategoryGetQuery(id), cancellationToken);
                return res.IsSuccessful ? Results.Ok(res) : Results.BadRequest(res);
            }).Produces<Result<CategoryDto>>();

        app.MapGet(string.Empty,
            async (
                ISender sender,
                int pageNumber = 1,
                int pageSize = 10,
                string? orderField = null,
                string? orderDir = "dir",
                string? search = null,
                CancellationToken cancellationToken = default) =>
            {
                var res = await sender.Send(
                    new CategoryGetAllPaginationQuery(
                        pageNumber,
                        pageSize,
                        orderField,
                        orderDir,
                        search),
                    cancellationToken);
                return Results.Ok(res);
            }).Produces<PaginationDto<CategoryDto>>();
    }
}