using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projedeneme2
{
    public class CategoryCache
    {
        private IMemoryCache _cache;

        public CategoryCache(IMemoryCache cache)
        {
            _cache = cache;
        }

        public List<CategoryItem> GetCategories()
        {

            List<CategoryItem> cachedItems = null;

            if (!_cache.TryGetValue<List<CategoryItem>>("catItems", out cachedItems))
            {
                var CacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(5));

                cachedItems = new List<CategoryItem>();
                var cocuk = new CategoryItem() { Id = 2, Name = "Cocuk Psikolojisi", Categories = new List<CategoryItem>() };
                cocuk.Categories.Add(new CategoryItem() { Id = 100, Name = "A Sub" });
                cocuk.Categories.Add(new CategoryItem() { Id = 200, Name = "B Sub" });
                cachedItems.Add(cocuk);

                var eriskin = new CategoryItem() { Id = 3, Name = "Erıskin Psikolojisi" };
                cachedItems.Add(eriskin);

                _cache.Set<List<CategoryItem>>("catItems", cachedItems, CacheEntryOptions);

            }


            return cachedItems;
        }
    }

    public class CategoryItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<CategoryItem> Categories
        {
            get; set;
        }
    }
}
