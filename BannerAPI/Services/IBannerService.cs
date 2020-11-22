using BannerAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BannerAPI.Services
{
    public interface IBannerService
    {
        public List<Banner> GetAll();
        public Task Save(List<Banner> banners);
        public Banner FindById(int id);


        public Task<IActionResult> DeleteById(int id);
        public Task<IActionResult> Update(Banner banner);
        public Task<IActionResult> Create(Banner banner);
    }
}
