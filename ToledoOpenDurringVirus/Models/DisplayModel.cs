using System;
using System.ComponentModel.DataAnnotations;

namespace ToledoOpenDurringVirus.Models
{
    public class DisplayModel
    {
        [Key]
        public Guid LID { get; set; }
        public string Name { get; set; }

        [Display(Name = "Area")]
        public string AreaName { get; set; }

        [Display(Name = "Type")]
        public string TypeName { get; set; }

        [Display(Name = "Open Now?")]
        public bool OpenNow { get; set; } = false;

        public bool FullMenu { get; set; }
        public bool HasSpecials { get; set; }
    }
}
