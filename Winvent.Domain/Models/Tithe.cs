using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Winvent.Domain.Models.Enums;

namespace Winvent.Domain.Models
{
    public class Tithe
    {
        [Key]
        public Guid TitheId { get; set; }
        public string? TitheName { get; set; }
        public string? TitheCollectedAt { get; set; }
        public string? TitheGivenBy { get; set; }
        public string? TitheCollectedBy { get; set; }
        public double TitheAmount { get; set; }
        public DateTime TitheCreatedAt { get; set; }
        public ServiceType ServiceType { get; set; }

        [ForeignKey("officer")]
        public Guid OfficerId { get; set; }
        public Officer? officer { get; set; }
    }
}
