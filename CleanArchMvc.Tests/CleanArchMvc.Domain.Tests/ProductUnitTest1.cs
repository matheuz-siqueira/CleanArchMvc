using CleanArchMvc.Domain.Entities;

using Xunit;
using FluentAssertions;

namespace CleanArchMvc.Domain.Tests;

public class ProductUnitTest1
{
    [Fact]
    public void CreateProduct_WithValidParameters_Success()
    {
        Action action = () => new Product(1, "Product name", "description", 
            9.99m, 45, "url-image");
        
        action.Should()
            .NotThrow<CleanArchMvc.Domain.Validation.DomainExceptionValidation>();
    }

    public void CreateProduct_NegativeIdValue_DomainException()
    {
        Action action = () => new Product(-1, "Product name", "description", 
            9.99m, 45, "url-image");
        
        action.Should()
            .Throw<CleanArchMvc.Domain.Validation.DomainExceptionValidation>()
                .WithMessage("Invalid id value");
    }

    [Fact]
    public void CreateProduct_ShortNameValue_DomainException()
    {
        Action action = () => new Product(1, "Pr", "description", 
            9.99m, 45, "url-image");
        
        action.Should()
            .Throw<CleanArchMvc.Domain.Validation.DomainExceptionValidation>()
                .WithMessage("Invalid name. Minimum 3 characters");
    }

    [Fact]
    public void CreateProduct_NameEmpty_DomainException()
    {
        Action action = () => new Product(1, "", "description", 
            9.99m, 45, "url-image");
        
        action.Should()
            .Throw<CleanArchMvc.Domain.Validation.DomainExceptionValidation>()
                .WithMessage("Invalid name. Name is required");
    }
    
    [Fact]
    public void CreateProduct_NameNUll_DomainException()
    {
        Action action = () => new Product(1, null, "description", 
            9.99m, 45, "url-image");
        
        action.Should()
            .Throw<CleanArchMvc.Domain.Validation.DomainExceptionValidation>();
    }
     [Fact]
    public void CreateProduct_ShortDescriptionValue_DomainException()
    {
        Action action = () => new Product(1, "Product name", "de", 
            9.99m, 45, "url-image");
        
        action.Should()
            .Throw<CleanArchMvc.Domain.Validation.DomainExceptionValidation>()
                .WithMessage("Invalid description. Minimum 3 characters");
    }

    [Fact]
    public void CreateProduct_DescriptionEmpty_DomainException()
    {
        Action action = () => new Product(1, "Product name", "", 
            9.99m, 45, "url-image");
        
        action.Should()
            .Throw<CleanArchMvc.Domain.Validation.DomainExceptionValidation>()
                .WithMessage("Invalid description. Description is required");
    }
    
    [Fact]
    public void CreateProduct_DescriptionNUll_DomainException()
    {
        Action action = () => new Product(1, "Product name", null, 
            9.99m, 45, "url-image");
        
        action.Should()
            .Throw<CleanArchMvc.Domain.Validation.DomainExceptionValidation>(); 
    }

    [Fact]
    public void CreateProduct_InvalidPriceValue_DomainException()
    {
        Action action = () => new Product(1, "Product name", "description", 
            -9.99m, 45, "url-image");
        
        action.Should()
            .Throw<CleanArchMvc.Domain.Validation.DomainExceptionValidation>()
                .WithMessage("Invalid price value"); 
    }

    [Fact]
    public void CreateProduct_InvalidStockValue_DomainException()
    {
        Action action = () => new Product(1, "Product name", "description", 
            9.99m, -10, "url-image");
        
        action.Should()
            .Throw<CleanArchMvc.Domain.Validation.DomainExceptionValidation>()
                .WithMessage("Invalid stock value"); 
    }

    [Fact]
    public void CreateProduct_InvalidUrlImageValue_DomainException()
    {
        Action action = () => new Product(1, "Product name", "description", 
            9.99m, 10,
            "teste-teste-teste-teste-teste-teste-teste-teste-teste-teste-teste-teste-teste-teste-teste-teste-teste-teste-teste-teste-teste-teste-teste-teste-teste-teste-teste-teste-teste-teste-teste-teste-teste-teste-teste-teste-teste-teste-teste-teste-teste-teste-");
        
        action.Should()
            .Throw<CleanArchMvc.Domain.Validation.DomainExceptionValidation>()
                .WithMessage("Invalid image. Maximum 250 characters"); 
    }
    
    [Fact]
    public void CreateProduct_UrlImageNull_DomainException()
    {
        Action action = () => new Product(1, "Product name", "description", 
            9.99m, 10, null);
        
        action.Should()
            .NotThrow<CleanArchMvc.Domain.Validation.DomainExceptionValidation>();
    }

    [Fact]
    public void CreateProduct_UrlImageEmpty_DomainException()
    {
        Action action = () => new Product(1, "Product name", "description", 
            9.99m, 10, "");
        
        action.Should()
            .NotThrow<CleanArchMvc.Domain.Validation.DomainExceptionValidation>();
    }

    [Theory]
    [InlineData(-5)]
    public void CreateProduct_InvalidStockValue_DomainExceptionNegativeValue(int value)
    {
        Action action = ()=> new Product(1, "Product", "Description", 9.99m, value, 
            "url-image");
        action.Should().Throw<CleanArchMvc.Domain.Validation.DomainExceptionValidation>()
            .WithMessage("Invalid stock value");
    }

    [Fact]
    public void CreateProduct_UrlImageNull_NoNullReferenceException()
    {
        Action action = () => new Product(1, "Product name", "description", 
            9.99m, 10, null);
        
        action.Should()
            .NotThrow<NullReferenceException>();
    }

}