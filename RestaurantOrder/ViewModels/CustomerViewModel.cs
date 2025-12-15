using Mopups.Services;
using RestaurantOrder.ApiService;
using RestaurantOrder.Models;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;

namespace RestaurantOrder.ViewModels
{
    public class CustomerViewModel : NotifyPropertyBaseViewModel
    {

        public CustomerViewModel()
        {
            try
            {
              _= LoadAllCustomerList();
                QuickBtnCLickCommand = new Command<string>(QuickBtnClicked);
                CustomerSelectedCommand = new Command(OnCustomerSelected);
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
                    case "SAVECUSTOMER":
                        if (NewCustomer == null)
                            break;

                         CustomerFilter ObjCustomerFilter = new CustomerFilter();
                        ObjCustomerFilter.Mode = ActionMode;
                        if (ActionMode == "INSERT")
                        {
                            if (!IsValidCustEntry())
                                break;
                            NewCustomer.BranchCode = "HQ";
                            NewCustomer.Prefix = "Mr.";
                            //NewCustomer.Mode = "INSERT";
                            NewCustomer.DateJoin = DateTime.Now;
                            NewCustomer.ExpiryDate = null;
                            NewCustomer.RegAmount = 0;
                            NewCustomer.SalePer = null;
                            NewCustomer.CompName = string.Empty;
                            NewCustomer.CardNo = null;
                            NewCustomer.Updated = "Y";
                            NewCustomer.LastUser = "ADMIN";
                            NewCustomer.LastDate = DateTime.Now;
                            NewCustomer.LastTime = NewCustomer.LastTime = DateTime.Now.ToString("hh:mm:ss tt"); //DateTime.Now.TimeOfDay.ToString("hh:mm:ss tt"); 
                            NewCustomer.CustType = "C001"; 
                            NewCustomer.Discount = 0; 
                            NewCustomer.PriceType = null; 
                            NewCustomer.CustImage = null; 
                            NewCustomer.Type = null; 

                        }
                        else if(ActionMode == "UPDATE")
                        {
                            NewCustomer.LastUser = "ADMIN";
                            NewCustomer.LastDate = DateTime.Now;
                            NewCustomer.LastTime = NewCustomer.LastTime = DateTime.Now.ToString("hh:mm:ss tt");
                            
                        }

                        ObjCustomerFilter.ObjCustomer = NewCustomer;
                        var response = await ApiClient.PostAsync<CustomerFilter, string>("Customer/InsertCustomer", ObjCustomerFilter,1);
                        if (response != null)
                        {

                            if(response == "Inserted")
                            {
                                App.cancellationTokenSource = new();
                                await Toast.Make("Customer Added..", ToastDuration.Short, 10).Show(App.cancellationTokenSource.Token);

                                _ = LoadAllCustomerList();
                                PageHeading = "CUSTOMER";
                                ActionMode = String.Empty;
                                NewCustomer = null;
                                IsVisibleAddPanel = false;
                            }
                            else if (response == "Insert Failed")
                            {
                                App.cancellationTokenSource = new();
                                await Toast.Make("Customer Added Failed.", ToastDuration.Short, 10).Show(App.cancellationTokenSource.Token);

                            }
                            else if (response == "Updated")
                            {
                                App.cancellationTokenSource = new();
                                await Toast.Make("Customer Updated..", ToastDuration.Short, 10).Show(App.cancellationTokenSource.Token);

                                _ = LoadAllCustomerList();
                                PageHeading = "CUSTOMER";
                                ActionMode = String.Empty;
                                NewCustomer = null;
                                IsVisibleAddPanel = false;

                            }
                            else if (response == "Updated Failed")
                            {
                                App.cancellationTokenSource = new();
                                await Toast.Make("Customer Updated Failed.", ToastDuration.Short, 10).Show(App.cancellationTokenSource.Token);
                            }
                            else if (response == "Unexpected error.")
                            {
                                App.cancellationTokenSource = new();
                                await Toast.Make("Something went wrong..", ToastDuration.Short, 10).Show(App.cancellationTokenSource.Token);
                            }
                            else if (response == "Customer data is required.")
                            {
                                App.cancellationTokenSource = new();
                                await Toast.Make("Valid Data required.", ToastDuration.Short, 10).Show(App.cancellationTokenSource.Token);
                            }
                            else if (response.Contains("Error saving customer:"))
                            {
                                App.cancellationTokenSource = new();
                                await Toast.Make("Something went wrong..", ToastDuration.Short, 10).Show(App.cancellationTokenSource.Token);
                            }

                        }
                        break;
                    case "EDIT":
                        if (SelectedCustomer == null)
                        {
                            App.cancellationTokenSource = new();
                            await Toast.Make("Please Select Customer to Update.", ToastDuration.Short, 10).Show(App.cancellationTokenSource.Token);
                        }
                        else
                        {
                            NewCustomer = SelectedCustomer;
                            IsEnableCode = false;
                            PageHeading = "EDIT CUSTOMER";
                            ActionMode = "UPDATE";
                            IsVisibleAddPanel = true;
                        }
                        break;
                    case "ADD":
                        PageHeading = "ADD CUSTOMER";
                        ActionMode = "INSERT";
                        NewCustomer = new();
                        IsEnableCode = true;
                        IsVisibleAddPanel = true;
                        break;
                    case "CLOSEADDEDITMODE":
                        PageHeading = "CUSTOMER";
                        ActionMode = String.Empty;
                        SelectedCustomer = null;
                        NewCustomer = null;
                        IsVisibleAddPanel = false;
                        break;
                    case "CLOSEPAGE":
                        PageHeading = "CUSTOMER";
                        ActionMode = String.Empty;
                        NewCustomer = null;
                        SelectedCustomer = null;
                        IsVisibleAddPanel = false;
                        if (MopupService.Instance.PopupStack.Count > 0)
                            await MopupService.Instance.PopAsync(animate: true);
                        break;
                    default:
                        // Handle default
                        break;
                }
                Console.WriteLine($"Button clicked: {parameter}");
            }
            catch (Exception ex)
            {

            }
            finally
            {
                IsQuickBtnCLick = false;
            }
        }
       
