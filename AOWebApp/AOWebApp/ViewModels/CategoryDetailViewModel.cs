
using System.Collections.Generic;
using System.Linq;
using AOWebApp.Models;

namespace AOWebApp.Models.ViewModels
{
    public class CategoryDetailViewModel
    {
        public ItemCategory? Category { get; set; }
        public IEnumerable<ItemCategory> ChildCategories { get; set; } = Enumerable.Empty<ItemCategory>();
        public IEnumerable<Item> Items { get; set; } = Enumerable.Empty<Item>();
    }
}

