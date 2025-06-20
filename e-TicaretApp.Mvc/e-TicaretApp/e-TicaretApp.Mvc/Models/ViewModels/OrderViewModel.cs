using App.Data.Data.Entities;

namespace e_TicaretApp.Mvc.Models.ViewModels
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string OrderCode { get; set; }
        public string Address { get; set; }
        public string FullName { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<OrderItemViewModel> Items { get; set; } = new();

        public decimal TotalAmount => Items.Sum(item => item.UnitPrice * item.Quantity);
    }
}
