using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Winvent.Domain.Models
{
    public class Officer
    {
        [Key]
        public Guid OficcerId { get; set; }
        public string? OfficerFirstname { get; set; }
        public string? OfficerLastname { get; set; }
        public string? OfficerUsername { get; set; }
        public string? OfficerPhone { get; set; }
        public string? OfficerEmail { get; set; }
        public string? OfficerPassword { get; set; }
        public bool OfficerIsDisabled { get; set; } = false;
        public DateTime OfficerCreatedAt { get; set; }
    }
}
