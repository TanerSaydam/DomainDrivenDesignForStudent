using Carter;

namespace DomainDrivenDesign.WebAPI.Modules;

public sealed class TestModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder group)
    {
        var app = group.MapGroup("test").WithTags("Test");
        //app.MapGet(string.Empty, async (IOrderRepository orderRepository) =>
        //{
        //    Guid id = Guid.Parse("019f0010-c043-7792-a02c-73822335024b");
        //    var order = await orderRepository.FirstOrDefaultAsync(p => p.Id == id)!;
        //    order.Items.Where(i => i.ProductId == , i.Stock == 1);
        //})
    }
}
