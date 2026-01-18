using Mopups.Services;
using RestaurantOrder.ApiService;
using RestaurantOrder.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            }
            catch (Exception ex)
            {

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

                        if (string.IsNullOrEmpty(QuickQtyEntryValue) || Convert.ToDouble(QuickQtyEntryValue) > BillAmount)
                        {
                            string modeText = IsVisibleCashMode ? "cash total" : "card total";
                            App.cancellationTokenSource = new();
                            await Toast.Make($"Please enter valid {modeText}", ToastDuration.Short, 10).Show(App.cancellationTokenSource.Token);
                            break;
                        }

                        if (IsVisibleCashMode)
                        {
                            CashTotal = Convert.ToDouble(QuickQtyEntryValue);
                        }
                        else if (IsVisibleCardMode)
                        {
                            CardTotal = Convert.ToDouble(QuickQtyEntryValue);
                        }
                        break;
                    case "SETCARDNO":
                        if(QuickQtyEntryValue.Length > 11 && QuickQtyEntryValue.Length < 13)
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
                        ReceivedAmount = Convert.ToDouble(QuickQtyEntryValue);
                        CalculateChangeAmount();
                            break;
                    case "FAST0":
                       // ReceivedAmount = Convert.ToDouble(QuickQtyEntryValue);
                        break;
                    case "FAST20":
                        ReceivedAmount = 20;
                        CalculateChangeAmount();
                        break;
                    case "FAST10":
                        ReceivedAmount = 10;
                        CalculateChangeAmount();
                        break;
                    case "FAST5":
                        ReceivedAmount = 5;
                        CalculateChangeAmount();
                        break;
                    case "FAST1":
                        ReceivedAmount = 1;
                        CalculateChangeAmount();
                        break;
                    case "SETTLE":
                        // if (CashTotal + CardTotal == BillAmount)
                        if (ReceivedAmount >= BillAmount)
                        {
                            if (CashTotal + CardTotal != BillAmount)
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
                if (ReceivedAmount > BillAmount)
                {
                    ChangeAmount = ReceivedAmount - BillAmount;
                }
                else
                {
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
                //Isbussy = true;
                //IsVisibleIndecator = true;
                UsersList = await ApiClient.GetAsync<ObservableCollection<CardTypes>>("User/GetCrCards");
                if (UsersList == null)
                {
                    //Isbussy = false;
                    //IsVisibleIndecator = false;
                    // RecivedMessageOnLoginpage(new string[] { $"DISPLAYALERT", $"Network Issue - Check your Setting." });
                }
            }
            catch (Exception ex)
            {
                //RecivedMessageOnLoginpage(new string[] { $"DISPLAYALERT", $"Something went wrong - Check your Setting." });
                //Isbussy = false;
                //IsVisibleIndecator = false;
            }
            finally
            {
                //Isbussy = false;
                //IsVisibleIndecator = false;
            }
        }

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

        private double _CashTotal;
        public double CashTotal { get => _CashTotal; set { _CashTotal = value; OnPropertyChanged(); } }

        private double _CardTotal;
        public double CardTotal { get => _CardTotal; set { _CardTotal = value; OnPropertyChanged(); } }

        private double _CurrencyTotal;
        public double CurrencyTotal { get => _CurrencyTotal; set { _CurrencyTotal = value; OnPropertyChanged(); } }

        private double _BillAmount;
        public double BillAmount { get => _BillAmount; set { _BillAmount = value; OnPropertyChanged(); } }

        private double _ReceivedAmount;
        public double ReceivedAmount { get => _ReceivedAmount; set { _ReceivedAmount = value; OnPropertyChanged(); } }

        private double _ChangeAmount;
        public double ChangeAmount { get => _ChangeAmount; set { _ChangeAmount = value; OnPropertyChanged(); } }

        private double _DueBalance;
        public double DueBalance { get => _DueBalance; set { _DueBalance = value; OnPropertyChanged(); } }


        //private bool _IsCardNOFocus = false;
        //public bool IsCardNOFocus { get => _IsCardNOFocus; set { _IsCardNOFocus = value; OnPropertyChanged(); } }



        private bool _IsVisibleCashMode = true;
        public bool IsVisibleCashMode { get => _IsVisibleCashMode; set { _IsVisibleCashMode = value; OnPropertyChanged(); } }

        private bool _IsVisibleCurrencyMode = false;
        public bool IsVisibleCurrencyMode { get => _IsVisibleCurrencyMode; set { _IsVisibleCurrencyMode = value; OnPropertyChanged(); } }

        private bool _IsVisibleCardMode = false;
        public bool IsVisibleCardMode { get => _IsVisibleCardMode; set { _IsVisibleCardMode = value; OnPropertyChanged(); } }

    }


}
