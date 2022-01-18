using System;
namespace apiv2.Models.AssociativeTables
{
    public class MasterclassApprentice
    {
        public Guid ApprenticeId { get; set; }
        public Guid MasterclassId { get; set; }
        public virtual Apprentice Apprentice { get; set; }
        public virtual Masterclass Masterclass { get; set; }
        
    }
}
