using RestaurantOrder.ApiService;
namespace RestaurantOrder.Views
{
    public partial class LoginPage : ContentPage
    {

        public LoginPage()
        {
            try
            {
                InitializeComponent();

                this.BindingContext = App.ObjMainUserViewModel;
                App.ObjMainUserViewModel.RecivedMessageOnLoginpage += (StringMsg) => RecivedMessageOntable(StringMsg);
            }
            catch (Exception ex) 
            { 
            }
        }
       
        protected async override void OnAppearing()
        {
            try
            {
                base.OnAppearing();
                await App.ObjMainUserViewModel.LoadUsersData();
              
            }
            catch (Exception ex)
            { 
            }
        }
        public async Task RecivedMessageOntable( string[] RecivedMessage)
        {
            try
            {
                //DisplayUsernamePassword.Text = RecivedMessage; 
                //SemanticScreenReader.Announce(DisplayUsernamePassword.Text);
                switch (RecivedMessage[0])
                {
                    case "DISPLAYALERT":
                        await DisplayAlert("Error", RecivedMessage[1], "OK");
                        break;
                    //case "LOGINSUCCESS":
                    //    // Application.Current.MainPage = new AppShell();
                    //    if (Application.Current != null)
                    //        Application.Current.Windows[0].Page = new AppShell();
                    //    break;
                    case "TOASTALERT":
                        CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
                        var toast = Toast.Make(RecivedMessage[1], ToastDuration.Short, 15).Show(cancellationTokenSource.Token);
                        break;
                    default:
                     //   DisplayUsernamePassword.Text = $"Network Issue - Check your Setting.";
                        break;
                }
                //SemanticScreenReader.Announce(DisplayUsernamePassword.Text);
            }
            catch (Exception ex)
            { 
            }
        }
        protected override bool OnBackButtonPressed()
        {
            return true;
        }
        private async void OnCounterClicked(object? sender, EventArgs e)
        {
            App.ObjMainUserViewModel.OnActionLoggedIn();

        }

        private async void BtnSaveipport_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (BtnSaveipport.Text == "Edit")
                {
                    BorderEntryIpPort.InputTransparent = false;
                    EntryIpPort.Focus();
                    BtnSaveipport.Text = "Save";
                }
                else
                {
                    BtnSaveipport.Text = "Edit";
                    EntryIpPort.Unfocus();
                    BorderEntryIpPort.InputTransparent = true;
                    ApiClient.SetBaseUrl("http://" + EntryIpPort.Text);
                    await App.ObjMainUserViewModel.LoadUsersData();
                }
            }
            catch (Exception ex)
            { }

        }
    }
}
