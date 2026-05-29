using System.ComponentModel.DataAnnotations;

namespace Pri.Ee.Client.Models.Categories
{
    public class CreateCategoryViewModel
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name can't be a longer than 100 characters")]
        public string Name { get; set; }
    }
}
