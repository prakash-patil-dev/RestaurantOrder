using RestaurantOrder.ViewModels;
using System.Threading.Tasks;

namespace RestaurantOrder.Views;

public partial class NewOrderTaking : ContentPage
{
   // CallBillsPage callBillsPage;
    NewOrderPage _NewOrederPage;


    private NewOrderViewModel NewOrderVM = new NewOrderViewModel();
    private NewOrderTakingViewModel VM = new NewOrderTakingViewModel();
    public NewOrderTaking()
    {
        InitializeComponent();
        BindingContext = VM;
    }
    protected override void OnAppearing()
    {
        try
        {
            base.OnAppearing();
            NewOrderVM.IsVisibleOrder = false;
            NewOrderVM.IsCallbillsPage = false; 
            NewOrderVM.CurrentOpenBill = null;
            NewOrderVM.CurrentOpenBillDetails = null;
        }
        catch (Exception ex)
        {
            // Handle exception
        }
    }

    protected override bool OnBackButtonPressed()
    {
        return true;
    }
    private void OnTableNoEntryTextChanged(object sender, TextChangedEventArgs e)
    {
        try
        {
            var entry = sender as Entry;
            if (entry == null) return;

            // Remove any non-digit characters
            string filtered = new string(e.NewTextValue.Where(char.IsDigit).ToArray());

            if (entry.Text != filtered || string.IsNullOrEmpty(entry.Text))
            {
                entry.Text = filtered;
            }
        }
        catch (Exception ex)
        {
            // Handle exception
        }
    }
    private void OnTotalGuestEntryTextChanged(object sender, TextChangedEventArgs e)
    {
        try
        {
            var entry = sender as Entry;
            if (entry == null) return;

            // Remove any non-digit characters
            string filtered = new string(e.NewTextValue.Where(char.IsDigit).ToArray());

            if (entry.Text != filtered || string.IsNullOrEmpty(entry.Text))
            {
                entry.Text =  filtered;

            }
        }
        catch (Exception ex)
        {
            // Handle exception
        }
    }

    private async void BtnCall_Clicked(object sender, EventArgs e)
    {
        try
        {
            if (VM.IsVisibleIndicator)
                return;
            VM.IsBusy = true;
            VM.IsVisibleIndicator = true;

            NewOrderVM.IsVisibleOrder = false;
            NewOrderVM.IsCategoryListVisible = false;
            NewOrderVM.IsItemListVisible = false;
            NewOrderVM.IsCallbillsPage = true;
            NewOrderVM.IsMoreKeysVisible = false;
            _NewOrederPage = new NewOrderPage(NewOrderVM);
            await Navigation.PushModalAsync(_NewOrederPage, true);
           
        }
        catch (Exception ex)
        {
            // Handle exception
            await DisplayAlert("Error", "An error occurred while trying to call bills.", "OK");
        }
        finally
        {
            VM.IsBusy = false;
            VM.IsVisibleIndicator = false;
        }
    }

    private async void BtnNewOrder_Clicked(object sender, EventArgs e)
    {
        try
        {
            if (VM.IsVisibleIndicator)
                return;
            VM.IsBusy = true;
            VM.IsVisibleIndicator = true;

            NewOrderVM.IsVisibleOrder = true;
            NewOrderVM.IsCallbillsPage = false;
            NewOrderVM.IsCategoryListVisible = false;
            NewOrderVM.IsItemListVisible = false;
            NewOrderVM.IsMoreKeysVisible = false;
            NewOrderVM.CurrentOpenBill = new();
            NewOrderVM.CurrentOpenBill.TABLENO = VM.TableNo;
            NewOrderVM.CurrentOpenBill.TOTCOSTAMT = VM.TotalGuest;
            NewOrderVM.CurrentOpenBillDetails = new();
            _NewOrederPage = new NewOrderPage(NewOrderVM);
            await Navigation.PushModalAsync(_NewOrederPage, true);
        }
        catch (Exception ex)
        {
            // Handle exception
            await DisplayAlert("Error", "An error occurred while trying to call bills.", "OK");
        }
        finally
        {
            VM.IsBusy = false;
            VM.IsVisibleIndicator = false;
        }
    }
}