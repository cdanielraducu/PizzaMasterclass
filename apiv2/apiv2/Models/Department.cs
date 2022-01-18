using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace apiv2.Models
{
    public class Department
    {
        public Guid Id { get; set; }
        public string Level { get; set; }
    }
}
