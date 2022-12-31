﻿namespace SafeShop.Service.DTO.Product
{
    public class ProductPutDTO
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Category { get; set; }
        public decimal? Price { get; set; }
        public byte[]? Image { get; set; }
    }
}
