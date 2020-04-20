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
    public class DetailsModel : PageModel
    {
        private readonly ResturantDBContext _context;
        public List<LocationTb> ListOfLocations { get; set; }

        public DetailsModel(ResturantDBContext context)
        {
            _context = context;
        }

        public AreaLookupTb AreaLookupTb { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            AreaLookupTb = await _context.AreaLookupTb.FirstOrDefaultAsync(m => m.Name == id);

            if (AreaLookupTb == null)
            {
                return NotFound();
            }

            ListOfLocations = _context.LocationTb.Where(l => l.AreaId == AreaLookupTb.AreaId).Include(t => t.Type).OrderBy(n => n.Name).ToList();

            return Page();
        }
    }
}
