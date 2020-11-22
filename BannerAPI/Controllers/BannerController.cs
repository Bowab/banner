using BannerAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;
using System.Net;
using BannerAPI.Services;
using Microsoft.Extensions.Options;

namespace BannerAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BannerController : ControllerBase
    {
        private readonly IBannerService _bannerService;
        public BannerController(IBannerService bannerService)
        {
            _bannerService = bannerService;
        }

        
        public IActionResult Get(int id)
        {
            var banner = _bannerService.FindById(id);

            if (banner.Id <= 0)
                return NotFound();

            return Ok(banner);
        }

        [Route("create")]
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] Banner banner) // Formulär
        {
            return await _bannerService.Create(banner);
        }

        [Route("create")]
        [HttpPost]
        [Consumes("application/json")]
        public async Task<IActionResult> CreateByJson([FromBody] Banner banner) // raw body post (json).
        {
            return await _bannerService.Create(banner);
        }

        [Route("update")]
        [HttpPost]
        public async Task<IActionResult> Update([FromForm] Banner banner)
        {
            return await _bannerService.Update(banner);
        }

        [Route("update")]
        [HttpPost]
        [Consumes("application/json")]
        public async Task<IActionResult> UpdateByBody([FromBody] Banner banner)
        {
            return await _bannerService.Update(banner);
        }

        [HttpPost]
        [Route("delete")]
        public async Task<IActionResult> Delete([FromForm] int id)
        {
            return await _bannerService.DeleteById(id);
        }

        [HttpPost]
        [Route("delete")]
        [Consumes("application/json")]
        public async Task<IActionResult> DeleteByBody([FromBody] int id)
        {
            return await _bannerService.DeleteById(id);
        }


        // Skön liten metod för att hämta alla nu under testning.
        [Route("all")]
        public IActionResult GetAll()
        {
            return Ok(_bannerService.GetAll());
        }
    }
}
