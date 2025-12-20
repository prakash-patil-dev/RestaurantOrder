using Microsoft.VisualBasic;
using System.Collections.ObjectModel;

namespace RestaurantOrder.Models
{
    public class INVHEAD : NotifyPropertyBaseViewModel
    {
        private double _SUBTOTAL;
        public double SUBTOTAL { get => _SUBTOTAL; set => SetProperty(ref _SUBTOTAL, value); }

        private string? _BRANCHCODE;
        public string? BRANCHCODE { get => _BRANCHCODE; set => SetProperty(ref _BRANCHCODE, value); }


        private double _TXNNO;
        public double TXNNO { get => _TXNNO; set => SetProperty(ref _TXNNO, value); }

        private DateTime _TXNDT;
        public DateTime TXNDT { get => _TXNDT; set => SetProperty(ref _TXNDT, value); }

        private string? _VIPNO = string.Empty;
        public string? VIPNO { get => _VIPNO; set => SetProperty(ref _VIPNO, value); }

        private string? _SHIFT = string.Empty;
        public string? SHIFT { get => _SHIFT; set => SetProperty(ref _SHIFT, value); }

        private string? _USER = string.Empty;
        public string? USER { get => _USER; set => SetProperty(ref _USER, value); }

        private string? _STAFF = string.Empty;
        public string? STAFF { get => _STAFF; set => SetProperty(ref _STAFF, value); }

        private double _ITEMDISC;
        public double ITEMDISC { get => _ITEMDISC; set => SetProperty(ref _ITEMDISC, value); }

        private double _BILLAMOUNT;
        public double BILLAMOUNT { get => _BILLAMOUNT; set => SetProperty(ref _BILLAMOUNT, value); }


        private double _TOTCOSTAMT;
        public double TOTCOSTAMT { get => _TOTCOSTAMT; set => SetProperty(ref _TOTCOSTAMT, value); }

        private DateTime? _DUEDATE;
        public DateTime? DUEDATE { get => _DUEDATE; set => SetProperty(ref _DUEDATE, value); }

        private double? _SPLPERC;
        public double? SPLPERC { get => _SPLPERC; set => SetProperty(ref _SPLPERC, value); }

        private double _SPLSALE;
        public double SPLSALE { get => _SPLSALE; set => SetProperty(ref _SPLSALE, value); }

        private string? _SPLNAME = string.Empty;
        public string? SPLNAME { get => _SPLNAME; set => SetProperty(ref _SPLNAME, value); }

        private double _DISCPERC;
        public double DISCPERC { get => _DISCPERC; set => SetProperty(ref _DISCPERC, value); }

        private double _BILLDISC;
        public double BILLDISC { get => _BILLDISC; set => SetProperty(ref _BILLDISC, value); }

        private double _BILLCASH;
        public double BILLCASH { get => _BILLCASH; set => SetProperty(ref _BILLCASH, value); }

        private double _BILLCARD;
        public double BILLCARD { get => _BILLCARD; set => SetProperty(ref _BILLCARD, value); }

        private double _DEPCASH;
        public double DEPCASH { get => _DEPCASH; set => SetProperty(ref _DEPCASH, value); }

        private double _DEPCARD;
        public double DEPCARD { get => _DEPCARD; set => SetProperty(ref _DEPCARD, value); }

        private double _DEPCURR;
        public double DEPCURR { get => _DEPCURR; set => SetProperty(ref _DEPCURR, value); }

        private double _BILLCURR;
        public double BILLCURR { get => _BILLCURR; set => SetProperty(ref _BILLCURR, value); }

        private double? _EXCHRATE;
        public double? EXCHRATE { get => _EXCHRATE; set => SetProperty(ref _EXCHRATE, value); }

        private double? _TIPCASH;
        public double? TIPCASH { get => _TIPCASH; set => SetProperty(ref _TIPCASH, value); }

        private double? _TIPCARD;
        public double? TIPCARD { get => _TIPCARD; set => SetProperty(ref _TIPCARD, value); }

        private string? _TIPCRDNAME;
        public string? TIPCRDNAME { get => _TIPCRDNAME; set => SetProperty(ref _TIPCRDNAME, value); }

        private double? _TAXAMT;
        public double? TAXAMT { get => _TAXAMT; set => SetProperty(ref _TAXAMT, value); }

        private double _TAX1AMT;
        public double TAX1AMT { get => _TAX1AMT; set => SetProperty(ref _TAX1AMT, value); }

        private double _TAX2AMT;
        public double TAX2AMT { get => _TAX2AMT; set => SetProperty(ref _TAX2AMT, value); }

        private double _TAXVATAMT;
        public double TAXVATAMT { get => _TAXVATAMT; set => SetProperty(ref _TAXVATAMT, value); }

        private double _TABLENO;
        public double TABLENO { get => _TABLENO; set => SetProperty(ref _TABLENO, value); }

