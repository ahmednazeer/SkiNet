using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace SKINET.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IGenericRepository<Product> _productRepo;
        private readonly IGenericRepository<ProductType> _productTypeRepo;
        private readonly IGenericRepository<ProductBrand> _productBrandRepo;
        


        public ProductsController(IGenericRepository<Product> productRepo,IGenericRepository<ProductType> productTypeRepo,IGenericRepository<ProductBrand>productBrandRepo)
        {
            _productRepo = productRepo;
            _productTypeRepo = productTypeRepo;
            _productBrandRepo = productBrandRepo;
        }
        [HttpGet]
        public async Task<IActionResult> GetProductList()
        {
            var spec = new ProductsWithTypesAndBrandsSpecification();
            var products =await _productRepo.ListAsync(spec);
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var spec= new ProductsWithTypesAndBrandsSpecification(id);
            var product = await _productRepo.GetEntityWithSpecification(spec);//_repository.GetProductByIdAsync(id);
            //var product = await _productRepo.GetEntityByIdAsync(id);//_repository.GetProductByIdAsync(id);
            return Ok(product);
        }

        [HttpGet("brands")]
        public async Task<IActionResult> GetProductBrandsList()
        {
            var brands = await _productBrandRepo.ListAllAsync();//_repository.GetProductsBrandsAsync();
            return Ok(brands);
        }
        [HttpGet("types")]
        public async Task<IActionResult> GetProductTypesList()
        {
            var types = await _productTypeRepo.ListAllAsync();//_repository.GetProductsTypesAsync();
            return Ok(types);
        }

    }
}
