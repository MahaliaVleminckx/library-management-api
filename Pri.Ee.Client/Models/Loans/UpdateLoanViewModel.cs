using System.ComponentModel.DataAnnotations;

namespace Pri.Ee.Client.Models.Loans
{
    public class UpdateLoanViewModel
    {
        [Required(ErrorMessage = "Return date is required")]
        public DateTime? ReturnDate { get; set; }
        public DateTime LoanDate { get; set; }
    }
}
