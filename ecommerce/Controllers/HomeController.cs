using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ecommerce.Data;
using Microsoft.AspNetCore.Mvc;
using ecommerce.Models;
using ecommerce.Models.Services;
using ecommerce.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ecommerce.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepository<Product> productRepository;
        private readonly IRepository<Cart> cartRepository;
        private readonly IRepository<Category> catRepository;
        private readonly UserManager<ApplicationUser> userManager;


        public HomeController(IRepository<Product> productRepository,
            IRepository<Cart> cartRepository, UserManager<ApplicationUser> userManager,
            IRepository<Category> catRepository)
        {
            this.productRepository = productRepository;
            this.cartRepository = cartRepository;
            this.userManager = userManager;
            this.catRepository = catRepository;
        }


        [HttpGet("")]
        public IActionResult Index(string category = "")
        {
            ProductCategoryViewModel pc = new ProductCategoryViewModel();
            pc.Categories = catRepository.OrderBy(x => x.Position, false).Include(x => x.SubCategories);
            if (!string.IsNullOrEmpty(category))
            {
                pc.Products = productRepository.Where(x => x.SubCategory.Name == category).OrderBy(x => x.Featured);
                return View(pc);
            }

            pc.Products = productRepository.All();
            return View(pc);
        }

        [Route("{id}")]
        public IActionResult Product(string id)
        {
            ProductCategoryViewModel pc = new ProductCategoryViewModel();
            pc.Product = productRepository.Where(x => x.Slug == id).Include(x => x.SubCategory).SingleOrDefault();
            pc.Categories = catRepository.OrderBy(x => x.Position, false).Include(x => x.SubCategories);
            return View(pc);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }

        [HttpGet]
        public async Task<string> GetCurrentUserId()
        {
            ApplicationUser usr = await GetCurrentUserAsync();
            return usr?.Id;
        }

        private Task<ApplicationUser> GetCurrentUserAsync() => userManager.GetUserAsync(HttpContext.User);


        [HttpPost]
        public async Task<IActionResult> addCard([FromBody] Cart cart)
        {
            string user = await GetCurrentUserId();
            string key = Request.Cookies["key"];
            if (string.IsNullOrEmpty(user))
            {
                if (string.IsNullOrEmpty(key))
                {
                    key = Guid.NewGuid().ToString();
                    var option = new CookieOptions();
                    option.Expires = DateTime.Now.AddMinutes(100);
                    Response.Cookies.Append("key", key, option);
                    cart.Id = new Guid(key);
                    cartRepository.Add(cart);
                }
                else
                {
                    if (cartRepository.Where(x => x.Id == new Guid(key) && x.ProductId == cart.ProductId).Any())
                    {
                        cart.Id = new Guid(key);
                        cart.Quantity += cart.Quantity;
                        cartRepository.Update(cart);
                    }
                    else
                    {
                        cart.Id = new Guid(key);
                        cartRepository.Add(cart);
                    }
                }

                return Json(cartRepository.Where(x => x.Id == new Guid(key)).Count());
            }
            else
            {
                if (string.IsNullOrEmpty(key))
                {
                    cart.Id = new Guid(key);
                    cartRepository.Add(cart);
                }
                else
                {
                    foreach (var item in cartRepository.Where(x => x.UserId == user))
                    {
                        item.UserId = user;
                        cartRepository.Update(item);
                    }

                    if (cartRepository.Where(x => x.UserId == user && x.ProductId == cart.ProductId) != null)
                    {
                        cart.Quantity += cart.Quantity;
                        cartRepository.Update(cart);
                    }
                    else
                    {
                        cart.Id = new Guid(key);
                        cartRepository.Add(cart);
                    }
                }

                return Json(cartRepository.Where(x => x.UserId == user).Count());
            }

            return Json("");
        }

        [HttpGet("Cart")]
        public async Task<IActionResult> Cart()
        {
            string user = await GetCurrentUserId();
            List<CartViewModel> cart = new List<CartViewModel>();
            if (!string.IsNullOrEmpty(user))
            {
                foreach (var item in cartRepository.Where(x => x.UserId == user).Include(x=>x.Product))
                {
                    cart.Add(new CartViewModel()
                    {
                        Product = item.Product,
                        Price = item.Product.Price,
                        Quantity = item.Quantity,
                    });
                }
            }
            else
            {
                if (string.IsNullOrEmpty(Request.Cookies["key"]))
                {
                    return View(cart);
                }

                foreach (var item in cartRepository.Where(x => x.Id == new Guid(Request.Cookies["key"])).Include(x => x.Product))
                {
                    cart.Add(new CartViewModel()
                    {
                        Product = item.Product,
                        Price = item.Product.Price,
                        Quantity = item.Quantity,
                    });
                }
            }

            return View(cart);
        }

        public async Task<IActionResult> RemoveFromCart(Guid id)
        {
            string user = await GetCurrentUserId();
            Cart cart = new Cart();

            if (!string.IsNullOrEmpty(user))
            {
                cart = cartRepository.Get(x => x.UserId == user && x.ProductId == id);
            }
            else
            {
                cart = cartRepository.Get(x => x.Id.ToString() == Request.Cookies["key"] && x.ProductId == id);
            }

            cartRepository.Delete(cart);
            return RedirectToAction(nameof(Cart));
        }
    }
}