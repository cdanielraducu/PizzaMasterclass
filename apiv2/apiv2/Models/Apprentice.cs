using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace apiv2.Models
{
    public class Apprentice
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Level { get; set; }
        public string Email { get; set; }

        [JsonIgnore]
        public string Password { get; set; }

        public Role Role { get; set; }
    }
}
