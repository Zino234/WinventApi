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
    public class Offering
    {
        [Key]
        public Guid OfferingId { get; set; }
        public double OfferingAmount { get; set; }
        public string? OfferingCollectedAt { get; set; }
        public string? OfferingCollectedBy { get; set; }
        public DateTime OfferingcreatedAt { get; set; }
        public ServiceType ServiceType { get; set; }
        [ForeignKey("Officer")]
        public Guid OfficerId { get; set; }
        public Officer? Officer { get; set; }
    }
}
