using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FruitablesProject.Models;
using FruitablesProject.Services.Interfaces;
using FruitablesProject.ViewModels;
using FruitablesProject.ViewModels.Basket;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FruitablesProject.ViewComponents
{
    public class HeaderViewComponent : ViewComponent
    {
        private readonly ISettingService _settingService;
        private readonly IHttpContextAccessor _accessor;
        private readonly UserManager<AppUser> _userManager;

        public HeaderViewComponent(ISettingService settingService,
                                   IHttpContextAccessor accessor,
                                   UserManager<AppUser> userManager)
        {
            _settingService = settingService;
            _accessor = accessor;
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            AppUser user = new();

            if (User.Identity.IsAuthenticated)
            {
                user = await _userManager.FindByNameAsync(User.Identity.Name);
            }

            Dictionary<string, string> settingDatas = await _settingService.GetAllAsync();

            List<BasketVM> basketProduct = new List<BasketVM>();

            string basketCookieValue = _accessor.HttpContext.Request.Cookies["basket"];

            if (!string.IsNullOrEmpty(basketCookieValue) && basketCookieValue != "null")
            {
                basketProduct = JsonConvert.DeserializeObject<List<BasketVM>>(basketCookieValue);
            }
            else
            {
                basketProduct = new List<BasketVM>(); 
            }


            int basketCount = basketProduct.Sum(m => m.Count);
            decimal basketTotalPrice = basketProduct.Sum(m => m.Count * m.Price);

            HeaderVM response = new HeaderVM
            {
                BasketCount = basketCount,
                BasketTotalPrice = basketTotalPrice,
                Settings = settingDatas,
                UserFullName = user.FullName
            };

          
            return View(response);
        }
    }
}
