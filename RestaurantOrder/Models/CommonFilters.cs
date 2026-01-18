namespace RestaurantOrder.Models;
public class LoginRequestFilter
{
    public string? Username { get; set; }
    public string? Password { get; set; }
}

public class CustomerFilter
{
    public string? Mode { get; set; }
    public Customer? ObjCustomer { get; set; }
}


public class CardTypes
{
    public string? CODE { get; set; }
    public string? NAME { get; set; } = string.Empty;
    public float PERCENTAGE { get; set; }
    public string? LOYALTY { get; set; } = string.Empty;
}
//public class CustomerFilter
//{
//    public string? Mode { get; set; }
//    public Customer? ObjCustomer { get; set; }
//}