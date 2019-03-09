using System.ComponentModel.DataAnnotations;

namespace TodoList.Web.Models
{
    public class ToDoItem
    {
        public int? Id { get; set; }
        [Required]
        public string Content { get; set; }
    }
}