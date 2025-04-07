﻿using Domain.contracts;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Persistence
{
    public class DbInitializer : IDbInitializer
    {
        private readonly StoreDbContext _context;

        public DbInitializer(StoreDbContext context)
        {
            _context = context;
        }
        public async Task InitializerAsync()
        {
            try {
                if(_context.Database.GetPendingMigrations().Any())
                
                    _context.Database.Migrate();
                
                if (!_context.ProductTypes.Any())
                {
                    var typesData = File.ReadAllText(@"..\Infrastructure\Persistence\Data\Seeding\types.json");
                    var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);
                    if (types is not null && types.Any())
                    {
                       await _context.ProductTypes.AddRangeAsync(types);
                       await _context.SaveChangesAsync();
                    }
                }

                if (!_context.ProductBrands.Any())
                {
                    var BrandsData = File.ReadAllText(@"..\Infrastructure\Persistence\Data\Seeding\brands.json");
                    var Brands = JsonSerializer.Deserialize<List<ProductBrand>>(BrandsData);
                    if (Brands is not null && Brands.Any())
                    {
                       await _context.ProductBrands.AddRangeAsync(Brands);
                       await _context.SaveChangesAsync();
                    }
                }

                if (!_context.Products.Any())
                {
                    var productsData = File.ReadAllText(@"../Infrastructure\Persistence\Data\Seeding\products.json");
                    var products = JsonSerializer.Deserialize<List<ProductType>>(productsData);
                    if (products is not null && products.Any())
                    {
                        await _context.ProductTypes.AddRangeAsync(products);
                        await _context.SaveChangesAsync();
                    }
                }
            }
            catch (Exception )
            {
                throw;
            }

                

        }
    }
}
