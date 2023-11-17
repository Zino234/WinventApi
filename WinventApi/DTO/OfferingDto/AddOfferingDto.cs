using System.ComponentModel.DataAnnotations;
using Winvent.Domain.Models.Enums;

namespace WinventApi.DTO.OfferingDto
{
    public class AddOfferingDto
    {
        [Required]
        public double OfferingAmount { get; set; }
        [Required]
        public string OfferingCollectedAt { get; set; }
        [Required]
        public ServiceType ServiceType { get; set; }
        [Required]
        public Guid OfficerId { get; set; }
    }
}
