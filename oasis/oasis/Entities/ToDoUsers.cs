using System;
using System.ComponentModel.DataAnnotations;

namespace oasis.Entities
{
    public class ToDoUsers
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public AppUser User { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;

    }
}
