﻿namespace BiteBuddy.Services.ShoppingCartAPI.Models.Dto
{
    public class ProductDto
    {
        public int ProductId { get; set; }
        public string Code {get; set;}
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}