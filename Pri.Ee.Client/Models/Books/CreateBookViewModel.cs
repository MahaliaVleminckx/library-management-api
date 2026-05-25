namespace Pri.Ee.Client.Models.Books
{
    public class CreateBookViewModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int AuthorId { get; set; }
        public int CategoryId { get; set; }
    }
}
