using UniversityProject.Areas.Admin.ViewModels.Shop;
using UniversityProject.DAL;
using UniversityProject.Helpers;
using UniversityProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniversityProject.Areas.Admin.ViewModels;

namespace UniversityProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ShopController : Controller
    {
        private readonly AppDbContext _appdbcontext;
        private readonly IFileService _fileservice;
        private readonly IWebHostEnvironment _webhostenvironment;

        public ShopController(AppDbContext appdbcontext, IFileService fileservice, IWebHostEnvironment webhostenvironment)
        {
            _appdbcontext = appdbcontext;
            _fileservice = fileservice;
            _webhostenvironment = webhostenvironment;
        }
        public async Task<IActionResult> index()
        {
            var model = new ShopIndexViewModel
            {
                shopProducts = await _appdbcontext.shopProducts.ToListAsync()
            };
            return View(model);
        }

        [HttpGet]

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Create(ShopCreateViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            bool isexist = await _appdbcontext.shopProducts.AnyAsync(b => b.Title.ToLower().Trim() == model.Title.ToLower().Trim());
            if (isexist)
            {
                ModelState.AddModelError("title", "bu adda blog movcuddur");
                return View(model);
            }

            if (!_fileservice.IsImage(model.MainPhoto))
            {
                ModelState.AddModelError("mainphoto", "file image formatinda deyil zehmet olmasa image formasinda secin!!");
                return View(model);
            }
            if (!_fileservice.CheckSize(model.MainPhoto, 100))
            {
                ModelState.AddModelError("mainphoto", "file olcusu 300 kbdan boyukdur");
                return View(model);
            }

            if (model.CreateDate == null)
            {
                model.CreateDate = DateTime.Today;
            }

            var product = new ShopProduct
            {
                Title = model.Title,
                Description = model.Description,
                Price = model.Price,
                CreateDate = model.CreateDate.Value,
                PhotoName = await _fileservice.UploadAsync(model.MainPhoto, _webhostenvironment.WebRootPath),
            };

            await _appdbcontext.shopProducts.AddAsync(product);
            await _appdbcontext.SaveChangesAsync();


            return RedirectToAction("index");
        }

        [HttpGet]

        public async Task<IActionResult> delete(int id)
        {
            var blog = await _appdbcontext.shopProducts.FindAsync(id);
            if (blog == null) return NotFound();

            _appdbcontext.shopProducts.Remove(blog);

            await _appdbcontext.SaveChangesAsync();
            return RedirectToAction("index");
        }

        [HttpGet]
        public async Task<IActionResult> update(int id)
        {
            var blog = await _appdbcontext.shopProducts.FindAsync(id);

            if (blog == null) return NotFound();

            var model = new ShopUpdateViewModel
            {

                Title = blog.Title,
                Description = blog.Description,
                CreateDate = blog.CreateDate,
                MainPhotoName = blog.PhotoName,
                Price = blog.Price


            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> update(ShopUpdateViewModel model, int id)
        {
            var blog = await _appdbcontext.shopProducts.FindAsync(id);

            if (!ModelState.IsValid) return View(model);

            if (id != model.Id) return BadRequest();

            bool isexits = await _appdbcontext.shopProducts.AnyAsync(p => p.Title.ToLower().Trim() == model.Title.ToLower().Trim() && p.Id != blog.Id);

            if (isexits)
            {
                ModelState.AddModelError("title", "bu blog movcuddur");
                return View(model);
            }
            blog.Title = model.Title;
            blog.Description = model.Description;
            blog.CreateDate = model.CreateDate.Value;
            model.MainPhotoName = blog.PhotoName;
            blog.Price = model.Price;


            if (model.MainPhoto != null)
            {

                if (!_fileservice.IsImage(model.MainPhoto))
                {
                    ModelState.AddModelError("photo", "image formatinda secin");
                    return View(model);
                }
                if (!_fileservice.CheckSize(model.MainPhoto, 300))
                {
                    ModelState.AddModelError("photo", "sekilin olcusu 300 kb dan boyukdur");
                    return View(model);
                }
                blog.PhotoName = await _fileservice.UploadAsync(model.MainPhoto, _webhostenvironment.WebRootPath);
            }


            await _appdbcontext.SaveChangesAsync();
            return RedirectToAction("index");
        }

        [HttpGet]

        public async Task<IActionResult> details(int id)
        {
            var blog = await _appdbcontext.shopProducts.FindAsync(id);

            if (blog == null) return NotFound();

            var model = new ShopDetailsViewModel
            {
                Id = blog.Id,
                Title = blog.Title,
                Description = blog.Description,
                CreateDate = blog.CreateDate,
                MainPhotoName = blog.PhotoName,


            };
            return View(model);

        }
    }
    }




