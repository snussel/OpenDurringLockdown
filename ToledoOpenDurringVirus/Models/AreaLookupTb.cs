using System;
using System.Collections.Generic;

namespace ToledoOpenDurringVirus.Models
{
    public partial class AreaLookupTb
    {
        public AreaLookupTb()
        {
            LocationTb = new HashSet<LocationTb>();
        }

        public int AreaId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<LocationTb> LocationTb { get; set; }
    }
}
