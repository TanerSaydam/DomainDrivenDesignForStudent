using Carter;
using DomainDrivenDesign.Application.Products;
using MediatR;
using TS.Result;

namespace DomainDrivenDesign.WebAPI.Modules;

public sealed class ProductModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder group)
    {
        var app = group.MapGroup("products").WithTags("Products");
        app.MapPost(string.Empty,
            async (ProductCreateCommand request, ISender sender, CancellationToken cancellationToken) =>
            {
                var res = await sender.Send(request, cancellationToken);
                return res.IsSuccessful ? Results.Ok(res) : Results.BadRequest(res);
            }).Produces<Result<string>>();

        //app.MapPut(string.Empty,
        //    async (ProductUpdateCommand request, ISender sender, CancellationToken cancellationToken) =>
        //    {
        //        var res = await sender.Send(request, cancellationToken);
        //        return res.IsSuccessful ? Results.Ok(res) : Results.BadRequest(res);
        //    }).Produces<Result<string>>();

        //app.MapDelete(string.Empty,
        //    async (Guid id, ISender sender, CancellationToken cancellationToken) =>
        //    {
        //        var res = await sender.Send(new ProductDeleteCommand(id), cancellationToken);
        //        return res.IsSuccessful ? Results.Ok(res) : Results.BadRequest(res);
        //    }).Produces<Result<string>>();
    }
}