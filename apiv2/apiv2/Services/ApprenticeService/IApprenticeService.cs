using System;
using apiv2.DTOs.ApprenticeDTO;
using apiv2.Models;

namespace apiv2.Services.ApprenticeService
{
    public interface IApprenticeService
    {
        //Auth
        ApprenticeResponseDTO Authenticate(ApprenticeRequestDTO apprentice);

        //GetBy
        Apprentice GetById(Guid id);
        Apprentice GetByEmail(string email);

        // Create - Delete - Update
        void Create(Apprentice apprentice);
        void Delete(Apprentice apprentice);
        void Update(Apprentice apprentice);
    }
}
