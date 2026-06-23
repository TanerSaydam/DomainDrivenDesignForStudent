using DomainDrivenDesign.Domain.Abstractions;
using DomainDrivenDesign.Domain.Categories;
using DomainDrivenDesign.Domain.Shared;
using FluentValidation;
using MediatR;
using TS.Result;

namespace DomainDrivenDesign.Application.Categories;

public sealed record CategoryUpdateCommand(
    Guid Id,
    string Name) : IRequest<Result<string>>;

public sealed class CategoryUpdateCommandValidator : AbstractValidator<CategoryUpdateCommand>
{
    public CategoryUpdateCommandValidator()
    {
        RuleFor(p => p.Name).NotEmpty().WithMessage("İsim boş olamaz");
        RuleFor(p => p.Name).MinimumLength(3).WithMessage("Kategori adı en az 3 karakter olmalıdır");
    }
}

internal sealed class CategoryUpdateCommandHandler(
    ICategoryRepository categoryRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<CategoryUpdateCommand, Result<string>>
{
    public async Task<Result<string>> Handle(CategoryUpdateCommand request, CancellationToken cancellationToken = default)
    {
        Category? category =
            await categoryRepository.FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);
        if (category is null)
        {
            return Result<string>.Failure("Kategori bulunamadı");
        }
        Name name = new Name(request.Name);
        category.SetName(name);
        categoryRepository.Update(category);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return "Kategori başarıyla güncellendi";
    }
}