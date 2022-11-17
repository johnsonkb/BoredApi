namespace BoredApiViewer.Models
{
    public class ActivityInput
    { 
        public string type { get; set; }
        public int participants { get; set; }
        public decimal priceMin { get; set; }
        public decimal priceMax { get; set; }
        public decimal accessibilityMin { get; set; }
        public decimal accessibilityMax { get; set; }
    }
}
