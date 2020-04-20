using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ToledoOpenDurringVirus.Models;

namespace ToledoOpenDurringVirus.Pages
{
    public class IndexModel : PageModel
    {
        public List<DisplayModel> ListOfResturants { get; set; }
        public List<stats> ListOfTypes { get; set; }
        public List<stats> ListOfAreas { get; set; }

        //public List<DisplayModel> TestCollection { get; set; }

        private readonly ResturantDBContext _context;
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger, ResturantDBContext context)
        {
            _context = context;
            _logger = logger;
        }

        public async Task OnGetAsync()
        {
            var TempCollection = await _context.LocationTb
                .Include(a => a.Area)
                .Include(t => t.Type)
                .ToListAsync();

            ListOfTypes = TempCollection
                .GroupBy(t => t.Type.Name)
                .Select(f => new stats { Name = f.Key, Count = f.Count() })
                .ToList();

            ListOfAreas = TempCollection
                .GroupBy(t => t.Area.Name)
                .Select(f => new stats {Name = f.Key, Count = f.Count() })
                .ToList();

            ListOfResturants = TempCollection.Select(f => new DisplayModel { LID = f.Lid, Name = f.Name, AreaName = f.Area.Name, TypeName = f.Type.Name, FullMenu = f.FullMenu, HasSpecials = f.HasSpecials }).ToList();

            foreach (var cur in ListOfResturants)
                cur.OpenNow = await IsOpenNow(cur.LID);

            ListOfResturants = ListOfResturants.OrderByDescending(o => o.OpenNow).ThenBy(n => n.Name).ToList();  //TempCollection.Select(f => new DisplayModel { LID = f.Lid, Name = f.Name, AreaName = f.Area.Name, TypeName = f.Type.Name }).ToList();
        }

        private async Task<bool> IsOpenNow(Guid ID)
        {
            var openhours =  _context.OpenHoursTb
                .AsEnumerable()
                .Where(i => i.Lid == ID && i.Day == (int)DateTime.Today.DayOfWeek && Extentions.Between(DateTime.Now.TimeOfDay, TimeSpan.Parse(i.HourOpen), TimeSpan.Parse(i.HourClose) ))
                .ToList();               

            if (openhours == null || openhours.Count == 0)
                return false;
            else
                return true;
        }
    }

    public class stats
    {        
        public string Name { get; set; }
        public int Count { get; set; }
    }
}
