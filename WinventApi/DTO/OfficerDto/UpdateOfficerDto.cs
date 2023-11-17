using System.ComponentModel.DataAnnotations;

namespace WinventApi.DTO.OfficerDto
{
    public class UpdateOfficerDto
    {
        [Required]
        public string? OfficerFirstname { get; set; }
        [Required]
        public string? OfficerLastname { get; set; }
        [Required]
        public string? OfficerUsername { get; set; }
        [Required]
        public string? OfficerPhone { get; set; }
        [Required]
        public string? OfficerEmail { get; set; }
        [Required]
        public string? OfficerPassword { get; set; }
    }
}
