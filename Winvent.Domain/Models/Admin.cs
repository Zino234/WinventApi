using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Winvent.Domain.Models
{
    public class Admin
    {
        [Key]
        public Guid AdminId { get; set; }
        public string? AdminFirstname { get; set; }
        public string? AdminLastname { get; set; }
        public string? AdminUsername { get; set; }
        public string? AdminPhone { get; set; }
        public string? AdminEmail { get; set; }
        public string? AdminPassword { get; set; }
    }
}
