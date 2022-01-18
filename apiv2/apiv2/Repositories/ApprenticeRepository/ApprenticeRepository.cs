using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using apiv2.Data;
using apiv2.Models;
using apiv2.Repositories.GenericRepository;

namespace apiv2.Repositories.ApprenticeRepository
{
    public class ApprenticeRepository : GenericRepository<Apprentice>, IApprenticeRepository
    {
        public ApprenticeRepository(ProjectContext context): base(context)
        {
        }
        public IEnumerable<Apprentice> GetAllMasterApprentices()
        {
            return _table.Where(x => x.Level.Equals("Master"));
        }


        public Apprentice FindByEmail(string email)
        {
            return _table.FirstOrDefault(x => x.Email == email);
        }

    }
}
