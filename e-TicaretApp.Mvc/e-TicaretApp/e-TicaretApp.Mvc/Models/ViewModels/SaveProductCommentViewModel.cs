using System.ComponentModel.DataAnnotations;

namespace e_TicaretApp.Mvc.Models.ViewModels
{
    public class SaveProductCommentViewModel
    {
        [Required, MinLength(5), MaxLength(500)]
        public string Text { get; set; }
        [Required, Range(1,5)]
        public byte StarCount { get; set; }
    }
}
