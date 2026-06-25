using Carter;
using DomainDrivenDesign.Application.Orders;
using MediatR;
using TS.Result;

namespace DomainDrivenDesign.WebAPI.Modules;

public sealed class OrderModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder group)
    {
        var app = group.MapGroup("orders").WithTags("orders");

        app.MapPost(string.Empty,
            async (OrderCreateCommand request, ISender sender, CancellationToken cancellationToken) =>
            {
                var res = await sender.Send(request, cancellationToken);
                return res.IsSuccessful ? Results.Ok(res) : Results.BadRequest(res);
            }).Produces<Result<string>>();

        app.MapPut(string.Empty,
            async (OrderUpdateCommand request, ISender sender, CancellationToken cancellationToken) =>
            {
                var res = await sender.Send(request, cancellationToken);
                return res.IsSuccessful ? Results.Ok(res) : Results.BadRequest(res);
            }).Produces<Result<string>>();
    }
}