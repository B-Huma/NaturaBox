using System.ComponentModel.DataAnnotations;

namespace e_TicaretApp.Mvc.Models.ViewModels
{
    public class ProductCommentViewModel
    {
        [Required]
        public int Id { get; set; }
        [Required, StringLength(500, MinimumLength = 2)]
        public string Text { get; set; } = null!;
        [Required, Range(1,5)]
        public byte StarCount { get; set; }
        public string UserName { get; set; } = null!;
        

    }
}
