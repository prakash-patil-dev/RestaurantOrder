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

public class SysParam
{
    public float Cash1 { get; set; }
    public float Cash2 { get; set; }
    public float Cash3 { get; set; }
    public float Cash4 { get; set; }
    public float Cash5 { get; set; }
    public float Cash6 { get; set; }
    public float Cash7 { get; set; }
    public float Cash8 { get; set; }
}

public class EXRATE
{
    public DateTime ERATE_DATE { get; set; }
    public string ERATE_CURR { get; set; } = string.Empty;
    public string ERATE_NAM1 { get; set; } = string.Empty;
    public string ERATE_NAM2 { get; set; } = string.Empty;
    public float ERATE_RATE { get; set; }
    public string ERATE_BASE { get; set; } = string.Empty;
    public string ERATE_COMP { get; set; } = string.Empty;
    public string LASTUSER { get; set; } = string.Empty;
    public DateTime LASTDATE { get; set; }
    public string LASTTIME { get; set; } = string.Empty;
    public string UPDATED { get; set; } = string.Empty;
}

//public class CustomerFilter
//{
//    public string? Mode { get; set; }
//    public Customer? ObjCustomer { get; set; }
//}