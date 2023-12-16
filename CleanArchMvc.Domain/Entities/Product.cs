﻿using CleanArchMvc.Domain.Validation;

namespace CleanArchMvc.Domain.Entities;

public sealed class Product : BaseEntity
{
    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public decimal Price { get; private set; }
    public int Stock { get; private set; }
    public string? Image { get; private set; }
    public int CategoryId { get; set; }
    public Category? Category { get; set; }

    public Product(string name, string description, decimal price, int stock, string? image)
    {
        ValidateDomain(name: name, description: description, price: price, stock: stock, image: image);
    }

    public Product(int id, string name, string description, decimal price, int stock, string? image)
    {
        DomainExceptionValidation.When(id < 0, "Invalid id");
        ValidateDomain(name: name, description: description, price: price, stock: stock, image: image);
        Id = id;
    }

    public void Update(string name, string description, decimal price, int stock, string? image, int categoryId)
    {
        DomainExceptionValidation.When(categoryId < 0, "Invalid id");
        ValidateDomain(name: name, description: description, price: price, stock: stock, image: image);
        CategoryId = categoryId;
    }

    private void ValidateDomain(string name, string description, decimal price, int stock, string? image)
    {
        DomainExceptionValidation.When(string.IsNullOrEmpty(name), "Invalid name. Name is required");
        DomainExceptionValidation.When(name.Length < 3, "Invalid name. Too short, minimum 3 characters");
        DomainExceptionValidation.When(string.IsNullOrEmpty(description), "Invalid description. Description is required");
        DomainExceptionValidation.When(description.Length < 5, "Invalid description. Too short, minimum 5 characters");
        DomainExceptionValidation.When(price < 0, "Invalid price. Must be positive");
        DomainExceptionValidation.When(stock < 0, "Invalid stock. Must be positive");
        DomainExceptionValidation.When(image?.Length > 250, "Invalid image name. Too long, maximum 250 characters");

        Name = name;
        Description = description;
        Price = price;
        Stock = stock;
        Image = image;
    }
}
