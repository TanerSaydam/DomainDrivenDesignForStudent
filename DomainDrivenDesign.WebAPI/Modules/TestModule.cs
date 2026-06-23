using Carter;
using DomainDrivenDesign.Application.Test;
using MediatR;

namespace DomainDrivenDesign.WebAPI.Modules;

public sealed class TestModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder group)
    {
        var app = group.MapGroup("test").WithTags("Test");

        app.MapPost(string.Empty, async (
            TestCreateCommand request,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var res = await sender.Send(request, cancellationToken);
            return res.IsSuccessful ? Results.Ok(res) : Results.BadRequest(res);
        });
    }
}
