using DomainDrivenDesign.Domain.Abstractions;
using DomainDrivenDesign.Domain.Categories;
using MediatR;
using TS.Result;

namespace DomainDrivenDesign.Application.Categories;

public sealed record CategoryDeleteCommand(
    Guid Id) : IRequest<Result<string>>;

internal sealed class CategoryDeleteCommandHandler(
    ICategoryRepository categoryRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<CategoryDeleteCommand, Result<string>>
{
    public async Task<Result<string>> Handle(CategoryDeleteCommand request, CancellationToken cancellationToken = default)
    {
        Category? category =
            await categoryRepository.FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);
        if (category is null)
        {
            return Result<string>.Failure("Kategori bulunamadı");
        }
        categoryRepository.Delete(category);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return "Kategori başarıyla silindi";
    }
}
