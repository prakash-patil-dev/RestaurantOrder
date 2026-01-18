namespace RestaurantOrder.Models
{
    //internal class Combo
    //{
    //}
    public class Combo
    {
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Unit { get; set; } = string.Empty;
        public double Price { get; set; }
        public double Qty { get; set; }
        public string MainType { get; set; } = string.Empty;
        public string ItemCode { get; set; } = string.Empty;
        public string ItemName { get; set; } = string.Empty;

        public double ItemPrice { get; set; }
        public string ItemUnit { get; set; } = string.Empty;

        public double BaseQty { get; set; }

        public string Alt1Unit { get; set; } = string.Empty;
        public double Alt1Amt { get; set; }
        public double Alt1Qty { get; set; }
        public string Alt2Unit { get; set; } = string.Empty;
        public double Alt2Amt { get; set; }
        public double Alt2Qty { get; set; }
        public string Alt3Unit { get; set; } = string.Empty;
        public double Alt3Amt { get; set; }
        public double Alt3Qty { get; set; }
        public string Alt4Unit { get; set; } = string.Empty;
        public double Alt4Amt { get; set; }
        public double Alt4Qty { get; set; }

        public string LastUser { get; set; } = string.Empty;
        public DateTime LastDate { get; set; }
        public string LastTime { get; set; } = string.Empty;

        public string SubType { get; set; } = string.Empty;
        public string Updated { get; set; } = string.Empty;
    }

    public class ComboDto
    {
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
    }
}
