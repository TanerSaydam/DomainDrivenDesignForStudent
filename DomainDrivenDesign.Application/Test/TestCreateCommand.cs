using MediatR;
using TS.Result;

namespace DomainDrivenDesign.Application.Test;

public sealed record TestCreateCommand(
    string TCNo) : IRequest<Result<bool>>;

internal sealed class TestCreateCommandHandler : IRequestHandler<TestCreateCommand, Result<bool>>
{
    public Task<Result<bool>> Handle(TestCreateCommand request, CancellationToken cancellationToken)
    {
        //..

        var res = Result<bool>.Succeed(true);
        return Task.FromResult(res);
        //return true;
    }
}