using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantOrder.Models
{
    public class INVLINE : NotifyPropertyBaseViewModel
    {
        private string? _branchCode;
        public string? BRANCHCODE { get => _branchCode; set => SetProperty(ref _branchCode, value); }

        private double? _TXNNO;
        public double? TXNNO { get => _TXNNO; set => SetProperty(ref _TXNNO, value); }

        private DateTime _TXNDT;
        public DateTime TXNDT { get => _TXNDT; set => SetProperty(ref _TXNDT, value); }

        private string? _VIPNO;
        public string? VIPNO { get => _VIPNO; set => SetProperty(ref _VIPNO, value); }

        private string? _SHIFT;
        public string? SHIFT { get => _SHIFT; set => SetProperty(ref _SHIFT, value); }

        private string? _USER;
        public string? USER { get => _USER; set => SetProperty(ref _USER, value); }

        private string? _STAFF;
        public string? STAFF { get => _STAFF; set => SetProperty(ref _STAFF, value); }

        private int _LINE;
        public int LINE { get => _LINE; set => SetProperty(ref _LINE, value); }


        private string? _ITEMCODE;
        public string? ITEMCODE { get => _ITEMCODE; set => SetProperty(ref _ITEMCODE, value); }

        private string? _ITEMNAME1;
        public string? ITEMNAME1 { get => _ITEMNAME1; set => SetProperty(ref _ITEMNAME1, value); }

        private string? _ITEMNAME2;
        public string? ITEMNAME2 { get => _ITEMNAME2; set => SetProperty(ref _ITEMNAME2, value); }

        private string? _CATCODE;
        public string? CATCODE { get => _CATCODE; set => SetProperty(ref _CATCODE, value); }

        private string? _SUBCATCODE;
        public string? SUBCATCODE { get => _SUBCATCODE; set => SetProperty(ref _SUBCATCODE, value); }

        private string? _BRANDCODE;
        public string? BRANDCODE { get => _BRANDCODE; set => SetProperty(ref _BRANDCODE, value); }

        private string? _UNITCODE;
        public string? UNITCODE { get => _UNITCODE; set => SetProperty(ref _UNITCODE, value); }

        private double? _quantity;
        public double? QUANTITY { get => _quantity; set => SetProperty(ref _quantity, value); }

        private double? _UNITRATE;
        public double? UNITRATE { get => _UNITRATE; set => SetProperty(ref _UNITRATE, value); }

        private double? _amount;
        public double? AMOUNT { get => _amount; set => SetProperty(ref _amount, value); }

        private double? _TAXPERC;
        public double? TAXPERC { get => _TAXPERC; set => SetProperty(ref _TAXPERC, value); }

        private double? _TAXVALUE;
        public double? TAXVALUE { get => _TAXVALUE; set => SetProperty(ref _TAXVALUE, value); }

        private double? _COSTAMT;
        public double? COSTAMT { get => _COSTAMT; set => SetProperty(ref _COSTAMT, value); }

        private double? _COSTAMTSPA;
        public double? COSTAMTSPA { get => _COSTAMTSPA; set => SetProperty(ref _COSTAMTSPA, value); }

        private string? _SPLDISC;
        public string? SPLDISC { get => _SPLDISC; set => SetProperty(ref _SPLDISC, value); }

        private double? _DISCPERC;
        public double? DISCPERC { get => _DISCPERC; set => SetProperty(ref _DISCPERC, value); }

        private double? _DISCOUNT;
        public double? DISCOUNT { get => _DISCOUNT; set => SetProperty(ref _DISCOUNT, value); }

        private double? _LESSAMT;
        public double? LESSAMT { get => _LESSAMT; set => SetProperty(ref _LESSAMT, value); }

        private string? _PRINTED;
        public string? PRINTED { get => _PRINTED; set => SetProperty(ref _PRINTED, value); }

        private string? _BPRINTED;
        public string? BPRINTED { get => _BPRINTED; set => SetProperty(ref _BPRINTED, value); }

        private string? _KPRINTED;
        public string? KPRINTED { get => _KPRINTED; set => SetProperty(ref _KPRINTED, value); }

        private string? _EATTAKE;
        public string? EATTAKE { get => _EATTAKE; set => SetProperty(ref _EATTAKE, value); }

        private string? _STATUS;
        public string? STATUS { get => _STATUS; set => SetProperty(ref _STATUS, value); }

        private string? _UPDATED;
        public string? UPDATED { get => _UPDATED; set => SetProperty(ref _UPDATED, value); }

        private string? _STYLECODE;
        public string? STYLECODE { get => _STYLECODE; set => SetProperty(ref _STYLECODE, value); }

        private string? _COLORCODE;
        public string? COLORCODE { get => _COLORCODE; set => SetProperty(ref _COLORCODE, value); }

        private string? _SIZECODE;
        public string? SIZECODE { get => _SIZECODE; set => SetProperty(ref _SIZECODE, value); }

        private string? _STARTTIME;
        public string? STARTTIME { get => _STARTTIME; set => SetProperty(ref _STARTTIME, value); }

        private string? _ENDTIME;
        public string? ENDTIME { get => _ENDTIME; set => SetProperty(ref _ENDTIME, value); }

        private double? _PACKAGEID;
        public double? PACKAGEID { get => _PACKAGEID; set => SetProperty(ref _PACKAGEID, value); }

        private double? _PACKLINE;
        public double? PACKLINE { get => _PACKLINE; set => SetProperty(ref _PACKLINE, value); }

        private double? _REQCUST;
        public double? REQCUST { get => _REQCUST; set => SetProperty(ref _REQCUST, value); }

        private double? _REQCOMPANY;
        public double? REQCOMPANY { get => _REQCOMPANY; set => SetProperty(ref _REQCOMPANY, value); }

        private string? _REQFLAG;
        public string? REQFLAG { get => _REQFLAG; set => SetProperty(ref _REQFLAG, value); }

        private string? _LASTUSER;
        public string? LASTUSER { get => _LASTUSER; set => SetProperty(ref _LASTUSER, value); }

        private DateTime _LASTDATE;
        public DateTime LASTDATE { get => _LASTDATE; set => SetProperty(ref _LASTDATE, value); }

        private string? _LASTTIME;
        public string? LASTTIME { get => _LASTTIME; set => SetProperty(ref _LASTTIME, value); }

        private string? _REPORTFLAG;
        public string? REPORTFLAG { get => _REPORTFLAG; set => SetProperty(ref _REPORTFLAG, value); }

        private string? _KOT;
        public string? KOT { get => _KOT; set => SetProperty(ref _KOT, value); }

        private string? _SCANITEMCODE;
        public string? SCANITEMCODE { get => _SCANITEMCODE; set => SetProperty(ref _SCANITEMCODE, value); }

        private string? _STATIONID;
        public string? STATIONID { get => _STATIONID; set => SetProperty(ref _STATIONID, value); }

        private string? _PREPARED;
        public string? PREPARED { get => _PREPARED; set => SetProperty(ref _PREPARED, value); }

        private string? _PRICETYPE;
        public string? PRICETYPE { get => _PRICETYPE; set => SetProperty(ref _PRICETYPE, value); }

        private string? _TOPPING;
        public string? TOPPING { get => _TOPPING; set => SetProperty(ref _TOPPING, value); }

        private string? _DEPTCODE;
        public string? DEPTCODE { get => _DEPTCODE; set => SetProperty(ref _DEPTCODE, value); }

        private string? _SEASONCODE;
        public string? SEASONCODE { get => _SEASONCODE; set => SetProperty(ref _SEASONCODE, value); }

        private double? _DINETAKEVAL;
        public double? DINETAKEVAL { get => _DINETAKEVAL; set => SetProperty(ref _DINETAKEVAL, value); }

        private bool _ISVoidItem;
        public bool ISVoidItem { get => _ISVoidItem; set => SetProperty(ref _ISVoidItem, value); }

    }

}
