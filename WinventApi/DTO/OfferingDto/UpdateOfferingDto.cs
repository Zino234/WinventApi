using System.ComponentModel.DataAnnotations;
using Winvent.Domain.Models.Enums;

namespace WinventApi.DTO.OfferingDto
{
    public class UpdateOfferingDto
    {
        [Required]
        public double OfferingAmount { get; set; }
        [Required]
        public string OfferingCollectedAt { get; set; }
        [Required]
        public ServiceType ServiceType { get; set; }
    }
}
