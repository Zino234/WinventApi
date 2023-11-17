using System.ComponentModel.DataAnnotations;

namespace WinventApi.DTO.OfficerDto
{
    public class OfficerLoginDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