        private bool IsValidCustEntry()
        {
            //bool IsValid = true;
            try
            {
                if (string.IsNullOrEmpty(NewCustomer?.Code))
                {
                    App.cancellationTokenSource = new();
                    Toast.Make("Please Enter Customer Code.", ToastDuration.Short, 10).Show(App.cancellationTokenSource.Token);
                   return false;
                }

                else if (string.IsNullOrEmpty(NewCustomer?.FirstName))
                {
                    App.cancellationTokenSource = new();
                    Toast.Make("Please Enter First Name.", ToastDuration.Short, 10).Show(App.cancellationTokenSource.Token);
                    return false;
                }
                else if (string.IsNullOrEmpty(NewCustomer?.LastName))
                {
                    App.cancellationTokenSource = new();
                    Toast.Make("Please Enter Last Name.", ToastDuration.Short, 10).Show(App.cancellationTokenSource.Token);
                    return false;
                }
            }
            catch(Exception ex)
            {
                return false;
            }
            return true;
        }
        private void OnCustomerSelected()
        {
            try
            {
                if (SelectedCustomer == null)
                    return;
                //NewCustomer = SelectedCustomer;

            }
            catch (Exception ex)
            {
                //SelectedCustomer = null;
            }
            finally
            {
                //SelectedCustomer = null;
            }
        }
        public async Task LoadAllCustomerList()
        {
            try
            {
                IsBusy = true;
                IsVisibleIndicator = true;
                AllCustomerList = await ApiClient.GetAsync<ObservableCollection<Customer>>("Customer/GetCustomers") ?? new ObservableCollection<Customer>();
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
        private string _ActionMode = "INSERT";
        public string ActionMode { get => _ActionMode; set { _ActionMode = value; OnPropertyChanged(); } }

        public ICommand QuickBtnCLickCommand { get; }
        private bool _IsQuickBtnCLick = false;
        public bool IsQuickBtnCLick { get => _IsQuickBtnCLick; set { _IsQuickBtnCLick = value; OnPropertyChanged(); } }

        private bool _IsVisibleIndicator = false;
        public bool IsVisibleIndicator { get => _IsVisibleIndicator; set { _IsVisibleIndicator = value; OnPropertyChanged(); } }

        private bool _IsVisibleAddPanel = false;
        public bool IsVisibleAddPanel { get => _IsVisibleAddPanel; set { _IsVisibleAddPanel = value; OnPropertyChanged(); } }
        private bool _IsEnableCode = false;
        public bool IsEnableCode { get => _IsEnableCode; set { _IsEnableCode = value; OnPropertyChanged(); } }

        private bool _IsBussy = false;
        public bool IsBusy { get => _IsBussy; set { _IsBussy = value; OnPropertyChanged(); } }
        
        private string _PageHeading = "CUSTOMER";
        public string PageHeading { get => _PageHeading; set { _PageHeading = value; OnPropertyChanged(); } }

        public ICommand CustomerSelectedCommand { get; set; }
        private ObservableCollection<Customer> _AllCustomerList = new();
        public ObservableCollection<Customer> AllCustomerList { get => _AllCustomerList; set { SetProperty(ref _AllCustomerList, value); } }
        
        private Customer? _NewCustomer = null;
        public Customer? NewCustomer { get => _NewCustomer; set { SetProperty(ref _NewCustomer, value); } }

        private Customer? _SelectedCustomer = null;
        public Customer? SelectedCustomer { get => _SelectedCustomer; set { SetProperty(ref _SelectedCustomer, value); } }




    }
}
