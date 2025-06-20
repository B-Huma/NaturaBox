using System.ComponentModel.DataAnnotations;

namespace AdminMvc.Models.ViewModels
{
    public class CommentViewModel
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public byte StarCount { get; set; }
        public bool IsConfirmed { get; set; }
        public DateTime CreatedAt { get; set; } 
        public string ProductName { get; set; }
        public string UserFullName { get; set; }
    }
}
