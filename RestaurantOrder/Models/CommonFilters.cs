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
    public decimal Cash1 { get; set; }
    public decimal Cash2 { get; set; }
    public decimal Cash3 { get; set; }
    public decimal Cash4 { get; set; }
    public decimal Cash5 { get; set; }
    public decimal Cash6 { get; set; }
    public decimal Cash7 { get; set; }
    public decimal Cash8 { get; set; }
}

public class EXRATE : NotifyPropertyBaseViewModel
{


    public DateTime ERATE_DATE { get; set; }
    public string ERATE_CURR { get; set; } = string.Empty;
    public string ERATE_NAM1 { get; set; } = string.Empty;
    public string ERATE_NAM2 { get; set; } = string.Empty;
    public float ERATE_RATE { get; set; }
    public string ERATE_BASE { get; set; } = string.Empty;
    public string ERATE_COMP { get; set; } = string.Empty;
    public string LASTUSER { get; set; } = string.Empty;
    
    public float _ERATE_PaidAmount = 0;
    public float ERATE_PaidAmount { get => _ERATE_PaidAmount; set => SetProperty(ref _ERATE_PaidAmount, value); }
    public DateTime LASTDATE { get; set; }
    public string LASTTIME { get; set; } = string.Empty;
    public string UPDATED { get; set; } = string.Empty;
}

//public class CustomerFilter
//{
//    public string? Mode { get; set; }
//    public Customer? ObjCustomer { get; set; }
//}