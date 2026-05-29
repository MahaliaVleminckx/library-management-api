namespace Pri.Ee.Client.Models.Books
{
    public class BookViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string AuthorName { get; set; }

        public int AuthorId { get; set; }
        public int? CategoryId { get; set; }
    }
}
