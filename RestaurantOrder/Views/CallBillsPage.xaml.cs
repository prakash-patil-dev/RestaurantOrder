
using RestaurantOrder.ViewModels;
using System.Diagnostics;

namespace RestaurantOrder.Views;

public partial class CallBillsPage : ContentPage
{
    INVHEADViewModel VM;
    public CallBillsPage()
    {
        try
        {
            InitializeComponent();
            VM = new();
            BindingContext = VM;
        }
        catch
        {

        }
	}
    protected override async void OnAppearing()
    {
        try
        {
            
            base.OnAppearing();
            _= VM.LoadAllOpenBillsList().ContinueWith(t =>
            {
                if (t.Exception != null)
                {
                    Debug.WriteLine($"Error loading bills: {t.Exception.Flatten().InnerException}");
                }
            }, TaskContinuationOptions.OnlyOnFaulted);
        }
        catch (Exception ex)
        {
            // Handle exception
            await DisplayAlert("Error", "An error occurred while loading bills.", "OK");
        }
    }

    private async void ImageCloseButton_Clicked(object sender, EventArgs e)
    { 
        try
        {
            await Navigation.PopModalAsync(animated:true);
        }
        catch(Exception ex)
        {

        }
    }

    
}