using DomainDrivenDesign.Domain.Abstractions;
using DomainDrivenDesign.Domain.Categories;
using DomainDrivenDesign.Domain.Shared;
using FluentValidation;
using MediatR;
using TS.Result;

namespace DomainDrivenDesign.Application.Categories;

public sealed record CategoryCreateCommand(
    string Name) : IRequest<Result<string>>;

public sealed class CategoryCreateCommandValidator : AbstractValidator<CategoryCreateCommand>
{
    public CategoryCreateCommandValidator()
    {
        RuleFor(p => p.Name).NotEmpty().WithMessage("İsim boş olamaz");
        RuleFor(p => p.Name).MinimumLength(3).WithMessage("Kategori adı en az 3 karakter olmalıdır");
    }
}

internal sealed class CategoryCreateCommandHandler(
    ICategoryRepository categoryRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<CategoryCreateCommand, Result<string>>
{
    public async Task<Result<string>> Handle(CategoryCreateCommand request, CancellationToken cancellationToken)
    {
        Name name = new Name(request.Name);
        Category category = Category.Create(name);
        categoryRepository.Add(category);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return "Kategori başarıyla oluşturuldu";
    }
}