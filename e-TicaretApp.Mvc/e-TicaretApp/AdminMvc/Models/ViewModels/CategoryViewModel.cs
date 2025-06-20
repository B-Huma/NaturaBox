using System.ComponentModel.DataAnnotations;

namespace AdminMvc.Models.ViewModels
{
    public class CategoryViewModel
    {
        public int Id { get; set; }
        [MinLength(2)]
        public string Name { get; set; } = default!;
        [MinLength(3)]
        public string Color { get; set; } = default!;
        [MinLength(2)]
        public string IconCssClass { get; set; } = default!;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
