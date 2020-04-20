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
    public class DetailsModel : PageModel
    {
        private readonly ToledoOpenDurringVirus.Models.ResturantDBContext _context;
        public List<LocationTb> ListOfLocations { get; set; }

        public DetailsModel(ToledoOpenDurringVirus.Models.ResturantDBContext context)
        {
            _context = context;
        }

        public TypeLookupTb TypeLookupTb { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TypeLookupTb = await _context.TypeLookupTb.FirstOrDefaultAsync(m => m.Name == id);

            if (TypeLookupTb == null)
            {
                return NotFound();
            }

            ListOfLocations = _context.LocationTb.Where(l => l.TypeId == TypeLookupTb.TypeId).Include(t => t.Area).OrderBy(a => a.Area.Name).ToList();

            return Page();
        }
    }
}
