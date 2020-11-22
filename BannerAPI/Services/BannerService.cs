using BannerAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace BannerAPI.Services
{
    public class BannerService : ControllerBase, IBannerService
    {
        public List<Banner> GetAll()
        {
            // Här använder vi oss av fulingen i Startup, där jag registerade en statisk GetPath och IWebHosting env.
            // Se detta lite som att ha ett dbContext från tillexempel EntityFramework.
            return JsonSerializer.Deserialize<List<Banner>>(System.IO.File.ReadAllText(Startup.GetPath("Database/Banners.json")));
        }

        // Liten fuling men man skulle kunna kalla denna "recreate banner" istället då den skapar om hela json filen,
        // men detta är som tidigare nämnt lite fejk databas.
        public async Task Save(List<Banner> bannerList)
        {
            var jsonString = JsonSerializer.Serialize(bannerList);
            await System.IO.File.WriteAllTextAsync(Startup.GetPath("Database/Banners.json"), jsonString);
        }

        public Banner FindById(int id)
        {
            var bannerList = GetAll();
            var banner = bannerList.FirstOrDefault(x => x.Id == id) ?? new Banner();

            return banner;
        }

        public async Task<IActionResult> Update(Banner banner)
        {
            // Observera att denna hämtar ut alla banners och sedan hämtar den ut den rätta, detta är ju inte optimalt
            // men detta pga av att jag försöker simulera en databas med .json filer.
            var bannerList = GetAll();
            var bannerToUpdate = bannerList.FirstOrDefault(x => x.Id == banner.Id) ?? new Banner();

            // Jag gillar att lägga exit state-ments så högt upp som möjligt i mina metoder.
            // Säg att vi hade 3-4 saker som skulle kunna gå fel, då kan vi retunera redan här uppe utan
            // att exikvera onödig kod och göra det läsligare, man får en "aha, vi kastar 404 om vi inte hittar objektet".
            if (bannerToUpdate.Id <= 0)
                return NotFound();

            bannerList.Remove(bannerToUpdate); // Lite mmhm men aja, funkar eftersom vi kollade id <= 0 innan.

            bannerToUpdate.Html = banner.Html;
            bannerToUpdate.Modified = DateTime.Now;

            bannerList.Add(bannerToUpdate);

            await Save(bannerList);

            var success = String.Format("Success in updating banner with id:{0}", bannerToUpdate.Id);
            return Ok(success);
        }

        public async Task<IActionResult> Create(Banner banner)
        {
            // Hack för att behålla tidigare data som ligger i filen, annars skriver vi över hela tiden.
            // Jag hämtar all data, gör om det till en Lista<T> och fyller på listan med nytt T för att sedan
            // mata in det i json-filen (databasen), append är ju ett annat alternativ.
            var bannerList = GetAll();

            var rng = new Random();

            banner.Id = rng.Next(0, 2000000); // Simulera ett nytt Id då vi låtsas att json-filer är en databas.

            // Här hade man kunnat bygga ut diverse metoder som validerar att det är riktig html
            // man skickar in, att det finns faktiskta värden inmatade osv osv.


            bannerList.Add(banner);

            // För att behålla befintlig data väljer jag att alltid skriva om den, givetvis
            // hade jag inte gjort såhär i en riktig applikation.
            await Save(bannerList);

            return Ok("Successfully created a banner");
        }

        public async Task<IActionResult> DeleteById(int id)
        {
            var bannerList = GetAll();
            var bannerToDelete = bannerList.FirstOrDefault(x => x.Id == id) ?? new Banner();

            if (bannerToDelete.Id <= 0)
                return NotFound();

            bannerList.Remove(bannerToDelete);

            await Save(bannerList);

            var success = String.Format("Successfully deleted banner with id {0}", id);
            return Ok(success);
        }

    }
}
