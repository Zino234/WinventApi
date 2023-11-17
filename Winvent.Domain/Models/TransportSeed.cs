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
    public class TransportSeed
    {
        [Key]
        public Guid TransportSeedId { get; set; }
        public string? TransportSeedName { get; set; }
        public string? TransportSeedCollectedAt { get; set; }
        public string? TransportSeedGivenBy { get; set; }
        public double TransportSeedAmount { get; set; }
        public string? TransportSeedCollectedBy { get; set; }
        public DateTime TransportSeedCreatedAt { get; set; }
        public ServiceType ServiceType { get; set; }

        [ForeignKey("officer")]
        public Guid OfficerId { get; set; }
        public Officer? officer { get; set; }
    }
}
