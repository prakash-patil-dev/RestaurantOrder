using Mopups.Services;
using RestaurantOrder.ApiService;
using RestaurantOrder.Models;
using RestaurantOrder.Views;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;

namespace RestaurantOrder.ViewModels
{
    public class NewOrderViewModel : NotifyPropertyBaseViewModel
    {
        public NewOrderViewModel()
        {
            try
            {       
                _ = LoadAllOpenBillsList();
                _ = LoadAllCATEGORYList();
                _ = LoadAllItemList();

                QuickBtnCLickCommand = new Command<string>(QuickBtnClicked);
                QuickNumKeysClickCommand = new Command<string>(QuickKeysClicked);
                ItemSelectedCommand = new Command(OnItemSelected);
                CategorySelectedCommand = new Command(OnCategorySelected);
                //AllOpenBillsSelectionChangedCommand = new Command<INVHEAD>(OnAllOpenBillSelectionChanged);
                // AllOpenBillsListRefreshCommand = new Command(async () => await LoadAllOpenBillsList());

            }
            catch (Exception ex)
            {
            }
        }
        // Replace all instances of "Isbussy" with "IsBusy" for correct spelling

        private CustomerPopup ObjCustomerPopup; 
        private MenuPopup ObjMenuPopup;
        private ConformPopup ObjConformPopup = new ConformPopup();
        public async Task LoadAllOpenBillsList()
        {
            try
            {
                IsBusy = true;
                IsVisibleIndicator = true;
                //var tabno = TableNo;
                //var gust = TotalGuest;
                // Run the API call in a background thread
                AllOpenBillsList = await ApiClient.GetAsync<ObservableCollection<INVHEAD>>("INVHEAD/GetOpenBills") ?? new ObservableCollection<INVHEAD>();
                //ObjCustomerPopup = new CustomerPopup();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"LoadAllOpenBillsList error: {ex}");
            }
            finally
            {
                IsBusy = false;
                IsVisibleIndicator = false;
            }
        }


        public async Task GetBillDetalsonTXNO(double txnNo)
        {
            try
            {
                IsBusy = true;
                IsVisibleIndicator = true;
                INVHEADDETAILS.CurrentOpenBillDetails = await ApiClient.GetAsync<ObservableCollection<INVLINE>>($"INVHEAD/GetInvLineFromTXNNO/{txnNo}") ?? new ObservableCollection<INVLINE>();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"LoadAllOpenBillsList error: {ex}");
            }
            finally
            {
                IsBusy = false;
                IsVisibleIndicator = false;
            }
        }

