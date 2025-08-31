using RestaurantOrder.ViewModels;

namespace RestaurantOrder.Views;

public partial class NewOrderPage : ContentPage
{
    //public NewOrderPage()
    NewOrderViewModel  NewOrderPageVM;
    public NewOrderPage(NewOrderViewModel NewOrderVM)
	{
		try
		{
            BindingContext = NewOrderPageVM = NewOrderVM;
            InitializeComponent();
        }
		catch (Exception ex)
		{

		}
	}
    protected override void OnAppearing()
    {
        try
        {
            base.OnAppearing();
            
            //if (BindingContext is NewOrderViewModel vm)
            //{
            //    if (!vm.IsVisibleOrder)
            //        _ = vm.LoadAllOpenBillsList();
            //}
        }
        catch (Exception ex)
        {

        }
    }
    private async void ImageCloseButton_Clicked(object sender, EventArgs e)
    {
        try
        {
            if (NewOrderPageVM.IsCallbillsPage && NewOrderPageVM.IsVisibleOrder)
            {
                NewOrderPageVM.IsVisibleOrder = false;
                NewOrderPageVM.CurrentOpenBill = null;
                NewOrderPageVM.CurrentOpenBillDetails = null;
            }
            else
            {
                BindingContext = null;
                NewOrderPageVM = null;
                await Navigation.PopModalAsync(animated: true);
            }
        }
        catch (Exception ex)
        {

        }
    }
}