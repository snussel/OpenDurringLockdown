using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToledoOpenDurringVirus.Models;

namespace ToledoOpenDurringVirus
{
    public class IsOpenNow
    {
        public static async Task<bool> IsOpen(Guid ID, ResturantDBContext context)
        {
            List<OpenHoursTb> openHours;


                openHours = context.OpenHoursTb
                    .AsEnumerable()
                    .Where(i => i.Lid == ID && i.Day == (int)DateTime.Today.DayOfWeek && Extentions.Between(DateTime.Now.TimeOfDay, TimeSpan.Parse(i.HourOpen), TimeSpan.Parse(i.HourClose)))
                    .ToList();
            

            if (openHours == null || openHours.Count == 0)
                return false;
            else
                return true;
        }
    }
}
