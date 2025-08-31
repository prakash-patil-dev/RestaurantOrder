namespace RestaurantOrder.Models
{
    public class CATEGORY
    {
        public string? CODE { get; set; } // Unchecked, so nullable
        public string? NAME { get; set; } = string.Empty;
        public string? UPDATED { get; set; } = string.Empty; // Typically "0" or "1"
        public string? LASTUSER { get; set; } = string.Empty;
        public DateTime LASTDATE { get; set; }
        public string? LASTTIME { get; set; } = string.Empty;
        public string? DEPTCODE { get; set; } = string.Empty;
    }
}
