using System;
using System.Collections.Generic;
using apiv2.DTOs.ApprenticeDTO;
using apiv2.Models;
using apiv2.Repositories.ApprenticeRepository;
using apiv2.Utilities;
using apiv2.Utilities.JWTUtils;
using BCryptNet = BCrypt.Net.BCrypt;

namespace apiv2.Services.ApprenticeService
{
    public class ApprenticeService : IApprenticeService
    {
        private readonly IJWTUtils _JWTUtils;
        public IApprenticeRepository _apprenticeRepository;
        //private readonly AppSettings _appSettings;

        public ApprenticeService(IJWTUtils jWTUtils, IApprenticeRepository apprenticeRepository)
        {
            _JWTUtils = jWTUtils;
            _apprenticeRepository = apprenticeRepository;
        }

        public ApprenticeResponseDTO Authenticate(ApprenticeRequestDTO model)
        {
            var apprentice = _apprenticeRepository.FindByEmail(model.Email);

            if (apprentice == null || !BCryptNet.Verify(model.Password, apprentice.Password))
            {
                return null;
            }

            // Generate Token
            var jwtToken = _JWTUtils.GenerateJWTToken(apprentice);
            return new ApprenticeResponseDTO(apprentice, jwtToken);
        }

        public Apprentice GetById(Guid id)
        {
            var apprentice = _apprenticeRepository.FindById(id);

            if (apprentice == null)
            {
                return null;
            }

            return apprentice;
        }

        public Apprentice GetByEmail(string email)
        {
            var apprentice = _apprenticeRepository.FindByEmail(email);
            if (apprentice == null)
            {
                return null;
            }
            return apprentice;
        }

        public IEnumerable<Apprentice> GetAllApprentices()
        {
            var allDepartments = _apprenticeRepository.GetAll();
            if (allDepartments == null)
            {
                return null;
            }
            return allDepartments;
        }


        public void Create(Apprentice apprentice)
        {
            _apprenticeRepository.Create(apprentice);
            _apprenticeRepository.Save();
        }

        public void Delete(Apprentice apprentice)
        {
            _apprenticeRepository.Remove(apprentice);
            _apprenticeRepository.Save();
        }

        public void Update(Apprentice apprentice)
        {
            _apprenticeRepository.Update(apprentice);
            _apprenticeRepository.Save();
        }
    }
}
