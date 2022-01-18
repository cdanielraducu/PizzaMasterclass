using System;
using System.Collections.Generic;
using apiv2.Models;
using apiv2.Repositories.GenericRepository;
using apiv2.Repositories.MasterclassRepository;

namespace apiv2.Services.MasterclassService
{
    public class MasterclassService : IMasterclassService
    {
        public IMasterclassRepository _masterclassRepository;
        public MasterclassService(IMasterclassRepository masterclassRepository)
        {
            _masterclassRepository = masterclassRepository;
        }

            public void Create(Masterclass masterclass)
            {
                _masterclassRepository.Create(masterclass);
                _masterclassRepository.Save();
            }

            public void Delete(Masterclass masterclass)
            {
                _masterclassRepository.Remove(masterclass);
                _masterclassRepository.Save();
            }

            public IEnumerable<Masterclass> GetAllMasterclasses()
            {
                var allMasterclasses = _masterclassRepository.GetAll();
                if (allMasterclasses == null)
                {
                    return null;
                }
                return allMasterclasses;
            }

            public void Update(Masterclass masterclass)
            {
                _masterclassRepository.Update(masterclass);
                _masterclassRepository.Save();
            }
    }
}
