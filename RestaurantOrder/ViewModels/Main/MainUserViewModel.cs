using RestaurantOrder.ApiService;
using RestaurantOrder.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace RestaurantOrder.ViewModels.Main;
public class MainUserViewModel : NotifyPropertyBaseViewModel
{
    private string _UserCode = string.Empty;
    public string UserCode { get { return _UserCode; } set { SetProperty(ref _UserCode, value); } }

    private string _UserEmail = string.Empty;
    public string UserEmail { get => _UserEmail; set { SetProperty(ref _UserEmail, value); } }
   
    private string _UserPassword = string.Empty;
    public string UserPassword { get => _UserPassword; set { SetProperty(ref _UserPassword, value); } }


    private string _Password = string.Empty;
    public string Password { get => _Password; set { SetProperty(ref _Password, value); } }

    private string _IconEye = "eye_on.png";
    public string IconEye { get => _IconEye; set { SetProperty(ref _IconEye, value); } }

    private bool _isPasswordHidden = true;
    public bool IsPasswordHidden { get => _isPasswordHidden; set { _isPasswordHidden = value; OnPropertyChanged(); } }

    public ICommand TogglePasswordCommand => new Command(() => 
    {
        IsPasswordHidden = !IsPasswordHidden;
        IconEye = IsPasswordHidden ? "eye_on.png" : "eye_off.png";
    });



    private ObservableCollection<User> _UsersList = new();
    public ObservableCollection<User> UsersList { get => _UsersList; set { SetProperty(ref _UsersList, value); } }
   
    private User _SelectedUser = new();
    public User SelectedUser { get => _SelectedUser; set { SetProperty(ref _SelectedUser, value); } }

    // public ICommand? LoggedIn_Command { protected set; get; }

    private bool _isbussy = false;
    public bool Isbussy { get => _isbussy; set { _isbussy = value; OnPropertyChanged(); } }

    private bool _isVisibleIndecator = false;
    public bool IsVisibleIndecator { get => _isVisibleIndecator; set { _isVisibleIndecator = value; OnPropertyChanged(); } }

    public Action<string[]>? RecivedMessageOnLoginpage;
    public Action? OnActionLoggedIn;
    public MainUserViewModel()
    {
        try
        {
          _=  CheckLoginStatusAsync();

             // LoggedIn_Command = new Command(OnCommandLoggedIn);
             OnActionLoggedIn += OnCommandLoggedIn;
        }
        catch (Exception ex)
        { 
        }
    }
    public async Task CheckLoginStatusAsync()
    {
        var isLoggedIn = await SecureStorage.GetAsync("IsLoggedIn");
        var isLoggedIn1 = await SecureStorage.GetAsync("Username");
        var isLoggedIn2 = await SecureStorage.GetAsync("Password");

        if (isLoggedIn == "true")
        {
            // Navigate to home page
        }
        else
        {
            // Show login page
        }
    }
    public async Task LoadUsersData()
    {
        try
        {
            Isbussy = true;
            IsVisibleIndecator = true;
            UsersList = await ApiClient.GetAsync<ObservableCollection<User>>("User/GetUsers");
            if(UsersList == null)
            {
                Isbussy = false;
                IsVisibleIndecator = false;
                RecivedMessageOnLoginpage(new string[] { $"DISPLAYALERT", $"Network Issue - Check your Setting." });
            }
        }
        catch (Exception ex)
        {
            RecivedMessageOnLoginpage(new string[] { $"DISPLAYALERT", $"Something went wrong - Check your Setting." });
            Isbussy = false;
            IsVisibleIndecator = false;
        }
        finally
        {
            Isbussy = false;
            IsVisibleIndecator = false;
        }
    }
    private async void  OnCommandLoggedIn()
    {
        try
        {
            Isbussy = true;
            IsVisibleIndecator = true;
            if (SelectedUser != null)
            {
                var senddata = new LoginRequestFilter { Username = SelectedUser?.Username, Password = Password };
                var peopleq = await ApiClient.PostAsync<LoginRequestFilter, User>("User/VerifyUser", senddata);
                if (peopleq != null)
                {
                    if (peopleq.Password.Contains("Invalid login") && peopleq.GroupName.Contains("Invalid login"))
                    {
                        Isbussy = false;
                        IsVisibleIndecator = false;
                        RecivedMessageOnLoginpage(new string[] { $"TOASTALERT", $"Please enter valid user name password." });
                    }
                    else
                    {
                        Isbussy = false;
                        IsVisibleIndecator = false;

                        await SecureStorage.SetAsync("IsLoggedIn", "true");
                        await SecureStorage.SetAsync("Username", peopleq?.Username ?? string.Empty);
                        await SecureStorage.SetAsync("Password", peopleq?.Password ?? string.Empty);
                        UserCode = peopleq?.UserCode ?? string.Empty;
                        UserEmail = peopleq?.Username ?? string.Empty;
                        UserPassword = peopleq?.Password ?? string.Empty;
                        if (Application.Current != null)
                            Application.Current.Windows[0].Page = new AppShell();
                        //RecivedMessageOnLoginpage(new string[] { $"LOGINSUCCESS", $"Login success UserCode : " + peopleq.UserCode + " Username: " + peopleq.Username + " and Password: " + peopleq.Password });
                    }
                }
                else
                {
                    Isbussy = false;
                    IsVisibleIndecator = false;
                    RecivedMessageOnLoginpage(new string[] { $"DISPLAYALERT", $"Network Issue - Check your Setting." });
                }
            }
            else
            {
                RecivedMessageOnLoginpage(new string[] { $"TOASTALERT", $"Please enter valid user name password." });
            }

        }
        catch (Exception ex)
        {
            Isbussy = false;
            IsVisibleIndecator = false;
        }
        finally
        {
            Isbussy = false;
            IsVisibleIndecator = false;
        }


    }
}

public class User
{
    public string? Username { get; set; }
    public string? GroupName { get; set; }
    public string? MacName { get; set; }
    public string? Password { get; set; }
    public string? LDays { get; set; }
    public DateTime LfTime { get; set; }
    public DateTime LtTime { get; set; }
    public string? UserCode { get; set; }

    public double SubDisc { get; set; }
    public double ItemDisc { get; set; }
    public double LessAmt { get; set; }

    public int FreeQty { get; set; }
    public double FreeVal { get; set; }

    public int IdKey { get; set; }
    public string? Administrator { get; set; }

    public string? Updated { get; set; }
    public int LanguageID { get; set; }

    public string? Image { get; set; }
}