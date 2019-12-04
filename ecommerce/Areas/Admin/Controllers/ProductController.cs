using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using ecommerce.Data;
using ecommerce.Models;
using ecommerce.Models.Services;
using ecommerce.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ecommerce.Areas.Admin.Controllers
{
    [Area("Admin")]
  //  [Authorize(Roles = "Admin")]
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IRepository<Product> productRepository;
        private readonly IHostingEnvironment hostingEnvironment;
        

        public ProductController(ApplicationDbContext context, IRepository<Product> productRepository,
            IHostingEnvironment hostingEnvironment)
        {
            this.productRepository = productRepository;
            this.context = context;
            this.hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Index()
        {
            return View(productRepository.All().Include(x => x.SubCategory));
        }


        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewData["SubCategoryId"] = new SelectList(context.SubCategories, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductViewModel product)
        {
            if (ModelState.IsValid)
            {
                string uniqueImageName = "";
                string imgPath = Path.Combine(hostingEnvironment.WebRootPath, "img");
                uniqueImageName = Guid.NewGuid().ToString().Substring(0, 16) + product.Image.FileName;
                string filePath = Path.Combine(imgPath, uniqueImageName);
                product.Image.CopyTo(new FileStream(filePath, FileMode.Create));

                Product instance = new Product()
                {
                    Id = Guid.NewGuid(),
                    Title = product.Title,
                    Brand = product.Brand,
                    Featured = product.Featured,
                    Image = "img/" + uniqueImageName,
                    SubCategoryId = new Guid(product.SubCategoryId),
                    ProductOptionId = new Guid(product.ProductOptionId),
                    Code = product.Code,
                    Description = product.Description,
                    GroupCode = product.GroupCode,
                    Price = product.Price,
                    Quantity = product.Quantity,
                    Slug = product.Title.Replace(" ", "-") + Guid.NewGuid().ToString().Substring(0, 10)
                };
                productRepository.Add(instance);

                return RedirectToAction(nameof(Index));
            }

            ViewData["SubCategoryId"] = new SelectList(context.SubCategories, "Id", "Name");
            return View(product);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var result = productRepository.Get(x => x.Id == id);

            ProductViewModel _product = new ProductViewModel()
            {
                Featured = result.Featured,
                Brand = result.Brand,
                Title = result.Title,
                Quantity = result.Quantity,
                Code = result.Code,
                Description = result.Description,
                GroupCode = result.GroupCode,
                Price = result.Price,
                ProductOptionId = result.ProductOptionId.ToString(),
                SubCategoryId = result.SubCategoryId.ToString()
            };
            ViewBag.image = result.Image;
            ViewData["SubCategoryId"] = new SelectList(context.SubCategories, "Id", "Name");
            return View(_product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductViewModel product)
        {
            if (ModelState.IsValid)
            {
                string uniqueImageName = "";
                if (product.Image != null)
                {
                    string imgPath = Path.Combine(hostingEnvironment.WebRootPath, "img");
                    uniqueImageName = Guid.NewGuid().ToString().Substring(0, 16) + product.Image.FileName;
                    string filePath = Path.Combine(imgPath, uniqueImageName);
                    product.Image.CopyTo(new FileStream(filePath, FileMode.Create));
                    uniqueImageName = "img/" + uniqueImageName;
                }
                else
                {
                    uniqueImageName = product.img;
                }

                Product instance = new Product()
                {
                    Id = product.Id,
                    Title = product.Title,
                    Brand = product.Brand,
                    Featured = product.Featured,
                    Image = uniqueImageName,
                    SubCategoryId = new Guid(product.SubCategoryId),
                    ProductOptionId = new Guid(product.ProductOptionId),
                    Code = product.Code,
                    Description = product.Description,
                    GroupCode = product.GroupCode,
                    Price = product.Price,
                    Quantity = product.Quantity,
                    Slug = product.Title.Replace(" ", "-") + Guid.NewGuid().ToString().Substring(0, 10)
                };
                productRepository.Update(instance);

                return RedirectToAction(nameof(Index));
            }

            ViewData["SubCategoryId"] = new SelectList(context.SubCategories, "Id", "Name");
            ViewBag.image = product.Image;
            return View(product);
        }

        public async Task<IActionResult> Details(Guid id)
        {
            return View(productRepository.Get(x => x.Id == id));
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var result = productRepository.Get(x => x.Id == id);
            productRepository.Delete(result);
            return RedirectToAction(nameof(Index));
        }

    }
}