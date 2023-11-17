using System.ComponentModel.DataAnnotations;

namespace WinventApi.DTO.AdminDto
{
    public class AdminLoginDto
    {
        [Required]
        public string? AdminUsername { get; set; }
        [Required]
        public string? AdminPassword { get; set; }
    }
}
