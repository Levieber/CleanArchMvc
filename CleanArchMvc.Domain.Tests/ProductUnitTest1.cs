using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Validation;
using FluentAssertions;

namespace CleanArchMvc.Domain.Tests;

public class ProductUnitTest1
{
    [Fact]
    public void CreateProduct_WithValidParameters_ReturnObjectWithValidState()
    {
        Action action = () => new Product(1, "Product Name", "Product Description", 9.99m, 99, "product image");
        action.Should().NotThrow<DomainExceptionValidation>();
    }

    [Fact]
    public void CreateProduct_WithNegativeIdValue_DomainExceptionInvalidId()
    {
        Action action = () => new Product(-1, "Product Name", "Product Description", 9.99m, 99, "product image");
        action.Should().Throw<DomainExceptionValidation>().WithMessage("Invalid id");
    }

    [Fact]
    public void CreateProduct_WithNullNameValue_DomainExceptionInvalidName()
    {
        Action action = () => new Product(1, null, "Product Description", 9.99m, 99, "product image");
        action.Should().Throw<DomainExceptionValidation>().WithMessage("Invalid name. Name is required");
    }

    [Fact]
    public void CreateProduct_WithEmptyNameValue_DomainExceptionInvalidName()
    {
        Action action = () => new Product(1, "", "Product Description", 9.99m, 99, "product image");
        action.Should().Throw<DomainExceptionValidation>().WithMessage("Invalid name. Name is required");
    }

    [Fact]
    public void CreateProduct_WithShortNameValue_DomainExceptionShortName()
    {
        Action action = () => new Product(1, "Pr", "Product Description", 9.99m, 99, "product image");
        action.Should().Throw<DomainExceptionValidation>().WithMessage("Invalid name. Too short, minimum 3 characters");
    }

    [Fact]
    public void CreateProduct_WithLongImageName_DomainExceptionLongImageName()
    {
        Action action = () => new Product(1, "Product Name", "Product Description", 9.99m, 99, "product image tttttttttttttttttttttttttttttttttttttttttooooooooooooooooooooooooooooooooooooooooooooooooo llllllllllllllllllllllllllllllllllllllllllllllooooooooooooooooooooooooooooooooooooooooooongggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggg");
        action.Should().Throw<DomainExceptionValidation>().WithMessage("Invalid image name. Too long, maximum 250 characters");
    }

    [Fact]
    public void CreateProduct_WithNullImageName_NoDomainException()
    {
        Action action = () => new Product(1, "Product Name", "Product Description", 9.99m, 99, null);
        action.Should().NotThrow<DomainExceptionValidation>();
    }

    [Fact]
    public void CreateProduct_WithNullImageName_NoNullReferenceException()
    {
        Action action = () => new Product(1, "Product Name", "Product Description", 9.99m, 99, null);
        action.Should().NotThrow<NullReferenceException>();
    }

    [Fact]
    public void CreateProduct_WithEmptyImageName_NoDomainException()
    {
        Action action = () => new Product(1, "Product Name", "Product Description", 9.99m, 99, "");
        action.Should().NotThrow<DomainExceptionValidation>();
    }

    [Theory]
    [InlineData(-5)]
    public void CreateProduct_InvalidStockValue_ExceptionDomainStockNegativeValue(int value)
    {
        Action action = () => new Product(1, "Product Name", "Product Description", 9.99m, value, "");
        action.Should().Throw<DomainExceptionValidation>().WithMessage("Invalid stock. Must be positive");
    }

    [Theory]
    [InlineData(-5)]
    public void CreateProduct_InvalidPriceValue_ExceptionDomainPriceNegativeValue(decimal value)
    {
        Action action = () => new Product(1, "Product Name", "Product Description", value, 99, "");
        action.Should().Throw<DomainExceptionValidation>().WithMessage("Invalid price. Must be positive");
    }
}
