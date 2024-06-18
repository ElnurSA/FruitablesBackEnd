using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FruitablesProject.Services.Interfaces;
using FruitablesProject.ViewModels;
using FruitablesProject.ViewModels.Basket;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FruitablesProject.ViewComponents
{
    public class HeaderViewComponent : ViewComponent
    {
        private readonly ISettingService _settingService;
        private readonly IHttpContextAccessor _accessor;

        public HeaderViewComponent(ISettingService settingService,
                                   IHttpContextAccessor accessor)
        {
            _settingService = settingService;
            _accessor = accessor;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
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
                Settings = settingDatas
            };

          
            return View(response);
        }
    }
}
