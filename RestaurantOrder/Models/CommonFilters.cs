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