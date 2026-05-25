namespace Pri.Ee.Client.Models.Loans
{
    public class CreateLoanViewModel
    {
        public int BookId { get; set; }
        public string UserId { get; set; }
        public DateTime LoanDate { get; set; }
        public DateTime? ReturnDate { get; set; }
    }
}
