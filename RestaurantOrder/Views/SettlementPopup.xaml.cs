using Microsoft.Win32.SafeHandles;
using Mopups.Pages;
using Mopups.Services;
using RestaurantOrder.ViewModels;
using System.Runtime.InteropServices;
using System.Windows.Input;

namespace RestaurantOrder.Views;

public partial class SettlementPopup : PopupPage
{
    public bool IsPageOpen = false;
    public ICommand PopupCommand { get; set; }
    SettlementViewModel SettlementVM;// = new();
    public SettlementPopup(SettlementViewModel viewModel)
	{
		InitializeComponent();
        SettlementVM = viewModel;

        SettlementVM.ClosePopup = async (success) =>
        {
            try
            {
                var Settledarray = new[] { "CLOSESETTLEDBILL" };
                if (PopupCommand?.CanExecute(Settledarray) == true)
                {
                    PopupCommand.Execute(Settledarray);
                }

                if (MopupService.Instance.PopupStack.Count > 0)
                    await MopupService.Instance.PopAsync();
            }
            catch
            {

            }
        };

        this.BindingContext = SettlementVM;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
       await SettlementVM.LoadCardsType();
    }
    protected override bool OnBackButtonPressed()
    {
        return false;
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        IsPageOpen = false;
    }

    ~SettlementPopup()
    {
        Dispose();
    }

    SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);
    bool disposed = false;
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposed)
            return;

        if (disposing)
        {
            handle?.Dispose();
        }
        disposed = true;
    }

    private void OnFastCashClicked(object sender, EventArgs e)
    {
        SettlementVM.IsVisibleCardMode = false;
        SettlementVM.IsVisibleCurrencyMode = false;
        SettlementVM.IsVisibleCashMode = true;
    }

    private void OnCardClicked(object sender, EventArgs e)
    {
        SettlementVM.IsVisibleCashMode = false;
        SettlementVM.IsVisibleCurrencyMode = false;
        SettlementVM.IsVisibleCardMode = true;
 
    }
    private void OnCurrencyClicked(object sender, EventArgs e)
    {
        SettlementVM.IsVisibleCashMode = false;
        SettlementVM.IsVisibleCardMode = false;
        SettlementVM.IsVisibleCurrencyMode = true;
 
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        // IsPageOpen = true;

        try
        {
            if (MopupService.Instance.PopupStack.Count > 0)
                await MopupService.Instance.PopAsync();
        }
        catch
        {

        }
    }
}