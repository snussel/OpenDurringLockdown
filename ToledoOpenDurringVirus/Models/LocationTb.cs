using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ToledoOpenDurringVirus.Models
{
    public partial class LocationTb
    {
        public LocationTb()
        {
            OpenHoursTb = new HashSet<OpenHoursTb>();
        }

        [Key]
        public Guid Lid { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        
        [Display(Name = "Area")]
        public int AreaId { get; set; }

        [Display(Name = "Type")]
        public int TypeId { get; set; }
        public string FaceBookId { get; set; }
        public string TwitterId { get; set; }
        public string InstaGramID { get; set; }

        [DataType(DataType.Url)]
        [Display(Name = "Web Site")]
        public string WebSiteUrl { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Phone #")]        
        public string Phone { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }

        [DataType(DataType.PostalCode)]
        public string Zipcode { get; set; }

        [Display(Name = "Full Menu?")]
        public bool FullMenu { get; set; }

        [Display(Name = "Has Specials?")]
        public bool HasSpecials { get; set; }


        public virtual AreaLookupTb Area { get; set; }
        public virtual TypeLookupTb Type { get; set; }

        public virtual ICollection<OpenHoursTb> OpenHoursTb { get; set; }
    }
}
