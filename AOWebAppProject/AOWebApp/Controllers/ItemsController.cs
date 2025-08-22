using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AOWebApp.Models;
using AOWebApp.Models.ViewModels;

namespace AOWebApp.Controllers
{
    public class ItemsController : Controller
    {
        private readonly AmazonOrders2025Context _context;

        public ItemsController(AmazonOrders2025Context context)
        {
            _context = context;
        }

       
        public async Task<IActionResult> Index(ItemSearchViewModel vm)
        {
            var categories = await _context.ItemCategories
                .Where(c => c.ParentCategoryId == null)
                .OrderBy(c => c.CategoryName)
                .Select(c => new { c.CategoryId, c.CategoryName })
                .ToListAsync();
            vm.CategoryList = new SelectList(categories, "CategoryId", "CategoryName", vm.CategoryId);

            var q = _context.Items.AsNoTracking().AsQueryable();

            if (!string.IsNullOrWhiteSpace(vm.SearchText))
            {
                var text = vm.SearchText.Trim();
                q = q.Where(i => i.ItemName.Contains(text));
            }
            if (vm.CategoryId.HasValue)
            {

                q= q.Where(i => i.Category.ParentCategoryId == vm.CategoryId);
             
            }

            vm.ItemList = await q
                .Select(i => new ItemWithRatingViewModel
                {
                    ItemId = i.ItemId,
                    ItemName = i.ItemName,
                    ItemDescription = i.ItemDescription,
                    ItemCost = i.ItemCost,
                    ItemImage = i.ItemImage,
                    CategoryName = i.Category.CategoryName,
                    ReviewCount = i.Reviews.Count(),
                    AverageRating = i.Reviews.Select(r => (double?)r.Rating).Average() ?? 0

                })
                .OrderBy(x => x.ItemName)
                .ToListAsync();



            return View(vm);
        }
            

        // GET: Items
        //public async Task<IActionResult> Index()
        //{
        //    var amazonOrders2025Context = _context.Items.Include(i => i.Category);
        //    return View(await amazonOrders2025Context.ToListAsync());
        //}

        //public async Task<IActionResult>Index(string searchText, int? CategoryId)
        //{
        //    #region CatrgoriesQuery
        //    var Categories = _context.ItemCategories
        //        .Where(c => c.ParentCategoryId == null)
        //        .OrderBy(c => c.CategoryName)
        //        .Select(c => new { c.CategoryId, c.CategoryName })
        //        .ToList();

        //    ViewBag.CategoryList = new SelectList(Categories,
        //        nameof(ItemCategory.CategoryId),
        //        nameof(ItemCategory.CategoryName),
        //        CategoryId);  // This will be used to populate the dropdown list in the view

        //    #endregion

        //    #region ItemQuery
        //    ViewBag.SearchText = searchText;
        //    var amazonOrdersContext = _context.Items                
        //        .Include(i => i.Category)
        //        .Include(i => i.Reviews)
        //        .OrderBy(i => i.ItemName)
        //        .AsQueryable();
        //    if (!string.IsNullOrWhiteSpace(searchText))
        //    {
        //        amazonOrdersContext = amazonOrdersContext
        //            .Where(i => i.ItemName.Contains(searchText));

        //    }
        //    if (CategoryId != null)
        //    {
        //        amazonOrdersContext = amazonOrdersContext
        //            .Where(i => i.Category.ParentCategoryId == CategoryId);
        //    }
        //    #endregion

        //    return View(await amazonOrdersContext.ToListAsync());
        // }


        // GET: Items/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Items
                .Include(i => i.Category)
                .FirstOrDefaultAsync(m => m.ItemId == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // GET: Items/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.ItemCategories, "CategoryId", "CategoryId");
            return View();
        }

        // POST: Items/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ItemId,ItemName,ItemDescription,ItemCost,ItemImage,CategoryId")] Item item)
        {
            if (ModelState.IsValid)
            {
                _context.Add(item);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.ItemCategories, "CategoryId", "CategoryId", item.CategoryId);
            return View(item);
        }

        // GET: Items/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Items.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.ItemCategories, "CategoryId", "CategoryId", item.CategoryId);
            return View(item);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ItemId,ItemName,ItemDescription,ItemCost,ItemImage,CategoryId")] Item item)
        {
            if (id != item.ItemId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(item);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemExists(item.ItemId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.ItemCategories, "CategoryId", "CategoryId", item.CategoryId);
            return View(item);
        }

        // GET: Items/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Items
                .Include(i => i.Category)
                .FirstOrDefaultAsync(m => m.ItemId == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var item = await _context.Items.FindAsync(id);
            if (item != null)
            {
                _context.Items.Remove(item);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItemExists(int id)
        {
            return _context.Items.Any(e => e.ItemId == id);
        }
    }
}
