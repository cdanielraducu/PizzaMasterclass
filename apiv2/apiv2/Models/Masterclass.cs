using System;
using System.Collections.Generic;
using apiv2.Models.AssociativeTables;

namespace apiv2.Models
{
    public class Masterclass
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<MasterclassApprentice> MasterclassApprentices { get; set; }
        
    }
}
