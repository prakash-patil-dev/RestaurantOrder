using Microsoft.Win32.SafeHandles;
using Mopups.Pages;
using Mopups.Services;
using System.Runtime.InteropServices;
using System.Windows.Input;

namespace RestaurantOrder.Views
{

    public partial class ConformPopup : PopupPage, IDisposable
    {
        public bool IsPageOpen = false;
        public ICommand PopupCommand { get; set; }

        public ConformPopup()
        {
            InitializeComponent();
            BindingContext = this;
        }

        private async void ImgYes_Clicked(object sender, EventArgs e)
        {
            try
            {
                var SaveArry = new string[] { PageType == "SAVEBILL" ? "SAVEBILL" : "REMOVEITEM" };//: new string[] { "REMOVEITEM" };
                if (PopupCommand?.CanExecute(SaveArry) == true)
                {
                    PopupCommand.Execute(SaveArry);
                }
                if (MopupService.Instance.PopupStack.Count > 0)
                    await MopupService.Instance.PopAsync();
            }
            catch
            {

            }
        }

        private async void ImgCancel_Clicked(object sender, EventArgs e)
        {
            try
            {
                var CancelArry = new string[] { "CANCELBILL" };
                if (PopupCommand?.CanExecute(CancelArry) == true)
                {
                    PopupCommand.Execute(CancelArry);
                }
                if (MopupService.Instance.PopupStack.Count > 0)
                    await MopupService.Instance.PopAsync();
            }
            catch
            {

            }
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

        ~ConformPopup()
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

        public string PageMessage
        {
            get { return (string)base.GetValue(_PageMessage); }
            set { base.SetValue(_PageMessage, value); }
        }

        private static readonly BindableProperty _PageMessage = BindableProperty.Create(
                                                         propertyName: "PageMessage",
                                                         returnType: typeof(string),
                                                         declaringType: typeof(ConformPopup),
                                                         defaultValue: "",
                                                         defaultBindingMode: BindingMode.TwoWay);
        public string PageHeader
        {
            get { return (string)base.GetValue(_PageHeader); }
            set { base.SetValue(_PageHeader, value); }
        }

        private static readonly BindableProperty _PageHeader = BindableProperty.Create(
                                                         propertyName: "PageHeader",
                                                         returnType: typeof(string),
                                                         declaringType: typeof(ConformPopup),
                                                         defaultValue: "Notice",
                                                         defaultBindingMode: BindingMode.TwoWay);

        public string PageType 
        {
            get { return (string)base.GetValue(_PageType); }
            set { base.SetValue(_PageType, value); }
        }

        private static readonly BindableProperty _PageType = BindableProperty.Create(
                                                         propertyName: "PageType",
                                                         returnType: typeof(string),
                                                         declaringType: typeof(ConformPopup),
                                                         defaultValue: "SAVEBILL",
                                                         defaultBindingMode: BindingMode.TwoWay, propertyChanged: (b, o, n) =>
                                                         {
                                                             var page = (ConformPopup)b;
                                                             //page.ImgCancel.Text = (string)n == "SAVEBILL" ? "? Yes, Save" : "? Yes";
                                                             //page.ImgExit.Text = (string)n == "SAVEBILL" ? "? No, Cancel": "? No" ;

                                                             page.ImgExit.Text = (string)n == "SAVEBILL" ? "Yes, Save" : "Yes";
                                                             page.ImgCancel.Text = (string)n == "SAVEBILL" ? "No, Cancel" : "No";
                                                         });

    }

}