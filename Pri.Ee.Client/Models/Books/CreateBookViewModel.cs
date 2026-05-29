using System.ComponentModel.DataAnnotations;

namespace Pri.Ee.Client.Models.Books
{
    public class CreateBookViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [StringLength(100,  ErrorMessage = "Name can't be a longer than 100 characters")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Author is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Invalid AuthorId")]
        public int AuthorId { get; set; }

        [Required(ErrorMessage = "Category is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Invalid CategoryId")]
        public int? CategoryId { get; set; }
    }
}
