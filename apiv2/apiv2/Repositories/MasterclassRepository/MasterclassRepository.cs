using System;
using apiv2.Data;
using apiv2.Models;
using apiv2.Repositories.GenericRepository;

namespace apiv2.Repositories.MasterclassRepository
{
    public class MasterclassRepository : GenericRepository<Masterclass>, IMasterclassRepository
    {
        public MasterclassRepository(ProjectContext context) : base(context)
        {
        }

    }
}
