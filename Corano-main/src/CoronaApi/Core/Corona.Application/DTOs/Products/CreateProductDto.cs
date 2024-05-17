﻿using Corona.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Corona.Application.DTOs.Products;

public class CreateProductDto
{
    public string Title { get; set; }
    public string? Description { get; set; }
    public int StockCount { get; set; }
    public string Size { get; set; }
    public string Color { get; set; }
    public double Price { get; set; }
    public ICollection<IFormFile>? ProductsImages { get; set; }
    public Guid ProductTypeId { get; set; }
    public Guid CategoryId { get; set; }
}