        public async Task LoadAllCATEGORYList()
        {
            try
            {
                IsBusy = true;
                IsVisibleIndicator = true;

                AllCATEGORYList = await ApiClient.GetAsync<ObservableCollection<CATEGORY>>("CATEGORY/GetCategorys") ?? new ObservableCollection<CATEGORY>();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"LoadAllOpenBillsList error: {ex}");
                // Optionally show alert message here
            }
            finally
            {
                IsBusy = false;
                IsVisibleIndicator = false;
            }
        }

        public async Task LoadAllItemList()
        {
            try
            {
                IsBusy = true;
                IsVisibleIndicator = true;

                _allItemsBackup = await ApiClient.GetAsync<ObservableCollection<item>>("Item/GetItems") ?? new ObservableCollection<item>();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"LoadAllOpenBillsList error: {ex}");
                // Optionally show alert message here
            }
            finally
            {
                IsBusy = false;
                IsVisibleIndicator = false;
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
                    case "CAT HELP":
                        IsItemListVisible = false;
                        IsCategoryListVisible = !IsCategoryListVisible;
                        break;
                    case "ITEM HELP":
                        if (IsItemListVisible)
                        {
                            AllItemList.Clear();
                            AllItemList = new ObservableCollection<item>(_allItemsBackup.ToList());
                        }
                        IsCategoryListVisible = false;
                        IsItemListVisible = !IsItemListVisible;
                        break;
                    case "BILL TYPE":
                        // Handle BILL TYPE
                        break;
                    case "ARROWCOMMAND":
                        IsMoreKeysVisible = !IsMoreKeysVisible;
                        break;
                    case "OPENOPTION":
                        if (ObjMenuPopup == null)
                        {
                            ObjMenuPopup = new MenuPopup();
                           // ObjMenuPopup.PopupCommand = new Command<string[]>(ExecutePopupCommand); 
                            ObjMenuPopup.QuickBtnCLickCommand = new Command<string>(QuickBtnClicked);
                            //ObjMenuPopup.QuickBtnCLickCommand = new Command<string>(async (param) => await QuickBtnClicked(param));
                            //(async (param) => await QuickBtnClicked(param));

                            // QuickBtnCLickCommand = new Command<string>(QuickBtnClicked);
                        }
                        ObjMenuPopup.IsPageOpen = true;
                        MopupService.Instance.PushAsync(ObjMenuPopup, animate: true);
                        break;
                    case "ESC":
                        // clear bindings / state
                        //Application.Current.MainPage.BindingContext = null;
                        //Application.Current.MainPage.Navigation.PopModalAsync(animated: true);
                        await ClosePageAsync();
                        // dispose VM if needed
                        // NewOrderPageVM = null; // not needed inside itself

                        break;
                    case "CLOSEMENU":
                        try
                        {
                            await MainThread.InvokeOnMainThreadAsync(async () =>
                            {
                                await MopupService.Instance.PopAsync(true);
                            });
                            //if (MopupService.Instance.PopupStack.Count > 0)
                            //    MopupService.Instance.PopAsync(animate: true);
                        }
                        catch
                        {

                        }
                        break;
                    default:
                        // Handle default
                        break;
                }

                if (parameter != "OPENOPTION")
                {
                    try
                    {
                        await MainThread.InvokeOnMainThreadAsync(async () =>
                        {
                            await MopupService.Instance.PopAsync(true);
                        });
                        //if (MopupService.Instance.PopupStack.Count > 0)
                        //    MopupService.Instance.PopAsync(animate: true);
                    }
                    catch
                    {
                        // swallow exception
                    }
                }

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

        //private CancellationTokenSource cancellationTokenSource;// = new CancellationTokenSource();
        private void QuickKeysClicked(string parameter)
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
                    case "+":
                        if (CurrentSelectedBillItem == null)
                            break;
                        if (CurrentSelectedBillItem.ISVoidItem)
                        {
                            //CurrentSelectedBillItem.QUANTITY -= 1;

                            if (CurrentSelectedBillItem.QUANTITY > 0)
                            {
                                CurrentSelectedBillItem.QUANTITY += 1;
                            }
                            else if (CurrentSelectedBillItem.QUANTITY < 0)
                            {
                                CurrentSelectedBillItem.QUANTITY -= 1;
                                if (CurrentSelectedBillItem.QUANTITY >= 0)
                                    CurrentSelectedBillItem.QUANTITY = -1;
                            }
                        }
                        else
                        {
                            CurrentSelectedBillItem.QUANTITY += 1;
                        }

                        CurrentSelectedBillItem.DISCOUNT = (CurrentSelectedBillItem.UNITRATE * CurrentSelectedBillItem.QUANTITY * CurrentSelectedBillItem.DISCPERC) / 100.0;
                        CurrentSelectedBillItem.AMOUNT = (CurrentSelectedBillItem.UNITRATE * CurrentSelectedBillItem.QUANTITY) - CurrentSelectedBillItem.DISCOUNT;
                        CalculateTotalAmout();
                        break;
                    case "-":
                        if (CurrentSelectedBillItem == null)
                            break;
                        if (CurrentSelectedBillItem.ISVoidItem)
                        {
                            //CurrentSelectedBillItem.QUANTITY += 1;
                            //if (CurrentSelectedBillItem.QUANTITY >= 0)
                            //    CurrentSelectedBillItem.QUANTITY = -1;

                            if (CurrentSelectedBillItem.QUANTITY > 0)
                            {
                                CurrentSelectedBillItem.QUANTITY -= 1;
                                if (CurrentSelectedBillItem.QUANTITY <= 0)
                                    CurrentSelectedBillItem.QUANTITY = 1;
                            }
                            else if (CurrentSelectedBillItem.QUANTITY < 0)
                            {
                                CurrentSelectedBillItem.QUANTITY += 1;
                                if (CurrentSelectedBillItem.QUANTITY >= 0)
                                    CurrentSelectedBillItem.QUANTITY = -1;
                            }
                        }
                        else
                        {
                            CurrentSelectedBillItem.QUANTITY -= 1;
                            if (CurrentSelectedBillItem.QUANTITY <= 0)
                                CurrentSelectedBillItem.QUANTITY = 1;
                        }
                        CurrentSelectedBillItem.DISCOUNT = (CurrentSelectedBillItem.UNITRATE * CurrentSelectedBillItem.QUANTITY * CurrentSelectedBillItem.DISCPERC) / 100.0;
                        CurrentSelectedBillItem.AMOUNT = (CurrentSelectedBillItem.UNITRATE * CurrentSelectedBillItem.QUANTITY) - CurrentSelectedBillItem.DISCOUNT;
                        CalculateTotalAmout();
                        break;
                    case "*":
                        if (CurrentSelectedBillItem == null)
                            break;

                        double multiplier = Convert.ToDouble(QuickQtyEntryValue);

                        if (multiplier == 0)
                            return; // Avoid zeroing out quantity unintentionally
                        //double newQty = Math.Abs(Convert.ToDouble(CurrentSelectedBillItem.QUANTITY)) * Math.Abs(multiplier);
                        double newQty = 1 * Math.Abs(multiplier);
                        if (CurrentSelectedBillItem.ISVoidItem)
                        {
                            // Make sure quantity is negative after multiplication

                            // CurrentSelectedBillItem.QUANTITY = -newQty;

                            if (CurrentSelectedBillItem.QUANTITY > 0)
                            {
                                CurrentSelectedBillItem.QUANTITY = newQty;
                            }
                            else if (CurrentSelectedBillItem.QUANTITY < 0)
                            {
                                CurrentSelectedBillItem.QUANTITY = -newQty;
                            }
                        }
                        else
                        {
                            // Make sure quantity is positive after multiplication
                            CurrentSelectedBillItem.QUANTITY = newQty;
                        }
                        CurrentSelectedBillItem.DISCOUNT = (CurrentSelectedBillItem.UNITRATE * CurrentSelectedBillItem.QUANTITY * CurrentSelectedBillItem.DISCPERC) / 100.0;
                        CurrentSelectedBillItem.AMOUNT = (CurrentSelectedBillItem.UNITRATE * CurrentSelectedBillItem.QUANTITY) - CurrentSelectedBillItem.DISCOUNT;
                        CalculateTotalAmout();
                        QuickQtyEntryValue = string.Empty;
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
                    case "CLR":
                        if (CurrentSelectedBillItem != null)
                        {
                            if (ObjConformPopup != null)
                            {
                                ObjConformPopup = new ConformPopup();
                                ObjConformPopup.PageType = "REMOVEITEM";
                                ObjConformPopup.PageMessage = "Do you want to Remove item from bill?";
                                ObjConformPopup.PopupCommand = new Command<string[]>(ExecutePopupCommand);
                            }

                            ObjConformPopup.IsPageOpen = true;
                            MopupService.Instance.PushAsync(ObjConformPopup, animate: true);
                            QuickQtyEntryValue = string.Empty;

                        }
                        else
                        {
                            App.cancellationTokenSource = new();
                            Toast.Make("Please Select Valid Item to Remove.", ToastDuration.Short, 10).Show(App.cancellationTokenSource.Token);
                        }
                          
                        //QuickQtyEntryValue = string.Empty;
                        break;
                    case "VOID":
                        if (INVHEADDETAILS == null)
                            break;
                        INVHEADDETAILS.ISVoidMode = !INVHEADDETAILS.ISVoidMode;
                        //CurrentSelectedBillItem.ISVoidItem = !CurrentSelectedBillItem.ISVoidItem;
                        //double VoidQty = Math.Abs(Convert.ToDouble(CurrentSelectedBillItem.QUANTITY));
                        //if (!CurrentSelectedBillItem.ISVoidItem)
                        //{
                        //    //CurrentSelectedBillItem.QUANTITY = -VoidQty;

                        //    INVHEADDETAILS.CurrentOpenBillDetails.Add(new INVLINE
                        //    {
                        //        BRANCHCODE = "HQ",
                        //        TXNNO = INVHEADDETAILS.CurrentOpenBill.TXNNO,
                        //        TXNDT = DateTime.Now, 
                        //        VIPNO = INVHEADDETAILS.CurrentOpenBill.VIPNO,
                        //        SHIFT = string.Empty,
                        //        USER = App.ObjMainUserViewModel.UserEmail,
                        //        STAFF = INVHEADDETAILS.CurrentOpenBill.STAFF,
                        //        LINE = 0,
                        //        ITEMCODE = CurrentSelectedBillItem.ITEMCODE,
                        //        ITEMNAME1 = CurrentSelectedBillItem.ITEMNAME1,
                        //        ITEMNAME2 = CurrentSelectedBillItem.ITEMNAME2,
                        //        CATCODE = CurrentSelectedBillItem.CATCODE,
                        //        SUBCATCODE = CurrentSelectedBillItem.SUBCATCODE,
                        //        BRANDCODE = CurrentSelectedBillItem.BRANDCODE,
                        //        UNITCODE = CurrentSelectedBillItem.UNITCODE,
                        //        QUANTITY = -VoidQty,
                        //        UNITRATE = CurrentSelectedBillItem.UNITRATE,
                        //        AMOUNT = CurrentSelectedBillItem.AMOUNT * 1,
                        //        TAXPERC = 0,
                        //        TAXVALUE = 0,
                        //        COSTAMT = CurrentSelectedBillItem.COSTAMT,
                        //        COSTAMTSPA = CurrentSelectedBillItem.COSTAMTSPA,
                        //        SPLDISC = null,
                        //        DISCPERC = 0,
                        //        DISCOUNT = 0,
                        //        LESSAMT = 0,
                        //        PRINTED = null,
                        //        BPRINTED = "N",
                        //        KPRINTED = "Y",
                        //        EATTAKE = "E",
                        //        STATUS = "O",
                        //        UPDATED = "Y",
                        //        STYLECODE = CurrentSelectedBillItem.STYLECODE,
                        //        COLORCODE = CurrentSelectedBillItem.COLORCODE,
                        //        SIZECODE = CurrentSelectedBillItem.SIZECODE,
                        //        PACKAGEID = 0,
                        //        PACKLINE = 0,
                        //        LASTUSER = App.ObjMainUserViewModel.UserEmail,
                        //        LASTDATE = DateTime.Now,
                        //        LASTTIME = DateTime.Now.ToString("HH:mm:ss"),
                        //        KOT = null,
                        //        SCANITEMCODE = CurrentSelectedBillItem.ITEMCODE,
                        //        PRICETYPE = "SP",
                        //        TOPPING = null,
                        //        DEPTCODE = "0",
                        //        SEASONCODE = string.Empty,
                        //        ISVoidItem = true
                        //    });
                        //    CurrentSelectedBillItem = INVHEADDETAILS.CurrentOpenBillDetails?.LastOrDefault();
                        //}
                        //else
                        //{
                        //   // CurrentSelectedBillItem.ISVoidItem = !CurrentSelectedBillItem.ISVoidItem;
                        //   // CurrentSelectedBillItem.QUANTITY = VoidQty;

                        //    if (CurrentSelectedBillItem.QUANTITY > 0)
                        //    {
                        //        CurrentSelectedBillItem.QUANTITY = -VoidQty;
                        //    }
                        //    else if (CurrentSelectedBillItem.QUANTITY < 0)
                        //    {
                        //        CurrentSelectedBillItem.QUANTITY = VoidQty;
                        //    }

                        //}
                        //CurrentSelectedBillItem.DISCOUNT = (CurrentSelectedBillItem.UNITRATE * CurrentSelectedBillItem.QUANTITY * CurrentSelectedBillItem.DISCPERC) / 100.0;
                        //CurrentSelectedBillItem.AMOUNT = (CurrentSelectedBillItem.UNITRATE * CurrentSelectedBillItem.QUANTITY) - CurrentSelectedBillItem.DISCOUNT;
                        //CalculateTotalAmout();
                        QuickQtyEntryValue = string.Empty;
                        break;
                    case "SAVE":
                        //MopupService.Instance.PushAsync(new YesNoPopup(), animate: true);
                        if (ObjConformPopup != null)
                        {
                           // ObjConformPopup = new ConformPopup();
                            ObjConformPopup.PageType = "SAVEBILL";
                            ObjConformPopup.PageMessage = "Do you want to save this bill?";
                            ObjConformPopup.PopupCommand = new Command<string[]>(ExecutePopupCommand);
                        }

                        ObjConformPopup.IsPageOpen = true;
                        MopupService.Instance.PushAsync(ObjConformPopup, animate: true);
                        QuickQtyEntryValue = string.Empty;
                        break;
                    case "CUST":
                        if (ObjCustomerPopup == null)
                        {
                            ObjCustomerPopup = new CustomerPopup();
                            ObjCustomerPopup.SelectedCustomerCommand = new Command<Customer>(ExecuteSelectedCustomer);
                        }

                        ObjCustomerPopup.IsPageOpen = true;
                        MopupService.Instance.PushAsync(ObjCustomerPopup, animate: true);
                       // QuickQtyEntryValue = string.Empty;
                        break;
                    case "EATTAKE":
                        if (CurrentSelectedBillItem == null)
                            break;
                        if (CurrentSelectedBillItem.EATTAKE == "E")
                            CurrentSelectedBillItem.EATTAKE = "T";
                        else
                            CurrentSelectedBillItem.EATTAKE = "E";
                       // QuickQtyEntryValue = string.Empty;
                        break;
                    case "LESSAMT":
                        if (string.IsNullOrEmpty(QuickQtyEntryValue) || Convert.ToDouble(QuickQtyEntryValue) <= 0 )
                        {
                            App.cancellationTokenSource = new();
                            Toast.Make("Please Enter Valid Less Amount.", ToastDuration.Short, 10).Show(App.cancellationTokenSource.Token);
                            break;

                        }
                        INVHEADDETAILS.CurrentOpenBill.lessamount = Math.Abs(Convert.ToDouble(QuickQtyEntryValue)); //INVHEADDETAILS.CurrentOpenBill.lessamount
                        CalculateTotalAmout();
                        QuickQtyEntryValue = string.Empty;
                        break;
                    case "ITEMDISC":
                        if (CurrentSelectedBillItem == null)
                            break;
                        if(string.IsNullOrEmpty(QuickQtyEntryValue) || Convert.ToDouble(QuickQtyEntryValue) <0 || Convert.ToDouble(QuickQtyEntryValue) > 100)
                        {
                            App.cancellationTokenSource = new();
                            Toast.Make("Please Enter Valid Item Discount Amount.", ToastDuration.Short, 10).Show(App.cancellationTokenSource.Token);
                            break;
                        }

                        double percent = Math.Abs(Convert.ToDouble(QuickQtyEntryValue));
                        CurrentSelectedBillItem.DISCPERC = percent;
                        CurrentSelectedBillItem.DISCOUNT = (CurrentSelectedBillItem.UNITRATE * CurrentSelectedBillItem.QUANTITY * percent) / 100.0;
                        CurrentSelectedBillItem.AMOUNT = (CurrentSelectedBillItem.UNITRATE * CurrentSelectedBillItem.QUANTITY )- CurrentSelectedBillItem.DISCOUNT;
                        CalculateTotalAmout();
                        QuickQtyEntryValue = string.Empty;
                        break;
                    case "BILLDISC":
                        if (string.IsNullOrEmpty(QuickQtyEntryValue) || Convert.ToDouble(QuickQtyEntryValue) < 0 || Convert.ToDouble(QuickQtyEntryValue) > 100)
                        {
                            App.cancellationTokenSource = new();
                            Toast.Make("Please Enter Valid Bill Discount.", ToastDuration.Short, 10).Show(App.cancellationTokenSource.Token);
                            break;
                        }
                        INVHEADDETAILS.CurrentOpenBill.DISCPERC = Math.Abs(Convert.ToDouble(QuickQtyEntryValue));
                        CalculateTotalAmout();
                        QuickQtyEntryValue = string.Empty;
                        break;
                    case "W":
                        break;
                    case "SETTLE":
                        break;

                    case "PLU":
                        if (!string.IsNullOrEmpty(QuickQtyEntryValue))
                        {
                            var selectedItem = _allItemsBackup.FirstOrDefault(x => x.ITEMCODE == QuickQtyEntryValue);
                            //INVHEADDETAILS.CurrentOpenBillDetails.LastOrDefault();
                            if (selectedItem != null)
                            {
                               
                                INVHEADDETAILS.CurrentOpenBillDetails.Add(new INVLINE
                                {
                                    BRANCHCODE = "HQ",
                                    TXNNO = INVHEADDETAILS.CurrentOpenBill.TXNNO,
                                    VIPNO = INVHEADDETAILS.CurrentOpenBill.VIPNO,
                                    SHIFT = string.Empty,
                                    USER = App.ObjMainUserViewModel.UserEmail,
                                    STAFF = INVHEADDETAILS.CurrentOpenBill.STAFF,
                                    LINE = 0,
                                    ITEMCODE = selectedItem.ITEMCODE,
                                    ITEMNAME1 = selectedItem.ITEMNAME1,
                                    ITEMNAME2 = selectedItem.ITEMNAME2,
                                    CATCODE = selectedItem.CATCODE,
                                    SUBCATCODE = selectedItem.SUBCATCODE,
                                    BRANDCODE = selectedItem.BRANDCODE,
                                    UNITCODE = selectedItem.UNITCODE,
                                    QUANTITY = INVHEADDETAILS.ISVoidMode ? -1 : 1,
                                    UNITRATE = selectedItem.SALEPRIC,
                                    AMOUNT = selectedItem.SALEPRIC * 1,
                                    TAXPERC = 0,
                                    TAXVALUE = 0,
                                    COSTAMT = selectedItem.COSTPRIC,
                                    COSTAMTSPA = selectedItem.COSTPRIC,
                                    SPLDISC = null,
                                    DISCPERC = 0,
                                    DISCOUNT = 0,
                                    LESSAMT = 0,
                                    PRINTED = null,
                                    BPRINTED = "N",
                                    KPRINTED = "Y",
                                    EATTAKE = "E",
                                    STATUS = "O",
                                    UPDATED = "Y",
                                    STYLECODE = selectedItem.STYLECODE,
                                    COLORCODE = selectedItem.COLORCODE,
                                    SIZECODE = selectedItem.SIZECODE,
                                    PACKAGEID = 0,
                                    PACKLINE = 0,
                                    LASTUSER = App.ObjMainUserViewModel.UserEmail,
                                    LASTDATE = DateTime.Now,
                                    LASTTIME = DateTime.Now.ToString("HH:mm:ss"),
                                    KOT = null,
                                    SCANITEMCODE = selectedItem.ITEMCODE,
                                    PRICETYPE = "SP",
                                    TOPPING = null,
                                    DEPTCODE = "0",
                                    SEASONCODE = string.Empty,
                                    ISVoidItem = INVHEADDETAILS.ISVoidMode ? true : false,
                                });
                                CurrentSelectedBillItem = INVHEADDETAILS.CurrentOpenBillDetails?.LastOrDefault();
                                QuickQtyEntryValue = string.Empty;
                            }
                            else
                            {
                                App.cancellationTokenSource = new();
                                Toast.Make("Please Enter Valid ITEMCODE.", ToastDuration.Short, 10).Show(App.cancellationTokenSource.Token);
                            }

                        }
                        else
                        {
                            App.cancellationTokenSource = new();
                            Toast.Make("Please Enter Valid ITEMCODE.", ToastDuration.Short, 10).Show(App.cancellationTokenSource.Token);
                            break;
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


        //SaveCurrentBill
        private async void ExecutePopupCommand(string[] parameter)
        {
            try
            {
                switch (parameter[0])
                {
                    case "SAVEBILL":
                        if (INVHEADDETAILS?.CurrentOpenBillDetails?.Count > 0)
                        {
                            INVHEADDETAILS.CurrentOpenBill.TXNDT = DateTime.Now;
                            //INVHEADDETAILS.CurrentOpenBill.USER = "ADMIN";
                            //INVHEADDETAILS.CurrentOpenBill.LASTUSER = "ADMIN";
                            INVHEADDETAILS.CurrentOpenBill.LASTDATE = DateTime.Now;
                            INVHEADDETAILS.CurrentOpenBill.LASTTIME = DateTime.Now.ToString("hh:mm:ss tt");
                           // INVHEADDETAILS.CurrentOpenBill.STATUS = "O";
                            INVHEADDETAILS.CurrentOpenBill.EATTAKE = "E";
                            INVHEADDETAILS.CurrentOpenBill.UPDATED = "Y";
                            INVHEADDETAILS.CurrentOpenBill.BRANCHCODE = "HQ"; 
                            INVHEADDETAILS.CurrentOpenBillDetails.ToList().ForEach(item =>
                            {
                                item.BRANCHCODE = "HQ";
                                item.UPDATED = "Y";
                               // item.VIPNO 
                                item.LASTUSER = App.ObjMainUserViewModel.UserEmail;
                                item.LASTDATE = DateTime.Now;
                                item.LASTTIME = DateTime.Now.ToString("hh:mm:ss tt");
                            });

                            var response = await ApiClient.PostAsync<INVHEADDETAILS, string>("INVHEAD/SaveCurrentBill", INVHEADDETAILS, 1);
                            if (response != null)
                            {
                                var StrArray = response.Split(':'); 
                                 string ReturnValue = StrArray[1].Trim(); // "Inserted"

                                if (ReturnValue == "Inserted")
                                {
                                    App.cancellationTokenSource = new();
                                    await Toast.Make("Bill Saved Successfully.", ToastDuration.Short, 10).Show(App.cancellationTokenSource.Token);
                                    await ClosePageAsync();
                                }
                                else if(ReturnValue == "Updated")
                                {
                                    try
                                    {
                                        App.cancellationTokenSource = new();
                                        await Toast.Make("Bill Updated Successfully.", ToastDuration.Short, 10).Show(App.cancellationTokenSource.Token);

                                        await ClosePageAsync();
                                    }
                                    catch (Exception ex)
                                    {

                                    }
                                    
                                }
                                else
                                {
                                    App.cancellationTokenSource = new();
                                    await Toast.Make("Something Went Wrong!.", ToastDuration.Short, 10).Show(App.cancellationTokenSource.Token);
                                }
                                _ = LoadAllOpenBillsList();
                            }
                        }
                        else
                        {
                            App.cancellationTokenSource = new();
                            await Toast.Make("Please Add Items in bill.", ToastDuration.Short, 10).Show(App.cancellationTokenSource.Token);
                        }
                        break;

                    case "REMOVEITEM":
                        if (CurrentSelectedBillItem != null)
                        {
                            if (CurrentSelectedBillItem.LINE == 0)
                            {
                                INVHEADDETAILS.CurrentOpenBillDetails.Remove(CurrentSelectedBillItem);
                                CurrentSelectedBillItem = null;
                                CalculateTotalAmout();
                            }
                            else
                            {
                                var response = await ApiClient.PostAsync<INVLINE, string>("INVHEAD/RemoveItemFromCurrentBill", CurrentSelectedBillItem, 1);
                                 if (response != null)
                                {
                                    if(response == "Item removed successfully!")
                                    {
                                        INVHEADDETAILS.CurrentOpenBillDetails.Remove(CurrentSelectedBillItem);
                                        CurrentSelectedBillItem = null;
                                        CalculateTotalAmout();
                                    }
                                    //else
                                    //{

                                    //}
                                    App.cancellationTokenSource = new();
                                    await Toast.Make(response, ToastDuration.Short, 10).Show(App.cancellationTokenSource.Token);
                                }
                                else
                                {
                                    App.cancellationTokenSource = new();
                                    await Toast.Make("Something Went Wrong!.", ToastDuration.Short, 10).Show(App.cancellationTokenSource.Token);
                                }
                            }
                        }
                            break;
                    case "CANCELBILL":
                        break;
                }


            }
            catch (Exception ex)
            {
            }
        }
        public event Func<Task> RequestClose;

        public async Task ClosePageAsync()
        {
            try
            {
                if (IsCallbillsPage && IsVisibleOrder)
                {
                    IsVisibleOrder = false;
                    INVHEADDETAILS.CurrentOpenBill = null;
                    INVHEADDETAILS.CurrentOpenBillDetails = null;
                    INVHEADDETAILS = null;
                }
                else
                {
                    if (RequestClose != null)
                        await RequestClose.Invoke();
                }
            }
            catch (Exception ex)
            {

            }
        }

        private async void ExecuteSelectedCustomer(Customer parameter)
        {
            try
            {
                if (parameter != null)
                {
                    if(INVHEADDETAILS?.CurrentOpenBill != null)
                    {
                        INVHEADDETAILS.CurrentOpenBill.VIPNO = parameter.Code;
                        INVHEADDETAILS.CurrentOpenBill.CUSTNAME = parameter.FirstName + " " + parameter.LastName;
                        INVHEADDETAILS.CurrentOpenBill.CUSTADD1 = parameter.Address1;
                        INVHEADDETAILS.CurrentOpenBill.CUSTADD2 = parameter.Address2;
                        INVHEADDETAILS.CurrentOpenBill.CUSTADD3 = parameter.Address3;
                        
                        INVHEADDETAILS?.CurrentOpenBillDetails?.ToList()?.ForEach(item =>
                        {
                            item.VIPNO = parameter.Code;
                        });
                    }
                }
                if (MopupService.Instance.PopupStack.Count > 0)
                    await MopupService.Instance.PopAsync(animate: true);
            }
            catch(Exception ex)
            {
            }
        }
        private void CalculateTotalAmout()
        {
            try
            {
                if (INVHEADDETAILS.CurrentOpenBillDetails == null)
                    return;

                //CurrentOpenBillDetails.

                INVHEADDETAILS.CurrentOpenBill.SUBTOTAL = INVHEADDETAILS.CurrentOpenBillDetails.Sum(i => i.AMOUNT ?? 0);
                INVHEADDETAILS.CurrentOpenBill.BILLDISC = INVHEADDETAILS.CurrentOpenBill.SUBTOTAL * INVHEADDETAILS.CurrentOpenBill.DISCPERC / 100;
                //CurrentOpenBill.DISCPERC = CurrentOpenBill.SUBTOTAL ;
                //CurrentOpenBill.TAXVATAMT = CurrentOpenBillDetails.Sum(i => i.AMOUNT ?? 0 * i.TAXPERC ?? 0 / 100);

                INVHEADDETAILS.CurrentOpenBill.TAXVATAMT = INVHEADDETAILS.CurrentOpenBillDetails.Sum(i =>
                {
                    double amount = (double)(i.AMOUNT ?? 0);
                    double taxRate = (double)(i.TAXPERC ?? 0);

                    // Proportional discount for this item
                    //double itemDiscount = (amount / INVHEADDETAILS.CurrentOpenBill.SUBTOTAL) * INVHEADDETAILS.CurrentOpenBill.BILLDISC;
                    //double taxableAmount = amount - itemDiscount;


                    double itemDiscount = 0;

                    if (INVHEADDETAILS.CurrentOpenBill.SUBTOTAL != 0)
                    {
                        itemDiscount = (amount / INVHEADDETAILS.CurrentOpenBill.SUBTOTAL) * INVHEADDETAILS.CurrentOpenBill.BILLDISC;


                        if (double.IsNaN(itemDiscount) || double.IsInfinity(itemDiscount))
                            itemDiscount = 0;
                    }

                    double taxableAmount = amount - itemDiscount;

                    return taxableAmount * taxRate / 100;
                });

               

                //    INVHEADDETAILS.CurrentOpenBill.BILLAMOUNT = INVHEADDETAILS.CurrentOpenBill.SUBTOTAL - INVHEADDETAILS.CurrentOpenBill.BILLDISC + INVHEADDETAILS.CurrentOpenBill.TAXVATAMT;


                INVHEADDETAILS.CurrentOpenBill.BILLAMOUNT = INVHEADDETAILS.CurrentOpenBill.SUBTOTAL - INVHEADDETAILS.CurrentOpenBill.BILLDISC + INVHEADDETAILS.CurrentOpenBill.TAXVATAMT - INVHEADDETAILS.CurrentOpenBill.lessamount;
            }
            catch (Exception ex)
            {
            }
        }

        private void OnCategorySelected()
        {
            try
            {
                if (CategorySelectedItem == null)
                    return;
                AllItemList.Clear();
                AllItemList = new ObservableCollection<item>(_allItemsBackup.Where(x => x.CATCODE == CategorySelectedItem.CODE).ToList());
                IsCategoryListVisible = false;
                IsItemListVisible = true;
                CategorySelectedItem = null;
            }
            catch (Exception ex)
            {
                CategorySelectedItem = null;
            }
        }

        private void OnItemSelected()
        {
            try
            {
                if (ListSelectedItem == null)
                    return;


                // var existingItem = INVHEADDETAILS.CurrentOpenBillDetails.FirstOrDefault(x => x.ITEMCODE == ListSelectedItem.ITEMCODE);
                //if (existingItem != null)
                //{
                //    //CurrentSelectedBillItem = null;
                //    CurrentSelectedBillItem = existingItem;
                //    CurrentSelectedBillItem.QUANTITY += CurrentSelectedBillItem.ISVoidItem ? -1 : 1;
                //    CurrentSelectedBillItem.AMOUNT = ListSelectedItem.SALEPRIC * existingItem.QUANTITY;
                //}


                var existingItem = INVHEADDETAILS.CurrentOpenBillDetails.LastOrDefault();
                if (existingItem != null && existingItem.ITEMCODE == ListSelectedItem.ITEMCODE)
                {
                    //CurrentSelectedBillItem = null;
                    CurrentSelectedBillItem = existingItem;
                    CurrentSelectedBillItem.QUANTITY += CurrentSelectedBillItem.ISVoidItem ? -1 : 1;
                    CurrentSelectedBillItem.AMOUNT = ListSelectedItem.SALEPRIC * existingItem.QUANTITY;
                }
                else
                {
                    INVHEADDETAILS.CurrentOpenBillDetails.Add(new INVLINE
                    {
                        BRANCHCODE = "HQ",
                        TXNNO = INVHEADDETAILS.CurrentOpenBill.TXNNO,
                        VIPNO = INVHEADDETAILS.CurrentOpenBill.VIPNO,
                        SHIFT = string.Empty,
                        USER = App.ObjMainUserViewModel.UserEmail,
                        STAFF = INVHEADDETAILS.CurrentOpenBill.STAFF,
                        LINE = 0,
                        ITEMCODE = ListSelectedItem.ITEMCODE,
                        ITEMNAME1 = ListSelectedItem.ITEMNAME1,
                        ITEMNAME2 = ListSelectedItem.ITEMNAME2,
                        CATCODE = ListSelectedItem.CATCODE,
                        SUBCATCODE = ListSelectedItem.SUBCATCODE,
                        BRANDCODE = ListSelectedItem.BRANDCODE,
                        UNITCODE = ListSelectedItem.UNITCODE,
                        QUANTITY = INVHEADDETAILS.ISVoidMode ? -1 : 1,
                        UNITRATE = ListSelectedItem.SALEPRIC,
                        AMOUNT = ListSelectedItem.SALEPRIC * 1,
                        TAXPERC = 0,
                        TAXVALUE = 0,
                        COSTAMT = ListSelectedItem.COSTPRIC,
                        COSTAMTSPA = ListSelectedItem.COSTPRIC,
                        SPLDISC = null,
                        DISCPERC = 0,
                        DISCOUNT = 0,
                        LESSAMT = 0,
                        PRINTED = null,
                        BPRINTED = "N",
                        KPRINTED = "Y",
                        EATTAKE = "E",
                        STATUS = "O",
                        UPDATED = "Y",
                        STYLECODE = ListSelectedItem.STYLECODE,
                        COLORCODE = ListSelectedItem.COLORCODE,
                        SIZECODE = ListSelectedItem.SIZECODE,
                        PACKAGEID = 0,
                        PACKLINE = 0,
                        LASTUSER = App.ObjMainUserViewModel.UserEmail,
                        LASTDATE = DateTime.Now,
                        LASTTIME = DateTime.Now.ToString("HH:mm:ss"),
                        KOT = null,
                        SCANITEMCODE = ListSelectedItem.ITEMCODE,
                        PRICETYPE = "SP",
                        TOPPING = null,
                        DEPTCODE = "0",
                        SEASONCODE = string.Empty,
                        ISVoidItem = INVHEADDETAILS.ISVoidMode ? true : false,
                    });
                    CurrentSelectedBillItem = INVHEADDETAILS.CurrentOpenBillDetails?.LastOrDefault();
                }

                ListSelectedItem = null;
            }
            catch(Exception ex)
            {
                ListSelectedItem = null;
            }
            finally 
            { 
                CalculateTotalAmout();
            }
        }

        private bool _IsVisibleIndicator = false;
        public bool IsVisibleIndicator { get => _IsVisibleIndicator; set { _IsVisibleIndicator = value; OnPropertyChanged(); } }

        private bool _IsCallbillsPage = false;
        public bool IsCallbillsPage { get => _IsCallbillsPage; set { _IsCallbillsPage = value; OnPropertyChanged(); } }

        private bool _IsVisibleOrder = false;
        public bool IsVisibleOrder { get => _IsVisibleOrder; set { _IsVisibleOrder = value; OnPropertyChanged(); } }

        private bool _IsBussy = false;
        public bool IsBusy { get => _IsBussy; set { _IsBussy = value; OnPropertyChanged(); } }


        public ICommand QuickBtnCLickCommand { get; }
        private bool _IsQuickBtnCLick = false;
        public bool IsQuickBtnCLick { get => _IsQuickBtnCLick; set { _IsQuickBtnCLick = value; OnPropertyChanged(); } }

        public ICommand QuickNumKeysClickCommand { get; }
       
        private bool _IsQuickNumKeysClick = false;
        public bool IsQuickNumKeysClick { get => _IsQuickNumKeysClick; set { _IsQuickNumKeysClick = value; OnPropertyChanged(); } }

        private string _QuickQtyEntryValue = string.Empty;
        public string QuickQtyEntryValue { get => _QuickQtyEntryValue; set { _QuickQtyEntryValue = value; OnPropertyChanged(); } }

        private bool _IsCategoryListVisible = false;
        public bool IsCategoryListVisible { get => _IsCategoryListVisible; set { _IsCategoryListVisible = value; OnPropertyChanged(); } }
      
        private bool _IsItemListVisible = false;
        public bool IsItemListVisible { get => _IsItemListVisible; set { _IsItemListVisible = value; OnPropertyChanged(); } }


        private bool _IsMoreKeysVisible = false;
        public bool IsMoreKeysVisible { get => _IsMoreKeysVisible; set { _IsMoreKeysVisible = value; OnPropertyChanged(); } }



        private ObservableCollection<CATEGORY> _AllCATEGORYList = new();
        public ObservableCollection<CATEGORY> AllCATEGORYList { get => _AllCATEGORYList; set { SetProperty(ref _AllCATEGORYList, value); } }
        public ICommand CategorySelectedCommand { get; set; }

        private CATEGORY? _CategorySelectedItem;
        public CATEGORY? CategorySelectedItem { get => _CategorySelectedItem; set { SetProperty(ref _CategorySelectedItem, value); } }


        private IReadOnlyList<item> _allItemsBackup;


        private ObservableCollection<item> _AllItemList = new();
        public ObservableCollection<item> AllItemList { get => _AllItemList; set { SetProperty(ref _AllItemList, value); } }
        public ICommand ItemSelectedCommand { get; set; }

        private item? _ListSelectedItem;
        public item? ListSelectedItem { get => _ListSelectedItem; set { SetProperty(ref _ListSelectedItem, value); } }


        private ObservableCollection<INVHEAD> _AllOpenBillsList = new();
        public ObservableCollection<INVHEAD> AllOpenBillsList { get => _AllOpenBillsList; set { SetProperty(ref _AllOpenBillsList, value); } }

        //private INVHEAD _CurrentOpenBill = new();
        //public INVHEAD CurrentOpenBill { get => _CurrentOpenBill; set { SetProperty(ref _CurrentOpenBill, value); } }
      
        //private ObservableCollection<INVLINE> _CurrentOpenBillDetails = new();
        //public ObservableCollection<INVLINE> CurrentOpenBillDetails { get => _CurrentOpenBillDetails; set { SetProperty(ref _CurrentOpenBillDetails, value); OnPropertyChanged(); } }


        private INVHEADDETAILS _INVHEADDETAILS = new();
        public INVHEADDETAILS INVHEADDETAILS { get => _INVHEADDETAILS; set { SetProperty(ref _INVHEADDETAILS, value); OnPropertyChanged(); } }

        private INVLINE? _CurrentSelectedBillItem;
        public INVLINE? CurrentSelectedBillItem { get => _CurrentSelectedBillItem; set { SetProperty(ref _CurrentSelectedBillItem, value); OnPropertyChanged(); } }
      
        private INVHEAD? _CurrentSelectedOpenBill = new();
        public INVHEAD? CurrentSelectedOpenBill
        {
            get => _CurrentSelectedOpenBill;
            set
            {
                if (SetProperty(ref _CurrentSelectedOpenBill, value) && value != null)
                {
                    IsCategoryListVisible = false;
                    IsItemListVisible = false;
                    IsMoreKeysVisible = false;
                    IsVisibleOrder = true;
                    INVHEADDETAILS = new();
                    INVHEADDETAILS.CurrentOpenBill = value;
                    _ = GetBillDetalsonTXNO(INVHEADDETAILS.CurrentOpenBill.TXNNO);
                    CurrentSelectedOpenBill = null;
                }
            }
        }

        //private INVHEAD _CurrentSelectedOpenBill = new();
        //public INVHEAD CurrentSelectedOpenBill { get => _CurrentSelectedOpenBill; set { SetProperty(ref _CurrentSelectedOpenBill, value); } }
       // public ICommand AllOpenBillsSelectionChangedCommand { get; }
    }

 
}
