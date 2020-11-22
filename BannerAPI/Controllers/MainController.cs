using BannerAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BannerAPI.Controllers
{
    [Route("/")]
    public class MainController : Controller
    {
        private readonly IBannerService _bannerService;

        public MainController(IBannerService bannerService)
        {
            _bannerService = bannerService;
        }
        public IActionResult Index()
        {
            var bannerList = _bannerService.GetAll();

            // Slumpa ut en banner.
            var rng = new Random();
            var rngNum = rng.Next(0, bannerList.Count);
            var randomBanner = bannerList[rngNum];
            
            return View(randomBanner);
        }
    }
}
