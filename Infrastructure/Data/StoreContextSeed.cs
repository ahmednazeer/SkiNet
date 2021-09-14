using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Data
{
    public class StoreContextSeed
    {
        public static async Task SeedData(StoreContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                if (!context.ProductTypes.Any())
                { 
                    var productTypesData = File.ReadAllText("../infrastructure/data/seeddata/types.json");
                    var types = JsonSerializer.Deserialize<List<ProductType>>(productTypesData);
                    foreach (var type in types)
                    {
                        var typeModel = new ProductType()
                        {
                            Name = type.Name
                        };
                        context.ProductTypes.Add(typeModel);
                    }

                    context.SaveChanges();
                }

                if (!context.ProductBrands.Any())
                {
                    var productBrandsData = File.ReadAllText("../infrastructure/data/seeddata/brands.json");
                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(productBrandsData);
                    foreach (var brand in brands)
                    {
                        var brandModel = new ProductBrand()
                        {
                            Name = brand.Name
                        };
                        context.ProductBrands.Add(brandModel);
                    }

                    context.SaveChanges();
                }
                if (!context.Products.Any())
                {
                    var productsData = File.ReadAllText("../infrastructure/data/seeddata/products.json");
                    var products = JsonSerializer.Deserialize<List<Product>>(productsData);
                    foreach (var product in products)
                    {
                        var productModel = new Product()
                        {
                            Name = product.Name,
                            Description = product.Description,
                            Price = product.Price,
                            PictureUrl = product.PictureUrl,
                            ProductTypeId = product.ProductTypeId,
                            ProductBrandId = product.ProductBrandId
                        };
                        context.Products.Add(productModel);
                    }

                    context.SaveChanges();
                }


            }
            catch (Exception e)
            {
                var logger=loggerFactory.CreateLogger<StoreContextSeed>();
                logger.LogError(e,"an error occured while seeding data!");

            }
            
        }
    }
}