using DomainDrivenDesign.Application.Test;
using MediatR;
using TS.Result;

namespace DomainDrivenDesign.Application.Behaviors;

public sealed class TCKontrolBehavior : IPipelineBehavior<TestCreateCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(TestCreateCommand request, RequestHandlerDelegate<Result<bool>> next, CancellationToken cancellationToken)
    {
        //metot çalışmadan önce
        var res = await next();

        // metot tamamladıktan sonra
        return res;
    }
}
