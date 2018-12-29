using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebMvc.Services;
using WebMvc.ViewModels;
using Polly.CircuitBreaker;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebMvc.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly IBasketService _basketService;
        private readonly ICatalogService _catalogService;
        private readonly IIdentityParser<ApplicationUser> _appUserParser;

        public CartController(IBasketService basketService, ICatalogService catalogService, IIdentityParser<ApplicationUser> appUserParser)
        {
            _basketService = basketService;
            _catalogService = catalogService;
            _appUserParser = appUserParser;
        }

        /// <summary>
        /// 购物车首页
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            try
            {
                var user = _appUserParser.Parse(HttpContext.User);
                var vm = await _basketService.GetBasket(user);
                return View(vm);
            }
            catch (BrokenCircuitException)
            {
                // Catch error when Basket.api is in circuit-opened mode                 
                HandleBrokenCircuitException();
            }
            return View();
        }

        /// <summary>
        /// 修改购物车
        /// </summary>
        /// <param name="quantities"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Index(Dictionary<string, int> quantities, string action)
        {
            try
            {
                //var user = _appUserParser.Parse(HttpContext.User);
                //var basket = await _basketService.SetQuantities(user, quantities);
                if (action == "[ Checkout ]")
                {
                    return RedirectToAction("Create", "Order");
                }
            }
            catch (BrokenCircuitException)
            {
                // Catch error when Basket.api is in circuit-opened mode                 
                HandleBrokenCircuitException();
            }
            return View();
        }

        public async Task<IActionResult> AddToCart(CatalogItem productDetails)
        {
            try
            {
                if (productDetails?.Id != null)
                {
                    var user = _appUserParser.Parse(HttpContext.User);
                    await _basketService.AddItemToBasket(user, productDetails.Id);
                }
                return RedirectToAction("Index", "Catalog");            
            }
            catch (BrokenCircuitException)
            {
                // Catch error when Basket.api is in circuit-opened mode                 
                HandleBrokenCircuitException();
            }

            return RedirectToAction("Index", "Catalog", new { errorMsg = ViewBag.BasketInoperativeMsg });
        }

        private void HandleBrokenCircuitException()
        {
            ViewBag.BasketInoperativeMsg = "Basket Service is inoperative, please try later on. (Business Msg Due to Circuit-Breaker)";
        }
    }
}