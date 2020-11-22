using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BannerAPI.Models
{
    public class Banner
    {

        [Key]
        public int Id { get; set; }
        public string Html { get; set; }

        // Rätt skönt att sätta default värde redan här.
        // Created kommer rimligtvis alltid vara DateTime.Now.
        public DateTime Created { get; set; } = DateTime.Now;

        // Såg att ni hade denna som nullable, antar att när en banner skapas ska den inte ha ett modifed värde.
        // Ett annat alternativ är ju att vid en skapad banner får den också DateTime.Now.
        public DateTime? Modified { get; set; }

    }
}
