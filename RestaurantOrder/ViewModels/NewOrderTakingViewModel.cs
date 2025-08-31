namespace RestaurantOrder.ViewModels
{
    public class NewOrderTakingViewModel : NotifyPropertyBaseViewModel
    {
        public NewOrderTakingViewModel()
        { 
        
        }
       
        private double _TableNo;
        public double TableNo { get => _TableNo; set { _TableNo = value; OnPropertyChanged(); } }

        private double _TotalGuest;
        public double TotalGuest { get => _TotalGuest; set { _TotalGuest = value; OnPropertyChanged(); } }




        private bool _IsVisibleIndicator = false;
        public bool IsVisibleIndicator { get => _IsVisibleIndicator; set { _IsVisibleIndicator = value; OnPropertyChanged(); } }
     
        private bool _IsBussy = false;
        public bool IsBusy { get => _IsBussy; set { _IsBussy = value; OnPropertyChanged(); } }
    }
}
