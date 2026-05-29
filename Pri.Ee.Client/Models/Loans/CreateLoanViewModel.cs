using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Pri.Ee.Client.Models.Loans
{
    public class CreateLoanViewModel
    {
        [Required(ErrorMessage = "Book is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Invalid BookId")]
        public int? BookId { get; set; }
        [Required(ErrorMessage = "User is required")]
        public string UserId { get; set; }
    }
}
