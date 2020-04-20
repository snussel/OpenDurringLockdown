using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ToledoOpenDurringVirus.Models;

namespace ToledoOpenDurringVirus.Pages.Type
{
    public class IndexModel : PageModel
    {
        private readonly ToledoOpenDurringVirus.Models.ResturantDBContext _context;

        public IndexModel(ToledoOpenDurringVirus.Models.ResturantDBContext context)
        {
            _context = context;
        }

        public IList<GroupingStats> TypeLookupTb { get;set; }

        public async Task OnGetAsync()
        {
            //TypeLookupTb = await _context.TypeLookupTb.ToListAsync();

            TypeLookupTb = _context.TypeLookupTb
                .AsEnumerable()
                .GroupJoin(_context.LocationTb, a => a.TypeId, b => b.TypeId, (a, b) => new GroupingStats { ID = a.TypeId, Name = a.Name, Description = a.Description, Count = b.Count() })
                .OrderBy(n => n.Name)
                .ToList();



        }
    }
}
