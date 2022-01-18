using System;
using apiv2.Models;

namespace apiv2.DTOs.ApprenticeDTO
{
    public class ApprenticeResponseDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public ApprenticeResponseDTO(Apprentice apprentice, string token)
        {
            Id = apprentice.Id;
            Name = apprentice.Name;
            Email = apprentice.Email;
            Token = token;
        }
    }
}
