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
    public class Expense
    {
        [Key]
        public Guid ExpenseId { get; set; }
        public string? ExpenseTitle { get; set; }
        public string? ExpenseDescription { get; set; }
        public float ExpenseAmount { get; set; }
        public string? ExpenseDoneAt { get; set; }
        public DateTime ExpenseCreatedAt { get; set; }
        public string? ExpenseDoneBy { get; set; }
        public ServiceType ServiceType { get; set; }

        [ForeignKey("officer")]
        public Guid OfficerId { get; set; }
       
        public Officer? officer { get; set; }
    }
}
