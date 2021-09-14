using System;
using System.Linq.Expressions;
using Core.Entities;

namespace Core.Specifications
{
    public class ProductsWithTypesAndBrandsSpecification:BaseSpecification<Product>
    {
        public ProductsWithTypesAndBrandsSpecification()
        {
            AddInclude(pro=>pro.ProductType);
            AddInclude(pro=>pro.ProductBrand);
        }

        public ProductsWithTypesAndBrandsSpecification(int id):base(prod=>prod.Id==id)
        {
                
        }
    }
}