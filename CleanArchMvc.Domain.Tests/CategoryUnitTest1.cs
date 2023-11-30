using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Validation;
using FluentAssertions;

namespace CleanArchMvc.Domain.Tests;

public class CategoryUnitTest1
{
    [Fact]
    public void CreateCategory_WithValidParameters_ReturnObjectWithValidState()
    {
        Action action = () => new Category(1, "Category Name");
        action.Should().NotThrow<DomainExceptionValidation>();
    }

    [Fact]
    public void CreateCategory_WithNegativeIdValue_DomainExceptionInvalidId()
    {
        Action action = () => new Category(-1, "Category Name");
        action.Should().Throw<DomainExceptionValidation>().WithMessage("Invalid id");
    }

    [Fact]
    public void CreateCategory_WithNullNamevalue_DomainExceptionInvalidName()
    {
        Action action = () => new Category(1, null);
        action.Should().Throw<DomainExceptionValidation>().WithMessage("Invalid name. Name is required");
    }

    [Fact]
    public void CreateCategory_WithEmptyNameValue_DomainExceptionInvalidName()
    {
        Action action = () => new Category(1, "");
        action.Should().Throw<DomainExceptionValidation>().WithMessage("Invalid name. Name is required");
    }

    [Fact]
    public void CreateCategory_WithShortNameValue_DomainExceptionShortName()
    {
        Action action = () => new Category(1, "Ca");
        action.Should().Throw<DomainExceptionValidation>().WithMessage("Invalid name. Too short, minimum 3 characters");
    }
}