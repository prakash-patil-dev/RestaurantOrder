using Mopups.Pages;
using Mopups.Services;
using RestaurantOrder.ViewModels;
using System.Windows.Input;

namespace RestaurantOrder.Views;

public partial class CustomerPopup : PopupPage
{
    public bool IsPageOpen = false;
    CustomerViewModel CustVM = new();
    public ICommand SelectedCustomerCommand { get; set; }

    public CustomerPopup()
    {
        try
        {
            InitializeComponent();
            this.BindingContext = CustVM;
            //SelectedCustomerCommand = new Command(OnCustomerSelected);
        }
        catch { }
    }

    protected override void OnAppearing()
    {
       
        try
        {
            base.OnAppearing();

            _ = CustVM.LoadAllCustomerList();
        }
        catch { }
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
    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        IsPageOpen = false;
    }

    protected override bool OnBackButtonPressed()
    {
        return false;
    }
    private async void OnBtnClosePopup(object sender, EventArgs args)
    {
        try
        {
            if (MopupService.Instance.PopupStack.Count > 0)
                await MopupService.Instance.PopAsync();
        }
        catch
        {

        }
    }
    private async void OnPopupUpClose(object sender, EventArgs args)
    {
        try
        {

          
                if (MopupService.Instance.PopupStack.Count > 0)
                    await MopupService.Instance.PopAsync();
        }
        catch
        {

        }
    }

    private void SelectCustomer_Clicked(object sender, EventArgs e)
    {
        try
        {
            if (CustVM.SelectedCustomer == null)
            {
                App.cancellationTokenSource = new();
                Toast.Make("Please Select Valid Customer.", ToastDuration.Short, 10).Show(App.cancellationTokenSource.Token);
            }
            else
            {
                if (SelectedCustomerCommand?.CanExecute(CustVM.SelectedCustomer) == true)
                {
                    SelectedCustomerCommand.Execute(CustVM.SelectedCustomer);
                }
            }

        } 
        catch(Exception ex)
        {


        }
    }



    //private async void ImageCloseButton_Clicked(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        if (MopupService.Instance.PopupStack.Any(p => p.GetType() == typeof(CustomerPopup)))
    //        {
    //            await MopupService.Instance.PopAsync(animate: true);
    //        }
    //    }
    //    catch (Exception ex)
    //    {

    //    }
    //}
}