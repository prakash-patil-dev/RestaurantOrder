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

            NewOrderVM.INVHEADDETAILS.CurrentOpenBill = null; 
            NewOrderVM.INVHEADDETAILS.CurrentOpenBillDetails = null; 
            NewOrderVM.INVHEADDETAILS = null;
        }
        catch (Exception ex)
        {
            // Handle exception
        }
    }

    private async void OnLogoutClicked(object sender, EventArgs e)
    {
        bool confirm = await Application.Current.MainPage.DisplayAlert(
            "Confirm Logout",          // Title
            "Are you sure you want to log out?", // Message
            "Yes",                     // Accept button
            "Cancel"                   // Cancel button
        );

        if (confirm)
        {
           // DependencyService.Get<ICloseAppService>()?.CloseApp();
            var closeService = IPlatformApplication.Current.Services.GetService<ICloseAppService>();
            closeService?.CloseApp();

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
            NewOrderVM.IsComboListVisible  = false;
            NewOrderVM.IsCallbillsPage = true;
            NewOrderVM.IsMoreKeysVisible = false;
            _NewOrederPage = new NewOrderPage(NewOrderVM);
            await Navigation.PushModalAsync(_NewOrederPage, true);
            //_ = NewOrderVM.LoadAllOpenBillsList();
        }
        catch (Exception ex)
        {
            // Handle exception
            await DisplayAlert("Error", "An error occurred while trying to call bills.", "OK");
        }
        finally
        {
            VM.TableNo = 0;
            VM.TotalGuest = 0;
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
            NewOrderVM.IsComboListVisible = false;
            NewOrderVM.IsMoreKeysVisible = false;
            NewOrderVM.INVHEADDETAILS = new();
            NewOrderVM.INVHEADDETAILS.CurrentOpenBill = new();
            NewOrderVM.INVHEADDETAILS.CurrentOpenBill.TABLENO = VM.TableNo;
            NewOrderVM.INVHEADDETAILS.CurrentOpenBill.NOOFPERSNS = VM.TotalGuest;
            //NewOrderVM.INVHEADDETAILS.CurrentOpenBill.TOTCOSTAMT = VM.TotalGuest;
            NewOrderVM.INVHEADDETAILS.CurrentOpenBill.STATUS = "O";
            NewOrderVM.INVHEADDETAILS.CurrentOpenBill.USER = App.ObjMainUserViewModel.UserEmail;
            NewOrderVM.INVHEADDETAILS.CurrentOpenBill.LASTUSER = App.ObjMainUserViewModel.UserEmail;
            NewOrderVM.INVHEADDETAILS.CurrentOpenBillDetails = new();
            //NewOrderVM.CurrentOpenBillDetails = new();
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
            VM.TableNo = 0;
            VM.TotalGuest = 0;
            VM.IsBusy = false;
            VM.IsVisibleIndicator = false;
        }
    }
}