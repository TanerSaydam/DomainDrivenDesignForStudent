using DomainDrivenDesign.Application.Categories;
using DomainDrivenDesign.Application.Orders;
using DomainDrivenDesign.Application.Products;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;

namespace DomainDrivenDesign.WebAPI.Controllers;

[ApiController]
[Route("odata")]
[EnableQuery]
public sealed class AppODataController(
    ISender sender) : ODataController
{
    public static IEdmModel GetEdmModel()
    {
        var builder = new ODataConventionModelBuilder();
        builder.EntitySet<CategoryDto>("categories");
        builder.EntitySet<ProductDto>("products");
        builder.EntitySet<OrderDto>("orders");
        return builder.GetEdmModel();
    }

    [HttpGet("categories")]
    public async Task<IQueryable<CategoryDto>> Categories(CancellationToken cancellationToken)
    {
        var res = await sender.Send(new CategoryGetAllODataQuery(), cancellationToken);

        return res;
    }


    [HttpGet("products")]
    public async Task<IQueryable<ProductDto>> Products(CancellationToken cancellationToken)
    {
        var res = await sender.Send(new ProductGetAllQuery(), cancellationToken);

        return res;
    }

    [HttpGet("orders")]
    public async Task<IQueryable<OrderDto>> Orders(CancellationToken cancellationToken)
    {
        var res = await sender.Send(new OrderGetAllQuery(), cancellationToken);

        return res;
    }
}
