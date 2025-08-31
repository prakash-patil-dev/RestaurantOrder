using RestaurantOrder.ApiService;
using RestaurantOrder.Models;
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
                //AllOpenBillsSelectionChangedCommand = new Command<INVHEAD>(OnAllOpenBillSelectionChanged);
                // AllOpenBillsListRefreshCommand = new Command(async () => await LoadAllOpenBillsList());

            }
            catch (Exception ex)
            {
            }
        }
        // Replace all instances of "Isbussy" with "IsBusy" for correct spelling


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
                CurrentOpenBillDetails = await ApiClient.GetAsync<ObservableCollection<INVLINE>>($"INVHEAD/GetInvLineFromTXNNO/{txnNo}") ?? new ObservableCollection<INVLINE>();
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

                AllItemList = await ApiClient.GetAsync<ObservableCollection<item>>("Item/GetItems") ?? new ObservableCollection<item>();
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

        private void QuickBtnClicked(string parameter)
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
                        IsCategoryListVisible = false;
                        IsItemListVisible = !IsItemListVisible  ;
                        break;
                    case "BILL TYPE":
                        // Handle BILL TYPE
                        break;
                    case "ARROWCOMMAND":
                        IsMoreKeysVisible = !IsMoreKeysVisible;
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

        //private void OnAllOpenBillSelectionChanged(INVHEAD selectedBill)
        //{
        //    if (selectedBill != null)
        //    {
        //        CurrentOpenBill = selectedBill;
        //    }
        //}

        //private string _TableNo;
        //public string TableNo { get => _TableNo; set { _TableNo = value; OnPropertyChanged(); } }
        
        //private string _TotalGuest;
        //public string TotalGuest { get => _TotalGuest; set { _TotalGuest = value; OnPropertyChanged(); } }


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

        private bool _IsCategoryListVisible = false;
        public bool IsCategoryListVisible { get => _IsCategoryListVisible; set { _IsCategoryListVisible = value; OnPropertyChanged(); } }
      
        private bool _IsItemListVisible = false;
        public bool IsItemListVisible { get => _IsItemListVisible; set { _IsItemListVisible = value; OnPropertyChanged(); } }


        private bool _IsMoreKeysVisible = false;
        public bool IsMoreKeysVisible { get => _IsMoreKeysVisible; set { _IsMoreKeysVisible = value; OnPropertyChanged(); } }



        private ObservableCollection<CATEGORY> _AllCATEGORYList = new();
        public ObservableCollection<CATEGORY> AllCATEGORYList { get => _AllCATEGORYList; set { SetProperty(ref _AllCATEGORYList, value); } }

        private ObservableCollection<item> _AllItemList = new();
        public ObservableCollection<item> AllItemList { get => _AllItemList; set { SetProperty(ref _AllItemList, value); } }

        private ObservableCollection<INVHEAD> _AllOpenBillsList = new();
        public ObservableCollection<INVHEAD> AllOpenBillsList { get => _AllOpenBillsList; set { SetProperty(ref _AllOpenBillsList, value); } }

        private INVHEAD _CurrentOpenBill = new();
        public INVHEAD CurrentOpenBill { get => _CurrentOpenBill; set { SetProperty(ref _CurrentOpenBill, value); } }
      
        private ObservableCollection<INVLINE> _CurrentOpenBillDetails = new();
        public ObservableCollection<INVLINE> CurrentOpenBillDetails { get => _CurrentOpenBillDetails; set { SetProperty(ref _CurrentOpenBillDetails, value); } }

        private INVHEAD _CurrentSelectedOpenBill = new();
        public INVHEAD CurrentSelectedOpenBill
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
                    CurrentOpenBill = value;
                    _ = GetBillDetalsonTXNO(CurrentOpenBill.TXNNO);
                    CurrentSelectedOpenBill = null;
                }
            }
        }

        //private INVHEAD _CurrentSelectedOpenBill = new();
        //public INVHEAD CurrentSelectedOpenBill { get => _CurrentSelectedOpenBill; set { SetProperty(ref _CurrentSelectedOpenBill, value); } }
       // public ICommand AllOpenBillsSelectionChangedCommand { get; }
    }
}
