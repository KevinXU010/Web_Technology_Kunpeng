using AOWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

public class ReportsController : Controller
{
    private readonly AmazonOrders2025Context _context;
    public ReportsController(AmazonOrders2025Context context) => _context = context;

    public async Task<IActionResult> Index()
    {
        var years = await _context.CustomerOrders
            .AsNoTracking()
            .Select(o => o.OrderDate.Year)
            .Distinct()
            .OrderByDescending(y => y)
            .ToListAsync();

        return View("AnnualSalesReport", new SelectList(years));
    }

 
    [Produces("application/json")]
    public IActionResult AnnualSalesReportData(int Year)
    {
        if (Year <= 0) return BadRequest();

        var orderSummary = _context.ItemsInOrders
            .AsNoTracking()
            .Where(iio => iio.OrderNumberNavigation != null
                       && iio.OrderNumberNavigation.OrderDate.Year == Year)
            .GroupBy(iio => new
            {
                Year = iio.OrderNumberNavigation.OrderDate.Year,
                Month = iio.OrderNumberNavigation.OrderDate.Month
            })
            .Select(g => new
            {
                year = g.Key.Year,
                monthNo = g.Key.Month,
                totalItems = g.Sum(x => x.NumberOf),
                totalSales = g.Sum(x => x.TotalItemCost)
            })
            .OrderBy(x => x.monthNo)
            .ToList();
   

        return Json(orderSummary);
    }
}



