using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ToledoOpenDurringVirus.Models;

namespace ToledoOpenDurringVirus.Pages.Area
{
    public class IndexModel : PageModel
    {
        private readonly ResturantDBContext _context;

        public IndexModel(ResturantDBContext context)
        {
            _context = context;
        }

        public IList<GroupingStats> AreaLookupTb { get; set; }        

        public async Task OnGetAsync()
        {
            //AreaLookupTb = await _context.AreaLookupTb.ToListAsync();
            //var AllAreas = await _context.AreaLookupTb.ToListAsync();
            //var AreaCount = await _context.LocationTb.GroupBy(a => a.AreaId).Select(k => new { id = k.Key, myCount = k.Count() }).ToListAsync();
            //AreaLookupTb = (from p in AllAreas join t in AreaCount on p.AreaId equals t.id into g from s in g.DefaultIfEmpty() select new GroupingStats { ID = p.AreaId, Name = p.Name, Description = p.Description, Count = g.m }).ToList();

            AreaLookupTb = _context.AreaLookupTb
                            .AsEnumerable()
                            .GroupJoin(_context.LocationTb, a => a.AreaId, b => b.AreaId, (a, b) => new GroupingStats { ID = a.AreaId, Name = a.Name, Description = a.Description, Count = b.Count() })
                            .OrderBy(n => n.Name)
                            .ToList();

            //AreaLookupTb = await _context.AreaLookupTb
            //    .ToAsyncEnumerable()
            //    .GroupJoin(_context.LocationTb.ToAsyncEnumerable(), a => a.AreaId, b => b.AreaId, (a, b) => new GroupingStats { ID = a.AreaId, Name = a.Name, Description = a.Description, Count = b.Count() })
            //    .OrderBy(n => n.Name)
            //    .ToListAsync();
        }
    }
}
