using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using apiv2.Models;
using apiv2.Repositories.GenericRepository;

namespace apiv2.Repositories.ApprenticeRepository
{
    public interface IApprenticeRepository: IGenericRepository<Apprentice>
    {
        IEnumerable<Apprentice> GetAllMasterApprentices();
        public Apprentice FindByEmail(string email);
    }
}
