﻿namespace MyAspNetCore.Web.Models
{
    public class ProductRepository
    {
        private static List<Product> _products = new List<Product>()
        {
            new() { Id = 1, Name = "kalem1", Price = 100, Stock = 200 },
            new () { Id = 2, Name = "kalem2", Price = 200, Stock = 300 },
            new () { Id = 3, Name = "kalem3", Price = 300, Stock = 400 }
        };

        public List<Product> GetAll() => _products;

        public void Add(Product newProduct) => _products.Add(newProduct);

        public void Remove(int id)
        {
            var hasProduct=_products.FirstOrDefault(p => p.Id == id);

            if(hasProduct==null)
            {
                throw new Exception($"Bu Id  ({id})ye sahip urun bulunamadi");
            }
            _products.Remove(hasProduct);
        }
        public void Update(Product updateProduct)
        {
            var hasProduct=_products.FirstOrDefault(p => p.Id == updateProduct.Id);

            if (hasProduct == null)
            {
                throw new Exception($"Bu Id  ({updateProduct.Id})ye sahip urun bulunamadi");
            }
            hasProduct.Name= updateProduct.Name;
            hasProduct.Price= updateProduct.Price;
            hasProduct.Stock= updateProduct.Stock;

            var index=_products.FindIndex(x=>x.Id==updateProduct.Id);

            _products[index] = hasProduct;
        }
    }
}
