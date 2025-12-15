using Microsoft.Win32.SafeHandles;
using Mopups.Pages;
using Mopups.Services;
using System.Runtime.InteropServices;
using System.Windows.Input;

namespace RestaurantOrder.Views;

public partial class MenuPopup : PopupPage, IDisposable
{
    public bool IsPageOpen = false;
    //public ICommand QuickBtnCLickCommand { get; set; }
    private ICommand _quickBtnCLickCommand;
    public ICommand QuickBtnCLickCommand
    {
        get => _quickBtnCLickCommand;
        set
        {
            _quickBtnCLickCommand = value;
            OnPropertyChanged(nameof(QuickBtnCLickCommand));
        }
    }

    public MenuPopup()
    {
        InitializeComponent();
        BindingContext = this;
    }
    protected override bool OnBackButtonPressed()
    {
        return false;
    }
    private async void OnBtnClosePopup(object sender, TappedEventArgs e)
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
    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        IsPageOpen = false;
    }

    ~MenuPopup()
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

}