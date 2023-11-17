using System.ComponentModel.DataAnnotations;
using Winvent.Domain.Models.Enums;

namespace WinventApi.DTO.ExpenseDto
{
    public class UpdateExpenseDto
    {
        [Required]
        public string? ExpenseTitle { get; set; }
        [Required]
        public string? ExpenseDescription { get; set; }
        [Required]
        public float ExpenseAmount { get; set; }
        [Required]
        public string? ExpenseDoneAt { get; set; }
        [Required]
        public Guid OfficerId { get; set; }
        [Required]
        public ServiceType ServiceType { get; set; }
    }
}
