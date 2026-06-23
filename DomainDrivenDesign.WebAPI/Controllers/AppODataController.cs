using DomainDrivenDesign.Application.Categories;
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
        return builder.GetEdmModel();
    }

    [HttpGet("categories")]
    public async Task<IQueryable<CategoryDto>> Categories(CancellationToken cancellationToken)
    {
        var res = await sender.Send(new CategoryGetAllODataQuery(), cancellationToken);

        return res;
    }
}
