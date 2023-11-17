using System.ComponentModel.DataAnnotations;
using Winvent.Domain.Models.Enums;

namespace WinventApi.DTO.TransportSeedDto
{
    public class UpdateTransportSeedDto
    {
        public string? TransportSeedName { get; set; }
        public string? TransportSeedCollectedAt { get; set; }
        public string? TransportSeedGivenBy { get; set; }
        public double TransportSeedAmount { get; set; }
        [Required]
        public ServiceType ServiceType { get; set; }
    }
}
