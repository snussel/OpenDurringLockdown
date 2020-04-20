using System;
using System.Collections.Generic;

namespace ToledoOpenDurringVirus.Models
{
    public partial class TypeLookupTb
    {
        public TypeLookupTb()
        {
            LocationTb = new HashSet<LocationTb>();
        }

        public int TypeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<LocationTb> LocationTb { get; set; }
    }
}
