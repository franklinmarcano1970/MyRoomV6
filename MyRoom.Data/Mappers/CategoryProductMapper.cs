using MyRoom.Model;
using MyRoom.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyRoom.Data.Mappers
{
    public class CategoryProductMapper
    {
        public static List<CategoryProduct> CreateModel(CategoryProductViewModel categoryViewModel)
        {
            List<CategoryProduct> categoryProds = new List<CategoryProduct>();
            foreach (int prodid in categoryViewModel.ProductsIds)
            {
                categoryProds.Add(new CategoryProduct()
                {
                    IdCategory = categoryViewModel.CategoryId,
                    IdProduct = prodid,
                    Active = true
                });
            }
            return categoryProds;
        }
    }
}