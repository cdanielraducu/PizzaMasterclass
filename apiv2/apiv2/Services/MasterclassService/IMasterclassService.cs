using System;
using System.Collections.Generic;
using apiv2.Models;

namespace apiv2.Services.MasterclassService
{
    public interface IMasterclassService
    {
        IEnumerable<Masterclass> GetAllMasterclasses();
        void Create(Masterclass masterclass);
        void Delete(Masterclass masterclass);
        void Update(Masterclass masterclass);
    }
}
