using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToledoOpenDurringVirus.Models;

namespace ToledoOpenDurringVirus.Pages.Location
{
    public class DetailsModel : PageModel
    {
        private readonly ToledoOpenDurringVirus.Models.ResturantDBContext _context;

        public DetailsModel(ToledoOpenDurringVirus.Models.ResturantDBContext context)
        {
            _context = context;
        }

        public LocationTb LocationTb { get; set; }
        public List<ListDetails> openHours { get; set; }
        public bool openNow { get; set; } = false;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            LocationTb = await _context.LocationTb
                .Include(l => l.Area)
                .Include(l => l.Type).FirstOrDefaultAsync(m => m.Lid == id);

            if (LocationTb == null)
            {
                return NotFound();
            }

            var HourCollections = await _context.OpenHoursTb.Where(i => i.Lid == id).OrderBy(d => d.Day).ToListAsync();

            openHours = ConvertData(HourCollections);
            openNow = await IsOpenNow.IsOpen(LocationTb.Lid, _context);

            return Page();
        }

        
        private List<ListDetails> ConvertData(List<OpenHoursTb> openHours)
        {
            List<ListDetails> returnCollection = new List<ListDetails>();

            foreach(var c in openHours)
            {
                StringBuilder cs = new StringBuilder();

                switch(c.Day)
                {
                    case 1:
                        cs.Append(DayOfWeek.Monday);
                        break;
                    case 2:
                        cs.Append(DayOfWeek.Tuesday);
                        break;
                    case 3:
                        cs.Append(DayOfWeek.Wednesday);
                        break;
                    case 4:
                        cs.Append(DayOfWeek.Thursday);
                        break;
                    case 5:
                        cs.Append(DayOfWeek.Friday);
                        break;
                    case 6:
                        cs.Append(DayOfWeek.Saturday);
                        break;
                    case 0:
                        cs.Append(DayOfWeek.Sunday);
                        break;
                }

                cs.Append(": ");
                cs.Append(convert12(c.HourOpen));
                cs.Append(" => ");
                cs.Append(convert12(c.HourClose));

                returnCollection.Add(new ListDetails { listContent = cs.ToString(), isToday = (c.Day == (int)DateTime.Today.DayOfWeek) });
            }

            return returnCollection;
        }


        private static string convert12(string str)
        {
            string returnValue = "";


            //hours
            int hours = Convert.ToInt32(str.Substring(0, 2));
            
            switch(hours)
            {
                case 0:
                    returnValue = $"12:{str.Substring(3, 2)} AM";
                    break;
                case 12:
                    returnValue = $"12:{str.Substring(3, 2)} PM";
                    break;
                default:
                    if (hours < 12)
                        returnValue = $"{hours.ToString().PadLeft(2, '0')}:{str.Substring(3, 2)} AM";
                    else
                        returnValue = $"{(hours - 12).ToString().PadLeft(2, '0')}:{str.Substring(3, 2)} PM";
                    break;
            }

            return returnValue;
        }
    }

    public class ListDetails
    {
        public string listContent { get; set; }
        public bool isToday { get; set; }
    }
}
