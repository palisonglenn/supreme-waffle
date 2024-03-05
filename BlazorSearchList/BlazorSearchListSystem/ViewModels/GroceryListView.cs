namespace BlazorSearchListSystem.ViewModels
{
    public class GroceryListView
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }
        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }
        public decimal MinDiscount { get; set; }
        public decimal MaxDiscount { get; set; }

    }
}