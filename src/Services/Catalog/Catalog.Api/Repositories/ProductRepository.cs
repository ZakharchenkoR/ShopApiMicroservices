using Catalog.Api.Data;
using Catalog.Api.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalog.Api.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ICatalogContext _context;

        public ProductRepository(ICatalogContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task CreateProdact(Product product)
        {
            await _context.Prodacts.InsertOneAsync(product);
        }

        public async Task<bool> DeleteProduct(string id)
        {
            var filter = Builders<Product>.Filter.ElemMatch(x => x.Id, id);

            var deleteResult = await _context.Prodacts.DeleteOneAsync(filter);

            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }

        public async Task<Product> GetProduct(string id)
        {
            return await _context.Prodacts.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _context.Prodacts.Find(x => true).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByCategory(string categoryName)
        {
            var filter = Builders<Product>.Filter.ElemMatch(x => x.Category, categoryName);

            return await _context.Prodacts.Find(filter).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByName(string name)
        {
            var filter = Builders<Product>.Filter.ElemMatch(x => x.Name, name);

            return await _context.Prodacts.Find(filter).ToListAsync();
        }

        public async Task<bool> UpdateProduct(Product product)
        {
            var updateResult = await _context.Prodacts.ReplaceOneAsync(filter: x => x.Id == product.Id, replacement: product);

            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
        }
    }
}
