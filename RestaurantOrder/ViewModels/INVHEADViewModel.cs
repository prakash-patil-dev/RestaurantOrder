using RestaurantOrder.ApiService;
using RestaurantOrder.Models;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace RestaurantOrder.ViewModels
{
    public class INVHEADViewModel : NotifyPropertyBaseViewModel
    {
        public INVHEADViewModel()
        {
            //try
            //{
            //   _= LoadAllOpenBillsList();
            //}
            //catch (Exception ex)
            //{
            //}
        }

        public async Task LoadAllOpenBillsList()
        {
            try
            {
                Isbussy = true;
                IsVisibleIndicator = true;

                // Run the API call in a background thread
                var bills = await Task.Run(async () =>
                {
                    return await ApiClient.GetAsync<ObservableCollection<INVHEAD>>("INVHEAD/GetOpenBills");
                });
                AllOpenBillsList = bills ?? new ObservableCollection<INVHEAD>();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"LoadAllOpenBillsList error: {ex}");
            }
            finally
            {
                Isbussy = false;
                IsVisibleIndicator = false;
            }
        }


        private bool _IsBussy = false;
        public bool Isbussy { get => _IsBussy; set { _IsBussy = value; OnPropertyChanged(); } }

        private bool _IsVisibleIndicator = false;
        public bool IsVisibleIndicator { get => _IsVisibleIndicator; set { _IsVisibleIndicator = value; OnPropertyChanged(); } }

        private ObservableCollection<INVHEAD> _AllOpenBillsList = new();
        public ObservableCollection<INVHEAD> AllOpenBillsList { get => _AllOpenBillsList; set { SetProperty(ref _AllOpenBillsList, value); } }

    }
}
