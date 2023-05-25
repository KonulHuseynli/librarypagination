using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using UniversityProject.Areas.Admin.ViewModels;
using UniversityProject.DAL;
using UniversityProject.Models;

namespace UniversityProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class RelatedProductController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public RelatedProductController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<IActionResult> Index()
        {
            var model = new RelatedProductIndexViewModel
            {

                RelatedProducts = await _appDbContext.RelatedProducts.ToListAsync()

            };
            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> Create()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(RelatedProducts relatedProducts)
        {
            if (!ModelState.IsValid) return View(relatedProducts);

            bool isExist = await _appDbContext.RelatedProducts
                                                   .AnyAsync(c => c.Title.ToLower().Trim() == relatedProducts.Title.ToLower().Trim());

            if (isExist)
            {
                ModelState.AddModelError("Title", "Bu adda product artiq movcuddur");

                return View(relatedProducts);
            }

            await _appDbContext.RelatedProducts.AddAsync(relatedProducts);
            await _appDbContext.SaveChangesAsync();

            return RedirectToAction("Index");

        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var relatedProducts = await _appDbContext.RelatedProducts.FindAsync(id);
            if (relatedProducts == null) return NotFound();

            return View(relatedProducts);

        }
        [HttpPost]
        public async Task<IActionResult> Update(int id, RelatedProducts relatedProducts)
        {
            if (!ModelState.IsValid) return View(relatedProducts);

            if (id != relatedProducts.Id) return BadRequest();
            var dbrelatedProducts = await _appDbContext.RelatedProducts.FindAsync(id);
            if (dbrelatedProducts == null) return NotFound();

            bool isExist = await _appDbContext.RelatedProducts
                .AnyAsync(rcw => rcw.Title.ToLower().Trim() == relatedProducts.Title.ToLower().Trim() &&
                rcw.Id != relatedProducts.Id);
            if (isExist)
            {
                ModelState.AddModelError("Title", "Bu adda komponent movcuddur");
                return View(relatedProducts);
            }
            dbrelatedProducts.Title = relatedProducts.Title;
            dbrelatedProducts.Writer = relatedProducts.Writer;
            dbrelatedProducts.FilePath = relatedProducts.FilePath;

            await _appDbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var relatedProducts = await _appDbContext.RelatedProducts.FindAsync(id);
            if (relatedProducts == null) return NotFound();

            return View(relatedProducts);

        }
        [HttpPost]
        public async Task<IActionResult> DeleteComponent(int id)
        {



            var dbrelatedProducts = await _appDbContext.RelatedProducts.FindAsync(id);
            if (dbrelatedProducts == null) return NotFound();


            _appDbContext.Remove(dbrelatedProducts);

            await _appDbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var relatedProducts = await _appDbContext.RelatedProducts.FindAsync(id);
            if (relatedProducts == null) return NotFound();

            return View(relatedProducts);

        }

    }
}

   
