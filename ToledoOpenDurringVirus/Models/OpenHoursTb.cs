using System;
using System.Collections.Generic;

namespace ToledoOpenDurringVirus.Models
{
    public partial class OpenHoursTb
    {
        public int Hid { get; set; }
        public Guid Lid { get; set; }
        public byte Day { get; set; }
        public string HourOpen { get; set; }
        public string HourClose { get; set; }

        public virtual LocationTb L { get; set; }
    }
}
