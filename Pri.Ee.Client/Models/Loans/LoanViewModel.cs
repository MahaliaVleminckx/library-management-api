namespace Pri.Ee.Client.Models.Loans
{
    public class LoanViewModel
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public string BookTitle { get; set; }
        public string UserId  { get; set; }
        public string Username {  get; set; }
        public DateTime LoanDate { get; set; }
        public DateTime? ReturnDate { get; set; }
    }
}
