﻿namespace FoodCorp.DataAccess.Entities;

public class ProductImage
{
    public int Id { get; set; }

    public int ProductId { get; set; }
    public Product Product { get; set; }

    public string Path { get; set; }
}