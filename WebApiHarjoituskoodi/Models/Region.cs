﻿using System;
using System.Collections.Generic;

namespace WebApiHarjoituskoodi.Models
{
    public partial class Region
    {
        public Region()
        {
            Shippers = new HashSet<Shippers>();
            Territories = new HashSet<Territories>();
        }

        public int RegionId { get; set; }
        public string RegionDescription { get; set; }

        public virtual ICollection<Shippers> Shippers { get; set; }
        public virtual ICollection<Territories> Territories { get; set; }
    }
}
