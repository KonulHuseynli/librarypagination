using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniversityProject.DAL;
using UniversityProject.Models;
using UniversityProject.ViewModels.Shop;

namespace UniversityProject.Controllers
{
    public class ShopController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public ShopController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<IActionResult> Index(ShopIndexViewModel model)
        {
            var pageCount = await GetPageCountAsync(model.Take);


            if (model.Page <= 0 || model.Page > pageCount) return NotFound();

            var shopProducts = await PaginateShopProductsAsync(model.Page, model.Take);

            model = new ShopIndexViewModel
            {
                ShopProducts = shopProducts,
                Page = model.Page,
                PageCount = pageCount,
                Take = model.Take
            };
            return View(model);
        }

        private async Task<List<ShopProduct>> PaginateShopProductsAsync(int page, int take)
        {

            return await _appDbContext.shopProducts
                 .OrderByDescending(b => b.Id)
                 .Skip((page - 1) * take).Take(take)
                 .ToListAsync();

        }


        private async Task<int> GetPageCountAsync(int take)
        {

            var productsCount = await _appDbContext.shopProducts.CountAsync();

            return (int)Math.Ceiling((decimal)productsCount / take);

        }



        public async Task<IActionResult> Details(int id)
        {
            var products = await _appDbContext.shopProducts.FindAsync(id);

            if (products == null) return NotFound();

            var model = new ShopDetailsViewModel
            {
                Title = products.Title,
                CreateDate = products.CreateDate,
                Description = products.Description,
                PhotoName = products.PhotoName,
                Price = products.Price, 
            };

            return View(model);

        }

    }
}
