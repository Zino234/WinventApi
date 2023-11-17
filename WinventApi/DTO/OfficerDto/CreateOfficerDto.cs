using System.ComponentModel.DataAnnotations;

namespace WinventApi.DTO.OfficerDto
{
    public class CreateOfficerDto
    {
        [Required]
        public string OfficerFirstname { get; set; }
        [Required]
        public string OfficerLastname { get; set; }
        [Required]
        public string OfficerUsername { get; set; }
        [Required]
        public string OfficerPhone { get; set; }
        [EmailAddress]
        public string OfficerEmail { get; set; }
        [Required]
        public string OfficerPassword { get; set; }
    }
}
