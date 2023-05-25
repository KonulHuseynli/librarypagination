using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniversityProject.DAL;
using UniversityProject.Models;
using UniversityProject.ViewModels.Product;

namespace UniversityProject.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public ProductController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
            //new RelatedProducts{Title="FRANKENSTEIN",Writer="By MARY SHELLEY",FilePath="/uni layihe/img/Image-1.jpg"},
            //new RelatedProducts{Title="HOW ANIMALS SEE THE WORLD",Writer="By MARTIN STEVENS",FilePath="/uni layihe/img/Image-2.jpg"},
            //new RelatedProducts{Title="THE BEST TRAVEL BOOKS",Writer="By JACOB REATHE",FilePath="/uni layihe/img/Image-3.jpg"},
            //new RelatedProducts{Title="JOKER ENDGAME",Writer="By SCOTT SNYDER GREG CAPULLO",FilePath="/uni layihe/img/Image-4.jpg"},
            //new RelatedProducts{Title="FASHION ILLUSTRATION AFRICA",Writer="By TAPIWA MATSINDE",FilePath="/uni layihe/img/Image-5.jpg"},
            //new RelatedProducts{Title="BIBBY'S KITCHEN ",Writer="By Sieanna Biby",FilePath="/uni layihe/img/Image-6.jpg"}

        public async Task<IActionResult> Index()
        {
            //var relatedProducts = await _appDbContext.RelatedProducts.ToListAsync();
            //var model = new ProductIndexViewModel
            //{
            //    RelatedProducts = relatedProducts
            //};
            var model = new ProductIndexViewModel
            {
                RelatedProducts = await _appDbContext.RelatedProducts
                                              .OrderByDescending(rwc => rwc.Id)
                                              .Take(6)
                                              .ToListAsync()
            };
            return View(model);
        }
        //public async Task<IActionResult> LoadMore(int skipRow)
        //{
        //    var relatedProducts = await _appDbContext.RelatedProducts
        //                                .OrderByDescending(rwc => rwc.Id)
        //                                .Skip(3 * skipRow)
        //                                .Take(3)
        //                                .ToListAsync();

        //    return PartialView("_RecentWorkComponentPartial", relatedProducts);
        //}


    }
}
