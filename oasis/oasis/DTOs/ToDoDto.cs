using System.ComponentModel.DataAnnotations;

namespace oasis.DTOs
{
    public class ToDoDto
    {
        [Required(ErrorMessage = "Field is Required")]
        [MinLength(1, ErrorMessage = "You must not enter less than 1 characters")]
        [MaxLength(30, ErrorMessage = "You must not enter more than 30 characters")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Field is Required")]
        public string Description { get; set; }
    }
    public class UpdateToDoDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Field is Required")]
        [MinLength(1, ErrorMessage = "You must not enter less than 1 characters")]
        [MaxLength(30, ErrorMessage = "You must not enter more than 30 characters")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Field is Required")]
        public string Description { get; set; }
    }
}
