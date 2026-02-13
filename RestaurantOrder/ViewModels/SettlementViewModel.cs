using RestaurantOrder.ApiService;
using RestaurantOrder.Models;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace RestaurantOrder.ViewModels
{
    public class SettlementViewModel : NotifyPropertyBaseViewModel
    {

        public SettlementViewModel()
        {
            try
            {
                //_ = LoadCardsType();
                QuickBtnCLickCommand = new Command<string>(QuickBtnClicked);
                QuickNumKeysClickCommand = new Command<string>(QuickKeysClicked);
                CurrencySelectedCommand = new Command(OnCurrencySelected);
            }
            catch (Exception ex)
            {

            }

        }
        private void OnCurrencySelected()
        {
            try
            {
                if (CurrencySelectedItem == null)
                    return;
                //AllItemList.Clear();
                //AllItemList = new ObservableCollection<item>(_allItemsBackup.Where(x => x.CATCODE == CategorySelectedItem.CODE).ToList());
                //IsComboListVisible = false;
                //IsCategoryListVisible = false;
                //IsItemListVisible = true;
               // CurrencySelectedItem = null;
            }
            catch (Exception ex)
            {
                CurrencySelectedItem = null;
            }
        }
        private async void QuickBtnClicked(string parameter)
        {
            try
            {


                if (IsQuickBtnCLick)
                    return;
                IsQuickBtnCLick = true;
                switch (parameter)
                {



                    default:
                        // Handle default
                        break;
                }

                //if (parameter != "OPENOPTION")
                //{
                //    try
                //    {
                //        await MainThread.InvokeOnMainThreadAsync(async () =>
                //        {
                //            await MopupService.Instance.PopAsync(true);
                //        });
                //        //if (MopupService.Instance.PopupStack.Count > 0)
                //        //    MopupService.Instance.PopAsync(animate: true);
                //    }
                //    catch
                //    {
                //        // swallow exception
                //    }
                //}

                // Console.WriteLine($"Button clicked: {parameter}");
            }
            catch (Exception ex)
            {

            }
            finally
            {
                IsQuickBtnCLick = false;
            }
        }

        private async void QuickKeysClicked(string parameter)
        {
            try
            {
                if (IsQuickNumKeysClick)
                    return;
                IsQuickNumKeysClick = true;
                switch (parameter)
                {

                    case "1":
                        QuickQtyEntryValue += "1";
                        break;
                    case "2":
                        QuickQtyEntryValue += "2";
                        break;
                    case "3":
                        QuickQtyEntryValue += "3";
                        break;
                    case "4":
                        QuickQtyEntryValue += "4";
                        break;
                    case "5":
                        QuickQtyEntryValue += "5";
                        break;
                    case "6":
                        QuickQtyEntryValue += "6";
                        break;
                    case "7":
                        QuickQtyEntryValue += "7";
                        break;
                    case "8":
                        QuickQtyEntryValue += "8";
                        break;
                    case "9":
                        QuickQtyEntryValue += "9";
                        break;
                    case "0":
                        QuickQtyEntryValue += "0";
                        break;
                    case "00":
                        QuickQtyEntryValue += "00";
                        break;
                    case ".":
                        QuickQtyEntryValue += ".";
                        break;
                    case "C":
                        QuickQtyEntryValue = string.Empty;
                        break;
                    case "C-":
                        if (!string.IsNullOrEmpty(QuickQtyEntryValue))
                        {
                            QuickQtyEntryValue = QuickQtyEntryValue.Remove(QuickQtyEntryValue.Length - 1);
                        }
                        break;

                    case "SET":
                        if (string.IsNullOrEmpty(QuickQtyEntryValue) || Convert.ToDecimal(QuickQtyEntryValue) > BillAmount && !IsVisibleCurrencyMode)
                        {
                            string modeText = IsVisibleCashMode ? "cash total" :  "card total";
                            App.cancellationTokenSource = new();
                            await Toast.Make($"Please enter valid {modeText}", ToastDuration.Short, 10).Show(App.cancellationTokenSource.Token);
                            break;
                        }

                        if (IsVisibleCashMode)
                        {
                            CashTotal = Convert.ToDecimal(QuickQtyEntryValue);
                        }
                        else if (IsVisibleCardMode)
                        {
                            CardTotal = Convert.ToDecimal(QuickQtyEntryValue);
                        }
                        else if (IsVisibleCurrencyMode)
                        {
                            if (CurrencySelectedItem != null)
                            {

                                decimal CurrTotal = (decimal)(CurrencySelectedItem.ERATE_RATE * Convert.ToDouble(QuickQtyEntryValue));
                                if (CurrTotal > BillAmount)
                                {
                                    App.cancellationTokenSource = new();
                                    await Toast.Make($"Please enter valid Currency Total", ToastDuration.Short, 10).Show(App.cancellationTokenSource.Token);
                                }
                                else
                                {
                                    CurrencySelectedItem.ERATE_PaidAmount = (float)Convert.ToDouble(QuickQtyEntryValue);
                                    CurrencyTotal = CurrTotal;
                                }
                            }
                            else
                            {
                                App.cancellationTokenSource = new();
                                await Toast.Make($"Please Select Valid Currency", ToastDuration.Short, 10).Show(App.cancellationTokenSource.Token);
                            }
                        }
                        break;
                    case "SETCARDNO":
                        if(QuickQtyEntryValue.Length > 3 && QuickQtyEntryValue.Length < 5)
                        {
                            if (QuickQtyEntryValue.Contains("."))
                            {
                                App.cancellationTokenSource = new();
                                await Toast.Make("Please Enter Valid Card Number.", ToastDuration.Short, 10).Show(App.cancellationTokenSource.Token);
                                break;
                            }
                           CardNumber = QuickQtyEntryValue;
                        }
                        else
                        {
                            App.cancellationTokenSource = new();
                            await Toast.Make("Please Enter Valid Card Number.", ToastDuration.Short, 10).Show(App.cancellationTokenSource.Token);
                        }
                        break;

                    case "RECIVED":
                        TenderedAmount = Convert.ToDecimal(QuickQtyEntryValue);
                        CalculateChangeAmount();
                            break;
                    case "FAST0":
                       // ReceivedAmount = Convert.ToDouble(QuickQtyEntryValue);
                        break;

                    case "FASTCASH1":
                        TenderedAmount = FastCash.Cash1;
                        CalculateChangeAmount();                        
                        break;
                    case "FASTCASH2":
                        TenderedAmount = FastCash.Cash2;
                        CalculateChangeAmount();
                        break;
                    case "FASTCASH3":
                        TenderedAmount = FastCash.Cash3;
                        CalculateChangeAmount();
                        break;
                    case "FASTCASH4":
                        TenderedAmount = FastCash.Cash4;
                        CalculateChangeAmount();
                        break;
                    case "FASTCASH5":
                        TenderedAmount = FastCash.Cash5;
                        CalculateChangeAmount();
                        break;
                    case "FASTCASH6":
                        TenderedAmount = FastCash.Cash6;
                        CalculateChangeAmount();
                        break;
                    case "FASTCASH7":
                        TenderedAmount = FastCash.Cash7;
                        CalculateChangeAmount();
                        break;
                    case "FASTCASH8":
                        TenderedAmount = FastCash.Cash8;
                        CalculateChangeAmount();
                        break;
                    case "SETTLE":
                        // if (CashTotal + CardTotal == BillAmount)
                        //if (TotalAmount >= BillAmount)
                        if (DueBalance == 0m)
                        {
                            //if (CashTotal + CardTotal + CurrencyTotal != BillAmount)
                            var roundedTotal = Math.Round(CashTotal + CardTotal + CurrencyTotal,2,MidpointRounding.AwayFromZero);

                            var roundedBill = Math.Round(BillAmount,2,MidpointRounding.AwayFromZero);

                            if (roundedTotal != roundedBill)
                            {
                                App.cancellationTokenSource = new();
                                await Toast.Make("Settlement total must match bill amount", ToastDuration.Short, 10).Show(App.cancellationTokenSource.Token);
                                break;
                            }

                            if (CardTotal > 0)
                            {
                                if(SelectedUser == null)
                                {
                                    App.cancellationTokenSource = new();
                                    await Toast.Make("Please select valid card type.", ToastDuration.Short, 10).Show(App.cancellationTokenSource.Token);
                                    break;
                                }
                                else if(string.IsNullOrEmpty(CardNumber))
                                {
                                    App.cancellationTokenSource = new();
                                    await Toast.Make("Enter valid card no.", ToastDuration.Short, 10).Show(App.cancellationTokenSource.Token);
                                    break;
                                }

                            }
                            var ObjSettlementRequest = new SettlementRequest
                            {
                                BILLNO = TXNNO,
                                BillAmount = BillAmount,
                                CardTotal = CardTotal,
                                CashTotal = CashTotal,
                                CurrencyTotal = CurrencyTotal,
                                CashEntry = new CASHDRAW
                                {
                                    BRANCHCODE = "HQ",
                                    TXNDATE = DateTime.Now,
                                    LASTUSER = App.ObjMainUserViewModel.UserEmail,
                                    LASTTIME = DateTime.Now.ToString("hh:mm:ss tt"),
                                    BILLNO = TXNNO,
                                    SHIFT = string.Empty,
                                    MODE = "C",
                                    TOPUPAMT = 0,
                                    CASHAMT = CashTotal,
                                    UPDATED = "Y"
                                },
                                InvCardEntry = new INVCARD
                                {
                                    TXNNO = TXNNO,
                                    CRNO = CardNumber,
                                    TXNDT = DateTime.Now,
                                    AMOUNT = CardTotal,
                                    CRCODE = SelectedUser?.CODE,
                                    LASTUSER = App.ObjMainUserViewModel.UserEmail,
                                    LASTDATE = DateTime.Now,
                                    LASTTIME = DateTime.Now.ToString("hh:mm:ss tt"),
                                    BRANCHCODE = "HQ",
                                    MODE = CashTotal > 0 && CardTotal > 0 ? "T" : CardTotal > 0 ? "N" : "N",
                                    UPDATED = "Y"
                                },
                                CurrencyEntry = new INVCURRENCY
                                {
                                    BranchCode = "HQ",
                                    CurCode = CurrencySelectedItem?.ERATE_CURR,
                                    TxnNo = TXNNO,
                                    TxnDt = DateTime.Now,
                                    Amount = Math.Round(CurrencySelectedItem?.ERATE_PaidAmount ?? 0d,2,MidpointRounding.AwayFromZero),
                                    Status = "C",
                                    LastUser = App.ObjMainUserViewModel.UserEmail,
                                    LastDate =  DateTime.Now,
                                    LastTime = DateTime.Now.ToString("hh:mm:ss tt"),
                                    Updated ="Y",
                                    ExchRate = Math.Round(CurrencySelectedItem?.ERATE_RATE ?? 0d, 2,  MidpointRounding.AwayFromZero)
                                },
                                LASTUSER = App.ObjMainUserViewModel.UserEmail,
                                LASTDATE = DateTime.Now,
                                LASTTIME = DateTime.Now.ToString("hh:mm:ss tt"),

                            };

                            var response = await ApiClient.PostAsync<SettlementRequest, string>("Settlement/SettledCurrentBill", ObjSettlementRequest, 1);
                            if (response != null)
                            {

                                if (response.Contains("settled successfully"))
                                {
                                    App.cancellationTokenSource = new();
                                    await Toast.Make("Bill Settled Successfully.", ToastDuration.Short, 10).Show(App.cancellationTokenSource.Token);

                                    ClosePopup?.Invoke(true);
                                }
                                else
                                {

                                    App.cancellationTokenSource = new();
                                    await Toast.Make("Something went wrong.", ToastDuration.Short, 10).Show(App.cancellationTokenSource.Token);
                                }
                            }
                            else
                            {
                                App.cancellationTokenSource = new();
                                await Toast.Make("Something went wrong.", ToastDuration.Short, 10).Show(App.cancellationTokenSource.Token);

                            }
                        }
                        else
                        {
                            App.cancellationTokenSource = new();
                            await Toast.Make("Please enter valid amount to settled bill.", ToastDuration.Short, 10).Show(App.cancellationTokenSource.Token);

                        }
                        break;
                    default:
                        // Handle default
                        break;

                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                IsQuickNumKeysClick = false;
            }
        }


        private void CalculateChangeAmount()
        {
            try
            {
                if (TenderedAmount > BillAmount)
                {
                    ChangeAmount = TenderedAmount - BillAmount;
                    CashTotal = BillAmount;
                }
                else
                {
                    CashTotal = TenderedAmount;
                    ChangeAmount = 0;
                }
            }
            catch (Exception)
            {


            }
        }
        public async Task LoadCardsType()
        {
            try
            {
                UsersList = await ApiClient.GetAsync<ObservableCollection<CardTypes>>("User/GetCrCards");
                FastCash = await ApiClient.GetAsync<SysParam>("Settlement/GetSysParam");
                CurrencyListWithRate = await ApiClient.GetAsync<ObservableCollection<EXRATE>>("Settlement/GetCurrencytypes");
            }
            catch (Exception ex)
            {

            }
            finally
            {
            }
        }


        private SysParam _FastCashModel = new ();
        public SysParam FastCash { get => _FastCashModel; set { SetProperty(ref _FastCashModel, value); ; OnPropertyChanged(); } }

        public Action<bool>? ClosePopup { get; set; }

        public ICommand QuickBtnCLickCommand { get; }
        private bool _IsQuickBtnCLick = false;
        public bool IsQuickBtnCLick { get => _IsQuickBtnCLick; set { _IsQuickBtnCLick = value; OnPropertyChanged(); } }

        public ICommand QuickNumKeysClickCommand { get; }

        private bool _IsQuickNumKeysClick = false;
        public bool IsQuickNumKeysClick { get => _IsQuickNumKeysClick; set { _IsQuickNumKeysClick = value; OnPropertyChanged(); } }

        private string _QuickQtyEntryValue = string.Empty;
        public string QuickQtyEntryValue { get => _QuickQtyEntryValue; set { _QuickQtyEntryValue = value; OnPropertyChanged(); } }


        private ObservableCollection<CardTypes> _UsersList = new();
        public ObservableCollection<CardTypes> UsersList { get => _UsersList; set { SetProperty(ref _UsersList, value); } }

        private CardTypes _SelectedUser = new();
        public CardTypes SelectedUser { get => _SelectedUser; set { SetProperty(ref _SelectedUser, value); } }


        private string _CardNumber;
        public string CardNumber { get => _CardNumber; set { _CardNumber = value; OnPropertyChanged(); } }

        private double _TXNNO;
        public double TXNNO { get => _TXNNO; set { _TXNNO = value; OnPropertyChanged(); } }

        private decimal _CashTotal;
        public decimal CashTotal { get => _CashTotal; set { _CashTotal = value; OnPropertyChanged(); UpdatesUIOnpropertyChanhes(); } }

        private decimal _CardTotal;
        public decimal CardTotal { get => _CardTotal; set { _CardTotal = value; OnPropertyChanged(); UpdatesUIOnpropertyChanhes(); } }

        private decimal _CurrencyTotal;
        public decimal CurrencyTotal { get => _CurrencyTotal; set { _CurrencyTotal = value; OnPropertyChanged(); UpdatesUIOnpropertyChanhes(); } }



        private decimal _BillAmount;
        public decimal BillAmount { get => _BillAmount; set { _BillAmount = value; OnPropertyChanged(); } }

        private decimal _TenderedAmount;
        public decimal TenderedAmount { get => _TenderedAmount; set { _TenderedAmount = value; OnPropertyChanged(); } }

        private decimal _ChangeAmount;
        public decimal ChangeAmount { get => _ChangeAmount; set { _ChangeAmount = value; OnPropertyChanged(); } }

        //  private double _DueBalance;
        //        public double DueBalance { get => Math.Max(0d, BillAmount - TotalAmount); }

        public decimal DueBalance
        {
            get
            {
                decimal due = BillAmount - TotalAmount;

                return Math.Max(
                    0m,
                    Math.Round(due, 2, MidpointRounding.AwayFromZero)
                );
            }
        }

        //private double _TotalAmount;
        //public double TotalAmount { get => _TotalAmount; set { _TotalAmount = value; OnPropertyChanged(); } }

        public decimal TotalAmount {   get => CashTotal + CardTotal + CurrencyTotal;  }

        private void UpdatesUIOnpropertyChanhes()
        {
            try
            {
                OnPropertyChanged(nameof(TotalAmount)); 
                OnPropertyChanged(nameof(DueBalance));
                OnPropertyChanged(nameof(IsBillSettled));
             
                OnPropertyChanged(nameof(IsEnableCashMode));
                OnPropertyChanged(nameof(IsEnableCurrencyMode));
                OnPropertyChanged(nameof(IsEnableCardMode));
            }
            catch(Exception ex)
            {

            }

        }


        private bool _IsVisibleCashMode = true;
        public bool IsVisibleCashMode { get => _IsVisibleCashMode; set { _IsVisibleCashMode = value; OnPropertyChanged(); } }

        private bool _IsVisibleCurrencyMode = false;
        public bool IsVisibleCurrencyMode { get => _IsVisibleCurrencyMode; set { _IsVisibleCurrencyMode = value; OnPropertyChanged(); } }

        private bool _IsVisibleCardMode = false;
        public bool IsVisibleCardMode { get => _IsVisibleCardMode; set { _IsVisibleCardMode = value; OnPropertyChanged(); } }



        //RemainingAmount > Tolerance &&
        //CardTotal <= Tolerance;
        //public bool IsBillSettled =>  Math.Abs(TotalAmount - BillAmount) < 0.01;
        public bool IsBillSettled => Math.Abs(TotalAmount - BillAmount) < 0.01m;


        public bool IsEnableCashMode => CashTotal > 0 || !IsBillSettled;
        public bool IsEnableCurrencyMode => CurrencyTotal > 0 || !IsBillSettled;    
        public bool IsEnableCardMode => CardTotal > 0 || !IsBillSettled;

        //private bool _IsEnableCashMode = true;
        //public bool IsEnableCashMode { get => _IsEnableCashMode; set { _IsEnableCashMode = value; OnPropertyChanged(); } }


        //   private bool _IsEnableCurrencyMode = true;


        //private bool _IsEnableCurrencyMode = true;
        //public bool IsEnableCurrencyMode { get => _IsEnableCurrencyMode; set { _IsEnableCurrencyMode = value; OnPropertyChanged(); } }



        //private bool _IsEnableCardMode = true;
        //public bool IsEnableCardMode { get => _IsEnableCardMode; set { _IsEnableCardMode = value; OnPropertyChanged(); } }


        private ObservableCollection<EXRATE> _CurrencyListWithRate = new();
        public ObservableCollection<EXRATE> CurrencyListWithRate { get => _CurrencyListWithRate; set { SetProperty(ref _CurrencyListWithRate, value); } }

        public ICommand CurrencySelectedCommand { get; set; }

        private EXRATE? _CurrencySelectedItem;
        public EXRATE? CurrencySelectedItem { get => _CurrencySelectedItem; set { SetProperty(ref _CurrencySelectedItem, value); } }


    }


}