        private double _NOOFPERSNS;
        public double NOOFPERSNS { get => _NOOFPERSNS; set => SetProperty(ref _NOOFPERSNS, value); }

        private string? _EATTAKE = "E";
        public string? EATTAKE { get => _EATTAKE; set => SetProperty(ref _EATTAKE, value); }

        private double? _LPRINTED;
        public double? LPRINTED { get => _LPRINTED; set => SetProperty(ref _LPRINTED, value); }
        
        private double? _ABSORBGST;
        public double? ABSORBGST { get => _ABSORBGST; set => SetProperty(ref _ABSORBGST, value); }
        
        private string? _GSTREFNO;
        public string? GSTREFNO { get => _GSTREFNO; set => SetProperty(ref _GSTREFNO, value); }
       
        private string? _REFERENCE1;
        public string? REFERENCE1 { get => _REFERENCE1; set => SetProperty(ref _REFERENCE1, value); }

        private string? _REFERENCE2;
        public string? REFERENCE2 { get => _REFERENCE2; set => SetProperty(ref _REFERENCE2, value); }
        
        private string? _CUSTNAME;
        public string? CUSTNAME { get => _CUSTNAME; set => SetProperty(ref _CUSTNAME, value); }

        private string? _CUSTADD1;
        public string? CUSTADD1 { get => _CUSTADD1; set => SetProperty(ref _CUSTADD1, value); }
       
        private string? _CUSTADD2;
        public string? CUSTADD2 { get => _CUSTADD2; set => SetProperty(ref _CUSTADD2, value); }
       
        private string? _CUSTADD3;
        public string? CUSTADD3 { get => _CUSTADD3; set => SetProperty(ref _CUSTADD3, value); }
       
        private double? _RECEIPTNO;
        public double? RECEIPTNO { get => _RECEIPTNO; set => SetProperty(ref _RECEIPTNO, value); }

        private string? _STATUS;
        public string? STATUS { get => _STATUS; set => SetProperty(ref _STATUS, value); }
        
        private string? _REMARKS;
        public string? REMARKS { get => _REMARKS; set => SetProperty(ref _REMARKS, value); }
        
        private double? _REPRINT;
        public double? REPRINT { get => _REPRINT; set => SetProperty(ref _REPRINT, value); }
        
        private string? _UPDATED;
        public string? UPDATED { get => _UPDATED; set => SetProperty(ref _UPDATED, value); }

        private double _lessamount;
        public double lessamount { get => _lessamount; set => SetProperty(ref _lessamount, value); }

        private double _GST;
        public double GST { get => _GST; set => SetProperty(ref _GST, value); }

        private double _tendered;
        public double tendered { get => _tendered; set => SetProperty(ref _tendered, value); }

        private double _BALANCE;
        public double BALANCE { get => _BALANCE; set => SetProperty(ref _BALANCE, value); }

        private string? _LASTUSER;
        public string? LASTUSER { get => _LASTUSER; set => SetProperty(ref _LASTUSER, value); }

        private DateTime _LASTDATE;
        public DateTime LASTDATE { get => _LASTDATE; set => SetProperty(ref _LASTDATE, value); }

        private string? _LASTTIME;
        public string? LASTTIME { get => _LASTTIME; set => SetProperty(ref _LASTTIME, value); }

        private string? _STATIONID;
        public string? STATIONID { get => _STATIONID; set => SetProperty(ref _STATIONID, value); }

        private string? _SETTLETYPE;
        public string? SETTLETYPE { get => _SETTLETYPE; set => SetProperty(ref _SETTLETYPE, value); }

        private string? _ROOMNO = string.Empty;
        public string? ROOMNO { get => _ROOMNO; set => SetProperty(ref _ROOMNO, value); }

        private string? _ProviderCd = string.Empty;
        public string? ProviderCd { get => _ProviderCd; set => SetProperty(ref _ProviderCd, value); }
    }


    public class INVHEADDETAILS : NotifyPropertyBaseViewModel
    {
        //public INVHEAD? CurrentOpenBill { get; set; }
        //public ObservableCollection<INVLINE>? CurrentOpenBillDetails { get; set; }

        private INVHEAD _CurrentOpenBill = new();
        public INVHEAD CurrentOpenBill { get => _CurrentOpenBill; set { SetProperty(ref _CurrentOpenBill, value); } }

        private ObservableCollection<INVLINE> _CurrentOpenBillDetails = new();
        public ObservableCollection<INVLINE> CurrentOpenBillDetails { get => _CurrentOpenBillDetails; set { SetProperty(ref _CurrentOpenBillDetails, value); OnPropertyChanged(); } }
       
        private bool _ISVoidMode = false;
        public bool ISVoidMode { get => _ISVoidMode; set => SetProperty(ref _ISVoidMode, value); }


    }
}