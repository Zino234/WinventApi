using System.ComponentModel.DataAnnotations;
using Winvent.Domain.Models.Enums;

namespace WinventApi.DTO.TitheDto
{
    public class AddTitheDto
    {
        public string? TitheName { get; set; }
        [Required]
        public string? TitheCollectedAt { get; set; }
        [Required]
        public string? TitheGivenBy { get; set; }
        public double TitheAmount { get; set; }
        [Required]
        public ServiceType ServiceType { get; set; }
        [Required]
        public Guid OfficerId { get; set; }
    }
}
