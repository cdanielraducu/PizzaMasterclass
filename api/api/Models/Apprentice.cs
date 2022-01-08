using System;
namespace api.Models
{
    public class Apprentice
    {
        public Apprentice()
        {
        }
        public int ApprenticeId { get; set; }
        public string Username { get; set; }
        public string Department { get; set; }
        public string DateOfJoining { get; set; }
    }
}
